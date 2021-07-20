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
        public List<FaceCube<Color>> prevFaceColors;
        bool isPrevColorShade;
        private readonly object syncLock = new object();

        public int AngleX, AngleY, AngleZ;

        public RubiksCube()
        {
            pendingAnimation = new Queue<Animation>();
            composingCubes = new List<Cube>();
            insideColor = Color.Black;
            prevFaceColors = new List<FaceCube<Color>>();
            isPrevColorShade = false;

            //FaceCube<bool> isInsideFaceColors = new FaceCube<bool>(false, false, false, false, false, false);

            /*
             isInsideFaceColors.Top = isInsideFaceColors.Bottom = isInsideFaceColors.Right = isInsideFaceColors.Left = 
                            isInsideFaceColors.Front = isInsideFaceColors.Back = false;
             */

            /* Drow 3*3*3 black cubes so the user 
             * wont see blank holes between the colored cubes */
            double blockSpace = 1.1;
            var internalCubeColor = new FaceCube<Color>(Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black);
            for (double x = -blockSpace; x <= blockSpace; x += blockSpace)
            {
                for (double y = -blockSpace; y <= blockSpace; y += blockSpace)
                {
                    for (double z = -blockSpace; z <= blockSpace; z += blockSpace)
                    {
                        FaceCube<bool> isInsideFaceColors = new FaceCube<bool>(true, true, true, true, true, true);
                        //var cubeColor = this.GenerateCubeColor(x, y, z, internalCubeColor, ref isInsideFaceColors);
                        //composingCubes.Add(new Cube(.55f, x, y, z, isInsideFaceColors, false));
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
                        var outerCubeColor = new FaceCube<Color>(Color.Green, Color.Blue, Color.White, Color.Yellow, Color.Orange, Color.Red);
                        FaceCube<bool> isInsideFaceColors = new FaceCube<bool>(false, false, false, false, false, false);
                        var cubeColor = this.GenerateCubeColor(x, y, z, outerCubeColor, ref isInsideFaceColors);
                        composingCubes.Add(new Cube(.60f, x, y, z, cubeColor, isInsideFaceColors, false));
                    }
                }
            }

            for (int i = 0; i < composingCubes.Count; i++)
            {
                prevFaceColors.Add(composingCubes[i].faceColors);
            }
        }

        private FaceCube<Color> GenerateCubeColor(double x, double y, double z, FaceCube<Color> everyCubeDefaultFaceColor, ref FaceCube<bool> isInsideFaceColors)
        {
            var cubeColor = everyCubeDefaultFaceColor;

            if (x < 0)
            {
                cubeColor.Right = insideColor;
                isInsideFaceColors.Right = true;
            }
            if (x == 0)
            {
                cubeColor.Left = insideColor;
                cubeColor.Right = insideColor;
                isInsideFaceColors.Left = true;
                isInsideFaceColors.Right = true;
            }
            if (x > 0)
            {
                cubeColor.Left = insideColor;
                isInsideFaceColors.Left = true;
            }
            if (y < 0)
            {
                cubeColor.Top = insideColor;
                isInsideFaceColors.Top = true;
            }
            if (y == 0)
            {
                cubeColor.Top = insideColor;
                cubeColor.Bottom = insideColor;
                isInsideFaceColors.Top = true;
                isInsideFaceColors.Bottom = true;
            }
            if (y > 0)
            {
                cubeColor.Bottom = insideColor;
                isInsideFaceColors.Bottom = true;
            }
            if (z < 0)
            {
                cubeColor.Front = insideColor;
                isInsideFaceColors.Front = true;
            }
            if (z == 0)
            {
                cubeColor.Front = insideColor;
                cubeColor.Back = insideColor;
                isInsideFaceColors.Front = true;
                isInsideFaceColors.Back = true;
            }
            if (z > 0)
            {
                cubeColor.Back = insideColor;
                isInsideFaceColors.Back = true;
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
                // update insideColorArray befaor drawing
                var outerCubeColor = new FaceCube<Color>(Color.Green, Color.Blue, Color.White, Color.Yellow, Color.Orange, Color.Red);
                FaceCube<bool> isInsideFaceColors = new FaceCube<bool>(false, false, false, false, false, false);
                this.GenerateCubeColor(item.X, item.Y, item.Z, outerCubeColor, ref isInsideFaceColors);
                item.isInsideFaceColors = isInsideFaceColors;

                // draw the cubes
                item.Draw(); 
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
                        composingCubes[i].isShadowState = true;
                    }
                    isPrevColorShade = true;
                }
            }
            else
            {
                for (int i = 0; i < composingCubes.Count; i++)
                {
                    composingCubes[i].isShadowState = false;
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
