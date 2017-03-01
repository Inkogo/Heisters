using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Heisters
{
    class SpriteRenderer
    {
        [Inject]
        AssetPipeline assetPipeline;

        public AssetTexture tex { get; private set; }

        public SpriteRenderer(string path, int depth = 0)
        {
            InjectionContainer.Instance.InjectDependencies(this);
            tex = assetPipeline.GetAsset<AssetTexture>(path);
        }

        public void DrawSprite(Vector2 pos, Color4 color, float depth = 0)
        {
            GL.BindTexture(TextureTarget.Texture2D, tex.texture);

            GL.Begin(PrimitiveType.Quads);

            GL.Color4(color);
            GL.TexCoord2(0f, 1f); GL.Vertex3(pos.X, pos.Y, -depth);
            GL.TexCoord2(1f, 1f); GL.Vertex3(pos.X + tex.width, pos.Y, -depth);
            GL.TexCoord2(1f, 0f); GL.Vertex3(pos.X + tex.width, pos.Y + tex.height, -depth);
            GL.TexCoord2(0f, 0f); GL.Vertex3(pos.X, pos.Y + tex.height, -depth);

            GL.End();
        }
    }
}
