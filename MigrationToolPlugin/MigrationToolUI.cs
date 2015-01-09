using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using CKAN;
using ICSharpCode.SharpZipLib.Zip;
using log4net;

namespace MigrationToolPlugin
{
    public partial class MigrationToolUI : Form
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MigrationToolUI));

        private BackgroundWorker m_BackgroundWorker = new BackgroundWorker();

        public MigrationToolUI()
        {
            InitializeComponent();
            RefreshModsList();

            m_BackgroundWorker = new BackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
            m_BackgroundWorker.RunWorkerCompleted += PostMigrateMods;
            m_BackgroundWorker.DoWork += MigrateMods;
            SetStatus("Waiting for user");
            SetProgress(0);

            Closing += (sender, args) =>
            {
                if (m_BackgroundWorker.IsBusy)
                {
                    args.Cancel = true;
                    return;
                }

                Main.Instance.UpdateRepo();
            };
        }

        void SetStatus(string message)
        {
            Util.Invoke(StatusLabel, () => StatusLabel.Text = message);
        }

        void SetProgress(int percent)
        {
            Util.Invoke(progressBar1, () => progressBar1.Style = ProgressBarStyle.Continuous);
            Util.Invoke(progressBar1, () => progressBar1.Minimum = 0);
            Util.Invoke(progressBar1, () => progressBar1.Maximum = 100);
            Util.Invoke(progressBar1, () => progressBar1.Value = percent);
        }

        void SetProgressMarquee()
        {
            Util.Invoke(progressBar1, () => progressBar1.Style = ProgressBarStyle.Marquee);
        }

        void RefreshModsList()
        {
            var ad = GetAutodetectedMods();
            FoundModsLabel.Text = String.Format("CKAN found {0} auto-detected mods", ad.Count);

            PossibleMigrateModsListBox.Items.Clear();

            foreach (var identifier in ad)
            {
                PossibleMigrateModsListBox.Items.Add(identifier);
            }
        }

        void MigrateMods(object sender, DoWorkEventArgs e)
        {
            SetStatus("Starting mod migration process");
            SetProgressMarquee();

            var registry = Main.Instance.CurrentInstance.Registry;

            var identifiers =
                (List<string>)e.Argument;

            foreach (var identifier in identifiers)
            {
                CkanModule module = null;

                try
                {
                    module = registry.LatestAvailable(identifier, Main.Instance.CurrentInstance.Version());
                }
                catch (Exception ex)
                {
                    e.Result = ex;
                    return;
                }

                if (module == null)
                {
                    e.Result = null;
                    return;
                }

                var installer = ModuleInstaller.GetInstance(Main.Instance.CurrentInstance, Main.Instance.m_User);
                SetStatus(String.Format("Downloading mod - {0}", identifier));
                string zip = installer.CachedOrDownload(module);

                var files = ModuleInstaller.FindInstallableFiles(module, new ZipFile(zip), Main.Instance.CurrentInstance);

                SetStatus(String.Format("Removing existing files for {0}", identifier));

                foreach (var file in files)
                {
                    if (File.Exists(file.destination))
                    {
                        try
                        {
                            File.Delete(file.destination);
                            log.InfoFormat("Deleted \"{0}\"", file);
                        }
                        catch (Exception ex)
                        {
                            log.WarnFormat("Failed to delete \"{0}\" - {1}", file, ex.Message);
                            break;
                        }
                    }
                }

                SetStatus("Scanning GameData");
                Main.Instance.CurrentInstance.ScanGameData();

                SetStatus("Unregistering auto-detected module");
                // registry.DeregisterModule(Main.Instance.CurrentInstance, identifier);

                var opts = new RelationshipResolverOptions()
                {
                    with_all_suggests = false,
                    with_recommends = true,
                    with_suggests = false,
                    without_toomanyprovides_kraken = true
                };

                SetStatus(String.Format("Installing {0} using CKAN", identifier));
                installer.User = new NullUser();

                bool success = false;
                while (!success)
                {
                    try
                    {
                        installer.InstallList(new List<string>() { identifier }, opts);
                        success = true;
                    }
                    catch (FileExistsKraken ex)
                    {
                        File.Delete(Path.Combine(Main.Instance.CurrentInstance.GameDir(), ex.filename));
                    }
                    catch (Exception ex)
                    {
                        e.Result = ex;
                        return;
                    }
                }
            }
        }

        void PostMigrateMods(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result is Exception)
            {
                var ex = e.Result as Exception;
                if (ex is InconsistentKraken)
                {
                    Main.Instance.m_User.RaiseError("Error: {0}", (ex as InconsistentKraken).InconsistenciesPretty);
                }
                else
                {
                    Main.Instance.m_User.RaiseError("Error: {0}", ex.Message);
                }
            }

            Main.Instance.CurrentInstance.ScanGameData();
            RefreshModsList();
            Main.Instance.Enabled = false;
            SetStatus("Waiting for user");
            SetProgress(0);
            Enabled = true;
        }

        List<string> GetAutodetectedMods()
        {
            var registry = Main.Instance.CurrentInstance.Registry;

            List<string> autodetected = new List<string>();
            foreach (var dll in registry.InstalledDlls)
            {
                try
                {
                    if (registry.LatestAvailable(dll, Main.Instance.CurrentInstance.Version()) != null)
                    {
                        autodetected.Add(dll);
                    }
                }
                catch (Exception ex)
                {
                    log.WarnFormat("Failed to get LatestAvailable() for {0} - {1}", dll, ex.Message);
                }
            }
            return autodetected;
        }

        private void RescanGameDataButton_Click(object sender, EventArgs e)
        {
            Main.Instance.CurrentInstance.ScanGameData();
            RefreshModsList();
        }

        private void MigrateSelectedButton_Click(object sender, EventArgs e)
        {
            if (PossibleMigrateModsListBox.SelectedItems.Count == 0)
            {
                return;
            }

            Enabled = false;
            var mods = new List<string>();
            foreach (var item in PossibleMigrateModsListBox.SelectedItems)
            {
                mods.Add((string)item);   
            }

            m_BackgroundWorker.RunWorkerAsync(mods);
        }

        private void MigrateAllButton_Click(object sender, EventArgs e)
        {
            Enabled = false;
            var mods = new List<string>();
            foreach (var item in PossibleMigrateModsListBox.Items)
            {
                mods.Add((string)item);
            }

            m_BackgroundWorker.RunWorkerAsync(mods);
        }
    }
}
