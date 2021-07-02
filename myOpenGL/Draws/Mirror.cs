using OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubikCube.Draws
{
    class Mirror : IDraw
    {
        private double mirrorHeight;

        private double mirrorWidth;
        public double x { get; private set; }
        public double y { get; private set; }
        public double z { get; private set; }

        public int AngleX = 0;
        public int AngleY = 0;
        public int AngleZ = 0;

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
            GL.glEnable(GL.GL_BLEND); 
            GL.glBlendFunc(GL.GL_SRC_ALPHA, GL.GL_ONE_MINUS_SRC_ALPHA);
        }

        public void Draw()
        {
            //   GL.glEnable(GL.GL_LIGHTING);

            GL.glPushMatrix();

            GL.glTranslated(x, y, z);
            GL.glRotatef(AngleX, 1, 0, 0);
            GL.glRotatef(AngleY, 0, 1, 0);
            GL.glRotatef(AngleZ, 0, 0, 1);

            GL.glBegin(GL.GL_QUADS);
            //!!! for blended REFLECTION 
            GL.glColor4d(0.9, 0.9, 0.9, 0.5);
            GL.glVertex3d(mirrorWidth, mirrorHeight / 2, 0);
            GL.glVertex3d(0, mirrorHeight / 2, 0);
            GL.glVertex3d(0, -mirrorHeight / 2, 0);
            GL.glVertex3d(mirrorWidth, -mirrorHeight / 2, 0);
            GL.glEnd();

            GL.glPopMatrix();
 
        }

        public void Rotate(int AngleAxisX, int AngleAxisY, int AngleAxisZ)
        {
            this.AngleX += AngleAxisX;
            this.AngleY += AngleAxisY;
            this.AngleZ += AngleAxisZ;
        }

    }    
}
