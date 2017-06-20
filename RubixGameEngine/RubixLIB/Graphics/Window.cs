using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace Rubix.RubixLIB.Graphics
{
    class Window : GameWindow
    {
        private Color4 background;

        public Window(int width, int height, string title, GameWindowFlags windowMode,
            Color4 background=new Color4()) : 
            base(width, height,  GraphicsMode.Default, title, windowMode, DisplayDevice.Default,
                4, 0, GraphicsContextFlags.ForwardCompatible)
        {
            this.background = background;
        }

        public void Show()
        {
            this.Visible = true;
        }

        public void Hide()
        {
            this.Visible = false;
        }

        public bool CanBeginRender()
        {
            ProcessEvents();
            if (Exists && !IsExiting)
            {
                GL.ClearColor(background);
                return true;
            }
            return false;
        }

        public void EndRender()
        {
            SwapBuffers();
        }
    }
}
