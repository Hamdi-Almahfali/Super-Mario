using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Super_Mario.Platform;

namespace Super_Mario
{
    internal class Enemy : Entity
    {
        public enum EnemyType
        {
            Goomba,
            Koopa
        }
        EnemyType enemyType;
        enum EnemyState
        {
            Walking,
            Aggro,
            Dead
        }
        EnemyState enemyState;

        public float Gravity = 13.4f; // 13.0f
        public float MaxFallSpeed = 1.0f; // 4f

        public bool hit { get; private set; }
        CustomTimer hitTimer;

        SpriteEffects spriteEffect;

        public Enemy(Rectangle bounds, Texture2D texture, int index) : base(bounds, texture)
        {
            enemyType = (EnemyType)index;
            hitTimer = new CustomTimer();
        }
        public override void Create()
        {
            speed = 1;
            hSpeed = Math.Sign(direction.X) * speed;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            AnimationHandler(gameTime);
            HitBehavior();
            MoveX(hSpeed, OnCollide);
            MoveY(vSpeed, null);

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
        public override void Draw(SpriteBatch sb)
        {
            Rectangle rect = new Rectangle(frame * Data.TileSize, 0, Data.TileSize, Data.TileSize);
            sb.Draw(texture, position, rect, Color.White, 0, Vector2.Zero, 1f, spriteEffect, 0);
        }
        public void HitEnemy()
        {
            switch (enemyType)
            {
                case EnemyType.Goomba:
                    if (!hit)
                        hit = true;
                    break;
            }
        }
        private void HitBehavior()
        {
            if (hit)
            {
                switch (enemyType)
                {
                    case EnemyType.Goomba:
                        enemyState = EnemyState.Dead;
                        hSpeed = 0;
                        vSpeed = -3;
                        vSpeed = Data.Approach(vSpeed, MaxFallSpeed, Gravity);
                        break;
                }
            }
        }
        protected override void OnCollide()
        {
            hSpeed = -direction.X * speed;
        }
        private void AnimationHandler(GameTime gameTime)
        {
            if (enemyType == EnemyType.Goomba)
            {
                switch (enemyState)
                {
                    case EnemyState.Walking:
                        UpdateFrames(gameTime, frameInterval - 0.05f, 7);
                        break;
                    case EnemyState.Dead:
                        frame = 8;
                        break;
                }
            }
        }
        private void UpdateFrames(GameTime gameTime, float frameSpeed, int maxFrames)
        {
            frameTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            // If enough time has passed for the next frame
            if (frameTimer <= 0)
            {
                frameTimer = frameSpeed;
                frame++;
                if (frame > maxFrames)
                {
                    frame = 0;
                }
            }
        }
    }
}
