using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BoxNuZombie
{
    class MouseKeyboardInput
    {
        MouseState currentMS, previousMS;
        KeyboardState previousKS, currentKS;

        public void Update()
        {
            previousMS = currentMS;
            currentMS = Mouse.GetState();

            previousKS = currentKS;
            currentKS = Keyboard.GetState();
        }

        public Vector2 MousePosition
        {
            get { return new Vector2(currentMS.X, currentMS.Y); }
        }

        public bool IsLeftPressed()
        {
            if ((currentMS.LeftButton == ButtonState.Pressed) && (previousMS.LeftButton == ButtonState.Released))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsRightPressed()
        {
            if ((currentMS.RightButton == ButtonState.Pressed) && (previousMS.RightButton == ButtonState.Released))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool KeyPressed(Keys k)
        {
            if ((currentKS.IsKeyDown(k)) && (previousKS.IsKeyUp(k)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool KeyPressedHold(Keys k)
        {
            if ((currentKS.IsKeyDown(k)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
