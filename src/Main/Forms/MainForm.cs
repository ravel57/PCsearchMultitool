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
using pc_finnder.src.Main;
using System.Globalization;
using System.Reflection;
//using System.Linq;

namespace pc_finnder {
	public partial class MainForm : Form {
		LoginsParser loginsParser = new LoginsParser();
		UsernameInputProcessing usernameInputProcessing = new UsernameInputProcessing();
		AdminTools adminTools = new AdminTools();

		private string selectedUser;
		LoginsParser.PCinfo[] computersOfSelectedUser;
		LoginsParser.PCinfo selectedComputerinfo;
		enum sortBy { DATA = 0, LOGS = 1 };
		sortBy selectedSortingMethod = sortBy.DATA;

		//static Debug_Form debug_Form;
		//async static void runDebugForm () {
		//	debug_Form = new Debug_Form();
		//	await Task.Run(() => {
		//		Application.Run(debug_Form);
		//	});
		//}


		public MainForm() {
			InitializeComponent();
		}

		//on load
		private async void MainForm_Load(object sender, EventArgs e) {
			if (Utility.DEBUG_MESSAGES) {
				MessageBox.Show("current path: " + Directory.GetCurrentDirectory()
				   + "\nappdata path: " + Utility.APPDATA_PATH
				   + "\ncurrent path == appdata path: " + String.Equals(Directory.GetCurrentDirectory(), Utility.APPDATA_PATH)
				   + "\nRUN_FROM_APPDATA: " + Utility.RUN_FROM_APPDATA,
				   "new start"
				);
			}
			if (Directory.GetCurrentDirectory() != Utility.APPDATA_PATH & Utility.RUN_FROM_APPDATA) {
				if (Utility.DEBUG_MESSAGES) { MessageBox.Show("running local"); }
				Utility.RunLocal();
			} else {
				if (Utility.DEBUG_MESSAGES) { MessageBox.Show("runned local"); }
				Utility.run();
				//window position 
				this.Location = Utility.settings.windowCord;
				//redraw listbox to select active computer
				this.PCs_listBox.DrawMode = DrawMode.OwnerDrawVariable;
				this.PCs_listBox.MeasureItem += new MeasureItemEventHandler(PCs_listBox_MeasureItem);
				this.PCs_listBox.DrawItem += new DrawItemEventHandler(PCs_listBox_DrawItem);
				this.version_label.Text = Utility.VERSION;
				this.info_button.Enabled = (Directory.Exists(Utility.configuration.inventoryPath));
				await Task.Run(async () => {
					while (true) {
						Users.updateUsersNames();
						await Task.Delay(TimeSpan.FromSeconds(60));
						this.PCs_listBox_SelectedIndexChanged(sender, e);
					}
				});
			}
		}

		private void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			Utility.settings.saveSettings(this.Location);
		}


		//need tests
		private void PCs_listBox_MeasureItem(object sender, MeasureItemEventArgs e) {
			e.ItemHeight = (int)e.Graphics.MeasureString(PCs_listBox.Items[e.Index].ToString(), PCs_listBox.Font, PCs_listBox.Width).Height;
		}

		private void PCs_listBox_DrawItem(object sender, DrawItemEventArgs e) {
			e.DrawBackground();
			////need tests
			//e.DrawFocusRectangle();
			//e.Graphics.DrawString(PCs_listBox.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds);
			////
			bool isItemSelected = ((e.State & DrawItemState.Selected) == DrawItemState.Selected);
			int itemIndex = e.Index;
			if (itemIndex >= 0 && itemIndex < PCs_listBox.Items.Count) {
				Graphics g = e.Graphics;
				///////
				string itemText = PCs_listBox.Items[itemIndex].ToString();

				// Background Color	
				SolidBrush backgroundColorBrush;
				SolidBrush itemTextColorBrush;
				//if (!computersOfSelectedUser.First(pc => pc.name == itemText).loginStatus)
				if (!computersOfSelectedUser.First(pc => itemText.IndexOf(pc.name) >= 0).loginStatus) {
					if (computersOfSelectedUser.First(pc => itemText.IndexOf(pc.name) >= 0).assisting != String.Empty) {
						backgroundColorBrush = new SolidBrush((isItemSelected) ? Color.SeaGreen : Color.White);
						itemTextColorBrush = (isItemSelected) ? new SolidBrush(Color.White) : new SolidBrush(Color.SeaGreen);
					} else {
						backgroundColorBrush = new SolidBrush((isItemSelected) ? Color.Transparent : Color.White);
						itemTextColorBrush = (isItemSelected) ? new SolidBrush(Color.White) : new SolidBrush(Color.Black);
					}
				} else {
					if (computersOfSelectedUser.First(pc => itemText.IndexOf(pc.name) >= 0).assisting != String.Empty) {
						backgroundColorBrush = new SolidBrush((isItemSelected) ? Color.Orange : Color.White);
						itemTextColorBrush = (isItemSelected) ? new SolidBrush(Color.White) : new SolidBrush(Color.Orange);
					} else {
						backgroundColorBrush = new SolidBrush((isItemSelected) ? Color.Red : Color.White);
						itemTextColorBrush = (isItemSelected) ? new SolidBrush(Color.White) : new SolidBrush(Color.Red);
					}
				}

				g.FillRectangle(backgroundColorBrush, e.Bounds);

				// Set text color
				//if (!computersOfSelectedUser.First(pc => itemText.IndexOf(pc.name) >= 0).loginStatus)
				//	itemTextColorBrush = (isItemSelected) ? new SolidBrush(Color.White) : new SolidBrush(Color.Black);
				//else
				//	itemTextColorBrush = (isItemSelected) ? new SolidBrush(Color.White) : new SolidBrush(Color.Red);

				//if (computersOfSelectedUser.First(pc => pc.name == itemText).assisting == String.Empty)
				g.DrawString(itemText, e.Font, itemTextColorBrush, PCs_listBox.GetItemRectangle(itemIndex).Location);
				//else {
				//	itemText += "\nmsra: " + computersOfSelectedUser.First(pc => pc.name == itemText).assisting;
				//	g.DrawString(itemText, e.Font, itemTextColorBrush, PCs_listBox.GetItemRectangle(itemIndex).Location);
				//	//this.PCs_listBox.DrawItem += new DrawItemEventHandler(PCs_listBox_DrawItem);
				//}
				//Задник
				//g.DrawString(itemText, new Font(FontFamily.GenericSansSerif, 8), new SolidBrush(Color.Black), e.Bounds);		
				//g.FillRectangle(new SolidBrush(Color.Silver), e.Bounds);
				//Текст
				//e.Graphics.DrawString(this.PCs_listBox.Items[e.Index].ToString(), new Font(FontFamily.GenericSansSerif, 8), new SolidBrush(c), e.Bounds);

				// Clean up
				backgroundColorBrush.Dispose();
				itemTextColorBrush.Dispose();
			}

			//e.Graphics.DrawString(PCs_listBox.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds);
			e.DrawFocusRectangle();
		}


		private void userName_comboBox_TextChanged(object sender, EventArgs e) {
			string[] userNameVariants = usernameInputProcessing.searchUsername(userName_comboBox.Text);
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
				this.PCs_listBox.DataSource = null;
				this.PCinfo_textBox.Clear();
				this.computersOfSelectedUser = new LoginsParser.PCinfo[0];
				this.userName_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
				this.userName_comboBox.Text = "";
				this.selectedComputerinfo = new LoginsParser.PCinfo();
			}
			//чтобы курсор не падал за список и был виден
			Cursor.Current = Cursors.Default;
		}

		private void sortComputers(ref LoginsParser.PCinfo[] computers) {
			switch (selectedSortingMethod) {
				case sortBy.LOGS: {
						Array.Sort<LoginsParser.PCinfo>(computersOfSelectedUser, (x, y) => y.count.CompareTo(x.count));
						break;
					}
				case sortBy.DATA: {
						Array.Sort<LoginsParser.PCinfo>(computersOfSelectedUser, (x, y) => (
								Convert.ToDateTime(y.lastLog.Substring(0, 14)).CompareTo(
								Convert.ToDateTime(x.lastLog.Substring(0, 14)))
						));
						break;
					}
			}
		}

		private async void UserName_comboBox_SelectedIndexChanged(object sender, EventArgs e) {
			this.selectedUser = this.userName_comboBox.GetItemText(this.userName_comboBox.SelectedItem);
			if (this.userName_comboBox.SelectedItem == null)
				this.userName_comboBox.SelectedItem = 0;
			//await Task.Run(async () => {
			//	while (true) {
			computersOfSelectedUser = loginsParser.ParceUsersComputers(selectedUser);
			sortComputers(ref computersOfSelectedUser);
			this.PCs_listBox.DataSource = loginsParser.getPcNames(computersOfSelectedUser);
			//this.PCs_listBox.SelectedIndex = 0;
			for (int i = 0; i < computersOfSelectedUser.Length; i++) {
				//DateTime dt = DateTime.ParseExact(computersOfSelectedUser[i].lastLog.Substring(0, 8), $"dd{r}MM{r}yy", CultureInfo.InvariantCulture);
				DateTime dt = Convert.ToDateTime(computersOfSelectedUser[i].lastLog.Substring(0, 8));
				if (dt.AddMonths(4) >= DateTime.Now) {
					if (await Task.Run(() => adminTools.checkUserLogedIn(selectedUser, computersOfSelectedUser[i].name))) {
						computersOfSelectedUser[i].loginStatus = true;
						this.PCs_listBox.Refresh();
						//this.PCs_listBox.DrawItem += new DrawItemEventHandler(PCs_listBox_DrawItem);
					}
					try {
						if (await Task.Run(() => (adminTools.checkAsistentConnected(computersOfSelectedUser[i].name) != String.Empty))) {
							computersOfSelectedUser[i].assisting += adminTools.checkAsistentConnected(computersOfSelectedUser[i].name);
							int curIndex = this.PCs_listBox.SelectedIndex;
							List<string> sa = loginsParser.getPcNames(computersOfSelectedUser);
							sa[i] += "\n    msra: " + adminTools.checkAsistentConnected(computersOfSelectedUser[i].name);
							this.PCs_listBox.DataSource = sa;
							this.PCs_listBox.Refresh();
							this.PCs_listBox.SelectedIndex = curIndex;
							//this.PCs_listBox.DrawItem += new DrawItemEventHandler(PCs_listBox_DrawItem);
						};
					} catch {
					}
				}
			}
			//		await Task.Delay(TimeSpan.FromSeconds(60));
			//		//this.PCs_listBox.DrawItem += new DrawItemEventHandler(PCs_listBox_DrawItem);
			//		//computersOfSelectedUser == loginsParser.ParceUsersComputers(selectedUser);
			//	}
			//});
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
			PCs_listBox.Focus();
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
							if (this.userName_comboBox.SelectedIndex <= -1)
								this.userName_comboBox.SelectedIndex = 0;
							//this.PCs_listBox.SelectedIndex = 0;
							this.PCs_listBox.Focus();
							break;
						}
				}
				if (e.KeyData == (Keys.Back | Keys.Control)) {
					userName_comboBox.Text = "";
				}
			}
		}


		private void PCs_listBox_SelectedIndexChanged(object sender, EventArgs e) {
			try {
				if (this.userName_comboBox.Text != "\u007f" & this.userName_comboBox.Text != "") {
					LoginsParser.PCinfo selectedPC = computersOfSelectedUser.First(pc =>
						pc.name == this.PCs_listBox.SelectedItem.ToString()
					);
					this.PCinfo_textBox.Lines = new string[4] {
					selectedPC.name,
					selectedPC.count.ToString(),
					selectedPC.firstLog,
					selectedPC.lastLog + " [" + selectedPC.lastLogType + "]"
				};
					selectedComputerinfo = selectedPC;
				}
			} catch (Exception) { }
		}



		private void openAsist_button_Click(object sender, EventArgs e) {
			adminTools.openAsist(selectedComputerinfo.name);
		}


		private void openRDP_button_Click(object sender, EventArgs e) {
			adminTools.openRDP(selectedComputerinfo.name);
		}


		private void info_button_Click(object sender, EventArgs e) {
			adminTools.GetComputerInfo(selectedComputerinfo.name);
		}

		private void copy_button_Click(object sender, EventArgs e) {
			if (selectedComputerinfo.name != null)
				Clipboard.SetText(selectedComputerinfo.name);
		}

		private void explorer_button_Click(object sender, EventArgs e) {
			adminTools.openComputerInExplorer(selectedComputerinfo.name);
		}

		private void ping_button_Click(object sender, EventArgs e) {
			adminTools.pingResalt(selectedComputerinfo.name);
		}

		async private void infinitePing_button_Click(object sender, EventArgs e) {
			if (selectedComputerinfo.name != null) {
				await Task.Run(() => {
					Application.Run(new InfinitePing_Form(selectedComputerinfo.name));
				});
			}
		}

		private void ip_button_Click(object sender, EventArgs e) {
			adminTools.getIpByHostname(selectedComputerinfo.name);
		}

		private void searchType_label_Click(object sender, EventArgs e) {

		}

		private void PCs_listBox_DoubleClick(object sender, MouseEventArgs e) {
			try {
				this.userName_comboBox.Text = (sender as ListBox).SelectedItem.ToString();
			} catch (NullReferenceException) { }
		}
	}
}