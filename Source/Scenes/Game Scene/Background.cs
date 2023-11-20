using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super_Mario
{
    internal class Background : Component
    {
        List<Vector2> foreground, middleground, background, clouds;
        int fgSpacing, mgSpacing, bgSpacing, cdSpacing;
        float fgSpeed, mgSpeed, bgSpeed, cdSpeed;
        Texture2D[] tex;
        GameWindow window;

        public Background(ContentManager Content)
        {
            this.tex = new Texture2D[4];
            var window = Main.graphics.GraphicsDevice.Viewport;
            tex[0] = Content.Load<Texture2D>("background0");
            tex[1] = Content.Load<Texture2D>("background1");
            tex[2] = Content.Load<Texture2D>("background2");
            tex[3] = Content.Load<Texture2D>("background3");

            foreground = new List<Vector2>();
            fgSpacing = tex[0].Width;
            fgSpeed = 0.75f;

            for (int i = 0; i < (window.Width / fgSpacing) + 2; i++)
            {
                foreground.Add(new Vector2(i * fgSpacing, window.Height * 1.3f - tex[0].Height));
            }

            middleground = new List<Vector2>();
            mgSpacing = window.Width;
            mgSpeed = 0.5f;

            for (int i = 0; i < (window.Width / mgSpacing) + 2; i++)
            {
                middleground.Add(new Vector2(i * mgSpacing, window.Height * 1.3f - tex[1].Height));
            }

            background = new List<Vector2>();
            bgSpacing = window.Width - 1;
            bgSpeed = 0f;

            for (int i = 0; i < (window.Width / bgSpacing) + 2; i++)
            {
                background.Add(new Vector2(i * bgSpacing, window.Height * 1.3f - tex[2].Height));
            }

            clouds = new List<Vector2>();
            cdSpacing = window.Width - 1;
            cdSpeed = 0f;

            for (int i = 0; i < (window.Width / cdSpacing) + 2; i++)
            {
                clouds.Add(new Vector2(i * cdSpacing, window.Height * 1.3f - tex[3].Height));
            }
        }


        internal override void LoadContent(ContentManager content)
        {
        }

        internal override void Update(GameTime gameTime)
        {
            for (int i = 0; i < foreground.Count; i++)
            {
                foreground[i] = new Vector2(foreground[i].X - fgSpeed, foreground[i].Y);
            }
            for (int i = 0; i < middleground.Count; i++)
            {
                middleground[i] = new Vector2(middleground[i].X - mgSpeed, middleground[i].Y);
            }
            for (int i = 0; i < background.Count; i++)
            {
                background[i] = new Vector2(background[i].X - bgSpeed, background[i].Y);
            }
            for (int i = 0; i < clouds.Count; i++)
            {
                clouds[i] = new Vector2(clouds[i].X - cdSpeed, clouds[i].Y);
            }

            for (int i = 0; i < foreground.Count; i++)
            {
                foreground[i] = new Vector2(foreground[i].X - fgSpeed, foreground[i].Y);
                if (foreground[i].X <= -fgSpacing)
                {
                    int j = i - 1;
                    if (j < 0)
                    {
                        j = foreground.Count - 1;
                    }

                    foreground[i] = new Vector2(foreground[j].X + fgSpacing - 1, foreground[i].Y);
                }
            }
            for (int i = 0; i < middleground.Count; i++)
            {
                middleground[i] = new Vector2(middleground[i].X - mgSpeed, middleground[i].Y);

                if (middleground[i].X <= -mgSpacing)
                {
                    int j = i - 1;

                    if (j < 0)
                    {
                        j = middleground.Count - 1;
                    }

                    middleground[i] = new Vector2(middleground[j].X + mgSpacing - 1, middleground[i].Y);
                }
            }
            for (int i = 0; i < background.Count; i++)
            {
                background[i] = new Vector2(background[i].X - bgSpeed, background[i].Y);

                if (background[i].X <= -bgSpacing)
                {
                    int j = i - 1;

                    if (j < 0)
                    {
                        j = background.Count - 1;
                    }
                    background[i] = new Vector2(background[j].X + bgSpacing - 1, background[i].Y);
                }
            }
            for (int i = 0; i < clouds.Count; i++)
            {
                clouds[i] = new Vector2(clouds[i].X - cdSpeed, clouds[i].Y);

                if (clouds[i].X <= -cdSpacing)
                {
                    int j = i - 1;

                    if (j < 0)
                    {
                        j = clouds.Count - 1;
                    }
                    clouds[i] = new Vector2(clouds[j].X + cdSpacing - 1, clouds[i].Y);
                }
            }

        }
        internal override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Vector2 v in clouds)
            {
                spriteBatch.Draw(tex[3], v, Color.White);
            }
            foreach (Vector2 v in background)
            {
                spriteBatch.Draw(tex[2], v, Color.White);
            }

            foreach (Vector2 v in middleground)
            {
                spriteBatch.Draw(tex[1], v, Color.White);
            }

            foreach (Vector2 v in foreground)
            {
                spriteBatch.Draw(tex[0], v, Color.White);
            }

        }
    }
}
