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
            else if (e.KeyCode == Keys.I)
            {
                cGL.rubiksCube.Rotate(5, 0, 0);
            }
            else if (e.KeyCode == Keys.O)
            {
                cGL.rubiksCube.Rotate(5, 0, 0);
            }
            cGL.Draw();
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
            RubikCubeMoviment moviment = new RubikCubeMoviment(Depth.First, Spin.Anticlockwise, Axis.Y);
            cGL.rubiksCube.Manipulate(moviment);
        }

        private void btnSecondYRight_Click(object sender, EventArgs e)
        {
            RubikCubeMoviment moviment = new RubikCubeMoviment(Depth.Second, Spin.Anticlockwise, Axis.Y);
            cGL.rubiksCube.Manipulate(moviment);
        }

        private void btnThirdYRight_Click(object sender, EventArgs e)
        {
            RubikCubeMoviment moviment = new RubikCubeMoviment(Depth.Third, Spin.Anticlockwise, Axis.Y);
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
            RubikCubeMoviment moviment = new RubikCubeMoviment(Depth.Third, Spin.Clockwise, Axis.Y);
            cGL.rubiksCube.Manipulate(moviment);
        }

        private void btnSecondYLeft_Click(object sender, EventArgs e)
        {
            RubikCubeMoviment moviment = new RubikCubeMoviment(Depth.Second, Spin.Clockwise, Axis.Y);
            cGL.rubiksCube.Manipulate(moviment);
        }

        private void btnFirstYLeft_Click(object sender, EventArgs e)
        {
            RubikCubeMoviment moviment = new RubikCubeMoviment(Depth.First, Spin.Clockwise, Axis.Y);
            cGL.rubiksCube.Manipulate(moviment);
        }
    }
}