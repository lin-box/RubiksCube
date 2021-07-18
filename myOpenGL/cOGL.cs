using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using RubikCube;
using RubikCube.Draws;

/**
 * חובה
 *1. לחצני רדיו לחדרים
 *2. להוסיף את הקוד של קיוב מפ כחדר
 *3. נורמלים לקירות של צל
 *4. לסדר פקדים במסך לפי חדרים
 *5. להוסיף טקסט ללחצנים
 *6.
 *
 *רצוי
 *1. בליינד לצל
 *2. להוסיף חומר לקוביה
 *3. קירות מתחלפים בקיוב מפ לפי משהו..
 *4.
 * 
 * 
 *
 * 
 * 
 */

namespace OpenGL
{
    class cOGL
    {
        Control p;
        int Width;
        int Height;
        double mirrorHeight = 12;
        double mirrorWidth = 9;

        const int x = 0;
        const int y = 1;
        const int z = 2;

        public Mirror backMirrorSurface;
        public Mirror rightMirrorSurface;
        public Mirror leftMirrorSurface;
        public RubiksCube rubiksCube;

        public uint[] texture = new uint[9];      // texture : [0] = mirror, [1] = bakground, [2] = ..
        public float[] ScrollValue = new float[14];
        float[] backWallColorArray = new float[4]   { 1.0f, 1.0f, 1.0f, 1f };    //{ 0.9f, 0.9f, 0.5f, 1f };
        float[] leftWallColorArray = new float[4]   { 1.0f, 1.0f, 1.0f, 1f };    //{ 0.8f, 0.9f, 0.6f, 1f };
        float[] rightWallColorArray = new float[4]  { 1.0f, 1.0f, 1.0f, 1f };    //{ 0.8f, 0.9f, 0.6f, 1f };
        float[] backMinusArray = new float[3] { 0f, 0.0f, -0.01f };
        float[] leftMinusArray = new float[3] { 0f, 0f, 0.01f };
        float[] rightMinusArray = new float[3] { 0f, 0f, -0.01f };
        float[] zeroArray = new float[3] { 0f, 0f, 0f };
        float[] planeCoeff = { 1, 1, 1, 1 };
        public float[] pos = new float[4];
        float[] cubeXform = new float[16];
        public float[] cubemapXYZAngles = new float[3] { 0, 0, 0 }; // cube map
        public int viewAngle = 70;

        public cOGL(Control pb)
        {
            p = pb;
            Width = p.Width;
            Height = p.Height;
            InitializeGL();
            InitTextures();

            rubiksCube = new RubiksCube();
            backMirrorSurface = new Mirror(mirrorHeight, mirrorWidth, -mirrorWidth / 2, 0, -mirrorWidth / 2, 0, 0, 0, false, texture);
            rightMirrorSurface = new Mirror(mirrorHeight, mirrorWidth * 1.3, mirrorWidth / 2, 0, -mirrorWidth / 2, 0, -90, 0, false, texture);
            leftMirrorSurface = new Mirror(mirrorHeight, mirrorWidth * 1.3, -mirrorWidth / 2, 0, -mirrorWidth / 2, 0, -90, 0, true, texture);

        }

        ~cOGL()
        {
            WGL.wglDeleteContext(m_uint_RC);
        }

        uint m_uint_HWND = 0;

        public uint HWND
        {
            get { return m_uint_HWND; }
        }

        uint m_uint_DC = 0;

        public uint DC
        {
            get { return m_uint_DC; }
        }
        uint m_uint_RC = 0;

        public uint RC
        {
            get { return m_uint_RC; }
        }

        void DrawMirrors()
        {
            //only wall, draw only to STENCIL buffer
            GL.glEnable(GL.GL_STENCIL_TEST);
            GL.glStencilOp(GL.GL_REPLACE, GL.GL_REPLACE, GL.GL_REPLACE); // change stencil according to the object color
            GL.glStencilFunc(GL.GL_ALWAYS, 1, 0xFFFFFFFF); // draw wall always
            GL.glColorMask((byte)GL.GL_FALSE, (byte)GL.GL_FALSE, (byte)GL.GL_FALSE, (byte)GL.GL_FALSE);
            GL.glDisable(GL.GL_DEPTH_TEST);
            // add mirrors for STENCIL buffer
            backMirrorSurface.Draw();
            rightMirrorSurface.Draw();
            leftMirrorSurface.Draw();

            // restore regular settings
            GL.glColorMask((byte)GL.GL_TRUE, (byte)GL.GL_TRUE, (byte)GL.GL_TRUE, (byte)GL.GL_TRUE);
            GL.glEnable(GL.GL_DEPTH_TEST);

            // first draw the real cube in DEPTH_TEST
            GL.glPushMatrix();
            rubiksCube.Draw();
            GL.glPopMatrix();

            // reflection is drawn only where STENCIL buffer value equal to 1
            GL.glStencilFunc(GL.GL_EQUAL, 1, 0xFFFFFFFF);
            GL.glStencilOp(GL.GL_KEEP, GL.GL_KEEP, GL.GL_KEEP); // keep object origenal color
            GL.glEnable(GL.GL_STENCIL_TEST);


            // draw reflected scene for back mirror
            GL.glPushMatrix();
            GL.glScalef(1, 1, -1); //swap on Z axis
            GL.glTranslated(0, 0, mirrorWidth);
            rightMirrorSurface.Draw();
            leftMirrorSurface.Draw();
            rubiksCube.Draw();
            GL.glPopMatrix();


            //   Draw scene in right mirror

            // draw reflected back mirror scene in right mirror stage 1 scene
            GL.glPushMatrix();
            GL.glTranslated(mirrorWidth, 0, -mirrorWidth);
            GL.glScalef(-1, 1, -1); //swap on X & Z axis
            leftMirrorSurface.Draw();
            rubiksCube.Draw();
            GL.glPopMatrix();

            // draw reflected back mirror scene in right mirror stage 2 scene
            GL.glPushMatrix();
            GL.glTranslated(mirrorWidth * 2, 0, -mirrorWidth);
            GL.glScalef(1, 1, -1); //swap on Z axis
            rightMirrorSurface.Draw();
            rubiksCube.Draw();
            GL.glPopMatrix();

            // draw reflected left mirror scene in right mirror stage 2 scene
            GL.glPushMatrix();
            //GL.glScalef(-1, 1, 1); //swap on X axis
            GL.glTranslated(mirrorWidth * 2, 0, 0);
            backMirrorSurface.Draw();
            rightMirrorSurface.Draw();
            rubiksCube.Draw();
            GL.glPopMatrix();

            // draw reflected scene for right mirror stage 1
            GL.glPushMatrix();
            GL.glScalef(-1, 1, 1); //swap on X axis
            GL.glTranslated(-mirrorWidth, 0, 0);
            leftMirrorSurface.Draw();
            backMirrorSurface.Draw();
            rubiksCube.Draw();
            GL.glPopMatrix();


            //   Draw scene in left mirror

            // draw reflected back mirror scene in left mirror stage 1 scene
            GL.glPushMatrix();
            GL.glTranslated(-mirrorWidth, 0, -mirrorWidth);
            GL.glScalef(-1, 1, -1); //swap on Z axis
            rightMirrorSurface.Draw();
            rubiksCube.Draw();
            GL.glPopMatrix();

            // draw reflected back mirror scene in left mirror stage 2 scene
            GL.glPushMatrix();
            GL.glTranslated(-mirrorWidth * 2, 0, -mirrorWidth);
            GL.glScalef(1, 1, -1); //swap on Z axis
            leftMirrorSurface.Draw();
            rubiksCube.Draw();
            GL.glPopMatrix();

            // draw reflected right mirror scene in left mirror stage 2 scene
            GL.glPushMatrix();
            //GL.glScalef(-1, 1, 1); //swap on X axis
            GL.glTranslated(-mirrorWidth * 2, 0, 0);
            backMirrorSurface.Draw();
            leftMirrorSurface.Draw();
            rubiksCube.Draw();
            GL.glPopMatrix();

            // draw reflected scene for left mirror stage 1
            GL.glPushMatrix();
            GL.glScalef(-1, 1, 1); //swap on X axis
            GL.glTranslated(mirrorWidth, 0, 0);
            rightMirrorSurface.Draw();
            backMirrorSurface.Draw();
            rubiksCube.Draw();
            GL.glPopMatrix();


            // really draw wall 
            //( half-transparent ( see its color's alpha byte)))
            // in order to see reflected objects 
            GL.glDepthMask((byte)GL.GL_FALSE);
            backMirrorSurface.Draw();

            if (ScrollValue[1] < (4.5 + 5 * ((-1.0 / 15) * (-90 - rightMirrorSurface.AngleY))) && ScrollValue[1] > -(4.5 + 5 * ((-1.0 / 15) * (-90 - rightMirrorSurface.AngleY))))
            {
                rightMirrorSurface.Draw(); //rightMinusArray
                leftMirrorSurface.Draw(); //leftMinusArray
            }
            else if (ScrollValue[1] < (4.5 + 5 * ((-1.0 / 15) * (-90 - rightMirrorSurface.AngleY))))
            {
                leftMirrorSurface.DrawAsWall(leftWallColorArray, zeroArray); //, leftMinusArray
                rightMirrorSurface.Draw(); //rightMinusArray
            }
            else
            {
                rightMirrorSurface.DrawAsWall(rightWallColorArray, zeroArray); //, rightMinusArray
                leftMirrorSurface.Draw(); //leftMinusArray
            }

            GL.glDepthMask((byte)GL.GL_TRUE);
            // Disable GL.GL_STENCIL_TEST to show All, else it will be cut on GL.GL_STENCIL
            GL.glDisable(GL.GL_STENCIL_TEST);

        }

        void DrawAxes(Color xColor, Color yColor, Color zColor)
        {
            GL.glBegin(GL.GL_LINES);
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

        void ReduceToUnit(float[] vector)
        {
            // Reduces a normal vector specified as a set of three coordinates,
            // to a unit normal vector of length one.

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

        void calcNormal(float[,] v, float[] outp)
        {
            // Points p1, p2, & p3 specified in counter clock-wise order

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

        void MakeShadowMatrix(float[,] points)
        {

            //float[] planeCoeff = new float[4];
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
            cubeXform[0] = dot - pos[0] * planeCoeff[0];
            cubeXform[4] = 0.0f - pos[0] * planeCoeff[1];
            cubeXform[8] = 0.0f - pos[0] * planeCoeff[2];
            cubeXform[12] = 0.0f - pos[0] * planeCoeff[3];

            // Second column
            cubeXform[1] = 0.0f - pos[1] * planeCoeff[0];
            cubeXform[5] = dot - pos[1] * planeCoeff[1];
            cubeXform[9] = 0.0f - pos[1] * planeCoeff[2];
            cubeXform[13] = 0.0f - pos[1] * planeCoeff[3];

            // Third Column
            cubeXform[2] = 0.0f - pos[2] * planeCoeff[0];
            cubeXform[6] = 0.0f - pos[2] * planeCoeff[1];
            cubeXform[10] = dot - pos[2] * planeCoeff[2];
            cubeXform[14] = 0.0f - pos[2] * planeCoeff[3];

            // Fourth Column
            cubeXform[3] = 0.0f - pos[3] * planeCoeff[0];
            cubeXform[7] = 0.0f - pos[3] * planeCoeff[1];
            cubeXform[11] = 0.0f - pos[3] * planeCoeff[2];
            cubeXform[15] = dot - pos[3] * planeCoeff[3];
        }

        void DrawLight()
        {
            GL.glDisable(GL.GL_TEXTURE_2D);
            GL.glDisable(GL.GL_STENCIL_TEST);

            GL.glPushMatrix();

            //Yellow Light source
            GL.glColor3f(1, 1, 0);
            GL.glTranslatef(pos[0], pos[1], pos[2]);
            GLUT.glutSolidSphere(0.1, 8, 8);
            GL.glTranslatef(-pos[0], -pos[1], -pos[2]);

            float[] light_position = { pos[0], pos[1], pos[2] };
            GL.glLightfv(GL.GL_LIGHT0, GL.GL_POSITION, light_position);
            GL.glEnable(GL.GL_LIGHTING);
            GL.glPopMatrix();
        }

        void DrawShadableWall(Mirror surface, float[] colorArray, float[] minusArray)
        {
            //!!!!!!!!!!!!!
            GL.glPushMatrix();
            //!!!!!!!!!!!!!

            //GL.glDisable(GL.GL_LIGHTING);

            // we want to cut the shadow that is not on this surface
            GL.glEnable(GL.GL_STENCIL_TEST);
            GL.glStencilOp(GL.GL_REPLACE, GL.GL_REPLACE, GL.GL_REPLACE); // change stencil according to the object color
            GL.glStencilFunc(GL.GL_ALWAYS, 1, 0xFFFFFFFF); // draw wall always

            // add surface for STENCIL buffer
            surface.DrawAsWall(colorArray, minusArray); //, backMinusArray

            // shadow is drawn only where STENCIL buffer value equal to 1          
            GL.glStencilFunc(GL.GL_EQUAL, 1, 0xFFFFFFFF);
            GL.glStencilOp(GL.GL_KEEP, GL.GL_KEEP, GL.GL_KEEP); // keep object origenal color

            //end of regular show
            //!!!!!!!!!!!!!
            GL.glPopMatrix();
            //!!!!!!!!!!!!!
        }

        void DrawShadowsOnSurface(Mirror surface)
        {
            //SHADING begin
            //we'll define cubeXform matrix in MakeShadowMatrix Sub
            // Disable lighting, we'll just draw the shadow
            //else instead of shadow we'll see stange projection of the same objects
            GL.glDisable(GL.GL_LIGHTING);

            // wall shadow
            //!!!!!!!!!!!!!
            GL.glPushMatrix();
            //!!!!!!!!!!!!   
            MakeShadowMatrix(surface.getSurf());
            GL.glMultMatrixf(cubeXform);

            GL.glEnable(GL.GL_STENCIL_TEST);

            DrawObjects(true);
            //!!!!!!!!!!!!!
            GL.glPopMatrix();
            //!!!!!!!!!!!!!
            GL.glDisable(GL.GL_STENCIL_TEST);
        }

        void DrawFigures()
        {
            DrawLight();

            DrawShadableWall(backMirrorSurface, backWallColorArray, backMinusArray);
            DrawShadableWall(rightMirrorSurface, rightWallColorArray, rightMinusArray);
            DrawShadableWall(leftMirrorSurface, leftWallColorArray, leftMinusArray);

            DrawLight();
            DrawObjects(false);
            DrawShadowsOnSurface(backMirrorSurface);

            DrawLight();
            DrawObjects(false);
            DrawShadowsOnSurface(rightMirrorSurface);

            DrawLight();
            DrawObjects(false);
            DrawShadowsOnSurface(leftMirrorSurface);

        }

        void DrawObjects(bool isForShades)
        {
            GL.glPushMatrix();
            rubiksCube.SetShadows(isForShades);
            rubiksCube.Draw();
            GL.glPopMatrix();
        }

        void DrawBackground()
        {
            //GL.glDisable(GL.GL_BLEND);
            GL.glTranslated(0, 0f, -5);

            GL.glEnable(GL.GL_TEXTURE_2D);
            GL.glBindTexture(GL.GL_TEXTURE_2D, texture[1]);
            GL.glBegin(GL.GL_QUADS);

            GL.glTexCoord2f(0,0);
            GL.glVertex3f(-20, -25, 0);
            GL.glTexCoord2f(0, 1);
            GL.glVertex3f(-20, 25, 0);
            GL.glTexCoord2f(1, 1);
            GL.glVertex3f(20, 25, 0);
            GL.glTexCoord2f(1, 0);
            GL.glVertex3f(20, -25, 0);

            GL.glEnd();
            GL.glDisable(GL.GL_TEXTURE_2D);
            GL.glTranslated(0, 0f, 5);
            //GL.glEnable(GL.GL_BLEND);
        }

        void DrawTexturedCube()
        {
            float big = 1.0f;
            float small = big;

            // front
            GL.glBindTexture(GL.GL_TEXTURE_2D, texture[3]);
            GL.glBegin(GL.GL_QUADS);
            GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(-big, -big, big);
            GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(big, -big, big);
            GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(big, big, big);
            GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(-big, big, big);
            GL.glEnd();
            // back
            GL.glBindTexture(GL.GL_TEXTURE_2D, texture[4]);
            GL.glBegin(GL.GL_QUADS);
            GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(small, -small, -small);
            GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(-small, -small, -small);
            GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(-small, small, -small);
            GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(small, small, -small);
            GL.glEnd();
            // left
            GL.glBindTexture(GL.GL_TEXTURE_2D, texture[5]);
            GL.glBegin(GL.GL_QUADS);
            GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(-small, -small, -small);
            GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(-big, -big, big);
            GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(-big, big, big);
            GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(-small, small, -small);
            GL.glEnd();
            // right
            GL.glBindTexture(GL.GL_TEXTURE_2D, texture[6]);
            GL.glBegin(GL.GL_QUADS);
            GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(big, -big, big);
            GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(small, -small, -small);
            GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(small, small, -small);
            GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(big, big, big);
            GL.glEnd();
            // top
            GL.glBindTexture(GL.GL_TEXTURE_2D, texture[7]);
            GL.glBegin(GL.GL_QUADS);
            GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(-big, big, big);
            GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(big, big, big);
            GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(small, small, -small);
            GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(-small, small, -small);
            GL.glEnd();
            // bottom
            GL.glBindTexture(GL.GL_TEXTURE_2D, texture[8]);
            GL.glBegin(GL.GL_QUADS);
            GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(-small, -small, -small);
            GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(small, -small, -small);
            GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(big, -big, big);
            GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(-big, -big, big);
            GL.glEnd();
        }

        public void update_cube_map_rotations()
        {
            GL.glTranslatef(0.0f, 0.0f, -1.4f);

            GL.glRotatef(cubemapXYZAngles[0], 1.0f, 0.0f, 0.0f);
            GL.glRotatef(cubemapXYZAngles[1], 0.0f, 1.0f, 0.0f);
            GL.glRotatef(cubemapXYZAngles[2], 0.0f, 0.0f, 1.0f);

            GL.glDisable(GL.GL_LIGHTING);
            GL.glDisable(GL.GL_TEXTURE_2D);

            //DrawBounds();

            GL.glColor4f(1.0f, 1.0f, 1.0f, 0.5f);
            GL.glEnable(GL.GL_TEXTURE_2D);
            DrawTexturedCube();
            GL.glColor4f(1.0f, 1.0f, 1.0f, 1);
            GL.glDisable(GL.GL_TEXTURE_2D);
        }

        public void update_cube_map_prespective()
        {
            GL.glMatrixMode(GL.GL_PROJECTION);
            GL.glLoadIdentity();

            GLU.gluPerspective(viewAngle, (float)Width / (float)Height, 0.45f, 30.0f);

            GL.glMatrixMode(GL.GL_MODELVIEW);
            GL.glLoadIdentity();
        }

        void DrawRoom()
        {

            GL.glPushMatrix();
            
            GL.glTranslatef(0.0f, 0.0f, -20.4f);
            //update_cube_map_prespective();
            GL.glTranslatef(0.0f, 0.0f, +20.4f);

            GL.glLoadIdentity();

            //GL.glTranslatef(0.0f, 0.0f, -20.4f);
            //update_cube_map_rotations();
            //GL.glTranslatef(0.0f, 0.0f, +20.4f);

            GL.glPopMatrix();
            GL.glLoadIdentity();

            //GL.glEnable(GL.GL_LIGHTING);
            GL.glTranslatef(0.0f, 0.0f, -7.4f);
            DrawObjects(false);
            GL.glTranslatef(0.0f, 0.0f, +7.4f);
            //GL.glDisable(GL.GL_LIGHTING);

        }

        public void Draw()
        {
            //Shadows
            pos[0] = ScrollValue[10];
            pos[1] = ScrollValue[11];
            pos[2] = ScrollValue[12];
            pos[3] = 1.0f;
            //Shadows

            if (m_uint_DC == 0 || m_uint_RC == 0)
                return;

            GL.glClear(GL.GL_COLOR_BUFFER_BIT | GL.GL_DEPTH_BUFFER_BIT | GL.GL_STENCIL_BUFFER_BIT);

            GL.glLoadIdentity();

            GLU.gluLookAt(ScrollValue[1], 2, 10, 0, 0, 0, 0, 1, 0);

            GL.glTranslated(0, 0, -20);

            //DrawBackground();

            GL.glRotated(20, 1, 0, 0);

            GL.glTranslated(0, 2, 8);

            //DrawAxes(Color.Red, Color.Green, Color.Blue);  

            DrawFigures();

            //DrawMirrors();

            //DrawRoom();
            //DrawObjects(false);


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
            //GL.glMateriali(GL.GL_FRONT_AND_BACK, GL.GL_SHININESS, 100);
            //GL.glLightfv(GL.GL_LIGHT1, GL.GL_SPECULAR, pos); //GL.GL_POSITION , GL.GL_SPECULAR , GL.GL_SPOT_DIRECTION , GL.GL_DIFFUSE

            GL.glEnable(GL.GL_DEPTH_TEST);
            GL.glDepthFunc(GL.GL_LEQUAL);

            GL.glViewport(0, 0, this.Width, this.Height);
            GL.glMatrixMode(GL.GL_PROJECTION);
            GL.glLoadIdentity();

            GLU.gluPerspective(45, ((double)Width) / Height, 1.0, 1000.0);
            GL.glMatrixMode(GL.GL_MODELVIEW);
            //save the current MODELVIEW Matrix (now it is Identity)
            //GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, AccumulatedRotationsTraslations);
        }

        void InitTextures()
        {
            GenerateTextures();
        }

        void GenerateTextures()
        {
            GL.glGenTextures(9, texture);
            string[] imagesName = { "IMG\\3.bmp", "IMG\\bluespace.bmp", "IMG\\spaceship_wall3.bmp",
            "IMG\\green.bmp","IMG\\blue.bmp", "IMG\\orange.bmp","IMG\\red.bmp","IMG\\white.bmp","IMG\\yellow.bmp"};
            for (int i = 0; i < 9; i++)
            {
                Bitmap image = new Bitmap(imagesName[i]);
                image.RotateFlip(RotateFlipType.RotateNoneFlipY); //Y axis in Windows is directed downwards, while in OpenGL-upwards
                System.Drawing.Imaging.BitmapData bitmapdata;
                Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);

                bitmapdata = image.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                GL.glBindTexture(GL.GL_TEXTURE_2D, texture[i]);
                //2D for XYZ
                //The level-of-detail number. Level 0 is the base image level
                //The number of color components in the texture. 
                //Must be 1, 2, 3, or 4, or one of the following 
                //    symbolic constants: 
                //                GL_ALPHA, GL_ALPHA4, 
                //                GL_ALPHA8, GL_ALPHA12, GL_ALPHA16, GL_LUMINANCE, GL_LUMINANCE4, 
                //                GL_LUMINANCE8, GL_LUMINANCE12, GL_LUMINANCE16, GL_LUMINANCE_ALPHA, 
                //                GL_LUMINANCE4_ALPHA4, GL_LUMINANCE6_ALPHA2, GL_LUMINANCE8_ALPHA8, 
                //                GL_LUMINANCE12_ALPHA4, GL_LUMINANCE12_ALPHA12, GL_LUMINANCE16_ALPHA16, 
                //                GL_INTENSITY, GL_INTENSITY4, GL_INTENSITY8, GL_INTENSITY12, 
                //                GL_INTENSITY16, GL_R3_G3_B2, GL_RGB, GL_RGB4, GL_RGB5, GL_RGB8, 
                //                GL_RGB10, GL_RGB12, GL_RGB16, GL_RGBA, GL_RGBA2, GL_RGBA4, GL_RGB5_A1, 
                //                GL_RGBA8, GL_RGB10_A2, GL_RGBA12, or GL_RGBA16.


                GL.glTexImage2D(GL.GL_TEXTURE_2D, 0, (int)GL.GL_RGB8, image.Width, image.Height,
                                                              //The width of the border. Must be either 0 or 1.
                                                              //The format of the pixel data
                                                              //The data type of the pixel data
                                                              //A pointer to the image data in memory
                                                              0, GL.GL_BGR_EXT, GL.GL_UNSIGNED_byte, bitmapdata.Scan0);
                GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MIN_FILTER, (int)GL.GL_LINEAR);
                GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MAG_FILTER, (int)GL.GL_LINEAR);

                image.UnlockBits(bitmapdata);
                image.Dispose();
            }
        }

    }

}