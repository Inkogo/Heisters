using OpenTK.Graphics.OpenGL;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;

namespace Heisters
{
    class TextRenderer
    {
        [Inject]
        AssetPipeline assetPipeline;

        public AssetFont font { get; private set; }

        public TextRenderer(string path)
        {
            InjectionContainer.Instance.InjectDependencies(this);
            font = assetPipeline.GetAsset<AssetFont>(path);
        }

        public void DrawText(int x, int y, string text)
        {
            GL.BindTexture(TextureTarget.Texture2D, font.texture);

            GL.Begin(PrimitiveType.Quads);

            float u_step = font.settings.GlyphWidth / font.width;
            float v_step = font.settings.GlyphHeight / font.height;

            for (int n = 0; n < text.Length; n++)
            {
                char idx = text[n];
                float u = (idx % font.settings.GlyphsPerLine) * u_step;
                float v = (idx / font.settings.GlyphsPerLine) * v_step;

                GL.TexCoord2(u, v);
                GL.Vertex2(x, y);
                GL.TexCoord2(u + u_step, v);
                GL.Vertex2(x + font.settings.GlyphWidth, y);
                GL.TexCoord2(u + u_step, v + v_step);
                GL.Vertex2(x + font.settings.GlyphWidth, y + font.settings.GlyphHeight);
                GL.TexCoord2(u, v + v_step);
                GL.Vertex2(x, y + font.settings.GlyphHeight);

                x += font.settings.CharXSpacing;
            }

            GL.End();
        }
    }
}
