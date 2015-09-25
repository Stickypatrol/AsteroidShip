using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AsteroidShip
{
    public class PlayerControls
    {
        Game1 game;
        InputController inputController;
        public Vector2 direction;
        public Vector2 shootdirection;

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
            int gamewidth = game.GraphicsDevice.Viewport.Width/2;
            int gameheight = game.GraphicsDevice.Viewport.Height/2;
            int shipwidth = game.objectController.ship.tex.Width / 2;
            int shipheight = game.objectController.ship.tex.Height / 2;
            shootdirection = new Vector2(direction.X - gamewidth, direction.Y - gameheight);
            if (inputController.getLeftMouseClick() || inputController.getRightTrigger())
            {
                game.objectController.CreateBullet(shootdirection,
                    new Vector2(gamewidth + shipwidth, gameheight + shipheight) -
                    new Vector2(shipwidth, shipheight));
                //game.soundController.PlaySound("shoot");
            }
            if (inputController.getRightMouseClick() || inputController.getLeftTrigger())
            {
                game.objectController.CreateBomb(shootdirection);
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