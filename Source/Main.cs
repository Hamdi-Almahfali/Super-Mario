using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Super_Mario
{
    public class Main : Game
    {
        internal static GameStateManager gameStateManager;
        public static GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        public GameWindow gameWindow;

        public static float dt;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // Fixed time step
            TargetElapsedTime = TimeSpan.FromSeconds(1.0 / 60.0f);
            IsFixedTimeStep = true;


            // SET SCREEN SIZE
            Data.SetScreenSize(graphics);

            // INITIALIZE GAME STATE
            gameStateManager = new();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Assets.LoadTextures(Content);
            gameStateManager.LoadContent(Content);

            graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;
        }

        protected override void Update(GameTime gameTime)
        {
            dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            gameStateManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            string hexColorCode = "#7892e0";
            Color color = Data.HexToColor(hexColorCode);

            GraphicsDevice.Clear(color);

            //spriteBatch.Begin();
            //spriteBatch.End();
            gameStateManager.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}