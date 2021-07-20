using myOpenGL;
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
        public FaceCube<bool> isInsideFaceColors;
        public bool isShadowState;

        private int AngleX = 0;
        private int AngleY = 0;
        private int AngleZ = 0;

        public Cube(double size, double X, double Y, double Z, FaceCube<bool> isInsideFaceColors, bool isShadowState = false)
        {
            this.size = size;
            this.X = X;
            this.Y = Y;
            this.Z = Z;
            this.faceColors = new FaceCube<Color>(Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black);
            this.isShadowState = isShadowState;
            this.isInsideFaceColors = isInsideFaceColors;
        }

        public Cube(double size, double X, double Y, double Z, FaceCube<Color> faceColors, FaceCube<bool> isInsideFaceColors, bool isShadowState = false)
        {
            this.size = size;
            this.X = X;
            this.Y = Y;
            this.Z = Z;
            this.faceColors = faceColors;
            this.isShadowState = isShadowState;
            this.isInsideFaceColors = isInsideFaceColors;
        }

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

        public byte CheckIsInsideColor(bool isInsideFaceColor, byte totalBlend, byte halfBlend)
        {
            // totalBlend = halfBlend; // Enable this if you **DO** want to draw the inside faces of the shadow cube (drawing with blend != 0)

            if (isInsideFaceColor == true)
            {
                return totalBlend; // Completely transparent
            }
            else
            {
                return halfBlend;  // Transparent as a shadow
            }
        }

        public void DrawShadowColor(double[] v1, double[] v2, double[] v3, double[] v4, double[] v5, double[] v6, double[] v7, double[] v8, byte totalBlend, byte halfBlend)
        {
            GL.glBegin(GL.GL_QUADS);

            byte a = 255; // Zero transparency

            //Back
            GL.glNormal3d((calcNormal(v1, v2, v6))[0], (calcNormal(v1, v2, v6))[1], (calcNormal(v1, v2, v6))[2]);
            a = CheckIsInsideColor(isInsideFaceColors.Back, totalBlend, halfBlend);
            // GL.glColor4ub(faceColors.Back.R, faceColors.Back.G, faceColors.Back.B, a);       // Enable this for colorfull shadow
            GL.glColor4ub(Color.Black.R, Color.Black.G, Color.Black.B, a);                      // Disable this for colorfull shadow // Define the solid color
            GL.glVertex3d(v1[0], v1[1], v1[2]);
            GL.glVertex3d(v2[0], v2[1], v2[2]);
            GL.glVertex3d(v7[0], v7[1], v7[2]);
            GL.glVertex3d(v6[0], v6[1], v6[2]);

            //Bottom
            GL.glNormal3d(-calcNormal(v8, v5, v7)[0], -calcNormal(v8, v5, v7)[1], -calcNormal(v8, v5, v7)[2]);
            a = CheckIsInsideColor(isInsideFaceColors.Bottom, totalBlend, halfBlend);
            // GL.glColor4ub(faceColors.Bottom.R, faceColors.Bottom.G, faceColors.Bottom.B, a); // Enable this for colorfull shadow
            GL.glColor4ub(Color.Black.R, Color.Black.G, Color.Black.B, a);                      // Disable this for colorfull shadow // Define the solid color
            GL.glVertex3d(v6[0], v6[1], v6[2]);
            GL.glVertex3d(v7[0], v7[1], v7[2]);
            GL.glVertex3d(v8[0], v8[1], v8[2]);
            GL.glVertex3d(v5[0], v5[1], v5[2]);

            //Left
            GL.glNormal3d(calcNormal(v4, v1, v5)[0], calcNormal(v4, v1, v5)[1], calcNormal(v4, v1, v5)[2]);
            a = CheckIsInsideColor(isInsideFaceColors.Left, totalBlend, halfBlend);
            // GL.glColor4ub(faceColors.Left.R, faceColors.Left.G, faceColors.Left.B, a);       // Enable this for colorfull shadow
            GL.glColor4ub(Color.Black.R, Color.Black.G, Color.Black.B, a);                      // Disable this for colorfull shadow // Define the solid color
            GL.glVertex3d(v1[0], v1[1], v1[2]);
            GL.glVertex3d(v4[0], v4[1], v4[2]);
            GL.glVertex3d(v5[0], v5[1], v5[2]);
            GL.glVertex3d(v6[0], v6[1], v6[2]);

            //Right
            GL.glNormal3d(calcNormal(v3, v8, v2)[0], calcNormal(v3, v8, v2)[1], calcNormal(v3, v8, v2)[2]);
            a = CheckIsInsideColor(isInsideFaceColors.Right, totalBlend, halfBlend);
            // GL.glColor4ub(faceColors.Right.R, faceColors.Right.G, faceColors.Right.B, a);    // Enable this for colorfull shadow
            GL.glColor4ub(Color.Black.R, Color.Black.G, Color.Black.B, a);                      // Disable this for colorfull shadow // Define the solid color
            GL.glVertex3d(v2[0], v2[1], v2[2]);
            GL.glVertex3d(v3[0], v3[1], v3[2]);
            GL.glVertex3d(v8[0], v8[1], v8[2]);
            GL.glVertex3d(v7[0], v7[1], v7[2]);

            //Top
            GL.glNormal3d(-calcNormal(v4, v3, v1)[0], -calcNormal(v4, v3, v1)[1], -calcNormal(v4, v3, v1)[2]);
            a = CheckIsInsideColor(isInsideFaceColors.Top, totalBlend, halfBlend);
            // GL.glColor4ub(faceColors.Top.R, faceColors.Top.G, faceColors.Top.B, a);          // Enable this for colorfull shadow
            GL.glColor4ub(Color.Black.R, Color.Black.G, Color.Black.B, a);                      // Disable this for colorfull shadow // Define the solid color
            GL.glVertex3d(v1[0], v1[1], v1[2]);
            GL.glVertex3d(v2[0], v2[1], v2[2]);
            GL.glVertex3d(v3[0], v3[1], v3[2]);
            GL.glVertex3d(v4[0], v4[1], v4[2]);

            //Front
            GL.glNormal3d(calcNormal(v8, v3, v5)[0], calcNormal(v8, v3, v5)[1], calcNormal(v8, v3, v5)[2]);
            a = CheckIsInsideColor(isInsideFaceColors.Front, totalBlend, halfBlend);
            // GL.glColor4ub(faceColors.Front.R, faceColors.Front.G, faceColors.Front.B, a);    // Enable this for colorfull shadow
            GL.glColor4ub(Color.Black.R, Color.Black.G, Color.Black.B, a);                      // Disable this for colorfull shadow // Define the solid color
            GL.glVertex3d(v4[0], v4[1], v4[2]);
            GL.glVertex3d(v3[0], v3[1], v3[2]);
            GL.glVertex3d(v8[0], v8[1], v8[2]);
            GL.glVertex3d(v5[0], v5[1], v5[2]);

            GL.glEnd();
        }

        public void DrawNotSolidColor(double[] v1, double[] v2, double[] v3, double[] v4, double[] v5, double[] v6, double[] v7, double[] v8, byte totalBlend, byte halfBlend)
        {
            byte a = 255;   // not blend at all

            GL.glBegin(GL.GL_QUADS);

            //Back
            GL.glNormal3d((calcNormal(v1, v2, v6))[0], (calcNormal(v1, v2, v6))[1], (calcNormal(v1, v2, v6))[2]);
            GL.glColor4ub(faceColors.Back.R, faceColors.Back.G, faceColors.Back.B, a);
            GL.glVertex3d(v1[0], v1[1], v1[2]);
            GL.glVertex3d(v2[0], v2[1], v2[2]);
            GL.glVertex3d(v7[0], v7[1], v7[2]);
            GL.glVertex3d(v6[0], v6[1], v6[2]);

            //Bottom
            GL.glNormal3d(-calcNormal(v8, v5, v7)[0], -calcNormal(v8, v5, v7)[1], -calcNormal(v8, v5, v7)[2]);
            GL.glColor4ub(faceColors.Bottom.R, faceColors.Bottom.G, faceColors.Bottom.B, a);
            GL.glVertex3d(v6[0], v6[1], v6[2]);
            GL.glVertex3d(v7[0], v7[1], v7[2]);
            GL.glVertex3d(v8[0], v8[1], v8[2]);
            GL.glVertex3d(v5[0], v5[1], v5[2]);

            //Left
            GL.glNormal3d(calcNormal(v4, v1, v5)[0], calcNormal(v4, v1, v5)[1], calcNormal(v4, v1, v5)[2]);
            GL.glColor4ub(faceColors.Left.R, faceColors.Left.G, faceColors.Left.B, a);
            GL.glVertex3d(v1[0], v1[1], v1[2]);
            GL.glVertex3d(v4[0], v4[1], v4[2]);
            GL.glVertex3d(v5[0], v5[1], v5[2]);
            GL.glVertex3d(v6[0], v6[1], v6[2]);

            //Right
            GL.glNormal3d(calcNormal(v3, v8, v2)[0], calcNormal(v3, v8, v2)[1], calcNormal(v3, v8, v2)[2]);
            GL.glColor4ub(faceColors.Right.R, faceColors.Right.G, faceColors.Right.B, a);
            GL.glVertex3d(v2[0], v2[1], v2[2]);
            GL.glVertex3d(v3[0], v3[1], v3[2]);
            GL.glVertex3d(v8[0], v8[1], v8[2]);
            GL.glVertex3d(v7[0], v7[1], v7[2]);

            //Top
            GL.glNormal3d(-calcNormal(v4, v3, v1)[0], -calcNormal(v4, v3, v1)[1], -calcNormal(v4, v3, v1)[2]);
            GL.glColor4ub(faceColors.Top.R, faceColors.Top.G, faceColors.Top.B, a);
            GL.glVertex3d(v1[0], v1[1], v1[2]);
            GL.glVertex3d(v2[0], v2[1], v2[2]);
            GL.glVertex3d(v3[0], v3[1], v3[2]);
            GL.glVertex3d(v4[0], v4[1], v4[2]);

            //Front
            GL.glNormal3d(calcNormal(v8, v3, v5)[0], calcNormal(v8, v3, v5)[1], calcNormal(v8, v3, v5)[2]);
            GL.glColor4ub(faceColors.Front.R, faceColors.Front.G, faceColors.Front.B, a);
            GL.glVertex3d(v4[0], v4[1], v4[2]);
            GL.glVertex3d(v3[0], v3[1], v3[2]);
            GL.glVertex3d(v8[0], v8[1], v8[2]);
            GL.glVertex3d(v5[0], v5[1], v5[2]);

            GL.glEnd();
        }

        public void DrawWithTexture(double[] v1, double[] v2, double[] v3, double[] v4, double[] v5, double[] v6, double[] v7, double[] v8)
        {
            GL.glEnable(GL.GL_TEXTURE_2D);

            //----------------------

            //before transforms
            //  hence it is in const position
            //GL.glDisable(GL.GL_LIGHTING);// in this case
            GL.glBindTexture(GL.GL_TEXTURE_2D, Textures.texture[9]);
            //If enabled, the s texture coordinate is computed 
            //using the texture generation
            //function defined with GL.glTexGen.
            //If disabled, the current s texture coordinate is used.
            GL.glEnable(GL.GL_TEXTURE_GEN_S);
            //If enabled, the t texture coordinate is computed 
            //using the texture generation
            //function defined with GL.glTexGen. '
            //If disabled, the current t texture coordinate is used.
            GL.glEnable(GL.GL_TEXTURE_GEN_T);

            GL.glTexGeni(GL.GL_S, GL.GL_TEXTURE_GEN_MODE, (int)GL.GL_SPHERE_MAP);
            GL.glTexGeni(GL.GL_T, GL.GL_TEXTURE_GEN_MODE, (int)GL.GL_SPHERE_MAP);
            //GL.glEnable(GL.GL_LIGHTING);

            // for it #include <glut.h> and glu32.lib...
            //GLUT.glutSolidTeapot(2);
            //DrawC();

            // ---------------------

            byte a = 255;   // not blend at all

            GL.glBegin(GL.GL_QUADS);

            //Back
            GL.glNormal3d((calcNormal(v1, v2, v6))[0], (calcNormal(v1, v2, v6))[1], (calcNormal(v1, v2, v6))[2]);
            GL.glColor4ub(faceColors.Back.R, faceColors.Back.G, faceColors.Back.B, a);
            GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3d(v1[0], v1[1], v1[2]);
            GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3d(v2[0], v2[1], v2[2]);
            GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3d(v7[0], v7[1], v7[2]);
            GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3d(v6[0], v6[1], v6[2]);

            //Bottom
            GL.glNormal3d(-calcNormal(v8, v5, v7)[0], -calcNormal(v8, v5, v7)[1], -calcNormal(v8, v5, v7)[2]);
            GL.glColor4ub(faceColors.Bottom.R, faceColors.Bottom.G, faceColors.Bottom.B, a);
            GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3d(v6[0], v6[1], v6[2]);
            GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3d(v7[0], v7[1], v7[2]);
            GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3d(v8[0], v8[1], v8[2]);
            GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3d(v5[0], v5[1], v5[2]);

            //Left
            GL.glNormal3d(calcNormal(v4, v1, v5)[0], calcNormal(v4, v1, v5)[1], calcNormal(v4, v1, v5)[2]);
            GL.glColor4ub(faceColors.Left.R, faceColors.Left.G, faceColors.Left.B, a);
            GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3d(v1[0], v1[1], v1[2]);
            GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3d(v4[0], v4[1], v4[2]);
            GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3d(v5[0], v5[1], v5[2]);
            GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3d(v6[0], v6[1], v6[2]);

            //Right
            GL.glNormal3d(calcNormal(v3, v8, v2)[0], calcNormal(v3, v8, v2)[1], calcNormal(v3, v8, v2)[2]);
            GL.glColor4ub(faceColors.Right.R, faceColors.Right.G, faceColors.Right.B, a);
            GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3d(v2[0], v2[1], v2[2]);
            GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3d(v3[0], v3[1], v3[2]);
            GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3d(v8[0], v8[1], v8[2]);
            GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3d(v7[0], v7[1], v7[2]);

            //Top
            GL.glNormal3d(-calcNormal(v4, v3, v1)[0], -calcNormal(v4, v3, v1)[1], -calcNormal(v4, v3, v1)[2]);
            GL.glColor4ub(faceColors.Top.R, faceColors.Top.G, faceColors.Top.B, a);
            GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3d(v1[0], v1[1], v1[2]);
            GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3d(v2[0], v2[1], v2[2]);
            GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3d(v3[0], v3[1], v3[2]);
            GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3d(v4[0], v4[1], v4[2]);

            //Front
            GL.glNormal3d(calcNormal(v8, v3, v5)[0], calcNormal(v8, v3, v5)[1], calcNormal(v8, v3, v5)[2]);
            GL.glColor4ub(faceColors.Front.R, faceColors.Front.G, faceColors.Front.B, a);
            GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3d(v4[0], v4[1], v4[2]);
            GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3d(v3[0], v3[1], v3[2]);
            GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3d(v8[0], v8[1], v8[2]);
            GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3d(v5[0], v5[1], v5[2]);

            GL.glEnd();

            // ---------------------
            //GL.glDisable(GL.GL_LIGHTING);
            GL.glDisable(GL.GL_TEXTURE_GEN_S);
            GL.glDisable(GL.GL_TEXTURE_GEN_T);
            GL.glDisable(GL.GL_TEXTURE_2D);
            // ---------------------
        }

        public void Draw()
        {
            GL.glPushMatrix();
            GL.glRotatef(AngleX, 1, 0, 0);
            GL.glRotatef(AngleY, 0, 1, 0);
            GL.glRotatef(AngleZ, 0, 0, 1);

            double[] v1 = { -this.size + X, +this.size + Y, -this.size + Z };
            double[] v2 = { +this.size + X, +this.size + Y, -this.size + Z };
            double[] v3 = { +this.size + X, +this.size + Y, +this.size + Z };
            double[] v4 = { -this.size + X, +this.size + Y, +this.size + Z };
            double[] v5 = { -this.size + X, -this.size + Y, +this.size + Z };
            double[] v6 = { -this.size + X, -this.size + Y, -this.size + Z };
            double[] v7 = { +this.size + X, -this.size + Y, -this.size + Z };
            double[] v8 = { +this.size + X, -this.size + Y, +this.size + Z };

            byte totalBlend = 0;
            byte halfBlend = 150;

            if (isShadowState)
            {
                DrawShadowColor(v1,v2,v3,v4,v5,v6,v7,v8, totalBlend, halfBlend);
            }
            else
            {
                // DrawNotSolidColor(v1, v2, v3, v4, v5, v6, v7, v8, totalBlend, halfBlend);
                DrawWithTexture(v1, v2, v3, v4, v5, v6, v7, v8);
            }

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
