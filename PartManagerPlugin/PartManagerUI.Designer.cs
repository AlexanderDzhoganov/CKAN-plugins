namespace PartManagerPlugin
{
    partial class PartManagerUI
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.InstalledModsListBox = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.DisableAllButton = new System.Windows.Forms.Button();
            this.EnableAllButton = new System.Windows.Forms.Button();
            this.PartsGridView = new System.Windows.Forms.DataGridView();
            this.EnabledColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.TitleColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PathColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FilterTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.RegexCheckbox = new System.Windows.Forms.CheckBox();
            this.ApplyFilterButton = new System.Windows.Forms.Button();
            this.FilterTypeCombobox = new System.Windows.Forms.ComboBox();
            this.ClearFilterbutton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PartsGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.InstalledModsListBox);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(293, 539);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Installed mods that contain parts";
            // 
            // InstalledModsListBox
            // 
            this.InstalledModsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.InstalledModsListBox.FormattingEnabled = true;
            this.InstalledModsListBox.Location = new System.Drawing.Point(6, 19);
            this.InstalledModsListBox.Name = "InstalledModsListBox";
            this.InstalledModsListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.InstalledModsListBox.Size = new System.Drawing.Size(281, 511);
            this.InstalledModsListBox.TabIndex = 0;
            this.InstalledModsListBox.SelectedIndexChanged += new System.EventHandler(this.InstalledModsListBox_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.DisableAllButton);
            this.groupBox2.Controls.Add(this.EnableAllButton);
            this.groupBox2.Controls.Add(this.PartsGridView);
            this.groupBox2.Location = new System.Drawing.Point(302, 46);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(628, 496);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Mod parts";
            // 
            // DisableAllButton
            // 
            this.DisableAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DisableAllButton.Location = new System.Drawing.Point(87, 464);
            this.DisableAllButton.Name = "DisableAllButton";
            this.DisableAllButton.Size = new System.Drawing.Size(75, 23);
            this.DisableAllButton.TabIndex = 9;
            this.DisableAllButton.Text = "Disable all";
            this.DisableAllButton.UseVisualStyleBackColor = true;
            this.DisableAllButton.Click += new System.EventHandler(this.DisableAllButton_Click);
            // 
            // EnableAllButton
            // 
            this.EnableAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.EnableAllButton.Location = new System.Drawing.Point(6, 464);
            this.EnableAllButton.Name = "EnableAllButton";
            this.EnableAllButton.Size = new System.Drawing.Size(75, 23);
            this.EnableAllButton.TabIndex = 8;
            this.EnableAllButton.Text = "Enable all";
            this.EnableAllButton.UseVisualStyleBackColor = true;
            this.EnableAllButton.Click += new System.EventHandler(this.EnableAllButton_Click);
            // 
            // PartsGridView
            // 
            this.PartsGridView.AllowUserToAddRows = false;
            this.PartsGridView.AllowUserToDeleteRows = false;
            this.PartsGridView.AllowUserToResizeRows = false;
            this.PartsGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PartsGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.PartsGridView.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.PartsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PartsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EnabledColumn,
            this.TitleColumn,
            this.PartNameColumn,
            this.PathColumn});
            this.PartsGridView.Location = new System.Drawing.Point(6, 19);
            this.PartsGridView.Name = "PartsGridView";
            this.PartsGridView.RowHeadersVisible = false;
            this.PartsGridView.Size = new System.Drawing.Size(616, 439);
            this.PartsGridView.TabIndex = 0;
            this.PartsGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.PartsGridView_CellValueChanged);
            // 
            // EnabledColumn
            // 
            this.EnabledColumn.HeaderText = "Enabled";
            this.EnabledColumn.Name = "EnabledColumn";
            this.EnabledColumn.Width = 52;
            // 
            // TitleColumn
            // 
            this.TitleColumn.HeaderText = "Title";
            this.TitleColumn.Name = "TitleColumn";
            this.TitleColumn.ReadOnly = true;
            this.TitleColumn.Width = 52;
            // 
            // PartNameColumn
            // 
            this.PartNameColumn.HeaderText = "Part name";
            this.PartNameColumn.Name = "PartNameColumn";
            this.PartNameColumn.ReadOnly = true;
            this.PartNameColumn.Width = 80;
            // 
            // PathColumn
            // 
            this.PathColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PathColumn.HeaderText = "Path";
            this.PathColumn.Name = "PathColumn";
            this.PathColumn.ReadOnly = true;
            // 
            // FilterTextBox
            // 
            this.FilterTextBox.Location = new System.Drawing.Point(467, 14);
            this.FilterTextBox.Name = "FilterTextBox";
            this.FilterTextBox.Size = new System.Drawing.Size(220, 20);
            this.FilterTextBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(305, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Filter:";
            // 
            // RegexCheckbox
            // 
            this.RegexCheckbox.AutoSize = true;
            this.RegexCheckbox.Location = new System.Drawing.Point(693, 16);
            this.RegexCheckbox.Name = "RegexCheckbox";
            this.RegexCheckbox.Size = new System.Drawing.Size(57, 17);
            this.RegexCheckbox.TabIndex = 4;
            this.RegexCheckbox.Text = "Regex";
            this.RegexCheckbox.UseVisualStyleBackColor = true;
            // 
            // ApplyFilterButton
            // 
            this.ApplyFilterButton.Location = new System.Drawing.Point(756, 11);
            this.ApplyFilterButton.Name = "ApplyFilterButton";
            this.ApplyFilterButton.Size = new System.Drawing.Size(75, 23);
            this.ApplyFilterButton.TabIndex = 5;
            this.ApplyFilterButton.Text = "Apply filter";
            this.ApplyFilterButton.UseVisualStyleBackColor = true;
            this.ApplyFilterButton.Click += new System.EventHandler(this.ApplyFilterButton_Click);
            // 
            // FilterTypeCombobox
            // 
            this.FilterTypeCombobox.FormattingEnabled = true;
            this.FilterTypeCombobox.Items.AddRange(new object[] {
            "Title",
            "Name",
            "Path"});
            this.FilterTypeCombobox.Location = new System.Drawing.Point(340, 14);
            this.FilterTypeCombobox.Name = "FilterTypeCombobox";
            this.FilterTypeCombobox.Size = new System.Drawing.Size(121, 21);
            this.FilterTypeCombobox.TabIndex = 6;
            this.FilterTypeCombobox.Text = "Path";
            // 
            // ClearFilterbutton
            // 
            this.ClearFilterbutton.Enabled = false;
            this.ClearFilterbutton.Location = new System.Drawing.Point(837, 11);
            this.ClearFilterbutton.Name = "ClearFilterbutton";
            this.ClearFilterbutton.Size = new System.Drawing.Size(75, 23);
            this.ClearFilterbutton.TabIndex = 7;
            this.ClearFilterbutton.Text = "Clear filter";
            this.ClearFilterbutton.UseVisualStyleBackColor = true;
            this.ClearFilterbutton.Click += new System.EventHandler(this.ClearFilterbutton_Click);
            // 
            // PartManagerUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ClearFilterbutton);
            this.Controls.Add(this.FilterTypeCombobox);
            this.Controls.Add(this.ApplyFilterButton);
            this.Controls.Add(this.RegexCheckbox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FilterTextBox);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "PartManagerUI";
            this.Size = new System.Drawing.Size(933, 546);
            this.Load += new System.EventHandler(this.PartManagerUI_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PartsGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox InstalledModsListBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView PartsGridView;
        private System.Windows.Forms.TextBox FilterTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox RegexCheckbox;
        private System.Windows.Forms.Button ApplyFilterButton;
        private System.Windows.Forms.ComboBox FilterTypeCombobox;
        private System.Windows.Forms.Button ClearFilterbutton;
        private System.Windows.Forms.DataGridViewCheckBoxColumn EnabledColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn TitleColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PathColumn;
        private System.Windows.Forms.Button DisableAllButton;
        private System.Windows.Forms.Button EnableAllButton;
    }
}
