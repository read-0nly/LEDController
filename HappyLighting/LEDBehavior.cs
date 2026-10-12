using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Windows.Devices.HumanInterfaceDevice;

namespace HappyLighting
{
    public class LEDBehavior
    {
        public LEDDevice device;
        public bool ready = false;
        public Color lastColor = Color.FromArgb(0,0,0,0);
        
        public LEDBehavior(LEDDevice dev)
        {
            device = dev;
        }
        public virtual void Init(Object arg, string param = "")
        {
            ready = true;
        }
        public virtual string Tick(Object arg)
        {
            if (alerting)
            {
                Alert();
                return "Alerting";
            }
            return "Not a real behavior";
        }
        public bool alerting;
        public virtual void Alert()
        {
            Color c = Color.FromArgb(255- lastColor.R, 255 - lastColor.G, 255 - lastColor.B);
            try
            {
                device.writeColor((int)c.R, (int)c.G, (int)c.B);
                lastColor = c;

            }
            catch (Exception ex) { }

        }
    }
    public class ColorStaticBehavior : LEDBehavior
    {
        Color setColor;
        public ColorStaticBehavior(LEDDevice dev) : base(dev)
        {
        }

        public override void Init(Object arg, string param = "")
        {
            ColorDialog cdlg = new ColorDialog();
            if (cdlg.ShowDialog() == DialogResult.OK && (device != null))
            {
                setColor = cdlg.Color;

            }
            ready = true;
        }

        public override string Tick(Object arg)
        {
            base.Tick(arg);
            if (alerting) { return "Alerting"; }
            if (setColor != lastColor)
            {
                try
                {
                    device.writeColor((int)setColor.R, (int)setColor.G, (int)setColor.B);
                    lastColor = setColor;
                }
                catch (Exception ex) { }

            }

            return "Static Color";
        }


    }

    public class TimeGradientBehavior : LEDBehavior
    {
        public Bitmap gradient;
        public string path;
        int offset = 0;


        public TimeGradientBehavior(LEDDevice dev) : base(dev)
        {
        }

        public override void Init(Object arg, string param = "")
        {
            if (param != "")
            {
                try
                {
                    offset = Int32.Parse(param);
                    if (offset < 0)
                    {
                        offset += 1440;
                    }
                }
                catch
                {
                    device.writeColor(255, 0, 0);
                }
            }

            OpenFileDialog openFileDialog1;
            openFileDialog1 = new OpenFileDialog();
            openFileDialog1.FileName = "";
            openFileDialog1.DefaultExt = ".png";
            if (openFileDialog1.ShowDialog() == DialogResult.OK && (device != null) && openFileDialog1.FileName != "" && File.Exists(openFileDialog1.FileName))
            {
                gradient = (Bitmap)Bitmap.FromFile(openFileDialog1.FileName);
                path = openFileDialog1.FileName;
                ready = true;
            }
        }
        public override string Tick(Object arg)
        {
            base.Tick(arg);
            if (alerting) { return "Alerting"; }
            int currentTime = ((DateTime.Now.Hour + offset) * 60 ) + (DateTime.Now.Minute );
            int idx = (int)Math.Round((
                    currentTime / (24 * 60.0)) *
                    gradient.Width);
            idx = (idx % gradient.Width);

            int idx2 = (int)Math.Round((
                    (currentTime+1) / (24 * 60.0)) *
                    gradient.Width);
            idx2 = (idx2 % gradient.Width);

            Color c = gradient.GetPixel(idx, 0);
            Color c2 = gradient.GetPixel(idx2, 0);

            double fac1 = (((DateTime.Now.Second) * 1000.0)+DateTime.Now.Millisecond)/(60*1000.0);
            double fac2 = 1 - fac1;

            c = Color.FromArgb(
                (int)((c.R * fac2) + (c2.R * fac1)),
                (int)((c.G * fac2) + (c2.G * fac1)),
                (int)((c.B * fac2) + (c2.B * fac1))
                );
            if (c != lastColor)
            {
                try
                {
                    device.writeColor((int)c.R, (int)c.G, (int)c.B);
                    lastColor = c;
                }
                catch (Exception ex) { }
            }
            return (((DateTime.Now.Hour * 60.0f) + DateTime.Now.Minute)).ToString();

        }
    }
}
