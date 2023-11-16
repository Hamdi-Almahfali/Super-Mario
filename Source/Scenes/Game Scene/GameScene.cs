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


        // Define lists
        public static List<Platform> platforms;

        internal override void LoadContent(ContentManager content)
        {
            platforms = new List<Platform>();
            ReadFromJSONFile(Assets.Level1); // Load game level
        }
        internal override void Update(GameTime gameTime)
        {
            camera.SetPosition(player.GetPosition()); // Update camera position

            player.Update(gameTime); // Update player object
            foreach (Platform platform in platforms) { platform.Update(gameTime);}
        }
        internal override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Platform platform in platforms) { platform.Draw(spriteBatch); } // Draw the platforms
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
            Rectangle playerRect = JsonParser.GetRectangle(fileName, "player");
            player = new(playerRect, Assets.texMario);
            player.Create();
        }
    }
}
