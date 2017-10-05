﻿using Rubix;
using System;
using OpenTK;
using RubixLIB.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace RubixLIB
{
    public class Sprite : Entity
    {
        private int width;
        private int height;

        private Texture2D[] costumes;

        #region Width and Height properties
        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }
        #endregion

        public Sprite(Vector4 velocity, Vector4 position) : base(velocity, position)
        {
                   
        }

        public override void Load()
        {
            
        }

        public override void Draw(float timeElapsed)
        {
            if (program != null)
                program.UseProgram();

            GL.DrawArrays(PrimitiveType.Points, 0, 1);
            GL.PointSize(10);

            Vector4 tempPosition = TempFixedUpdate(timeElapsed);

            GL.UseProgram(0);
        }
    }
}
