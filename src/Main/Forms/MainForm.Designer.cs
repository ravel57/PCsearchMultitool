﻿namespace rPCSMT {
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
			this.parsedEntitiesNames_listBox = new System.Windows.Forms.ListBox();
			this.searchType_label = new System.Windows.Forms.Label();
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
			this.info_button = new System.Windows.Forms.Button();
			this.ip_button = new System.Windows.Forms.Button();
			this.explorer_button = new System.Windows.Forms.Button();
			this.printerInfo_button = new System.Windows.Forms.Button();
			this.extraToolsMenu_button = new System.Windows.Forms.Button();
			this.extra_contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.version_textBox = new System.Windows.Forms.TextBox();
			this.ChangeSearchObjects = new System.Windows.Forms.Button();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.SuspendLayout();
			// 
			// parsedEntitiesNames_listBox
			// 
			this.parsedEntitiesNames_listBox.FormattingEnabled = true;
			this.parsedEntitiesNames_listBox.Location = new System.Drawing.Point(7, 31);
			this.parsedEntitiesNames_listBox.Name = "parsedEntitiesNames_listBox";
			this.parsedEntitiesNames_listBox.Size = new System.Drawing.Size(159, 277);
			this.parsedEntitiesNames_listBox.TabIndex = 1;
			this.parsedEntitiesNames_listBox.SelectedIndexChanged += new System.EventHandler(this.parsedEntitiesNames_listBox_SelectedIndexChanged);
			this.parsedEntitiesNames_listBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.parsedEntitiesNames_listBox_KeyDown);
			this.parsedEntitiesNames_listBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.parsedEntitiesNames_listBox_DoubleClick);
			// 
			// searchType_label
			// 
			this.searchType_label.AutoSize = true;
			this.searchType_label.Location = new System.Drawing.Point(4, 7);
			this.searchType_label.Name = "searchType_label";
			this.searchType_label.Size = new System.Drawing.Size(106, 13);
			this.searchType_label.TabIndex = 1;
			this.searchType_label.Text = "Имя пользователя:";
			// 
			// copy_button
			// 
			this.copy_button.Location = new System.Drawing.Point(172, 196);
			this.copy_button.Name = "copy_button";
			this.copy_button.Size = new System.Drawing.Size(187, 25);
			this.copy_button.TabIndex = 6;
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
			this.userName_comboBox.Location = new System.Drawing.Point(128, 4);
			this.userName_comboBox.Name = "userName_comboBox";
			this.userName_comboBox.Size = new System.Drawing.Size(206, 21);
			this.userName_comboBox.TabIndex = 0;
			this.userName_comboBox.SelectedIndexChanged += new System.EventHandler(this.UserName_comboBox_SelectedIndexChanged);
			this.userName_comboBox.TextUpdate += new System.EventHandler(this.userName_comboBox_TextChanged);
			this.userName_comboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UserName_comboBox_KeyDown);
			// 
			// PCinfo_textBox
			// 
			this.PCinfo_textBox.BackColor = System.Drawing.SystemColors.Window;
			this.PCinfo_textBox.Location = new System.Drawing.Point(237, 31);
			this.PCinfo_textBox.Multiline = true;
			this.PCinfo_textBox.Name = "PCinfo_textBox";
			this.PCinfo_textBox.ReadOnly = true;
			this.PCinfo_textBox.Size = new System.Drawing.Size(122, 66);
			this.PCinfo_textBox.TabIndex = 0;
			this.PCinfo_textBox.TabStop = false;
			// 
			// ping_button
			// 
			this.ping_button.Location = new System.Drawing.Point(172, 256);
			this.ping_button.Name = "ping_button";
			this.ping_button.Size = new System.Drawing.Size(136, 23);
			this.ping_button.TabIndex = 8;
			this.ping_button.Text = "                 PING";
			this.ping_button.UseVisualStyleBackColor = true;
			this.ping_button.Click += new System.EventHandler(this.ping_button_Click);
			// 
			// sub_textBox
			// 
			this.sub_textBox.BackColor = System.Drawing.SystemColors.Window;
			this.sub_textBox.Location = new System.Drawing.Point(172, 31);
			this.sub_textBox.Multiline = true;
			this.sub_textBox.Name = "sub_textBox";
			this.sub_textBox.ReadOnly = true;
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
			this.sortPCnamesMethod_button.TabIndex = 12;
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
			this.infinitePing_button.Location = new System.Drawing.Point(307, 256);
			this.infinitePing_button.Margin = new System.Windows.Forms.Padding(1);
			this.infinitePing_button.Name = "infinitePing_button";
			this.infinitePing_button.Size = new System.Drawing.Size(23, 23);
			this.infinitePing_button.TabIndex = 9;
			this.infinitePing_button.Text = "/t";
			this.infinitePing_button.UseVisualStyleBackColor = true;
			this.infinitePing_button.Click += new System.EventHandler(this.infinitePing_button_Click);
			// 
			// info_button
			// 
			this.info_button.Location = new System.Drawing.Point(172, 165);
			this.info_button.Name = "info_button";
			this.info_button.Size = new System.Drawing.Size(93, 25);
			this.info_button.TabIndex = 4;
			this.info_button.Text = "ИНФО";
			this.info_button.UseVisualStyleBackColor = true;
			this.info_button.Click += new System.EventHandler(this.info_button_Click);
			// 
			// ip_button
			// 
			this.ip_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ip_button.Location = new System.Drawing.Point(332, 256);
			this.ip_button.Margin = new System.Windows.Forms.Padding(1);
			this.ip_button.Name = "ip_button";
			this.ip_button.Size = new System.Drawing.Size(27, 23);
			this.ip_button.TabIndex = 10;
			this.ip_button.Text = "IP";
			this.ip_button.UseVisualStyleBackColor = true;
			this.ip_button.Click += new System.EventHandler(this.ip_button_Click);
			// 
			// explorer_button
			// 
			this.explorer_button.Enabled = false;
			this.explorer_button.Location = new System.Drawing.Point(172, 227);
			this.explorer_button.Name = "explorer_button";
			this.explorer_button.Size = new System.Drawing.Size(187, 25);
			this.explorer_button.TabIndex = 7;
			this.explorer_button.Text = "ПРОВОДНИК";
			this.explorer_button.UseVisualStyleBackColor = true;
			this.explorer_button.Click += new System.EventHandler(this.explorer_button_Click);
			// 
			// printerInfo_button
			// 
			this.printerInfo_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.printerInfo_button.Location = new System.Drawing.Point(266, 165);
			this.printerInfo_button.Margin = new System.Windows.Forms.Padding(1);
			this.printerInfo_button.Name = "printerInfo_button";
			this.printerInfo_button.Size = new System.Drawing.Size(93, 25);
			this.printerInfo_button.TabIndex = 5;
			this.printerInfo_button.Text = "ПРИНТЕРЫ";
			this.printerInfo_button.UseVisualStyleBackColor = true;
			this.printerInfo_button.Click += new System.EventHandler(this.printerInfo_button_Click);
			// 
			// extraToolsMenu_button
			// 
			this.extraToolsMenu_button.AllowDrop = true;
			this.extraToolsMenu_button.CausesValidation = false;
			this.extraToolsMenu_button.Location = new System.Drawing.Point(172, 284);
			this.extraToolsMenu_button.Name = "extraToolsMenu_button";
			this.extraToolsMenu_button.Size = new System.Drawing.Size(187, 23);
			this.extraToolsMenu_button.TabIndex = 11;
			this.extraToolsMenu_button.Text = "ДОПОЛНИТЕЛЬНО";
			this.extraToolsMenu_button.UseVisualStyleBackColor = true;
			this.extraToolsMenu_button.Click += new System.EventHandler(this.extraToolsMenu_button_Click);
			// 
			// extra_contextMenuStrip
			// 
			this.extra_contextMenuStrip.Name = "extra_contextMenuStrip";
			this.extra_contextMenuStrip.Size = new System.Drawing.Size(61, 4);
			// 
			// version_textBox
			// 
			this.version_textBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.version_textBox.Enabled = false;
			this.version_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.version_textBox.Location = new System.Drawing.Point(299, 307);
			this.version_textBox.Name = "version_textBox";
			this.version_textBox.ReadOnly = true;
			this.version_textBox.Size = new System.Drawing.Size(57, 10);
			this.version_textBox.TabIndex = 100;
			this.version_textBox.TabStop = false;
			this.version_textBox.Text = "versin";
			this.version_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// ChangeSearchObjects
			// 
			this.ChangeSearchObjects.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ChangeSearchObjects.Location = new System.Drawing.Point(7, 3);
			this.ChangeSearchObjects.Margin = new System.Windows.Forms.Padding(0);
			this.ChangeSearchObjects.Name = "ChangeSearchObjects";
			this.ChangeSearchObjects.Size = new System.Drawing.Size(118, 23);
			this.ChangeSearchObjects.TabIndex = 13;
			this.ChangeSearchObjects.Text = "Имя пользователя:";
			this.ChangeSearchObjects.UseVisualStyleBackColor = true;
			this.ChangeSearchObjects.Click += new System.EventHandler(this.ChangeSearchObjects_button_Click);
			// 
			// MainForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(366, 317);
			this.Controls.Add(this.version_textBox);
			this.Controls.Add(this.printerInfo_button);
			this.Controls.Add(this.ip_button);
			this.Controls.Add(this.infinitePing_button);
			this.Controls.Add(this.sortPCnamesMethod_button);
			this.Controls.Add(this.ping_button);
			this.Controls.Add(this.sub_textBox);
			this.Controls.Add(this.PCinfo_textBox);
			this.Controls.Add(this.userName_comboBox);
			this.Controls.Add(this.info_button);
			this.Controls.Add(this.openRDP_button);
			this.Controls.Add(this.openAsist_button);
			this.Controls.Add(this.explorer_button);
			this.Controls.Add(this.copy_button);
			this.Controls.Add(this.parsedEntitiesNames_listBox);
			this.Controls.Add(this.ChangeSearchObjects);
			this.Controls.Add(this.searchType_label);
			this.Controls.Add(this.extraToolsMenu_button);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Text = "rPcsmt";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_Closing);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label searchType_label;
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
		public System.Windows.Forms.ListBox parsedEntitiesNames_listBox;
		private System.Windows.Forms.Button info_button;
		private System.Windows.Forms.Button ip_button;
		private System.Windows.Forms.Button explorer_button;
		private System.Windows.Forms.Button printerInfo_button;
		private System.Windows.Forms.Button extraToolsMenu_button;
		private System.Windows.Forms.ContextMenuStrip extra_contextMenuStrip;
		private System.Windows.Forms.TextBox version_textBox;
		private System.Windows.Forms.Button ChangeSearchObjects;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
	}
}

