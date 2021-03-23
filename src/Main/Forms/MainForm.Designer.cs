namespace pc_finnder {
	partial class MainForm {
		/// <summary>
		/// Обязательная переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.PCs_listBox = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.copy_button = new System.Windows.Forms.Button();
			this.openAsist_button = new System.Windows.Forms.Button();
			this.openRDP_button = new System.Windows.Forms.Button();
			this.userName_comboBox = new System.Windows.Forms.ComboBox();
			this.PCinfo_textBox = new System.Windows.Forms.TextBox();
			this.ping_button = new System.Windows.Forms.Button();
			this.sub_textBox = new System.Windows.Forms.TextBox();
			this.sortPCnamesMethod_button = new System.Windows.Forms.Button();
			this.sortBatton_toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.infinitePing_button = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.info_button = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// PCs_listBox
			// 
			this.PCs_listBox.FormattingEnabled = true;
			this.PCs_listBox.Location = new System.Drawing.Point(7, 31);
			this.PCs_listBox.Name = "PCs_listBox";
			this.PCs_listBox.Size = new System.Drawing.Size(159, 225);
			this.PCs_listBox.TabIndex = 1;
			this.PCs_listBox.SelectedIndexChanged += new System.EventHandler(this.PCs_listBox_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(4, 7);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(106, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Имя пользователя:";
			// 
			// copy_button
			// 
			this.copy_button.Location = new System.Drawing.Point(172, 196);
			this.copy_button.Name = "copy_button";
			this.copy_button.Size = new System.Drawing.Size(187, 25);
			this.copy_button.TabIndex = 4;
			this.copy_button.Text = "В БУФЕР ОБМАНА";
			this.copy_button.UseVisualStyleBackColor = true;
			this.copy_button.Click += new System.EventHandler(this.copy_button_Click);
			// 
			// openAsist_button
			// 
			this.openAsist_button.Location = new System.Drawing.Point(172, 103);
			this.openAsist_button.Name = "openAsist_button";
			this.openAsist_button.Size = new System.Drawing.Size(187, 25);
			this.openAsist_button.TabIndex = 2;
			this.openAsist_button.Text = "АСИСТЕНТ";
			this.openAsist_button.UseVisualStyleBackColor = true;
			this.openAsist_button.Click += new System.EventHandler(this.openAsist_button_Click);
			// 
			// openRDP_button
			// 
			this.openRDP_button.Location = new System.Drawing.Point(172, 134);
			this.openRDP_button.Name = "openRDP_button";
			this.openRDP_button.Size = new System.Drawing.Size(187, 25);
			this.openRDP_button.TabIndex = 3;
			this.openRDP_button.Text = "РДП";
			this.openRDP_button.UseVisualStyleBackColor = true;
			this.openRDP_button.Click += new System.EventHandler(this.openRDP_button_Click);
			// 
			// userName_comboBox
			// 
			this.userName_comboBox.AllowDrop = true;
			this.userName_comboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
			this.userName_comboBox.Cursor = System.Windows.Forms.Cursors.Default;
			this.userName_comboBox.DropDownHeight = 132;
			this.userName_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
			this.userName_comboBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.userName_comboBox.FormattingEnabled = true;
			this.userName_comboBox.IntegralHeight = false;
			this.userName_comboBox.Location = new System.Drawing.Point(111, 4);
			this.userName_comboBox.Name = "userName_comboBox";
			this.userName_comboBox.Size = new System.Drawing.Size(223, 21);
			this.userName_comboBox.TabIndex = 0;
			this.userName_comboBox.SelectedIndexChanged += new System.EventHandler(this.UserName_comboBox_SelectedIndexChanged);
			this.userName_comboBox.TextUpdate += new System.EventHandler(this.userName_comboBox_TextChanged);
			this.userName_comboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UserName_comboBox_KeyDown);
			// 
			// PCinfo_textBox
			// 
			this.PCinfo_textBox.Location = new System.Drawing.Point(237, 31);
			this.PCinfo_textBox.Multiline = true;
			this.PCinfo_textBox.Name = "PCinfo_textBox";
			this.PCinfo_textBox.Size = new System.Drawing.Size(122, 66);
			this.PCinfo_textBox.TabIndex = 0;
			this.PCinfo_textBox.TabStop = false;
			// 
			// ping_button
			// 
			this.ping_button.Location = new System.Drawing.Point(172, 227);
			this.ping_button.Name = "ping_button";
			this.ping_button.Size = new System.Drawing.Size(162, 23);
			this.ping_button.TabIndex = 5;
			this.ping_button.Text = "        ПИНГАНУТЬ";
			this.ping_button.UseVisualStyleBackColor = true;
			this.ping_button.Click += new System.EventHandler(this.ping_button_Click);
			// 
			// sub_textBox
			// 
			this.sub_textBox.Location = new System.Drawing.Point(172, 31);
			this.sub_textBox.Multiline = true;
			this.sub_textBox.Name = "sub_textBox";
			this.sub_textBox.Size = new System.Drawing.Size(66, 66);
			this.sub_textBox.TabIndex = 0;
			this.sub_textBox.TabStop = false;
			this.sub_textBox.Text = "           Имя:\r\n        Логов:\r\n     Первый:\r\nПоследний:";
			// 
			// sortPCnamesMethod_button
			// 
			this.sortPCnamesMethod_button.Location = new System.Drawing.Point(336, 3);
			this.sortPCnamesMethod_button.Name = "sortPCnamesMethod_button";
			this.sortPCnamesMethod_button.Size = new System.Drawing.Size(23, 23);
			this.sortPCnamesMethod_button.TabIndex = 7;
			this.sortPCnamesMethod_button.Text = "DATA";
			this.sortBatton_toolTip.SetToolTip(this.sortPCnamesMethod_button, "Выбрана сортировка по ДАТЕ");
			this.sortPCnamesMethod_button.UseVisualStyleBackColor = true;
			this.sortPCnamesMethod_button.Click += new System.EventHandler(this.sortPCnamesMethod_button_Click);
			// 
			// sortBatton_toolTip
			// 
			this.sortBatton_toolTip.ShowAlways = true;
			// 
			// infinitePing_button
			// 
			this.infinitePing_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.infinitePing_button.Location = new System.Drawing.Point(336, 227);
			this.infinitePing_button.Margin = new System.Windows.Forms.Padding(1);
			this.infinitePing_button.Name = "infinitePing_button";
			this.infinitePing_button.Size = new System.Drawing.Size(23, 23);
			this.infinitePing_button.TabIndex = 6;
			this.infinitePing_button.Text = "/t";
			this.infinitePing_button.UseVisualStyleBackColor = true;
			this.infinitePing_button.Click += new System.EventHandler(this.infinitePing_button_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.Location = new System.Drawing.Point(336, 251);
			this.label2.Margin = new System.Windows.Forms.Padding(0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(28, 7);
			this.label2.TabIndex = 8;
			this.label2.Text = "v. 1.3 b";
			// 
			// info_button
			// 
			this.info_button.Location = new System.Drawing.Point(172, 165);
			this.info_button.Name = "info_button";
			this.info_button.Size = new System.Drawing.Size(187, 25);
			this.info_button.TabIndex = 3;
			this.info_button.Text = "ИНФО";
			this.info_button.UseVisualStyleBackColor = true;
			this.info_button.Click += new System.EventHandler(this.info_button_Click);
			// 
			// MainForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(366, 263);
			this.Controls.Add(this.infinitePing_button);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.sortPCnamesMethod_button);
			this.Controls.Add(this.ping_button);
			this.Controls.Add(this.sub_textBox);
			this.Controls.Add(this.PCinfo_textBox);
			this.Controls.Add(this.userName_comboBox);
			this.Controls.Add(this.info_button);
			this.Controls.Add(this.openRDP_button);
			this.Controls.Add(this.openAsist_button);
			this.Controls.Add(this.copy_button);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.PCs_listBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Text = "rPcsmt";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_Closing);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button copy_button;
		private System.Windows.Forms.Button openAsist_button;
		private System.Windows.Forms.Button openRDP_button;
		private System.Windows.Forms.ComboBox userName_comboBox;
		private System.Windows.Forms.TextBox PCinfo_textBox;
		private System.Windows.Forms.Button ping_button;
		private System.Windows.Forms.TextBox sub_textBox;
		private System.Windows.Forms.Button sortPCnamesMethod_button;
		private System.Windows.Forms.ToolTip sortBatton_toolTip;
		private System.Windows.Forms.Button infinitePing_button;
		public System.Windows.Forms.ListBox PCs_listBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button info_button;
	}
}

