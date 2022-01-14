
namespace rPCSMT.src.Main.Forms {
	partial class ConfigForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.logs_FolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.inventary_FolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.logs_textBox = new System.Windows.Forms.TextBox();
			this.inventary_textBox = new System.Windows.Forms.TextBox();
			this.logs_label = new System.Windows.Forms.Label();
			this.inventary_label = new System.Windows.Forms.Label();
			this.setLogsFolder_button = new System.Windows.Forms.Button();
			this.setInventaryFolder_button = new System.Windows.Forms.Button();
			this.logs_openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.inventary_openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.distroPath_textBox = new System.Windows.Forms.TextBox();
			this.distro_label = new System.Windows.Forms.Label();
			this.distroFolder_button = new System.Windows.Forms.Button();
			this.ExtraResources_tabControl = new System.Windows.Forms.TabControl();
			this.ExtraResourcesFolders_tabPage = new System.Windows.Forms.TabPage();
			this.ExtraFolders_dataGridView = new System.Windows.Forms.DataGridView();
			this.ExtraResourcesUrls_tabPage = new System.Windows.Forms.TabPage();
			this.ExtraUrls_dataGridView = new System.Windows.Forms.DataGridView();
			this.ExtraResources_tabControl.SuspendLayout();
			this.ExtraResourcesFolders_tabPage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ExtraFolders_dataGridView)).BeginInit();
			this.ExtraResourcesUrls_tabPage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ExtraUrls_dataGridView)).BeginInit();
			this.SuspendLayout();
			// 
			// logs_textBox
			// 
			this.logs_textBox.Location = new System.Drawing.Point(13, 26);
			this.logs_textBox.Name = "logs_textBox";
			this.logs_textBox.Size = new System.Drawing.Size(351, 20);
			this.logs_textBox.TabIndex = 0;
			this.logs_textBox.TextChanged += new System.EventHandler(this.logs_textBox_TextChanged);
			// 
			// inventary_textBox
			// 
			this.inventary_textBox.Location = new System.Drawing.Point(13, 70);
			this.inventary_textBox.Name = "inventary_textBox";
			this.inventary_textBox.Size = new System.Drawing.Size(351, 20);
			this.inventary_textBox.TabIndex = 0;
			this.inventary_textBox.TextChanged += new System.EventHandler(this.inventary_textBox_TextChanged);
			// 
			// logs_label
			// 
			this.logs_label.AutoSize = true;
			this.logs_label.Location = new System.Drawing.Point(12, 9);
			this.logs_label.Name = "logs_label";
			this.logs_label.Size = new System.Drawing.Size(74, 13);
			this.logs_label.TabIndex = 1;
			this.logs_label.Text = "Путь к логам";
			// 
			// inventary_label
			// 
			this.inventary_label.AutoSize = true;
			this.inventary_label.Location = new System.Drawing.Point(12, 53);
			this.inventary_label.Name = "inventary_label";
			this.inventary_label.Size = new System.Drawing.Size(126, 13);
			this.inventary_label.TabIndex = 2;
			this.inventary_label.Text = "Путь к инвентаризации";
			// 
			// setLogsFolder_button
			// 
			this.setLogsFolder_button.Enabled = false;
			this.setLogsFolder_button.Location = new System.Drawing.Point(371, 26);
			this.setLogsFolder_button.Name = "setLogsFolder_button";
			this.setLogsFolder_button.Size = new System.Drawing.Size(75, 23);
			this.setLogsFolder_button.TabIndex = 3;
			this.setLogsFolder_button.Text = "Открыть";
			this.setLogsFolder_button.UseVisualStyleBackColor = true;
			this.setLogsFolder_button.Click += new System.EventHandler(this.setLogsFolder_button_Click);
			// 
			// setInventaryFolder_button
			// 
			this.setInventaryFolder_button.Enabled = false;
			this.setInventaryFolder_button.Location = new System.Drawing.Point(371, 68);
			this.setInventaryFolder_button.Name = "setInventaryFolder_button";
			this.setInventaryFolder_button.Size = new System.Drawing.Size(75, 23);
			this.setInventaryFolder_button.TabIndex = 3;
			this.setInventaryFolder_button.Text = "Открыть";
			this.setInventaryFolder_button.UseVisualStyleBackColor = true;
			this.setInventaryFolder_button.Click += new System.EventHandler(this.setInventaryFolder_button_Click);
			// 
			// logs_openFileDialog
			// 
			this.logs_openFileDialog.AddExtension = false;
			this.logs_openFileDialog.CheckFileExists = false;
			// 
			// distroPath_textBox
			// 
			this.distroPath_textBox.Location = new System.Drawing.Point(13, 113);
			this.distroPath_textBox.Name = "distroPath_textBox";
			this.distroPath_textBox.Size = new System.Drawing.Size(351, 20);
			this.distroPath_textBox.TabIndex = 0;
			this.distroPath_textBox.TextChanged += new System.EventHandler(this.distroPath_textBox_TextChanged);
			// 
			// distro_label
			// 
			this.distro_label.AutoSize = true;
			this.distro_label.Location = new System.Drawing.Point(12, 96);
			this.distro_label.Name = "distro_label";
			this.distro_label.Size = new System.Drawing.Size(68, 13);
			this.distro_label.TabIndex = 2;
			this.distro_label.Text = "Путь к distro";
			// 
			// distroFolder_button
			// 
			this.distroFolder_button.Enabled = false;
			this.distroFolder_button.Location = new System.Drawing.Point(371, 111);
			this.distroFolder_button.Name = "distroFolder_button";
			this.distroFolder_button.Size = new System.Drawing.Size(75, 23);
			this.distroFolder_button.TabIndex = 3;
			this.distroFolder_button.Text = "Открыть";
			this.distroFolder_button.UseVisualStyleBackColor = true;
			this.distroFolder_button.Click += new System.EventHandler(this.distroFolder_button_Click);
			// 
			// ExtraResources_tabControl
			// 
			this.ExtraResources_tabControl.Controls.Add(this.ExtraResourcesFolders_tabPage);
			this.ExtraResources_tabControl.Controls.Add(this.ExtraResourcesUrls_tabPage);
			this.ExtraResources_tabControl.Location = new System.Drawing.Point(10, 142);
			this.ExtraResources_tabControl.Name = "ExtraResources_tabControl";
			this.ExtraResources_tabControl.SelectedIndex = 0;
			this.ExtraResources_tabControl.Size = new System.Drawing.Size(432, 194);
			this.ExtraResources_tabControl.TabIndex = 4;
			// 
			// ExtraResourcesFolders_tabPage
			// 
			this.ExtraResourcesFolders_tabPage.Controls.Add(this.ExtraFolders_dataGridView);
			this.ExtraResourcesFolders_tabPage.Location = new System.Drawing.Point(4, 22);
			this.ExtraResourcesFolders_tabPage.Name = "ExtraResourcesFolders_tabPage";
			this.ExtraResourcesFolders_tabPage.Padding = new System.Windows.Forms.Padding(3);
			this.ExtraResourcesFolders_tabPage.Size = new System.Drawing.Size(424, 168);
			this.ExtraResourcesFolders_tabPage.TabIndex = 0;
			this.ExtraResourcesFolders_tabPage.Text = "Папки";
			this.ExtraResourcesFolders_tabPage.UseVisualStyleBackColor = true;
			// 
			// ExtraFolders_dataGridView
			// 
			this.ExtraFolders_dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.ExtraFolders_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.ExtraFolders_dataGridView.Location = new System.Drawing.Point(-1, -1);
			this.ExtraFolders_dataGridView.Name = "ExtraFolders_dataGridView";
			this.ExtraFolders_dataGridView.Size = new System.Drawing.Size(429, 173);
			this.ExtraFolders_dataGridView.TabIndex = 0;
			this.ExtraFolders_dataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.ExtraFolders_dataGridView_CellEndEdit);
			this.ExtraFolders_dataGridView.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.ExtraFolders_dataGridView_UserDeletedRow);
			// 
			// ExtraResourcesUrls_tabPage
			// 
			this.ExtraResourcesUrls_tabPage.Controls.Add(this.ExtraUrls_dataGridView);
			this.ExtraResourcesUrls_tabPage.Location = new System.Drawing.Point(4, 22);
			this.ExtraResourcesUrls_tabPage.Name = "ExtraResourcesUrls_tabPage";
			this.ExtraResourcesUrls_tabPage.Padding = new System.Windows.Forms.Padding(3);
			this.ExtraResourcesUrls_tabPage.Size = new System.Drawing.Size(424, 168);
			this.ExtraResourcesUrls_tabPage.TabIndex = 1;
			this.ExtraResourcesUrls_tabPage.Text = "Ссылки";
			this.ExtraResourcesUrls_tabPage.UseVisualStyleBackColor = true;
			// 
			// ExtraUrls_dataGridView
			// 
			this.ExtraUrls_dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.ExtraUrls_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.ExtraUrls_dataGridView.Location = new System.Drawing.Point(-1, -1);
			this.ExtraUrls_dataGridView.Name = "ExtraUrls_dataGridView";
			this.ExtraUrls_dataGridView.Size = new System.Drawing.Size(429, 173);
			this.ExtraUrls_dataGridView.TabIndex = 1;
			this.ExtraUrls_dataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.ExtraUrls_dataGridView_CellEndEdit);
			this.ExtraUrls_dataGridView.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.ExtraUrls_dataGridView_UserDeletedRow);
			// 
			// ConfigForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(454, 344);
			this.Controls.Add(this.ExtraResources_tabControl);
			this.Controls.Add(this.distroFolder_button);
			this.Controls.Add(this.setInventaryFolder_button);
			this.Controls.Add(this.setLogsFolder_button);
			this.Controls.Add(this.logs_label);
			this.Controls.Add(this.distroPath_textBox);
			this.Controls.Add(this.inventary_textBox);
			this.Controls.Add(this.logs_textBox);
			this.Controls.Add(this.inventary_label);
			this.Controls.Add(this.distro_label);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "ConfigForm";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Configuration";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConfigForm_Closing);
			this.Load += new System.EventHandler(this.ConfigForm_Load);
			this.ExtraResources_tabControl.ResumeLayout(false);
			this.ExtraResourcesFolders_tabPage.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.ExtraFolders_dataGridView)).EndInit();
			this.ExtraResourcesUrls_tabPage.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.ExtraUrls_dataGridView)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.FolderBrowserDialog logs_FolderBrowserDialog;
		private System.Windows.Forms.FolderBrowserDialog inventary_FolderBrowserDialog;
		private System.Windows.Forms.TextBox logs_textBox;
		private System.Windows.Forms.TextBox inventary_textBox;
		private System.Windows.Forms.Label logs_label;
		private System.Windows.Forms.Label inventary_label;
		private System.Windows.Forms.Button setLogsFolder_button;
		private System.Windows.Forms.Button setInventaryFolder_button;
		private System.Windows.Forms.OpenFileDialog logs_openFileDialog;
		private System.Windows.Forms.OpenFileDialog inventary_openFileDialog;
		private System.Windows.Forms.TextBox distroPath_textBox;
		private System.Windows.Forms.Label distro_label;
		private System.Windows.Forms.Button distroFolder_button;
		private System.Windows.Forms.TabControl ExtraResources_tabControl;
		private System.Windows.Forms.TabPage ExtraResourcesFolders_tabPage;
		private System.Windows.Forms.DataGridView ExtraFolders_dataGridView;
		private System.Windows.Forms.TabPage ExtraResourcesUrls_tabPage;
		private System.Windows.Forms.DataGridView ExtraUrls_dataGridView;
	}
}