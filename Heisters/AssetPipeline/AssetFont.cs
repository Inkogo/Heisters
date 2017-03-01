using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;

namespace Heisters
{
    class AssetFont : AssetTexture
    {
        public FontSettings settings { get; private set; }

        public override void Load(string path)
        {
            Load(path, new FontSettings());
        }

        public void Load(string path, FontSettings s)
        {
            settings = s;
            GenerateFontImage(path);
            base.Load(settings.fontName);
        }

        void GenerateFontImage(string fontPath)
        {
            int bitmapWidth = settings.GlyphsPerLine * settings.GlyphWidth;
            int bitmapHeight = settings.GlyphLineCount * settings.GlyphHeight;

            using (Bitmap bitmap = new Bitmap(bitmapWidth, bitmapHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
            {
                Font font;
                var collection = new PrivateFontCollection();
                collection.AddFontFile(fontPath);
                var fontFamily = new FontFamily(Path.GetFileNameWithoutExtension(fontPath), collection);
                font = new Font(fontFamily, settings.FontSize);
                
                using (var g = Graphics.FromImage(bitmap))
                {
                    g.SmoothingMode = SmoothingMode.None;
                    g.TextRenderingHint = TextRenderingHint.SingleBitPerPixel;

                    for (int p = 0; p < settings.GlyphLineCount; p++)
                    {
                        for (int n = 0; n < settings.GlyphsPerLine; n++)
                        {
                            char c = (char)(n + p * settings.GlyphsPerLine);
                            g.DrawString(c.ToString(), font, Brushes.White, n * settings.GlyphWidth + settings.AtlasOffsetX, p * settings.GlyphHeight + settings.AtlassOffsetY);
                        }
                    }
                }
                bitmap.Save(settings.fontName);
            }
            //Process.Start(Settings.FontBitmapFilename);
        }
    }
}
