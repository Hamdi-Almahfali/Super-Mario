using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super_Mario
{
    internal static class Data
    {
        public static int ScreenW { get; private set; } = 768;
        public static int ScreenH { get; private set; } = 540;
        public static int WorldW { get; private set; } = 1792;
        public static int WorldH { get; private set; } = 736;

        public static int TileSize = 32;

        public static bool Debug = false;

        public static GraphicsDeviceManager graphics;

        // CHANGE SCREEN SIZE TO PREFERRED SIZE
        public static void SetScreenSize(GraphicsDeviceManager graphics)
        {
            graphics.PreferredBackBufferWidth = ScreenW;
            graphics.PreferredBackBufferHeight = ScreenH;
            graphics.ApplyChanges();
        }
        // Useful Math Functions
        public static float Approach(float current, float target, float increase)
        {
            if (current < target)
            {
                return MathF.Min(current + increase, target);
            }
            return MathF.Max(current - increase, target);
        }
        public static void DrawRectangle(this SpriteBatch spriteBatch, Rectangle rectangle)
        {
            Texture2D pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.White });

            spriteBatch.Draw(pixel, rectangle, Color.Yellow);
        }
        public static Color HexToColor(string hex)
        {
            hex = hex.Replace("#", "");
            byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

            // Check if the hex string includes alpha (transparency) information
            byte a = 255; // Default alpha value
            if (hex.Length == 8)
            {
                a = byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
            }

            return new Color(r, g, b, a);
        }
    }
}
