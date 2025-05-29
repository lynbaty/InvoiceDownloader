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
            txtSecretKey = new TextBox();
            label1 = new Label();
            btnGetToken = new Button();
            logText1 = new TextBox();
            listView1 = new ListView();
            label2 = new Label();
            startTime = new DateTimePicker();
            label3 = new Label();
            label4 = new Label();
            btnExport = new Button();
            endTime = new DateTimePicker();
            errorText1 = new TextBox();
            button1 = new Button();
            button5 = new Button();
            txtFilePath = new TextBox();
            SuspendLayout();
            // 
            // txtSecretKey
            // 
            txtSecretKey.Location = new Point(47, 45);
            txtSecretKey.Margin = new Padding(4, 5, 4, 5);
            txtSecretKey.Name = "txtSecretKey";
            txtSecretKey.Size = new Size(777, 31);
            txtSecretKey.TabIndex = 0;
            txtSecretKey.Text = "48003E0F6F7B9500EA6D445B70113421A2CAC8C4";
            txtSecretKey.TextChanged += txtSecretKey_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(47, 12);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(93, 25);
            label1.TabIndex = 1;
            label1.Text = "Secret Key";
            // 
            // btnGetToken
            // 
            btnGetToken.Location = new Point(854, 45);
            btnGetToken.Margin = new Padding(4, 5, 4, 5);
            btnGetToken.Name = "btnGetToken";
            btnGetToken.Size = new Size(174, 38);
            btnGetToken.TabIndex = 3;
            btnGetToken.Text = "Check Connection";
            btnGetToken.UseVisualStyleBackColor = true;
            btnGetToken.Click += check_Click;
            // 
            // logText1
            // 
            logText1.BackColor = SystemColors.Control;
            logText1.BorderStyle = BorderStyle.None;
            logText1.Location = new Point(47, 93);
            logText1.Margin = new Padding(4, 5, 4, 5);
            logText1.Name = "logText1";
            logText1.Size = new Size(779, 24);
            logText1.TabIndex = 4;
            // 
            // listView1
            // 
            listView1.CheckBoxes = true;
            listView1.Location = new Point(47, 170);
            listView1.Margin = new Padding(4, 5, 4, 5);
            listView1.Name = "listView1";
            listView1.Size = new Size(980, 358);
            listView1.TabIndex = 5;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.List;
            listView1.ItemChecked += listView1_ItemChecked;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(47, 140);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(153, 25);
            label2.TabIndex = 6;
            label2.Text = "Branches Selected";
            // 
            // startTime
            // 
            startTime.CustomFormat = "MM/dd/yyyy";
            startTime.Format = DateTimePickerFormat.Custom;
            startTime.ImeMode = ImeMode.On;
            startTime.Location = new Point(47, 585);
            startTime.Margin = new Padding(4, 5, 4, 5);
            startTime.Name = "startTime";
            startTime.RightToLeft = RightToLeft.Yes;
            startTime.Size = new Size(197, 31);
            startTime.TabIndex = 7;
            startTime.Value = new DateTime(2022, 11, 15, 0, 0, 0, 0);
            startTime.ValueChanged += dateTimePicker1_ValueChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(47, 555);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(91, 25);
            label3.TabIndex = 8;
            label3.Text = "Start Time";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(424, 555);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(85, 25);
            label4.TabIndex = 10;
            label4.Text = "End Time";
            label4.Click += label4_Click;
            // 
            // btnExport
            // 
            btnExport.BackColor = SystemColors.ActiveCaption;
            btnExport.Font = new Font("Tahoma", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnExport.ForeColor = SystemColors.ButtonFace;
            btnExport.Location = new Point(799, 570);
            btnExport.Margin = new Padding(4, 5, 4, 5);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(181, 53);
            btnExport.TabIndex = 11;
            btnExport.Text = "Export Invoices";
            btnExport.UseVisualStyleBackColor = false;
            btnExport.Click += exportClick;
            // 
            // endTime
            // 
            endTime.CustomFormat = "MM/dd/yyyy";
            endTime.Format = DateTimePickerFormat.Custom;
            endTime.ImeMode = ImeMode.On;
            endTime.Location = new Point(424, 585);
            endTime.Margin = new Padding(4, 5, 4, 5);
            endTime.Name = "endTime";
            endTime.RightToLeft = RightToLeft.Yes;
            endTime.Size = new Size(193, 31);
            endTime.TabIndex = 12;
            endTime.Value = new DateTime(2022, 11, 15, 0, 0, 0, 0);
            endTime.ValueChanged += endTime_ValueChanged;
            // 
            // errorText1
            // 
            errorText1.BackColor = SystemColors.Control;
            errorText1.BorderStyle = BorderStyle.None;
            errorText1.Location = new Point(47, 640);
            errorText1.Margin = new Padding(4, 5, 4, 5);
            errorText1.Multiline = true;
            errorText1.Name = "errorText1";
            errorText1.Size = new Size(720, 97);
            errorText1.TabIndex = 13;
            // 
            // button1
            // 
            button1.BackColor = SystemColors.ActiveCaption;
            button1.Font = new Font("Tahoma", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            button1.ForeColor = SystemColors.ButtonFace;
            button1.Location = new Point(799, 652);
            button1.Margin = new Padding(4, 5, 4, 5);
            button1.Name = "button1";
            button1.Size = new Size(181, 52);
            button1.TabIndex = 14;
            button1.Text = "Export Returns";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button5
            // 
            button5.Location = new Point(761, 95);
            button5.Margin = new Padding(6, 5, 6, 5);
            button5.Name = "button5";
            button5.Size = new Size(114, 40);
            button5.TabIndex = 16;
            button5.Text = "Browse...";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // txtFilePath
            // 
            txtFilePath.Location = new Point(47, 97);
            txtFilePath.Margin = new Padding(6, 5, 6, 5);
            txtFilePath.Name = "txtFilePath";
            txtFilePath.Size = new Size(684, 31);
            txtFilePath.TabIndex = 15;
            txtFilePath.Text = "C:\\Users\\boank\\Downloads\\SOURCEID_04541 - Cole Johnson.xml";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1054, 750);
            Controls.Add(button5);
            Controls.Add(txtFilePath);
            Controls.Add(button1);
            Controls.Add(errorText1);
            Controls.Add(endTime);
            Controls.Add(btnExport);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(startTime);
            Controls.Add(label2);
            Controls.Add(listView1);
            Controls.Add(logText1);
            Controls.Add(btnGetToken);
            Controls.Add(label1);
            Controls.Add(txtSecretKey);
            Margin = new Padding(4, 5, 4, 5);
            Name = "Form1";
            Text = "KiotViet Invoice Downloader v5.1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
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