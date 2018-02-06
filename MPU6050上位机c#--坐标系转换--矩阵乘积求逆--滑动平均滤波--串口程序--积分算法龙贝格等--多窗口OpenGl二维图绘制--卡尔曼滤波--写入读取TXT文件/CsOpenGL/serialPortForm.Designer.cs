namespace CsOpenGL
{
    partial class serialPortForm
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
            this.btnSwitchSerial = new System.Windows.Forms.Button();
            this.comboBaudRate = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboPortName = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSwitchSerial
            // 
            this.btnSwitchSerial.Location = new System.Drawing.Point(157, 57);
            this.btnSwitchSerial.Name = "btnSwitchSerial";
            this.btnSwitchSerial.Size = new System.Drawing.Size(75, 23);
            this.btnSwitchSerial.TabIndex = 14;
            this.btnSwitchSerial.Text = "打开串口";
            this.btnSwitchSerial.UseVisualStyleBackColor = true;
            this.btnSwitchSerial.Click += new System.EventHandler(this.btnSwitchSerial_Click);
            // 
            // comboBaudRate
            // 
            this.comboBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBaudRate.FormattingEnabled = true;
            this.comboBaudRate.Items.AddRange(new object[] {
            "115200",
            "9600"});
            this.comboBaudRate.Location = new System.Drawing.Point(65, 59);
            this.comboBaudRate.Name = "comboBaudRate";
            this.comboBaudRate.Size = new System.Drawing.Size(61, 20);
            this.comboBaudRate.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "波特率：";
            // 
            // comboPortName
            // 
            this.comboPortName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboPortName.FormattingEnabled = true;
            this.comboPortName.Location = new System.Drawing.Point(65, 23);
            this.comboPortName.Name = "comboPortName";
            this.comboPortName.Size = new System.Drawing.Size(61, 20);
            this.comboPortName.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "串口号：";
            // 
            // serialPortForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(239, 103);
            this.Controls.Add(this.btnSwitchSerial);
            this.Controls.Add(this.comboBaudRate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboPortName);
            this.Controls.Add(this.label3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "serialPortForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "用于设置串口与打开串口的窗体";
            this.Text = "串口设置";
            this.Load += new System.EventHandler(this.serialPortForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Button btnSwitchSerial;
        public System.Windows.Forms.ComboBox comboBaudRate;
        public System.Windows.Forms.ComboBox comboPortName;
    }
}