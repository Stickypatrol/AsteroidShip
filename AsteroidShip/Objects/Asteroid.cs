using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsteroidShip
{
    class Asteroid : Entity
    {
        ObjectController objectController;
        InputController inputController;
        Random rand = new Random();

        public Asteroid(Game1 _game, int side)
        {
            game = _game;
            tex = game.Content.Load<Texture2D>("black");
            if (side == -1) { side = rand.Next(0, 4); };
            if (side == 0)
            {
                position = new Vector2(-100, (int)(rand.NextDouble() * game.GraphicsDevice.Viewport.Height));//move right
                speed = new Vector2((float)rand.NextDouble() * rand.Next(1, 5)+1, (float)rand.NextDouble() * rand.Next(-3, 2)+1);
            }
            else if (side == 1)
            {
                position = new Vector2(game.GraphicsDevice.Viewport.Width+100, (int)(rand.NextDouble() * game.GraphicsDevice.Viewport.Height));
                speed = new Vector2((float)rand.NextDouble() * rand.Next(-4, 0), (float)rand.NextDouble() * rand.Next(-3, 2));//move left
            }
            else if(side == 2)
            {
                position = new Vector2((int)(rand.NextDouble() * game.GraphicsDevice.Viewport.Width), -100);//move down
                speed = new Vector2((float)rand.NextDouble() * rand.Next(-3, 2), (float)rand.NextDouble() * rand.Next(1, 5));
            }
            else if(side == 3)
            {
                position = new Vector2((int)(rand.NextDouble() * game.GraphicsDevice.Viewport.Width), game.GraphicsDevice.Viewport.Height+100);
                speed = new Vector2((float)rand.NextDouble() * rand.Next(-3, 2), (float)rand.NextDouble() * rand.Next(-4, 0));//move up
            }
            rotation = (float)rand.NextDouble();
            sourceRect = new Rectangle(0, 0, tex.Width, tex.Height);
            scale = 1f;
            origin = new Vector2(tex.Width / 2, tex.Height / 2);
            inputController = game.inputController;
            objectController = game.objectController;
            radius = (float)Math.Sqrt(Math.Pow(tex.Height, 2) + Math.Pow(tex.Width, 2));
        }
        public override void Update()
        {
            position += speed;
            position += game.objectController.basespeed;
            rect = new Rectangle((int)position.X, (int)position.Y, tex.Width, tex.Height);
        }
    }
}
