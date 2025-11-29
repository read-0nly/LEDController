namespace HappyLighting
{
    partial class DeviceManager
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            flowLayoutPanel1 = new FlowLayoutPanel();
            timer1 = new System.Windows.Forms.Timer(components);
            flowLayoutPanel2 = new FlowLayoutPanel();
            button4 = new Button();
            button1 = new Button();
            button2 = new Button();
            button5 = new Button();
            button3 = new Button();
            button6 = new Button();
            button7 = new Button();
            button8 = new Button();
            BehaviorTick = new System.Windows.Forms.Timer(components);
            flowLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Padding = new Padding(0, 0, 0, 60);
            flowLayoutPanel1.Size = new Size(553, 286);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.BackColor = Color.FromArgb(64, 64, 64);
            flowLayoutPanel2.Controls.Add(button4);
            flowLayoutPanel2.Controls.Add(button1);
            flowLayoutPanel2.Controls.Add(button2);
            flowLayoutPanel2.Controls.Add(button5);
            flowLayoutPanel2.Controls.Add(button3);
            flowLayoutPanel2.Controls.Add(button6);
            flowLayoutPanel2.Controls.Add(button7);
            flowLayoutPanel2.Controls.Add(button8);
            flowLayoutPanel2.Dock = DockStyle.Bottom;
            flowLayoutPanel2.Location = new Point(0, 246);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(553, 40);
            flowLayoutPanel2.TabIndex = 1;
            flowLayoutPanel2.WrapContents = false;
            // 
            // button4
            // 
            button4.FlatStyle = FlatStyle.Flat;
            button4.ForeColor = SystemColors.ButtonHighlight;
            button4.Location = new Point(3, 3);
            button4.Name = "button4";
            button4.Size = new Size(63, 33);
            button4.TabIndex = 2;
            button4.Text = "Alert";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button1
            // 
            button1.FlatStyle = FlatStyle.Flat;
            button1.ForeColor = SystemColors.ButtonHighlight;
            button1.Location = new Point(72, 3);
            button1.Name = "button1";
            button1.Size = new Size(63, 33);
            button1.TabIndex = 0;
            button1.Text = "Tick";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.FlatStyle = FlatStyle.Flat;
            button2.ForeColor = SystemColors.ButtonHighlight;
            button2.Location = new Point(141, 3);
            button2.Name = "button2";
            button2.Size = new Size(63, 33);
            button2.TabIndex = 1;
            button2.Text = "Scan";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button5
            // 
            button5.FlatStyle = FlatStyle.Flat;
            button5.ForeColor = SystemColors.ButtonHighlight;
            button5.Location = new Point(210, 3);
            button5.Name = "button5";
            button5.Size = new Size(63, 33);
            button5.TabIndex = 5;
            button5.Text = "Server";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button3
            // 
            button3.FlatStyle = FlatStyle.Flat;
            button3.ForeColor = SystemColors.ButtonHighlight;
            button3.Location = new Point(279, 3);
            button3.Name = "button3";
            button3.Size = new Size(63, 33);
            button3.TabIndex = 3;
            button3.Text = "Top";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button6
            // 
            button6.Enabled = false;
            button6.FlatStyle = FlatStyle.Flat;
            button6.ForeColor = SystemColors.ButtonHighlight;
            button6.Location = new Point(348, 3);
            button6.Name = "button6";
            button6.Size = new Size(63, 33);
            button6.TabIndex = 4;
            button6.Text = "XXXX";
            button6.UseVisualStyleBackColor = true;
            button6.Visible = false;
            // 
            // button7
            // 
            button7.Enabled = false;
            button7.FlatStyle = FlatStyle.Flat;
            button7.ForeColor = SystemColors.ButtonHighlight;
            button7.Location = new Point(417, 3);
            button7.Name = "button7";
            button7.Size = new Size(63, 33);
            button7.TabIndex = 7;
            button7.Text = "XXXX";
            button7.UseVisualStyleBackColor = true;
            button7.Visible = false;
            // 
            // button8
            // 
            button8.Enabled = false;
            button8.FlatStyle = FlatStyle.Flat;
            button8.ForeColor = SystemColors.ButtonHighlight;
            button8.Location = new Point(486, 3);
            button8.Name = "button8";
            button8.Size = new Size(63, 33);
            button8.TabIndex = 6;
            button8.Text = "XXXX";
            button8.UseVisualStyleBackColor = true;
            button8.Visible = false;
            // 
            // BehaviorTick
            // 
            BehaviorTick.Interval = 1000;
            BehaviorTick.Tick += BehaviorTick_Tick;
            // 
            // DeviceManager
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(553, 286);
            ControlBox = false;
            Controls.Add(flowLayoutPanel2);
            Controls.Add(flowLayoutPanel1);
            Name = "DeviceManager";
            SizeGripStyle = SizeGripStyle.Show;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LED Manager";
            FormClosing += DeviceManager_FormClosing;
            SizeChanged += DeviceManager_SizeChanged;
            flowLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Timer timer1;
        private FlowLayoutPanel flowLayoutPanel2;
        private Button button1;
        private System.Windows.Forms.Timer BehaviorTick;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button7;
        private Button button8;
    }
}