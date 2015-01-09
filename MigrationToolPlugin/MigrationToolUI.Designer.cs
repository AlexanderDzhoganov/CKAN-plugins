namespace MigrationToolPlugin
{
    partial class MigrationToolUI
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.FoundModsLabel = new System.Windows.Forms.Label();
            this.RescanGameDataButton = new System.Windows.Forms.Button();
            this.PossibleMigrateModsListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.MigrateSelectedButton = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.MigrateAllButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // FoundModsLabel
            // 
            this.FoundModsLabel.AutoSize = true;
            this.FoundModsLabel.Location = new System.Drawing.Point(8, 17);
            this.FoundModsLabel.Name = "FoundModsLabel";
            this.FoundModsLabel.Size = new System.Drawing.Size(180, 13);
            this.FoundModsLabel.TabIndex = 0;
            this.FoundModsLabel.Text = "CKAN found {0} auto-detected mods";
            // 
            // RescanGameDataButton
            // 
            this.RescanGameDataButton.Location = new System.Drawing.Point(194, 12);
            this.RescanGameDataButton.Name = "RescanGameDataButton";
            this.RescanGameDataButton.Size = new System.Drawing.Size(112, 23);
            this.RescanGameDataButton.TabIndex = 1;
            this.RescanGameDataButton.Text = "Rescan GameData";
            this.RescanGameDataButton.UseVisualStyleBackColor = true;
            this.RescanGameDataButton.Click += new System.EventHandler(this.RescanGameDataButton_Click);
            // 
            // PossibleMigrateModsListBox
            // 
            this.PossibleMigrateModsListBox.FormattingEnabled = true;
            this.PossibleMigrateModsListBox.Location = new System.Drawing.Point(11, 64);
            this.PossibleMigrateModsListBox.Name = "PossibleMigrateModsListBox";
            this.PossibleMigrateModsListBox.Size = new System.Drawing.Size(269, 290);
            this.PossibleMigrateModsListBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(186, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Mods that can be managed by CKAN:";
            // 
            // MigrateSelectedButton
            // 
            this.MigrateSelectedButton.Location = new System.Drawing.Point(286, 64);
            this.MigrateSelectedButton.Name = "MigrateSelectedButton";
            this.MigrateSelectedButton.Size = new System.Drawing.Size(168, 23);
            this.MigrateSelectedButton.TabIndex = 4;
            this.MigrateSelectedButton.Text = "Migrate selected";
            this.MigrateSelectedButton.UseVisualStyleBackColor = true;
            this.MigrateSelectedButton.Click += new System.EventHandler(this.MigrateSelectedButton_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(286, 331);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(168, 23);
            this.progressBar1.TabIndex = 5;
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Location = new System.Drawing.Point(283, 315);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(117, 13);
            this.StatusLabel.TabIndex = 6;
            this.StatusLabel.Text = "Status: Waiting for user";
            // 
            // MigrateAllButton
            // 
            this.MigrateAllButton.Location = new System.Drawing.Point(286, 93);
            this.MigrateAllButton.Name = "MigrateAllButton";
            this.MigrateAllButton.Size = new System.Drawing.Size(168, 23);
            this.MigrateAllButton.TabIndex = 7;
            this.MigrateAllButton.Text = "Migrate all";
            this.MigrateAllButton.UseVisualStyleBackColor = true;
            this.MigrateAllButton.Click += new System.EventHandler(this.MigrateAllButton_Click);
            // 
            // MigrationToolUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 369);
            this.Controls.Add(this.MigrateAllButton);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.MigrateSelectedButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PossibleMigrateModsListBox);
            this.Controls.Add(this.RescanGameDataButton);
            this.Controls.Add(this.FoundModsLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MigrationToolUI";
            this.Text = "Migration Tool";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label FoundModsLabel;
        private System.Windows.Forms.Button RescanGameDataButton;
        private System.Windows.Forms.ListBox PossibleMigrateModsListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button MigrateSelectedButton;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.Button MigrateAllButton;
    }
}