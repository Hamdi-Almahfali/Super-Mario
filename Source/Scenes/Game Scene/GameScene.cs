using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super_Mario
{
    internal class GameScene : Component
    {
        public Camera camera; // Main game camera

        private Mario player; // Main player

        private ScoreManager scoreManager;

        private Background bg;

        // Define lists
        public List<Platform> platforms;
        public List<Enemy> enemies;

        internal override void LoadContent(ContentManager content)
        {
            scoreManager = new ScoreManager();
            platforms = new List<Platform>();
            enemies = new List<Enemy>();
            bg = new Background(content);
            ReadFromJSONFile(Assets.Level1); // Load game level

        }
        internal override void Update(GameTime gameTime)
        {
            camera.SetPosition(player.GetPosition()); // Update camera position

            player.Update(gameTime); // Update player object
            foreach (Platform platform in platforms) { platform.Update(gameTime);}
            foreach (Enemy enemy in enemies) { enemy.Update(gameTime);}
            bg.Update(gameTime);
        }
        internal override void Draw(SpriteBatch spriteBatch)
        {
            bg.Draw(spriteBatch);
            spriteBatch.Draw(Assets.texBackground0, new Vector2(0, 10 * Data.TileSize), Color.White);
            foreach (Platform platform in platforms) { platform.Draw(spriteBatch); }    // Draw the platforms
            foreach (Enemy enemy in enemies) { enemy.Draw(spriteBatch); }               // Draw the enemies
            player.Draw(spriteBatch); // Draw player
            player.DrawDebug(spriteBatch); // Draw player
        }
        internal void DrawGUI(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Assets.NESFont, "Score", new Vector2(                        Data.TileSize, 5), Color.White);
            spriteBatch.DrawString(Assets.NESFont, scoreManager.score.ToString(), new Vector2(  Data.TileSize * 2, 25), Color.White);
            spriteBatch.DrawString(Assets.NESFont, "Coins", new Vector2(                        Data.TileSize * 8, 5), Color.White);
            spriteBatch.DrawString(Assets.NESFont, scoreManager.coins.ToString(), new Vector2(  Data.TileSize * 9, 25), Color.White);
            spriteBatch.DrawString(Assets.NESFont, "World", new Vector2(                        Data.TileSize * 18, 5), Color.White);
            spriteBatch.DrawString(Assets.NESFont, scoreManager.world, new Vector2(             Data.TileSize * 19 - 10, 25), Color.White);
            spriteBatch.DrawString(Assets.NESFont, "Time", new Vector2(                         Data.TileSize * 26, 5), Color.White);
            spriteBatch.DrawString(Assets.NESFont, scoreManager.score.ToString(), new Vector2(  Data.TileSize * 27, 25), Color.White);
        }
        private void ReadFromJSONFile(string fileName)
        {
            List<PlatformData> platformDataList = JsonParser.GetType(fileName, "platforms");

            foreach (PlatformData platformData in platformDataList)
            {
                Platform p = new Platform(
                    new Rectangle(
                        platformData.rect.X,
                        platformData.rect.Y,
                        platformData.rect.Width * Data.TileSize,
                        platformData.rect.Height * Data.TileSize
                    ),
                    Assets.texPlatform,
                    platformData.type,
                    platformData.prize
                );
                platforms.Add(p);
            }
            List<PlatformData> EnemyList = JsonParser.GetType(fileName, "enemies");

            foreach (PlatformData EnemyData in EnemyList)
            {
                Enemy e = new Enemy(
                    new Rectangle(
                        EnemyData.rect.X,
                        EnemyData.rect.Y,
                        EnemyData.rect.Width * Data.TileSize,
                        EnemyData.rect.Height * Data.TileSize
                    ),
                    Assets.texGoomba,
                    EnemyData.type
                );
                e.Create();
                enemies.Add(e);
            }
            Rectangle playerRect = JsonParser.GetRectangle(fileName, "player");
            player = new(playerRect, Assets.texMario);
            player.Create();
        }
    }
}
