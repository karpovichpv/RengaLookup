using Inspector.Domain;
using Inspector.Model;
using Inspector.View;
using Inspector.ViewModels;
using Inspector.ViewModels.Helpers;
using Renga;

namespace Inspector
{
    public class Plugin : IPlugin
    {
        private IApplication? _app;
        private readonly List<ActionEventSource> _eventSources = [];

        public bool Initialize(string pluginFolder)
        {
            _app = ApplicationSingleton.GetApp();
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
            action.DisplayName = PluginConstants.PluginName;

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

            List<OutObject> selectedObjects = GetSelectedObjects();
            OutObject? firstObject = selectedObjects?.FirstOrDefault();
            OutObject? outObject = selectedObjects?.FirstOrDefault();
            if (firstObject != null && outObject != null && selectedObjects != null)
            {
                var control = new PluginWindow(
                 new ViewModel()
                 {
                     CurrentObject = outObject,
                     SelectedObjects = selectedObjects.ToObservableCollection(),
                     Data = new ChiefCollector(firstObject.Object)
                        .Collect()
                        .ToObservableCollection()
                 });
                control.Show();
            }
        }

        private List<OutObject> GetSelectedObjects()
        {
            List<OutObject> selectedObjects =
                new SelectedObjectsGetter().GetSelected();
            if (selectedObjects.Count == 0 && _app != null)
            {
                return
                [
                    new(_app.Project, $"Project")
                ];
            }

            return selectedObjects;
        }
    }
}
