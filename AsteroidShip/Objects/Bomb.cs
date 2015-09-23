using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsteroidShip
{
    class Bomb : Entity, IDestructible
    {
        InputController inputController;
        ObjectController objectController;

        public Bomb(Game1 _game, Vector2 direction)
        {
            game = _game;
            tex = game.Content.Load<Texture2D>("Bomb");
            position = new Vector2(game.GraphicsDevice.Viewport.Width / 2, game.GraphicsDevice.Viewport.Height / 2);
            rotation = 0f;
            sourceRect = new Rectangle(0, 0, tex.Width, tex.Height);
            scale = 1f;
            origin = new Vector2(tex.Width / 2, tex.Height / 2);
            speed = new Vector2(direction.X / 8, direction.Y / 8);
            inputController = game.inputController;
            objectController = game.objectController;
            time = 0;
            radius = (float)Math.Sqrt(Math.Pow(tex.Height, 2) + Math.Pow(tex.Width, 2));
        }
        public override void Update()
        {
            if (time == 25)
            {
                Explode();
            }
            time++;
            position += speed;
            position += game.objectController.basespeed;
            rect = new Rectangle((int)position.X, (int)position.Y, tex.Width, tex.Height);
            
        }
        public void Explode()
        {
            for (float i = 0; i < 2; i+=0.125f)
            {
                game.objectController.CreateBullet(new Vector2(-25f * (float)(Math.Sin(Math.PI * i)), 25f * (float)(Math.Cos(Math.PI * i))), new Vector2(position.X + tex.Width / 2, position.Y + tex.Height / 2));
            }
        }
    }
}
