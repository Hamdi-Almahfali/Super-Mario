﻿using Microsoft.Xna.Framework;
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


        // Define lists
        public List<Platform> platforms;
        public List<Enemy> enemies;

        internal override void LoadContent(ContentManager content)
        {
            platforms = new List<Platform>();
            enemies = new List<Enemy>();
            ReadFromJSONFile(Assets.Level1); // Load game level
        }
        internal override void Update(GameTime gameTime)
        {
            camera.SetPosition(player.GetPosition()); // Update camera position

            player.Update(gameTime); // Update player object
            foreach (Platform platform in platforms) { platform.Update(gameTime);}
            foreach (Enemy enemy in enemies) { enemy.Update(gameTime);}
        }
        internal override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Assets.texBackground, new Vector2(0, 10 * Data.TileSize), Color.White);
            foreach (Platform platform in platforms) { platform.Draw(spriteBatch); }    // Draw the platforms
            foreach (Enemy enemy in enemies) { enemy.Draw(spriteBatch); }               // Draw the enemies
            player.Draw(spriteBatch); // Draw player
            player.DrawDebug(spriteBatch); // Draw player
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
                    platformData.type
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
