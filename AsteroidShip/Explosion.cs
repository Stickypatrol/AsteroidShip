using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsteroidShip
{
    class Explosion : Entity
    {
        public Explosion(Game1 _game, Vector2 _position, bool _big)
        {
            game = _game;
            if(_big){
                tex = game.Content.Load<Texture2D>("bigexplosion");
            }else{
                tex = game.Content.Load<Texture2D>("smallexplosion");
            }
            position = new Vector2(_position.X - tex.Width/18, _position.Y - tex.Height/18);
            sizeX = tex.Width / 9;
            sizeY = tex.Height / 9;
            column = 0;
            row = 0;
            Console.WriteLine(sizeX + " " + sizeY);
            time = 0;
            scale = 1f;
            sourceRect = new Rectangle(sizeX * column, sizeY * row, sizeX, sizeY);
        }
        public override void Update()
        {
            time += 1;
            position += game.objectController.basespeed;
            if(time % 1 == 0){
                column++;
                if (column == 9)
                {
                    column = 0;
                    row++;
                }
            }
            rect = new Rectangle((int)position.X, (int)position.Y, tex.Width, tex.Height);
            sourceRect = new Rectangle(sizeX * column, sizeY * row, sizeX, sizeY);
        }
    }
}
