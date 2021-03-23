using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pc_finnder.src.Main {
	static class Users {

		//public Users() {
		//	//userNames = getUsersNames();
		//}

		public static string[] userNames { get; set; }

		public static void updateUsersNames() {
			try {
				string[] usernames = Directory.GetDirectories(Utility.configuration.loginsPath);
				for (int i = 0; i < usernames.Length; i++)
					usernames[i] = usernames[i].Substring(Utility.configuration.loginsPath.Length + 1).ToLower();
				userNames = usernames;
			} catch {
				//MessageBox.Show("Ошибка в имени");
			}
		}

	}
}
