using Rubix;
using OpenTK;
using OpenTK.Graphics;
using System.Threading;
using OpenTK.Graphics.OpenGL4;

namespace RubixLIB.Graphics
{
    class Window : GameWindow
    {

        public static GameWindowFlags FULLSCREEN = GameWindowFlags.Fullscreen;
        public static GameWindowFlags DEFAULT = GameWindowFlags.Default;
        public static GameWindowFlags FIXED = GameWindowFlags.FixedWindow;

        private Color4 background;

        private ThreadStart swapBuffers;

        public Window(int width, int height, string title, GameWindowFlags windowMode) : 
            base(width, height,  GraphicsMode.Default, title, windowMode, DisplayDevice.Default,
                4, 0, GraphicsContextFlags.ForwardCompatible)
        {
            this.background = new Color4(0, 0, 0, 1);
            swapBuffers = new ThreadStart(SwapBuffers);

            #region VSync Config Check
            bool exists;
            string vsyncMode;

            exists = Config.Exists("VSync");
            if (!exists)
                Config.SetOption("VSync", new string[] { "On" });

            vsyncMode = Config.GetOption("VSync")[0];
            if (vsyncMode != "On" || vsyncMode != "Off" || vsyncMode != "Adaptive")
            {
                Debug.Log("VSync setting was unreadable, resetting to On!");
                vsyncMode = "On";
            }

            if (vsyncMode == "On")
                VSync = VSyncMode.On;
            else if (vsyncMode == "Off")
                VSync = VSyncMode.Off;
            else
                VSync = VSyncMode.Adaptive;
            #endregion
        }

        #region Window Settings
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
        #endregion 

        #region VSync 
        public void ToggleVSync()
        {
            if (VSync == VSyncMode.On || VSync == VSyncMode.Adaptive)
            {
                VSync = VSyncMode.Off;
                Debug.Log("Turned VSync Off");
            }
            else
            {
                VSync = VSyncMode.On;
                Debug.Log("Turned VSync On");
            }
        }

        public void UseAdaptiveVSync()
        {
            VSync = VSyncMode.Adaptive;
            Debug.Log("Turned on Adaptive VSync");
        }

        public void SetVSyncMode(VSyncMode mode)
        {
            VSync = mode;
            Debug.Log("Enabled " + mode);
        }
        #endregion

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

        #region Render
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
        #endregion
    }
}
