using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super_Mario
{
    internal class Assets
    {
        // Load all game's contents
        public static Texture2D texMario;
        public static Texture2D texPlatform;

        public static Texture2D texBackground;

        public static SpriteFont Font;

        public static string Level1 = "level1.JSON";

        public static void LoadTextures(ContentManager content)
        {
            texMario = content.Load<Texture2D>(@"Entities\mario");
            texPlatform = content.Load<Texture2D>("tileset");
            texBackground = content.Load<Texture2D>("background0");

            Font = content.Load<SpriteFont>("Font");
        }
    }
}
