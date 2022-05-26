using System;
using System.Linq;
using System.Windows.Forms;
using rPCSMT.src.Main;
using System.Web;
using System.IO;
using System.Text;
using System.Diagnostics;

namespace rPCSMT {
	static class Program {
		/// <summary>
		/// Главная точка входа для приложения.
		/// </summary>
		[STAThread]
		static void Main(string[] args) {
			Application.EnableVisualStyles();
			if (args.Contains("--service") | args.Contains("-s"))
				Utility.RUN_FROM_APPDATA = false;
			string rpcsmtUrl = args.FirstOrDefault(el => el.StartsWith("rpcsmt://"));
			if (rpcsmtUrl != null) {
				// by default run from "c:\windows\system32"
				Directory.SetCurrentDirectory(Utility.APPDATA_PATH);
				string[] strs = HttpUtility.UrlDecode(rpcsmtUrl.Remove(0, 8).Replace("/", ""))
					.Split(' ').Where(s => s != "").ToArray();
				if (strs.FirstOrDefault(s => s.StartsWith("(") && s.EndsWith(")")) == null)
					Utility.inputString = strs[1];
				else
					Utility.inputString = strs[2].Replace("(", "").Replace(")", "");
			}
			Application.Run(new MainForm());
		}
	}
}
