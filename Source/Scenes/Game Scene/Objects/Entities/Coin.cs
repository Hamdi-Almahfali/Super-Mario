using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Super_Mario
{
    internal class Coin : Entity
    {

        public Coin(Rectangle bounds, Texture2D texture) : base(bounds, texture)
        {
        }
        public override void Create()
        {
            dead = true;
            base.Create();
            frameInterval = 0.2f;
        }
        public override void Update(GameTime gt)
        {
            if (dead) return;
            position.Y -= 100 * (float)gt.ElapsedGameTime.TotalSeconds;
            frameTimer -= gt.ElapsedGameTime.TotalSeconds;
            // If enough time has passed for the next frame
            if (frameTimer <= 0)
            {
                frameTimer = frameInterval;
                frame++;
                if (frame > 5)
                {
                    frame = 0;
                    dead = true;
                }
            }
        }
        public override void Draw(SpriteBatch sb)
        {
            if (dead) return;
            Rectangle rect = new Rectangle(frame * Data.TileSize / 2, 0,Data.TileSize / 2, Assets.texCoin.Height);
            sb.Draw(Assets.texCoin, new Vector2(position.X + Data.TileSize / 4, position.Y), rect, Color.White);
        }
        public void Release()
        {
            dead = false;
        }
    }
}
