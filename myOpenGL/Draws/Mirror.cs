using OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubikCube.Draws
{
    class Mirror
    {
        private double mirrorHeight;

        private double mirrorWidth;
        public double x { get; private set; }
        public double y { get; private set; }
        public double z { get; private set; }

        public int AngleX = 0;
        public int AngleY = 0;
        public int AngleZ = 0;
        float[] mirrorColorArray = new float[4] { 0.9f, 0.9f, 0.9f, 0.5f };


        public Mirror(double mirrorHeight, double mirrorWidth, double x, double y, double z, int AngleX, int AngleY, int AngleZ)   
        {
            this.mirrorHeight = mirrorHeight;
            this.mirrorWidth = mirrorWidth;
            this.x = x;
            this.y = y;
            this.z = z;
            this.AngleX = AngleX;
            this.AngleY = AngleY;
            this.AngleZ = AngleZ;
           
            //make the surface transparent  
            GL.glBlendFunc(GL.GL_SRC_ALPHA, GL.GL_ONE_MINUS_SRC_ALPHA);
        }

        public float[,] getSurf()
        {
            float[,] surf = new float[3, 3];
            surf[0, 0] = (float)mirrorWidth;
            surf[0, 1] = (float)mirrorHeight / 2;
            surf[0, 2] = (float)0;

            surf[1, 0] = (float)0;
            surf[1, 1] = (float)mirrorHeight / 2;
            surf[1, 2] = (float)0;

            surf[2, 0] = (float)0;
            surf[2, 1] = (float)-mirrorHeight / 2;
            surf[2, 2] = (float)0;

            return surf;
        }

        public void doRotations()
        {
            GL.glTranslated(x, y, z);
            GL.glRotatef(AngleX, 1, 0, 0);
            GL.glRotatef(AngleY, 0, 1, 0);
            GL.glRotatef(AngleZ, 0, 0, 1);
        }

        public void DrawAsWall(float[] colorArray, float[] minumArray)
        {
            GL.glDisable(GL.GL_BLEND);

            GL.glPushMatrix();

            doRotations();
            GL.glBegin(GL.GL_QUADS);
            //!!! for blended REFLECTION
            GL.glColor4d(colorArray[0], colorArray[1], colorArray[2], colorArray[3]);
            GL.glVertex3d(mirrorWidth - minumArray[0], mirrorHeight / 2 - minumArray[1], 0 - minumArray[2]);
            GL.glVertex3d(0 - minumArray[0], mirrorHeight / 2 - minumArray[1], 0 - minumArray[2]);
            GL.glVertex3d(0 - minumArray[0], -mirrorHeight / 2 - minumArray[1], 0 - minumArray[2]);
            GL.glVertex3d(mirrorWidth - minumArray[0], -mirrorHeight / 2 - minumArray[1], 0 - minumArray[2]);
            GL.glEnd();

            GL.glPopMatrix();
        }

        public void Draw(float[] minumArray)
        {
            // GL.glEnable(GL.GL_LIGHTING);
            
            //make the surface transparent
            GL.glEnable(GL.GL_BLEND);
            GL.glPushMatrix();
            
            doRotations();
            GL.glBegin(GL.GL_QUADS);
            //!!! for blended REFLECTION
            GL.glColor4d(mirrorColorArray[0], mirrorColorArray[1], mirrorColorArray[2], mirrorColorArray[3]);
            GL.glVertex3d(mirrorWidth - minumArray[0], mirrorHeight / 2 - minumArray[1], 0 - minumArray[2]);
            GL.glVertex3d(0 - minumArray[0], mirrorHeight / 2 - minumArray[1], 0 - minumArray[2]);
            GL.glVertex3d(0 - minumArray[0], -mirrorHeight / 2 - minumArray[1], 0 - minumArray[2]);
            GL.glVertex3d(mirrorWidth - minumArray[0], -mirrorHeight / 2 - minumArray[1], 0 - minumArray[2]);
            GL.glEnd();

            GL.glPopMatrix();
            GL.glDisable(GL.GL_BLEND);
        }

        public void Rotate(int AngleAxisX, int AngleAxisY, int AngleAxisZ)
        {
            this.AngleX += AngleAxisX;
            this.AngleY += AngleAxisY;
            this.AngleZ += AngleAxisZ;
        }

    }    
}
