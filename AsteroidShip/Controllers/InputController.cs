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
        GamePadState gamePad;
        public bool isconnected { get; set; }

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
            gamePad = GamePad.GetState(PlayerIndex.One);
            if (gamePad.IsConnected)
            {
                isconnected = true;
            }
            else
            {
                isconnected = false;
            }
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
        public Vector2 getRightJoystick()
        {
            return new Vector2(gamePad.ThumbSticks.Right.X, gamePad.ThumbSticks.Right.Y);
        }
        public Vector2 getLeftJoystick()
        {
            return new Vector2(gamePad.ThumbSticks.Left.X, gamePad.ThumbSticks.Left.Y);
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