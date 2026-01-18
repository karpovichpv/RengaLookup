using Renga;
using RengaLookup.Plugin2.Domain;
using RengaLookup.Plugin2.Model;
using RengaLookup.Plugin2.View;
using RengaLookup.Plugin2.ViewModels;
using RengaLookup.Plugin2.ViewModels.Helpers;

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

            List<RengaObject> selectedObjects = GetSelectedObjects(model);
            RengaObject firstObject = selectedObjects.FirstOrDefault();
            var control = new PluginWindow(
             new ViewModel()
             {
                 CurrentObject = selectedObjects.FirstOrDefault(),
                 SelectedObjects = selectedObjects.ToObservableCollection(),
                 Data = new ChiefCollector(firstObject.Object)
                    .Collect()
                    .ToObservableCollection()
             });
            control.Show();
        }

        private static List<RengaObject> GetSelectedObjects(IModel model)
        {
            List<RengaObject> selectedObjects =
                new SelectedObjectsGetter().GetSelected();
            if (selectedObjects.Count == 0)
            {
                return
                [
                    new() { Name = $"Model {model.Id}", Object = model }
                ];
            }

            return selectedObjects;
        }
    }
}
