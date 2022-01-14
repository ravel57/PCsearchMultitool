using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace rPCSMT.src.Main {
	static class LogFileParser {

		public class foundElementInfo {
			public string name;
			public string firstLog;
			public string lastLog;
			public string lastLogType;
			public int count;
			public bool loginStatus;
			public string assisting;

			public foundElementInfo() { }
			public foundElementInfo(string name, string firstLog, string lastLog) {
				this.name = name;
				this.lastLog = lastLog;
				this.firstLog = firstLog;
				this.count = 1;
				this.loginStatus = false;
				this.lastLogType = "LogIn";
				this.assisting = "";
			}

			public void update(string lastLog, string lastLogType) {
				this.lastLog = lastLog;
				this.lastLogType = lastLogType;
				count++;
			}
		}

		public static foundElementInfo[] ParceUsersComputers(string searchElement) {
			List<foundElementInfo> foundElementInfoList = new List<foundElementInfo>();
			try {
				string[] lines = File.ReadAllLines(Utility.configuration.loginsPath + '\\' + searchElement + "\\login.txt");
				foreach (string line in lines) {
					int logPosition = line.IndexOf(";Log");
					// "DD.MM.YYYY;hh:mm:ss,ms;".length = 23 
					string computerName = line.Substring(23, logPosition - 23);
					string lastLogType = line.Substring(logPosition + 1, line.IndexOf(';', logPosition + 1) - 1 - logPosition);
					string username = line.Substring(logPosition + lastLogType.Length + 2).Replace(" ", "");
					string searchStr = String.Empty;
					string lastLogDate = $"{line.Substring(0, 6)}{line.Substring(8, 2)}\t{line.Substring(11, 5)}";
					switch (MainForm.selectedSearchType) {
						case MainForm.SearchFor.users:
							searchStr = computerName;
							break;
						case MainForm.SearchFor.computers:
							searchStr = username.ToLower();
							break;
					}
					//pcInfoList.Select(..)
					if (foundElementInfoList.Exists(element => element.name == searchStr)) {
						for (int i = 0; i < foundElementInfoList.Count; i++)
							if (searchStr == foundElementInfoList[i].name) {
								foundElementInfoList[i].update(lastLogDate, lastLogType);
								break;
							}
					} else {
						foundElementInfoList.Add(new foundElementInfo(searchStr, lastLogDate, lastLogDate));
					}
				}
			} catch {
				MessageBox.Show("Ошибка при парсинге логов");
			}
			return foundElementInfoList.ToArray();
		}


		public static List<string> getNames(foundElementInfo[] PCsOfSelectedUser) {
			List<string> pcNames = new List<string>();
			for (int i = 0; i < PCsOfSelectedUser.Length; i++) {
				pcNames.Add(PCsOfSelectedUser[i].name);
			}
			return pcNames;
		}

	}
}
