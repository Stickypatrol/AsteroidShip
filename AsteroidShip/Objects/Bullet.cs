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
        public Bullet(Game1 _game, Vector2 direction)
        {
            game = _game;
            tex = game.Content.Load<Texture2D>("bullet");
            position = new Vector2(game.GraphicsDevice.Viewport.Width / 2, game.GraphicsDevice.Viewport.Height / 2 - tex.Height / 2);
            rotation = 0f;
            sourceRect = new Rectangle(0, 0, tex.Width, tex.Height);
            scale = 1f;
            origin = new Vector2(tex.Width / 2, tex.Height / 2);
            speed = new Vector2(direction.X/10, direction.Y/10);
            inputController = game.inputController;
            objectController = game.objectController;
        }
        public override void Update()
        {
            position += speed;
            position += game.objectController.basespeed;
            rect = new Rectangle((int)position.X, (int)position.Y, tex.Width, tex.Height);
        }
    }
}
