using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsteroidShip
{
    public class Target : Entity
    {
        public Target(Game1 _game)
        {
            game = _game;
            tex = game.Content.Load<Texture2D>("target");
            position = new Vector2(game.GraphicsDevice.Viewport.Width / 2, game.GraphicsDevice.Viewport.Height / 2 - tex.Height / 2);
            rotation = 0f;
            sourceRect = new Rectangle((int)position.X, (int)position.Y, tex.Width, tex.Height);
            scale = 1f;
            origin = new Vector2(tex.Width / 2, tex.Height / 2);
            speed = new Vector2(0, 0);
        }
        public override void Update()
        {
            position += speed;
            rect = new Rectangle((int)position.X, (int)position.Y, tex.Width, tex.Height);
            sourceRect = new Rectangle(0, 0, tex.Width, tex.Height);
        }
        public Vector2 getPosition
        {
            set
            {
                position = value;
            }
        }
    }
}
