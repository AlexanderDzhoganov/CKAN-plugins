using System;
using System.Windows.Forms;
using mshtml;
using CKAN;

namespace KerbalStuffPlugin
{

    public class KerbalStuffPlugin : CKAN.IGUIPlugin
    {

        private readonly CKAN.Version VERSION = new CKAN.Version("v1.0.0");

        public override void Initialize()
        {
            var webBrowser = new WebBrowser();
            webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            webBrowser.Url = new System.Uri("http://kerbalstuff.com", System.UriKind.Absolute);

            webBrowser.DocumentCompleted += (sender, args) =>
            {
                HtmlElement downloadLink = webBrowser.Document.GetElementById("download-link-primary");
                if (downloadLink == null)
                {
                    return;
                }

                int mod_id = -1;

                var downloadUrl = downloadLink.GetAttribute("href");
                if (downloadUrl.StartsWith("#"))
                {
                    mod_id = int.Parse(downloadUrl.Substring(1));
                }
                else if (!int.TryParse(downloadUrl.Split('/')[4], out mod_id))
                {
                    mod_id = -1;
                }

                var ckanModule = CkanModuleForKerbalStuffID(mod_id);
                if (ckanModule != null)
                {
                    downloadLink.SetAttribute("href", "#" + mod_id.ToString());
                    downloadLink.InnerHtml = "Add to CKAN install";
                }
                else
                {
                    downloadLink.InnerHtml += "(Manually)";
                }
            };

            var tabPage = new TabPage();
            tabPage.Name = "KerbalStuffBrowserTabPage";
            tabPage.Text = "KerbalStuff";

            tabPage.Controls.Add(webBrowser);

            Main.Instance.m_TabController.m_TabPages.Add("KerbalStuffBrowser", tabPage);
            Main.Instance.m_TabController.ShowTab("KerbalStuffBrowser", 1, false);
        }

        public override void Deinitialize()
        {
            Main.Instance.m_TabController.HideTab("KerbalStuffBrowser");
            Main.Instance.m_TabController.m_TabPages.Remove("KerbalStuffBrowser");
        }

        public override string GetName()
        {
            return "KerbalStuffPlugin by nlight";
        }

        public override CKAN.Version GetVersion()
        {
            return VERSION;
        }

        private bool IsModuleInstalled(string identifier)
        {
            var registry = Main.Instance.CurrentInstance.Registry;
            return registry.IsInstalled(identifier);
        }

        private bool IsModuleSelectedForInstall(string identifier)
        {
            if (IsModuleInstalled(identifier)) return false;

            var registry = Main.Instance.CurrentInstance.Registry;
            var changeset = Main.Instance.mainModList.ComputeChangeSetFromModList(registry,
                Main.Instance.CurrentInstance);

            foreach (var change in changeset)
            {
                if (change.Value == GUIModChangeType.Install)
                {
                    if (change.Key.identifier == identifier)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void SelectModuleForInstall(string identifier)
        {
            
        }

        private CkanModule CkanModuleForKerbalStuffID(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            var registry = Main.Instance.CurrentInstance.Registry;
            var kspVersion = Main.Instance.CurrentInstance.Version();

            foreach (var module in registry.Available(kspVersion))
            {
                var latest = registry.LatestAvailable(module.identifier, kspVersion);
                if (latest.resources != null)
                {
                    if (latest.resources.kerbalstuff != null)
                    {
                        if (latest.resources.kerbalstuff.ToString().Split('/')[4] == id.ToString())
                        {
                            return latest;
                        }
                    }
                }
            }

            return null;
        }

    }

}
