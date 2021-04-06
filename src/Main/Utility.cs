using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace pc_finnder.src.Main {
	static class Utility {

		public static bool RUN_FROM_APPDATA = true;
		public static bool DEBUG_MESSAGES = false;
		public static string VERSION = "1.5 beta";
		public class Configuration {
			public string loginsPath; //{ get; set;} 
			public string inventoryPath; //{ get; set;} 
		}
		public class Settings {
			public Point windowCord = new Point(0, 0);
			public void saveSettings(Point cord) {
				settings.windowCord = cord;
				File.WriteAllText("set.json", JsonConvert.SerializeObject(settings));
			}
		}

		public static string APPDATA_PATH { get => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\rPCSMT\\" + VERSION; }
		public static Configuration configuration = new Configuration();
		public static Settings settings = new Settings();

		public static async void RunLocal() {
			try {
				CopyDirectory(".", APPDATA_PATH, true);
				//await Task.Run(() => {
				//MessageBox.Show(localPath);
				Directory.SetCurrentDirectory(APPDATA_PATH);
				if (Utility.DEBUG_MESSAGES) { MessageBox.Show("starting rPCSMT.exe"); }
				Utility.execProcess("rPCSMT.exe");
				//});
				Environment.Exit(0);
			} catch (Exception e) { MessageBox.Show(e.Message); }
		}

		public static void run() {
			try {
				string configJson = File.ReadAllText("config.json").Replace('\\', '/');
				//MessageBox.Show(configJson);
				configuration.loginsPath = JsonConvert.DeserializeObject<Configuration>(configJson).loginsPath.Replace('/', '\\');
				configuration.inventoryPath = JsonConvert.DeserializeObject<Configuration>(configJson).inventoryPath.Replace('/', '\\');
			} catch {
				MessageBox.Show("Файл \'config.json\' не найден или поврежден");
			}
			try {
				if (File.Exists(".\\set.json")) {
					string setJson = File.ReadAllText(".\\set.json");
					settings.windowCord = JsonConvert.DeserializeObject<Settings>(setJson).windowCord;
				} else {
					settings.windowCord = new Point((Screen.AllScreens.Length > 1 ? Screen.PrimaryScreen.Bounds.Width + 100 : 100), 50);
					//MessageBox.Show(JsonConvert.SerializeObject(windowCord));
					File.WriteAllText("set.json", JsonConvert.SerializeObject(settings));
				}
			} catch {
				MessageBox.Show("Файл \'set.json\' поврежден");
			}
		}

		public static void execProcess(string proces) {
			System.Diagnostics.Process process = new System.Diagnostics.Process();
			System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
			startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
			startInfo.FileName = "cmd.exe";
			startInfo.Arguments = $"/c {proces}";
			process.StartInfo = startInfo;
			process.Start();
		}


		static void CopyDirectory(string sourceDirName, string destDirName, bool copySubDirs) {
			// Get the subdirectories for the specified directory.
			DirectoryInfo dir = new DirectoryInfo(sourceDirName);

			if (!dir.Exists) {
				throw new DirectoryNotFoundException(
					"Source directory does not exist or could not be found: "
					+ sourceDirName);
			}

			DirectoryInfo[] dirs = dir.GetDirectories();

			// If the destination directory doesn't exist, create it.       
			Directory.CreateDirectory(destDirName);

			// Get the files in the directory and copy them to the new location.
			FileInfo[] files = dir.GetFiles();
			foreach (FileInfo file in files) {
				string tempPath = Path.Combine(destDirName, file.Name);
				file.CopyTo(tempPath, true);
			}

			// If copying subdirectories, copy them and their contents to new location.
			if (copySubDirs) {
				foreach (DirectoryInfo subdir in dirs) {
					string tempPath = Path.Combine(destDirName, subdir.Name);
					CopyDirectory(subdir.FullName, tempPath, copySubDirs);
				}
			}
		}
	}
}
