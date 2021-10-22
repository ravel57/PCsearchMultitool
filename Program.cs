using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Resources;
using pc_finnder.src.Main;

namespace pc_finnder {
	static class Program {
		/// <summary>
		/// Главная точка входа для приложения.
		/// </summary>
		[STAThread]
		static void Main(string[] args) {
			Application.EnableVisualStyles();
			if (args.Contains("-debug")) Utility.RUN_FROM_APPDATA = false;
			Application.Run(new MainForm());
		}
	}
}
