using Microsoft.Xna.Framework;
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
        enum MarioType
        {
            Default,
            Shooter,
        }
        MarioType marioType; // All mario states

        public float MaxSpeed = 5.2f; // 3.0f
        public float Gravity = 13.4f; // 13.0f
        public float Acceleration = 10.2f; // 2.0f

        public float Deacceleration = 12.0f; // 22.0f
        public float AirDecceleration= 12.0f; // 12.0f

        public float MaxFallSpeed = 20.5f; // 4f
        public float JumpSpeed = -7.1f; // -3.0f

        private float dt; // Delta time

        private SpriteEffects spriteEffect; // Sprite effect for mirroring image
        public CustomTimer superStarTimer { get; private set; } // Timer for mario ability
        private Controller controller; // Debug controller for showing stats in debug mode

        private List<Fireball> fireballs;
        private int shootDelay = 5;
        private float canShoot = 0.0f;

        public Mario(Rectangle bounds, Texture2D texture) : base(bounds, texture)
        {
            controller = new Controller(this);
            spriteEffect = SpriteEffects.None;
        }
        public override void Create()
        {
            vSpeed = 0f;
            hSpeed = 0f;
            isGrounded = false;

            fireballs = new();
            marioType = MarioType.Shooter;
        }
        public override void Update(GameTime gameTime)
        {
            controller.Update();
            // Update delta time
            dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            SpriteHandler();
            AnimationHandler();
            Movement();
            MoveX(hSpeed, OnCollide);
            MoveY(vSpeed, OnCollide);
            ShootingBehavior(gameTime);
            base.Update(gameTime);

        }
        private void ShootingBehavior(GameTime gt)
        {
            if (marioType != MarioType.Shooter) return;
            if (KeyStatesManager.KeyPressed(Keys.LeftShift) && canShoot == 0.0f)
            {
                Fireball fireball = new Fireball(this.GetBounds(), Assets.texFireball, new Vector2(direction.X,0));
                fireball.Create(position);
                fireballs.Add(fireball);
                canShoot = shootDelay;
            }
            if (canShoot > 0)
            {
                canShoot -= 0.1f;
            }
            else
                canShoot = 0;

            foreach (Fireball fb in fireballs) { fb.Update(gt); }
        }
        private void AnimationHandler()
        {
            switch ((int)animState)
            {
                case 0:
                    frame = 0;
                    break;
                case 1:
                    frameTimer -= dt;
                    // If enough time has passed for the next frame
                    if (frameTimer <= 0)
                    {
                        frameTimer = frameInterval;
                        frame++;
                        if (frame > 5)
                        {
                            frame = 1;
                        }
                    }
                    break;
                case 2:
                    frame = 6;
                    break;
                case 3:
                    frame = 4;
                    break;
            }
        }
        private void SpriteHandler()
        {
            // Facing the Player in the right direction
            if (hSpeed > 0)
            {
                spriteEffect = SpriteEffects.None;
                direction.X = 1;
            }
            if (hSpeed < 0)
            {
                spriteEffect = SpriteEffects.FlipHorizontally;
                direction.X = -1;

            }
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
            if (KeyStatesManager.KeyReleased(Keys.Space) && vSpeed < 0)
            {
                vSpeed *= .5f;
            }
            if (!isGrounded)
            {
                animState = AnimationState.Jumping;
            }
            if (KeyStatesManager.KeyHeld(Keys.A))
            {
                if (isGrounded)
                {
                    animState = AnimationState.Moving;
                }
                float multi = 1.0f;
                if ( hSpeed > 0.0f)
                {
                    multi = 3.0f;
                }
                hSpeed = Data.Approach(hSpeed, -MaxSpeed, Acceleration * multi * dt);
            }
            if (KeyStatesManager.KeyHeld(Keys.D))
            {
                if (isGrounded)
                {
                    animState = AnimationState.Moving;
                }
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
                    animState = AnimationState.Idle;
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
        protected override void OnCollide()
        {
            return;
        }
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, new Vector2(position.X, position.Y - (texture.Height / 2 - Data.TileSize)), new Rectangle(frame * Data.TileSize, 0, Data.TileSize, 40), Color.White, 0, Vector2.Zero, 1f, spriteEffect, 0);
            foreach(Fireball fb in fireballs) { fb.Draw(sb); }
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
