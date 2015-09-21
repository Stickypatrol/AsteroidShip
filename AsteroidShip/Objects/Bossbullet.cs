using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsteroidShip
{
    class Bossbullet : Entity
    {
        public Bossbullet(Game1 _game, Vector2 _position, Vector2 direction)
        {
            game = _game;
            tex = game.Content.Load<Texture2D>("bullet");
            position = _position;
            rotation = 0f;
            sourceRect = new Rectangle(0, 0, tex.Width, tex.Height);
            scale = 1f;
            origin = new Vector2(tex.Width / 2, tex.Height / 2);
            speed = new Vector2(direction.X*10, direction.Y*10);
        }
        public override void Update()
        {
            position += speed;
            position += game.objectController.basespeed;
        }
    }
}
