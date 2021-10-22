
namespace pc_finnder.src.Main.Forms {
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
			this.almonahUrl_textBox = new System.Windows.Forms.TextBox();
			this.almanah_label = new System.Windows.Forms.Label();
			this.distroPath_textBox = new System.Windows.Forms.TextBox();
			this.distro_label = new System.Windows.Forms.Label();
			this.distroFolder_button = new System.Windows.Forms.Button();
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
			this.inventary_textBox.Location = new System.Drawing.Point(13, 73);
			this.inventary_textBox.Name = "inventary_textBox";
			this.inventary_textBox.Size = new System.Drawing.Size(351, 20);
			this.inventary_textBox.TabIndex = 0;
			this.inventary_textBox.TextChanged += new System.EventHandler(this.inventary_textBox_TextChanged);
			// 
			// logs_label
			// 
			this.logs_label.AutoSize = true;
			this.logs_label.Location = new System.Drawing.Point(13, 7);
			this.logs_label.Name = "logs_label";
			this.logs_label.Size = new System.Drawing.Size(74, 13);
			this.logs_label.TabIndex = 1;
			this.logs_label.Text = "Путь к логам";
			// 
			// inventary_label
			// 
			this.inventary_label.AutoSize = true;
			this.inventary_label.Location = new System.Drawing.Point(13, 54);
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
			this.setInventaryFolder_button.Location = new System.Drawing.Point(371, 70);
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
			// almonahUrl_textBox
			// 
			this.almonahUrl_textBox.Location = new System.Drawing.Point(12, 125);
			this.almonahUrl_textBox.Name = "almonahUrl_textBox";
			this.almonahUrl_textBox.Size = new System.Drawing.Size(430, 20);
			this.almonahUrl_textBox.TabIndex = 0;
			this.almonahUrl_textBox.TextChanged += new System.EventHandler(this.almonahUrl_textBox_TextChanged);
			// 
			// almanah_label
			// 
			this.almanah_label.AutoSize = true;
			this.almanah_label.Location = new System.Drawing.Point(12, 105);
			this.almanah_label.Name = "almanah_label";
			this.almanah_label.Size = new System.Drawing.Size(113, 13);
			this.almanah_label.TabIndex = 2;
			this.almanah_label.Text = "Ссылка на альмонах";
			// 
			// distroPath_textBox
			// 
			this.distroPath_textBox.Location = new System.Drawing.Point(10, 177);
			this.distroPath_textBox.Name = "distroPath_textBox";
			this.distroPath_textBox.Size = new System.Drawing.Size(351, 20);
			this.distroPath_textBox.TabIndex = 0;
			this.distroPath_textBox.TextChanged += new System.EventHandler(this.distroPath_textBox_TextChanged);
			// 
			// distro_label
			// 
			this.distro_label.AutoSize = true;
			this.distro_label.Location = new System.Drawing.Point(10, 158);
			this.distro_label.Name = "distro_label";
			this.distro_label.Size = new System.Drawing.Size(68, 13);
			this.distro_label.TabIndex = 2;
			this.distro_label.Text = "Путь к distro";
			// 
			// distroFolder_button
			// 
			this.distroFolder_button.Enabled = false;
			this.distroFolder_button.Location = new System.Drawing.Point(368, 174);
			this.distroFolder_button.Name = "distroFolder_button";
			this.distroFolder_button.Size = new System.Drawing.Size(75, 23);
			this.distroFolder_button.TabIndex = 3;
			this.distroFolder_button.Text = "Открыть";
			this.distroFolder_button.UseVisualStyleBackColor = true;
			this.distroFolder_button.Click += new System.EventHandler(this.distroFolder_button_Click);
			// 
			// ConfigForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(454, 208);
			this.Controls.Add(this.distroFolder_button);
			this.Controls.Add(this.setInventaryFolder_button);
			this.Controls.Add(this.setLogsFolder_button);
			this.Controls.Add(this.almanah_label);
			this.Controls.Add(this.distro_label);
			this.Controls.Add(this.inventary_label);
			this.Controls.Add(this.logs_label);
			this.Controls.Add(this.almonahUrl_textBox);
			this.Controls.Add(this.distroPath_textBox);
			this.Controls.Add(this.inventary_textBox);
			this.Controls.Add(this.logs_textBox);
			this.Name = "ConfigForm";
			this.Text = "Configuration";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConfigForm_Closing);
			this.Load += new System.EventHandler(this.ConfigForm_Load);
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
		private System.Windows.Forms.TextBox almonahUrl_textBox;
		private System.Windows.Forms.Label almanah_label;
		private System.Windows.Forms.TextBox distroPath_textBox;
		private System.Windows.Forms.Label distro_label;
		private System.Windows.Forms.Button distroFolder_button;
	}
}