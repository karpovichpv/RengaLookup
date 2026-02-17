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
        private IImage? _icon;
        private readonly List<ActionEventSource> _eventSources = [];

        public bool Initialize(string pluginFolder)
        {
            try
            {

                _app = ApplicationSingleton.GetApp();
                var ui = _app.UI;

                var panelExtension = ui.CreateUIPanelExtension();
                string icoPath = pluginFolder + @"\ico.png";
                _icon = ui.CreateImage();
                _icon?.LoadFromFile(icoPath);

                panelExtension.AddToolButton(CreateAction(ui));

                ui.AddExtensionToPrimaryPanel(panelExtension);
            }
            catch (Exception ex)
            {
                CrashLogCreator.WriteCrashLog(ex, GetType());
            }

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
            throw new Exception("Some ex");

            IAction action = ui.CreateAction();
            action.DisplayName = PluginConstants.PluginName;
            action.Icon = _icon;

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

            List<IOutObject> selectedObjects = GetSelectedObjects();
            IOutObject? firstObject = selectedObjects?.FirstOrDefault();
            IOutObject? outObject = selectedObjects?.FirstOrDefault();
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

        private List<IOutObject> GetSelectedObjects()
        {
            List<IOutObject> selectedObjects =
                new SelectedObjectsGetter().GetSelected();
            if (selectedObjects.Count == 0 && _app != null)
            {
                return
                [
                    new OutObject(_app.Project, $"Project")
                ];
            }

            return selectedObjects;
        }
    }
}
