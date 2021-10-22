using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace pc_finnder.src.Main.Forms {
	public partial class ConfigForm : Form {
		public ConfigForm() {
			InitializeComponent();
			this.ShowDialog();
		}

		private void ConfigForm_Load(object sender, EventArgs e) {
			logs_textBox.Text = Utility.configuration.loginsPath;
			inventary_textBox.Text = Utility.configuration.inventoryPath;
			almonahUrl_textBox.Text = Utility.configuration.almonahUrl;
			distroPath_textBox.Text = Utility.configuration.distroPath;
			this.logs_textBox.Focus();
		}

		private void setLogsFolder_button_Click(object sender, EventArgs e) {
			if (logs_FolderBrowserDialog.ShowDialog() == DialogResult.OK) {
				//logs_textBox.Text = logs_openFileDialog.;
				logs_textBox.Text = logs_FolderBrowserDialog.SelectedPath;
			}
		}

		private void setInventaryFolder_button_Click(object sender, EventArgs e) {
			if (inventary_openFileDialog.ShowDialog() == DialogResult.OK) {
				inventary_textBox.Text = inventary_openFileDialog.FileName;
			}
		}

		private void distroFolder_button_Click(object sender, EventArgs e) {
		}

		private void logs_textBox_TextChanged(object sender, EventArgs e) {
			if (Directory.Exists(logs_textBox.Text))
				Utility.configuration.loginsPath = logs_textBox.Text;
		}

		private void inventary_textBox_TextChanged(object sender, EventArgs e) {
			if (Directory.Exists(inventary_textBox.Text))
				Utility.configuration.inventoryPath = inventary_textBox.Text;
		}

		private void almonahUrl_textBox_TextChanged(object sender, EventArgs e) {
			Utility.configuration.almonahUrl = almonahUrl_textBox.Text;
		}

		private void distroPath_textBox_TextChanged(object sender, EventArgs e) {
			Utility.configuration.distroPath = distroPath_textBox.Text;
		}

		private void ConfigForm_Closing(object sender, CancelEventArgs e) {
			Utility.configuration.saveConfiguration();
		}
	}
}
