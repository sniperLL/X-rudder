namespace CsOpenGL
{
    partial class GlForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.CsGlPanel = new System.Windows.Forms.Panel();
            this.GlTimer = new System.Windows.Forms.Timer(this.components);
            this.serialPort = new System.IO.Ports.SerialPort(this.components);
            this.serialBtn = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.accelerateVelocityPage = new System.Windows.Forms.TabPage();
            this.angularVelocityPage = new System.Windows.Forms.TabPage();
            this.anglePage = new System.Windows.Forms.TabPage();
            this.anglePanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.xAccLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.yAccLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.sxLabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.xAngularVLabel = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.yAngularVLabel = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.zAngularVLabel = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.rollAngleLabel = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.pitchAngleLabel = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.yawAngleLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.temperatureLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.accxEarthLabel = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.accyEarthLabel = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.acczEarthLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.accxRealLabel = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.accyRealLabel = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.acczRealLabel = new System.Windows.Forms.Label();
            this.dataLabel = new System.Windows.Forms.Label();
            this.updateZeroDriftBtn = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.daxEarthLabel = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.dayEarthLabel = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.dazEarthLabel = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.zAccLabel = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.syLabel = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.szLabel = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.vxLabel = new System.Windows.Forms.Label();
            this.vyLabel = new System.Windows.Forms.Label();
            this.vzLabel = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.depthLabel = new System.Windows.Forms.Label();
            this.remoteBtn = new System.Windows.Forms.Button();
            this.RemoteSerialPort = new System.IO.Ports.SerialPort(this.components);
            this.ShiftOffestTxtbox = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.writeBtn = new System.Windows.Forms.Button();
            this.stopBtn = new System.Windows.Forms.Button();
            this.label31 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.EmergencyBtn = new System.Windows.Forms.Button();
            this.motorspeedBtn = new System.Windows.Forms.Button();
            this.motorspeedTxt = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.YBtn = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label37 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.motorPtbx = new System.Windows.Forms.TextBox();
            this.motorItbx = new System.Windows.Forms.TextBox();
            this.motorDtbx = new System.Windows.Forms.TextBox();
            this.label40 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.motorPidBtn = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label43 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.accelerateVelocityPage.SuspendLayout();
            this.anglePage.SuspendLayout();
            this.SuspendLayout();
            // 
            // CsGlPanel
            // 
            this.CsGlPanel.Location = new System.Drawing.Point(3, 3);
            this.CsGlPanel.Name = "CsGlPanel";
            this.CsGlPanel.Size = new System.Drawing.Size(500, 456);
            this.CsGlPanel.TabIndex = 0;
            this.CsGlPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.CsGlPanel_Paint);
            // 
            // GlTimer
            // 
            this.GlTimer.Tick += new System.EventHandler(this.GlTimer_Tick);
            // 
            // serialPort
            // 
            this.serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort_DataReceived);
            // 
            // serialBtn
            // 
            this.serialBtn.BackColor = System.Drawing.SystemColors.Control;
            this.serialBtn.Location = new System.Drawing.Point(558, 172);
            this.serialBtn.Name = "serialBtn";
            this.serialBtn.Size = new System.Drawing.Size(75, 23);
            this.serialBtn.TabIndex = 1;
            this.serialBtn.Text = "串口设置";
            this.serialBtn.UseVisualStyleBackColor = false;
            this.serialBtn.Click += new System.EventHandler(this.serialBtn_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.accelerateVelocityPage);
            this.tabControl1.Controls.Add(this.angularVelocityPage);
            this.tabControl1.Controls.Add(this.anglePage);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(507, 432);
            this.tabControl1.TabIndex = 0;
            // 
            // accelerateVelocityPage
            // 
            this.accelerateVelocityPage.BackColor = System.Drawing.SystemColors.Control;
            this.accelerateVelocityPage.Controls.Add(this.CsGlPanel);
            this.accelerateVelocityPage.Location = new System.Drawing.Point(4, 25);
            this.accelerateVelocityPage.Name = "accelerateVelocityPage";
            this.accelerateVelocityPage.Padding = new System.Windows.Forms.Padding(3);
            this.accelerateVelocityPage.Size = new System.Drawing.Size(499, 403);
            this.accelerateVelocityPage.TabIndex = 0;
            this.accelerateVelocityPage.Text = "加速度";
            // 
            // angularVelocityPage
            // 
            this.angularVelocityPage.BackColor = System.Drawing.SystemColors.Control;
            this.angularVelocityPage.Location = new System.Drawing.Point(4, 25);
            this.angularVelocityPage.Name = "angularVelocityPage";
            this.angularVelocityPage.Padding = new System.Windows.Forms.Padding(3);
            this.angularVelocityPage.Size = new System.Drawing.Size(499, 403);
            this.angularVelocityPage.TabIndex = 1;
            this.angularVelocityPage.Text = "角速度";
            // 
            // anglePage
            // 
            this.anglePage.BackColor = System.Drawing.SystemColors.Control;
            this.anglePage.Controls.Add(this.anglePanel);
            this.anglePage.Location = new System.Drawing.Point(4, 25);
            this.anglePage.Name = "anglePage";
            this.anglePage.Size = new System.Drawing.Size(499, 403);
            this.anglePage.TabIndex = 2;
            this.anglePage.Text = "角度";
            // 
            // anglePanel
            // 
            this.anglePanel.Location = new System.Drawing.Point(3, 0);
            this.anglePanel.Name = "anglePanel";
            this.anglePanel.Size = new System.Drawing.Size(492, 402);
            this.anglePanel.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(704, 468);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "x轴加速度";
            this.label1.Visible = false;
            // 
            // xAccLabel
            // 
            this.xAccLabel.AutoSize = true;
            this.xAccLabel.Location = new System.Drawing.Point(774, 468);
            this.xAccLabel.Name = "xAccLabel";
            this.xAccLabel.Size = new System.Drawing.Size(29, 12);
            this.xAccLabel.TabIndex = 3;
            this.xAccLabel.Text = "0.00";
            this.xAccLabel.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(826, 459);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "y轴加速度";
            this.label3.Visible = false;
            // 
            // yAccLabel
            // 
            this.yAccLabel.AutoSize = true;
            this.yAccLabel.Location = new System.Drawing.Point(897, 459);
            this.yAccLabel.Name = "yAccLabel";
            this.yAccLabel.Size = new System.Drawing.Size(29, 12);
            this.yAccLabel.TabIndex = 3;
            this.yAccLabel.Text = "0.00";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(699, 438);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "x轴位移";
            // 
            // sxLabel
            // 
            this.sxLabel.AutoSize = true;
            this.sxLabel.Location = new System.Drawing.Point(755, 438);
            this.sxLabel.Name = "sxLabel";
            this.sxLabel.Size = new System.Drawing.Size(29, 12);
            this.sxLabel.TabIndex = 3;
            this.sxLabel.Text = "0.00";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(65, 498);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "x轴角速度";
            // 
            // xAngularVLabel
            // 
            this.xAngularVLabel.AutoSize = true;
            this.xAngularVLabel.Location = new System.Drawing.Point(135, 498);
            this.xAngularVLabel.Name = "xAngularVLabel";
            this.xAngularVLabel.Size = new System.Drawing.Size(29, 12);
            this.xAngularVLabel.TabIndex = 3;
            this.xAngularVLabel.Text = "0.00";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(192, 498);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 12);
            this.label9.TabIndex = 2;
            this.label9.Text = "y轴角速度";
            // 
            // yAngularVLabel
            // 
            this.yAngularVLabel.AutoSize = true;
            this.yAngularVLabel.Location = new System.Drawing.Point(263, 498);
            this.yAngularVLabel.Name = "yAngularVLabel";
            this.yAngularVLabel.Size = new System.Drawing.Size(29, 12);
            this.yAngularVLabel.TabIndex = 3;
            this.yAngularVLabel.Text = "0.00";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(316, 498);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 12);
            this.label11.TabIndex = 2;
            this.label11.Text = "z轴角速度";
            // 
            // zAngularVLabel
            // 
            this.zAngularVLabel.AutoSize = true;
            this.zAngularVLabel.Location = new System.Drawing.Point(385, 498);
            this.zAngularVLabel.Name = "zAngularVLabel";
            this.zAngularVLabel.Size = new System.Drawing.Size(29, 12);
            this.zAngularVLabel.TabIndex = 3;
            this.zAngularVLabel.Text = "0.00";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(65, 522);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(65, 12);
            this.label13.TabIndex = 2;
            this.label13.Text = "横滚角Roll";
            // 
            // rollAngleLabel
            // 
            this.rollAngleLabel.AutoSize = true;
            this.rollAngleLabel.Location = new System.Drawing.Point(135, 522);
            this.rollAngleLabel.Name = "rollAngleLabel";
            this.rollAngleLabel.Size = new System.Drawing.Size(29, 12);
            this.rollAngleLabel.TabIndex = 3;
            this.rollAngleLabel.Text = "0.00";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(188, 522);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(71, 12);
            this.label15.TabIndex = 2;
            this.label15.Text = "纵倾角Pitch";
            // 
            // pitchAngleLabel
            // 
            this.pitchAngleLabel.AutoSize = true;
            this.pitchAngleLabel.Location = new System.Drawing.Point(264, 522);
            this.pitchAngleLabel.Name = "pitchAngleLabel";
            this.pitchAngleLabel.Size = new System.Drawing.Size(29, 12);
            this.pitchAngleLabel.TabIndex = 3;
            this.pitchAngleLabel.Text = "0.00";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(316, 522);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(59, 12);
            this.label17.TabIndex = 2;
            this.label17.Text = "偏航角yaw";
            // 
            // yawAngleLabel
            // 
            this.yawAngleLabel.AutoSize = true;
            this.yawAngleLabel.Location = new System.Drawing.Point(385, 522);
            this.yawAngleLabel.Name = "yawAngleLabel";
            this.yawAngleLabel.Size = new System.Drawing.Size(29, 12);
            this.yawAngleLabel.TabIndex = 3;
            this.yawAngleLabel.Text = "0.00";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(65, 547);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "温度";
            // 
            // temperatureLabel
            // 
            this.temperatureLabel.AutoSize = true;
            this.temperatureLabel.Location = new System.Drawing.Point(103, 547);
            this.temperatureLabel.Name = "temperatureLabel";
            this.temperatureLabel.Size = new System.Drawing.Size(29, 12);
            this.temperatureLabel.TabIndex = 3;
            this.temperatureLabel.Text = "0.00";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(705, 540);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "地x加速度";
            // 
            // accxEarthLabel
            // 
            this.accxEarthLabel.AutoSize = true;
            this.accxEarthLabel.Location = new System.Drawing.Point(775, 540);
            this.accxEarthLabel.Name = "accxEarthLabel";
            this.accxEarthLabel.Size = new System.Drawing.Size(29, 12);
            this.accxEarthLabel.TabIndex = 3;
            this.accxEarthLabel.Text = "0.00";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(827, 531);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 12);
            this.label8.TabIndex = 2;
            this.label8.Text = "地y加速度";
            // 
            // accyEarthLabel
            // 
            this.accyEarthLabel.AutoSize = true;
            this.accyEarthLabel.Location = new System.Drawing.Point(898, 531);
            this.accyEarthLabel.Name = "accyEarthLabel";
            this.accyEarthLabel.Size = new System.Drawing.Size(29, 12);
            this.accyEarthLabel.TabIndex = 3;
            this.accyEarthLabel.Text = "0.00";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(951, 531);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(59, 12);
            this.label12.TabIndex = 2;
            this.label12.Text = "地z加速度";
            // 
            // acczEarthLabel
            // 
            this.acczEarthLabel.AutoSize = true;
            this.acczEarthLabel.Location = new System.Drawing.Point(1020, 531);
            this.acczEarthLabel.Name = "acczEarthLabel";
            this.acczEarthLabel.Size = new System.Drawing.Size(29, 12);
            this.acczEarthLabel.TabIndex = 3;
            this.acczEarthLabel.Text = "0.00";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(705, 561);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 2;
            this.label6.Text = "实x加速度";
            // 
            // accxRealLabel
            // 
            this.accxRealLabel.AutoSize = true;
            this.accxRealLabel.Location = new System.Drawing.Point(775, 561);
            this.accxRealLabel.Name = "accxRealLabel";
            this.accxRealLabel.Size = new System.Drawing.Size(29, 12);
            this.accxRealLabel.TabIndex = 3;
            this.accxRealLabel.Text = "0.00";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(827, 552);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(59, 12);
            this.label14.TabIndex = 2;
            this.label14.Text = "实y加速度";
            // 
            // accyRealLabel
            // 
            this.accyRealLabel.AutoSize = true;
            this.accyRealLabel.Location = new System.Drawing.Point(898, 552);
            this.accyRealLabel.Name = "accyRealLabel";
            this.accyRealLabel.Size = new System.Drawing.Size(29, 12);
            this.accyRealLabel.TabIndex = 3;
            this.accyRealLabel.Text = "0.00";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(951, 552);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(59, 12);
            this.label18.TabIndex = 2;
            this.label18.Text = "实z加速度";
            // 
            // acczRealLabel
            // 
            this.acczRealLabel.AutoSize = true;
            this.acczRealLabel.Location = new System.Drawing.Point(1020, 552);
            this.acczRealLabel.Name = "acczRealLabel";
            this.acczRealLabel.Size = new System.Drawing.Size(29, 12);
            this.acczRealLabel.TabIndex = 3;
            this.acczRealLabel.Text = "0.00";
            // 
            // dataLabel
            // 
            this.dataLabel.AutoSize = true;
            this.dataLabel.Location = new System.Drawing.Point(727, 438);
            this.dataLabel.Name = "dataLabel";
            this.dataLabel.Size = new System.Drawing.Size(0, 12);
            this.dataLabel.TabIndex = 3;
            // 
            // updateZeroDriftBtn
            // 
            this.updateZeroDriftBtn.BackColor = System.Drawing.SystemColors.Control;
            this.updateZeroDriftBtn.Location = new System.Drawing.Point(558, 198);
            this.updateZeroDriftBtn.Name = "updateZeroDriftBtn";
            this.updateZeroDriftBtn.Size = new System.Drawing.Size(75, 23);
            this.updateZeroDriftBtn.TabIndex = 1;
            this.updateZeroDriftBtn.Text = "更新零漂";
            this.updateZeroDriftBtn.UseVisualStyleBackColor = false;
            this.updateZeroDriftBtn.Click += new System.EventHandler(this.updateZeroDriftBtn_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(700, 570);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 2;
            this.label10.Text = "实dx加速度";
            // 
            // daxEarthLabel
            // 
            this.daxEarthLabel.AutoSize = true;
            this.daxEarthLabel.Location = new System.Drawing.Point(770, 570);
            this.daxEarthLabel.Name = "daxEarthLabel";
            this.daxEarthLabel.Size = new System.Drawing.Size(29, 12);
            this.daxEarthLabel.TabIndex = 3;
            this.daxEarthLabel.Text = "0.00";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(827, 570);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(65, 12);
            this.label19.TabIndex = 2;
            this.label19.Text = "实dy加速度";
            // 
            // dayEarthLabel
            // 
            this.dayEarthLabel.AutoSize = true;
            this.dayEarthLabel.Location = new System.Drawing.Point(898, 570);
            this.dayEarthLabel.Name = "dayEarthLabel";
            this.dayEarthLabel.Size = new System.Drawing.Size(29, 12);
            this.dayEarthLabel.TabIndex = 3;
            this.dayEarthLabel.Text = "0.00";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(951, 570);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(65, 12);
            this.label21.TabIndex = 2;
            this.label21.Text = "实dz加速度";
            // 
            // dazEarthLabel
            // 
            this.dazEarthLabel.AutoSize = true;
            this.dazEarthLabel.Location = new System.Drawing.Point(1020, 570);
            this.dazEarthLabel.Name = "dazEarthLabel";
            this.dazEarthLabel.Size = new System.Drawing.Size(29, 12);
            this.dazEarthLabel.TabIndex = 3;
            this.dazEarthLabel.Text = "0.00";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(951, 459);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(59, 12);
            this.label16.TabIndex = 2;
            this.label16.Text = "x轴加速度";
            // 
            // zAccLabel
            // 
            this.zAccLabel.AutoSize = true;
            this.zAccLabel.Location = new System.Drawing.Point(1019, 459);
            this.zAccLabel.Name = "zAccLabel";
            this.zAccLabel.Size = new System.Drawing.Size(29, 12);
            this.zAccLabel.TabIndex = 3;
            this.zAccLabel.Text = "0.00";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(979, 459);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(0, 12);
            this.label22.TabIndex = 3;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(699, 460);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(47, 12);
            this.label20.TabIndex = 2;
            this.label20.Text = "y轴位移";
            // 
            // syLabel
            // 
            this.syLabel.AutoSize = true;
            this.syLabel.Location = new System.Drawing.Point(755, 460);
            this.syLabel.Name = "syLabel";
            this.syLabel.Size = new System.Drawing.Size(29, 12);
            this.syLabel.TabIndex = 3;
            this.syLabel.Text = "0.00";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(727, 460);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(0, 12);
            this.label24.TabIndex = 3;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(699, 481);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(47, 12);
            this.label25.TabIndex = 2;
            this.label25.Text = "z轴位移";
            // 
            // szLabel
            // 
            this.szLabel.AutoSize = true;
            this.szLabel.Location = new System.Drawing.Point(755, 481);
            this.szLabel.Name = "szLabel";
            this.szLabel.Size = new System.Drawing.Size(29, 12);
            this.szLabel.TabIndex = 3;
            this.szLabel.Text = "0.00";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(727, 481);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(0, 12);
            this.label27.TabIndex = 3;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(699, 502);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(47, 12);
            this.label23.TabIndex = 2;
            this.label23.Text = "x轴速度";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(699, 524);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(47, 12);
            this.label26.TabIndex = 2;
            this.label26.Text = "y轴速度";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(699, 545);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(47, 12);
            this.label28.TabIndex = 2;
            this.label28.Text = "z轴速度";
            // 
            // vxLabel
            // 
            this.vxLabel.AutoSize = true;
            this.vxLabel.Location = new System.Drawing.Point(755, 502);
            this.vxLabel.Name = "vxLabel";
            this.vxLabel.Size = new System.Drawing.Size(29, 12);
            this.vxLabel.TabIndex = 3;
            this.vxLabel.Text = "0.00";
            // 
            // vyLabel
            // 
            this.vyLabel.AutoSize = true;
            this.vyLabel.Location = new System.Drawing.Point(755, 524);
            this.vyLabel.Name = "vyLabel";
            this.vyLabel.Size = new System.Drawing.Size(29, 12);
            this.vyLabel.TabIndex = 3;
            this.vyLabel.Text = "0.00";
            // 
            // vzLabel
            // 
            this.vzLabel.AutoSize = true;
            this.vzLabel.Location = new System.Drawing.Point(755, 545);
            this.vzLabel.Name = "vzLabel";
            this.vzLabel.Size = new System.Drawing.Size(29, 12);
            this.vzLabel.TabIndex = 3;
            this.vzLabel.Text = "0.00";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(727, 502);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(0, 12);
            this.label32.TabIndex = 3;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(727, 524);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(0, 12);
            this.label33.TabIndex = 3;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(727, 545);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(0, 12);
            this.label34.TabIndex = 3;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(188, 547);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(29, 12);
            this.label29.TabIndex = 2;
            this.label29.Text = "深度";
            // 
            // depthLabel
            // 
            this.depthLabel.AutoSize = true;
            this.depthLabel.Location = new System.Drawing.Point(226, 547);
            this.depthLabel.Name = "depthLabel";
            this.depthLabel.Size = new System.Drawing.Size(29, 12);
            this.depthLabel.TabIndex = 3;
            this.depthLabel.Text = "0.00";
            // 
            // remoteBtn
            // 
            this.remoteBtn.BackColor = System.Drawing.SystemColors.Control;
            this.remoteBtn.Location = new System.Drawing.Point(558, 225);
            this.remoteBtn.Name = "remoteBtn";
            this.remoteBtn.Size = new System.Drawing.Size(75, 23);
            this.remoteBtn.TabIndex = 1;
            this.remoteBtn.Text = "开启遥控器";
            this.remoteBtn.UseVisualStyleBackColor = false;
            this.remoteBtn.Click += new System.EventHandler(this.remoteBtn_Click);
            // 
            // RemoteSerialPort
            // 
            this.RemoteSerialPort.BaudRate = 115200;
            this.RemoteSerialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.RemoteSerialPort_DataReceived);
            // 
            // ShiftOffestTxtbox
            // 
            this.ShiftOffestTxtbox.Location = new System.Drawing.Point(595, 134);
            this.ShiftOffestTxtbox.Name = "ShiftOffestTxtbox";
            this.ShiftOffestTxtbox.ReadOnly = true;
            this.ShiftOffestTxtbox.Size = new System.Drawing.Size(55, 21);
            this.ShiftOffestTxtbox.TabIndex = 4;
            this.ShiftOffestTxtbox.Text = "0.0";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(523, 137);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(71, 12);
            this.label30.TabIndex = 2;
            this.label30.Text = "Y轴偏移遥控";
            // 
            // writeBtn
            // 
            this.writeBtn.Location = new System.Drawing.Point(575, 62);
            this.writeBtn.Name = "writeBtn";
            this.writeBtn.Size = new System.Drawing.Size(75, 23);
            this.writeBtn.TabIndex = 5;
            this.writeBtn.Text = "写入数据";
            this.writeBtn.UseVisualStyleBackColor = true;
            this.writeBtn.Click += new System.EventHandler(this.writeBtn_Click);
            // 
            // stopBtn
            // 
            this.stopBtn.Location = new System.Drawing.Point(575, 91);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(75, 23);
            this.stopBtn.TabIndex = 6;
            this.stopBtn.Text = "停止写入";
            this.stopBtn.UseVisualStyleBackColor = true;
            this.stopBtn.Click += new System.EventHandler(this.stopBtn_Click);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(656, 137);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(17, 12);
            this.label31.TabIndex = 2;
            this.label31.Text = "mm";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(513, 390);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(53, 12);
            this.label35.TabIndex = 7;
            this.label35.Text = "艉升降舵";
            this.label35.Click += new System.EventHandler(this.label35_Click);
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(520, 423);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(41, 12);
            this.label36.TabIndex = 7;
            this.label36.Text = "方向舵";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.Location = new System.Drawing.Point(575, 420);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(78, 21);
            this.textBox1.TabIndex = 8;
            this.textBox1.Text = "0";
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.Control;
            this.textBox2.Location = new System.Drawing.Point(575, 387);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(78, 21);
            this.textBox2.TabIndex = 8;
            this.textBox2.Text = "0";
            // 
            // EmergencyBtn
            // 
            this.EmergencyBtn.BackColor = System.Drawing.SystemColors.Control;
            this.EmergencyBtn.Location = new System.Drawing.Point(558, 252);
            this.EmergencyBtn.Name = "EmergencyBtn";
            this.EmergencyBtn.Size = new System.Drawing.Size(75, 23);
            this.EmergencyBtn.TabIndex = 1;
            this.EmergencyBtn.Text = "紧急上浮";
            this.EmergencyBtn.UseVisualStyleBackColor = false;
            this.EmergencyBtn.Click += new System.EventHandler(this.EmergencyBtn_Click);
            // 
            // motorspeedBtn
            // 
            this.motorspeedBtn.Location = new System.Drawing.Point(575, 540);
            this.motorspeedBtn.Name = "motorspeedBtn";
            this.motorspeedBtn.Size = new System.Drawing.Size(75, 23);
            this.motorspeedBtn.TabIndex = 9;
            this.motorspeedBtn.Text = "确认";
            this.motorspeedBtn.UseVisualStyleBackColor = true;
            this.motorspeedBtn.Click += new System.EventHandler(this.motorspeedBtn_Click);
            // 
            // motorspeedTxt
            // 
            this.motorspeedTxt.BackColor = System.Drawing.SystemColors.Control;
            this.motorspeedTxt.Location = new System.Drawing.Point(575, 513);
            this.motorspeedTxt.Name = "motorspeedTxt";
            this.motorspeedTxt.Size = new System.Drawing.Size(75, 21);
            this.motorspeedTxt.TabIndex = 10;
            this.motorspeedTxt.Text = "0.0";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(575, 477);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "确认";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.SystemColors.Control;
            this.textBox4.Location = new System.Drawing.Point(575, 450);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(78, 21);
            this.textBox4.TabIndex = 10;
            this.textBox4.Text = "0.0";
            // 
            // YBtn
            // 
            this.YBtn.Location = new System.Drawing.Point(611, 33);
            this.YBtn.Name = "YBtn";
            this.YBtn.Size = new System.Drawing.Size(39, 23);
            this.YBtn.TabIndex = 12;
            this.YBtn.Text = "确认";
            this.YBtn.UseVisualStyleBackColor = true;
            this.YBtn.Click += new System.EventHandler(this.YBtn_Click);
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.SystemColors.Control;
            this.textBox3.Location = new System.Drawing.Point(575, 33);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(30, 21);
            this.textBox3.TabIndex = 8;
            this.textBox3.Text = "0";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(526, 37);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(47, 12);
            this.label37.TabIndex = 2;
            this.label37.Text = "Y轴偏移";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(526, 453);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(29, 12);
            this.label38.TabIndex = 7;
            this.label38.Text = "艏舵";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(520, 516);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(53, 12);
            this.label39.TabIndex = 7;
            this.label39.Text = "电机转速";
            // 
            // motorPtbx
            // 
            this.motorPtbx.BackColor = System.Drawing.SystemColors.Control;
            this.motorPtbx.Location = new System.Drawing.Point(572, 281);
            this.motorPtbx.Name = "motorPtbx";
            this.motorPtbx.Size = new System.Drawing.Size(78, 21);
            this.motorPtbx.TabIndex = 8;
            this.motorPtbx.Text = "10";
            // 
            // motorItbx
            // 
            this.motorItbx.BackColor = System.Drawing.SystemColors.Control;
            this.motorItbx.Location = new System.Drawing.Point(572, 308);
            this.motorItbx.Name = "motorItbx";
            this.motorItbx.Size = new System.Drawing.Size(78, 21);
            this.motorItbx.TabIndex = 8;
            this.motorItbx.Text = "3";
            // 
            // motorDtbx
            // 
            this.motorDtbx.BackColor = System.Drawing.SystemColors.Control;
            this.motorDtbx.Location = new System.Drawing.Point(572, 335);
            this.motorDtbx.Name = "motorDtbx";
            this.motorDtbx.Size = new System.Drawing.Size(78, 21);
            this.motorDtbx.TabIndex = 8;
            this.motorDtbx.Text = "2";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(531, 284);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(35, 12);
            this.label40.TabIndex = 7;
            this.label40.Text = "电机P";
            this.label40.Click += new System.EventHandler(this.label35_Click);
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(531, 311);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(35, 12);
            this.label41.TabIndex = 7;
            this.label41.Text = "电机I";
            this.label41.Click += new System.EventHandler(this.label35_Click);
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(531, 338);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(35, 12);
            this.label42.TabIndex = 7;
            this.label42.Text = "电机D";
            this.label42.Click += new System.EventHandler(this.label35_Click);
            // 
            // motorPidBtn
            // 
            this.motorPidBtn.Location = new System.Drawing.Point(572, 358);
            this.motorPidBtn.Name = "motorPidBtn";
            this.motorPidBtn.Size = new System.Drawing.Size(78, 23);
            this.motorPidBtn.TabIndex = 11;
            this.motorPidBtn.Text = "确认";
            this.motorPidBtn.UseVisualStyleBackColor = true;
            this.motorPidBtn.Click += new System.EventHandler(this.motorPidBtn_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(422, 547);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(86, 23);
            this.button2.TabIndex = 13;
            this.button2.Text = "切换为PID";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(420, 498);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(53, 12);
            this.label43.TabIndex = 7;
            this.label43.Text = "最大舵角";
            this.label43.Click += new System.EventHandler(this.label35_Click);
            // 
            // textBox5
            // 
            this.textBox5.BackColor = System.Drawing.SystemColors.Control;
            this.textBox5.Location = new System.Drawing.Point(477, 495);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(47, 21);
            this.textBox5.TabIndex = 10;
            this.textBox5.Text = "0.0";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(420, 518);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 11;
            this.button3.Text = "确认";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.max_angle9_btn_Click);
            // 
            // GlForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(698, 575);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.YBtn);
            this.Controls.Add(this.motorPidBtn);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.motorspeedTxt);
            this.Controls.Add(this.motorspeedBtn);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.motorDtbx);
            this.Controls.Add(this.motorItbx);
            this.Controls.Add(this.motorPtbx);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label39);
            this.Controls.Add(this.label38);
            this.Controls.Add(this.label36);
            this.Controls.Add(this.label43);
            this.Controls.Add(this.label42);
            this.Controls.Add(this.label41);
            this.Controls.Add(this.label40);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.stopBtn);
            this.Controls.Add(this.writeBtn);
            this.Controls.Add(this.ShiftOffestTxtbox);
            this.Controls.Add(this.depthLabel);
            this.Controls.Add(this.temperatureLabel);
            this.Controls.Add(this.yawAngleLabel);
            this.Controls.Add(this.zAngularVLabel);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label34);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.label32);
            this.Controls.Add(this.dataLabel);
            this.Controls.Add(this.dazEarthLabel);
            this.Controls.Add(this.acczRealLabel);
            this.Controls.Add(this.acczEarthLabel);
            this.Controls.Add(this.zAccLabel);
            this.Controls.Add(this.vzLabel);
            this.Controls.Add(this.szLabel);
            this.Controls.Add(this.vyLabel);
            this.Controls.Add(this.syLabel);
            this.Controls.Add(this.vxLabel);
            this.Controls.Add(this.sxLabel);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.label37);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pitchAngleLabel);
            this.Controls.Add(this.yAngularVLabel);
            this.Controls.Add(this.dayEarthLabel);
            this.Controls.Add(this.accyRealLabel);
            this.Controls.Add(this.accyEarthLabel);
            this.Controls.Add(this.yAccLabel);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.rollAngleLabel);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.xAngularVLabel);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.daxEarthLabel);
            this.Controls.Add(this.accxRealLabel);
            this.Controls.Add(this.accxEarthLabel);
            this.Controls.Add(this.xAccLabel);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.EmergencyBtn);
            this.Controls.Add(this.remoteBtn);
            this.Controls.Add(this.updateZeroDriftBtn);
            this.Controls.Add(this.serialBtn);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GlForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GlForm";
            this.TransparencyKey = System.Drawing.Color.White;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GlForm_FormClosing);
            this.Load += new System.EventHandler(this.GlForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.accelerateVelocityPage.ResumeLayout(false);
            this.anglePage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel CsGlPanel;
        private System.Windows.Forms.Timer GlTimer;
        private System.Windows.Forms.Button serialBtn;
        public System.IO.Ports.SerialPort serialPort;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage accelerateVelocityPage;
        private System.Windows.Forms.TabPage angularVelocityPage;
        private System.Windows.Forms.TabPage anglePage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label xAccLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label yAccLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label sxLabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label xAngularVLabel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label yAngularVLabel;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label zAngularVLabel;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label rollAngleLabel;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label pitchAngleLabel;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label yawAngleLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label temperatureLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label accxEarthLabel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label accyEarthLabel;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label acczEarthLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label accxRealLabel;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label accyRealLabel;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label acczRealLabel;
        private System.Windows.Forms.Label dataLabel;
        private System.Windows.Forms.Button updateZeroDriftBtn;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label daxEarthLabel;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label dayEarthLabel;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label dazEarthLabel;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label zAccLabel;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label syLabel;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label szLabel;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label vxLabel;
        private System.Windows.Forms.Label vyLabel;
        private System.Windows.Forms.Label vzLabel;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Panel anglePanel;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label depthLabel;
        private System.Windows.Forms.Button remoteBtn;
        private System.IO.Ports.SerialPort RemoteSerialPort;
        private System.Windows.Forms.TextBox ShiftOffestTxtbox;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Button writeBtn;
        private System.Windows.Forms.Button stopBtn;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button EmergencyBtn;
        private System.Windows.Forms.Button motorspeedBtn;
        private System.Windows.Forms.TextBox motorspeedTxt;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Button YBtn;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.TextBox motorPtbx;
        private System.Windows.Forms.TextBox motorItbx;
        private System.Windows.Forms.TextBox motorDtbx;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Button motorPidBtn;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Button button3;
    }
}

