using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;

namespace Heisters
{
    class Game : GameWindow
    {
        AssetPipeline assetPipeline;
        Input input;
        SaveLoadUtility saveLoad;
        PlayerSettings settings;
        GameState gameState;

        public Game(int width, int height) : base(width, height, GraphicsMode.Default, "Heisters") { }

        Vector2 center = new Vector2(0, 0);

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            saveLoad = new SaveLoadUtility();
            assetPipeline = new AssetPipeline();
            input = new Input(this);
            settings = new PlayerSettings();
            gameState = GameState.LoadOrCreate();

            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            GL.Enable(EnableCap.DepthTest);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Width, Height);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (gameState.map != null)
                gameState.Update((float)e.Time);

            input.Update();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.MatrixMode(MatrixMode.Modelview);

            if (gameState.map != null)
            {
                Matrix4 world = gameState.player.GetViewMatrix(Width, Height);
                GL.LoadIdentity();
                GL.MultMatrix(ref world);
                GL.PushMatrix();

                gameState.map.OnRender();
            }
            GL.PopMatrix();

            SwapBuffers();
        }
    }
}
