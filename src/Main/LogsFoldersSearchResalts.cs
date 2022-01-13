using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rPCSMT.src.Main {
	static class LogsFoldersSearchResalts {

		//public Users() {
		//	//userNames = getUsersNames();
		//}

		public static string[] searchResalts { get; set; }

		public static void updateSearchResalts() {
			try {
				string[] usernames = Directory.GetDirectories(Utility.configuration.loginsPath);
				for (int i = 0; i < usernames.Length; i++)
					usernames[i] = usernames[i].Substring(Utility.configuration.loginsPath.Length + 1).ToLower();
				searchResalts = usernames;
			} catch {
				//MessageBox.Show("Ошибка в имени");
			}
		}

	}
}
