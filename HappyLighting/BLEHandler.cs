using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.Advertisement;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Devices.Bluetooth;
using Windows.Foundation;
using System.Diagnostics.Eventing.Reader;


namespace HappyLighting
{ 
    class BLEHandler

    {
        public static HashSet<LEDDevice> deviceList = new HashSet<LEDDevice>();
        public static HashSet<LEDDevice> loadedDevices = new HashSet<LEDDevice>();
        public static HashSet<string> seen_macs = new HashSet<string>();
        public static bool keeprunning = false;
        public static async void Watcher_Received(
            BluetoothLEAdvertisementWatcher sender,
            BluetoothLEAdvertisementReceivedEventArgs args)
        {
            var device = await BluetoothLEDevice.FromBluetoothAddressAsync(args.BluetoothAddress);
            if (device != null)
            {
                GattCharacteristic resultTX = null;
                LEDDevice di = null;
                bool found = false;
                foreach (LEDDevice ld in loadedDevices)
                {
                    if(!found && ld.mac== device.BluetoothAddress.ToString())
                    {
                        ld.device = device;
                        ld.isLoaded = true;
                        di = ld;
                        found = true;
                        if (ld.hidden) { seen_macs.Add(device.BluetoothAddress.ToString()); }
                        break;
                    }
                }
                if (!found)
                {
                    di = new LEDDevice(device);
                }
                if (device.DeviceInformation != null)
                {
                    lock (deviceList)
                    {
                        if (!deviceList.Contains(di) && !seen_macs.Contains(di.device.BluetoothAddress.ToString()))
                        {
                            deviceList.Add(di);
                            seen_macs.Add(di.device.BluetoothAddress.ToString());

                        }
                    }

                }
            }

        }
        static BluetoothLEAdvertisementWatcher watcher = new BluetoothLEAdvertisementWatcher();
        public static void stopWatch()
        {
            watcher.Stop();
        }
        public static void startWatch()
        {
            watcher.Start();
        }
        static async Task exec(string[] args)
        {
            keeprunning = true;
            watcher = new BluetoothLEAdvertisementWatcher()
            {
                ScanningMode = BluetoothLEScanningMode.Passive
            };

            watcher.Received += Watcher_Received;
            watcher.Start();
            while (keeprunning)
            {
                await Task.Delay(1000);
            }
            watcher.Stop();
        }
        public static void exec(bool start, TypedEventHandler<BluetoothLEAdvertisementWatcher, BluetoothLEAdvertisementReceivedEventArgs> rxer)
        {
            watcher = new BluetoothLEAdvertisementWatcher()
            {
                ScanningMode = BluetoothLEScanningMode.Passive
            };

            watcher.Received += rxer;
            if (start)
            {
                watcher.Start();
            }
        }
        /*
        private static async void Watcher_Received(
            BluetoothLEAdvertisementWatcher sender,
            BluetoothLEAdvertisementReceivedEventArgs args)
        {
            var device = await BluetoothLEDevice.FromBluetoothAddressAsync(args.BluetoothAddress);
            if (device != null)
            {
                if (device.DeviceInformation != null && device.DeviceInformation.Name.Contains("QHM"))
                {
                    if (string.Format("{0:X}", device.BluetoothAddress) == "720513000274")
                    {
                        await device.RequestAccessAsync();
                        await device.GetGattServicesAsync();
                        foreach (GattDeviceService service in device.GattServices)
                        {
                            Console.WriteLine("Found serv " + service.Uuid);
                            await service.RequestAccessAsync();
                            GattCharacteristicsResult gcr = await service.GetCharacteristicsAsync();
                            foreach (var x in gcr.Characteristics)
                            {
                                GattCharacteristicProperties properties = x.CharacteristicProperties;
                                if (properties.HasFlag(GattCharacteristicProperties.WriteWithoutResponse) && properties.HasFlag(GattCharacteristicProperties.Write))
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("Found TX");
                                    Console.ResetColor();
                                }
                                else if (service.Uuid.ToString() == "00001800-0000-1000-8000-00805f9b34fb")
                                {

                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("Found RX");
                                    Console.ResetColor();
                                }
                                Console.WriteLine(properties.ToString());
                            }

                        }
                    }

                }
            }
        }
        */
    }

}
