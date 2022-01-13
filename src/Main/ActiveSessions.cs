using Cassia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace rPCSMT.src.Main {
	class ActiveSessions {

		public ActiveSessions() { }

		private void SearchActiveSessions(LogFileParser. foundElementInfo [] selectedUserPCs, string selectedUser) {
			ITerminalServicesManager manager = new TerminalServicesManager();
			for (int i = 0; i < selectedUserPCs.Length; i++) {
				try {
					Ping ping = new Ping();
					PingOptions pingOptions = new PingOptions();
					pingOptions.DontFragment = true;
					string data = "fff";
					byte[] buffer = Encoding.ASCII.GetBytes(data);
					PingReply reply = ping.Send(selectedUserPCs[i].name, 120, buffer, pingOptions);
					if (reply.Status == IPStatus.Success) {
						using (ITerminalServer server = manager.GetRemoteServer(selectedUserPCs[i].name)) {
							server.Open();
							foreach (ITerminalServicesSession session in server.GetSessions()) {
								if (session.UserName.ToLower() == selectedUser.ToLower() && session.ConnectionState == ConnectionState.Active)
									selectedUserPCs[i].name = "->" + selectedUserPCs[i].name + "<-";
							}
						}
					}
				} catch { }
			}
		}
	}
}
