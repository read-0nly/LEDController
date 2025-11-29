using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Devices.Bluetooth;
using Windows.Storage.Streams;

namespace HappyLighting
{
    public class LEDDevice
    {
        public enum Protocols
        {
            None = -1,
            HappyLighting = 1,
            HiLighting = 2

        }
        public enum Modes
        {
            None = -1,
            Pulsating_rainbow = 37,
            Pulsating_red = 38,
            Pulsating_green = 39,
            Pulsating_blue = 40,
            Pulsating_yellow = 41,
            Pulsating_cyan = 42,
            Pulsating_purple = 43,
            Pulsating_white = 44,
            Pulsating_red_green = 45,
            Pulsating_red_blue = 46,
            Pulsating_green_blue = 47,
            Rainbow_strobe = 48,
            Red_strobe = 49,
            Green_strobe = 50,
            Blue_strobe = 51,
            Yellow_strobe = 52,
            Cyan_strobe = 53,
            Purple_strobe = 54,
            white_strobe = 55,
            Rainbow_jumping_change = 56,
            Pulsating_RGB = 97,
            RGB_jumping_change = 98,
            Music_Mode = 99
        }

        public string mac = "";
        public string name = "";
        
        public HashSet<string> deviceServices = new HashSet<string>();
        public BluetoothLEDevice device = null;
        public GattCharacteristic tx = null;
        public GattCharacteristic rx = null;
        public DeviceControl control = null;
        public Protocols protocol = Protocols.None;
        public bool hidden = false;
        public bool isLoaded = false;

        public string Serialize()
        {
            return name + ";" + mac + ";" + protocol.ToString() + ";" + hidden;
        }
        public bool Deserialize(string s)
        {
            string[] values = s.Split(";");
            if (values.Length < 4) { return false; }
            name = values[0];
            mac = values[1];
            Enum.TryParse<Protocols>(values[2], out protocol);
            bool.TryParse(values[3], out hidden);
            return true;
        }

        public LEDDevice(BluetoothLEDevice dev)
        {
            device = dev;
            if (dev != null)
            {
                mac = dev.BluetoothAddress.ToString();
                name = device.Name;
            }
        }
        public LEDDevice(BluetoothLEDevice dev, string proto)
        {
            device = dev;
            if (dev != null)
            {
                mac = dev.BluetoothAddress.ToString();
                name = device.Name;
            }
            try
            {
                protocol = Enum.Parse<Protocols>(proto);
            }
            catch { }
        }

        public bool ValidateDevice()

        {
            lock (device)
            {
                if (!BLEHandler.deviceList.Contains(this)) { return false; }
                if (!Task.Run(() => device.RequestAccessAsync()).Wait(5000) || !Task.Run(() => device.GetGattServicesAsync()).Wait(5000) || device.GattServices == null)
                {
                    tx = null;
                    return false;
                }
                foreach (GattDeviceService service in device.GattServices)
                {
                    lock (deviceServices) { 
                        if (!deviceServices.Contains(service.Uuid.ToString()))
                        {
                            deviceServices.Add(service.Uuid.ToString());
                        }
                        Console.WriteLine("Found serv " + service.Uuid);
                        GattCharacteristicsResult gcr = null;
                        try { Task.Run(async () => gcr = await service.GetCharacteristicsAsync()).Wait(5000); } catch (Exception e) { }
                        if (gcr == null)
                        {
                            continue;
                        }
                        foreach (var x in gcr.Characteristics)
                        {
                            GattCharacteristicProperties properties = x.CharacteristicProperties;
                            if (properties.HasFlag(GattCharacteristicProperties.WriteWithoutResponse) && properties.HasFlag(GattCharacteristicProperties.Write))
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Found TX");
                                tx = x;
                                Console.ResetColor();
                                return true;
                            }
                        }
                    }


                }
            }
            tx = null;
            return false;
        }


        public static Color GetColorAt(int x, int y)
        {
            Bitmap bmp = new Bitmap(1, 1);
            Rectangle bounds = new Rectangle(x, y, 1, 1);
            using (Graphics g = Graphics.FromImage(bmp))
                g.CopyFromScreen(bounds.Location, System.Drawing.Point.Empty, bounds.Size);
            return bmp.GetPixel(0, 0);
        }
        public void writePower(bool on)
        {
            byte[] payload = (byte[])[((byte)204), ((byte)(36 - (on ? 1 : 0))), ((byte)51)];
            IBuffer buff = Windows.Security.Cryptography.CryptographicBuffer.CreateFromByteArray(payload);
            Task.Run(() => tx.WriteValueAsync(buff));

        }
        public byte[] lastColor = new byte[3];


        public static int modifyWordWidth(int i, int i2, int i3)
        {
            return (i << (i3 - i2)) & ((1 << i3) - 1);
        }

        public static int quantizedBlue(int i)
        {
            return i & 15;
        }

        public static int quantizedGreen(int i)
        {
            return (i >> 4) & 15;
        }

        public static int quantizedRed(int i)
        {
            return (i >> 8) & 15;
        }

        public void writeMode(byte mode, byte speed)
        {
            byte[] payload = (byte[])[((byte)(256 - 69)), ((byte)mode), ((byte)speed), ((byte)68)];
            IBuffer buff = Windows.Security.Cryptography.CryptographicBuffer.CreateFromByteArray(payload);
            Task.Run(() => tx.WriteValueAsync(buff));
        }

        public void writeColor(int r, int g, int b)
        {
            writeColor((byte)r, (byte)g, (byte)b);
        }
        public void writeColor(byte r, byte g, byte b)
        {
            if (protocol == Protocols.HappyLighting)
            {
                writeColor_happy(r, g, b);
            }
            else if (protocol == Protocols.HiLighting)
            {
                writeColor_hi(r, g, b);
            }
        }


        public void writeColor_hi(byte r, byte g, byte b)
        {
            int magicnum = Color.FromArgb(r, g, b).ToArgb();
            int rint = r;
            int gint = g;
            int bint = b;
            rint = (int)Math.Min(255, rint * 1.2);
            gint = (int)(gint * 0.5);
            bint = (int)(bint * 0.35);
            byte[] payload = (byte[])[85,  7, 1, (byte)modifyWordWidth(quantizedGreen(rint),4,8),
                (byte)modifyWordWidth(quantizedGreen(gint),4,8), (byte)modifyWordWidth(quantizedGreen(bint),4,8)];
            IBuffer buff = Windows.Security.Cryptography.CryptographicBuffer.CreateFromByteArray(payload);
            Task.Run(() => {
                if (tx == null) { ValidateDevice(); }
                else
                {
                    try
                    {
                        tx.WriteValueAsync(buff);
                    }
                    catch (Exception e) { }
                }
            });
        }

        public void writeColor_happy(byte r, byte g, byte b)
        {
            byte magicnum = (byte)(10 * 255 / 100);
            byte[] payload = (byte[])[((byte)86), r, g, b, magicnum, (byte)(256 - 16), (byte)(256 - 86)];
            IBuffer buff = Windows.Security.Cryptography.CryptographicBuffer.CreateFromByteArray(payload);
            Task.Run(() => { if (tx == null) { ValidateDevice(); } else { try { tx.WriteValueAsync(buff); } catch (Exception e) { } } });
            lastColor[0] = r;
            lastColor[1] = g;
            lastColor[2] = b;
        }

    }
}
