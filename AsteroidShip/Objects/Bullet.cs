using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsteroidShip
{
    class Bullet : Entity
    {
        InputController inputController;
        ObjectController objectController;

        public Bullet(Game1 _game, Vector2 direction, Vector2 offset, Vector2 _position)
        {
            game = _game;
            tex = game.Content.Load<Texture2D>("bullet");
            position = _position + offset;
            rotation = 0f;
            sourceRect = new Rectangle(0, 0, tex.Width, tex.Height);
            scale = 1f;
            origin = new Vector2(tex.Width / 2, tex.Height / 2);
            speed = new Vector2(direction.X/8, direction.Y/8);
            inputController = game.inputController;
            objectController = game.objectController;
            radius = (float)Math.Sqrt(Math.Pow(tex.Height, 2) + Math.Pow(tex.Width, 2));
        }
        public override void Update()
        {
            position += speed;
            position += game.objectController.basespeed;
        }
    }
}
