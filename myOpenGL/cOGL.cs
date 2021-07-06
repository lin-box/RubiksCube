using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using RubikCube;
using RubikCube.Draws;

namespace OpenGL
{
    class cOGL
    {
        Control p;
        int Width;
        int Height;
        double mirrorHeight = 8;
        double mirrorWidth = 6;
        public Mirror backMirrorSurface;
        public Mirror rightMirrorSurface;
        public Mirror leftMirrorSurface;
        public RubiksCube rubiksCube;

        public float[] ScrollValue = new float[14] { 1, -10, 10, 0, 0, 0, 0, 1, 0, -0.6f, -3, -2, 3, 4.1f };
        public float[] lightPos = new float[4] { -0.1f, 4.3f, 2f, -1f };
        double[] AccumulatedRotationsTraslations = new double[16];
        float[] shadowCubeXform = new float[16];
        public float[] pos = new float[4];
        float[] backWallColorArray = new float[4] { 0.9f, 0.9f, 0.5f, 1f };
        float[] leftWallColorArray = new float[4] { 0.8f, 0.9f, 0.6f, 1f };
        float[] rightWallColorArray = new float[4] { 0.8f, 0.9f, 0.6f, 1f };
        float[] backMinusArray = new float[3] { 0, 0, 1f };
        float[] leftMinusArray = new float[3] { 1f, 0, 0};
        float[] rightMinusArray = new float[3] { 1f, 0, 0 };
        public float[] cubemapXYZAngles = new float[3] { 0, 0, 0 }; // cube map
        public int display_mod;
        public uint[] cubeMapTextures = new uint[6];
        public int viewAngle = 70;

        public cOGL(Control pb)
        {
            p=pb;
            Width = p.Width;
            Height = p.Height;
            InitializeGL();

            display_mod = 1;

            rubiksCube = new RubiksCube();
            backMirrorSurface = new Mirror(mirrorHeight, mirrorWidth, -mirrorWidth / 2, 0, -mirrorHeight / 2, 0, 0, 0, texture[0]);
            rightMirrorSurface = new Mirror(mirrorHeight, mirrorWidth*1.5, mirrorWidth / 2, 0, -mirrorHeight / 2, 0, -90, 0, texture[0]);
            leftMirrorSurface = new Mirror(mirrorHeight, mirrorWidth*1.5, -mirrorWidth / 2, 0, -mirrorHeight / 2, 0, -90, 0, texture[0]);
            
        }

        ~cOGL()
        {
            WGL.wglDeleteContext(m_uint_RC);
        }

		uint m_uint_HWND = 0;

        public uint HWND
		{
			get{ return m_uint_HWND; }
		}
		
        uint m_uint_DC   = 0;

        public uint DC
		{
			get{ return m_uint_DC;}
		}
		uint m_uint_RC   = 0;

        public uint RC
		{
			get{ return m_uint_RC; }
		}

        void GenerateTextures()
        {
            GL.glBlendFunc(GL.GL_SRC_ALPHA, GL.GL_ONE_MINUS_SRC_ALPHA);
            GL.glGenTextures(6, cubeMapTextures);
            string[] imagesName ={"IMG\\green.bmp","IMG\\blue.bmp",
                                    "IMG\\orange.bmp","IMG\\red.bmp","IMG\\white.bmp","IMG\\yellow.bmp"};
            for (int i = 0; i < 6; i++)
            {
                Bitmap image = new Bitmap(imagesName[i]);
                image.RotateFlip(RotateFlipType.RotateNoneFlipY); //Y axis in Windows is directed downwards, while in OpenGL-upwards
                System.Drawing.Imaging.BitmapData bitmapdata;
                Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);

                bitmapdata = image.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                GL.glBindTexture(GL.GL_TEXTURE_2D, cubeMapTextures[i]);
                //2D for XYZ
                GL.glTexImage2D(GL.GL_TEXTURE_2D, 0, (int)GL.GL_RGB8, image.Width, image.Height,
                                                              0, GL.GL_BGR_EXT, GL.GL_UNSIGNED_byte, bitmapdata.Scan0);
                GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MIN_FILTER, (int)GL.GL_LINEAR);
                GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MAG_FILTER, (int)GL.GL_LINEAR);

                image.UnlockBits(bitmapdata);
                image.Dispose();
            }
        }

        void DrawTexturedCube()
        {
            float big = 1.0f;
            float small = big;

            // front
            GL.glBindTexture(GL.GL_TEXTURE_2D, cubeMapTextures[0]);
            GL.glBegin(GL.GL_QUADS);
            GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(-big, -big, big);
            GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(big, -big, big);
            GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(big, big, big);
            GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(-big, big, big);
            GL.glEnd();
            // back
            GL.glBindTexture(GL.GL_TEXTURE_2D, cubeMapTextures[1]);
            GL.glBegin(GL.GL_QUADS);
            GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(small, -small, -small);
            GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(-small, -small, -small);
            GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(-small, small, -small);
            GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(small, small, -small);
            GL.glEnd();
            // left
            GL.glBindTexture(GL.GL_TEXTURE_2D, cubeMapTextures[2]);
            GL.glBegin(GL.GL_QUADS);
            GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(-small, -small, -small);
            GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(-big, -big, big);
            GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(-big, big, big);
            GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(-small, small, -small);
            GL.glEnd();
            // right
            GL.glBindTexture(GL.GL_TEXTURE_2D, cubeMapTextures[3]);
            GL.glBegin(GL.GL_QUADS);
            GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(big, -big, big);
            GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(small, -small, -small);
            GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(small, small, -small);
            GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(big, big, big);
            GL.glEnd();
            // top
            GL.glBindTexture(GL.GL_TEXTURE_2D, cubeMapTextures[4]);
            GL.glBegin(GL.GL_QUADS);
            GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(-big, big, big);
            GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(big, big, big);
            GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(small, small, -small);
            GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(-small, small, -small);
            GL.glEnd();
            // bottom
            GL.glBindTexture(GL.GL_TEXTURE_2D, cubeMapTextures[5]);
            GL.glBegin(GL.GL_QUADS);
            GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(-small, -small, -small);
            GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(small, -small, -small);
            GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(big, -big, big);
            GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(-big, -big, big);
            GL.glEnd();
        }

        void DrawBounds()
        {
            GL.glScalef(0.99f, 0.99f, 0.99f);
            GL.glLineWidth(2);
            GL.glColor3f(0.0f, 0.0f, 0.0f);
            //GL.glDisable(GL.GL_LIGHTING);
            GL.glBegin(GL.GL_LINE_LOOP);
            GL.glVertex3f(-1, -1, -1);
            GL.glVertex3f(1, -1, -1);
            GL.glVertex3f(1, -1, 1);
            GL.glVertex3f(-1, -1, 1);
            GL.glEnd();
            GL.glBegin(GL.GL_LINE_LOOP);
            GL.glVertex3f(-1, 1, -1);
            GL.glVertex3f(1, 1, -1);
            GL.glVertex3f(1, 1, 1);
            GL.glVertex3f(-1, 1, 1);
            GL.glEnd();
            GL.glBegin(GL.GL_LINES);
            GL.glVertex3f(-1, -1, -1);
            GL.glVertex3f(-1, 1, -1);

            GL.glVertex3f(1, -1, -1);
            GL.glVertex3f(1, 1, -1);

            GL.glVertex3f(1, -1, 1);
            GL.glVertex3f(1, 1, 1);

            GL.glVertex3f(-1, -1, 1);
            GL.glVertex3f(-1, 1, 1);
            GL.glEnd();
            GL.glScalef(1.0f / 0.99f, 1.0f / 0.99f, 1.0f / 0.99f);

            //GL.glEnable(GL.GL_COLOR_MATERIAL);
            //GL.glEnable(GL.GL_LIGHT0);
            //GL.glEnable(GL.GL_LIGHTING);
            GL.glTranslatef(0.1f, 0.2f, -0.7f);
            GL.glColor3f(0, 1, 0);
            //GLU.gluSphere(obj, 0.05, 16, 16);
            //rubiksCube.Draw();
            GL.glTranslatef(-0.1f, -0.2f, 0.7f);
            //GL.glDisable(GL.GL_LIGHTING);
        }

        public void update_cube_map_prespective()
        {
            GL.glMatrixMode(GL.GL_PROJECTION);
            GL.glLoadIdentity();

            GLU.gluPerspective(viewAngle, (float)Width / (float)Height, 0.45f, 30.0f);

            GL.glMatrixMode(GL.GL_MODELVIEW);
            GL.glLoadIdentity();
        }

        public void update_cube_map_rotations()
        {
            GL.glTranslatef(0.0f, 0.0f, -1.4f);

            GL.glRotatef(cubemapXYZAngles[0], 1.0f, 0.0f, 0.0f);
            GL.glRotatef(cubemapXYZAngles[1], 0.0f, 1.0f, 0.0f);
            GL.glRotatef(cubemapXYZAngles[2], 0.0f, 0.0f, 1.0f);

            GL.glDisable(GL.GL_LIGHTING);
            GL.glDisable(GL.GL_TEXTURE_2D);

            DrawBounds();

            GL.glColor4f(1.0f, 1.0f, 1.0f, 0.5f);
            GL.glEnable(GL.GL_TEXTURE_2D);
            DrawTexturedCube();
            GL.glColor4f(1.0f, 1.0f, 1.0f, 1);
            GL.glDisable(GL.GL_TEXTURE_2D);
        }

        void DrawWalls()
        {
            backMirrorSurface.DrawAsWall(backWallColorArray, backMinusArray);
            leftMirrorSurface.DrawAsWall(leftWallColorArray, leftMinusArray);
            rightMirrorSurface.DrawAsWall(rightWallColorArray, rightMinusArray);
        }

        void DrawMirrors()
        {
            //only wall, draw only to STENCIL buffer
            GL.glEnable(GL.GL_BLEND);
            GL.glEnable(GL.GL_STENCIL_TEST);
            GL.glStencilOp(GL.GL_REPLACE, GL.GL_REPLACE, GL.GL_REPLACE); // change stencil according to the object color
            GL.glStencilFunc(GL.GL_ALWAYS, 1, 0xFFFFFFFF); // draw wall always
            GL.glColorMask((byte)GL.GL_FALSE, (byte)GL.GL_FALSE, (byte)GL.GL_FALSE, (byte)GL.GL_FALSE);
            //GL.glDisable(GL.GL_DEPTH_TEST);
            // add mirrors for STENCIL buffer
            backMirrorSurface.Draw(backMinusArray);
            rightMirrorSurface.Draw(rightMinusArray);
            leftMirrorSurface.Draw(leftMinusArray);

            // restore regular settings
            GL.glColorMask((byte)GL.GL_TRUE, (byte)GL.GL_TRUE, (byte)GL.GL_TRUE, (byte)GL.GL_TRUE);
            GL.glEnable(GL.GL_DEPTH_TEST);

            // reflection is drawn only where STENCIL buffer value equal to 1
            GL.glStencilFunc(GL.GL_EQUAL, 1, 0xFFFFFFFF);
            GL.glStencilOp(GL.GL_KEEP, GL.GL_KEEP, GL.GL_KEEP); // keep object origenal color

            GL.glEnable(GL.GL_STENCIL_TEST);

            // draw reflected scene for back mirror
            GL.glPushMatrix();
            GL.glScalef(1, 1, -1); //swap on Z axis
            GL.glTranslated(0, 0, mirrorWidth);   
            rubiksCube.Draw();
            GL.glPopMatrix();

            // draw reflected left mirror scene for right mirror scene
            GL.glPushMatrix();
            //GL.glScalef(-1, 1, 1); //swap on Z axis
            GL.glTranslated(mirrorWidth * 2, 0, 0);
            rubiksCube.Draw();
            GL.glPopMatrix();

            // draw reflected scene for right mirror
            GL.glPushMatrix();
            GL.glScalef(-1, 1, 1); //swap on X axis
            GL.glTranslated(-mirrorWidth, 0, 0);
            leftMirrorSurface.Draw(leftMinusArray);
            rubiksCube.Draw();
            GL.glPopMatrix();


            // draw reflected right mirror scene for left mirror scene
            GL.glPushMatrix();
            //GL.glScalef(-1, 1, 1); //swap on Z axis
            GL.glTranslated(-mirrorWidth * 2, 0, 0);
            rubiksCube.Draw();
            GL.glPopMatrix();


            // draw reflected scene for left mirror
            GL.glPushMatrix();
            GL.glScalef(-1, 1, 1); //swap on X axis
            GL.glTranslated(mirrorWidth, 0, 0);
            rightMirrorSurface.Draw(rightMinusArray);
            rubiksCube.Draw();
            GL.glPopMatrix();

            // really draw wall 
            //( half-transparent ( see its color's alpha byte)))
            // in order to see reflected objects 
            GL.glDepthMask((byte)GL.GL_FALSE);
            backMirrorSurface.Draw(backMinusArray);
            rightMirrorSurface.Draw(rightMinusArray);
            leftMirrorSurface.Draw(leftMinusArray);
            GL.glDepthMask((byte)GL.GL_TRUE);
            // Disable GL.GL_STENCIL_TEST to show All, else it will be cut on GL.GL_STENCIL
            GL.glDisable(GL.GL_STENCIL_TEST);
            GL.glDisable(GL.GL_DEPTH_TEST);
            GL.glDisable(GL.GL_BLEND);
        }

        void DrawAxes(Color xColor, Color yColor, Color zColor)
        {
            GL.glBegin( GL.GL_LINES);
            //x
            GL.glColor3f(xColor.R, xColor.G, xColor.B);
            GL.glVertex3f(-3.0f, 0.0f, 0.0f);
            GL.glVertex3f(3.0f, 0.0f, 0.0f);
            //y 
            GL.glColor3f(yColor.R, yColor.G, yColor.B);
            GL.glVertex3f(0.0f, -3.0f, 0.0f);
            GL.glVertex3f(0.0f, 3.0f, 0.0f);
            //z
            GL.glColor3f(zColor.R, zColor.G, zColor.B);
            GL.glVertex3f(0.0f, 0.0f, -3.0f);
            GL.glVertex3f(0.0f, 0.0f, 3.0f);
            GL.glEnd();
        }

        void UpdateScrollInput()
        {
            lightPos[0] = ScrollValue[10];
            lightPos[1] = ScrollValue[11];
            lightPos[2] = ScrollValue[12];
            lightPos[3] = 1.0f;

            pos[0] = ScrollValue[10];
            pos[1] = ScrollValue[11];
            pos[2] = ScrollValue[12];
            pos[3] = 1.0f;
        }

 //       void UpdateLightSettings()
 //       {
 //           GL.glClear(GL.GL_COLOR_BUFFER_BIT | GL.GL_DEPTH_BUFFER_BIT);
 //
 //           GL.glLoadIdentity();
 //
 //           // not trivial
 //           double[] ModelVievMatrixBeforeSpecificTransforms = new double[16];
 //           double[] CurrentRotationTraslation = new double[16];
 //
 //           GLU.gluLookAt(ScrollValue[0], ScrollValue[1], ScrollValue[2],
 //                      ScrollValue[3], ScrollValue[4], ScrollValue[5],
 //                      ScrollValue[6], ScrollValue[7], ScrollValue[8]);
 //           GL.glTranslatef(0.0f, 0.0f, -1.0f);
 //
 //           //save current ModelView Matrix values
 //           //in ModelVievMatrixBeforeSpecificTransforms array
 //           //ModelView Matrix ========>>>>>> ModelVievMatrixBeforeSpecificTransforms
 //           GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, ModelVievMatrixBeforeSpecificTransforms);
 //           //ModelView Matrix was saved, so
 //           GL.glLoadIdentity(); // make it identity matrix
 //
 //           //as result - the ModelView Matrix now is pure representation
 //           //of KeyCode transform and only it !!!
 //
 //           //save current ModelView Matrix values
 //           //in CurrentRotationTraslation array
 //           //ModelView Matrix =======>>>>>>> CurrentRotationTraslation
 //           GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, CurrentRotationTraslation);
 //
 //           //The GL.glLoadMatrix function replaces the current matrix with
 //           //the one specified in its argument.
 //           //The current matrix is the
 //           //projection matrix, modelview matrix, or texture matrix,
 //           //determined by the current matrix mode (now is ModelView mode)
 //           GL.glLoadMatrixd(AccumulatedRotationsTraslations); //Global Matrix
 //
 //           //The GL.glMultMatrix function multiplies the current matrix by
 //           //the one specified in its argument.
 //           //That is, if M is the current matrix and T is the matrix passed to
 //           //GL.glMultMatrix, then M is replaced with M � T
 //           GL.glMultMatrixd(CurrentRotationTraslation);
 //
 //           //save the matrix product in AccumulatedRotationsTraslations1
 //           GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, AccumulatedRotationsTraslations);
 //
 //           //replace ModelViev Matrix with stored ModelVievMatrixBeforeSpecificTransforms
 //           GL.glLoadMatrixd(ModelVievMatrixBeforeSpecificTransforms);
 //           //multiply it by KeyCode defined AccumulatedRotationsTraslations1 matrix
 //           GL.glMultMatrixd(AccumulatedRotationsTraslations);
 //       }
 //
        void DrawLights()
        {
            //GL.glPushMatrix();
            GL.glLightfv(GL.GL_LIGHT0, GL.GL_POSITION, lightPos);

            //Draw Light Source
            GL.glDisable(GL.GL_LIGHTING);
            GL.glTranslatef(lightPos[0], lightPos[1], lightPos[2]);
            //Yellow Light source
            GL.glColor3f(1, 1, 0);
            GLUT.glutSolidSphere(0.05, 8, 8);
            GL.glTranslatef(-lightPos[0], -lightPos[1], -lightPos[2]);

            //main System draw
            GL.glEnable(GL.GL_LIGHTING);
            //GL.glPopMatrix();
        }

        void DrawObjects(bool isForShades)
        {
            if (!isForShades)
            {
                rubiksCube.Draw(false);
            }
            else
            {
                rubiksCube.Draw(true);
            }
        }

        // Reduces a normal vector specified as a set of three coordinates,
        // to a unit normal vector of length one.
        void ReduceToUnit(float[] vector)
        {
            float length;

            // Calculate the length of the vector		
            length = (float)Math.Sqrt((vector[0] * vector[0]) +
                                (vector[1] * vector[1]) +
                                (vector[2] * vector[2]));

            // Keep the program from blowing up by providing an exceptable
            // value for vectors that may calculated too close to zero.
            if (length == 0.0f)
                length = 1.0f;

            // Dividing each element by the length will result in a
            // unit normal vector.
            vector[0] /= length;
            vector[1] /= length;
            vector[2] /= length;
        }

        // Points p1, p2, & p3 specified in counter clock-wise order
        void calcNormal(float[,] v, float[] outp)
        {
            const int x = 0;
            const int y = 1;
            const int z = 2;

            float[] v1 = new float[3];
            float[] v2 = new float[3];

            // Calculate two vectors from the three points
            v1[x] = v[0, x] - v[1, x];
            v1[y] = v[0, y] - v[1, y];
            v1[z] = v[0, z] - v[1, z];

            v2[x] = v[1, x] - v[2, x];
            v2[y] = v[1, y] - v[2, y];
            v2[z] = v[1, z] - v[2, z];

            // Take the cross product of the two vectors to get
            // the normal vector which will be stored in out
            outp[x] = v1[y] * v2[z] - v1[z] * v2[y];
            outp[y] = v1[z] * v2[x] - v1[x] * v2[z];
            outp[z] = v1[x] * v2[y] - v1[y] * v2[x];

            // Normalize the vector (shorten length to one)
            ReduceToUnit(outp);
        }

        // Creates a shadow projection matrix out of the plane equation
        // coefficients and the position of the light. The return value is stored
        // in cubeXform[,]
        void MakeShadowMatrix(float[,] points)
        {
            float[] planeCoeff = new float[4];
            float dot;

            // Find the plane equation coefficients
            // Find the first three coefficients the same way we
            // find a normal.
            calcNormal(points, planeCoeff);

            // Find the last coefficient by back substitutions
            planeCoeff[3] = -(
                (planeCoeff[0] * points[2, 0]) + (planeCoeff[1] * points[2, 1]) +
                (planeCoeff[2] * points[2, 2]));


            // Dot product of plane and light position
            dot = planeCoeff[0] * pos[0] +
                    planeCoeff[1] * pos[1] +
                    planeCoeff[2] * pos[2] +
                    planeCoeff[3];

            // Now do the projection
            // First column
            shadowCubeXform[0] = dot - pos[0] * planeCoeff[0];
            shadowCubeXform[4] = 0.0f - pos[0] * planeCoeff[1];
            shadowCubeXform[8] = 0.0f - pos[0] * planeCoeff[2];
            shadowCubeXform[12] = 0.0f - pos[0] * planeCoeff[3];

            // Second column
            shadowCubeXform[1] = 0.0f - pos[1] * planeCoeff[0];
            shadowCubeXform[5] = dot - pos[1] * planeCoeff[1];
            shadowCubeXform[9] = 0.0f - pos[1] * planeCoeff[2];
            shadowCubeXform[13] = 0.0f - pos[1] * planeCoeff[3];

            // Third Column
            shadowCubeXform[2] = 0.0f - pos[2] * planeCoeff[0];
            shadowCubeXform[6] = 0.0f - pos[2] * planeCoeff[1];
            shadowCubeXform[10] = dot - pos[2] * planeCoeff[2];
            shadowCubeXform[14] = 0.0f - pos[2] * planeCoeff[3];

            // Fourth Column
            shadowCubeXform[3] = 0.0f - pos[3] * planeCoeff[0];
            shadowCubeXform[7] = 0.0f - pos[3] * planeCoeff[1];
            shadowCubeXform[11] = 0.0f - pos[3] * planeCoeff[2];
            shadowCubeXform[15] = dot - pos[3] * planeCoeff[3];
        }

        void DrawRoom()
        {
            GL.glPushMatrix();
            update_cube_map_prespective();

            GL.glLoadIdentity();

            update_cube_map_rotations();
            GL.glPopMatrix();
            GL.glLoadIdentity();
        }

        void DrawFigures()
        {
            GL.glPushMatrix();

            DrawLights();

            switch (display_mod)
            {
                case 1:
                    DrawMirrors();
                    DrawObjects(false);
                    GL.glPopMatrix();
                    break;
                case 2:
                    DrawWalls();

                    DrawObjects(false);
                    GL.glPopMatrix();

                    //SHADING begin
                    //we'll define cubeXform matrix in MakeShadowMatrix Sub
                    // Disable lighting, we'll just draw the shadow
                    //else instead of shadow we'll see stange projection of the same objects
                    GL.glDisable(GL.GL_LIGHTING);

                    /*
                    // back shadow
                    //!!!!!!!!!!!!!
                    GL.glPushMatrix();
                    backMirrorSurface.doRotations();
                    //!!!!!!!!!!!!    		
                    MakeShadowMatrix(backMirrorSurface.getSurf());
                    GL.glMultMatrixf(cubeXform);
                    DrawObjects(true);
                    //!!!!!!!!!!!!!
                    GL.glPopMatrix();
                    //!!!!!!!!!!!!!
                    */
                    // left shadow
                    //!!!!!!!!!!!!!
                    GL.glPushMatrix();
                    leftMirrorSurface.doRotations();
                    //!!!!!!!!!!!!    		
                    MakeShadowMatrix(leftMirrorSurface.getSurf());
                    GL.glMultMatrixf(shadowCubeXform);
                    DrawObjects(true);
                    //!!!!!!!!!!!!!
                    GL.glPopMatrix();
                    //!!!!!!!!!!!!!

                    // right shadow
                    //!!!!!!!!!!!!!
                    GL.glPushMatrix();
                    //!!!!!!!!!!!!    
                    rightMirrorSurface.doRotations();
                    MakeShadowMatrix(rightMirrorSurface.getSurf());
                    GL.glMultMatrixf(shadowCubeXform);
                    DrawObjects(true);
                    //!!!!!!!!!!!!!
                    GL.glPopMatrix();
                    //!!!!!!!!!!!!!
                    break;
                case 3:
                    DrawRoom();
                    GL.glEnable(GL.GL_LIGHTING);
                    GL.glTranslatef(0.0f, 0.0f, -7.4f);
                    DrawObjects(false);
                    GL.glTranslatef(0.0f, 0.0f, +7.4f);
                    GL.glPopMatrix();
                    GL.glDisable(GL.GL_LIGHTING);
                    break;
            }

        }

        public void Draw()
        {
            if (m_uint_DC == 0 || m_uint_RC == 0)
                return;

            GL.glClear(GL.GL_COLOR_BUFFER_BIT | GL.GL_DEPTH_BUFFER_BIT | GL.GL_STENCIL_BUFFER_BIT);
            
            GL.glLoadIdentity();

            GLU.gluLookAt(ScrollValue[1], 1, 10, 0, 0, 0, 0, 1, 0);
           
            GL.glTranslated(0, 0, -6);
            //GL.glRotated(30, 0, 1, 0);
            GL.glRotated(20, 1, 0, 0);

         //   rubiksCube.Draw();

            DrawAxes(Color.Red, Color.Green, Color.Blue);
            
            DrawAxes(Color.Red, Color.Green, Color.Blue);
            
            UpdateScrollInput();
            //UpdateLightSettings();
            DrawLights();

            DrawMirrors();

            DrawFigures();

            //rubiksCube.Draw();

            GL.glFlush();

            WGL.wglSwapBuffers(m_uint_DC);
        }

		protected virtual void InitializeGL()
		{
            m_uint_HWND = (uint)p.Handle.ToInt32();
            m_uint_DC = WGL.GetDC(m_uint_HWND);

            // Not doing the following WGL.wglSwapBuffers() on the DC will
            // result in a failure to subsequently create the RC.
            WGL.wglSwapBuffers(m_uint_DC);

            WGL.PIXELFORMATDESCRIPTOR pfd = new WGL.PIXELFORMATDESCRIPTOR();
            WGL.ZeroPixelDescriptor(ref pfd);
            pfd.nVersion = 1;
            pfd.dwFlags = (WGL.PFD_DRAW_TO_WINDOW | WGL.PFD_SUPPORT_OPENGL | WGL.PFD_DOUBLEBUFFER);
            pfd.iPixelType = (byte)(WGL.PFD_TYPE_RGBA);
            pfd.cColorBits = 32;
            pfd.cDepthBits = 32;
            pfd.iLayerType = (byte)(WGL.PFD_MAIN_PLANE);

            //for Stencil support 

            pfd.cStencilBits = 32;

            int pixelFormatIndex = 0;
            pixelFormatIndex = WGL.ChoosePixelFormat(m_uint_DC, ref pfd);
            if (pixelFormatIndex == 0)
            {
                MessageBox.Show("Unable to retrieve pixel format");
                return;
            }

            if (WGL.SetPixelFormat(m_uint_DC, pixelFormatIndex, ref pfd) == 0)
            {
                MessageBox.Show("Unable to set pixel format");
                return;
            }
            //Create rendering context
            m_uint_RC = WGL.wglCreateContext(m_uint_DC);
            if (m_uint_RC == 0)
            {
                MessageBox.Show("Unable to get rendering context");
                return;
            }
            if (WGL.wglMakeCurrent(m_uint_DC, m_uint_RC) == 0)
            {
                MessageBox.Show("Unable to make rendering context current");
                return;
            }

            initRenderingGL();
        }

        public void OnResize()
        {
            Width = p.Width;
            Height = p.Height;
            GL.glViewport(0, 0, Width, Height);
            Draw();
        }

        public void initRenderingGL()
		{
            if (m_uint_DC == 0 || m_uint_RC == 0)
                return;
            if (this.Width == 0 || this.Height == 0)
                return;

            GL.glShadeModel(GL.GL_SMOOTH);
            GL.glClearColor(1.0f, 1.0f, 1.0f, 0.0f);
            GL.glClearDepth(1.0f);

            GL.glEnable(GL.GL_LIGHT0);
            GL.glEnable(GL.GL_COLOR_MATERIAL);
            GL.glColorMaterial(GL.GL_FRONT_AND_BACK, GL.GL_AMBIENT_AND_DIFFUSE); // GL.GL_AMBIENT_AND_DIFFUSE / GL.GL_SHININESS


            //GL.glEnable(GL.GL_DEPTH_TEST);
            GL.glDepthFunc(GL.GL_LEQUAL);

            GL.glViewport(0, 0, this.Width, this.Height);
            GL.glMatrixMode(GL.GL_PROJECTION);
            GL.glLoadIdentity();

            GLU.gluPerspective(45, ((double)Width) / Height, 1.0, 1000.0);
            GL.glMatrixMode(GL.GL_MODELVIEW);
            //save the current MODELVIEW Matrix (now it is Identity)
            //GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, AccumulatedRotationsTraslations);

            InitTexture("1.bmp");
        }

        public uint[] texture;      // texture

        void InitTexture(string imageBMPfile) // Update from P2
        {
            GL.glEnable(GL.GL_TEXTURE_2D);

            texture = new uint[1];		// storage for texture

            Bitmap image = new Bitmap(imageBMPfile);
            image.RotateFlip(RotateFlipType.RotateNoneFlipY); //Y axis in Windows is directed downwards, while in OpenGL-upwards
            System.Drawing.Imaging.BitmapData bitmapdata;
            Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);

            bitmapdata = image.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly,
                System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            GL.glGenTextures(1, texture);
            GL.glBindTexture(GL.GL_TEXTURE_2D, texture[0]);
            //  VN-in order to use System.Drawing.Imaging.BitmapData Scan0 I've added overloaded version to
            //  OpenGL.cs
            //  [DllImport(GL_DLL, EntryPoint = "glTexImage2D")]
            //  public static extern void glTexImage2D(uint target, int level, int internalformat, int width, int height, int border, uint format, uint type, IntPtr pixels);
            GL.glTexImage2D(GL.GL_TEXTURE_2D, 0, (int)GL.GL_RGB8, image.Width, image.Height,
                0, GL.GL_BGR_EXT, GL.GL_UNSIGNED_byte, bitmapdata.Scan0);

            GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MIN_FILTER, (int)GL.GL_LINEAR);		// Linear Filtering
            GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MAG_FILTER, (int)GL.GL_LINEAR);		// Linear Filtering

            image.UnlockBits(bitmapdata);
            image.Dispose();
            GenerateTextures();
        }
    }

}