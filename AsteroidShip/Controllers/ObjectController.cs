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
        List<Entity> entityList;
        List<Bullet> bulletList;
        List<Asteroid> asteroidList;
        Boss boss;
        Random rand = new Random();
        InputController inputController;
        SpriteFont verdana;
        string score, countDown, levelType;
        int points, highscore, bossHealth, weaponMode;

        public ObjectController(Game1 _game)
        {
            game = _game;
            inputController = game.inputController;
            basespeed = new Vector2(0, 0);
            entityList = new List<Entity>();
            bulletList = new List<Bullet>();
            asteroidList = new List<Asteroid>();
            points = 0;
            highscore = 0;
            verdana = game.Content.Load<SpriteFont>("Verdana");
            countDown = "";
            levelType = "";
            bossHealth = -1;
            weaponMode = 1;
        }
        public void Update()
        {
            BaseSpeed();
            ObjectUpdate();
            if (boss != null)
            {
                boss.Update();
            }
            levelType = game.levelController.leveltype;
            if (game.levelController.CountDown()>-1)
            {
                countDown = game.levelController.CountDown().ToString();
            }
            else
            {
                countDown = "";
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
        private bool Collision(Entity a, Entity b)
        {
            int[] sides = new int[] { 0, 0, 0, 0 };
            Vector2[] A = new Vector2[4];
            Vector2[] B = new Vector2[4];
            Vector2 originA = new Vector2(a.position.X + (int)a.tex.Width / 2, a.position.Y + (int)a.tex.Height / 2);
            Vector2 originB = new Vector2(b.position.X + (int)b.tex.Width / 2, b.position.Y + (int)b.tex.Height / 2);
            float radiusA = (float)Math.Sqrt(2f * Math.Pow(a.tex.Width, 2));
            float radiusB = (float)Math.Sqrt(2f * Math.Pow(b.tex.Width, 2));
            A[0] = new Vector2(radiusA * (float)Math.Cos(a.rotation%2*Math.PI), radiusA * (float)Math.Sin(a.rotation%2*Math.PI));
            A[2] = new Vector2((A[0].X - originA.X) * -1 + originA.X, (A[0].X - originA.Y) * -1 + originA.Y);
            A[1] = new Vector2(originA.X + A[0].Y - originA.Y, originA.Y + -(A[0].X - originA.X));
            A[3] = new Vector2((A[1].X - originA.X) * -1 + originA.X, (A[1].X - originA.Y) * -1 + originA.Y);
            B[0] = new Vector2(radiusB * (float)Math.Cos(b.rotation%2*Math.PI), radiusB * (float)Math.Sin(b.rotation%2*Math.PI));
            B[2] = new Vector2((B[0].X - originB.X) * -1 + originB.X, (B[0].X - originB.Y) * -1 + originB.Y);
            B[1] = new Vector2(originB.X + B[0].Y - originB.Y, originB.Y + -(B[0].X - originB.X));
            B[3] = new Vector2((B[1].X - originB.X) * -1 + originB.X, (B[1].X - originB.Y) * -1 + originB.Y);
            for (int p = 0; p < B.Length; p++)
			{
                for (int i = 1; i < A.Length+1; i++)
                {
                    float deltaY = 0;
                    float deltaX = 0;
                    if(A[i%4].Y - A[i-1].Y != 0){
                        deltaY = (float)Math.Abs(A[i%4].Y - A[i-1].Y);
                    }else{
                        deltaY = 0;
                    }
                    if(A[i%4].X - A[i-1].X != 0){
                        deltaX = (float)Math.Abs(A[i%4].X - A[i-1].X);
                    }else{
                        deltaX = 0;
                    }
                    if (deltaY == 0 || deltaX == 0){
                        if (A[i].X > B[p].X){
                            sides[i - 1] = 1;
                        }else{
                            sides[i - 1] = -1;
                        }
                    }else{
                        if ((deltaY / deltaX) * B[p].X > B[p].Y){//target coordinate is above the A line
                            sides[i - 1] = 1;
                        }else{
                            sides[i - 1] = -1;
                        }
                    }
                }
            }
            for (int i = 0; i < sides.Length-1; i++)
            {
                if (sides[i] == sides[i + 1])
                {
                    if(sides.Sum() == 0){
                        return true;
                    }
                }
            }
            return false;
        }
        private void ObjectUpdate()
        {
            if (boss != null)
            {
                if (boss.rect.Intersects(game.world.ship.rect))
                {
                    points -= 200;//boss - ship intersection
                }
                for (int i = 0; i < bulletList.Count; i++)
                {
                    if (bulletList[i].rect.Intersects(boss.rect))
                    {
                        bulletList.RemoveAt(i);//boss - bullet intersection
                        bossHealth--;
                    }
                }
            }
            if (boss != null && bossHealth < 1)
            {
                boss = null;//checks if boss is dead
                points += 500;
            }
            for (int i = bulletList.Count - 1; i >= 0; i--)
            {
                bulletList[i].Update();
                if (CheckBounds(bulletList[i].position, bulletList[i].tex, 0))
                {
                    bulletList.RemoveAt(i);//bullet bounds checking
                }
            }
            for (int i = asteroidList.Count - 1; i >= 0; i--)
            {
                asteroidList[i].Update();
                if (CheckBounds(asteroidList[i].position, asteroidList[i].tex, 100))
                {
                    asteroidList.RemoveAt(i);//asteroid bounds checking
                }
            }
            for (int i = asteroidList.Count - 1; i >= 0; i--)
            {
                if (game.world.ship.rect.Intersects(asteroidList[i].rect))
                {
                    asteroidList.RemoveAt(i);//asteroid - ship collision
                    points -= 40;
                }
                else
                {
                    for (int p = bulletList.Count - 1; p >= 0; p--)
                    {
                        if (asteroidList[i].rect.Intersects(bulletList[p].rect))
                        {
                            points += 15;
                            asteroidList.RemoveAt(i);//asteroid - bullet collision
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
            if (points > 80)
            {
                weaponMode = 3;
            }
            else if (points > 400)
            {
                weaponMode = 2;
            }
            else
            {
                weaponMode = 1; 
            }
            if (weaponMode == 1)
            {
                bulletList.Add(new Bullet(game, direction, new Vector2(0,0)));
            }else if(weaponMode == 2){
                bulletList.Add(new Bullet(game, direction, new Vector2(direction.Y * 0.2f * -1f, direction.X * 0.2f)));
                bulletList.Add(new Bullet(game, direction, new Vector2(direction.Y * 0.2f, direction.X * 0.2f * -1f)));
            }
            else if (weaponMode == 3)
            {
                bulletList.Add(new Bullet(game, direction, new Vector2(0, 0)));
                bulletList.Add(new Bullet(game, direction, new Vector2(direction.Y * 0.2f * -1f, direction.X * 0.2f)));
                bulletList.Add(new Bullet(game, direction, new Vector2(direction.Y * 0.2f, direction.X * 0.2f * -1f)));
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
            bossHealth = int.MaxValue;
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
            batch.DrawString(verdana, countDown, new Vector2((game.GraphicsDevice.Viewport.Width / 2)-10, (game.GraphicsDevice.Viewport.Height / 3) * 2), Color.White);
            batch.DrawString(verdana, score, new Vector2((game.GraphicsDevice.Viewport.Width / 10), (game.GraphicsDevice.Viewport.Height / 10) * 2), Color.White);
            batch.DrawString(verdana, levelType, new Vector2((game.GraphicsDevice.Viewport.Width / 2) - 50, (game.GraphicsDevice.Viewport.Height / 11) * 8), Color.White);
        }
    }
}