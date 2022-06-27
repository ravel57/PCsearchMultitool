using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using rPCSMT.src.Main.Forms;

namespace rPCSMT.src.Main {
	static class Utility {

		public static bool RUN_FROM_APPDATA = true;
		public const string VERSION = "1.9.0";
		private const string SETTINGS_FILENAME = "set.json";
		private const string CONFIGURATIONS_FILENAME = "config.json";
		public static string APPDATA_PATH {
			get => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\rPCSMT";
		}
		private const string VERSION_FILENAME = "versio.n";
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
			public string rPrinterManagerPath;
			public string sourcePath = Directory.GetCurrentDirectory();
			public List<ConfigForm.Extra> extraFolders = new List<ConfigForm.Extra>();
			public List<ConfigForm.Extra> extraURLs = new List<ConfigForm.Extra>();

			public void saveConfiguration() {
				_isSaving = true;
				string json = JsonConvert.SerializeObject(configuration);
				File.WriteAllText(CONFIGURATIONS_FILENAME, json);
				File.WriteAllText($"{sourcePath}\\{CONFIGURATIONS_FILENAME}", json);
				_isSaving = false;
			}
			public string getOriginalLogPath() {
				return _loginsPath;
			}
			public void initialize() {
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
			}

		}

		public class Settings {
			public Point windowCord = new Point(0, 0);
			public List<ConfigForm.Extra> extraFolders = new List<ConfigForm.Extra>();
			public List<ConfigForm.Extra> extraURLs = new List<ConfigForm.Extra>();
			public bool closeAnotherCopyOfProgram = false;
			public bool movingInfinitePing = false;
			public void saveSettings() {
				if (Directory.GetCurrentDirectory() == Utility.APPDATA_PATH & Utility.RUN_FROM_APPDATA)
					File.WriteAllText(SETTINGS_FILENAME, JsonConvert.SerializeObject(settings));
			}
			public void saveSettings(Point cord) {
				settings.windowCord = cord;
				saveSettings();
			}
			public void initialize() {
				try {
					if (File.Exists(SETTINGS_FILENAME)) {
						string setJson = File.ReadAllText(SETTINGS_FILENAME);
						settings = JsonConvert.DeserializeObject<Settings>(setJson);
						if (settings.windowCord.X > 0) {
							while (settings.windowCord.X > Screen.AllScreens.Sum(s => s.Bounds.Width))
								settings.windowCord.X -= Screen.PrimaryScreen.Bounds.Width;
						} else {
							settings.windowCord.X = 100;
						}
						if (settings.windowCord.Y < 0 || settings.windowCord.Y > Screen.AllScreens.Max(s => s.Bounds.Height)) {
							settings.windowCord.Y = 50;
						}
					} else {
						settings.windowCord = new Point(100, 50);
					}
				} catch (Exception e) {
					MessageBox.Show($"Файл '{SETTINGS_FILENAME}' поврежден\n\n{e.Message}\n\n{e.StackTrace}");
					settings.windowCord = new Point(100, 50);
				}
				File.WriteAllText(VERSION_FILENAME, VERSION);
			}
		}

		public static Configuration configuration = new Configuration();
		public static Settings settings = new Settings();


		public static void initialize() {
			configuration.initialize();
			copyToAppdata();
			if (Directory.GetCurrentDirectory() != APPDATA_PATH & RUN_FROM_APPDATA) {
				startFromAppdata();
			} else {
				settings.initialize();
			}
		}

		private static void startFromAppdata() {
			try {
				if (Directory.GetCurrentDirectory() != configuration.sourcePath) {
					configuration.sourcePath = Directory.GetCurrentDirectory();
					configuration.saveConfiguration();
				}
				Directory.SetCurrentDirectory(APPDATA_PATH);
				Utility.execProcess("rPCSMT.exe " + inputString);
				Environment.Exit(0);
			} catch (Exception e) {
				MessageBox.Show(e.Message);
			}
		}

		private static void copyToAppdata() {
			if (File.Exists($"{APPDATA_PATH}\\{VERSION_FILENAME}") && File.ReadAllText($"{APPDATA_PATH}\\{VERSION_FILENAME}") == VERSION) {
				return;
			}
			string[] ignoreFiles = new string[] { SETTINGS_FILENAME, VERSION_FILENAME };
			CopyDirectory(configuration.sourcePath, APPDATA_PATH, false, ignoreFiles);

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
			try {
				AdminTools.killAnotherCopyOfProgram();
				Thread.Sleep(200);
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
			} catch (Exception e) {
				MessageBox.Show(e.Message);
			}
		}
	}
}
