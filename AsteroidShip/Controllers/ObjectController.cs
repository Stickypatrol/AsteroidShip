using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;


namespace AsteroidShip
{
    public class ObjectController
    {
        public Vector2 basespeed;
        public bool isspawning;
        Game1 game;
        List<Bullet> bulletList;
        List<Asteroid> asteroidList;
        Boss boss;
        Random rand = new Random();
        InputController inputController;
        SpriteFont verdana;
        string score, countDown;
        int points, highscore, bossHealth, weaponMode;

        public ObjectController(Game1 _game)
        {
            game = _game;
            inputController = game.inputController;
            basespeed = new Vector2(0, 0);
            bulletList = new List<Bullet>();
            asteroidList = new List<Asteroid>();
            points = 0;
            highscore = 0;
            verdana = game.Content.Load<SpriteFont>("Verdana");
            countDown = "";
            bossHealth = -1;
            weaponMode = 3;
        }
        public void Update()
        {
            BaseSpeed();
            ObjectUpdate();
            if (boss != null)
            {
                boss.Update();
            }
        }
        private void BaseSpeed()
        {
            if (game.inputController.isconnected)
            {
                Vector2 leftController = game.inputController.getLeftJoystick();
                basespeed.Y += 0.2f * leftController.Y;
                basespeed.X += -0.2f * leftController.X;
                if (basespeed.Y > 5f)
                {
                    basespeed.Y = 5f;
                }
                else if (basespeed.Y < -5f)
                {
                    basespeed.Y = -5f;
                }
                if (basespeed.X > 5f)
                {
                    basespeed.X = 5f;
                }
                else if (basespeed.X < -5f)
                {
                    basespeed.X = -5f;
                }
            }
            else
            {
                if (game.inputController.getKeys(Keys.S) || game.inputController.getKeys(Keys.Down))
                {
                    if (basespeed.Y >= -5f)
                    {
                        basespeed.Y -= 0.2f;
                    }
                }
                if (game.inputController.getKeys(Keys.W) || game.inputController.getKeys(Keys.Up))
                {
                    if (basespeed.Y <= 5f)
                    {
                        basespeed.Y += 0.2f;
                    }
                }
                if (game.inputController.getKeys(Keys.D) || game.inputController.getKeys(Keys.Right))
                {
                    if (basespeed.X >= -5f)
                    {
                        basespeed.X -= 0.2f;
                    }
                }
                if (game.inputController.getKeys(Keys.A) || game.inputController.getKeys(Keys.Left))
                {
                    if (basespeed.X <= 5f)
                    {
                        basespeed.X += 0.2f;
                    }
                }
            }
        }
        private void ObjectUpdate()
        {
            if (boss != null)
            {
                if (boss.rect.Intersects(game.world.ship.rect))
                {
                    points -= 200;
                }
                for (int i = 0; i < bulletList.Count; i++)
                {
                    if (bulletList[i].rect.Intersects(boss.rect))
                    {
                        bulletList.RemoveAt(i);
                        bossHealth--;
                    }
                }
            }
            for (int i = bulletList.Count - 1; i >= 0; i--)
            {
                bulletList[i].Update();
                if (CheckBounds(bulletList[i].position, bulletList[i].tex, 0))
                {
                    bulletList.RemoveAt(i);
                }
            }
            for (int i = asteroidList.Count - 1; i >= 0; i--)
            {
                asteroidList[i].Update();
                if (CheckBounds(asteroidList[i].position, asteroidList[i].tex, 100))
                {
                    asteroidList.RemoveAt(i);
                }
            }
            for (int i = asteroidList.Count - 1; i >= 0; i--)
            {
                if (game.world.ship.rect.Intersects(asteroidList[i].rect))
                {
                    asteroidList.RemoveAt(i);
                    points -= 40;
                }
                else
                {
                    for (int p = bulletList.Count - 1; p >= 0; p--)
                    {
                        if (asteroidList[i].rect.Intersects(bulletList[p].rect))
                        {
                            points += 15;
                            asteroidList.RemoveAt(i);
                            bulletList.RemoveAt(p);
                            break;
                        }
                    }
                }
            }
            if (points > highscore)
            {
                highscore = points;
            }
        }
        private bool CheckBounds(Vector2 position, Texture2D tex, int offset)
        {
            int height = game.GraphicsDevice.Viewport.Height;
            int width = game.GraphicsDevice.Viewport.Width;

            if (position.Y > height + offset)
            {
                return true;
            }
            else if (position.Y + tex.Height < 0 - offset)
            {
                return true;
            }
            if (position.X > width + offset)
            {
                return true;
            }
            else if (position.X + tex.Width < 0 - offset)
            {
                return true;
            }
            return false;
        }
        public void CreateBullet(Vector2 direction)
        {
            if (weaponMode == 1)
            {
                bulletList.Add(new Bullet(game, direction, new Vector2(0,0)));
            }else if(weaponMode == 2){
                bulletList.Add(new Bullet(game, direction, new Vector2(direction.Y * 0.5f * -1f, direction.X * 0.5f)));
                bulletList.Add(new Bullet(game, direction, new Vector2(direction.Y * 0.5f, direction.X * 0.5f * -1f)));
            }
            else if (weaponMode == 3)
            {
                bulletList.Add(new Bullet(game, direction, new Vector2(0, 0)));
                bulletList.Add(new Bullet(game, direction, new Vector2(direction.Y * 0.5f * -1f, direction.X * 0.5f)));
                bulletList.Add(new Bullet(game, direction, new Vector2(direction.Y * 0.5f, direction.X * 0.5f * -1f)));
            }
        }
        public void CreateAsteroid(int side, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                asteroidList.Add(new Asteroid(game, side));
            }
        }
        public void CreateBoss()
        {
            boss = new Boss(game);
            bossHealth = 50;
        }
        public void Draw(SpriteBatch batch)
        {
            for (int i = 0; i < bulletList.Count; i++)
            {
                bulletList[i].Draw(batch);
            }
            for (int i = 0; i < asteroidList.Count; i++)
            {
                asteroidList[i].Draw(batch);
            }
            if (points < 0)
            {
                score = "You Lost, your highscore is " + highscore.ToString();
            }
            else
            {
                score = "Score = " + points.ToString();
            }
            if (boss != null)
            {
                boss.Draw(batch);
            }
            batch.DrawString(verdana, countDown, new Vector2((game.GraphicsDevice.Viewport.Width / 2), (game.GraphicsDevice.Viewport.Height / 3) * 2), Color.White);
            batch.DrawString(verdana, score, new Vector2((game.GraphicsDevice.Viewport.Width / 10), (game.GraphicsDevice.Viewport.Height / 10) * 2), Color.White);
        }
    }
}