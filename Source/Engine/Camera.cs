using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Super_Mario
{
    internal class Camera
    {
        private Matrix transform;  //håller en transformation från position i spelvärden till position i fönstret. 
        private Vector2 position;  //spelarens position
        private Viewport view;

        public Matrix Transform
        {
            get { return transform; }
        }
        public Camera(Viewport view)
        {
            this.view = view;
        }
        public void SetPosition(Vector2 position)
        {
            this.position.X = MathHelper.Lerp(this.position.X, position.X, 0.05f); // Smooth camera movement
            this.position.Y = MathHelper.Lerp(this.position.Y, position.Y, 0.1f);
            this.position.X = Math.Clamp(this.position.X, view.Width / 2, Data.WorldW - view.Width); // Bounds the camera to the edges of the world
            this.position.Y = Math.Clamp(this.position.Y, view.Height / 2, view.Height - Data.TileSize * 2);
            transform = Matrix.CreateTranslation(-this.position.X + view.Width / 2, -this.position.Y + view.Height / 2, 0);
        }
        public Vector2 GetPosition()
        {
            return position;
        }
    }
}
