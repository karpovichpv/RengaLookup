using Renga;
using RengaLookup.Plugin.Domain;

namespace RengaLookup.Plugin
{
	public class Plugin : IPlugin
	{
		private readonly List<ActionEventSource> _eventSources = [];

		private IApplication? _app;

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
			ActionEventSource source = new(action);
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
				if (modelObject is not null)
					ShowMessageBox(ui, "Luck", modelObject.Id.ToString(), modelObject);
				else
					ShowMessageBox(ui, "Fail", "Object is null", null);
			}
		}

		private static void ShowMessageBox(IUI ui, string title, string id, IModelObject? modelObject)
		{
			string info = string.Empty;
			if (modelObject is not null)
			{
				RengaInfoGetter getter = new(modelObject);
				info = getter.Get();
			}

			ui.ShowMessageBox(
				MessageIcon.MessageIcon_Info,
				title,
				$"{id}\r\n{info}");
		}
	}
}