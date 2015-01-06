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
            this.PartsGridView = new System.Windows.Forms.DataGridView();
            this.EnabledColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.TitleColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PathColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.InstalledModsListBox.Size = new System.Drawing.Size(281, 511);
            this.InstalledModsListBox.TabIndex = 0;
            this.InstalledModsListBox.SelectedIndexChanged += new System.EventHandler(this.InstalledModsListBox_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.PartsGridView);
            this.groupBox2.Location = new System.Drawing.Point(302, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(558, 539);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Mod parts";
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
            this.PartsGridView.Size = new System.Drawing.Size(546, 514);
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
            this.PathColumn.HeaderText = "Path";
            this.PathColumn.Name = "PathColumn";
            this.PathColumn.ReadOnly = true;
            this.PathColumn.Width = 54;
            // 
            // PartManagerUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "PartManagerUI";
            this.Size = new System.Drawing.Size(863, 546);
            this.Load += new System.EventHandler(this.PartManagerUI_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PartsGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox InstalledModsListBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView PartsGridView;
        private System.Windows.Forms.DataGridViewCheckBoxColumn EnabledColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn TitleColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PathColumn;
    }
}
