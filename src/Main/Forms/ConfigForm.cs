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
			rPrinterManager_textBox.Text = Utility.configuration.rPrinterManagerPath;
			movingInfinitePing_checkBox.Checked = Utility.configuration.movingInfinitePing;

			setValuesToDataGridView();

			closeAnotherCopyOfProgram_checkBox.Checked = Utility.settings.closeAnotherCopyOfProgram;
			this.logs_textBox.Focus();
		}

		private void setValuesToDataGridView() {
			this.ExtraFolders_dataGridView.Columns.Add("name", "Название");
			this.ExtraFolders_dataGridView.Columns.Add("path", "Путь");
			foreach (Extra extraFolder in Utility.configuration.extraFolders) {
				ExtraFolders_dataGridView.Rows.Add(new object[] { extraFolder.key, extraFolder.value });
			}
			foreach (Extra extraFolder in Utility.settings.extraFolders) {
				ExtraFolders_dataGridView.Rows.Add(new object[] { extraFolder.key, extraFolder.value });
			}

			this.ExtraUrls_dataGridView.Columns.Add("name", "Название");
			this.ExtraUrls_dataGridView.Columns.Add("url", "Ссылка");
			foreach (Extra extraURL in Utility.configuration.extraURLs) {
				ExtraUrls_dataGridView.Rows.Add(new object[] { extraURL.key, extraURL.value });
			}
			foreach (Extra extraURL in Utility.settings.extraURLs) {
				ExtraUrls_dataGridView.Rows.Add(new object[] { extraURL.key, extraURL.value });
			}
		}

		private void ConfigForm_Closing(object sender, CancelEventArgs e) {
			Utility.configuration.saveConfiguration();
			Utility.settings.saveSettings();
		}

		private void ExtraFolders_dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e) {
			string cellValue = (sender as DataGridView).Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
			List<ConfigForm.Extra> extraFoldersRef;
			//Utility.RUN_FROM_APPDATA = true;
			if (Utility.RUN_FROM_APPDATA)
				extraFoldersRef = Utility.settings.extraFolders;
			else
				extraFoldersRef = Utility.configuration.extraFolders;
			switch (e.ColumnIndex) {
				case 0:
					if (e.RowIndex >= extraFoldersRef.Count + (!Utility.RUN_FROM_APPDATA ? 0 : Utility.configuration.extraFolders.Count)) {
						extraFoldersRef.Add(new Extra(cellValue, ""));
					} else {
						extraFoldersRef[e.RowIndex - (!Utility.RUN_FROM_APPDATA ? 0 : Utility.configuration.extraFolders.Count)].key = cellValue;
					}
					return;
				case 1:
					if (e.RowIndex >= extraFoldersRef.Count + (!Utility.RUN_FROM_APPDATA ? 0 : Utility.configuration.extraFolders.Count)) {
						extraFoldersRef.Add(new Extra("", cellValue));
					} else {
						extraFoldersRef[e.RowIndex - (!Utility.RUN_FROM_APPDATA ? 0 : Utility.configuration.extraFolders.Count)].value = cellValue;
					}
					return;
			}
			//(sender as DataGridView).Rows[e.RowIndex].Cells[e.ColumnIndex].
			ExtraFolders_dataGridView.Rows.Clear();
			ExtraFolders_dataGridView.Columns.Clear();
			setValuesToDataGridView();
		}

		private void ExtraUrls_dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e) {
			string cellValue = (string)(sender as DataGridView).Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
			List<ConfigForm.Extra> extraURLsRef;
			if (Utility.RUN_FROM_APPDATA)
				extraURLsRef = Utility.settings.extraURLs;
			else
				extraURLsRef = Utility.configuration.extraURLs;
			switch (e.ColumnIndex) {
				case 0:
					if (e.RowIndex >= Utility.configuration.extraURLs.Count) {
						extraURLsRef.Add(new Extra(cellValue, ""));
					} else {
						extraURLsRef[e.RowIndex].key = cellValue;
					}
					return;
				case 1:
					if (e.RowIndex >= Utility.configuration.extraURLs.Count) {
						extraURLsRef.Add(new Extra("", cellValue));
					} else {
						extraURLsRef[e.RowIndex].value = cellValue;
					}
					return;
			}
			//(sender as DataGridView).Rows[e.RowIndex].Cells[e.ColumnIndex].
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

		private void distroPath_textBox_TextChanged(object sender, EventArgs e) {
			Utility.configuration.distroPath = distroPath_textBox.Text;
		}

		private void rPrinterManager_textBox_TextChanged(object sender, EventArgs e) {
			Utility.configuration.rPrinterManagerPath = rPrinterManager_textBox.Text;
		}


		private void ExtraFolders_dataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e) {
			List<ConfigForm.Extra> extraFoldersRef;
			if (Utility.RUN_FROM_APPDATA) extraFoldersRef = Utility.settings.extraFolders;
			else extraFoldersRef = Utility.configuration.extraFolders;
			extraFoldersRef.Remove(Utility.configuration.extraFolders
				.Find(el => el.key == e.Row.Cells[0].Value.ToString()));
		}


		private void ExtraUrls_dataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e) {
			List<ConfigForm.Extra> extraURLsRef;
			if (Utility.RUN_FROM_APPDATA) extraURLsRef = Utility.settings.extraURLs;
			else extraURLsRef = Utility.configuration.extraURLs;
			extraURLsRef.Remove(Utility.configuration.extraURLs
				.Find(el => el.key == e.Row.Cells[0].Value.ToString()));
		}

		private void closeAnotherCopyOfProgram_checkBox_CheckedChanged(object sender, EventArgs e) {
			Utility.settings.closeAnotherCopyOfProgram = closeAnotherCopyOfProgram_checkBox.Checked;
		}

		private void movingInfinitePing_checkBox_CheckedChanged(object sender, EventArgs e) {
			Utility.configuration.movingInfinitePing = movingInfinitePing_checkBox.Checked;
		}
	}
}
