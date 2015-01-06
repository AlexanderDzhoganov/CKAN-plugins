using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CKAN;
using Newtonsoft.Json;

namespace PartManagerPlugin
{

    public partial class PartManagerUI : UserControl
    {

        private Dictionary<string, string> m_DisabledParts = new Dictionary<string, string>();

        private readonly string ConfigPath = "PartManager/PartManager.json";

        private void LoadConfig()
        {
            var fullPath = Path.Combine(Main.Instance.CurrentInstance.CkanDir(), ConfigPath);
            if (!File.Exists(fullPath))
            {
                return;
            }

            var json = File.ReadAllText(fullPath);
            PartManagerConfig config = (PartManagerConfig) JsonConvert.DeserializeObject<PartManagerConfig>(json);
            foreach (var item in config.disabledParts)
            {
                m_DisabledParts.Add(item.Key, item.Value);
            }
        }

        private void SaveConfig()
        {
            var fullPath = Path.Combine(Main.Instance.CurrentInstance.CkanDir(), ConfigPath);
            if (!Directory.Exists(Path.GetDirectoryName(fullPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            }

            PartManagerConfig config = new PartManagerConfig();
            config.disabledParts = m_DisabledParts.ToList();

            var json = JsonConvert.SerializeObject(config);
            File.WriteAllText(fullPath, json);
        }

        private void MovePartToCache(string part)
        {
            var partManagerPath = Path.Combine(Main.Instance.CurrentInstance.CkanDir(), "PartManager");
            if (!Directory.Exists(partManagerPath))
            {
                Directory.CreateDirectory(partManagerPath);
            }

            var cachePath = Path.Combine(partManagerPath, "cache");
            if (!Directory.Exists(cachePath))
            {
                Directory.CreateDirectory(cachePath);
            }

            var fullPath = Path.Combine(Main.Instance.CurrentInstance.GameDir(), part);
            var targetPath = Path.Combine(cachePath, part);

            try
            {
                Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetPath));
            }
            catch (Exception) {}

            File.Move(fullPath, targetPath);
        }

        private void MovePartFromCache(string part)
        {
            var partManagerPath = Path.Combine(Main.Instance.CurrentInstance.CkanDir(), "PartManager");
            if (!Directory.Exists(partManagerPath))
            {
                Directory.CreateDirectory(partManagerPath);
            }

            var cachePath = Path.Combine(partManagerPath, "cache");
            if (!Directory.Exists(cachePath))
            {
                Directory.CreateDirectory(cachePath);
            }

            var fullPath = Path.Combine(cachePath, part);
            if (!Directory.Exists(fullPath))
            {
                return;
            }

            var targetPath = Path.Combine(Main.Instance.CurrentInstance.GameDir(), part);
            try
            {
                Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetPath));
            }
            catch (Exception) {}

            File.Move(fullPath, targetPath);
        }

        public PartManagerUI()
        {
            InitializeComponent();
        }

        private void PartManagerUI_Load(object sender, EventArgs e)
        {
            LoadConfig();
            RefreshInstalledModsList();
        }

        private void RefreshInstalledModsList()
        {
            var installedMods = Main.Instance.CurrentInstance.Registry.Installed();

            InstalledModsListBox.Items.Clear();

            foreach (var mod in installedMods)
            {
                var parts = GetInstalledModParts(mod.Key);
                if (parts != null && parts.Any())
                {
                    InstalledModsListBox.Items.Add(String.Format("{0} | {1}", mod.Key, mod.Value));
                }
            }
        }

        private Dictionary<string, string> GetInstalledModParts(string identifier)
        {
            var registry = Main.Instance.CurrentInstance.Registry;
            var module = registry.InstalledModule(identifier);

            if (module == null)
            {
                return null;
            }

            Dictionary<string, string> parts = new Dictionary<string, string>();

            foreach (var item in module.Files)
            {
                if (m_DisabledParts.ContainsKey(item))
                {
                    parts.Add(item, m_DisabledParts[item]);
                    continue;
                }

                var filename = System.IO.Path.GetFileName(item);

                if (filename.EndsWith(".cfg"))
                {
                    var configNode = LoadPart(item);
                    if (configNode != null)
                    {
                        parts.Add(item, configNode.GetValue("name"));
                    }
                }
            }

            return parts;
        }

        private ConfigNode LoadPart(string part)
        {
            var kspDir = Main.Instance.CurrentInstance.GameDir();
            var fullPath = Path.Combine(kspDir, part);
            if (!File.Exists(fullPath))
            {
                var partManagerPath = Path.Combine(Main.Instance.CurrentInstance.CkanDir(), "PartManager");
                var cachePath = Path.Combine(partManagerPath, "cache");
                fullPath = Path.Combine(cachePath, part);
                if (!File.Exists(fullPath))
                {
                    return null;
                }
            }

            var configNode = ConfigNodeReader.FileToConfigNode(fullPath);
            return configNode;
        }

        private void InstalledModsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (InstalledModsListBox.SelectedItem == null)
            {
                return;
            }

            var item = (InstalledModsListBox.SelectedItem as string).Split('|');
            var identifier = item[0].Trim();

            var parts = GetInstalledModParts(identifier);

            PartsGridView.Rows.Clear();

            foreach (var part in parts)
            {
                var row = new DataGridViewRow();
                row.Tag = part;

                var enabledCheckbox = new DataGridViewCheckBoxCell();
                enabledCheckbox.Value = !m_DisabledParts.ContainsKey(part.Key);
                row.Cells.Add(enabledCheckbox);

                var titleTextbox = new DataGridViewTextBoxCell();
                var configNode = LoadPart(part.Key);
                titleTextbox.Value = configNode.GetValue("title");
                row.Cells.Add(titleTextbox);

                var nameTextbox = new DataGridViewTextBoxCell();
                nameTextbox.Value = part.Value;
                row.Cells.Add(nameTextbox);

                var pathTextbox = new DataGridViewTextBoxCell();
                pathTextbox.Value = part.Key;
                row.Cells.Add(pathTextbox);

                PartsGridView.Rows.Add(row);
            }
        }

        private void PartsGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if (e.ColumnIndex < 0)
            {
                return;
            }

            var grid = sender as DataGridView;
            var row = grid.Rows[e.RowIndex];
            var columnIndex = e.ColumnIndex;

            if (columnIndex != 0)
            {
                return;
            }

            var part = (KeyValuePair<string, string>) row.Tag;

            var gridViewCell = row.Cells[columnIndex] as DataGridViewCheckBoxCell;
            var state = (bool)gridViewCell.Value;
            if (state == false)
            {
                if (m_DisabledParts.ContainsKey(part.Key))
                {
                    return;
                }

                m_DisabledParts.Add(part.Key, part.Value);
                MovePartToCache(part.Key);
                SaveConfig();
            }
            else
            {
                if (!m_DisabledParts.ContainsKey(part.Key))
                {
                    return;
                }

                m_DisabledParts.Remove(part.Key);
                MovePartFromCache(part.Key);
                SaveConfig();
            }
        }
    }
}
