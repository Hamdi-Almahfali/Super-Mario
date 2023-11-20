using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super_Mario
{
    internal class MenuHUD
    {
        enum Choice
        {
            OnePlayer,
            TwoPlayer,
            EditLevel
        }
        Choice choice;

        Color color0;
        Color color1;
        Color color2;

        float scale0;
        float scale1;
        float scale2;

        float buttonScale = 1.5f;

        public MenuHUD()
        {
            Create();
        }

        public void Create()
        {
            choice = Choice.OnePlayer;
        }
        public void Update(GameTime gt)
        {
            ButtonsController();
        }
        public void Draw(SpriteBatch sb)
        {
            // Draw background
            sb.Draw(Assets.texBackground1, new Vector2(0, Data.TileSize * 6), Color.White);
            // Draw title
            var tex = Assets.texTitle;
            sb.Draw(tex, new Vector2(Data.ScreenW / 2 - tex.Width / 2, Data.TileSize * 2), null, Color.White);
            ButtonsDraw(sb);
        }
        private void ButtonsController()
        {
            if (KeyStatesManager.KeyPressed(Keys.Enter))
            {
                switch (choice)
                {
                    case Choice.OnePlayer:
                        Main.gameStateManager.ChangeLevel(GameStateManager.GameState.Game);
                        break;
                    case Choice.TwoPlayer:
                        Main.gameStateManager.ChangeLevel(GameStateManager.GameState.Game);
                        break;
                    case Choice.EditLevel:
                        Main.gameStateManager.ChangeLevel(GameStateManager.GameState.LevelEditor);
                        break;
                }
            }
            if (KeyStatesManager.KeyPressed(Keys.Down) && (int)choice >= 0 && (int)choice < 2)
            {
                choice++;
            }
            if (KeyStatesManager.KeyPressed(Keys.Up) && (int)choice <= 2 && (int)choice > 0)
            {
                choice--;
            }
        }
        private void ButtonsDraw(SpriteBatch sb)
        {
            color0 = Color.White;
            color1 = Color.White;
            color2 = Color.White;

            scale0 = MathHelper.Lerp(scale0, 1f, 0.1f);
            scale1 = MathHelper.Lerp(scale1, 1f, 0.1f);
            scale2 = MathHelper.Lerp(scale2, 1f, 0.1f);

            switch (choice)
            {
                case Choice.OnePlayer:
                    color0 = Color.Yellow; scale0 = MathHelper.Lerp(scale0, buttonScale, 0.1f);
                    break;
                case Choice.TwoPlayer:
                    color1 = Color.Yellow; scale1 = MathHelper.Lerp(scale1, buttonScale, 0.1f);
                    break;
                case Choice.EditLevel:
                    color2 = Color.Yellow; scale2 =MathHelper.Lerp(scale2, buttonScale, 0.1f);
                    break;
            }
            WriteText(sb, Data.TileSize * 10, Assets.NESFont, "1 Player game", color0, scale0);
            WriteText(sb, Data.TileSize * 11, Assets.NESFont, "2 Player game", color1, scale1);
            WriteText(sb, Data.TileSize * 13, Assets.NESFont, "Level  editor ", color2, scale2);
        }
        private void WriteText(SpriteBatch spriteBatch, float y, SpriteFont font, string text, Color color, float scale)
        {
            // Measure the size of the text
            Vector2 textSize = font.MeasureString(text);
            var graphicsDevice = Main.graphics.GraphicsDevice;

            float unscaledX = (graphicsDevice.Viewport.Width - textSize.X * scale) / 2;

            // Adjust x position considering the scale and text's center origin
            float x = unscaledX + textSize.X * scale / 2;

            // Draw each letter
            for (int i = 0; i < text.Length; i++)
            {
                float alpha = (float)i / text.Length;
                Color characterColor = new Color(color.R, color.G, color.B, color.A);

                spriteBatch.DrawString(
                    font,
                    text[i].ToString(),
                    new Vector2(x, y),
                    characterColor,
                    0f, // Rotation angle (no rotation in this case)
                    new Vector2(120,0), // Origin for rotation and scaling (text's center)
                    scale, // Scale factor
                    SpriteEffects.None,
                    0f // Layer depth
                );

                x += font.MeasureString(text[i].ToString()).X * scale;
            }
        }


    }
}
