using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HappyLighting
{
    public partial class DeviceManager : Form
    {
        public static HashSet<LEDDevice> deviceList = new HashSet<LEDDevice>();

        public DeviceManager()
        {
            InitializeComponent();
            this.ClientSize = new System.Drawing.Size((int)(3 * (DeviceControl.width + 6)),
                (int)(1 * (DeviceControl.height + 6)) + 40);
            if (File.Exists("savedLEDs.txt"))
            {
                string[] LEDLoad = File.ReadAllLines("savedLEDs.txt");
                foreach (string s in LEDLoad)
                {
                    LEDDevice test = new LEDDevice(null);
                    if (test.Deserialize(s))
                    {
                        BLEHandler.loadedDevices.Add(test);
                    }
                }
            }
            else
            {
                File.Create("savedLEDs.txt");
            }
            BLEHandler.exec(false, BLEHandler.Watcher_Received);

        }


        public static bool StartTick = false;
        public static bool StopTick = false;
        public static bool StartAlert = false;
        public static bool StopAlert = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            lock (BLEHandler.deviceList)
            {
                foreach (LEDDevice device in BLEHandler.deviceList)
                {
                    if (!deviceList.Contains(device))
                    {
                        deviceList.Add(device);
                        device.control = new DeviceControl(device);
                        this.flowLayoutPanel1.Controls.Add(device.control);

                    }
                }
            }
            if (StartTick)
            {
                swapTick(true);
                StartTick = false;
            }
            if (StopTick)
            {
                swapTick(false);
                StopTick = false;
            }
            if (StartAlert)
            {
                swapAlert(true);
                StartAlert = false;
            }
            if (StopAlert)
            {
                swapAlert(false);
                StopAlert = false;
            }
            if (!Webhost.listener.IsListening)
            {
                button5.ForeColor = Color.White;
                button5.BackColor = Color.Transparent;
            }
            else
            {
                button5.BackColor = Color.Black;
                button5.ForeColor = Color.Cyan;

            }
        }

        private void DeviceManager_SizeChanged(object sender, EventArgs e)
        {
            int bestSize = (int)(1 * (DeviceControl.width + 6));
            int targetWidth = (int)(Math.Floor((this.Size.Width / (DeviceControl.width + 6.0))) * (DeviceControl.width + 6.0));
            this.ClientSize = new System.Drawing.Size(targetWidth < bestSize ? bestSize : targetWidth,
                (int)((this.Size.Height / (DeviceControl.height + 6)) * (DeviceControl.height + 6)) + 40);

        }

        private void swapTick(bool? switchbool = null)
        {
            if (switchbool != null)
            {
                BehaviorTick.Enabled = !(bool)(switchbool);
            }
            if (!BehaviorTick.Enabled)
            {
                button1.ForeColor = Color.Red;
                button1.BackColor = Color.Black;
                BehaviorTick.Start();
            }
            else
            {
                button1.ForeColor = Color.White;
                button1.BackColor = Color.Transparent;
                BehaviorTick.Stop();

            }

        }
        private void swapAlert(bool? switchbool = null)
        {
            if (switchbool != null)
            {
                button4bool = !(bool)(switchbool);
            }
            if (button4bool)
            {
                button4.ForeColor = Color.White;
                button4.BackColor = Color.Transparent;
                button4bool = false;
                foreach (LEDDevice d in deviceList)
                {
                    d.control.ledBehavior.alerting = false;
                }
            }
            else
            {

                button4.ForeColor = Color.Red;
                button4.BackColor = Color.Black;
                button4bool = true;
                foreach (LEDDevice d in deviceList)
                {
                    d.control.ledBehavior.alerting = true;
                }
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            swapTick();
        }

        private void BehaviorTick_Tick(object sender, EventArgs e)
        {
            foreach (LEDDevice device in deviceList)
            {
                LEDBehavior behavior = null;
                if (device.hidden == false && device.control != null && device.control.isReady)
                {
                    device.control.tickBehavior();
                    Thread.Sleep(50);
                }

            }
        }
        bool scanning = false;
        private void button2_Click(object sender, EventArgs e)
        {
            if (scanning)
            {
                BLEHandler.stopWatch();
                button2.ForeColor = Color.White;
                button2.BackColor = Color.Transparent;
                scanning = false;
            }
            else
            {

                BLEHandler.startWatch();
                button2.ForeColor = Color.Cyan;
                button2.BackColor = Color.Black;
                scanning = true;
            }

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        bool button3bool = false;
        private void button3_Click(object sender, EventArgs e)
        {

            if (button3bool)
            {
                this.TopMost = false;
                button3.ForeColor = Color.White;
                button3.BackColor = Color.Transparent;
                button3bool = false;
            }
            else
            {
                this.TopMost = true;

                button3.ForeColor = Color.Lime;
                button3.BackColor = Color.Black;
                button3bool = true;
            }
        }

        bool button4bool = false;
        private void button4_Click(object sender, EventArgs e)
        {
            swapAlert();

        }

        private void DeviceManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            Webhost.Stop();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Webhost.listener.IsListening)
            {
                Webhost.Stop();
                button5.ForeColor = Color.White;
                button5.BackColor = Color.Transparent;
            }
            else
            {
                // ThreadStart delegate.
                Webhost.listener = new HttpListener();

                Thread StaticCaller = new(new ThreadStart(Webhost.Start));
                // Start the thread.
                StaticCaller.Start();
                button5.BackColor = Color.Black;
                button5.ForeColor = Color.Cyan;

            }

        }
    }
}
