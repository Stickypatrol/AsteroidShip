using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace AsteroidShip
{
    public class InputController
    {
        Game1 game;
        KeyboardState curKey;
        KeyboardState prevKey;
        MouseState curMouse;
        MouseState prevMouse;

        public InputController(Game1 _game)
        {
            game = _game;
        }
        public void Update()
        {
            prevKey = curKey;
            curKey = Keyboard.GetState();
            prevMouse = curMouse;
            curMouse = Mouse.GetState();
            
        }
        public bool getKey(Keys keyvar){
            if (curKey.IsKeyDown(keyvar) && prevKey.IsKeyUp(keyvar)){
                return true;
            } return false;
        }
        public bool getKeys(Keys keyvar)
        {
            if (curKey.IsKeyDown(keyvar))
            {
                return true;
            } return false;
        }
        public Vector2 getMousePosition()
        {
            return new Vector2(curMouse.X, curMouse.Y);
        }
        public bool getMouseClick()
        {
            if(curMouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton == ButtonState.Released){
                return true;
            }
            return false;
        }
    }
}