using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace CsOpenGL
{
    public partial class serialPortForm : Form
    {
        public GlForm parent;
        public serialPortForm formSerial;

        public serialPortForm()
        {
            InitializeComponent();
            formSerial = this;
        }

        public serialPortForm(GlForm parent)
        {
            InitializeComponent();
            this.parent = parent;
            formSerial = this;
        }

        private void serialPortForm_Load(object sender, EventArgs e)
        {
            //设定串口的停靠位置
            this.Location = new Point(parent.Location.X - this.Width,parent.Location.Y);

            string[] portsName = SerialPort.GetPortNames();
            Array.Sort(portsName);  //使用快速排序方法进行排序
            comboPortName.Items.AddRange(portsName);
            comboPortName.SelectedIndex = comboPortName.Items.Count > 0 ? 0 : -1;
            comboBaudRate.SelectedIndex = comboBaudRate.Items.IndexOf("115200");

            //设置控件状态
            btnSwitchSerial.Text = this.parent.serialPort.IsOpen ? "关闭串口" : "打开串口";
            comboPortName.Enabled = !this.parent.serialPort.IsOpen;
            comboBaudRate.Enabled = !this.parent.serialPort.IsOpen;
        }

        private void btnSwitchSerial_Click(object sender, EventArgs e)
        {
            try {
                if (this.parent.serialPort.IsOpen)
                {
                    this.parent.serialPort.Close();
                    comboPortName.Enabled = !this.parent.serialPort.IsOpen;
                    comboBaudRate.Enabled = !this.parent.serialPort.IsOpen;
                }
                else
                {
                    this.parent.serialPort.PortName = comboPortName.Text;
                    this.parent.serialPort.BaudRate = int.Parse(comboBaudRate.Text);
                    this.parent.serialPort.DataBits = 8;
                    this.parent.serialPort.StopBits = StopBits.One;
                    this.parent.serialPort.Parity = Parity.None;
                    try
                    {
                        this.parent.serialPort.Open();
                    }
                    catch (Exception ex)
                    {
                        //捕获到异常信息，创建一个新的comm对象，之前的不能用了
                        this.parent.serialPort = new SerialPort();
                        MessageBox.Show(ex.Message, "Error");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            //设置控件状态
            btnSwitchSerial.Text = this.parent.serialPort.IsOpen ? "关闭串口" : "打开串口";
            //this.parent.btnCtrlDepthSend.Enabled = this.parent.COMM.IsOpen;
            comboPortName.Enabled = !this.parent.serialPort.IsOpen;
            comboBaudRate.Enabled = !this.parent.serialPort.IsOpen;
        }
    }
}
