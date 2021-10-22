using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cassia;

namespace pc_finnder.src.Main.Forms {
	public partial class TaskManagerForm : Form {

		public class TableProces {
			public int PID { get; set; }
			public string processName { get; set; }
			public string username { get; set; }

			public TableProces(int PID, string processName) {
				this.PID = PID;
				this.processName = processName;
			}
		}

		//List<ITerminalServicesProcess> process;
		List<TableProces> tableProcess = new List<TableProces>();
		string computerName;

		public TaskManagerForm(string computerName) {
			this.computerName = computerName;
			InitializeComponent();
			this.Text = this.computerName;
		}

		private void TaskManagerForm_Load(object sender, EventArgs e) {
			refreshProcessTable();
		}

		private void refreshProcessTable() {
			List<ITerminalServicesProcess> process = AdminTools.getProcess(this.computerName);
			ITerminalServer server = process[0].Server;
			foreach (ITerminalServicesProcess proc in process) {
				ITerminalServicesSession session = server.GetSession(proc.SessionId);
				if (session.UserAccount != null) {
					TableProces tmpProc = new TableProces(
						proc.ProcessId,
						proc.ProcessName);
					tmpProc.username = session.UserName;
					tableProcess.Add(tmpProc);
				}
			}
			tableProcess.Sort((x, y) => x.processName.CompareTo(y.processName));
			process_dataGridView.DataSource = this.tableProcess;
		}

		private void killProcces_button_Click(object sender, EventArgs e) {
			try {
				AdminTools.killProcess(
					this.computerName,
					(process_dataGridView.SelectedRows[0].DataBoundItem as TableProces).PID
					);
				this.tableProcess.RemoveAt(process_dataGridView.SelectedRows[0].Index);
				process_dataGridView.Select();
			} catch (Exception ex) {
				MessageBox.Show(ex.Message +"\n\n"+ ex.StackTrace); 
			}
		}

		private void refresh_button_Click(object sender, EventArgs e) {
			refreshProcessTable();
			process_dataGridView.Select();
		}
	}
}
