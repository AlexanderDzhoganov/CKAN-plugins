using System;
using System.Windows.Forms;
using CKAN;
using Newtonsoft.Json;

namespace MigrationToolPlugin
{

    public class MigrationToolPlugin : CKAN.IGUIPlugin
    {

        private readonly CKAN.Version VERSION = new CKAN.Version("v1.0.0");

        private MigrationToolUI m_Form = null;
        private ToolStripMenuItem m_MenuItem = null;

        public override void Initialize()
        {
            ToolStripMenuItem menuItem = new ToolStripMenuItem();

            menuItem.Name = "migrationToolMenuItem";
            menuItem.Size = new System.Drawing.Size(176, 22);
            menuItem.Text = "Migration Tool";
            menuItem.Click += menuItem_Click;
            Main.Instance.settingsToolStripMenuItem.DropDownItems.Add(menuItem);
            m_MenuItem = menuItem;
        }
     
        public override void Deinitialize()
        {
            Main.Instance.settingsToolStripMenuItem.DropDownItems.Remove(m_MenuItem);
        }

        private void menuItem_Click(object sender, EventArgs e)
        {
            Main.Instance.Enabled = false;
            m_Form = new MigrationToolUI();
            m_Form.ShowDialog();
            Main.Instance.Enabled = true;
        }

        public override string GetName()
        {
            return "MigrationTool by nlight";
        }

        public override CKAN.Version GetVersion()
        {
            return VERSION;
        }

    }

}
