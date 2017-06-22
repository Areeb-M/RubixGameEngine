using Rubix;
using System;
using OpenTK;
using RubixLIB.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace RubixLIB
{
    class Sprite : Entity
    {
        private int width;
        private int height;

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

        public Sprite(Vector4 velocity, Vector4 position, int width, int height) : base(velocity, position)
        {
            this.width = width;
            this.height = height;            
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
