using Renga;
using RengaLookup.Plugin.Domain.Model;
using RengaLookup.Plugin2.Domain;
using RengaLookup.Plugin2.Domain.Model;
using System.Collections.Generic;
using System.Text;
using Application = Renga.Application;

namespace RengaLookup.Plugin2
{
	public class Plugin : IPlugin
	{
		private readonly List<ActionEventSource> _eventSources = new List<ActionEventSource>();

		private IApplication _app;

		public bool Initialize(string pluginFolder)
		{
			IApplication app = new Application();
			_app = app;
			IUI ui = app.UI;

			IUIPanelExtension extension = ui.CreateUIPanelExtension();
			extension.AddToolButton(CreateChangeViewStyleAction(ui));
			ui.AddExtensionToPrimaryPanel(extension);

			return true;
		}

		public void Stop()
		{
			foreach (ActionEventSource eventSource in _eventSources)
				eventSource.Dispose();

			_eventSources.Clear();
		}

		private IAction CreateChangeViewStyleAction(IUI ui)
		{
			IAction action = ui.CreateAction();
			action.ToolTip = "RengaLookup";
			ActionEventSource source = new ActionEventSource(action);
			_eventSources.Add(source);
			source.Triggered += (sender, arguments) =>
			{
				ShowInfoAboutObject(ui);
			};

			return action;
		}

		private void ShowInfoAboutObject(IUI ui)
		{
			if (_app is null)
				return;

			IModel model = _app.Project.Model;
			if (model is null)
				return;

			ISelection selection = _app.Selection;
			int[] array = (int[])selection.GetSelectedObjects();

			IModelObjectCollection modelObjects = model.GetObjects();
			foreach (int index in array)
			{
				IModelObject modelObject = modelObjects.GetById(index);
				if (modelObject != null)
					ShowMessageBox(ui, "Luck", modelObject.Id.ToString(), modelObject);
				else
					ShowMessageBox(ui, "Fail", "Object is null", null);
			}
		}

		private static void ShowMessageBox(IUI ui, string title, string id, IModelObject modelObject)
		{
			string info = string.Empty;
			if (modelObject != null)
			{
				RengaInfoGetter getter = new RengaInfoGetter(modelObject);
				info = ConvertToString(getter.Get());
			}

			ui.ShowMessageBox(
				MessageIcon.MessageIcon_Info,
				title,
				$"{id}\r\n{info}");
		}

		private static string ConvertToString(IEnumerable<InterfaceEntry> collection)
		{
			StringBuilder builder = new StringBuilder();
			foreach (InterfaceEntry entry in collection)
			{
				builder.AppendLine("--------");
				builder.AppendLine(entry.Name);

				if (entry.Infos != null)
				{
					foreach (Data data in entry.Infos)
					{
						if (data is PropertyData)
							builder.AppendLine($"Property: {data.Label}, Value: {data.Value}");

						if (data is FieldData)
							builder.AppendLine($"Field: {data.Label}, Value: {data.Value}");
					}
				}
			}

			return builder.ToString();
		}
	}
}