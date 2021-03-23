using Cassia;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
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
			if (pcName != null) {
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
				return (ping.Send(pcName, 120, buffer, pingOptions).Status == IPStatus.Success) ? true : false;
			} catch (System.Net.NetworkInformation.PingException) {
				return false;
			}
		}


		public void pingResalt(string pcName) {
			if (pcName != null) {
				if (ping(pcName))
					MessageBox.Show("Пингуется", @"¯\_(ツ)_/¯", MessageBoxButtons.OK, MessageBoxIcon.Information);
				else
					MessageBox.Show("НЕ пингуется", @"¯\_(ツ)_/¯", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
								& session.ConnectionState == Cassia.ConnectionState.Active) {
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
	}
}

