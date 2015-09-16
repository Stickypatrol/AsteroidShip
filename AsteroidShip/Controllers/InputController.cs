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
        GamePadState curGamePad;
        GamePadState prevGamePad;
        Vector2 mouseSpeed;
        public bool isconnected { get; set; }

        public InputController(Game1 _game)
        {
            game = _game;
        }
        public void Update()
        {
            CheckController();
            prevKey = curKey;
            curKey = Keyboard.GetState();
            prevMouse = curMouse;
            if (isconnected)
            {
                MouseController();
            }
            curMouse = Mouse.GetState();
            prevGamePad = curGamePad;
            curGamePad = GamePad.GetState(PlayerIndex.One);
        }
        private void CheckController()
        {
            if (curGamePad.IsConnected)
            {
                isconnected = true;
            }
            else
            {
                isconnected = false;
            }
        }
        private void MouseController()
        {
            Console.WriteLine(getRightJoystick().X + " " + getRightJoystick().Y);
            mouseSpeed = new Vector2(getRightJoystick().X, getRightJoystick().Y);
            Mouse.SetPosition(curMouse.X + (int)(mouseSpeed.X*10), curMouse.Y + (int)(mouseSpeed.Y*-10));
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
            return new Vector2(curGamePad.ThumbSticks.Right.X, curGamePad.ThumbSticks.Right.Y);
        }
        public Vector2 getLeftJoystick()
        {
            return new Vector2(curGamePad.ThumbSticks.Left.X, curGamePad.ThumbSticks.Left.Y);
        }
        public Vector2 getMousePosition()
        {
            return new Vector2(curMouse.X, curMouse.Y);
        }
        public bool getTriggerClick()
        {
            if(curGamePad.IsButtonDown(Buttons.RightTrigger) && prevGamePad.IsButtonUp(Buttons.RightTrigger)){
                return true;
            }
            return false;
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