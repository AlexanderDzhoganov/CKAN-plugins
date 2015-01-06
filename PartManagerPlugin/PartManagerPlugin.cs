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

        private readonly CKAN.Version VERSION = new CKAN.Version("v1.0.0");

        public override void Initialize()
        {
            var tabPage = new TabPage();
            tabPage.Name = "PartManager";
            tabPage.Text = "PartManager";

            var partManager = new PartManagerUI();
            partManager.Dock = DockStyle.Fill;
            tabPage.Controls.Add(partManager);

            Main.Instance.m_TabController.m_TabPages.Add("PartManager", tabPage);
            Main.Instance.m_TabController.ShowTab("PartManager", 1, false);
        }

        public override void Deinitialize()
        {
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
