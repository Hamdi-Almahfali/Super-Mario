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
    abstract class GameObject
    {
        protected Vector2 position;
        protected Vector2 startPosition;

        protected Rectangle bounds;
        protected int width;
        protected int height;

        protected Texture2D texture;

        protected bool dead;

        public GameObject(Rectangle bounds, Texture2D texture)
        {
            this.texture = texture;
            this.bounds = bounds;

            width = bounds.Width;
            height = bounds.Height;

            position.X = bounds.X * Data.TileSize;
            position.Y = bounds.Y * Data.TileSize;
            startPosition = position;

            dead = false;
            
        }
        public virtual void Create()
        {
        }


        public virtual void Update(GameTime gameTime)
        {
            UpdateBounds();
        }

        public virtual void Draw(SpriteBatch sb)
        {
            Rectangle defaultTile = new(0, 0, width, height);
            sb.Draw(texture, position, defaultTile, Color.White);
        }
        public Rectangle GetBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, width, height);
        }
        public virtual int GetPlatformType()
        {
            return -1;
        }
        public virtual int GetPlatformPrize()
        {
            return -1;
        }
        public Vector2 GetPosition()
        {
            return position;
        }
        private void UpdateBounds()
        {
            if (dead) return;

            bounds = new Rectangle((int)position.X, (int)position.Y, width, height);
        }
    }
}
