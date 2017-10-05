using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL4;


namespace RubixLIB.Graphics
{
    public class Texture2D
    {
        private int textureID;
        private string texturePath;
        private Bitmap textureData;

        public Texture2D(string pathToTexture)
        {
            texturePath = pathToTexture;
        }

        public void LoadTexture()
        {
            textureData = new Bitmap(texturePath);
        }

        public void LoadTextureGPU()
        {
            LoadTexture();

            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);

            GL.GenTextures(1, out textureID);
            GL.BindTexture(TextureTarget.Texture2D, textureID);

            BitmapData data = textureData.LockBits(new Rectangle(0, 0, textureData.Width, textureData.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
        OpenTK.Graphics.OpenGL4.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            textureData.UnlockBits(data);

        }
    }
}
