using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HappyLighting
{
    public partial class DeviceControl : UserControl
    {
        LEDDevice ledDevice;
        public LEDBehavior ledBehavior;
        public static int width = 132;
        public static int height = 152;
        public bool isReady = false;
        public DeviceControl(LEDDevice dev)
        {
            InitializeComponent();
            ledDevice = dev;
            label1.Text = ledDevice.name;
            if (ledDevice.isLoaded)
            {
                this.BackColor = Color.ForestGreen;
            }
            foreach (string s in Enum.GetNames<LEDDevice.Protocols>())
            {
                comboBox2.Items.Add(s);
                if (s == dev.protocol.ToString())
                {
                    comboBox2.Text = s;
                }
            }
            comboBox1.Items.Add("Time Gradient");
            comboBox1.Items.Add("Static Color");
            comboBox1.SelectedText = dev.behavior;
            ledBehavior = new LEDBehavior(ledDevice);

            setBehavior(dev.behavior);
            if(dev.behavior == "Time Gradient")
            {

                (ledBehavior as TimeGradientBehavior).gradient = (Bitmap)Bitmap.FromFile(dev.behavior_param);
                ledBehavior.ready = dev.ticking;
                checkBox1.Checked = dev.ticking;
            }
            width = Size.Width;
            height = Size.Height;

            //Enum.TryParse<LEDDevice.Protocols>(comboBox2.SelectedItem as String, true, out ledDevice.protocol);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Enum.TryParse<LEDDevice.Protocols>(comboBox2.SelectedItem as String, true, out ledDevice.protocol);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            ledBehavior.Init(null, textBox1.Text);

            if (ledBehavior is TimeGradientBehavior)
            {
                ledDevice.behavior = "Time Gradient";
                ledDevice.behavior_param = (ledBehavior as TimeGradientBehavior).path;
                ledBehavior.ready = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (ledBehavior.ready)
            {
                ledBehavior.Tick(null);
                label2.BackColor = ledBehavior.lastColor;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            isReady = checkBox1.Checked;
            ledDevice.ticking = checkBox1.Checked;
        }
        public void tickBehavior()
        {
            if (ledBehavior != null && ledBehavior.ready)
            {
                if (ledDevice.tx == null)
                {
                    ledDevice.ValidateDevice();
                }
                else
                {
                    ledBehavior.Tick(null);
                }
                label2.BackColor = ledBehavior.lastColor;
            }

        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            tickBehavior();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            StreamWriter sw = File.AppendText("savedLEDs.txt");
            sw.WriteLine(ledDevice.Serialize());
            sw.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            ledDevice.tx = null;
            BLEHandler.deviceList.Remove(ledDevice.device.BluetoothAddress);
            ledDevice.device = null;
            ledDevice.hidden = true;
            isReady = false;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            setBehavior(comboBox1.SelectedItem.ToString());

        }
        private void setBehavior(string s)
        {
            switch (s)
            {
                case "Time Gradient":
                    if (!(ledBehavior is TimeGradientBehavior))
                    {
                        ledBehavior = new TimeGradientBehavior(ledDevice);
                    }
                    break;
                case "Static Color":
                    if (!(ledBehavior is ColorStaticBehavior))
                    {
                        ledBehavior = new ColorStaticBehavior(ledDevice);
                    }
                    break;
            }

        }
    }
}
