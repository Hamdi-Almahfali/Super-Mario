using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super_Mario
{
    internal class MenuScene : Component
    {
        MenuHUD hud;

        internal override void LoadContent(ContentManager content)
        {
            hud = new();
        }

        internal override void Update(GameTime gameTime)
        {
            hud.Update(gameTime);
        }
        internal override void Draw(SpriteBatch spriteBatch)
        {
            hud.Draw(spriteBatch);
        }
    }
}
