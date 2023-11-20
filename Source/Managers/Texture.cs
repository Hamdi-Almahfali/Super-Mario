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
        public static Texture2D texLuckyblock;
        public static Texture2D texCoin;

        public static Texture2D texGoomba;
        public static Texture2D texFireball;

        public static Texture2D texTitle;
        public static Texture2D texBackground0;
        public static Texture2D texBackground1;

        public static SpriteFont Font;
        public static SpriteFont NESFont;

        public static string Level1 = "level1.JSON";

        public static void LoadTextures(ContentManager content)
        {
            texMario = content.Load<Texture2D>(@"Entities\finn");
            texPlatform = content.Load<Texture2D>("tileset");
            texLuckyblock = content.Load<Texture2D>("luckyblock");
            texCoin = content.Load<Texture2D>("coin");

            texTitle = content.Load<Texture2D>("title");
            texBackground0 = content.Load<Texture2D>("background0");
            texBackground1 = content.Load<Texture2D>("background1");

            texGoomba = content.Load<Texture2D>("goomba");
            texFireball = content.Load<Texture2D>("fireball");

            Font = content.Load<SpriteFont>("Font");
            NESFont = content.Load<SpriteFont>("NES");
        }
    }
}
