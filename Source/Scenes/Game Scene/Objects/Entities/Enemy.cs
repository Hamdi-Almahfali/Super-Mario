using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super_Mario
{
    internal class Enemy : Entity
    {
        public Enemy(Rectangle bounds, Texture2D texture) : base(bounds, texture)
        {
        }
    }
}
