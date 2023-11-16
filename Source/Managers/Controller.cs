using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Super_Mario
{
    internal class Controller
    {
        public Color[] choice;
        private float[] variables;

        Mario p;
        enum CurrentVariable
        {
            RunSpeed,
            Gravity,
            Accel,
            Deaccel,
            AirDeaccel,
            FallSpeed,
            JumpHeight
        }
        CurrentVariable current = CurrentVariable.JumpHeight;
        public Controller(Mario p)
        {

            choice = new Color[7];
            variables = new float[7];

            for (int i = 0; i < choice.Length; i++)
            {
                choice[i] = Color.White;
            }
            this.p = p;

        }
        public void Update()
        {
            if (!Data.Debug) return;

            if (KeyStatesManager.KeyPressed(Keys.Down))
                if (current > 0)
                current--;
            if (KeyStatesManager.KeyPressed(Keys.Up))
                if ((int)current < 6)
                current++;

            for (int i = 0; i < choice.Length; i++)
            {
                if ((int)current == i)
                    choice[i] = Color.Yellow;
                else
                    choice[i] = Color.White;
            }

            if (KeyStatesManager.KeyHeld(Keys.Right))
            {
                switch ((int)current)
                {
                    case 0:
                        p.MaxSpeed += 0.10f;
                        break;
                    case 1:
                        p.Gravity += 0.10f;
                        break;
                    case 2:
                        p.Acceleration += 0.10f;
                        break;
                    case 3:
                        p.Deacceleration += 0.10f;
                        break;
                    case 4:
                        p.AirDecceleration += 0.10f;
                        break;
                    case 5:
                        p.MaxFallSpeed += 0.10f;
                        break;
                    case 6:
                        p.JumpSpeed += 0.10f;
                        break;
                }
            }
            if (KeyStatesManager.KeyHeld(Keys.Left))
            {
                switch ((int)current)
                {
                    case 0:
                        p.MaxSpeed -= 0.1f;
                        break;
                    case 1:
                        p.Gravity -= 0.1f;
                        break;
                    case 2:
                        p.Acceleration -= 0.1f;
                        break;
                    case 3:
                        p.Deacceleration -= 0.1f;
                        break;
                    case 4:
                        p.AirDecceleration -= 0.10f;
                        break;
                    case 5:
                        p.MaxFallSpeed -= 0.10f;
                        break;
                    case 6:
                        p.JumpSpeed -= 0.10f;
                        break;
                }
            }
        }
    }
}
