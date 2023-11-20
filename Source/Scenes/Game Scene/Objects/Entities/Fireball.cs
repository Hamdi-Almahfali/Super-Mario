using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super_Mario
{
    internal class Fireball : Entity
    {
        int tileSize = 16; // Fireball's size
        float Gravity = 1.9f;
        float MaxFallSpeed = 3.0f;

        Vector2 remainder;

        bool exploded;
        float explosionDuration = 1.0f;
        float scale = 1.0f; // The scale of the image

        public Fireball(Rectangle bounds, Texture2D texture, Vector2 direction) : base(bounds, texture)
        {
            this.direction = direction;
        }
        public void Create(Vector2 pos)
        {
            this.position = pos;
            bounds.Width = tileSize;
            bounds.Height = tileSize;
            speed = 5;
        }
        public override void Update(GameTime gameTime)
        {
            if (dead) return;

            UpdateBounds(tileSize);
            vSpeed = Data.Approach(vSpeed, MaxFallSpeed, Gravity);

            ExplosionHandler();
            if (exploded) return;
            MoveX(direction.X * speed, OnCollide);
            MoveY(vSpeed, OnCollide);
            Animation(gameTime);
        }
        public override void Draw(SpriteBatch sb)
        {
            if (dead) return;

            Rectangle rect = new Rectangle(frame * tileSize, 0, tileSize, tileSize);
            var origin = new Vector2(bounds.Width /2 , bounds.Height / 2);
            sb.Draw(texture, position + origin, rect, Color.White, 0, origin, scale, SpriteEffects.None, 0);
        }
        protected override void OnCollide()
        {
            exploded = true;
        }
        private void ExplosionHandler()
        {
            if (exploded)
            {
                hSpeed = 0;
                vSpeed = 0;
                scale = 2.0f;
                explosionDuration -= 0.1f;
            }
            if (explosionDuration <= 0) dead = true;
        }
        private void UpdateBounds(int ts)
        {
            bounds = new Rectangle((int)position.X, (int)position.Y, ts, ts);
        }
        private void Animation(GameTime gt)
        {
            frameTimer -= gt.ElapsedGameTime.TotalSeconds;
            // If enough time has passed for the next frame
            if (frameTimer <= 0)
            {
                frameTimer = frameInterval;
                frame++;
                if (frame > 3)
                {
                    frame = 0;
                }
            }
            if (exploded) frame = 4;
        }
        public override void MoveY(float amount, Action onCollide)
        {
            Rectangle playerRect = bounds;

            remainder.Y += amount;
            int moveY = (int)Math.Round(remainder.Y);

            if (moveY != 0)
            {
                remainder.Y -= moveY;
                int moveSign = Math.Sign(moveY);

                void MovePlayerY()
                {
                    while (moveY != 0)
                    {
                        playerRect.Y += moveSign;

                        // Test collision against Solids
                        foreach (Platform solid in Main.gameStateManager.gameScene.platforms)
                        {
                            Rectangle solidRect = solid.GetBounds();

                            if (CheckCollision(playerRect, solidRect))
                            {
                                // Moving down/falling
                                if (vSpeed > 0.0f)
                                {
                                    isGrounded = true;
                                    vSpeed = -3;
                                }
                                vSpeed = -9;
                                return;
                            }
                        }

                        // Move the Player
                        position.Y += moveSign;
                        moveY -= moveSign;
                    }
                };
                MovePlayerY();
            }
        }
    }
}
