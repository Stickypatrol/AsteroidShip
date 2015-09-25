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
        Rectangle destRect;
        public Ship(Game1 _game, Vector2 _position, float _rotation)
        {
            game = _game;
            tex = game.Content.Load<Texture2D>("ship");
            position = new Vector2(_position.X + tex.Width/4 + 25, _position.Y + tex.Height/3);
            sizeX = tex.Width / 4;
            sizeY = tex.Height / 3;
            column = 0;
            row = 2;
            scale = 1f;
            origin = new Vector2(tex.Width / 2, tex.Height / 2);
            speed = new Vector2(0, 0);
            rotation = 0f;
            radius = (float)Math.Sqrt(Math.Pow(tex.Height/3, 2) + Math.Pow(tex.Width/4, 2));
            sourceRect = new Rectangle(sizeX * column - sizeX, sizeY * row - sizeY, sizeX, sizeY);
            destRect = new Rectangle((int)position.X, (int)position.Y, sizeX, sizeY);
        }
        public override void Update()
        {
            Spriteselector();
            sourceRect = new Rectangle(sizeX * column, sizeY * row, sizeX, sizeY);
        }
        private void Spriteselector()
        {
            Console.WriteLine("update");
            if (game.objectController.moveDown)
            {
                Console.WriteLine("down");
                column = 0;
                row = 0;
                if (game.objectController.moveLeft)
                {
                    column = 1;
                    row = 1;
                }
                else if (game.objectController.moveRight)
                {
                    column = 0;
                    row = 1;
                }
            }
            else if (game.objectController.moveUp)
            {
                column = 2;
                row = 0;
                if (game.objectController.moveLeft)
                {
                    column = 2;
                    row = 1;
                }
                else if (game.objectController.moveRight)
                {
                    column = 3;
                    row = 1;
                }
            }
            else if (game.objectController.moveLeft)
            {
                column = 1;
                row = 0;
            }
            else if (game.objectController.moveRight)
            {
                column = 3;
                row = 0;
            }
            else
            {
                column = 0;
                row = 2;
            }
        }
        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(tex, destRect, sourceRect, Color.White, rotation, origin, SpriteEffects.None, 0f);
            Console.WriteLine(destRect.X +" "+ destRect.Y);
            Console.WriteLine(game.GraphicsDevice.Viewport.Width + " " + game.GraphicsDevice.Viewport.Height+" gamesize");
        }
    }
}
