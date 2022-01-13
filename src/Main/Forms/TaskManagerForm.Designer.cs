
namespace rPCSMT.src.Main.Forms {
	partial class TaskManagerForm {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskManagerForm));
			this.process_dataGridView = new System.Windows.Forms.DataGridView();
			this.killProcces_button = new System.Windows.Forms.Button();
			this.refresh_button = new System.Windows.Forms.Button();
			this.allProcess_checkBox = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.process_dataGridView)).BeginInit();
			this.SuspendLayout();
			// 
			// process_dataGridView
			// 
			this.process_dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.process_dataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders;
			this.process_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.process_dataGridView.Location = new System.Drawing.Point(13, 13);
			this.process_dataGridView.MultiSelect = false;
			this.process_dataGridView.Name = "process_dataGridView";
			this.process_dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.process_dataGridView.Size = new System.Drawing.Size(481, 279);
			this.process_dataGridView.TabIndex = 0;
			// 
			// killProcces_button
			// 
			this.killProcces_button.Location = new System.Drawing.Point(419, 305);
			this.killProcces_button.Name = "killProcces_button";
			this.killProcces_button.Size = new System.Drawing.Size(75, 23);
			this.killProcces_button.TabIndex = 1;
			this.killProcces_button.Text = "Завершить";
			this.killProcces_button.UseVisualStyleBackColor = true;
			this.killProcces_button.Click += new System.EventHandler(this.killProcces_button_Click);
			// 
			// refresh_button
			// 
			this.refresh_button.Location = new System.Drawing.Point(338, 305);
			this.refresh_button.Name = "refresh_button";
			this.refresh_button.Size = new System.Drawing.Size(75, 23);
			this.refresh_button.TabIndex = 2;
			this.refresh_button.Text = "Обновить";
			this.refresh_button.UseVisualStyleBackColor = true;
			this.refresh_button.Click += new System.EventHandler(this.refresh_button_Click);
			// 
			// allProcess_checkBox
			// 
			this.allProcess_checkBox.AutoSize = true;
			this.allProcess_checkBox.Location = new System.Drawing.Point(13, 305);
			this.allProcess_checkBox.Name = "allProcess_checkBox";
			this.allProcess_checkBox.Size = new System.Drawing.Size(98, 17);
			this.allProcess_checkBox.TabIndex = 3;
			this.allProcess_checkBox.Text = "Все процессы";
			this.allProcess_checkBox.UseVisualStyleBackColor = true;
			this.allProcess_checkBox.CheckedChanged += new System.EventHandler(this.allProcess_checkBox_CheckedChanged);
			// 
			// TaskManagerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(506, 340);
			this.Controls.Add(this.allProcess_checkBox);
			this.Controls.Add(this.refresh_button);
			this.Controls.Add(this.killProcces_button);
			this.Controls.Add(this.process_dataGridView);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "TaskManagerForm";
			this.Text = "TaskManagerForm";
			this.Load += new System.EventHandler(this.TaskManagerForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.process_dataGridView)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView process_dataGridView;
		private System.Windows.Forms.Button killProcces_button;
		private System.Windows.Forms.Button refresh_button;
		private System.Windows.Forms.CheckBox allProcess_checkBox;
	}
}