using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsteroidShip
{
    public class Ship : Entity
    {
        int health;
        public Ship(Game1 _game, Vector2 _position, int _health, float _rotation)
        {
            game = _game;
            position = _position;
            health = _health;
            rotation = _rotation;
            tex = game.Content.Load<Texture2D>("Red");
            sourceRect = new Rectangle(0, 0, tex.Width, tex.Height);
            scale = 1f;
            origin = new Vector2(tex.Width / 2, tex.Height / 2);
            speed = new Vector2(0, 0);
        }
        public override void Update()
        {
            rect = new Rectangle((int)position.X, (int)position.Y, tex.Width, tex.Height);
        }
    }
}
