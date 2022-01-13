using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cassia;

namespace rPCSMT.src.Main.Forms {
	public partial class TaskManagerForm : Form {

		public class TableProces {
			public int PID { get; set; }
			public string processName { get; set; }
			public string username { get; set; }

			public TableProces(int PID, string processName, string username) {
				this.PID = PID;
				this.processName = processName;
				this.username = username;
			}
		}


		//List<ITerminalServicesProcess> process;
		List<TableProces> tableProcess = new List<TableProces>();
		List<ITerminalServicesProcess> process;
		private string computerName;
		private bool showAllProcess = false;

		public TaskManagerForm(string computerName) {
			this.computerName = computerName;
			InitializeComponent();
			this.Text = this.computerName;
		}


		private void TaskManagerForm_Load(object sender, EventArgs e) {
			refreshProcessTable();
		}


		private void refreshProcessTable() {
			this.tableProcess = new List<TableProces>();
			this.process = AdminTools.getComputerProcess(this.computerName);
			ITerminalServer server = process[0].Server;
			foreach (ITerminalServicesProcess proc in process) {
				ITerminalServicesSession session = server.GetSession(proc.SessionId);
				if (showAllProcess | session.UserAccount != null) {
					TableProces tmpProc = new TableProces(
						proc.ProcessId,
						proc.ProcessName,
						session.UserName
					);
					tableProcess.Add(tmpProc);
				}
			}
			tableProcess.Sort((x, y) => x.processName.CompareTo(y.processName));
			//process_dataGridView.Refresh();
			process_dataGridView.DataSource = this.tableProcess;
		}


		private void killProcces_button_Click(object sender, EventArgs e) {
			try {
				AdminTools.killProcess(
					this.computerName,
					(process_dataGridView.SelectedRows[0].DataBoundItem as TableProces).PID
				);
				int index = process_dataGridView.SelectedRows[0].Index;
				this.tableProcess.RemoveAt(process_dataGridView.SelectedRows[0].Index);
				this.refreshProcessTable();
				this.process_dataGridView.CurrentCell = process_dataGridView.Rows[index].Cells[0];
			} catch (Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}


		private void refresh_button_Click(object sender, EventArgs e) {
			refreshProcessTable();
		}


		private void allProcess_checkBox_CheckedChanged(object sender, EventArgs e) {
			showAllProcess = allProcess_checkBox.Checked;
			int bufPID = (int)process_dataGridView.CurrentRow.Cells[0].Value;
			var bufRow = process_dataGridView.SelectedRows[0].Index;
			refreshProcessTable();
			//List<int> pids = tttt.Select(o => (int)o.Cells[0].Value).ToList();
			DataGridViewRow selectedRow = process_dataGridView.Rows
				.Cast<DataGridViewRow>().ToList()
				.Find(el => (int)el.Cells[0].Value == bufPID);
			if (selectedRow != null)
				process_dataGridView.CurrentCell = process_dataGridView.Rows[selectedRow.Index].Cells[0];
			else {
				//int i = process_dataGridView.SelectedCells[0].Index;
				//while (process_dataGridView.Rows[i].Cells[2].Value == "")
				//	i--;
				//process_dataGridView.CurrentCell = process_dataGridView.Rows[i].Cells[0];
			}
		}
	}
}
