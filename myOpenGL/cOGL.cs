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

        public int intOptionC = 0;

        public float[] ScrollValue = new float[14] { 1, -10, 10, 0, 0, 0, 0, 1, 0, -0.6f, -3, -2, 3, 4.1f };
        public float[] pos = new float[4] { -0.1f, 4.3f, 2f, -1f };
        double[] AccumulatedRotationsTraslations = new double[16];

        public cOGL(Control pb)
        {
            p=pb;
            Width = p.Width;
            Height = p.Height; 
            InitializeGL();

            rubiksCube = new RubiksCube();
            backMirrorSurface = new Mirror(mirrorHeight, mirrorWidth, -mirrorWidth / 2, 0, -mirrorHeight / 2, 0, 0, 0);
            rightMirrorSurface = new Mirror(mirrorHeight, mirrorWidth*1.5, mirrorWidth / 2, 0, -mirrorHeight / 2, 0, -90, 0);
            leftMirrorSurface = new Mirror(mirrorHeight, mirrorWidth*1.5, -mirrorWidth / 2, 0, -mirrorHeight / 2, 0, -90, 0);
            
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
            leftMirrorSurface.Draw();
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
            rightMirrorSurface.Draw();
            rubiksCube.Draw();
            GL.glPopMatrix();

            // really draw wall 
            //( half-transparent ( see its color's alpha byte)))
            // in order to see reflected objects 
            GL.glDepthMask((byte)GL.GL_FALSE);
            backMirrorSurface.Draw();
            rightMirrorSurface.Draw();
            leftMirrorSurface.Draw();
            GL.glDepthMask((byte)GL.GL_TRUE);
            // Disable GL.GL_STENCIL_TEST to show All, else it will be cut on GL.GL_STENCIL
            GL.glDisable(GL.GL_STENCIL_TEST);

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
            pos[0] = ScrollValue[10];
            pos[1] = ScrollValue[11];
            pos[2] = ScrollValue[12];
            pos[3] = 1.0f;
        }

        void UpdateLightSettings()
        {
            GL.glClear(GL.GL_COLOR_BUFFER_BIT | GL.GL_DEPTH_BUFFER_BIT);

            GL.glLoadIdentity();

            // not trivial
            double[] ModelVievMatrixBeforeSpecificTransforms = new double[16];
            double[] CurrentRotationTraslation = new double[16];

            GLU.gluLookAt(ScrollValue[0], ScrollValue[1], ScrollValue[2],
                       ScrollValue[3], ScrollValue[4], ScrollValue[5],
                       ScrollValue[6], ScrollValue[7], ScrollValue[8]);
            GL.glTranslatef(0.0f, 0.0f, -1.0f);

            //save current ModelView Matrix values
            //in ModelVievMatrixBeforeSpecificTransforms array
            //ModelView Matrix ========>>>>>> ModelVievMatrixBeforeSpecificTransforms
            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, ModelVievMatrixBeforeSpecificTransforms);
            //ModelView Matrix was saved, so
            GL.glLoadIdentity(); // make it identity matrix

            //as result - the ModelView Matrix now is pure representation
            //of KeyCode transform and only it !!!

            //save current ModelView Matrix values
            //in CurrentRotationTraslation array
            //ModelView Matrix =======>>>>>>> CurrentRotationTraslation
            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, CurrentRotationTraslation);

            //The GL.glLoadMatrix function replaces the current matrix with
            //the one specified in its argument.
            //The current matrix is the
            //projection matrix, modelview matrix, or texture matrix,
            //determined by the current matrix mode (now is ModelView mode)
            GL.glLoadMatrixd(AccumulatedRotationsTraslations); //Global Matrix

            //The GL.glMultMatrix function multiplies the current matrix by
            //the one specified in its argument.
            //That is, if M is the current matrix and T is the matrix passed to
            //GL.glMultMatrix, then M is replaced with M • T
            GL.glMultMatrixd(CurrentRotationTraslation);

            //save the matrix product in AccumulatedRotationsTraslations1
            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, AccumulatedRotationsTraslations);

            //replace ModelViev Matrix with stored ModelVievMatrixBeforeSpecificTransforms
            GL.glLoadMatrixd(ModelVievMatrixBeforeSpecificTransforms);
            //multiply it by KeyCode defined AccumulatedRotationsTraslations1 matrix
            GL.glMultMatrixd(AccumulatedRotationsTraslations);
        }

        void DrawLights()
        {
            GL.glPushMatrix();
            GL.glLightfv(GL.GL_LIGHT0, GL.GL_POSITION, pos);

            //Draw Light Source
            GL.glDisable(GL.GL_LIGHTING);
            GL.glTranslatef(pos[0], pos[1], pos[2]);
            //Yellow Light source
            GL.glColor3f(1, 1, 0);
            GLUT.glutSolidSphere(0.05, 8, 8);
            GL.glTranslatef(-pos[0], -pos[1], -pos[2]);

            //main System draw
            GL.glEnable(GL.GL_LIGHTING);
            GL.glPopMatrix();
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
            
            UpdateScrollInput();
            //UpdateLightSettings();
            DrawLights();

            DrawMirrors();

            rubiksCube.Draw();

            // draw simple cube instead
            //GL.glColor3d(0.8, 0.8, 0.8);
            //GLUT.glutSolidCube(1);

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
            GL.glColorMaterial(GL.GL_FRONT_AND_BACK, GL.GL_AMBIENT_AND_DIFFUSE);

            GL.glEnable(GL.GL_DEPTH_TEST);
            GL.glDepthFunc(GL.GL_LEQUAL);

            GL.glViewport(0, 0, this.Width, this.Height);
            GL.glMatrixMode(GL.GL_PROJECTION);
            GL.glLoadIdentity();

            GLU.gluPerspective(45, ((double)Width) / Height, 1.0, 1000.0);
            GL.glMatrixMode(GL.GL_MODELVIEW);
            //save the current MODELVIEW Matrix (now it is Identity)
            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, AccumulatedRotationsTraslations);


           
        }
    
    }

}

