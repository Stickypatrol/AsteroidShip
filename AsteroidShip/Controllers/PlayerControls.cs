using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AsteroidShip
{
    class PlayerControls
    {
        Game1 game;
        InputController inputController;
        Vector2 direction;


        public PlayerControls(Game1 _game)
        {
            game = _game;
            inputController = game.inputController;
            game.IsMouseVisible = false;
        }
        public void Update()
        {
            MouseControls(inputController.getMousePosition());
            ShootControl();
        }
        private void ShootControl()
        {
            if (inputController.getLeftMouseClick() || inputController.getRightTrigger())
            {
                game.objectController.CreateBullet(new Vector2(direction.X - game.GraphicsDevice.Viewport.Width/2, direction.Y - game.GraphicsDevice.Viewport.Height/2), 
                    new Vector2(game.GraphicsDevice.Viewport.Width / 2 + game.objectController.ship.tex.Width / 2, game.GraphicsDevice.Viewport.Height / 2 + game.objectController.ship.tex.Height / 2) -
                    new Vector2(game.objectController.ship.tex.Width / 2, game.objectController.ship.tex.Height / 2));
            }
            if (inputController.getRightMouseClick() || inputController.getLeftTrigger())
            {
                game.objectController.CreateBomb(new Vector2(direction.X - game.GraphicsDevice.Viewport.Width/2, direction.Y - game.GraphicsDevice.Viewport.Height/2));
            }
        }
        private void MouseControls(Vector2 mousePos)
        {
            int width = game.GraphicsDevice.Viewport.Width / 2;
            int height = game.GraphicsDevice.Viewport.Height / 2;
            mousePos.X -= width;
            mousePos.Y -= height;
            float ratio = (float)Math.Cos(Math.Atan(mousePos.Y / mousePos.X));
            float x = ratio * 75;
            float y = (float)Math.Sqrt(Math.Pow(75, 2) - x * Math.Abs(x));
            if (mousePos.X >= 0 && mousePos.Y >= 0)
            {
                direction = new Vector2((int)x + width, (int)y + height);
                game.world.target.position = direction;
            }
            else if (mousePos.X >= 0 && mousePos.Y < 0)
            {
                direction = new Vector2((int)x + width, (int)-y + height);
                game.world.target.position = direction;
            }
            else if (mousePos.X < 0 && mousePos.Y < 0)
            {
                direction = new Vector2((int)-x + width, (int)-y + height);
                game.world.target.position = direction;
            }
            else if (mousePos.X < 0 && mousePos.Y >= 0)
            {
                direction = new Vector2((int)-x + width, (int)y + height);
                game.world.target.position = direction;
            }
        }
    }
}