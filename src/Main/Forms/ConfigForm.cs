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


namespace rPCSMT.src.Main.Forms {
	public partial class ConfigForm : Form {

		public class Extra {
			public string key;
			public string value;

			public Extra(string key, string value) {
				this.key = key;
				this.value = value;
			}
		}

		public ConfigForm() {
			InitializeComponent();
			this.ShowDialog();
		}

		private void ConfigForm_Load(object sender, EventArgs e) {
			logs_textBox.Text = Utility.configuration.getOriginalLogPath();
			inventary_textBox.Text = Utility.configuration.inventoryPath;
			distroPath_textBox.Text = Utility.configuration.distroPath;

			this.ExtraFolders_dataGridView.Columns.Add("name", "Название");
			this.ExtraFolders_dataGridView.Columns.Add("path", "Путь");
			foreach (Extra extraFolder in Utility.configuration.extraFolders) {
				ExtraFolders_dataGridView.Rows.Add(new object[] { extraFolder.key, extraFolder.value });
			}

			this.ExtraUrls_dataGridView.Columns.Add("name", "Название");
			this.ExtraUrls_dataGridView.Columns.Add("url", "Ссылка");
			foreach (Extra extraURL in Utility.configuration.extraURLs) {
				ExtraFolders_dataGridView.Rows.Add(new object[] { extraURL.key, extraURL.value });
			}
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
			//Utility.configuration.almonahUrl = almonahUrl_textBox.Text;
		}

		private void distroPath_textBox_TextChanged(object sender, EventArgs e) {
			Utility.configuration.distroPath = distroPath_textBox.Text;
		}

		private void ConfigForm_Closing(object sender, CancelEventArgs e) {
			Utility.configuration.saveConfiguration();
		}

		private void ExtraFolders_dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e) {
			string cellValue = (string)(sender as DataGridView).Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
			switch (e.ColumnIndex) {
				case 0:
					if (e.RowIndex >= Utility.configuration.extraFolders.Count) {
						Utility.configuration.extraFolders.Add(
							new Extra(cellValue, "")
						);
					} else {
						Utility.configuration.extraFolders[e.RowIndex].key = cellValue;
					}
					return;
				case 1:
					if (e.RowIndex >= Utility.configuration.extraFolders.Count) {
						Utility.configuration.extraFolders.Add(
							new Extra("", cellValue)
						);
					} else {
						Utility.configuration.extraFolders[e.RowIndex].value = cellValue;
					}
					return;
			}
			//(sender as DataGridView).Rows[e.RowIndex].Cells[e.ColumnIndex].
		}

		private void ExtraUrls_dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e) {
			string cellValue = (string)(sender as DataGridView).Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
			switch (e.ColumnIndex) {
				case 0:
					if (e.RowIndex >= Utility.configuration.extraURLs.Count) {
						Utility.configuration.extraURLs.Add(
							new Extra(cellValue, "")
						);
					} else {
						Utility.configuration.extraURLs[e.RowIndex].key = cellValue;
					}
					return;
				case 1:
					if (e.RowIndex >= Utility.configuration.extraURLs.Count) {
						Utility.configuration.extraURLs.Add(
							new Extra("", cellValue)
						);
					} else {
						Utility.configuration.extraURLs[e.RowIndex].value = cellValue;
					}
					return;
			}
			//(sender as DataGridView).Rows[e.RowIndex].Cells[e.ColumnIndex].
		}


		private void ExtraFolders_dataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e) {
			Utility.configuration.extraFolders.Remove(Utility.configuration.extraFolders
				.Find(el => el.key == e.Row.Cells[0].Value.ToString()));
		}


		private void ExtraUrls_dataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e) {
			Utility.configuration.extraURLs.Remove(Utility.configuration.extraURLs
				.Find(el => el.key == e.Row.Cells[0].Value.ToString()));
		}
	}
}
