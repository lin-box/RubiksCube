
/*
 * 
 * משימות ג.ממוחשבת:

1. לבנות צורה
1.1. לתקן קצוות של צורה שיראה חלק
2. שיהיה אפשר לעשות תזוזה עם העכבר/מקלדת לראות כמה פוינטס אוף וויו.
3. לסובב שורות ועמודות.
4. להוסיף מראות עם טקסטורה של מראה 
5. להוסיף טקסטורות
6. לעצב כפתורים
7. סוגי אורות עם הצללה
8. להוסיף פקדים מיוחדים לתאורה, וסקרולדאון לזום
*** האם צריך לשנות טקסטורות?
בונוס:
1. *לייבא נתונים מקובץ
2. לכתוב אלגוריתם שפותר.
3. LABEL שאומר עוד כמה צעדים מינימום צריך לעשות כדי לנצח.
4. להפעיל מצב מנחה:
כותב מה צריך לעשות למשל:
RU,...

משה
1. להפוך את הלחצנים ל SCROLLBAR
2. להכניס טקסטורה של מראה למראות
https://www.geogebra.org/3d/xkpa4d88
 * 
 */

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

        private FaceCube<Color> faceColors;

        private int AngleX = 0;
        private int AngleY = 0;
        private int AngleZ = 0;


        public Cube(double size, double X, double Y, double Z)
        {
            this.size = size;
            this.X = X;
            this.Y = Y;
            this.Z = Z;
            this.faceColors = new FaceCube<Color>(Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black);
        }

        public Cube(double size, double X, double Y, double Z, FaceCube<Color> faceColors)
        {
            this.size = size;
            this.X = X;
            this.Y = Y;
            this.Z = Z;
            this.faceColors = faceColors;
        }

        public Cube(double size, double X, double Y, double Z, FaceCube<Texture> faceColors)
        {
            this.size = size;
            this.X = X;
            this.Y = Y;
            this.Z = Z;
            //this.faceColors = faceColors;
        }

        public void Rotate(int AngleAxisX, int AngleAxisY, int AngleAxisZ)
        {
            this.AngleX += AngleAxisX;
            this.AngleY += AngleAxisY;
            this.AngleZ += AngleAxisZ;
        }

        public void Draw(bool isShadow = false)
        {

            GL.glEnable(GL.GL_TEXTURE);

            GL.glPushMatrix();
            GL.glRotatef(AngleX, 1, 0, 0);
            GL.glRotatef(AngleY, 0, 1, 0);
            GL.glRotatef(AngleZ, 0, 0, 1);

            GL.glBegin(GL.GL_QUADS);

            //Back
            if(!isShadow)
                GL.glColor3ub(faceColors.Back.R, faceColors.Back.G, faceColors.Back.B);
            else
                GL.glColor3d(0.5, 0.5,0.5);
            GL.glVertex3d(this.size + X, this.size + Y, -this.size + Z);
            GL.glVertex3d(this.size + X, -this.size + Y, -this.size + Z);
            GL.glVertex3d(-this.size + X, -this.size + Y, -this.size + Z);
            GL.glVertex3d(-this.size + X, this.size + Y, -this.size + Z);

            //Bottom
            if (!isShadow)
                GL.glColor3ub(faceColors.Bottom.R, faceColors.Bottom.G, faceColors.Bottom.B);
            else
                GL.glColor3d(0.5, 0.5, 0.5);
            GL.glVertex3d(-this.size + X, -this.size + Y, -this.size + Z);
            GL.glVertex3d(this.size + X, -this.size + Y, -this.size + Z);
            GL.glVertex3d(this.size + X, -this.size + Y, this.size + Z);
            GL.glVertex3d(-this.size + X, -this.size + Y, this.size + Z);

            //Left
            if (!isShadow)
                GL.glColor3ub(faceColors.Left.R, faceColors.Left.G, faceColors.Left.B);
            else
                GL.glColor3d(0.5, 0.5, 0.5);
            GL.glVertex3d(-this.size + X, this.size + Y, -this.size + Z);
            GL.glVertex3d(-this.size + X, -this.size + Y, -this.size + Z);
            GL.glVertex3d(-this.size + X, -this.size + Y, this.size + Z);
            GL.glVertex3d(-this.size + X, this.size + Y, this.size + Z);

            //Right
            if (!isShadow)
                GL.glColor3ub(faceColors.Right.R, faceColors.Right.G, faceColors.Right.B);
            else
                GL.glColor3d(0.5, 0.5, 0.5);
            GL.glVertex3d(this.size + X, this.size + Y, this.size + Z);
            GL.glVertex3d(this.size + X, -this.size + Y, this.size + Z);
            GL.glVertex3d(this.size + X, -this.size + Y, -this.size + Z);
            GL.glVertex3d(this.size + X, this.size + Y, -this.size + Z);

            //Top
            if (!isShadow)
                GL.glColor3ub(faceColors.Top.R, faceColors.Top.G, faceColors.Top.B);
            else
                GL.glColor3d(0.5, 0.5, 0.5);
            GL.glVertex3d(-this.size + X, this.size + Y, -this.size + Z);
            GL.glVertex3d(-this.size + X, this.size + Y, this.size + Z);
            GL.glVertex3d(this.size + X, this.size + Y, this.size + Z);
            GL.glVertex3d(this.size + X, this.size + Y, -this.size + Z);

            //Front
            if (!isShadow)
                GL.glColor3ub(faceColors.Front.R, faceColors.Front.G, faceColors.Front.B);
            else
                GL.glColor3d(0.5, 0.5, 0.5);
            GL.glVertex3d(-this.size + X, this.size + Y, this.size + Z);
            GL.glVertex3d(-this.size + X, -this.size + Y, this.size + Z);
            GL.glVertex3d(this.size + X, -this.size + Y, this.size + Z);
            GL.glVertex3d(this.size + X, this.size + Y, this.size + Z);
            
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
