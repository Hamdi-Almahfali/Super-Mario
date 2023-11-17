using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Super_Mario
{
    public class Main : Game
    {
        internal static GameStateManager gameStateManager;
        public static GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
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
        }

        protected override void Update(GameTime gameTime)
        {
            gameStateManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //spriteBatch.Begin();
            //spriteBatch.End();
            gameStateManager.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}