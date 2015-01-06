using System;
using System.Collections.Generic;
using System.Windows.Forms;
using mshtml;
using CKAN;

namespace KerbalStuffPlugin
{

    public class KerbalStuffPlugin : CKAN.IGUIPlugin
    {

        private readonly CKAN.Version VERSION = new CKAN.Version("v1.0.0");

        private Dictionary<int, CkanModule> KerbalStuffToCkanMap = new Dictionary<int, CkanModule>();

        public override void Initialize()
        {
            var registry = Main.Instance.CurrentInstance.Registry;
            var kspVersion = Main.Instance.CurrentInstance.Version();

            foreach (var module in registry.Available(kspVersion))
            {
                var latest = registry.LatestAvailable(module.identifier, kspVersion);
                if (latest.resources != null)
                {
                    if (latest.resources.kerbalstuff != null)
                    {
                        int ks_id = int.Parse(latest.resources.kerbalstuff.ToString().Split('/')[4]);
                        KerbalStuffToCkanMap[ks_id] = latest;
                    }
                }
            }

            var webBrowser = new WebBrowser();
            webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            webBrowser.Url = new System.Uri("http://kerbalstuff.com", System.UriKind.Absolute);

            webBrowser.DocumentCompleted += (sender, args) =>
            {
                var thumbnails = GetElementsByClass(webBrowser.Document, "thumbnail");
                foreach (var thumbnail in thumbnails)
                {
                    var url = thumbnail.Children[1].GetAttribute("href");
                    var ksmod_id = int.Parse(url.Split('/')[4]);

                    if (CkanModuleForKerbalStuffID(ksmod_id) != null)
                    {
                        thumbnail.Children[0].InnerHtml = "<img src=\"https://raw.githubusercontent.com/KSP-CKAN/CKAN-cmdline/master/assets/ckan-64.png\"/>";
                    }
                }

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

                    if (IsModuleInstalled(ckanModule.identifier))
                    {
                        downloadLink.InnerHtml = "Installed";
                    }
                    else if (IsModuleSelectedForInstall(ckanModule.identifier))
                    {
                        downloadLink.InnerHtml = "Selected for install";
                    }
                    else
                    {
                        downloadLink.InnerHtml = "Add to CKAN install";

                        webBrowser.Document.Body.MouseDown += (o, e) =>
                        {
                            switch (e.MouseButtonsPressed)
                            {
                                case MouseButtons.Left:
                                    HtmlElement element = webBrowser.Document.GetElementFromPoint(e.ClientMousePosition);
                                    if (element != null && element.Id == "download-link-primary")
                                    {
                                        SelectModuleForInstall(ckanModule.identifier);
                                    }

                                    break;
                            }
                        };
                    }
                }
            };

            var tabPage = new TabPage();
            tabPage.Name = "KerbalStuffBrowserTabPage";
            tabPage.Text = "KerbalStuff";

            tabPage.Controls.Add(webBrowser);

            Main.Instance.m_TabController.m_TabPages.Add("KerbalStuffBrowser", tabPage);
            Main.Instance.m_TabController.ShowTab("KerbalStuffBrowser", 1, false);
        }
        static IEnumerable<HtmlElement> GetElementsByClass(HtmlDocument doc, string className)
        {
            foreach (HtmlElement e in doc.All)
                if (e.GetAttribute("className") == className)
                    yield return e;
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
            foreach (DataGridViewRow row in Main.Instance.ModList.Rows)
            {
                var mod = ((GUIMod) row.Tag);
                if (mod.Identifier == identifier)
                {
                    (row.Cells[0] as DataGridViewCheckBoxCell).Value = true;
                    mod.IsInstallChecked = true;
                }
            }
        }

        private CkanModule CkanModuleForKerbalStuffID(int id)
        {
            if (!KerbalStuffToCkanMap.ContainsKey(id))
            {
                return null;
            }

            return KerbalStuffToCkanMap[id];
        }

    }

}
