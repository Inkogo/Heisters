using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL;

namespace Heisters
{
    class AssetTexture : Asset
    {
        public int texture { get; protected set; }
        public int width { get; protected set; }
        public int height { get; protected set; }

        public override void Load(string path)
        {
            texture = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, texture);

            using (Bitmap bitmap = new Bitmap(path))
            {
                BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
                bitmap.UnlockBits(data);

                width = bitmap.Width;
                height = bitmap.Height;

                //Set Quality
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMinFilter.Nearest);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMagFilter.Nearest);

                //Set Wrapping
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
            }
        }
    }
}
