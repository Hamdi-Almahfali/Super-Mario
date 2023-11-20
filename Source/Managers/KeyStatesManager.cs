using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

static class KeyStatesManager
{
    public static KeyboardState keyState, oldKeyState = Keyboard.GetState();
    public static MouseState mouseState, oldMouseState = Mouse.GetState();

    public static bool KeyPressed(Keys key)
    {
        return keyState.IsKeyDown(key) && oldKeyState.IsKeyUp(key);
    }
    public static bool KeyHeld(Keys key)
    {
        return keyState.IsKeyDown(key) && oldKeyState.IsKeyDown(key);
    }
    public static bool KeyReleased(Keys key)
    {
        return keyState.IsKeyUp(key) && oldKeyState.IsKeyDown(key);
    }
    public static bool LeftClick()
    {
        return mouseState.LeftButton == ButtonState.Pressed &&
        oldMouseState.LeftButton == ButtonState.Released;
    }
    public static bool LeftHold()
    {
        return mouseState.LeftButton == ButtonState.Pressed &&
        oldMouseState.LeftButton == ButtonState.Pressed;
    }
    public static bool RightHold()
    {
        return mouseState.RightButton == ButtonState.Pressed &&
        oldMouseState.RightButton == ButtonState.Pressed;
    }
    public static bool RightClick()
    {
        return mouseState.RightButton == ButtonState.Pressed &&
        oldMouseState.RightButton == ButtonState.Released;
    }
    // Gets the mouse's bounds
    public static Rectangle GetMouseRect ()
    {
        Rectangle rect = new Rectangle(mouseState.X, mouseState.Y, 1, 1);
        return rect;
    }
    //Should be called at beginning of Update in Game
    public static void Update()
    {
        oldKeyState = keyState;
        keyState = Keyboard.GetState();
        oldMouseState = mouseState;
        mouseState = Mouse.GetState();
    }
}

