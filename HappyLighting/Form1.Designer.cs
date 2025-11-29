namespace HappyLighting
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            button2 = new Button();
            button1 = new Button();
            comboBox1 = new ComboBox();
            timer1 = new System.Windows.Forms.Timer(components);
            colorDialog1 = new ColorDialog();
            button3 = new Button();
            openFileDialog1 = new OpenFileDialog();
            textBox1 = new TextBox();
            comboBox2 = new ComboBox();
            trackBar1 = new TrackBar();
            timer2 = new System.Windows.Forms.Timer(components);
            comboBox3 = new ComboBox();
            button4 = new Button();
            pixelAnchorCoordTB = new TextBox();
            pixelAnchorColorTB = new TextBox();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            SuspendLayout();
            // 
            // button2
            // 
            button2.Location = new Point(93, 77);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 2;
            button2.Text = "Set Color";
            button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(12, 77);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 1;
            button1.Text = "Toggle";
            button1.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            comboBox1.BackColor = SystemColors.ControlDarkDark;
            comboBox1.FlatStyle = FlatStyle.Flat;
            comboBox1.ForeColor = SystemColors.ScrollBar;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(12, 48);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(480, 23);
            comboBox1.TabIndex = 0;
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 1000;
            // 
            // button3
            // 
            button3.Location = new Point(174, 77);
            button3.Name = "button3";
            button3.Size = new Size(156, 23);
            button3.TabIndex = 3;
            button3.Text = "PIck time gradient";
            button3.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // textBox1
            // 
            textBox1.BackColor = SystemColors.ControlDarkDark;
            textBox1.BorderStyle = BorderStyle.FixedSingle;
            textBox1.ForeColor = SystemColors.Info;
            textBox1.Location = new Point(336, 78);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(156, 23);
            textBox1.TabIndex = 4;
            // 
            // comboBox2
            // 
            comboBox2.BackColor = SystemColors.ControlDarkDark;
            comboBox2.FlatStyle = FlatStyle.Flat;
            comboBox2.ForeColor = SystemColors.ScrollBar;
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(12, 107);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(480, 23);
            comboBox2.TabIndex = 5;
            // 
            // trackBar1
            // 
            trackBar1.Location = new Point(12, 136);
            trackBar1.Maximum = 255;
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(480, 45);
            trackBar1.TabIndex = 6;
            // 
            // timer2
            // 
            timer2.Enabled = true;
            timer2.Interval = 10000;
            // 
            // comboBox3
            // 
            comboBox3.BackColor = SystemColors.ControlDarkDark;
            comboBox3.FlatStyle = FlatStyle.Flat;
            comboBox3.ForeColor = SystemColors.ScrollBar;
            comboBox3.FormattingEnabled = true;
            comboBox3.Items.AddRange(new object[] { "HappyLighting", "HiLighting" });
            comboBox3.Location = new Point(12, 19);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(480, 23);
            comboBox3.TabIndex = 7;
            comboBox3.Text = "HappyLighting";
            // 
            // button4
            // 
            button4.Location = new Point(12, 187);
            button4.Name = "button4";
            button4.Size = new Size(156, 23);
            button4.TabIndex = 8;
            button4.Text = "PixelAnchor";
            button4.UseVisualStyleBackColor = true;
            // 
            // pixelAnchorCoordTB
            // 
            pixelAnchorCoordTB.BackColor = SystemColors.ControlDarkDark;
            pixelAnchorCoordTB.BorderStyle = BorderStyle.FixedSingle;
            pixelAnchorCoordTB.ForeColor = SystemColors.Info;
            pixelAnchorCoordTB.Location = new Point(174, 187);
            pixelAnchorCoordTB.Name = "pixelAnchorCoordTB";
            pixelAnchorCoordTB.ReadOnly = true;
            pixelAnchorCoordTB.Size = new Size(156, 23);
            pixelAnchorCoordTB.TabIndex = 9;
            // 
            // pixelAnchorColorTB
            // 
            pixelAnchorColorTB.BackColor = SystemColors.ControlDarkDark;
            pixelAnchorColorTB.BorderStyle = BorderStyle.FixedSingle;
            pixelAnchorColorTB.ForeColor = SystemColors.Info;
            pixelAnchorColorTB.Location = new Point(336, 187);
            pixelAnchorColorTB.Name = "pixelAnchorColorTB";
            pixelAnchorColorTB.ReadOnly = true;
            pixelAnchorColorTB.Size = new Size(156, 23);
            pixelAnchorColorTB.TabIndex = 10;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(504, 249);
            Controls.Add(pixelAnchorColorTB);
            Controls.Add(pixelAnchorCoordTB);
            Controls.Add(button4);
            Controls.Add(comboBox3);
            Controls.Add(trackBar1);
            Controls.Add(comboBox2);
            Controls.Add(textBox1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(comboBox1);
            Controls.Add(button1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button button1;
        private ComboBox comboBox1;
        private System.Windows.Forms.Timer timer1;
        private Button button2;
        private ColorDialog colorDialog1;
        private Button button3;
        private OpenFileDialog openFileDialog1;
        private TextBox textBox1;
        private ComboBox comboBox2;
        private TrackBar trackBar1;
        private System.Windows.Forms.Timer timer2;
        private ComboBox comboBox3;
        private Button button4;
        private TextBox pixelAnchorCoordTB;
        private TextBox pixelAnchorColorTB;
    }
}
