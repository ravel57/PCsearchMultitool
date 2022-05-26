namespace rPCSMT {
	partial class InfinitePing_Form {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.PingRes_label = new System.Windows.Forms.Label();
			this.history_label = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
			// 
			// PingRes_label
			// 
			this.PingRes_label.AutoSize = true;
			this.PingRes_label.Location = new System.Drawing.Point(10, 4);
			this.PingRes_label.Name = "PingRes_label";
			this.PingRes_label.Size = new System.Drawing.Size(59, 26);
			this.PingRes_label.TabIndex = 1;
			this.PingRes_label.Text = "Усешно: 0\r\nОшибок: 0";
			// 
			// history_label
			// 
			this.history_label.AutoSize = true;
			this.history_label.Location = new System.Drawing.Point(6, 30);
			this.history_label.Name = "history_label";
			this.history_label.Size = new System.Drawing.Size(0, 13);
			this.history_label.TabIndex = 1;
			// 
			// InfinitePing_Form
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.ClientSize = new System.Drawing.Size(176, 47);
			this.Controls.Add(this.history_label);
			this.Controls.Add(this.PingRes_label);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "InfinitePing_Form";
			this.ShowIcon = false;
			this.Load += new System.EventHandler(this.InfinitePing_Form_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.Label PingRes_label;
		private System.Windows.Forms.Label history_label;
	}
}