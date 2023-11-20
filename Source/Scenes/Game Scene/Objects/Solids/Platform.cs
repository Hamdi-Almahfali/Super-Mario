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
        public enum BrickPrize
        {
            None,
            Coin,
            Mushrom,
            Super,
            Star
        }
        private BrickPrize prize;

        float bounceSpeed = 20;
        float bounceTimer = 0f;
        const float maxBounceTime = 0.23f;
        public bool isBouncing;

        int frame;
        float frameInterval = 0.2f;  // Animation speed
        double frameTimer = 0;
        float dt;

        bool releasedItem = false;
        Coin coin;

        public Platform(Rectangle bounds, Texture2D texture, int type, int prize) : base(bounds, texture)
        {
            this.type = (BrickType)type;
            this.prize = (BrickPrize)prize;
            LuckyBlockPrizeGen();
        }
        public override void Update(GameTime gameTime)
        {
            dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            CheckBouncingBehavior(gameTime);
            DrawLuckyBlock();
            base.Update(gameTime);
            if (coin != null)
                coin.Update(gameTime);
        }
        public override void Draw(SpriteBatch sb)
        {
            if (coin != null)
                coin.Draw(sb);
            DrawSolids(sb);

        }
        public override int GetPlatformType()
        {
            return (int)type;
        }
        public override int GetPlatformPrize()
        {
            return (int)prize;
        }
        private void DrawLuckyBlock()
        {
            if (type == BrickType.luckyBlock)
            {
                frameTimer -= dt;
                // If enough time has passed for the next frame
                if (frameTimer <= 0)
                {
                    frameTimer = frameInterval;
                    frame++;
                    if (frame > 2)
                    {
                        frame = 0;
                    }
                }
            }
        }
        private void CheckBouncingBehavior(GameTime gameTime)
        {
            if (isBouncing)
            {
                BounceBlock(gameTime);
                if (!releasedItem)
                {
                    if (coin == null) return;
                    coin.Release();
                    releasedItem = true;
                }
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

                // Reset the block's position
                position = startPosition;

            }
        }
        private void LuckyBlockPrizeGen()
        {
            if (type == BrickType.luckyBlock && prize == BrickPrize.Coin)
            {
                coin = new(bounds, texture);
                coin.Create();
            }
        }
        private void DrawSolids(SpriteBatch sb)
        {

            for (int i = 0; i < height; i += Data.TileSize)
            {
                for (int j = 0; j < width; j += Data.TileSize)
                {
                    if (type == BrickType.luckyBlock) break;
                    if (type == BrickType.rock) break;
                    Rectangle defaultTile = new(6 * Data.TileSize, 0, Data.TileSize, Data.TileSize);
                    sb.Draw(texture, new Vector2(position.X + j, position.Y + i), defaultTile, Color.White);
                }
            }
            if (width == Data.TileSize && type == BrickType.rock)
            {
                Rectangle defaultTile = new(6 * Data.TileSize, 0, Data.TileSize, Data.TileSize);
                sb.Draw(texture, new Vector2(position.X, position.Y), defaultTile, Color.White);
            }
            else
            {
                for (int i = 0; i < height; i += Data.TileSize)
                {
                    for (int j = 0; j < width; j += Data.TileSize)
                    {
                        if (type != BrickType.rock) break;
                        if (j == 0 && i == 0)
                        {
                            Rectangle defaultTile = new(5 * Data.TileSize, 0, Data.TileSize, Data.TileSize);
                            sb.Draw(texture, new Vector2(position.X + j, position.Y + i), defaultTile, Color.White);
                        }
                        if (j > Data.TileSize && j < width && i == 0)
                        {
                            Rectangle defaultTile = new(6 * Data.TileSize, 0, Data.TileSize, Data.TileSize);
                            sb.Draw(texture, new Vector2(position.X + j, position.Y + i), defaultTile, Color.White);
                        }
                        else if (j < width && i == 0)
                        {
                            Rectangle defaultTile = new(7 * Data.TileSize, 0, Data.TileSize, Data.TileSize);
                            sb.Draw(texture, new Vector2(position.X + j, position.Y + i), defaultTile, Color.White);
                        }
                        else if (i < height)
                        {
                            Rectangle defaultTile = new(5 * Data.TileSize, Data.TileSize, Data.TileSize, Data.TileSize);
                            sb.Draw(texture, new Vector2(position.X + j, position.Y + i), defaultTile, Color.White);
                        }
                    }
                }
            }
            if (type != BrickType.luckyBlock) return;
            sb.Draw(Assets.texLuckyblock, position, new Rectangle(frame * Data.TileSize, Data.TileSize, Data.TileSize, Data.TileSize), Color.White);

        }

    }
}
