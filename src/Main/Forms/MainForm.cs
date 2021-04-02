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
//using System.Linq;

namespace pc_finnder {
	public partial class MainForm : Form {
		LoginsParser loginsParser = new LoginsParser();
		UsernameInputProcessing usernameInputProcessing = new UsernameInputProcessing();
		AdminTools adminTools = new AdminTools();

		private string selectedUser;
		LoginsParser.PCinfo[] computersOfSelectedUser;
		LoginsParser.PCinfo selectedPCinfo;
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
		private void MainForm_Load(object sender, EventArgs e) {
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
				Users.updateUsersNames();
				this.version_label.Text = Utility.VERSION;
			}
			//redraw listbox to select active computer
			this.PCs_listBox.DrawMode = DrawMode.OwnerDrawVariable;
			this.PCs_listBox.DrawItem += new DrawItemEventHandler(PCs_listBox_DrawItem);
			//window position 
			this.Location = Utility.settings.windowCord;
		}

		private void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			Utility.settings.saveSettings(this.Location);
		}

		private void PCs_listBox_DrawItem(object sender, DrawItemEventArgs e) {
			e.DrawBackground();

			bool isItemSelected = ((e.State & DrawItemState.Selected) == DrawItemState.Selected);
			int itemIndex = e.Index;
			if (itemIndex >= 0 && itemIndex < PCs_listBox.Items.Count) {
				Graphics g = e.Graphics;
				///////
				string itemText = PCs_listBox.Items[itemIndex].ToString();

				// Background Color	
				SolidBrush backgroundColorBrush;
				if (!computersOfSelectedUser.First(pc => pc.name == itemText).loginStatus)
					backgroundColorBrush = new SolidBrush((isItemSelected) ? Color.Transparent : Color.White);
				else
					backgroundColorBrush = new SolidBrush((isItemSelected) ? Color.Red : Color.White);
				g.FillRectangle(backgroundColorBrush, e.Bounds);

				// Set text color
				SolidBrush itemTextColorBrush;
				if (!computersOfSelectedUser.First(pc => pc.name == itemText).loginStatus)
					itemTextColorBrush = (isItemSelected) ? new SolidBrush(Color.White) : new SolidBrush(Color.Black);
				else
					itemTextColorBrush = (isItemSelected) ? new SolidBrush(Color.White) : new SolidBrush(Color.Red);
				g.DrawString(itemText, e.Font, itemTextColorBrush, PCs_listBox.GetItemRectangle(itemIndex).Location);
				//Задник
				//g.DrawString(itemText, new Font(FontFamily.GenericSansSerif, 8), new SolidBrush(Color.Black), e.Bounds);		
				//g.FillRectangle(new SolidBrush(Color.Silver), e.Bounds);
				//Текст
				//e.Graphics.DrawString(this.PCs_listBox.Items[e.Index].ToString(), new Font(FontFamily.GenericSansSerif, 8), new SolidBrush(c), e.Bounds);

				// Clean up
				backgroundColorBrush.Dispose();
				itemTextColorBrush.Dispose();
			}

			e.DrawFocusRectangle();
		}


		private void userName_comboBox_TextChanged(object sender, EventArgs e) {
			string[] userNameVariants = usernameInputProcessing.searchUsername(userName_comboBox.Text);

			//this.userName_comboBox.DataSource = userNameVariants;
			if (userName_comboBox.Text.Length > 0/* | userNameVariants.Length > 0*/) {
				int cursorPosition = userName_comboBox.SelectionStart;
				this.userName_comboBox.Items.Clear();
				this.userName_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
				this.userName_comboBox.DroppedDown = true;
				this.userName_comboBox.Select(cursorPosition, 0);
				this.userName_comboBox.Items.AddRange(userNameVariants);
			} else {
				this.userName_comboBox.DroppedDown = false;
				//this.PCs_listBox.Items.Clear();
				this.PCs_listBox.DataSource = null;
				this.PCinfo_textBox.Clear();
				//this.PCsOfSelectedUser = null;
				this.computersOfSelectedUser = new LoginsParser.PCinfo[0];
				this.userName_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
				this.userName_comboBox.Text = "";
				this.selectedPCinfo = new LoginsParser.PCinfo();
			}
			//чтобы курсор не падал за список и был виден
			Cursor.Current = Cursors.Default;
		}



		private async void UserName_comboBox_SelectedIndexChanged(object sender, EventArgs e) {
			this.selectedUser = this.userName_comboBox.GetItemText(this.userName_comboBox.SelectedItem);
			if (this.userName_comboBox.SelectedItem == null)
				this.userName_comboBox.SelectedItem = 0;

			/*ParseLogins.PCinfo[]*/
			computersOfSelectedUser = loginsParser.ParceUsersComputers(selectedUser);
			switch (selectedSortingMethod) {
				case sortBy.LOGS: {
						Array.Sort<LoginsParser.PCinfo>(computersOfSelectedUser, (x, y) => y.count.CompareTo(x.count));
						break;
					}
				case sortBy.DATA: {
						Array.Sort<LoginsParser.PCinfo>(computersOfSelectedUser, (x, y) => (
							Convert.ToDateTime(y.lastLog.Substring(0, 14))
							//		(y.lastLog.Substring(6, 2) + y.lastLog.Substring(3, 2) + y.lastLog.Substring(0, 2)
							//		+ y.lastLog.Substring(9, 2) + y.lastLog.Substring(12, 2))
							.CompareTo(
								Convert.ToDateTime(x.lastLog.Substring(0, 14))
							//		(x.lastLog.Substring(6, 2) + x.lastLog.Substring(3, 2) + x.lastLog.Substring(0, 2)
							//		+ x.lastLog.Substring(9, 2)) + x.lastLog.Substring(12, 2)
							)
						));
						break;
					}
			}
			List<string> sortedPcNames = loginsParser.getPcNames(computersOfSelectedUser);

			this.PCs_listBox.DataSource = sortedPcNames;
			//this.PCs_listBox.Items.Clear(); this.PCs_listBox.Items.AddRange(sortedPcNames.ToArray());
			this.PCs_listBox.SelectedIndex = 0;

			//foreach (LoginsParser.PCinfo pc in PCsOfSelectedUser) {
			//	if (await Task.Run(() => adminTools.checkUserLogedIn(selectedUser, pc.name))) {
			//		int activPcPosition = PCs_listBox.Items.IndexOf(pc.name);
			//		if (activPcPosition >= 0) {
			//			int listBoxPosition = PCs_listBox.SelectedIndex;
			//			PCs_listBox.Items.RemoveAt(activPcPosition);
			//			PCs_listBox.Items.Insert(activPcPosition, "→" + pc.name + "←");
			//			PCs_listBox.SelectedIndex = listBoxPosition;
			//		}
			//	}
			//}

			//List<string> pcs = new List<string>();
			for (int i = 0; i < computersOfSelectedUser.Length; i++) {
				//if (PCsOfSelectedUser[i].lastLog.Substring(7,8))
				DateTime dt = DateTime.ParseExact(computersOfSelectedUser[i].lastLog.Substring(0, 8), "dd.MM.yy", CultureInfo.InvariantCulture);
				//MessageBox.Show(dt.AddMonths(4).ToString());
				if (dt.AddMonths(4) >= DateTime.Now)
					//await Task.Run(() => PCsOfSelectedUser[i].loginStatus = adminTools.checkUserLogedIn(selectedUser, PCsOfSelectedUser[i].name));
					if (await Task.Run(() => adminTools.checkUserLogedIn(selectedUser, computersOfSelectedUser[i].name))) {
						computersOfSelectedUser[i].loginStatus = true;
						this.PCs_listBox.Refresh();
						//pcs.Add(PCsOfSelectedUser[i].name);
					}
			}
			//MessageBox.Show(String.Join("\n",pcs));
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
			if (userName_comboBox.Text != "") {
				UserName_comboBox_SelectedIndexChanged(sender, e);
				PCs_listBox.SelectedIndex = 0;
			}
			//userName_comboBox.Focus(); // Focused
			PCs_listBox.Focus();
		}
		private void UserName_comboBox_KeyDown(object sender, KeyEventArgs e) {
			if (userName_comboBox.Text.Length > 0) {
				switch (e.KeyCode) {
					case Keys.Enter: {
							if (this.userName_comboBox.SelectedIndex == -1)
								this.userName_comboBox.SelectedIndex = 0;
							this.PCs_listBox.SelectedIndex = 0;
							this.PCs_listBox.Focus();
							break;
						}
					case Keys.Escape: {
							Environment.Exit(0);
							break;
						}

				}
				if (e.KeyData == (Keys.Back | Keys.Control)) {
					//MessageBox.Show(e.KeyCode.ToString());
					userName_comboBox.Text = "";
				}
			}
		}


		private void PCs_listBox_SelectedIndexChanged(object sender, EventArgs e) {
			try {
				LoginsParser.PCinfo selectedPC = computersOfSelectedUser.First(pc => pc.name == this.PCs_listBox.GetItemText(this.PCs_listBox.SelectedItem));
				this.PCinfo_textBox.Lines = new string[4] {
					selectedPC.name,
					selectedPC.count.ToString(),
					selectedPC.firstLog,
					selectedPC.lastLog + " [" + selectedPC.lastLogType + "]"
				};
				selectedPCinfo = selectedPC;
			} catch { }
		}



		private void openAsist_button_Click(object sender, EventArgs e) {
			adminTools.openAsist(selectedPCinfo.name);
		}


		private void openRDP_button_Click(object sender, EventArgs e) {
			adminTools.openRDP(selectedPCinfo.name);
		}


		private void info_button_Click(object sender, EventArgs e) {
			adminTools.GetComputerInfo(selectedPCinfo.name);
		}

		private void copy_button_Click(object sender, EventArgs e) {
			if (selectedPCinfo.name != null)
				Clipboard.SetText(selectedPCinfo.name);
		}

		private void explorer_button_Click(object sender, EventArgs e) {
			adminTools.openComputerInExplorer(selectedPCinfo.name);
		}

		private void ping_button_Click(object sender, EventArgs e) {
			adminTools.pingResalt(selectedPCinfo.name);
		}

		async private void infinitePing_button_Click(object sender, EventArgs e) {
			if (selectedPCinfo.name != null) {
				await Task.Run(() => {
					Application.Run(new InfinitePing_Form(selectedPCinfo.name));
				});
			}
		}

		private void ip_button_Click(object sender, EventArgs e) {
			adminTools.getIpByHostname(selectedPCinfo.name);
		}

	}
}