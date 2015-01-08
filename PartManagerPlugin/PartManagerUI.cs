using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CKAN;
using Newtonsoft.Json;

namespace PartManagerPlugin
{

    public enum FilterType
    {
        Path,
        Name,
        Title
    }

    public partial class PartManagerUI : UserControl
    {

        private Dictionary<string, ConfigNode> m_DisabledParts = new Dictionary<string, ConfigNode>();

        private readonly string ConfigPath = "PartManager/PartManager.json";

        private string m_Filter = null;
        private bool m_FilterRegex = false;
        private FilterType m_FilterType;

        private void LoadConfig()
        {
            var fullPath = Path.Combine(Main.Instance.CurrentInstance.CkanDir(), ConfigPath);
            if (!File.Exists(fullPath))
            {
                return;
            }

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

            var json = File.ReadAllText(fullPath);
            PartManagerConfig config = (PartManagerConfig) JsonConvert.DeserializeObject<PartManagerConfig>(json);
            foreach (var item in config.disabledParts)
            {
                m_DisabledParts.Add(item.Key, ConfigNodeReader.FileToConfigNode(Path.Combine(cachePath, item.Key)));
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
            config.disabledParts = new List<KeyValuePair<string, string>>();
            foreach (var part in m_DisabledParts)
            {
                config.disabledParts.Add(new KeyValuePair<string, string>(part.Key, null));
            }

            var json = JsonConvert.SerializeObject(config);
            File.WriteAllText(fullPath, json);
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

        public void OnModChanged(CkanModule module, GUIModChangeType changeType)
        {
            if (changeType == GUIModChangeType.Update || changeType == GUIModChangeType.Install)
            {
                var parts = GetInstalledModParts(module.identifier);
                foreach (var part in parts)
                {
                    if (m_DisabledParts.ContainsKey(part.Key))
                    {
                        Cache.RemovePartFromCache(part.Key);
                        Cache.MovePartToCache(part.Key);
                    }
                }
            }

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

        private Dictionary<string, ConfigNode> GetInstalledModParts(string identifier)
        {
            var registry = Main.Instance.CurrentInstance.Registry;
            var module = registry.InstalledModule(identifier);

            if (module == null)
            {
                return null;
            }

            Dictionary<string, ConfigNode> parts = new Dictionary<string, ConfigNode>();

            foreach (var item in module.Files)
            {
                if (m_DisabledParts.ContainsKey(item))
                {
                    parts.Add(item, m_DisabledParts[item]);
                    continue;
                }

                var filename = Path.GetFileName(item);

                if (filename.EndsWith(".cfg"))
                {
                    var configNode = LoadPart(item);
                    if (configNode != null)
                    {
                        parts.Add(item, configNode);
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
            if (InstalledModsListBox.SelectedItems == null || InstalledModsListBox.SelectedItems.Count == 0)
            {
                return;
            }

            PartsGridView.Rows.Clear();

            foreach (var selectedItem in InstalledModsListBox.SelectedItems)
            {
                var item = (selectedItem as string).Split('|');
                var identifier = item[0].Trim();

                var parts = GetInstalledModParts(identifier);

                foreach (var part in parts)
                {
                    if (m_FilterType == FilterType.Path && !FilterString(part.Key))
                    {
                        continue;
                    }

                    var row = new DataGridViewRow();
                    row.Tag = part;

                    var enabledCheckbox = new DataGridViewCheckBoxCell();
                    enabledCheckbox.Value = !m_DisabledParts.ContainsKey(part.Key);
                    row.Cells.Add(enabledCheckbox);

                    var titleTextbox = new DataGridViewTextBoxCell();
                    var title = part.Value.GetValue("title");

                    if (m_FilterType == FilterType.Title && !FilterString(title))
                    {
                        continue;
                    }

                    titleTextbox.Value = title;

                    row.Cells.Add(titleTextbox);

                    var nameTextbox = new DataGridViewTextBoxCell();
                    nameTextbox.Value = part.Value.GetValue("name");
                    row.Cells.Add(nameTextbox);

                    if (m_FilterType == FilterType.Name && !FilterString(part.Value.GetValue("name")))
                    {
                        continue;
                    }

                    var pathTextbox = new DataGridViewTextBoxCell();
                    pathTextbox.Value = part.Key;
                    row.Cells.Add(pathTextbox);

                    PartsGridView.Rows.Add(row);
                }
            }
        }

        private bool FilterString(string value)
        {
            if (m_Filter == null)
            {
                return true;
            }

            if (value == null)
            {
                return false;
            }

            if (m_Filter.Length == 0)
            {
                return true;
            }

            if (m_FilterRegex)
            {
                try
                {
                    return Regex.IsMatch(value, m_Filter);
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                return value.ToLower().Contains(m_Filter.ToLower());
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

            var part = (KeyValuePair<string, ConfigNode>) row.Tag;

            var gridViewCell = row.Cells[columnIndex] as DataGridViewCheckBoxCell;
            var state = (bool)gridViewCell.Value;
            if (state == false)
            {
                if (m_DisabledParts.ContainsKey(part.Key))
                {
                    return;
                }

                m_DisabledParts.Add(part.Key, part.Value);
                Cache.MovePartToCache(part.Key);
                SaveConfig();
            }
            else
            {
                if (!m_DisabledParts.ContainsKey(part.Key))
                {
                    return;
                }

                m_DisabledParts.Remove(part.Key);
                Cache.MovePartFromCache(part.Key);
                SaveConfig();
            }
        }

        private void ApplyFilterButton_Click(object sender, EventArgs e)
        {
            m_Filter = FilterTextBox.Text;
            m_FilterRegex = RegexCheckbox.Checked;
            try
            {
                m_FilterType = (FilterType)Enum.Parse(typeof(FilterType), FilterTypeCombobox.Text, true);
            }
            catch (Exception)
            {
                FilterTypeCombobox.Text = "Path";
                m_FilterType = FilterType.Path;                
            }
            InstalledModsListBox_SelectedIndexChanged(null, new EventArgs());
        }

        private void ClearFilterbutton_Click(object sender, EventArgs e)
        {
            m_Filter = null;
            InstalledModsListBox_SelectedIndexChanged(null, new EventArgs());
        }

        private void EnableAllButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in PartsGridView.Rows)
            {
                (row.Cells[0] as DataGridViewCheckBoxCell).Value = true;
            }
        }

        private void DisableAllButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in PartsGridView.Rows)
            {
                (row.Cells[0] as DataGridViewCheckBoxCell).Value = false;
            }
        }

    }
}
