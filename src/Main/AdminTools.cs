using Cassia;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pc_finnder.src.Main {
	class AdminTools {

		public void openAsist(string pcName) {
			if (pcName != null)
				Utility.execProcess($"MSRA.exe /offerra  {pcName}");
		}


		public void openRDP(string pcName) {
			if (pcName != null)
				Utility.execProcess($"MSTSC.exe /v:{pcName}");
		}


		public void GetComputerInfo(string pcName) {
			if (pcName != null & Utility.configuration.inventoryPath != String.Empty) {
				string[] founfComputers = Directory.GetFiles(Utility.configuration.inventoryPath, pcName + '*', SearchOption.AllDirectories);
				if (founfComputers.Length == 1)
					Utility.execProcess(founfComputers[0]);
			}
		}

		private static bool ping(string pcName) {
			Ping ping = new Ping();
			PingOptions pingOptions = new PingOptions();
			pingOptions.DontFragment = true;
			pingOptions.Ttl = 5;
			string data = "fff";
			byte[] buffer = Encoding.ASCII.GetBytes(data);
			try {
				return (ping.Send(pcName, 120, buffer, pingOptions).Status == IPStatus.Success);
			} catch (System.Net.NetworkInformation.PingException) {
				return false;
			}
		}


		public async void pingResalt(string pcName) {
			if (pcName != null) {
				if (ping(pcName))
					await Task.Run(() => {
						MessageBox.Show("Пингуется", pcName, MessageBoxButtons.OK, MessageBoxIcon.Information);
					});
				else
					await Task.Run(() => {
						MessageBox.Show("НЕ пингуется", pcName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					});
				//try {
				//	PingReply reply = ping.Send(pcName, 120, buffer, pingOptions);
				//	if (reply.Status == IPStatus.Success)
				//		MessageBox.Show("Пингуется", @"¯\_(ツ)_/¯", MessageBoxButtons.OK, MessageBoxIcon.Information);
				//	else
				//		MessageBox.Show("НЕ пингуется", @"¯\_(ツ)_/¯", MessageBoxButtons.OK, MessageBoxIcon.Error);
				//} catch (System.Net.NetworkInformation.PingException) {
				//	MessageBox.Show("НЕ пингуется", @"¯\_(ツ)_/¯", MessageBoxButtons.OK, MessageBoxIcon.Error);
				//}
			}
		}

		public bool checkUserLogedIn(string username, string computerName) {
			try {
				if (ping(computerName)) {
					ITerminalServer server = new TerminalServicesManager().GetRemoteServer(computerName);
					server.Open();
					//ITerminalServicesSession session = server.GetSessions().First(_session => _session.UserAccount.Value.ToLower() == username.ToLower());
					foreach (ITerminalServicesSession session in server.GetSessions()) {
						if (session.UserAccount != null /*& session.LastInputTime != null*/) {
							string userAccount = session.UserAccount.Value;
							//if (session.UserAccount.Value.ToLower().Contains(username.ToLower())
							if (userAccount.ToLower().Remove(0, userAccount.IndexOf("\\") + 1) == username.ToLower()
									& session.ConnectionState == Cassia.ConnectionState.Active
								) {
								return true;
							}
						}
					}
				} //else MessageBox.Show($"{computerName} don't ping");
			} catch {
				//MessageBox.Show(e.Message);
			}
			return false;
		}

		public async void getIpByHostname(string pcName) {
			if (pcName != String.Empty)
				if (ping(pcName)) {
					IPAddress[] iPAddresses = Dns.GetHostAddresses(pcName);
					foreach (IPAddress ip in iPAddresses)
						if (ip.AddressFamily == AddressFamily.InterNetwork & iPAddresses.Length > 0) {
							Clipboard.SetText(ip.ToString());
							await Task.Run(() => { 
								MessageBox.Show(ip.ToString() + "\n[ скопированно ]", pcName); 
							});
						}
				}
		}

		public async void openComputerInExplorer(string computerName) {
			if (computerName != String.Empty)
				await Task.Run(() => {
					Utility.execProcess("explorer.exe \\\\" + computerName + "\\c$");
				});
		}
	}
}

