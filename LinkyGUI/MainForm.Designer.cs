namespace LinkyGUI
{
    partial class MainForm
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
            this.tbOutput = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnDailyData = new System.Windows.Forms.Button();
            this.btnHourlyData = new System.Windows.Forms.Button();
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpDailyEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpDailyStart = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpHourly = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.btnClearTextBox = new System.Windows.Forms.Button();
            this.btnRawOutput = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbOutput
            // 
            this.tbOutput.Location = new System.Drawing.Point(270, 12);
            this.tbOutput.Multiline = true;
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbOutput.Size = new System.Drawing.Size(841, 405);
            this.tbOutput.TabIndex = 1;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(108, 85);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(156, 23);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnDailyData
            // 
            this.btnDailyData.Location = new System.Drawing.Point(108, 198);
            this.btnDailyData.Name = "btnDailyData";
            this.btnDailyData.Size = new System.Drawing.Size(156, 23);
            this.btnDailyData.TabIndex = 4;
            this.btnDailyData.Text = "Get Daily Data";
            this.btnDailyData.UseVisualStyleBackColor = true;
            this.btnDailyData.Click += new System.EventHandler(this.btnDailyData_Click);
            // 
            // btnHourlyData
            // 
            this.btnHourlyData.Location = new System.Drawing.Point(108, 281);
            this.btnHourlyData.Name = "btnHourlyData";
            this.btnHourlyData.Size = new System.Drawing.Size(156, 23);
            this.btnHourlyData.TabIndex = 5;
            this.btnHourlyData.Text = "Get Half-hourly Data";
            this.btnHourlyData.UseVisualStyleBackColor = true;
            this.btnHourlyData.Click += new System.EventHandler(this.btnHourlyData_Click);
            // 
            // tbUsername
            // 
            this.tbUsername.Location = new System.Drawing.Point(164, 33);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(100, 20);
            this.tbUsername.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(73, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Username/Email";
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(164, 59);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(100, 20);
            this.tbPassword.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(105, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Password";
            // 
            // dtpDailyEnd
            // 
            this.dtpDailyEnd.Location = new System.Drawing.Point(64, 172);
            this.dtpDailyEnd.MinDate = new System.DateTime(2018, 1, 1, 0, 0, 0, 0);
            this.dtpDailyEnd.Name = "dtpDailyEnd";
            this.dtpDailyEnd.Size = new System.Drawing.Size(200, 20);
            this.dtpDailyEnd.TabIndex = 10;
            // 
            // dtpDailyStart
            // 
            this.dtpDailyStart.Location = new System.Drawing.Point(64, 146);
            this.dtpDailyStart.MinDate = new System.DateTime(2018, 1, 1, 0, 0, 0, 0);
            this.dtpDailyStart.Name = "dtpDailyStart";
            this.dtpDailyStart.Size = new System.Drawing.Size(200, 20);
            this.dtpDailyStart.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 152);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "From";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(38, 178);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "To";
            // 
            // dtpHourly
            // 
            this.dtpHourly.Location = new System.Drawing.Point(64, 255);
            this.dtpHourly.Name = "dtpHourly";
            this.dtpHourly.Size = new System.Drawing.Size(200, 20);
            this.dtpHourly.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 261);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Date";
            // 
            // btnClearTextBox
            // 
            this.btnClearTextBox.Location = new System.Drawing.Point(108, 344);
            this.btnClearTextBox.Name = "btnClearTextBox";
            this.btnClearTextBox.Size = new System.Drawing.Size(156, 23);
            this.btnClearTextBox.TabIndex = 16;
            this.btnClearTextBox.Text = "Clear Textbox";
            this.btnClearTextBox.UseVisualStyleBackColor = true;
            this.btnClearTextBox.Click += new System.EventHandler(this.btnClearTextBox_Click);
            // 
            // btnRawOutput
            // 
            this.btnRawOutput.Location = new System.Drawing.Point(108, 373);
            this.btnRawOutput.Name = "btnRawOutput";
            this.btnRawOutput.Size = new System.Drawing.Size(156, 23);
            this.btnRawOutput.TabIndex = 17;
            this.btnRawOutput.Text = "Raw Output of Prev. Query";
            this.btnRawOutput.UseVisualStyleBackColor = true;
            this.btnRawOutput.Click += new System.EventHandler(this.btnRawOutput_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1123, 432);
            this.Controls.Add(this.btnRawOutput);
            this.Controls.Add(this.btnClearTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dtpHourly);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpDailyStart);
            this.Controls.Add(this.dtpDailyEnd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbUsername);
            this.Controls.Add(this.btnHourlyData);
            this.Controls.Add(this.btnDailyData);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.tbOutput);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Text = "Linky/Enedis Demo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox tbOutput;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnDailyData;
        private System.Windows.Forms.Button btnHourlyData;
        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpDailyEnd;
        private System.Windows.Forms.DateTimePicker dtpDailyStart;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpHourly;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnClearTextBox;
        private System.Windows.Forms.Button btnRawOutput;
    }
}

