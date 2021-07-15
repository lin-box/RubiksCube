using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using OpenGL;

namespace RubikCube.Draws
{
    class RubiksCube : IDraw
    {
        List<Cube> composingCubes;
        Queue<Animation> pendingAnimation;
        Animation current;
        Color insideColor;
        public FaceCube<Color> shadowsFaceColors;
        public List<FaceCube<Color>> prevFaceColors;
        bool isPrevColorShade;
        private readonly object syncLock = new object();

        public int AngleX, AngleY, AngleZ;

        public RubiksCube()
        {
            pendingAnimation = new Queue<Animation>();
            composingCubes = new List<Cube>();
            insideColor = Color.Black;
            shadowsFaceColors = new FaceCube<Color>(Color.Gray, Color.Gray, Color.Gray, Color.Gray, Color.Gray, Color.Gray);
            prevFaceColors = new List<FaceCube<Color>>();
            isPrevColorShade = false;

            /* Drow 3*3*3 black cubes so the user 
             * wont see blank holes between the colored cubes */
            double blockSpace = 1.1;
            for (double x = -blockSpace; x <= blockSpace; x += blockSpace)
            {
                for (double y = -blockSpace; y <= blockSpace; y += blockSpace)
                {
                    for (double z = -blockSpace; z <= blockSpace; z += blockSpace)
                    {
                        composingCubes.Add(new Cube(.55f, x, y, z));
                        // half of 1.1 so we get a full black cube
                    }
                }
            }

            /* Drow 3*3*3 colored cubes
             * with block that bigger than the black 
             * and color area that is smaller then the black area */
            blockSpace = 1.32;

            for (double x = -blockSpace; x <= blockSpace; x += blockSpace)
            {
                for (double y = -blockSpace; y <= blockSpace; y += blockSpace)
                {
                    for (double z = -blockSpace; z <= blockSpace; z += blockSpace)
                    {
                        var cubeColor = this.GenerateCubeColor(x, y, z);
                        composingCubes.Add(new Cube(.60f, x, y, z, cubeColor));
                    }
                }
            }

            for (int i = 0; i < composingCubes.Count; i++)
            {
                prevFaceColors.Add(composingCubes[i].faceColors);
            }
        }

        private FaceCube<Color> GenerateCubeColor(double x, double y, double z)
        {
            var cubeColor = new FaceCube<Color>(Color.Green, Color.Blue, Color.White, Color.Yellow, Color.Orange, Color.Red);

            if (x < 0)
            {
                cubeColor.Right = insideColor;
            }
            if (x == 0)
            {
                cubeColor.Left = insideColor;
                cubeColor.Right = insideColor;
            }
            if (x > 0)
            {
                cubeColor.Left = insideColor;
            }
            if (y < 0)
            {
                cubeColor.Top = insideColor;
            }
            if (y == 0)
            {
                cubeColor.Top = insideColor;
                cubeColor.Bottom = insideColor;
            }
            if (y > 0)
            {
                cubeColor.Bottom = insideColor;
            }
            if (z < 0)
            {
                cubeColor.Front = insideColor;
            }
            if (z == 0)
            {
                cubeColor.Front = insideColor;
                cubeColor.Back = insideColor;
            }
            if (z > 0)
            {
                cubeColor.Back = insideColor;
            }

            return cubeColor;
        }

        public void Manipulate(RubikCubeMoviment moviment)
        {
            lock (syncLock)
            {
                List<Cube> movimentingPieces = new List<Cube>();

                if (moviment.Axis == Axis.X)
                {
                    if (moviment.Depth == Depth.First)
                    {
                        movimentingPieces = this.composingCubes.FindAll(pieces => pieces.X < 0);
                    }
                    else if (moviment.Depth == Depth.Second)
                    {
                        movimentingPieces = this.composingCubes.FindAll(pieces => pieces.X == 0);
                    }
                    else if (moviment.Depth == Depth.Third)
                    {
                        movimentingPieces = this.composingCubes.FindAll(pieces => pieces.X > 0);
                    }

                    Animation animation = new Animation(movimentingPieces, 90, moviment);
                    this.pendingAnimation.Enqueue(animation);

                }
                else if (moviment.Axis == Axis.Y)
                {
                    if (moviment.Depth == Depth.First)
                    {
                        movimentingPieces = this.composingCubes.FindAll(pieces => pieces.Y > 0);
                    }
                    else if (moviment.Depth == Depth.Second)
                    {
                        movimentingPieces = this.composingCubes.FindAll(pieces => pieces.Y == 0);
                    }
                    else if (moviment.Depth == Depth.Third)
                    {
                        movimentingPieces = this.composingCubes.FindAll(pieces => pieces.Y < 0);
                    }


                    Animation animation = new Animation(movimentingPieces, 90, moviment);
                    this.pendingAnimation.Enqueue(animation);
                }
                else if (moviment.Axis == Axis.Z)
                {
                    if (moviment.Depth == Depth.First)
                    {
                        movimentingPieces = this.composingCubes.FindAll(pieces => pieces.Z > 0);
                    }
                    else if (moviment.Depth == Depth.Second)
                    {
                        movimentingPieces = this.composingCubes.FindAll(pieces => pieces.Z == 0);
                    }
                    else if (moviment.Depth == Depth.Third)
                    {
                        movimentingPieces = this.composingCubes.FindAll(pieces => pieces.Z < 0);
                    }
                    Animation animation = new Animation(movimentingPieces, 90, moviment);
                    this.pendingAnimation.Enqueue(animation);
                }
            }
        }

        public void Rotate(int AngleAxisX, int AngleAxisY, int AngleAxisZ)
        {
           
            this.AngleX += AngleAxisX;
            this.AngleY += AngleAxisY;
            this.AngleZ += AngleAxisZ;
          
        }

        public void Draw()
        {
            DoAnimation();
            AdjustRotation();
            foreach (var item in composingCubes)
            {
                //GL.glPushMatrix();
                //GL.glTranslated(0, 0, -21);
                //GL.glTranslated(0, -3.5f, 0);
                item.Draw();
                //GL.glTranslated(0, 0, +21);
                //GL.glTranslated(0, +3.5f, 0);
                //GL.glPopMatrix();
            }
        }

        public void SetShadows(bool isShadows)
        {
            if (isShadows)
            {
                if (!isPrevColorShade)
                {
                    for (int i = 0; i < composingCubes.Count; i++)
                    {
                        FaceCube<Color> tempFaceColors = composingCubes[i].faceColors;
                        composingCubes[i].faceColors = shadowsFaceColors;
                        prevFaceColors[i] = tempFaceColors;
                    }
                    isPrevColorShade = true;
                }
            }
            else
            {
                for (int i = 0; i < composingCubes.Count; i++)
                {
                    composingCubes[i].faceColors = prevFaceColors[i];
                }
                isPrevColorShade = false;
            }
        }

        private void DoAnimation()
        {
            if(current == null)
            {
                if (this.pendingAnimation.Count > 0)
                {
                    this.current = this.pendingAnimation.Dequeue();
                }
                else return;
            }
            else
            {
                if (current.AnimationEnded)
                {
                    current = null;
                }
                else
                {
                    current.Animate();
                }
            }
        }

        private void AdjustRotation()
        {
            GL.glRotatef(AngleX, 1, 0, 0);
            GL.glRotatef(AngleY, 0, 1, 0);
            GL.glRotatef(AngleZ, 0, 0, 1);
        }

    }
}
