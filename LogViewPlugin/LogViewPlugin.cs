using System.Windows.Forms;
using CKAN;
using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Repository.Hierarchy;

namespace LogViewPlugin
{

    public class LogViewPlugin : CKAN.IGUIPlugin
    {

        private readonly CKAN.Version VERSION = new CKAN.Version("v1.0.0");

        public override void Initialize()
        {
            var tabPage = new TabPage();
            tabPage.Name = "LogViewTabPage";
            tabPage.Text = "LogView";

            var textbox = new TextBox();
            textbox.Dock = System.Windows.Forms.DockStyle.Fill;
            textbox.Multiline = true;
            textbox.Name = "LogViewTextBox";
            textbox.ReadOnly = true;

            tabPage.Controls.Add(textbox);

            Main.Instance.m_TabController.m_TabPages.Add("LogViewTabPage", tabPage);
            Main.Instance.m_TabController.ShowTab("LogViewTabPage", 1, false);

            Hierarchy h = (Hierarchy)LogManager.GetRepository();
            h.Root.Level = Level.All;

            IAppender appender = new LogAppender(textbox);
            h.Root.AddAppender(appender);
        }

        public override void Deinitialize()
        {
            Main.Instance.m_TabController.HideTab("LogViewTabPage");
            Main.Instance.m_TabController.m_TabPages.Remove("LogViewTabPage");
        }

        public override string GetName()
        {
            return "LogView";
        }

        public override CKAN.Version GetVersion()
        {
            return VERSION;
        }

    }

}
