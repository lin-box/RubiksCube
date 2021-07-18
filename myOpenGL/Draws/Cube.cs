using OpenGL;
using System.Diagnostics;
using System.Drawing;

namespace RubikCube.Draws
{
    class Cube  : IDraw
    {
        private double size;
        public double X { get; private set; }
        public double Y { get; private set; }
        public double Z { get; private set; }

        public FaceCube<Color> faceColors;

        private int AngleX = 0;
        private int AngleY = 0;
        private int AngleZ = 0;

        public Cube(double size, double X, double Y, double Z)
        {
            //GL.glDisable(GL.GL_LIGHTING);
            this.size = size;
            this.X = X;
            this.Y = Y;
            this.Z = Z;
            this.faceColors = new FaceCube<Color>(Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black);
        }

        public Cube(double size, double X, double Y, double Z, FaceCube<Color> faceColors)
        {
            //GL.glDisable(GL.GL_LIGHTING);
            this.size = size;
            this.X = X;
            this.Y = Y;
            this.Z = Z;
            this.faceColors = faceColors;
        }

        //public Cube(double size, double X, double Y, double Z, FaceCube<Texture> faceColors)
        //{
        //    this.size = size;
        //    this.X = X;
        //    this.Y = Y;
        //    this.Z = Z;
        //    //this.faceColors = faceColors;
        //}

        public void Rotate(int AngleAxisX, int AngleAxisY, int AngleAxisZ)
        {
            this.AngleX += AngleAxisX;
            this.AngleY += AngleAxisY;
            this.AngleZ += AngleAxisZ;
        }

        double[] calcNormal(double[] v1, double[] v2, double[] v3)
        {
            double[] v2_v1 = { v2[0] - v1[0], v2[1] - v1[1], v2[2] - v1[2] };
            double[] v3_v1 = { v3[0] - v1[0], v3[1] - v1[1], v3[2] - v1[2] };

            double[] i = { v2_v1[0], v3_v1[0] };
            double[] j = { v2_v1[1], v3_v1[1] };
            double[] k = { v2_v1[2], v3_v1[2] };

            double[] normal = new double[3];
            normal[0] = j[0] * k[1] - j[1] * k[0];
            normal[1] = i[0] * k[1] - i[1] * k[0];
            normal[2] = i[0] * j[1] - i[1] * j[0];

            return normal;
        }

        public void Draw()
        {
            GL.glPushMatrix();
            GL.glRotatef(AngleX, 1, 0, 0);
            GL.glRotatef(AngleY, 0, 1, 0);
            GL.glRotatef(AngleZ, 0, 0, 1);

            GL.glBegin(GL.GL_QUADS);

            double[] v1 = { -this.size + X, +this.size + Y, -this.size + Z };
            double[] v2 = { +this.size + X, +this.size + Y, -this.size + Z };
            double[] v3 = { +this.size + X, +this.size + Y, +this.size + Z };
            double[] v4 = { -this.size + X, +this.size + Y, +this.size + Z };
            double[] v5 = { -this.size + X, -this.size + Y, +this.size + Z };
            double[] v6 = { -this.size + X, -this.size + Y, -this.size + Z };
            double[] v7 = { +this.size + X, -this.size + Y, -this.size + Z };
            double[] v8 = { +this.size + X, -this.size + Y, +this.size + Z };

            byte a = 50;

            //Back
            GL.glNormal3d((calcNormal(v1, v2, v6))[0], (calcNormal(v1, v2, v6))[1], (calcNormal(v1, v2, v6))[2]);
            //GL.glColor3ub(faceColors.Back.R, faceColors.Back.G, faceColors.Back.B);
            GL.glColor4ub(faceColors.Back.R, faceColors.Back.G, faceColors.Back.B, a);
            GL.glVertex3d(v1[0], v1[1], v1[2]);
            GL.glVertex3d(v2[0], v2[1], v2[2]);
            GL.glVertex3d(v7[0], v7[1], v7[2]);
            GL.glVertex3d(v6[0], v6[1], v6[2]);

            //Bottom
            GL.glNormal3d(-calcNormal(v8, v5, v7)[0], -calcNormal(v8, v5, v7)[1], -calcNormal(v8, v5, v7)[2]);
            //GL.glColor3ub(faceColors.Bottom.R, faceColors.Bottom.G, faceColors.Bottom.B);
            GL.glColor4ub(faceColors.Bottom.R, faceColors.Bottom.G, faceColors.Bottom.B, a);
            GL.glVertex3d(v6[0], v6[1], v6[2]);
            GL.glVertex3d(v7[0], v7[1], v7[2]);
            GL.glVertex3d(v8[0], v8[1], v8[2]);
            GL.glVertex3d(v5[0], v5[1], v5[2]);

            //Left
            GL.glNormal3d(calcNormal(v4, v1, v5)[0], calcNormal(v4, v1, v5)[1], calcNormal(v4, v1, v5)[2]);
            //GL.glColor3ub(faceColors.Left.R, faceColors.Left.G, faceColors.Left.B);
            GL.glColor4ub(faceColors.Left.R, faceColors.Left.G, faceColors.Left.B, a);
            GL.glVertex3d(v1[0], v1[1], v1[2]);
            GL.glVertex3d(v4[0], v4[1], v4[2]);
            GL.glVertex3d(v5[0], v5[1], v5[2]);
            GL.glVertex3d(v6[0], v6[1], v6[2]);

            //Right
            GL.glNormal3d(calcNormal(v3, v8, v2)[0], calcNormal(v3, v8, v2)[1], calcNormal(v3, v8, v2)[2]);
            //GL.glColor3ub(faceColors.Right.R, faceColors.Right.G, faceColors.Right.B);
            GL.glColor4ub(faceColors.Right.R, faceColors.Right.G, faceColors.Right.B, a);
            GL.glVertex3d(v2[0], v2[1], v2[2]);
            GL.glVertex3d(v3[0], v3[1], v3[2]);
            GL.glVertex3d(v8[0], v8[1], v8[2]);
            GL.glVertex3d(v7[0], v7[1], v7[2]);

            //Top
            GL.glNormal3d(-calcNormal(v4, v3, v1)[0], -calcNormal(v4, v3, v1)[1], -calcNormal(v4, v3, v1)[2]);
            //GL.glColor3ub(faceColors.Top.R, faceColors.Top.G, faceColors.Top.B);
            GL.glColor4ub(faceColors.Top.R, faceColors.Top.G, faceColors.Top.B, a);
            GL.glVertex3d(v1[0], v1[1], v1[2]);
            GL.glVertex3d(v2[0], v2[1], v2[2]);
            GL.glVertex3d(v3[0], v3[1], v3[2]);
            GL.glVertex3d(v4[0], v4[1], v4[2]);

            //Front
            GL.glNormal3d(calcNormal(v8, v3, v5)[0], calcNormal(v8, v3, v5)[1], calcNormal(v8, v3, v5)[2]);
            //GL.glColor3ub(faceColors.Front.R, faceColors.Front.G, faceColors.Front.B);
            GL.glColor4ub(faceColors.Front.R, faceColors.Front.G, faceColors.Front.B, a);
            GL.glVertex3d(v4[0], v4[1], v4[2]);
            GL.glVertex3d(v3[0], v3[1], v3[2]);
            GL.glVertex3d(v8[0], v8[1], v8[2]);
            GL.glVertex3d(v5[0], v5[1], v5[2]);

            GL.glEnd();

            GL.glPopMatrix();
        }

        public void Place(RubikCubeMoviment moviment)
        {
            this.faceColors.Rotate(moviment);
            this.Transform(moviment);
            this.ResetTransforms();
        }

        private void Transform(RubikCubeMoviment moviment)
        {
            if (moviment.Axis == Axis.X)
            {
                if (moviment.Spin == Spin.Clockwise) //Up
                {
                    //Edges
                    if (this.Y < 0 && this.Z < 0)
                    {
                        this.Z = -this.Z;
                    }
                    else if (this.Y < 0 && this.Z > 0)
                    {
                        this.Y = -this.Y;
                    }
                    else if (this.Y > 0 && this.Z < 0)
                    {
                        this.Y = -this.Y;
                    }
                    else if (this.Y > 0 && this.Z > 0)
                    {
                        this.Z = -this.Z;
                    }

                    //Middles
                    else if (this.Y == 0 && this.Z < 0)
                    {
                        this.Y = this.Z;
                        this.Z = 0;
                    }
                    else if (this.Y == 0 && this.Z > 0)
                    {
                        this.Y = this.Z;
                        this.Z = 0;
                    }
                    else if (this.Z == 0 && this.Y < 0)
                    {
                        this.Z = -this.Y;
                        this.Y = 0;
                    }
                    else if (this.Z == 0 && this.Y > 0)
                    {
                        this.Z = -this.Y;
                        this.Y = 0;
                    }
                }
                else //Down
                {
                    //Edges
                    if (this.Y < 0 && this.Z < 0)
                    {
                        this.Y = -this.Y;
                    }
                    else if (this.Y < 0 && this.Z > 0)
                    {
                        this.Z = -this.Z;
                    }
                    else if (this.Y > 0 && this.Z < 0)
                    {
                        this.Z = -this.Z;
                    }
                    else if (this.Y > 0 && this.Z > 0)
                    {
                        this.Y = -this.Y;
                    }

                    //Middles
                    else if (this.Y == 0 && this.Z < 0)
                    {
                        this.Y = -this.Z;
                        this.Z = 0;
                    }
                    else if (this.Y == 0 && this.Z > 0)
                    {
                        this.Y = -this.Z;
                        this.Z = 0;
                    }
                    else if (this.Z == 0 && this.Y < 0)
                    {
                        this.Z = this.Y;
                        this.Y = 0;
                    }
                    else if (this.Z == 0 && this.Y > 0)
                    {
                        this.Z = this.Y;
                        this.Y = 0;
                    }
                }
            }

            if (moviment.Axis == Axis.Y)
            {
                if (moviment.Spin == Spin.Clockwise) //Left
                {
                    if (this.X < 0 && this.Z < 0)
                    {
                        this.X = -this.X;
                    }
                    else if (this.X < 0 && this.Z > 0)
                    {
                        this.Z = -this.Z;
                    }
                    else if (this.X > 0 && this.Z < 0)
                    {
                        this.Z = -this.Z;
                    }
                    else if (this.X > 0 && this.Z > 0)
                    {
                        this.X = -this.X;
                    }

                    //Middle
                    else if (this.X == 0 && this.Z < 0)
                    {
                        this.X = -this.Z;
                        this.Z = 0;
                    }
                    else if (this.X == 0 && this.Z > 0)
                    {
                        this.X = -this.Z;
                        this.Z = 0;
                    }
                    else if (this.Z == 0 && this.X < 0)
                    {
                        this.Z = this.X;
                        this.X = 0;
                    }
                    else if (this.Z == 0 && this.X > 0)
                    {
                        this.Z = this.X;
                        this.X = 0;
                    }
                }
                else
                {
                    if (this.X < 0 && this.Z < 0)
                    {
                        this.Z = -this.Z;
                    }
                    else if (this.X < 0 && this.Z > 0)
                    {
                        this.X = -this.X;
                    }
                    else if (this.X > 0 && this.Z < 0)
                    {
                        this.X = -this.X;
                    }
                    else if (this.X > 0 && this.Z > 0)
                    {
                        this.Z = -this.Z;
                    }

                    //Middle
                    else if (this.X == 0 && this.Z < 0)
                    {
                        this.X = this.Z;
                        this.Z = 0;
                    }
                    else if (this.X == 0 && this.Z > 0)
                    {
                        this.X = this.Z;
                        this.Z = 0;
                    }
                    else if (this.Z == 0 && this.X < 0)
                    {
                        this.Z = -this.X;
                        this.X = 0;
                    }
                    else if (this.Z == 0 && this.X > 0)
                    {
                        this.Z = -this.X;
                        this.X = 0;
                    }
                }
            }

            if (moviment.Axis == Axis.Z)
            {
                if (moviment.Spin == Spin.Clockwise)
                {
                    if (this.X < 0 && this.Y < 0)
                    {
                        this.Y = -this.Y;
                    }
                    else if (this.X < 0 && this.Y > 0)
                    {
                        this.X = -this.X;
                    }
                    else if (this.X > 0 && this.Y < 0)
                    {
                        this.X = -this.X;
                    }
                    else if (this.X > 0 && this.Y > 0)
                    {
                        this.Y = -this.Y;
                    }

                    //Middle
                    else if (this.X == 0 && this.Y < 0)
                    {
                        this.X = this.Y;
                        this.Y = 0;
                    }
                    else if (this.X == 0 && this.Y > 0)
                    {
                        this.X = this.Z;
                        this.Y = 0;
                    }
                    else if (this.Y == 0 && this.X < 0)
                    {
                        this.Y = -this.X;
                        this.X = 0;
                    }
                    else if (this.Y == 0 && this.X > 0)
                    {
                        this.Y = -this.X;
                        this.X = 0;
                    }
                }
                else
                {
                    if (this.X < 0 && this.Y < 0)
                    {
                        this.X = -this.X;
                    }
                    else if (this.X < 0 && this.Y > 0)
                    {
                        this.Y = -this.Y;
                    }
                    else if (this.X > 0 && this.Y < 0)
                    {
                        this.Y = -this.Y;
                    }
                    else if (this.X > 0 && this.Y > 0)
                    {
                        this.X = -this.X;
                    }

                    //Middle
                    else if (this.X == 0 && this.Y < 0)
                    {
                        this.X = -this.Y;
                        this.Y = 0;
                    }
                    else if (this.X == 0 && this.Y > 0)
                    {
                        this.X = -this.Z;
                        this.Y = 0;
                    }
                    else if (this.Y == 0 && this.X < 0)
                    {
                        this.Y = this.X;
                        this.X = 0;
                    }
                    else if (this.Y == 0 && this.X > 0)
                    {
                        this.Y = this.X;
                        this.X = 0;
                    }
                }
            }

            this.ResetTransforms();
        }

        private void ResetTransforms()
        {
            this.AngleX = 0;
            this.AngleY = 0;
            this.AngleZ = 0;
        }
    }
}
