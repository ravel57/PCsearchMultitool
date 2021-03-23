using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Resources;

namespace pc_finnder {
	static class Program {
		/// <summary>
		/// Главная точка входа для приложения.
		/// </summary>
		[STAThread]
		static void Main(string[] args) {
			Application.EnableVisualStyles();
			Application.Run(new MainForm());


			//else if (args[0] == "-debug"){
			//Application.Run(new Debug_Form());
		}
	}
}
