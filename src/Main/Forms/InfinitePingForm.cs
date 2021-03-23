using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace pc_finnder {
	public partial class InfinitePing_Form : Form {
		string PCname;
		List<string> results = new List<string>();

		//public delegate void PingReceivedEventHandler(int time);
		//public event PingReceivedEventHandler PingReceived;
		public InfinitePing_Form(string PCname) {
			InitializeComponent();
			this.PCname = PCname;
			this.Text = $"ping {PCname}";
		}


		async private void InfinitePing_Form_Load(object sender, EventArgs e) {
			//int counter = 0;
			int successPingCounter = 0;
			int errorPingCounter = 0;

			int leftCordX = 10;
			int rightCordX = 110;
			bool way = true;
			int textCord = leftCordX;
			//int winCord = this.Location.X;
			int endCord = rightCordX;

			Ping ping = new Ping();
			PingOptions pingOptions = new PingOptions(10, true);
			//await Task.Run(async () => {
			while (true) {
				// Пока не край
				while (textCord != endCord) {
					// Пингование 
					try {
						PingReply reply = ping.Send(PCname, 120, Encoding.ASCII.GetBytes("fff"), pingOptions);
						if (reply.Status == IPStatus.Success) successPingCounter++;
						else errorPingCounter++;
					} catch (System.Net.NetworkInformation.PingException) {
						MessageBox.Show("НЕ пингуется", @"¯\_(ツ)_/¯", MessageBoxButtons.OK, MessageBoxIcon.Error);
						Close();
					}
					//});
					// Двиганье
					PingRes_label.Text = $"Усешно: {successPingCounter}\nОшибок: {errorPingCounter}";
					for (int z = 0; z < 50; z++) {
						if (way) {
							PingRes_label.Location = new Point(textCord++, PingRes_label.Location.Y);
							this.Location = new Point(this.Location.X - 1, this.Location.Y);
						} else {
							PingRes_label.Location = new Point(textCord--, PingRes_label.Location.Y);
							this.Location = new Point(this.Location.X + 1, this.Location.Y);
						}
						await Task.Delay(2);
					}

				}
				if (way) endCord = leftCordX;
				else endCord = rightCordX;
				way = !way;
			}
		}
	}
}