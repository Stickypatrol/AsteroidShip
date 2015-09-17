using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsteroidShip
{
    class Boss : Entity
    {
        public Boss(Game1 _game)
        {
            game = _game;
            tex = game.Content.Load<Texture2D>("Boss");
            position = new Vector2(game.GraphicsDevice.Viewport.Width+200, game.GraphicsDevice.Viewport.Height/2 - tex.Height/2);
            origin = new Vector2(tex.Width / 2, tex.Height / 2);
            speed = new Vector2(0, 0);
            rotation = 0f;
            sourceRect = new Rectangle(0, 0, tex.Width, tex.Height);
            scale = 1f;
        }
        public override void Update()
        {
            Behaviour();
            position += speed;
            position += game.objectController.basespeed;
            rotation += 0.01f;
        }
        private void Behaviour()
        {
            if (position.X + tex.Width / 2 >= game.world.ship.position.X && speed.X > -3)
            {
                speed.X += -0.02f;
            }
            else if (position.X + tex.Width / 2 < game.world.ship.position.X && speed.X < 3)
            {
                speed.X += 0.02f;
            }
            if (position.Y + tex.Height / 2 >= game.world.ship.position.Y && speed.Y > -3)
            {
                speed.Y += -0.02f;
            }
            else if (position.Y + tex.Height / 2 < game.world.ship.position.Y && speed.Y < 3)
            {
                speed.Y += 0.02f;
            }
        }
    }
}
