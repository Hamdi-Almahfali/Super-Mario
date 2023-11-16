using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super_Mario
{
    internal class Platform : GameObject
    {
        public enum BrickType
        {
            rock,
            brickTop,
            brick,
            metal,
            brickSolid,
            luckyBlock
        }
        private BrickType type;

        public Platform(Rectangle bounds, Texture2D texture, int type) : base(bounds, texture)
        {
            this.type = (BrickType)type;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch sb)
        {
            DrawSolids(sb);
            DrawLuckyBlock(sb);

        }
        public override int GetPlatformType()
        {
            return (int)type;
        }
        private void DrawLuckyBlock(SpriteBatch sb)
        {

        }
        private void DrawSolids(SpriteBatch sb)
        {

            for (int i = 0; i < height; i += 32)
            {
                for (int j = 0; j < width; j += 32)
                {
                    Rectangle defaultTile = new((int)type * Data.TileSize, 0, Data.TileSize, Data.TileSize);
                    sb.Draw(texture, new Vector2(position.X + j, position.Y + i), defaultTile, Color.White);
                }
            }

        }

    }
}
