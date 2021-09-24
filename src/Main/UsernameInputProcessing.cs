using Cassia;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pc_finnder.src.Main {
	class UsernameInputProcessing {

		//private string[] users;
		public UsernameInputProcessing() { }

		LoginsParser parseLogins = new LoginsParser();

		string[] translitUsername(string inpStr) {
			//StringBuilder outStr = new StringBuilder();
			List<StringBuilder> outStrbuilderArray = new List<StringBuilder>();
			outStrbuilderArray.Add(new StringBuilder(""));
			int step = 1;
			char[] rusAlphabet = { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н',
				'о', 'п', 'р', 'с','т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я'};
			string[][] engTranslitAlphabet = new string[33][] {
				new[] {"a"}, new[] {"b"}, new[] {"v", "w"}, new[] {"g"}, new[] {"d"}, new[] {"e"}, new[] {"e", "yo"}, new[] {"zh", "j" },
				new[] {"z"}, new[] {"i"}, new[] {"y", "i"}, new[] {"k"}, new[] {"l"}, new[] {"m"}, new[] {"n"}, new[] {"o"}, new[] {"p"},
				new[] {"r"}, new[] {"s"}, new[] {"t"}, new[] {"u"}, new[] {"f"}, new[] {"h"}, new[] {"c", "tc", "ts"}, new[] {"ch"}, new[] {"sh"},
				new[] {"sh"}, new[] {""}, new[] {"i", "y"}, new[] {""}, new[] {"e"}, new[] {"u", "j", "yu", "y" }, new[] {"ya"}
			};
			char[] engAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToLower().ToCharArray();
			foreach (char currentCharFromImputStr in inpStr.ToLower()) {
				int index = Array.IndexOf(rusAlphabet, currentCharFromImputStr);
				if (index >= 0) {
					int count = outStrbuilderArray.Count;
					for (int j = 0; j < count; j++)
						for (int i = 0; i < engTranslitAlphabet[index].Length - 1; i++)
							outStrbuilderArray.Add(new StringBuilder(outStrbuilderArray[j].ToString()));

					for (int engLetter = 0; engLetter < engTranslitAlphabet[index].Length; engLetter++)
						for (int i = 0; i < count; i++)
							outStrbuilderArray[step * engLetter + i].Append(engTranslitAlphabet[index][engLetter]);
					step *= engTranslitAlphabet[index].Length;
				} else if (rusAlphabet.Contains(currentCharFromImputStr) | engAlphabet.Contains(currentCharFromImputStr))
					foreach (StringBuilder outStr in outStrbuilderArray)
						outStr.Append(currentCharFromImputStr);
			}
			string[] outStrArr = new string[outStrbuilderArray.Count];
			for (int i = 0; i < outStrbuilderArray.Count; i++) {
				outStrArr[i] = outStrbuilderArray[i].ToString();
			}
			return outStrArr;
		}


		public string[] searchUsername(string inpUserName) {
			string[] inpUserNameVariants = translitUsername(inpUserName.ToLower());
			List<string> outStrList = new List<string>();
			foreach (string username in Users.userNames) {
				foreach (string userNameVarriant in inpUserNameVariants)
					if (username.IndexOf(userNameVarriant) >= 0)
						outStrList.Add(username);
			}
			return outStrList.ToArray();
		}

	}
}
