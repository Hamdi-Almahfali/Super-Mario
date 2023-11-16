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
    }
}
