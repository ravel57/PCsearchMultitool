using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using rPCSMT.src.Main.Forms;

namespace rPCSMT.src.Main {
	static class Utility {

		public static bool RUN_FROM_APPDATA = true;
		public const string VERSION = "1.7.1";
		private const string SETTINGS_FILENAME = "set.json";
		private const string CONFIGURATIONS_FILENAME = "config.json";
		public static string APPDATA_PATH {
			get => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\rPCSMT";
		}
		public static string inputString = "";

		public class Configuration {

			private string _loginsPath;
			private bool _isSaving = false;
			public string loginsPath {
				set => _loginsPath = value;
				get {
					if (!_isSaving)
						switch (MainForm.selectedSearchType) {
							case MainForm.SearchFor.users:
								return _loginsPath + "\\users";
							case MainForm.SearchFor.computers:
								return _loginsPath + "\\computers";
						}
					return _loginsPath;
				}
			}
			public string inventoryPath;
			public string distroPath;
			public List<ConfigForm.Extra> extraFolders = new List<ConfigForm.Extra>();
			public List<ConfigForm.Extra> extraURLs = new List<ConfigForm.Extra>();
			public void saveConfiguration() {
				_isSaving = true;
				File.WriteAllText(CONFIGURATIONS_FILENAME, JsonConvert.SerializeObject(configuration));
				_isSaving = false;
			}
			public string getOriginalLogPath() {
				return _loginsPath;
			}
		}

		public class Settings {
			public Point windowCord = new Point(0, 0);
			public void saveSettings(Point cord) {
				settings.windowCord = cord;
				if (RUN_FROM_APPDATA)
					File.WriteAllText(SETTINGS_FILENAME, JsonConvert.SerializeObject(settings));
			}
		}

		public static Configuration configuration = new Configuration();
		public static Settings settings = new Settings();

		public static /*async*/ void startFromAppdata() {
			try {
				CopyDirectory(".", APPDATA_PATH, false, new string[] { SETTINGS_FILENAME });
				Directory.SetCurrentDirectory(APPDATA_PATH);
				Utility.execProcess("rPCSMT.exe " + Utility.inputString);
				Environment.Exit(0);
			} catch (Exception e) {
				MessageBox.Show(e.Message);
			}
		}

		public static void initializeSettings() {
			try {
				if (File.Exists(CONFIGURATIONS_FILENAME)) {
					string configJson = File.ReadAllText(CONFIGURATIONS_FILENAME);
					configuration = JsonConvert.DeserializeObject<Configuration>(configJson);
					if (!Directory.Exists(configuration.loginsPath)) {
						MessageBox.Show($"В файле '{CONFIGURATIONS_FILENAME}' указан некорректный путь к папке с логами");
						new ConfigForm();
					}
				} else
					new ConfigForm();
			} catch (Exception e) {
				MessageBox.Show($"Файл '{CONFIGURATIONS_FILENAME}' поврежден\n\n{e.Message}\n\n{e.StackTrace}");
			}
			try {
				if (File.Exists(SETTINGS_FILENAME)) {
					string setJson = File.ReadAllText(SETTINGS_FILENAME);
					settings = JsonConvert.DeserializeObject<Settings>(setJson);
					while (settings.windowCord.X > Screen.AllScreens.Sum(s => s.Bounds.Width))
						settings.windowCord.X -= Screen.PrimaryScreen.Bounds.Width;
				} else {
					settings.windowCord = new Point(100, 50);
				}
			} catch (Exception e) {
				MessageBox.Show($"Файл '{SETTINGS_FILENAME}' поврежден\n\n{e.Message}\n\n{e.StackTrace}");
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
