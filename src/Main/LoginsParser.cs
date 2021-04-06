using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pc_finnder.src.Main {
	class LoginsParser {
		//private string pathFileName = ".\\path.txt";
		//private string path;

		public struct PCinfo {
			public string name;
			public string firstLog;
			public string lastLog;
			public string lastLogType;
			public int count;
			public bool loginStatus;
			public string assisting;
			public PCinfo(string name, string firstLog, string lastLog) {
				this.name = name;
				this.lastLog = lastLog;
				this.firstLog = firstLog;
				this.count = 1;
				this.loginStatus = false;
				this.lastLogType = "LogIn";
				this.assisting = "";
			}
		}


		public LoginsParser() { }
		//if (File.Exists(pathFileName)) {
		//	path = File.ReadAllLines(pathFileName)[0];
		//	users.userNames = getUserNames(path);
		//	if (users == null) {
		//		MessageBox.Show("В файле path.txt указан неверный путь");
		//		Environment.Exit(1);
		//	}
		//} else {
		//	MessageBox.Show("Отсутствует path.txt в папке программы");
		//	//Application.Exit();
		//	Environment.Exit(1);
		//}


		public PCinfo[] ParceUsersComputers(string userName) {
			List<PCinfo> pcInfoList = new List<PCinfo>();
			try {
				string[] lines = File.ReadAllLines(Utility.configuration.loginsPath + '\\' + userName + "\\login.txt");
				foreach (string line in lines) {
					int logPosition = line.IndexOf(";Log");
					string pcName = line.Substring(23, logPosition - 23);
					if (Array.Exists(pcInfoList.ToArray(), element => element.name == pcName)) {
						//int numOfCurPC = Array.IndexOf(pcInfoList.ToArray(), pcName);
						//if(numOfCurPC >= 0) {
						for (int i = 0; i < pcInfoList.Count; i++) {
							//foreach(PCinfo pcinfo in pcInfoList) {
							if (pcName == pcInfoList[i].name) {
								PCinfo tmp = pcInfoList[i];
								tmp.count++;
								tmp.lastLog = $"{line.Substring(0, 6)}{line.Substring(8, 2)}\t{line.Substring(11, 5)}";
								tmp.lastLogType = line.Substring(logPosition + 1, line.IndexOf(';', logPosition + 1) - 1 - logPosition);
								pcInfoList[i] = tmp;
								break;
							}
						}
					} else {
						string date = $"{line.Substring(0, 6)}{line.Substring(8, 2)}\t{line.Substring(11, 5)}";
						string lastLogType = line.Substring(logPosition+1, line.IndexOf(';', logPosition + 1) - 1 - logPosition);
						pcInfoList.Add(new PCinfo(pcName, date, date));
					}
				}
			} catch {
				MessageBox.Show("Ошибка при парсинге логов");
			}
			return pcInfoList.ToArray();
		}


		public List<string> getPcNames(PCinfo[] PCsOfSelectedUser) {
			List<string> pcNames = new List<string>();
			for (int i = 0; i < PCsOfSelectedUser.Length; i++) {
				pcNames.Add(PCsOfSelectedUser[i].name);
			}
			return pcNames;
		}

	}
}
