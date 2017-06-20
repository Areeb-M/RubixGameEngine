using Rubix;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace RubixLIB.Graphics
{
    class Window : GameWindow
    {

        public static GameWindowFlags FULLSCREEN = GameWindowFlags.Fullscreen;
        public static GameWindowFlags DEFAULT = GameWindowFlags.Default;
        public static GameWindowFlags FIXED = GameWindowFlags.FixedWindow;

        private Color4 background;

        public Window(int width, int height, string title, GameWindowFlags windowMode) : 
            base(width, height,  GraphicsMode.Default, title, windowMode, DisplayDevice.Default,
                4, 0, GraphicsContextFlags.ForwardCompatible)
        {
            this.background = new Color4(0, 0, 0, 1);
        }

        public void Show()
        {
            this.Visible = true;
            Debug.Log("Window shown.");
        }

        public void Hide()
        {
            this.Visible = false;
            Debug.Log("Window hidden.");
        }

        public void SetScreenResolution(int width, int height)
        {
            Width = width;
            Height = height;
            ResizeViewport();
        }

        private void ResizeViewport()
        {
            GL.Viewport(0, 0, Width, Height);
        }

        #region SetBackgroundColor()
        public void SetBackgroundColor(Color4 color)
        {
            background = color;
        }
        public void SetBackgroundColor(float r, float g, float b, float a)
        {
            background = new Color4(r, g, b, a);
        }
        #endregion

        public bool CanBeginRender()
        {
            ProcessEvents();
            if (Exists && !IsExiting)
            {
                GL.ClearColor(background);
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
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
