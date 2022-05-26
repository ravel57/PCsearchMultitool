using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using rPCSMT.src.Main;
using rPCSMT.src.Main.Forms;

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
				Utility.startFromAppdata();
			} else {
				// load settings
				Utility.initializeSettings();

				//window position 
				this.Location = Utility.settings.windowCord;

				//redraw listbox to select active computer
				this.parsedEntitiesNames_listBox.DrawMode = DrawMode.OwnerDrawVariable;
				this.parsedEntitiesNames_listBox.MeasureItem += new MeasureItemEventHandler(PCs_listBox_MeasureItem);
				this.parsedEntitiesNames_listBox.DrawItem += new DrawItemEventHandler(PCs_listBox_DrawItem);

				// enable buttons and version textbox
				this.openAsist_button.Enabled = false;
				this.openRDP_button.Enabled = false;
				this.info_button.Enabled = false;
				this.printerInfo_button.Enabled = false;
				this.copy_button.Enabled = false;
				this.ping_button.Enabled = false;
				this.infinitePing_button.Enabled = false;
				this.ip_button.Enabled = false;
				this.version_textBox.Text = Utility.VERSION;

				// adding items to extra menu
				updateExtraContextMenuStripItems();

				// update log folders and set input parameter string
				LogsFoldersSearchResalts.updateSearchResalts();
				if (Utility.inputString != string.Empty) {
					this.userName_comboBox.Text = Utility.inputString;
					this.userName_comboBox_TextChanged(new object(), new EventArgs());
					if (this.userName_comboBox.Items.Count > 0 && this.userName_comboBox.SelectedIndex <= -1) {
						this.userName_comboBox.SelectedIndex = 0;
						this.userName_comboBox.DroppedDown = false;
						this.parsedEntitiesNames_listBox.Focus();
					}
					if (Utility.settings.closeAnotherCopyOfProgram) {
						AdminTools.killAnotherCopyOfProgram();
					}
				}

				// starting updating for log folder
				await Task.Run(async () => {
					while (true) {
						await Task.Delay(TimeSpan.FromSeconds(60));
						LogsFoldersSearchResalts.updateSearchResalts();
						this.parsedEntitiesNames_listBox_SelectedIndexChanged(sender, e);
					}
				});
			}
		}


		private void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			Utility.settings.saveSettings(this.Location);
		}


		private void PCs_listBox_MeasureItem(object sender, MeasureItemEventArgs e) {
			e.ItemHeight = (int)e.Graphics.MeasureString(
				parsedEntitiesNames_listBox.Items[e.Index].ToString(),
				parsedEntitiesNames_listBox.Font,
				parsedEntitiesNames_listBox.Width
			).Height;
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
			//this.userName_comboBox.DataSource = userNameVariants;
			if (userName_comboBox.Text.StartsWith("\u007f")) 
				userName_comboBox.Text = userName_comboBox.Text.Replace("\u007f", "");
			if (userName_comboBox.Text.Length > 0 /* | userNameVariants.Length > 0*/) {
				this.openAsist_button.Enabled = true;
				this.openRDP_button.Enabled = true;
				this.info_button.Enabled = Directory.Exists(Utility.configuration.inventoryPath);
				this.printerInfo_button.Enabled = File.Exists(Utility.configuration.rPrinterManagerPath);
				this.copy_button.Enabled = true;
				this.ping_button.Enabled = true;
				this.infinitePing_button.Enabled = true;
				this.ip_button.Enabled = true;
				string[] foundNames = UsernameInputProcessing.searchUsername(userName_comboBox.Text);
				int cursorPosition = userName_comboBox.SelectionStart;
				this.userName_comboBox.Items.Clear();
				this.userName_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
				this.userName_comboBox.DroppedDown = true;
				this.userName_comboBox.Select(cursorPosition, 0);
				this.userName_comboBox.Items.AddRange(foundNames);
			} else {
				this.openAsist_button.Enabled = false;
				this.openRDP_button.Enabled = false;
				this.info_button.Enabled = false;
				this.printerInfo_button.Enabled = false;
				this.copy_button.Enabled = false;
				this.ping_button.Enabled = false;
				this.infinitePing_button.Enabled = false;
				this.ip_button.Enabled = false;
				//this.userName_comboBox.DroppedDown = false;
				this.parsedEntitiesNames_listBox.DataSource = null;
				this.PCinfo_textBox.Clear();
				this.parsedEntities = new LogFileParser.foundElementInfo[0];
				this.userName_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
				this.userName_comboBox.Text = "";
				this.parsedEntity = new LogFileParser.foundElementInfo();
				this.selectedComputer = null;
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
					case Keys.Escape: {
							Environment.Exit(0);
							break;
						}
				}
				if (e.KeyData == (Keys.Back | Keys.Control)) {
					this.userName_comboBox.Text = this.userName_comboBox.Text.Remove(0, this.userName_comboBox.SelectionStart);
				}
			}
		}


		private async void parsedEntitiesNames_listBox_SelectedIndexChanged(object sender, EventArgs e) {
			try {
				if (this.userName_comboBox.Text != "") {
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
					this.ping_button.Text = "                 PING";
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

		private async void ping_button_Click(object sender, EventArgs e) {
			int time = 5;
			string selectedComputerName = this.selectedComputer;
			bool pingResalt = AdminTools.pingResalt(selectedComputer);
			while (time > 0 && selectedComputerName == selectedComputer) {
				if (pingResalt)
					ping_button.Text = $"   ПИНГУЕТСЯ ({time--})";
				else
					ping_button.Text = $"НЕ ПИНГУЕТСЯ ({time--})";
				await Task.Delay(TimeSpan.FromSeconds(1));
			}
			ping_button.Text = "                 PING";
		}

		async private void infinitePing_button_Click(object sender, EventArgs e) {
			if (selectedComputer != null) {
				await Task.Run(() => {
					Application.Run(new InfinitePing_Form(selectedComputer));
				});
			}
		}


		private async void printerInfo_button_Click(object sender, EventArgs e) {
			//await Task.Run(() => {
			//	Application.Run(new PrinterForm(selectedComputer));
			//});
			//AdminTools.GetPrintersInfo(selectedComputer);
			Utility.execProcess($"{Utility.configuration.rPrinterManagerPath} {selectedComputer}");
		}


		private void ip_button_Click(object sender, EventArgs e) {
			AdminTools.getIpByHostname(selectedComputer);
		}


		private void parsedEntitiesNames_listBox_DoubleClick(object sender, MouseEventArgs e) {
			swapSearchObject((sender as ListBox).SelectedItem.ToString());
		}


		private void parsedEntitiesNames_listBox_KeyDown(object sender, KeyEventArgs e) {
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
			extra_contextMenuStrip.Items[0].Enabled = selectedComputer != null;
			extra_contextMenuStrip.Items[1].Enabled = selectedComputer != null;
			extra_contextMenuStrip.Items[2].Enabled = selectedComputer != null;
			extra_contextMenuStrip.Items[3].Enabled = selectedComputer != null;
			extra_contextMenuStrip.Items[7].Enabled = Directory.Exists(Utility.configuration.distroPath);
			extra_contextMenuStrip.Show(extraToolsMenu_button, 1, (sender as Button).Height - 2);
		}


		private void closeAssistent() {
			if (selectedComputer != null) {
				AdminTools.killProcess(selectedComputer, "msra.exe");
			}
		}


		private async void openTaskManager() {
			if (selectedComputer != null) {
				await Task.Run(() => {
					Application.Run(new TaskManagerForm(selectedComputer));
				});
			}
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


		private void updateExtraContextMenuStripItems() {
			this.extra_contextMenuStrip.Items.Clear();
			this.extra_contextMenuStrip.Items.Add("Закрыть ассистента");
			this.extra_contextMenuStrip.Items.Add("Диспечер задач");
			extra_contextMenuStrip.Items.Add(new ToolStripSeparator());
			this.extra_contextMenuStrip.Items.Add("Поиск по выбраному элементу");
			extra_contextMenuStrip.Items.Add(new ToolStripSeparator());
			this.extra_contextMenuStrip.Items.Add("Настройки");
			extra_contextMenuStrip.Items.Add(new ToolStripSeparator());
			this.extra_contextMenuStrip.Items.Add("Distro");
			foreach (var el in Utility.configuration.extraFolders)
				this.extra_contextMenuStrip.Items.Add(el.key);
			foreach (var el in Utility.settings.extraFolders)
				this.extra_contextMenuStrip.Items.Add(el.key);
			if (Utility.configuration.extraURLs.Count + Utility.settings.extraURLs.Count > 0)
				extra_contextMenuStrip.Items.Add(new ToolStripSeparator());
			foreach (var el in Utility.configuration.extraURLs)
				this.extra_contextMenuStrip.Items.Add(el.key);
			foreach (var el in Utility.settings.extraURLs)
				this.extra_contextMenuStrip.Items.Add(el.key);

			this.extra_contextMenuStrip.Items[0].Click += (obj, eventArgs) => {
				closeAssistent();
			};
			this.extra_contextMenuStrip.Items[1].Click += (obj, eventArgs) => {
				openTaskManager();
			};
			// [2] - ToolStripSeparator
			this.extra_contextMenuStrip.Items[3].Click += (obj, eventArgs) => {
				swapSearchObject(parsedEntitiesNames_listBox.SelectedItem.ToString());
			};
			// [4] - ToolStripSeparator
			this.extra_contextMenuStrip.Items[5].Click += (obj, eventArgs) => {
				new ConfigForm();
				updateExtraContextMenuStripItems();
			};
			// [6] - ToolStripSeparator
			this.extra_contextMenuStrip.Items[7].Click += (obj, eventArgs) => {
				Utility.execProcess("explorer " + Utility.configuration.distroPath); ;
			};
			// one more ToolStripSeparator
			int selectedItem = 8;
			for (; selectedItem < Utility.configuration.extraFolders.Count + 8; selectedItem++) {
				this.extra_contextMenuStrip.Items[selectedItem].Click += (obj, eventArgs) => {
					Utility.execProcess(
						"explorer "
						+ Utility.configuration.extraFolders.First(s => s.key == (obj as ToolStripMenuItem).Text).value);
				};
			}
			// one more ToolStripSeparator
			selectedItem++;
			for (; selectedItem < Utility.configuration.extraURLs.Count + Utility.configuration.extraFolders.Count + 9; selectedItem++) {
				this.extra_contextMenuStrip.Items[selectedItem].Click += (obj, eventArgs) => {
					Utility.execProcess(
						"\"C:\\Program Files (x86)\\Google\\Chrome\\Application\\chrome.exe\" "
						+ Utility.configuration.extraURLs.First(s => s.key == (obj as ToolStripMenuItem).Text).value
					);
				};
			}
		}


	}
}