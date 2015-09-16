using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsteroidShip
{
    class BackgroundStar : Entity
    {
        Random rand;

        public BackgroundStar(Game1 _game, Vector2 _position, int startex, Random _rand)
        {
            game = _game;
            position = _position;
            rotation = 0f;
            if (startex == 0)
            {
                tex = game.Content.Load<Texture2D>("star1");
                speed = new Vector2(0, 0);
                multiplier = 0.4f;
            }
            else if(startex == 1)
            {
                tex = game.Content.Load<Texture2D>("star2");
                speed = new Vector2(0, 0);
                multiplier = 0.5f;
            }
            else if (startex == 2)
            {
                tex = game.Content.Load<Texture2D>("star3");
                speed = new Vector2(0, 0);
                multiplier = 0.5f;
            }
            else if (startex == 3)
            {
                tex = game.Content.Load<Texture2D>("star4");
                speed = new Vector2(0, 0);
                multiplier = 0.4f;
            }
            sourceRect = new Rectangle((int)position.X, (int)position.Y, tex.Width, tex.Height);
            scale = 0.5f;
            origin = new Vector2(tex.Width / 2, tex.Height / 2);
            rand = _rand;
        }
        public override void Update()
        {
            Vector2 basespeed = game.objectController.basespeed;
            speed.X = basespeed.X * multiplier;
            speed.Y = basespeed.Y * multiplier;
            position += speed;
            rect = new Rectangle((int)position.X, (int)position.Y, tex.Width, tex.Height);
            sourceRect = new Rectangle(0, 0, tex.Width, tex.Height);
            CheckBounds();
        }
        private void CheckBounds()
        {
            if (rect.Bottom < 0)
            {
                position.Y = game.GraphicsDevice.Viewport.Height + tex.Height/2;
                position.X = rand.Next(0, game.GraphicsDevice.Viewport.Width);
            }
            else if (position.Y - tex.Height/2> game.GraphicsDevice.Viewport.Height)
            {
                position.Y = 0 - tex.Height/2;
                position.X = rand.Next(0, game.GraphicsDevice.Viewport.Width);
            }
            if (position.X - tex.Width / 2 > game.GraphicsDevice.Viewport.Width)
            {
                position.X = 0 - tex.Width / 2;
                position.Y = rand.Next(0, game.GraphicsDevice.Viewport.Height);
            }
            else if (rect.Right < 0)
            {
                position.X = game.GraphicsDevice.Viewport.Width + tex.Width / 2;
                position.Y = rand.Next(0, game.GraphicsDevice.Viewport.Height);
            }
        }
    }
}
