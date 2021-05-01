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
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel1.Location = new System.Drawing.Point(12, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(515, 500);
            this.panel1.TabIndex = 6;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.Resize += new System.EventHandler(this.panel1_Resize);
            // 
            // btnThirdXDown
            // 
            this.btnThirdXDown.Location = new System.Drawing.Point(658, 302);
            this.btnThirdXDown.Name = "btnThirdXDown";
            this.btnThirdXDown.Size = new System.Drawing.Size(30, 23);
            this.btnThirdXDown.TabIndex = 29;
            this.btnThirdXDown.Text = "\\/";
            this.btnThirdXDown.UseVisualStyleBackColor = true;
            this.btnThirdXDown.Click += new System.EventHandler(this.btnThirdXDown_Click);
            // 
            // btnSecondXDown
            // 
            this.btnSecondXDown.Location = new System.Drawing.Point(622, 302);
            this.btnSecondXDown.Name = "btnSecondXDown";
            this.btnSecondXDown.Size = new System.Drawing.Size(30, 23);
            this.btnSecondXDown.TabIndex = 28;
            this.btnSecondXDown.Text = "\\/";
            this.btnSecondXDown.UseVisualStyleBackColor = true;
            this.btnSecondXDown.Click += new System.EventHandler(this.btnSecondXDown_Click);
            // 
            // btnThirdYRight
            // 
            this.btnThirdYRight.Location = new System.Drawing.Point(690, 215);
            this.btnThirdYRight.Name = "btnThirdYRight";
            this.btnThirdYRight.Size = new System.Drawing.Size(28, 23);
            this.btnThirdYRight.TabIndex = 27;
            this.btnThirdYRight.Text = ">";
            this.btnThirdYRight.UseVisualStyleBackColor = true;
            this.btnThirdYRight.Click += new System.EventHandler(this.btnThirdYRight_Click);
            // 
            // btnFirstYRight
            // 
            this.btnFirstYRight.Location = new System.Drawing.Point(690, 273);
            this.btnFirstYRight.Name = "btnFirstYRight";
            this.btnFirstYRight.Size = new System.Drawing.Size(28, 23);
            this.btnFirstYRight.TabIndex = 26;
            this.btnFirstYRight.Text = ">";
            this.btnFirstYRight.UseVisualStyleBackColor = true;
            this.btnFirstYRight.Click += new System.EventHandler(this.btnFirstYRight_Click);
            // 
            // btnSecondYRight
            // 
            this.btnSecondYRight.Location = new System.Drawing.Point(690, 244);
            this.btnSecondYRight.Name = "btnSecondYRight";
            this.btnSecondYRight.Size = new System.Drawing.Size(28, 23);
            this.btnSecondYRight.TabIndex = 25;
            this.btnSecondYRight.Text = ">";
            this.btnSecondYRight.UseVisualStyleBackColor = true;
            this.btnSecondYRight.Click += new System.EventHandler(this.btnSecondYRight_Click);
            // 
            // btnThirdYLeft
            // 
            this.btnThirdYLeft.Location = new System.Drawing.Point(556, 215);
            this.btnThirdYLeft.Name = "btnThirdYLeft";
            this.btnThirdYLeft.Size = new System.Drawing.Size(28, 23);
            this.btnThirdYLeft.TabIndex = 24;
            this.btnThirdYLeft.Text = "<";
            this.btnThirdYLeft.UseVisualStyleBackColor = true;
            this.btnThirdYLeft.Click += new System.EventHandler(this.btnThirdYLeft_Click);
            // 
            // btnFirstYLeft
            // 
            this.btnFirstYLeft.Location = new System.Drawing.Point(556, 273);
            this.btnFirstYLeft.Name = "btnFirstYLeft";
            this.btnFirstYLeft.Size = new System.Drawing.Size(28, 23);
            this.btnFirstYLeft.TabIndex = 23;
            this.btnFirstYLeft.Text = "<";
            this.btnFirstYLeft.UseVisualStyleBackColor = true;
            this.btnFirstYLeft.Click += new System.EventHandler(this.btnFirstYLeft_Click);
            // 
            // btnFirstXDown
            // 
            this.btnFirstXDown.Location = new System.Drawing.Point(586, 302);
            this.btnFirstXDown.Name = "btnFirstXDown";
            this.btnFirstXDown.Size = new System.Drawing.Size(30, 23);
            this.btnFirstXDown.TabIndex = 22;
            this.btnFirstXDown.Text = "\\/";
            this.btnFirstXDown.UseVisualStyleBackColor = true;
            this.btnFirstXDown.Click += new System.EventHandler(this.btnFirstXDown_Click);
            // 
            // btnSecondYLeft
            // 
            this.btnSecondYLeft.Location = new System.Drawing.Point(556, 244);
            this.btnSecondYLeft.Name = "btnSecondYLeft";
            this.btnSecondYLeft.Size = new System.Drawing.Size(28, 23);
            this.btnSecondYLeft.TabIndex = 21;
            this.btnSecondYLeft.Text = "<";
            this.btnSecondYLeft.UseVisualStyleBackColor = true;
            this.btnSecondYLeft.Click += new System.EventHandler(this.btnSecondYLeft_Click);
            // 
            // btnThirdXUp
            // 
            this.btnThirdXUp.Location = new System.Drawing.Point(658, 186);
            this.btnThirdXUp.Name = "btnThirdXUp";
            this.btnThirdXUp.Size = new System.Drawing.Size(30, 23);
            this.btnThirdXUp.TabIndex = 20;
            this.btnThirdXUp.Text = "/\\";
            this.btnThirdXUp.UseVisualStyleBackColor = true;
            this.btnThirdXUp.Click += new System.EventHandler(this.btnThirdXUp_Click);
            // 
            // btnSecondXUp
            // 
            this.btnSecondXUp.Location = new System.Drawing.Point(622, 186);
            this.btnSecondXUp.Name = "btnSecondXUp";
            this.btnSecondXUp.Size = new System.Drawing.Size(30, 23);
            this.btnSecondXUp.TabIndex = 19;
            this.btnSecondXUp.Text = "/\\";
            this.btnSecondXUp.UseVisualStyleBackColor = true;
            this.btnSecondXUp.Click += new System.EventHandler(this.btnSecondXUp_Click);
            // 
            // btnFirstXUp
            // 
            this.btnFirstXUp.Location = new System.Drawing.Point(586, 186);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 528);
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
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.ResumeLayout(false);

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
    }
}

