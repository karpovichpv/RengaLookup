using Renga;
using RengaLookup.Model.Contracts;
using RengaLookup.Plugin.Domain;
using RengaLookup.UIControl;
using RengaLookup.UIControl.ViewModel;
using System.Collections.Generic;
using Application = Renga.Application;

namespace RengaLookup.Plugin
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
			action.ToolTip = "RengaLookup2";
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
				IEnumerable<IInterfaceInfo> collection = getter.Get();

				var control = new PluginWindow(new DesignViewModel(collection));
				control.Show();
			}
		}
	}
}