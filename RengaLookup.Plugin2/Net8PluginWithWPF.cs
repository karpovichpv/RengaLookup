/*  Sample: Net8 Plugin with WPF
 *
 *  This sample illustrates how to create Net8 plugin with WPF.
 *
 *  Copyright Renga Software LLC, 2025. All rights reserved.
 */

using Renga;

namespace Net8PluginWithWPF
{
  public class Net8PluginWithWPF : Renga.IPlugin
  {
    private Renga.IApplication m_app;
    private readonly List<Renga.ActionEventSource> m_eventSources = new List<Renga.ActionEventSource>();

    public bool Initialize(string pluginFolder)
    {
      m_app = new Renga.Application();
      var ui = m_app.UI;
      var panelExtension = ui.CreateUIPanelExtension();

      panelExtension.AddToolButton(CreateAction(ui));

      ui.AddExtensionToPrimaryPanel(panelExtension);

      return true;
    }

    public void Stop()
    {
      foreach (var eventSource in m_eventSources)
        eventSource.Dispose();

      m_eventSources.Clear();
    }

    public IAction CreateAction(IUI ui)
    {
      var action = ui.CreateAction();
      action.DisplayName = "Net8 plugin with WPF";

      var events = new Renga.ActionEventSource(action);
      events.Triggered += (s, e) =>
      {
        CustomWindow window = new CustomWindow();
        window.Show();
      };
      m_eventSources.Add(events);

      return action;
    }
  }
}
