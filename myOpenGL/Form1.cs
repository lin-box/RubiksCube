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
        }


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


        private void btnFirstXUp_Click(object sender, EventArgs e)
        {
            Console.WriteLine("hei");
            RubikCubeMoviment moviment = new RubikCubeMoviment(Depth.First, Spin.Clockwise, Axis.X);
            cGL.Manipulate(moviment);
        }

        private void btnSecondXUp_Click(object sender, EventArgs e)
        {
            RubikCubeMoviment moviment = new RubikCubeMoviment(Depth.Second, Spin.Clockwise, Axis.X);
            cGL.Manipulate(moviment);
        }

        private void btnThirdXUp_Click(object sender, EventArgs e)
        {
            RubikCubeMoviment moviment = new RubikCubeMoviment(Depth.Third, Spin.Clockwise, Axis.X);
            cGL.Manipulate(moviment);
        }

        private void btnFirstYRight_Click(object sender, EventArgs e)
        {
            RubikCubeMoviment moviment = new RubikCubeMoviment(Depth.First, Spin.Anticlockwise, Axis.Y);
            cGL.Manipulate(moviment);
        }

        private void btnSecondYRight_Click(object sender, EventArgs e)
        {
            RubikCubeMoviment moviment = new RubikCubeMoviment(Depth.Second, Spin.Anticlockwise, Axis.Y);
            cGL.Manipulate(moviment);
        }

        private void btnThirdYRight_Click(object sender, EventArgs e)
        {
            RubikCubeMoviment moviment = new RubikCubeMoviment(Depth.Third, Spin.Anticlockwise, Axis.Y);
            cGL.Manipulate(moviment);
        }

        private void btnThirdXDown_Click(object sender, EventArgs e)
        {
            RubikCubeMoviment moviment = new RubikCubeMoviment(Depth.Third, Spin.Anticlockwise, Axis.X);
            cGL.Manipulate(moviment);
        }

        private void btnSecondXDown_Click(object sender, EventArgs e)
        {
            RubikCubeMoviment moviment = new RubikCubeMoviment(Depth.Second, Spin.Anticlockwise, Axis.X);
            cGL.Manipulate(moviment);
        }

        private void btnFirstXDown_Click(object sender, EventArgs e)
        {
            RubikCubeMoviment moviment = new RubikCubeMoviment(Depth.First, Spin.Anticlockwise, Axis.X);
            cGL.Manipulate(moviment);
        }

        private void btnThirdYLeft_Click(object sender, EventArgs e)
        {
            RubikCubeMoviment moviment = new RubikCubeMoviment(Depth.Third, Spin.Clockwise, Axis.Y);
            cGL.Manipulate(moviment);
        }

        private void btnSecondYLeft_Click(object sender, EventArgs e)
        {
            RubikCubeMoviment moviment = new RubikCubeMoviment(Depth.Second, Spin.Clockwise, Axis.Y);
            cGL.Manipulate(moviment);
        }

        private void btnFirstYLeft_Click(object sender, EventArgs e)
        {
            RubikCubeMoviment moviment = new RubikCubeMoviment(Depth.First, Spin.Clockwise, Axis.Y);
            cGL.Manipulate(moviment);
        }
    }
}