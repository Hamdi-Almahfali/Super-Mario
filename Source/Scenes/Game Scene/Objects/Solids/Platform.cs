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


        float bounceSpeed = 20;
        float bounceTimer = 0f;
        const float maxBounceTime = 0.23f;
        public bool isBouncing;


        public Platform(Rectangle bounds, Texture2D texture, int type) : base(bounds, texture)
        {
            this.type = (BrickType)type;
        }
        public override void Update(GameTime gameTime)
        {
            CheckBouncingBehavior(gameTime);
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
        private void CheckBouncingBehavior(GameTime gameTime)
        {
            if (isBouncing)
            {
                BounceBlock(gameTime);
            }
            else
            {

            }
            bounds.Location = position.ToPoint();
        }
        private void BounceBlock(GameTime gameTime)
        {
            bounceTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Simulate bouncing effect by moving the block upwards
            position -= new Vector2(0, bounceSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);

            if (bounceTimer >= maxBounceTime)
            {
                // Bouncing sequence is over
                isBouncing = false;
                bounceTimer = 0f;

                // Reset the block's position or modify behavior as needed
                // For instance, you might set the block back to its original position
                position = startPosition;
            }
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
