namespace ModeBus
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.RText = new System.Windows.Forms.TextBox();
            this.connect1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.serverPort = new System.Windows.Forms.TextBox();
            this.serverIP = new System.Windows.Forms.TextBox();
            this.btnReceive = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // RText
            // 
            this.RText.Location = new System.Drawing.Point(73, 258);
            this.RText.Multiline = true;
            this.RText.Name = "RText";
            this.RText.Size = new System.Drawing.Size(156, 105);
            this.RText.TabIndex = 1;
            // 
            // connect1
            // 
            this.connect1.Location = new System.Drawing.Point(235, 97);
            this.connect1.Name = "connect1";
            this.connect1.Size = new System.Drawing.Size(75, 23);
            this.connect1.TabIndex = 9;
            this.connect1.Text = "连接";
            this.connect1.UseVisualStyleBackColor = true;
            this.connect1.Click += new System.EventHandler(this.connect1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "端口号:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "服务器IP地址:";
            // 
            // serverPort
            // 
            this.serverPort.Location = new System.Drawing.Point(93, 142);
            this.serverPort.Name = "serverPort";
            this.serverPort.Size = new System.Drawing.Size(100, 21);
            this.serverPort.TabIndex = 6;
            this.serverPort.Text = "502";
            // 
            // serverIP
            // 
            this.serverIP.Location = new System.Drawing.Point(93, 99);
            this.serverIP.Name = "serverIP";
            this.serverIP.Size = new System.Drawing.Size(100, 21);
            this.serverIP.TabIndex = 5;
            this.serverIP.Text = "127.0.0.1";
            // 
            // btnReceive
            // 
            this.btnReceive.Location = new System.Drawing.Point(154, 229);
            this.btnReceive.Name = "btnReceive";
            this.btnReceive.Size = new System.Drawing.Size(75, 23);
            this.btnReceive.TabIndex = 10;
            this.btnReceive.Text = "修改(W)";
            this.btnReceive.UseVisualStyleBackColor = true;
            this.btnReceive.Click += new System.EventHandler(this.btnReceive_Click);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(73, 229);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 11;
            this.btnSend.Text = "读取(R)";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 417);
            this.Controls.Add(this.btnReceive);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.connect1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.serverPort);
            this.Controls.Add(this.serverIP);
            this.Controls.Add(this.RText);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox RText;
        private System.Windows.Forms.Button connect1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox serverPort;
        private System.Windows.Forms.TextBox serverIP;
        private System.Windows.Forms.Button btnReceive;
        private System.Windows.Forms.Button btnSend;
    }
}

