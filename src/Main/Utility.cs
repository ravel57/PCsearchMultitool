using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using pc_finnder.src.Main.Forms;

namespace pc_finnder.src.Main {
	static class Utility {

		public static bool RUN_FROM_APPDATA = true;
		public const string VERSION  = "1.6";
		private const string SETTINGS_FILENAME = ".\\set.json";
		private const string CONFIGURATIONS_FILENAME = ".\\config.json";
		public static string APPDATA_PATH {
			get => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\rPCSMT";
		}

		public class Configuration {
			public string loginsPath;
			public string inventoryPath;
			public string almonahUrl;
			public string distroPath;
			public void saveConfiguration() {
				File.WriteAllText(CONFIGURATIONS_FILENAME, JsonConvert.SerializeObject(configuration));
			}
		}

		public class Settings {
			public Point windowCord = new Point(0, 0);
			public void saveSettings(Point cord) {
				settings.windowCord = cord;
				File.WriteAllText(SETTINGS_FILENAME, JsonConvert.SerializeObject(settings));
			}
		}

		public static Configuration configuration = new Configuration();
		public static Settings settings = new Settings();

		public static async void RunLocal() {
			try {
				CopyDirectory(".", APPDATA_PATH, false, new string[] { SETTINGS_FILENAME });
				Directory.SetCurrentDirectory(APPDATA_PATH);
				Utility.execProcess("rPCSMT.exe");
				Environment.Exit(0);
			} catch (Exception e) {
				MessageBox.Show(e.Message);
			}
		}

		public static void run() {
			try {
				if (File.Exists(CONFIGURATIONS_FILENAME)) {
					string configJson = File.ReadAllText(CONFIGURATIONS_FILENAME);//.Replace('\\', '/');
					configuration = JsonConvert.DeserializeObject<Configuration>(configJson);
					if (!Directory.Exists(configuration.loginsPath))
						new ConfigForm();
				} else
					new ConfigForm();
			} catch (Exception e) {
				MessageBox.Show($"Файл '{CONFIGURATIONS_FILENAME}' не найден или поврежден\n\n{e.Message}\n\n{e.StackTrace}");
			}
			try {
				if (File.Exists(SETTINGS_FILENAME)) {
					string setJson = File.ReadAllText(SETTINGS_FILENAME);
					settings = JsonConvert.DeserializeObject<Settings>(setJson);
					while (settings.windowCord.X > Screen.AllScreens.Sum(s => s.Bounds.Width))
						settings.windowCord.X -= Screen.PrimaryScreen.Bounds.Width;
				} else {
					settings.windowCord = new Point((Screen.AllScreens.Length > 1 ? Screen.PrimaryScreen.Bounds.Width + 100 : 100), 50);
					//MessageBox.Show(JsonConvert.SerializeObject(windowCord));
					File.WriteAllText(SETTINGS_FILENAME, JsonConvert.SerializeObject(settings));
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


		static void CopyDirectory(string sourceDirName, string destDirName, bool copySubDirs, string[] ignoreFiles) {
			// Get the subdirectories for the specified directory.
			DirectoryInfo dir = new DirectoryInfo(sourceDirName);

			if (!dir.Exists) {
				throw new DirectoryNotFoundException(
					"Source directory does not exist or could not be found: "
					+ sourceDirName
					);
			}

			DirectoryInfo[] dirs = dir.GetDirectories();

			// If the destination directory doesn't exist, create it.       
			Directory.CreateDirectory(destDirName);

			// Get the files in the directory and copy them to the new location.
			FileInfo[] files = dir.GetFiles();
			foreach (FileInfo file in files) {
				if (ignoreFiles.Contains(file.Name)) continue;
				string tempPath = Path.Combine(destDirName, file.Name);
				file.CopyTo(tempPath, true);
			}

			// If copying subdirectories, copy them and their contents to new location.
			if (copySubDirs) {
				foreach (DirectoryInfo subdir in dirs) {
					string tempPath = Path.Combine(destDirName, subdir.Name);
					CopyDirectory(subdir.FullName, tempPath, copySubDirs, ignoreFiles);
				}
			}
		}
	}
}
