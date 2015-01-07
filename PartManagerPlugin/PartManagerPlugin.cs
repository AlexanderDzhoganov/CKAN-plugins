using System.Collections.Generic;
using System.Windows.Forms;
using CKAN;

namespace PartManagerPlugin
{

    public class PartManagerConfig
    {
        public List<KeyValuePair<string, string>> disabledParts;
    }

    public class PartManagerPlugin : CKAN.IGUIPlugin
    {

        private readonly CKAN.Version VERSION = new CKAN.Version("v1.1.0");

        private PartManagerUI m_UI = null;

        public override void Initialize()
        {
            var tabPage = new TabPage();
            tabPage.Name = "PartManager";
            tabPage.Text = "PartManager";

            m_UI = new PartManagerUI();
            m_UI.Dock = DockStyle.Fill;
            tabPage.Controls.Add(m_UI);

            Main.modChangedCallback += m_UI.OnModChanged;
            Main.Instance.m_TabController.m_TabPages.Add("PartManager", tabPage);
            Main.Instance.m_TabController.ShowTab("PartManager", 1, false);
        }

        public override void Deinitialize()
        {
            Main.modChangedCallback -= m_UI.OnModChanged;
            Main.Instance.m_TabController.HideTab("PartManager");
            Main.Instance.m_TabController.m_TabPages.Remove("PartManager");
        }

        public override string GetName()
        {
            return "PartManager by nlight";
        }

        public override CKAN.Version GetVersion()
        {
            return VERSION;
        }

    }

}
