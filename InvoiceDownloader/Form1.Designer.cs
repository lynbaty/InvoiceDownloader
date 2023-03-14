namespace InvoiceDownloader
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtSecretKey = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGetToken = new System.Windows.Forms.Button();
            this.logText1 = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.label2 = new System.Windows.Forms.Label();
            this.startTime = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.endTime = new System.Windows.Forms.DateTimePicker();
            this.errorText1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtSecretKey
            // 
            this.txtSecretKey.Location = new System.Drawing.Point(33, 27);
            this.txtSecretKey.Name = "txtSecretKey";
            this.txtSecretKey.Size = new System.Drawing.Size(545, 23);
            this.txtSecretKey.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Secret Key";
            // 
            // btnGetToken
            // 
            this.btnGetToken.Location = new System.Drawing.Point(598, 27);
            this.btnGetToken.Name = "btnGetToken";
            this.btnGetToken.Size = new System.Drawing.Size(122, 23);
            this.btnGetToken.TabIndex = 3;
            this.btnGetToken.Text = "Check Connection";
            this.btnGetToken.UseVisualStyleBackColor = true;
            this.btnGetToken.Click += new System.EventHandler(this.check_Click);
            // 
            // logText1
            // 
            this.logText1.BackColor = System.Drawing.SystemColors.Control;
            this.logText1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.logText1.Location = new System.Drawing.Point(33, 56);
            this.logText1.Name = "logText1";
            this.logText1.Size = new System.Drawing.Size(545, 16);
            this.logText1.TabIndex = 4;
            // 
            // listView1
            // 
            this.listView1.CheckBoxes = true;
            this.listView1.Location = new System.Drawing.Point(33, 106);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(687, 212);
            this.listView1.TabIndex = 5;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.List;
            this.listView1.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listView1_ItemChecked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Branches Selected";
            // 
            // startTime
            // 
            this.startTime.CustomFormat = "MM/dd/yyyy";
            this.startTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.startTime.ImeMode = System.Windows.Forms.ImeMode.On;
            this.startTime.Location = new System.Drawing.Point(33, 351);
            this.startTime.Name = "startTime";
            this.startTime.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.startTime.Size = new System.Drawing.Size(139, 23);
            this.startTime.TabIndex = 7;
            this.startTime.Value = new System.DateTime(2022, 11, 15, 0, 0, 0, 0);
            this.startTime.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 333);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "Start Time";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(297, 333);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 15);
            this.label4.TabIndex = 10;
            this.label4.Text = "End Time";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnExport.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnExport.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnExport.Location = new System.Drawing.Point(559, 342);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(127, 32);
            this.btnExport.TabIndex = 11;
            this.btnExport.Text = "Export Invoices";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.exportClick);
            // 
            // endTime
            // 
            this.endTime.CustomFormat = "MM/dd/yyyy";
            this.endTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.endTime.ImeMode = System.Windows.Forms.ImeMode.On;
            this.endTime.Location = new System.Drawing.Point(297, 351);
            this.endTime.Name = "endTime";
            this.endTime.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.endTime.Size = new System.Drawing.Size(136, 23);
            this.endTime.TabIndex = 12;
            this.endTime.Value = new System.DateTime(2022, 11, 15, 0, 0, 0, 0);
            this.endTime.ValueChanged += new System.EventHandler(this.endTime_ValueChanged);
            // 
            // errorText1
            // 
            this.errorText1.BackColor = System.Drawing.SystemColors.Control;
            this.errorText1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.errorText1.Location = new System.Drawing.Point(33, 384);
            this.errorText1.Multiline = true;
            this.errorText1.Name = "errorText1";
            this.errorText1.Size = new System.Drawing.Size(504, 58);
            this.errorText1.TabIndex = 13;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.button1.Location = new System.Drawing.Point(559, 391);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(127, 31);
            this.button1.TabIndex = 14;
            this.button1.Text = "Export Returns";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(533, 57);
            this.button5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(80, 24);
            this.button5.TabIndex = 16;
            this.button5.Text = "Browse...";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(33, 58);
            this.txtFilePath.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(480, 23);
            this.txtFilePath.TabIndex = 15;
            this.txtFilePath.Text = "C:\\Users\\boank\\Downloads\\SOURCEID_04541 - Cole Johnson.xml";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 450);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.errorText1);
            this.Controls.Add(this.endTime);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.startTime);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.logText1);
            this.Controls.Add(this.btnGetToken);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSecretKey);
            this.Name = "Form1";
            this.Text = "KiotViet Invoice Downloader";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox txtSecretKey;
        private Label label1;
        private Button btnGetToken;
        private TextBox logText1;
        private ListView listView1;
        private Label label2;
        private DateTimePicker startTime;
        private Label label3;
        private Label label4;
        private Button btnExport;
        private DateTimePicker endTime;
        private TextBox errorText1;
        private Button button1;
        private Button button5;
        private TextBox txtFilePath;
    }
}