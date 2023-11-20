using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super_Mario
{
    abstract class Entity : GameObject
    {
        public enum AnimationState
        {
            Idle,
            Moving,
            Jumping,
            Sliding,
            Dead
        }
        protected int speed;
        protected bool isGrounded;

        protected Vector2 direction = new Vector2(-1, 0);

        private Vector2 remainder = new Vector2(0, 0);

        protected float vSpeed;
        protected float hSpeed;

        protected AnimationState animState = AnimationState.Idle;
        protected int frame = 0;
        protected float frameInterval = 0.1f;  // Animation speed
        protected double frameTimer = 0;


        public Entity(Rectangle bounds, Texture2D texture) : base(bounds, texture)
        {
        }

        public override void Draw(SpriteBatch sb)
        {
            if (dead) return;

            base.Draw(sb);
        }
        
        protected virtual void OnCollide()
        {
            dead = true;
        }
        protected bool CheckCollision(Vector2 pos, Rectangle rect)
        {
            return (pos.X >= rect.X &&
                    pos.X <= rect.X + rect.Width &&
                    pos.Y >= rect.Y &&
                    pos.Y <= rect.Y + rect.Height);
        }
        protected bool CheckCollision(Rectangle rectA, Rectangle rectB)
        {
            return (rectA.Intersects(rectB));
        }
        protected virtual void HitSelf()
        {
            return;
        }
        public void MoveX(float amount, Action onCollide)
        {
            Rectangle playerRect = bounds;

            remainder.X += amount;
            int moveX = (int)Math.Round(remainder.X);

            if (moveX != 0)
            {
                remainder.X -= moveX;
                int moveSign = Math.Sign(moveX);

                void MovePlayerX()
                {
                    while (moveX != 0)
                    {
                        playerRect.X += moveSign;

                        // Test collision against Solids
                        foreach (Platform solid in Main.gameStateManager.gameScene.platforms)
                        {
                            Rectangle solidRect = solid.GetBounds();

                            if (CheckCollision(playerRect, solidRect))
                            {
                                hSpeed = 0;
                                if (onCollide != null)
                                    onCollide();
                                return;
                            }
                        }

                        // Move the Player
                        position.X += moveSign;
                        moveX -= moveSign;
                    }
                };
                MovePlayerX();
            }
        }
        public virtual void MoveY(float amount, Action onCollide)
        {
            Rectangle playerRect = GetBounds();

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
                                }
                                else if (bounds.Bottom > solidRect.Bottom)
                                    solid.isBouncing = true;
                                vSpeed = 0;
                                return;
                            }
                            //else
                                //isGrounded = false;
                        }
                        foreach (Enemy enemy in Main.gameStateManager.gameScene.enemies)
                        {
                            Rectangle enemyRect = enemy.GetBounds();

                            if (CheckCollision(playerRect, enemyRect))
                            {
                                if (enemy.hit) break;
                                // Moving down/falling
                                if (vSpeed > 0.0f)
                                {
                                    if (!enemy.hit)
                                        vSpeed = -3;
                                    enemy.HitEnemy();
                                }
                                else
                                {
                                    HitSelf();
                                }
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
