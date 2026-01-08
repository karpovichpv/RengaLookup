using Renga;
using RengaLookup.Plugin2.Domain;
using RengaLookup.Plugin2.Model.Contracts;
using RengaLookup.Plugin2.View;
using RengaLookup.Plugin2.ViewModels;

namespace RengaLookup.Plugin2
{
    public class Plugin : IPlugin
    {
        private IApplication _app;
        private readonly List<ActionEventSource> _eventSources = [];

        public bool Initialize(string pluginFolder)
        {
            _app = new Application();
            var ui = _app.UI;
            var panelExtension = ui.CreateUIPanelExtension();

            panelExtension.AddToolButton(CreateAction(ui));

            ui.AddExtensionToPrimaryPanel(panelExtension);

            return true;
        }

        public void Stop()
        {
            foreach (var eventSource in _eventSources)
                eventSource.Dispose();

            _eventSources.Clear();
        }

        public IAction CreateAction(IUI ui)
        {
            var action = ui.CreateAction();
            action.DisplayName = "Renga Lookup 2";

            var events = new ActionEventSource(action);
            events.Triggered += (s, e) =>
            {
                ShowInfoAboutObject();
            };
            _eventSources.Add(events);

            return action;
        }

        private void ShowInfoAboutObject()
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
                    ShowMessageBox(modelObject);
            }
        }

        private static void ShowMessageBox(IModelObject modelObject)
        {
            if (modelObject != null)
            {
                var getter = new RengaInfoGetter(modelObject);
                IEnumerable<IInterfaceInfo> collection = getter.Get();

                var control = new PluginWindow(new DesignViewModel(collection));
                control.Show();
            }
        }
    }
}
