﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsteroidShip
{
    class Boss : Entity, IDestructible
    {

        public Boss(Game1 _game)
        {
            game = _game;
            tex = game.Content.Load<Texture2D>("bossguiseppe");
            position = new Vector2(game.GraphicsDevice.Viewport.Width+200, game.GraphicsDevice.Viewport.Height/2 - tex.Height/2);
            origin = new Vector2(tex.Width/2f, tex.Height/2f);
            speed = new Vector2(0, 0);
            rotation = 0f;
            sourceRect = new Rectangle(0, 0, tex.Width, tex.Height);
            scale = 1f;
            health = 50;
            radius = (float)Math.Sqrt(Math.Pow(tex.Height, 2) + Math.Pow(tex.Width, 2));
        }
        public override void Update()
        {
            if (time > 60)
            {
                Shoot();
                time = 0;
            }
            time++;
            Follow();
            position += speed;
            position += game.objectController.basespeed;
            rotation += 0.01f;
            rect = new Rectangle((int)position.X, (int)position.Y, 0, 0);
        }
        public void Explode()
        {
            if (health <= 0)
            {
                for (float i = 0; i < 2; i += 0.1f)
                {
                    game.objectController.CreateBossBullet(new Vector2(-25f * (float)(Math.Sin(Math.PI * i)), 25f * (float)(Math.Cos(Math.PI * i))), new Vector2(position.X + tex.Width / 2, position.Y + tex.Height / 2));
                }
            }
            health--;
        }
        private void Shoot()
        {
            float targetX = game.objectController.ship.position.X;
            float targetY = game.objectController.ship.position.Y;
            Vector2 target = new Vector2(0,0);
            if (game.objectController.ship.position.X >= position.X &&
                game.objectController.ship.position.Y >= position.Y)
            {//rightbottom
                float angle = (float)Math.Atan((position.Y - game.objectController.ship.position.Y) / (position.X - game.objectController.ship.position.X));
                target = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
            }else if(game.objectController.ship.position.X >= position.X &&
                game.objectController.ship.position.Y < position.Y)
            {//righttop
                float angle = (float)Math.Atan((position.Y - game.objectController.ship.position.Y) / (position.X - game.objectController.ship.position.X));
                target = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
            }else if(game.objectController.ship.position.X < position.X &&
                game.objectController.ship.position.Y < position.Y)
            {//lefttop
                float angle = (float)Math.Atan((position.Y - game.objectController.ship.position.Y) / (position.X - game.objectController.ship.position.X));
                target = new Vector2(-(float)Math.Cos(angle), -(float)Math.Sin(angle));
            }else if(game.objectController.ship.position.X < position.X &&
                game.objectController.ship.position.Y >= position.Y)
            {//leftbottom
                float angle = (float)Math.Atan((position.Y - game.objectController.ship.position.Y) / (position.X - game.objectController.ship.position.X));
                target = new Vector2(-(float)Math.Cos(angle), -(float)Math.Sin(angle));
            }
            game.objectController.CreateBossBullet(position, target);
        }
        private void Follow()
        {
            if (position.X + tex.Width / 2 >= game.objectController.ship.position.X && speed.X > -3)
            {
                speed.X += -0.02f;
            }
            else if (position.X + tex.Width / 2 < game.objectController.ship.position.X && speed.X < 3)
            {
                speed.X += 0.02f;
            }
            if (position.Y + tex.Height / 2 >= game.objectController.ship.position.Y && speed.Y > -3)
            {
                speed.Y += -0.02f;
            }
            else if (position.Y + tex.Height / 2 < game.objectController.ship.position.Y && speed.Y < 3)
            {
                speed.Y += 0.02f;
            }
        }
    }
}
