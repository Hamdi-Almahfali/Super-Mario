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

        public Enemy(Rectangle bounds, Texture2D texture, int index) : base(bounds, texture)
        {
            enemyType = (EnemyType)index;
            hitTimer = new CustomTimer();
        }
        public override void Create()
        {
            hSpeed = Math.Sign(direction.X) * 1;
        }
        public override void Update(GameTime gameTime)
        {
            AnimationHandler(gameTime);
            HitBehavior();
            MoveX(hSpeed, null);
            MoveY(vSpeed, null);
        }
        public override void Draw(SpriteBatch sb)
        {
            Rectangle rect = new Rectangle(frame * Data.TileSize, 0, Data.TileSize, Data.TileSize);
            sb.Draw(texture, position, rect, Color.White);
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
        private void AnimationHandler(GameTime gameTime)
        {
            if (enemyType == EnemyType.Goomba)
            {
                switch (enemyState)
                {
                    case EnemyState.Walking:
                        UpdateFrames(gameTime, frameInterval + 0.1f, 1);
                        break;
                    case EnemyState.Dead:
                        frame = 2;
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
