﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Super_Mario.GameStateManager;

namespace Super_Mario
{
    internal class Mario : Entity
    {
        private float dt;

        private float vSpeed;
        private float hSpeed;

        public float MaxSpeed = 5.2f; // 3.0f
        public float Gravity = 13.4f; // 13.0f
        public float Acceleration = 10.2f; // 2.0f

        public float Deacceleration = 22.0f; // 22.0f
        public float AirDecceleration= 12.0f; // 12.0f

        public float MaxFallSpeed = 20.5f; // 4f
        public float JumpSpeed = -7.1f; // -3.0f

        private Vector2 remainder = new Vector2(0, 0);


        private Controller controller;

        public Mario(Rectangle bounds, Texture2D texture) : base(bounds, texture)
        {
            controller = new Controller(this);
        }
        public override void Create()
        {
            vSpeed = 0f;
            hSpeed = 0f;
            isGrounded = false;
        }
        public override void Update(GameTime gameTime)
        {
            controller.Update();
            // Update delta time
            dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Debug.Print(isGrounded.ToString());
            Movement();
            MoveX(hSpeed, OnCollide);
            MoveY(vSpeed, OnCollide);
            base.Update(gameTime);

        }

        private void Movement()
        {
            // Jump
            if (KeyStatesManager.KeyHeld(Keys.Space) && isGrounded)
            {
                vSpeed = JumpSpeed;
                //hSpeed += player.solidSpeed.x;
                //vSpeed += player.solidSpeed.y;
                isGrounded = false;
            }
            if (KeyStatesManager.KeyHeld(Keys.A))
            {
                float multi = 1.0f;
                if ( hSpeed > 0.0f)
                {
                    multi = 3.0f;
                }
                hSpeed = Data.Approach(hSpeed, -MaxSpeed, Acceleration * multi * dt);
            }
            if (KeyStatesManager.KeyHeld(Keys.D))
            {
                float multi = 1.0f;
                if (hSpeed < 0.0f)
                {
                    multi = 3.0f;
                }
                hSpeed = Data.Approach(hSpeed, MaxSpeed, Acceleration * multi * dt);
            }

            // Friction
            if (!KeyStatesManager.KeyHeld(Keys.A) && !KeyStatesManager.KeyHeld(Keys.D))
            {
                if (isGrounded)
                {
                    hSpeed = Data.Approach(hSpeed, 0, Deacceleration * dt);
                }
                else
                {
                    hSpeed = Data.Approach(hSpeed, 0, AirDecceleration * dt);
                }
            }

            // Gravity
            vSpeed = Data.Approach(vSpeed, MaxFallSpeed, Gravity * dt); 

            position.X = Math.Clamp(position.X, 0, Data.WorldW - width);
            position.Y = Math.Clamp(position.Y, 0, Data.WorldH - height * 2f);

        }
        public void MoveX(float amount, Action onCollide)
        {
            Rectangle playerRect = GetBounds();

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
                        foreach (Platform solid in GameScene.platforms)
                        {
                            Rectangle solidRect = solid.GetBounds();

                            if (CheckCollision(playerRect, solidRect))
                            {
                                hSpeed = 0;
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
        public void MoveY(float amount, Action onCollide)
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
                        foreach (Platform solid in GameScene.platforms)
                        {
                            Rectangle solidRect = solid.GetBounds();

                            if (CheckCollision(playerRect, solidRect))
                            {
                                // Moving down/falling
                                if (vSpeed > 0.0f)
                                {
                                    isGrounded = true;
                                }
                                vSpeed = 0;
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
        protected override void OnCollide()
        {
            return;
        }
        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
        }
        public void DrawDebug(SpriteBatch sb)
        {
            if (Data.Debug)
            {
                var height = 150;
                var margin = 20;
                sb.DrawString(Assets.Font, $"RunMaxSpeed         {   MaxSpeed}",         new Vector2(position.X, position.Y - height),                controller.choice[0]);
                sb.DrawString(Assets.Font, $"Gravity                {Gravity}",          new Vector2(position.X, position.Y - height - (margin * 1)), controller.choice[1]);
                sb.DrawString(Assets.Font, $"Accelration          {  Acceleration}",     new Vector2(position.X, position.Y - height - (margin * 2)), controller.choice[2]);
                sb.DrawString(Assets.Font, $"Deaccelrration     {    Deacceleration}",   new Vector2(position.X, position.Y - height - (margin * 3)), controller.choice[3]);
                sb.DrawString(Assets.Font, $"Air Deaccelrration {    AirDecceleration}", new Vector2(position.X, position.Y - height - (margin * 4)), controller.choice[4]);
                sb.DrawString(Assets.Font, $"MaxFallSpeed       {    MaxFallSpeed}",     new Vector2(position.X, position.Y - height - (margin * 5)), controller.choice[5]);
                sb.DrawString(Assets.Font, $"Jump Height         {   JumpSpeed}",        new Vector2(position.X, position.Y - height - (margin * 6)), controller.choice[6]);
            }
        }

    }
}