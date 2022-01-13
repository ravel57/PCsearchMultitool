using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net.NetworkInformation;
using Cassia;
using rPCSMT.src.Main;
using System.Globalization;
using System.Reflection;
using System.Diagnostics;
using rPCSMT.src.Main.Forms;
//using System.Linq;

namespace rPCSMT {
	public partial class MainForm : Form {

		private string selectedComboboxItem;
		private string selectedComputer;
		LogFileParser.foundElementInfo[] parsedEntities;
		LogFileParser.foundElementInfo parsedEntity;
		enum sortBy { DATA = 0, LOGS = 1 };
		sortBy selectedSortingMethod = sortBy.DATA;

		public enum SearchFor { users = 0, computers = 1 };
		public static SearchFor selectedSearchType = SearchFor.users;

		public MainForm() {
			InitializeComponent();
		}


		//on load
		private async void MainForm_Load(object sender, EventArgs e) {
			if (Directory.GetCurrentDirectory() != Utility.APPDATA_PATH & Utility.RUN_FROM_APPDATA) {
				Utility.startLocal();
			} else {
				// load settings
				Utility.run();
				//window position 
				this.Location = Utility.settings.windowCord;
				//redraw listbox to select active computer
				this.parsedEntitiesNames_listBox.DrawMode = DrawMode.OwnerDrawVariable;
				this.parsedEntitiesNames_listBox.MeasureItem += new MeasureItemEventHandler(PCs_listBox_MeasureItem);
				this.parsedEntitiesNames_listBox.DrawItem += new DrawItemEventHandler(PCs_listBox_DrawItem);
				this.version_textBox.Text = Utility.VERSION;
				this.info_button.Enabled = Directory.Exists(Utility.configuration.inventoryPath);
				this.printerInfo_button.Enabled = Directory.Exists(Utility.configuration.inventoryPath);
				//foreach (var el in Utility.configuration.extraFolders)
				//	this.extra_contextMenuStrip.Items.Add(el.key);
				//foreach (var el in Utility.configuration.extraURLs)
				//	this.extra_contextMenuStrip.Items.Add(el.key);
				LogsFoldersSearchResalts.updateSearchResalts();
				this.userName_comboBox.Text = Utility.inputString;
				this.userName_comboBox_TextChanged(sender, e);

				await Task.Run(async () => {
					while (true) {
						await Task.Delay(TimeSpan.FromSeconds(60));
						LogsFoldersSearchResalts.updateSearchResalts();
						this.PCs_listBox_SelectedIndexChanged(sender, e);
					}
				});
			}
		}


		private void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			Utility.settings.saveSettings(this.Location);
		}


		private void PCs_listBox_MeasureItem(object sender, MeasureItemEventArgs e) {
			e.ItemHeight = (int)e.Graphics.MeasureString(parsedEntitiesNames_listBox.Items[e.Index].ToString(), parsedEntitiesNames_listBox.Font, parsedEntitiesNames_listBox.Width).Height;
		}


		private void PCs_listBox_DrawItem(object sender, DrawItemEventArgs e) {
			e.DrawBackground();
			bool isItemSelected = ((e.State & DrawItemState.Selected) == DrawItemState.Selected);
			int itemIndex = e.Index;
			if (itemIndex >= 0 && itemIndex < parsedEntitiesNames_listBox.Items.Count) {
				Graphics g = e.Graphics;
				string itemText = parsedEntitiesNames_listBox.Items[itemIndex].ToString();

				// Background Color	
				SolidBrush backgroundColorBrush;
				SolidBrush itemTextColorBrush;
				if (!parsedEntities.First(pc => itemText.Contains(pc.name)).loginStatus) {
					if (parsedEntities.First(pc => itemText.IndexOf(pc.name) >= 0).assisting != String.Empty) {
						backgroundColorBrush = new SolidBrush((isItemSelected) ? Color.SeaGreen : Color.White);
						itemTextColorBrush = (isItemSelected) ? new SolidBrush(Color.White) : new SolidBrush(Color.SeaGreen);
					} else {
						backgroundColorBrush = new SolidBrush((isItemSelected) ? Color.Transparent : Color.White);
						itemTextColorBrush = (isItemSelected) ? new SolidBrush(Color.White) : new SolidBrush(Color.Black);
					}
				} else {
					if (parsedEntities.First(pc => itemText.Contains(pc.name)).assisting != String.Empty) {
						backgroundColorBrush = new SolidBrush((isItemSelected) ? Color.Orange : Color.White);
						itemTextColorBrush = (isItemSelected) ? new SolidBrush(Color.White) : new SolidBrush(Color.Orange);
					} else {
						backgroundColorBrush = new SolidBrush((isItemSelected) ? Color.Red : Color.White);
						itemTextColorBrush = (isItemSelected) ? new SolidBrush(Color.White) : new SolidBrush(Color.Red);
					}
				}
				g.FillRectangle(backgroundColorBrush, e.Bounds);
				// Set text color
				g.DrawString(itemText, e.Font, itemTextColorBrush, parsedEntitiesNames_listBox.GetItemRectangle(itemIndex).Location);
				// Clean up
				backgroundColorBrush.Dispose();
				itemTextColorBrush.Dispose();
			}
			e.DrawFocusRectangle();
		}


		private void userName_comboBox_TextChanged(object sender, EventArgs e) {
			string[] userNameVariants = UsernameInputProcessing.searchUsername(userName_comboBox.Text);
			//this.userName_comboBox.DataSource = userNameVariants;
			if (userName_comboBox.Text.Length > 0 & userName_comboBox.Text != "\u007f" /* | userNameVariants.Length > 0*/) {
				int cursorPosition = userName_comboBox.SelectionStart;
				this.userName_comboBox.Items.Clear();
				this.userName_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
				this.userName_comboBox.DroppedDown = true;
				this.userName_comboBox.Select(cursorPosition, 0);
				this.userName_comboBox.Items.AddRange(userNameVariants);
			} else {
				//this.userName_comboBox.DroppedDown = false;
				this.parsedEntitiesNames_listBox.DataSource = null;
				this.PCinfo_textBox.Clear();
				this.parsedEntities = new LogFileParser.foundElementInfo[0];
				this.userName_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
				this.userName_comboBox.Text = "";
				this.parsedEntity = new LogFileParser.foundElementInfo();
			}
			//чтобы курсор не падал за список и был виден
			Cursor.Current = Cursors.Default;
		}


		private void sortComputers(ref LogFileParser.foundElementInfo[] computers) {
			switch (selectedSortingMethod) {
				case sortBy.LOGS: {
						Array.Sort<LogFileParser.foundElementInfo>(parsedEntities,
							(x, y) => y.count.CompareTo(x.count));
						break;
					}
				case sortBy.DATA: {
						Array.Sort<LogFileParser.foundElementInfo>(parsedEntities, (x, y) => (
							  Convert.ToDateTime(y.lastLog.Substring(0, 14)).CompareTo(
							  Convert.ToDateTime(x.lastLog.Substring(0, 14)))
						));
						break;
					}
			}
		}


		private async void UserName_comboBox_SelectedIndexChanged(object sender, EventArgs e) {
			if (this.userName_comboBox.SelectedItem == null)
				//this.userName_comboBox.SelectedItem = 0;
				this.userName_comboBox.SelectedItem = this.userName_comboBox.Items[0];
			this.selectedComboboxItem = this.userName_comboBox.GetItemText(this.userName_comboBox.SelectedItem).ToString();

			parsedEntities = LogFileParser.ParceUsersComputers(selectedComboboxItem);
			sortComputers(ref parsedEntities);
			this.parsedEntitiesNames_listBox.DataSource = LogFileParser.getNames(parsedEntities);
			foreach (var el in parsedEntities) {
				if (Convert.ToDateTime(el.lastLog.Substring(0, 8)).AddMonths(4) >= DateTime.Now) {
					string computerName = String.Empty;
					string userName = String.Empty;
					switch (selectedSearchType) {
						case SearchFor.users:
							computerName = selectedComboboxItem;
							userName = el.name;
							break;
						case SearchFor.computers:
							computerName = el.name;
							userName = selectedComboboxItem;
							break;
					}
					if (await Task.Run(() => AdminTools.checkUserLogedIn(computerName, userName))) {
						el.loginStatus = true;
						this.parsedEntitiesNames_listBox.Refresh();
					}
				}
			}
		}


		private void sortPCnamesMethod_button_Click(object sender, EventArgs e) {
			switch (selectedSortingMethod) {
				case sortBy.DATA:
					selectedSortingMethod = sortBy.LOGS;
					this.sortBatton_toolTip.SetToolTip(this.sortPCnamesMethod_button, "Выбрана сортировка по ЛОГАМ");
					break;
				case sortBy.LOGS:
					selectedSortingMethod = sortBy.DATA;
					this.sortBatton_toolTip.SetToolTip(this.sortPCnamesMethod_button, "Выбрана сортировка по ДАТЕ");
					break;
			}
			sortPCnamesMethod_button.Text = selectedSortingMethod.ToString().ToUpper()[0].ToString();
			if (userName_comboBox.Text != String.Empty) {
				UserName_comboBox_SelectedIndexChanged(sender, e);
				//PCs_listBox.SelectedIndex = 0;
			}
			parsedEntitiesNames_listBox.Focus();
		}


		private void MainForm_KeyDown(object sender, KeyEventArgs e) {
			if (e.KeyData == Keys.Escape) {
				Environment.Exit(0);
			}
		}


		private void UserName_comboBox_KeyDown(object sender, KeyEventArgs e) {
			if (userName_comboBox.Text.Length > 0) {
				switch (e.KeyCode) {
					case Keys.Enter: {
							if (this.userName_comboBox.Items.Count > 0 && this.userName_comboBox.SelectedIndex <= -1)
								this.userName_comboBox.SelectedIndex = 0;
								//this.PCs_listBox.SelectedIndex = 0;
							this.parsedEntitiesNames_listBox.Focus();
							break;
						}
				}
				if (e.KeyData == (Keys.Back | Keys.Control)) {
					userName_comboBox.Text = "";
				}
			}
		}


		private async void PCs_listBox_SelectedIndexChanged(object sender, EventArgs e) {
			try {
				if (this.userName_comboBox.Text != "\u007f" & this.userName_comboBox.Text != "") {
					//LogFileParser.foundElementInfo selectedPC = parsedEntities
					parsedEntity = parsedEntities.First(el => el.name == this.parsedEntitiesNames_listBox.SelectedItem.ToString());
					this.PCinfo_textBox.Lines = new string[4] {
						parsedEntity.name,
						parsedEntity.count.ToString(),
						parsedEntity.firstLog,
						parsedEntity.lastLog + " [" + parsedEntity.lastLogType + "]"
					};
					switch (selectedSearchType) {
						case SearchFor.users:
							selectedComputer = parsedEntity.name;
							break;
						case SearchFor.computers:
							selectedComputer = selectedComboboxItem;
							break;
					}
					this.explorer_button.Enabled = false;
					this.explorer_button.Enabled = await Task.Run(() => Directory.Exists("\\\\" + selectedComputer + "\\c$\\"));
				}
			} catch (Exception) { }
		}



		private void openAsist_button_Click(object sender, EventArgs e) {
			AdminTools.openAsist(selectedComputer);
		}


		private void openRDP_button_Click(object sender, EventArgs e) {
			AdminTools.openRDP(selectedComputer);
		}


		private void info_button_Click(object sender, EventArgs e) {
			AdminTools.GetComputerInfo(selectedComputer);
		}

		private void copy_button_Click(object sender, EventArgs e) {
			if (selectedComputer != null)
				Clipboard.SetText(selectedComputer);
		}

		private void explorer_button_Click(object sender, EventArgs e) {
			AdminTools.openComputerInExplorer(selectedComputer);
		}

		private void ping_button_Click(object sender, EventArgs e) {
			AdminTools.pingResalt(selectedComputer);
		}

		async private void infinitePing_button_Click(object sender, EventArgs e) {
			if (selectedComputer != null) {
				await Task.Run(() => {
					Application.Run(new InfinitePing_Form(selectedComputer));
				});
			}
		}


		private void printerInfo_button_Click(object sender, EventArgs e) {
			AdminTools.GetPrintersInfo(selectedComputer);
		}


		private void ip_button_Click(object sender, EventArgs e) {
			AdminTools.getIpByHostname(selectedComputer);
		}


		private void PCs_listBox_DoubleClick(object sender, MouseEventArgs e) {
			swapSearchObject((sender as ListBox).SelectedItem.ToString());
		}


		private void PCs_listBox_KeyDown(object sender, KeyEventArgs e) {
			switch (e.KeyData) {
				case Keys.Escape:
					Environment.Exit(0);
					break;
					//case Keys.Enter:
					//	swapSearchObject((sender as ListBox).SelectedItem.ToString());
					//	break;
			}
		}


		private void swapSearchObject(string selectedElement) {
			ChangeSearchObjects_button_Click(new Object(), new EventArgs());
			userName_comboBox.Text = selectedElement;
			userName_comboBox_TextChanged(new Object(), new EventArgs());
			this.userName_comboBox.DroppedDown = false;
			UserName_comboBox_SelectedIndexChanged(new Object(), new EventArgs());
			parsedEntitiesNames_listBox.Focus();
		}


		private void extraToolsMenu_button_Click(object sender, EventArgs e) {
			if (selectedComputer == null) {
				extra_contextMenuStrip.Items[0].Enabled = false;
				extra_contextMenuStrip.Items[1].Enabled = false;
			} else {
				extra_contextMenuStrip.Items[0].Enabled = true;
				extra_contextMenuStrip.Items[1].Enabled = true;
			}
			extra_contextMenuStrip.Show(extraToolsMenu_button, 1, (sender as Button).Height - 2);
		}


		private void closeAssistent_toolStripMenuItem_Click(object sender, EventArgs e) {
			if (selectedComputer != null) {
				AdminTools.killProcess(selectedComputer, "msra.exe");
			}
		}


		private async void taskManager_toolStripMenuItem_Click(object sender, EventArgs e) {
			if (selectedComputer != null) {
				await Task.Run(() => {
					Application.Run(new TaskManagerForm(selectedComputer));
				});
			}
		}


		private void config_toolStripMenuItem_Click(object sender, EventArgs e) {
			new ConfigForm();
		}


		private void almonah_toolStripMenuItem_Click(object sender, EventArgs e) {
			//Utility.execProcess("\"C:\\Program Files (x86)\\Google\\Chrome\\Application\\chrome.exe\" " + Utility.configuration.almonahUrl);
		}


		private void openDistro_toolStripMenuItem_Click(object sender, EventArgs e) {
			Utility.execProcess("explorer " + Utility.configuration.distroPath);
		}


		private void ChangeSearchObjects_button_Click(object sender, EventArgs e) {
			switch (selectedSearchType) {
				case SearchFor.users:
					selectedSearchType = SearchFor.computers;
					this.ChangeSearchObjects.Text = "Имя компьютера:";
					break;
				case SearchFor.computers:
					selectedSearchType = SearchFor.users;
					this.ChangeSearchObjects.Text = "Имя пользователя:";
					break;
			}
			userName_comboBox.Text = String.Empty;
			LogsFoldersSearchResalts.updateSearchResalts();
			parsedEntitiesNames_listBox.DataSource = null;
			userName_comboBox.Focus();
		}

		private void extra_contextMenuStrip_Click(object sender, EventArgs e) {
			Utility.execProcess(this.extra_contextMenuStrip.Items[((e as MouseEventArgs).Y / 22)].Text);
		}
	}
}