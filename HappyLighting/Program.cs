namespace HappyLighting
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            if (args.Length > 0)
            {
                foreach (string s in args)
                {
                    if(s == "quickkill")
                    {
                        DeviceManager.killDelay = 10000;
                    }
                    if (s == "alert")
                    {
                        DeviceManager.StartAlert = true;
                    }
                    if (s == "server")
                    {
                        DeviceManager.StartAlert = true;
                    }
                    if (s == "ultra")
                    {
                        DeviceManager.StartAlert = true;
                    }
                    Console.WriteLine(s);
                }
            }
            DeviceManager devmgr = new DeviceManager();
            Application.Run(devmgr);


        }
    }
}