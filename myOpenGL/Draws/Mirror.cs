﻿using OpenGL;
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
        public uint texture1;
        public uint texture2;
        public bool leftMirror;
        public Mirror(double mirrorHeight, double mirrorWidth, double x, double y, double z, int AngleX, int AngleY, int AngleZ, bool leftMirror, uint[] texture)   
        {
            this.mirrorHeight = mirrorHeight;
            this.mirrorWidth = mirrorWidth;
            this.x = x;
            this.y = y;
            this.z = z;
            this.AngleX = AngleX;
            this.AngleY = AngleY;
            this.AngleZ = AngleZ;
            this.texture1 = texture[0];
            this.texture2 = texture[2];
            this.leftMirror = leftMirror;

            //make the surface transparent  
            GL.glBlendFunc(GL.GL_SRC_ALPHA, GL.GL_ONE_MINUS_SRC_ALPHA);
        }

        public float[,] getSurf()
        {
            float[,] surf = new float[3, 3];
            if (leftMirror)
            {
                float cosAngle = (float)(Math.Cos(Math.PI * (-this.AngleY+180) / 180.0));
                float sinAngle = (float)(Math.Sin(Math.PI * (-this.AngleY+180) / 180.0));
                surf[0, 0] = (float)this.x + (float)((-cosAngle * this.mirrorWidth) + 0 + (sinAngle * 0));    // Rotation matrix to Y axis
                surf[0, 1] = (float)this.y + (float)(0 + -mirrorHeight / 2 + 0);                              // Rotation matrix to Y axis
                surf[0, 2] = (float)this.z + (float)((-sinAngle * this.mirrorWidth) + 0 + (-cosAngle * 0));   // Rotation matrix to Y axis
            }                                                                                                 
            else                                                                                              
            {                                                                                                 
                float cosAngle = (float)(Math.Cos(Math.PI * (-this.AngleY) / 180.0));                         
                float sinAngle = (float)(Math.Sin(Math.PI * (-this.AngleY) / 180.0));                         
                surf[0, 0] = (float)this.x + (float)((cosAngle * this.mirrorWidth) + 0 + (-sinAngle * 0));     // Rotation matrix to Y axis
                surf[0, 1] = (float)this.y + (float)(0 + mirrorHeight / 2 + 0);                               // Rotation matrix to Y axis
                surf[0, 2] = (float)this.z + (float)((sinAngle * this.mirrorWidth) + 0 + (cosAngle * 0));     // Rotation matrix to Y axis
            }
            
            surf[1, 0] = (float)this.x;                            
            surf[1, 1] = (float)this.y;                            
            surf[1, 2] = (float)this.z;                             

            surf[2, 0] = surf[0, 0];        
            surf[2, 1] = -surf[0, 1];
            surf[2, 2] = surf[0, 2];

            return surf;
        }

        public void doRotations()
        {
            GL.glTranslated(x, y, z);
            GL.glRotatef(AngleX, 1, 0, 0);
            GL.glRotatef(AngleY, 0, 1, 0);
            GL.glRotatef(AngleZ, 0, 0, 1);
        }

        public void DrawAsWall(float[] colorArray, float[] minusArray) //, float[] minusArray
        {
            
            GL.glEnable(GL.GL_TEXTURE_2D);
            GL.glBindTexture(GL.GL_TEXTURE_2D, texture2);

            GL.glPushMatrix();
            
            doRotations();
            GL.glBegin(GL.GL_QUADS);

            GL.glColor4d(colorArray[0], colorArray[1], colorArray[2], colorArray[3]);
            GL.glTexCoord2d(0, 0);
            GL.glVertex3d(mirrorWidth + minusArray[0], mirrorHeight / 2 + minusArray[1], 0 + minusArray[2]);
            GL.glTexCoord2d(1, 0);
            GL.glVertex3d(0 + minusArray[0], mirrorHeight / 2 + minusArray[1], 0 + minusArray[2]);
            GL.glTexCoord2d(1, 1);
            GL.glVertex3d(0 + minusArray[0], -mirrorHeight / 2 + minusArray[1], 0 + minusArray[2]);
            GL.glTexCoord2d(0, 1);
            GL.glVertex3d(mirrorWidth + minusArray[0], -mirrorHeight / 2 + minusArray[1], 0 + minusArray[2]);

            GL.glEnd();

            GL.glPopMatrix();

            GL.glDisable(GL.GL_TEXTURE_2D);
           
        }

        public void Draw() //float[] minumArray
        {
            GL.glEnable(GL.GL_BLEND);
            GL.glEnable(GL.GL_TEXTURE_2D);
            GL.glBindTexture(GL.GL_TEXTURE_2D, texture1);

            GL.glPushMatrix();

            doRotations();

            GL.glBegin(GL.GL_QUADS);
            //!!! for blended REFLECTION 
            GL.glColor4d(1, 1, 1, 0.3);
            GL.glTexCoord2d(0, 0);
            GL.glVertex3d(mirrorWidth, mirrorHeight / 2, 0);
            GL.glTexCoord2d(1, 0);
            GL.glVertex3d(0, mirrorHeight / 2, 0);
            GL.glTexCoord2d(1, 1);
            GL.glVertex3d(0, -mirrorHeight / 2, 0);
            GL.glTexCoord2d(0, 1);
            GL.glVertex3d(mirrorWidth, -mirrorHeight / 2, 0);
            
            GL.glEnd();

            GL.glPopMatrix();

            GL.glDisable(GL.GL_TEXTURE_2D);
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
