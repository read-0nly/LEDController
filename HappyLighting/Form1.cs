using System;
using System.Drawing;
using System.Reflection.PortableExecutable;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.Advertisement;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Foundation;
using Windows.Storage.Streams;
using System.Timers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using Windows.UI.ViewManagement.Core;
using System.IO;
using System.Buffers;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HappyLighting
{
    public partial class Form1 : Form
    {
        private static Form1 _self;


       // private static Dictionary<string, DeviceIdentity> Devices = new Dictionary<string, DeviceIdentity>();
       //class DeviceIdentity
       // {
       //     public DeviceIdentity(string m = "", string n = "", BluetoothLEDevice d = null)
       //     {
       //         mac = m;
       //         name = n;
       //         device = d;
       //     }

       //     public static implicit operator string(DeviceIdentity d) => d.mac + ":" + d.name;
            

       // }
       // public Form1()
       // {
       //     _self = this;
       //     InitializeComponent();
       //     BLEHandler.exec(BLEHandler.Watcher_Received);
       //     foreach (string s in Enum.GetNames<LEDDevice.Modes>())
       //     {
       //         comboBox2.Items.Add(s);
       //     }
       // }
       // int tick = 0;
       // private void updateText()
       // {
       //     string[] deviceListArray;
       //     lock (deviceList)
       //     {
       //         deviceListArray = deviceList.ToArray();
       //     }
       //     string result = "";
       //     result = "";
       //     foreach (string name in deviceListArray)
       //     {
       //         if (!comboBox1.Items.Contains(name))
       //         {
       //             comboBox1.Items.Add(name);
       //         }
       //     }
       //     if (tx_svc != null && rx != "Not Found" && sendDemo)
       //     {
       //         writePower((tick % 2) == 0);
       //         if ((tick % 2) == 0)
       //         {
       //             switch (tick % 3)
       //             {
       //                 case 0:
       //                     writeColor(255, 0, 0);
       //                     break;
       //                 case 1:
       //                     writeColor(255, 255, 0);
       //                     break;
       //                 case 2:
       //                     writeColor(255, 0, 255);
       //                     break;
       //             }
       //         }
       //         tick++;
       //     }
       // }
       // private void timer1_Tick(object sender, EventArgs e)
       // {
       //     updateText();
       // }

       // private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
       // {
       //     if (comboBox1.SelectedItem != null)
       //     {
       //         lock (selectedDevice)
       //         {

       //             selectedDevice = comboBox1.SelectedItem.ToString();
       //             Task.Run(() => ConnectDevice()).Wait(5000);
       //         }
       //     }
       // }
       // bool sendDemo = false;
       // private void button1_Click(object sender, EventArgs e)
       // {
       //     sendDemo = !sendDemo;
       // }

       // private void button2_Click(object sender, EventArgs e)
       // {
       //     colorDialog1.ShowDialog();
       //     if ((selectedDevice != null))
       //     {
       //         writeColor(colorDialog1.Color.R, colorDialog1.Color.G, colorDialog1.Color.B);
       //         textBox1.Text = colorDialog1.Color.ToString();
       //     }
       // }
       // Bitmap gradient = null;
       // private void button3_Click(object sender, EventArgs e)
       // {
       //     openFileDialog1.DefaultExt = ".png";
       //     if (openFileDialog1.ShowDialog() == DialogResult.OK && (selectedDevice != null) && openFileDialog1.FileName != "" && File.Exists(openFileDialog1.FileName))
       //     {


       //     }
       // }

       // private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
       // {
       //     if (selectedDevice != null)
       //     {
       //         Modes m = Modes.None;
       //         if (comboBox2.SelectedItem != null)
       //         {
       //             Enum.TryParse(comboBox2.SelectedItem.ToString(), out m);
       //         }
       //         if (m != Modes.None)
       //         {
       //             writeMode((byte)m, (byte)(255 - trackBar1.Value));
       //         }
       //     }

       // }

       // private void trackBar1_Scroll(object sender, EventArgs e)
       // {
       //     if (selectedDevice != null)
       //     {
       //         Modes m = Modes.None;
       //         if (comboBox2.SelectedItem != null)
       //         {
       //             Enum.TryParse(comboBox2.SelectedItem.ToString(), out m);
       //         }
       //         if (m != Modes.None)
       //         {
       //             writeMode((byte)m, (byte)(255 - trackBar1.Value));
       //         }
       //     }
       // }


       // private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
       // {
       //     selectedProtocol = Enum.Parse<Protocols>(comboBox3.Text);
       // }
       // System.Drawing.Point CursorPosition = System.Drawing.Point.Empty;
       // private void button4_Click(object sender, EventArgs e)
       // {
       //     CursorPosition = Cursor.Position;
       // }
    }
   }
