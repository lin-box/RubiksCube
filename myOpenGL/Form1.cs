using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenGL;
using System.Runtime.InteropServices;
//3D model b1
using Milkshape;
//3D model e
using RubikCube;

namespace myOpenGL
{
    public partial class Form1 : Form
    {
        cOGL cGL;

        public Form1()
        {
            InitializeComponent();
            cGL = new cOGL(panel1);

            radioButtonCheckedChanged(radioButton1, null);

            //apply the bars values as cGL.ScrollValue[..] properties 
            //!!!
            hScrollBarScroll(hScrollBar1, null);


            // initialize ScrollValue array

            cGL.ScrollValue[1] = (hScrollBar2.Value - 100) / 10.0f;
            cGL.ScrollValue[2] = (hScrollBar3.Value - 100) / 10.0f;
            cGL.ScrollValue[3] = (hScrollBar4.Value - 100) / 10.0f;
        }

        // happens when the window is re-rendered.
        // for example when another window is hiding it and it gets clear and then should be re-paint.
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            cGL.Draw();
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            cGL.OnResize();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D)
            {
                cGL.rubiksCube.Rotate(0, 5, 0);
            }
            else if (e.KeyCode == Keys.A)
            {
                cGL.rubiksCube.Rotate(0, -5, 0);
            }
            else if (e.KeyCode == Keys.W)
            {
                cGL.rubiksCube.Rotate(-5, 0, 0);
            }
            else if (e.KeyCode == Keys.S)
            {
                cGL.rubiksCube.Rotate(5, 0, 0);
            }
        }

        private void btnFirstXUp_Click(object sender, EventArgs e)
        {
            RubikCubeMoviment moviment = new RubikCubeMoviment(Depth.First, Spin.Clockwise, Axis.X);
            cGL.rubiksCube.Manipulate(moviment);
        }

        private void btnSecondXUp_Click(object sender, EventArgs e)
        {
            RubikCubeMoviment moviment = new RubikCubeMoviment(Depth.Second, Spin.Clockwise, Axis.X);
            cGL.rubiksCube.Manipulate(moviment);
        }

        private void btnThirdXUp_Click(object sender, EventArgs e)
        {
            RubikCubeMoviment moviment = new RubikCubeMoviment(Depth.Third, Spin.Clockwise, Axis.X);
            cGL.rubiksCube.Manipulate(moviment);
        }

        private void btnFirstYRight_Click(object sender, EventArgs e)
        {
            RubikCubeMoviment moviment = new RubikCubeMoviment(Depth.Third, Spin.Anticlockwise, Axis.Y);
            cGL.rubiksCube.Manipulate(moviment);
        }

        private void btnSecondYRight_Click(object sender, EventArgs e)
        {
            RubikCubeMoviment moviment = new RubikCubeMoviment(Depth.Second, Spin.Anticlockwise, Axis.Y);
            cGL.rubiksCube.Manipulate(moviment);
        }

        private void btnThirdYRight_Click(object sender, EventArgs e)
        {
            RubikCubeMoviment moviment = new RubikCubeMoviment(Depth.First, Spin.Anticlockwise, Axis.Y);
            cGL.rubiksCube.Manipulate(moviment);
        }

        private void btnThirdXDown_Click(object sender, EventArgs e)
        {
            RubikCubeMoviment moviment = new RubikCubeMoviment(Depth.Third, Spin.Anticlockwise, Axis.X);
            cGL.rubiksCube.Manipulate(moviment);
        }

        private void btnSecondXDown_Click(object sender, EventArgs e)
        {
            RubikCubeMoviment moviment = new RubikCubeMoviment(Depth.Second, Spin.Anticlockwise, Axis.X);
            cGL.rubiksCube.Manipulate(moviment);
        }

        private void btnFirstXDown_Click(object sender, EventArgs e)
        {
            RubikCubeMoviment moviment = new RubikCubeMoviment(Depth.First, Spin.Anticlockwise, Axis.X);
            cGL.rubiksCube.Manipulate(moviment);
        }

        private void btnThirdYLeft_Click(object sender, EventArgs e)
        {
            RubikCubeMoviment moviment = new RubikCubeMoviment(Depth.First, Spin.Clockwise, Axis.Y);
            cGL.rubiksCube.Manipulate(moviment);
        }

        private void btnSecondYLeft_Click(object sender, EventArgs e)
        {
            RubikCubeMoviment moviment = new RubikCubeMoviment(Depth.Second, Spin.Clockwise, Axis.Y);
            cGL.rubiksCube.Manipulate(moviment);
        }

        private void btnFirstYLeft_Click(object sender, EventArgs e)
        {
            RubikCubeMoviment moviment = new RubikCubeMoviment(Depth.Third, Spin.Clockwise, Axis.Y);
            cGL.rubiksCube.Manipulate(moviment);
        }

        private void timerRepaint_Tick(object sender, EventArgs e)
        {
            Console.WriteLine("Timer Draw!");
            cGL.Draw();
        }

        private void button1_Click(object sender, EventArgs e) //open
        {
            if (cGL.rightMirrorSurface.AngleY < -75 )
            {
                cGL.rightMirrorSurface.Rotate(0, 5, 0);
                cGL.leftMirrorSurface.Rotate(0, -5, 0);
            }
        }

        private void button2_Click(object sender, EventArgs e) //close
        {
            if (cGL.rightMirrorSurface.AngleY > -90)
            {
                cGL.rightMirrorSurface.Rotate(0, -5, 0);
                cGL.leftMirrorSurface.Rotate(0, 5, 0);
            }     
        }

        private void hScrollBarScroll(object sender, ScrollEventArgs e)
        {
            HScrollBar hb = (HScrollBar)sender;
            int n = int.Parse(hb.Name.Substring(10));
            cGL.ScrollValue[n - 1] = (hb.Value - 100) / 10.0f;
            if (e != null)
                cGL.Draw();
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (cGL.mode)
            {
                case 0: // mirror state
                    break;
                case 1: // shadow state
                    break;
                case 2: // room state
                    switch (e.KeyChar)
                    {
                        case 'x':
                            cGL.cubemapXYZAngles[0] -= 5;
                            cGL.rubiksCube.Rotate(-5, 0, 0);
                            cGL.Draw();
                            break;
                        case 'X':
                            cGL.cubemapXYZAngles[0] += 5;
                            cGL.rubiksCube.Rotate(5, 0, 0);
                            cGL.Draw();
                            break;
                        case 'y':
                            cGL.cubemapXYZAngles[1] -= 5;
                            cGL.rubiksCube.Rotate(0, -5, 0);
                            cGL.Draw();
                            break;
                        case 'Y':
                            cGL.cubemapXYZAngles[1] += 5;
                            cGL.rubiksCube.Rotate(0, 5, 0);
                            cGL.Draw();
                            break;
                        case 'z':
                            cGL.cubemapXYZAngles[2] -= 5;
                            cGL.rubiksCube.Rotate(0, 0, -5);
                            cGL.Draw();
                            break;
                        case 'Z':
                            cGL.cubemapXYZAngles[2] += 5;
                            cGL.rubiksCube.Rotate(0, 0, 5);
                            cGL.Draw();
                            break;
                    }
                    break;
            }
        }

        private void radioButtonCheckedChanged(object sender, EventArgs e)
        {
            RadioButton rd = (RadioButton)sender;
            int n = int.Parse(rd.Name.Substring(11));
            cGL.radioButtonChecked[n - 1] = rd.Checked;
            if (n == 1) // mirrors state
            {
                groupBox1.Enabled = false;  //light
                groupBox3.Enabled = true;   //surface rotate
                groupBox4.Enabled = true;  //LookAt X Axis
                label5.Enabled = false;     // cubemap rotations
            }
            else if (n == 2) // shadow state
            {
                groupBox1.Enabled = true;   //light
                groupBox3.Enabled = true;   //surface rotate
                groupBox4.Enabled = true;  //LookAt X Axis
                label5.Enabled = false;     // cubemap rotations
            }
            else // room state
            {
                groupBox1.Enabled = false;  //light
                groupBox3.Enabled = false;  //surface rotate
                groupBox4.Enabled = false;  //LookAt X Axis
                label5.Enabled = true;     // cubemap rotations
            }

            if (e != null)
                cGL.Draw();
        }

    }
}