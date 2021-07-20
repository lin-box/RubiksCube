namespace myOpenGL
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnThirdXDown = new System.Windows.Forms.Button();
            this.btnSecondXDown = new System.Windows.Forms.Button();
            this.btnThirdYRight = new System.Windows.Forms.Button();
            this.btnFirstYRight = new System.Windows.Forms.Button();
            this.btnSecondYRight = new System.Windows.Forms.Button();
            this.btnThirdYLeft = new System.Windows.Forms.Button();
            this.btnFirstYLeft = new System.Windows.Forms.Button();
            this.btnFirstXDown = new System.Windows.Forms.Button();
            this.btnSecondYLeft = new System.Windows.Forms.Button();
            this.btnThirdXUp = new System.Windows.Forms.Button();
            this.btnSecondXUp = new System.Windows.Forms.Button();
            this.btnFirstXUp = new System.Windows.Forms.Button();
            this.timerRepaint = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.hScrollBar3 = new System.Windows.Forms.HScrollBar();
            this.hScrollBar2 = new System.Windows.Forms.HScrollBar();
            this.hScrollBar4 = new System.Windows.Forms.HScrollBar();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel1.Location = new System.Drawing.Point(12, 14);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(550, 533);
            this.panel1.TabIndex = 6;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.Resize += new System.EventHandler(this.panel1_Resize);
            // 
            // btnThirdXDown
            // 
            this.btnThirdXDown.Location = new System.Drawing.Point(694, 193);
            this.btnThirdXDown.Name = "btnThirdXDown";
            this.btnThirdXDown.Size = new System.Drawing.Size(30, 23);
            this.btnThirdXDown.TabIndex = 29;
            this.btnThirdXDown.Text = "\\/";
            this.btnThirdXDown.UseVisualStyleBackColor = true;
            this.btnThirdXDown.Click += new System.EventHandler(this.btnThirdXDown_Click);
            // 
            // btnSecondXDown
            // 
            this.btnSecondXDown.Location = new System.Drawing.Point(658, 193);
            this.btnSecondXDown.Name = "btnSecondXDown";
            this.btnSecondXDown.Size = new System.Drawing.Size(30, 23);
            this.btnSecondXDown.TabIndex = 28;
            this.btnSecondXDown.Text = "\\/";
            this.btnSecondXDown.UseVisualStyleBackColor = true;
            this.btnSecondXDown.Click += new System.EventHandler(this.btnSecondXDown_Click);
            // 
            // btnThirdYRight
            // 
            this.btnThirdYRight.Location = new System.Drawing.Point(726, 107);
            this.btnThirdYRight.Name = "btnThirdYRight";
            this.btnThirdYRight.Size = new System.Drawing.Size(28, 23);
            this.btnThirdYRight.TabIndex = 27;
            this.btnThirdYRight.Text = ">";
            this.btnThirdYRight.UseVisualStyleBackColor = true;
            this.btnThirdYRight.Click += new System.EventHandler(this.btnThirdYRight_Click);
            // 
            // btnFirstYRight
            // 
            this.btnFirstYRight.Location = new System.Drawing.Point(726, 165);
            this.btnFirstYRight.Name = "btnFirstYRight";
            this.btnFirstYRight.Size = new System.Drawing.Size(28, 23);
            this.btnFirstYRight.TabIndex = 26;
            this.btnFirstYRight.Text = ">";
            this.btnFirstYRight.UseVisualStyleBackColor = true;
            this.btnFirstYRight.Click += new System.EventHandler(this.btnFirstYRight_Click);
            // 
            // btnSecondYRight
            // 
            this.btnSecondYRight.Location = new System.Drawing.Point(726, 136);
            this.btnSecondYRight.Name = "btnSecondYRight";
            this.btnSecondYRight.Size = new System.Drawing.Size(28, 23);
            this.btnSecondYRight.TabIndex = 25;
            this.btnSecondYRight.Text = ">";
            this.btnSecondYRight.UseVisualStyleBackColor = true;
            this.btnSecondYRight.Click += new System.EventHandler(this.btnSecondYRight_Click);
            // 
            // btnThirdYLeft
            // 
            this.btnThirdYLeft.Location = new System.Drawing.Point(592, 107);
            this.btnThirdYLeft.Name = "btnThirdYLeft";
            this.btnThirdYLeft.Size = new System.Drawing.Size(28, 23);
            this.btnThirdYLeft.TabIndex = 24;
            this.btnThirdYLeft.Text = "<";
            this.btnThirdYLeft.UseVisualStyleBackColor = true;
            this.btnThirdYLeft.Click += new System.EventHandler(this.btnThirdYLeft_Click);
            // 
            // btnFirstYLeft
            // 
            this.btnFirstYLeft.Location = new System.Drawing.Point(592, 165);
            this.btnFirstYLeft.Name = "btnFirstYLeft";
            this.btnFirstYLeft.Size = new System.Drawing.Size(28, 23);
            this.btnFirstYLeft.TabIndex = 23;
            this.btnFirstYLeft.Text = "<";
            this.btnFirstYLeft.UseVisualStyleBackColor = true;
            this.btnFirstYLeft.Click += new System.EventHandler(this.btnFirstYLeft_Click);
            // 
            // btnFirstXDown
            // 
            this.btnFirstXDown.Location = new System.Drawing.Point(622, 193);
            this.btnFirstXDown.Name = "btnFirstXDown";
            this.btnFirstXDown.Size = new System.Drawing.Size(30, 23);
            this.btnFirstXDown.TabIndex = 22;
            this.btnFirstXDown.Text = "\\/";
            this.btnFirstXDown.UseVisualStyleBackColor = true;
            this.btnFirstXDown.Click += new System.EventHandler(this.btnFirstXDown_Click);
            // 
            // btnSecondYLeft
            // 
            this.btnSecondYLeft.Location = new System.Drawing.Point(592, 136);
            this.btnSecondYLeft.Name = "btnSecondYLeft";
            this.btnSecondYLeft.Size = new System.Drawing.Size(28, 23);
            this.btnSecondYLeft.TabIndex = 21;
            this.btnSecondYLeft.Text = "<";
            this.btnSecondYLeft.UseVisualStyleBackColor = true;
            this.btnSecondYLeft.Click += new System.EventHandler(this.btnSecondYLeft_Click);
            // 
            // btnThirdXUp
            // 
            this.btnThirdXUp.Location = new System.Drawing.Point(694, 78);
            this.btnThirdXUp.Name = "btnThirdXUp";
            this.btnThirdXUp.Size = new System.Drawing.Size(30, 23);
            this.btnThirdXUp.TabIndex = 20;
            this.btnThirdXUp.Text = "/\\";
            this.btnThirdXUp.UseVisualStyleBackColor = true;
            this.btnThirdXUp.Click += new System.EventHandler(this.btnThirdXUp_Click);
            // 
            // btnSecondXUp
            // 
            this.btnSecondXUp.Location = new System.Drawing.Point(658, 78);
            this.btnSecondXUp.Name = "btnSecondXUp";
            this.btnSecondXUp.Size = new System.Drawing.Size(30, 23);
            this.btnSecondXUp.TabIndex = 19;
            this.btnSecondXUp.Text = "/\\";
            this.btnSecondXUp.UseVisualStyleBackColor = true;
            this.btnSecondXUp.Click += new System.EventHandler(this.btnSecondXUp_Click);
            // 
            // btnFirstXUp
            // 
            this.btnFirstXUp.Location = new System.Drawing.Point(622, 78);
            this.btnFirstXUp.Name = "btnFirstXUp";
            this.btnFirstXUp.Size = new System.Drawing.Size(30, 23);
            this.btnFirstXUp.TabIndex = 18;
            this.btnFirstXUp.Text = "/\\";
            this.btnFirstXUp.UseVisualStyleBackColor = true;
            this.btnFirstXUp.Click += new System.EventHandler(this.btnFirstXUp_Click);
            // 
            // timerRepaint
            // 
            this.timerRepaint.Enabled = true;
            this.timerRepaint.Interval = 10;
            this.timerRepaint.Tick += new System.EventHandler(this.timerRepaint_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(127, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(28, 23);
            this.button1.TabIndex = 30;
            this.button1.Text = ">";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(55, 20);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(28, 23);
            this.button2.TabIndex = 31;
            this.button2.Text = "<";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // hScrollBar3
            // 
            this.hScrollBar3.Location = new System.Drawing.Point(52, 74);
            this.hScrollBar3.Maximum = 250;
            this.hScrollBar3.Minimum = 50;
            this.hScrollBar3.Name = "hScrollBar3";
            this.hScrollBar3.Size = new System.Drawing.Size(143, 17);
            this.hScrollBar3.TabIndex = 34;
            this.hScrollBar3.Value = 150;
            this.hScrollBar3.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarScroll);
            // 
            // hScrollBar2
            // 
            this.hScrollBar2.AccessibleName = "";
            this.hScrollBar2.Location = new System.Drawing.Point(52, 46);
            this.hScrollBar2.Maximum = 200;
            this.hScrollBar2.Name = "hScrollBar2";
            this.hScrollBar2.Size = new System.Drawing.Size(143, 17);
            this.hScrollBar2.TabIndex = 33;
            this.hScrollBar2.Tag = "";
            this.hScrollBar2.Value = 100;
            this.hScrollBar2.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarScroll);
            // 
            // hScrollBar4
            // 
            this.hScrollBar4.Location = new System.Drawing.Point(52, 101);
            this.hScrollBar4.Maximum = 350;
            this.hScrollBar4.Minimum = -100;
            this.hScrollBar4.Name = "hScrollBar4";
            this.hScrollBar4.Size = new System.Drawing.Size(143, 17);
            this.hScrollBar4.TabIndex = 32;
            this.hScrollBar4.Value = 250;
            this.hScrollBar4.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarScroll);
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.LargeChange = 2;
            this.hScrollBar1.Location = new System.Drawing.Point(52, 22);
            this.hScrollBar1.Maximum = 175;
            this.hScrollBar1.Minimum = 25;
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(143, 17);
            this.hScrollBar1.SmallChange = 2;
            this.hScrollBar1.TabIndex = 32;
            this.hScrollBar1.Value = 100;
            this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarScroll);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(5, 25);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(56, 17);
            this.radioButton1.TabIndex = 35;
            this.radioButton1.Text = "Mirrors";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButtonCheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(64, 25);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(51, 17);
            this.radioButton2.TabIndex = 35;
            this.radioButton2.Text = "Walls";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButtonCheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Checked = true;
            this.radioButton3.Location = new System.Drawing.Point(123, 25);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(71, 17);
            this.radioButton3.TabIndex = 35;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "CubeMap";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButtonCheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox2);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.hScrollBar2);
            this.groupBox1.Controls.Add(this.hScrollBar3);
            this.groupBox1.Controls.Add(this.hScrollBar4);
            this.groupBox1.Location = new System.Drawing.Point(572, 348);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 130);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Light Postion";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(85, 10);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(86, 17);
            this.checkBox1.TabIndex = 37;
            this.checkBox1.Text = "Light Source";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Z Axis";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "X Axis";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Y Axis";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButton1);
            this.groupBox2.Controls.Add(this.radioButton3);
            this.groupBox2.Controls.Add(this.radioButton2);
            this.groupBox2.Location = new System.Drawing.Point(572, 14);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 53);
            this.groupBox2.TabIndex = 37;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Room Options";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Location = new System.Drawing.Point(572, 226);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 54);
            this.groupBox3.TabIndex = 38;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Mirror/Surface Rotate";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Close";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(162, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Open";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(645, 117);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "RobikCube";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(641, 137);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Row/Column";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(655, 161);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(39, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "Rotate";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.hScrollBar1);
            this.groupBox4.Location = new System.Drawing.Point(572, 287);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(200, 54);
            this.groupBox4.TabIndex = 39;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "LookAt X Axis";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Location = new System.Drawing.Point(572, 484);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(200, 63);
            this.groupBox5.TabIndex = 40;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Keyboard Controll";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(2, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(199, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Press X,x,Y,y,Z,z to control the cubemap";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(196, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Press A,D,W,S to rotate the whole cube";
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(85, 27);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(108, 17);
            this.checkBox2.TabIndex = 38;
            this.checkBox2.Text = "Realistic Shadow";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnThirdXDown);
            this.Controls.Add(this.btnSecondXDown);
            this.Controls.Add(this.btnThirdYRight);
            this.Controls.Add(this.btnFirstYRight);
            this.Controls.Add(this.btnSecondYRight);
            this.Controls.Add(this.btnThirdYLeft);
            this.Controls.Add(this.btnFirstYLeft);
            this.Controls.Add(this.btnFirstXDown);
            this.Controls.Add(this.btnSecondYLeft);
            this.Controls.Add(this.btnThirdXUp);
            this.Controls.Add(this.btnSecondXUp);
            this.Controls.Add(this.btnFirstXUp);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "GraphiCube";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnThirdXDown;
        private System.Windows.Forms.Button btnSecondXDown;
        private System.Windows.Forms.Button btnThirdYRight;
        private System.Windows.Forms.Button btnFirstYRight;
        private System.Windows.Forms.Button btnSecondYRight;
        private System.Windows.Forms.Button btnThirdYLeft;
        private System.Windows.Forms.Button btnFirstYLeft;
        private System.Windows.Forms.Button btnFirstXDown;
        private System.Windows.Forms.Button btnSecondYLeft;
        private System.Windows.Forms.Button btnThirdXUp;
        private System.Windows.Forms.Button btnSecondXUp;
        private System.Windows.Forms.Button btnFirstXUp;
        private System.Windows.Forms.Timer timerRepaint;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.HScrollBar hScrollBar3;
        private System.Windows.Forms.HScrollBar hScrollBar2;
        private System.Windows.Forms.HScrollBar hScrollBar4;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
    }
}

