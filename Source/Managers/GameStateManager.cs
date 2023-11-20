using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Super_Mario.Source.Scenes;
using Super_Mario.Source.Scenes.Lose_Scene;

namespace Super_Mario
{
    internal class GameStateManager : Component
    {
        public enum GameState
        {
            Menu,
            Game,
            LevelEditor,
            CustomLevel,
            HighScores,
            Settings,
            Lost,
            Won
        }
        public static GameState State { get; set; } = GameState.Menu;

        private MenuScene menuScene;
        public GameScene gameScene;
        private ScoreScene scoreScene;
        private EditorScene editorScene;
        private WinScene winScene;
        private LoseScene loseScene;

        ContentManager content;

        internal override void LoadContent(ContentManager content)
        {
            this.content = content;
            ChangeLevel(GameState.Menu);
        }

        internal override void Update(GameTime gameTime)
        {
            KeyStatesManager.Update();
            switch (State)
            {
                case GameState.Menu:
                    menuScene.Update(gameTime);
                    break;
                case GameState.Game:
                    gameScene.Update(gameTime);
                    break;
                case GameState.Settings:
                    break;
                case GameState.Lost:
                    break;
                case GameState.Won:
                    break;
            }
        }

        internal override void Draw(SpriteBatch spriteBatch)
        {
            switch (State)
            {
                case GameState.Menu:
                    spriteBatch.Begin();
                    menuScene.Draw(spriteBatch);
                    spriteBatch.End();
                    break;
                case GameState.Game:
                    spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, gameScene.camera.Transform);
                    gameScene.Draw(spriteBatch);
                    spriteBatch.End();
                    spriteBatch.Begin();
                    gameScene.DrawGUI(spriteBatch);
                    spriteBatch.End();
                    break;
                case GameState.Settings:
                    break;
                case GameState.Lost:
                    break;
                case GameState.Won:
                    break;
            }
        }
        // Changes game scene and state
        public void ChangeLevel(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Menu:
                    menuScene = new MenuScene();
                    menuScene.LoadContent(content);
                    State = gameState;
                    break;
                case GameState.Game:
                    gameScene = new GameScene();
                    gameScene.camera = new(Main.graphics.GraphicsDevice.Viewport);
                    gameScene.LoadContent(content);
                    State = gameState;
                    break;
                case GameState.LevelEditor:
                    editorScene = new EditorScene();
                    State = gameState;
                    break;
                case GameState.CustomLevel:
                    gameScene = new GameScene();
                    gameScene.LoadContent(content);
                    State = gameState;
                    break;
                case GameState.HighScores:
                    scoreScene = new ScoreScene();
                    scoreScene.LoadContent(content);
                    State = gameState;
                    break;
                case GameState.Won:
                    winScene = new WinScene();
                    State = gameState;
                    break;
                case GameState.Lost:
                    loseScene = new LoseScene();
                    State = gameState;
                    break;
            }
        }
    }
}
