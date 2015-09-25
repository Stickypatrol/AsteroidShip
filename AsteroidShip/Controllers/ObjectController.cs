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
        public bool isSpawning, moveUp, moveDown, moveRight, moveLeft;
        public Ship ship;
        public Random rand = new Random();
        Game1 game;
        Gun gun;
        InputController inputController;
        SpriteFont verdana;
        List<Entity> entityList;
        List<int> cleanList;
        string score, countDown, levelType;
        int points, highscore, weaponMode;

        public ObjectController(Game1 _game)
        {
            game = _game;
            inputController = game.inputController;
            basespeed = new Vector2(0, 0);
            entityList = new List<Entity>();
            cleanList = new List<int>();
            points = 0;
            highscore = 0;
            verdana = game.Content.Load<SpriteFont>("Verdana");
            countDown = "";
            levelType = "";
            weaponMode = 1;
            ship = new Ship(game, new Vector2(game.GraphicsDevice.Viewport.Width / 2, game.GraphicsDevice.Viewport.Height / 2), 0f);
            gun = new Gun(game, new Vector2(game.GraphicsDevice.Viewport.Width / 2, game.GraphicsDevice.Viewport.Height / 2));
            entityList.Add(ship);
            entityList.Add(gun);
        }
        public void Update()
        {
            BaseSpeed();
            ObjectUpdate();
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
                        moveUp = false;
                        moveDown = true;
                    }
                }
                else if (game.inputController.getKeys(Keys.W) || game.inputController.getKeys(Keys.Up))
                {
                    if (basespeed.Y <= 5f)
                    {
                        basespeed.Y += 0.2f;
                        moveUp = true;
                        moveDown = false;
                    }
                }
                else
                {
                    moveUp = false;
                    moveDown = false;
                }
                if (game.inputController.getKeys(Keys.D) || game.inputController.getKeys(Keys.Right))
                {
                    if (basespeed.X >= -5f)
                    {
                        basespeed.X -= 0.2f;
                        moveLeft = false;
                        moveRight = true;
                    }
                }
                else if (game.inputController.getKeys(Keys.A) || game.inputController.getKeys(Keys.Left))
                {
                    if (basespeed.X <= 5f)
                    {
                        basespeed.X += 0.2f;
                        moveLeft = true;
                        moveRight = false;
                    }
                }
                else
                {
                    moveLeft = false;
                    moveRight = false;
                }
            }
        }
        private bool Collision(Entity a, Entity b)
        {
            if (a.position.X - b.position.X > a.radius + b.radius && a.position.Y - b.position.Y > a.radius + b.radius)
            {
                return false;
                //this is the pruning
            }
            int[] sides = new int[] { 0, 0, 0, 0 };
            Vector2[] A = new Vector2[4];
            Vector2[] B = new Vector2[4];
            for (int i = 0; i < 4; i++)
            {
                A[i] = new Vector2(a.radius / 2 * (float)Math.Cos((a.rotation + (Math.PI / 4) + (Math.PI * 0.5 * i)) % (2 * Math.PI)),
                    a.radius / 2 * (float)Math.Sin((a.rotation + (Math.PI / 4) + (Math.PI * 0.5 * i)) % (2 * Math.PI))) + a.position - a.origin;
                B[i] = new Vector2(b.radius / 2 * (float)Math.Cos((b.rotation + (Math.PI / 4) + (Math.PI * 0.5 * i)) % (2 * Math.PI)),
                    b.radius / 2 * (float)Math.Sin((b.rotation + (Math.PI / 4) + (Math.PI * 0.5 * i)) % (2 * Math.PI))) + b.position - b.origin;
            }
            for (int p = 0; p < B.Length; p++)
            {//I've fixed the above, I just need to shorten the next line of ifs
                for (int i = 0; i < A.Length; i++)
                {
                    float deltaY = 0;
                    float deltaX = 0;
                    if (A[i].Y - A[(i + 1) % 4].Y != 0)
                    {
                        deltaY = A[i].Y - A[(i + 1) % 4].Y;
                    }
                    else
                    {
                        deltaY = 0;
                    }
                    if (A[i].X - A[(i + 1) % 4].X != 0)
                    {
                        deltaX = A[i].X - A[(i + 1) % 4].X;
                    }
                    else
                    {
                        deltaX = 0;
                    }
                    if (deltaY == 0)
                    {
                        if (A[i].Y > B[p].Y)
                        {
                            sides[i] = 1;
                        }
                        else
                        {
                            sides[i] = -1;
                        }
                    }
                    else if (deltaX == 0)
                    {
                        if (A[i].X > B[p].X)
                        {
                            sides[i] = 1;
                        }
                        else
                        {
                            sides[i] = -1;
                        }
                    }
                    else
                    {
                        float slope = deltaY / deltaX;
                        float Yintercept = A[i].Y - (slope * A[i].X);
                        if ((slope * B[p].X) + Yintercept > B[p].Y)
                        {
                            sides[i] = 1;
                        }
                        else
                        {
                            sides[i] = -1;
                        }
                    }
                }
                for (int i = 0; i < sides.Length; i++)
                {
                    if (sides.Sum() == 0)
                    {
                        if (sides[i] == sides[(i + 1) % 4])
                        {
                            return true;
                        }
                    }
                    else
                    {
                        break;
                    }//2nd part, compares the rectangles the other way around
                }
                for (int i = 0; i < B.Length; i++)
                {
                    float deltaY = 0;
                    float deltaX = 0;
                    if (B[i].Y - B[(i + 1) % 4].Y != 0)
                    {
                        deltaY = B[i].Y - B[(i + 1) % 4].Y;
                    }
                    else
                    {
                        deltaY = 0;
                    } if (B[i].X - B[(i + 1) % 4].X != 0)
                    {
                        deltaX = B[i].X - B[(i + 1) % 4].X;
                    }
                    else
                    {
                        deltaX = 0;
                    }
                    if (deltaY == 0)
                    {
                        if (B[i].Y > A[p].Y)
                        {
                            sides[i] = 1;
                        }
                        else
                        {
                            sides[i] = -1;
                        }
                    }
                    else if (deltaX == 0)
                    {
                        if (B[i].X > A[p].X)
                        {
                            sides[i] = 1;
                        }
                        else
                        {
                            sides[i] = -1;
                        }
                    }
                    else
                    {
                        float slope = deltaY / deltaX;
                        float Yintercept = B[i].Y - (slope * B[i].X);
                        if ((slope * A[p].X) + Yintercept > A[p].Y)
                        {
                            sides[i] = 1;
                        }
                        else
                        {
                            sides[i] = -1;
                        }
                    }
                }
                for (int i = 0; i < sides.Length; i++)
                {
                    if (sides.Sum() == 0)
                    {
                        if (sides[i] == sides[(i + 1) % 4])
                        {
                            return true;
                        }
                    }
                    else
                    {
                        break;
                    }//2nd part, compares the rectangles the other way around
                }
            }
            return false;
        }
        private int CollisionEventHandler(Entity A, Entity B)
        {//this method handles the collision event, it takes the cleanList and returns it filled with shit that has to be cleaned up
            if (A is Ship)
            {
                if (B is Boss)
                {
                    points -= 100;
                }
                else if (B is Asteroid)
                {
                    points -= 40;
                    return 2;
                }
                else if (B is Bossbullet)
                {
                    points -= 25;
                    return 2;
                }
            }
            else if (A is Bullet)
            {
                if (B is Boss)
                {
                    return 1;
                    B.health--;
                }
                else if (B is Asteroid)
                {
                    points += 15;
                    entityList.Add(new Explosion(game, B.position, false));
                    return 3;
                }
            }
            else if (A is Bomb)
            {
                if (B is Asteroid)
                {
                    return 2;
                }
            }
            return 0;
            //if A is to be removed then 1
            //if B '' then 2
            //if both '' then 3
            //if nothing then 0
        }
        private bool DestructionHandler(Entity entity)
        {
            if (entity is Bomb)
            {
                if (entity.time > 25)
                {
                    return true;
                }
            }
            else if (entity is Boss)
            {
                if (entity.health < 0)
                {
                    entityList.Add(new Explosion(game, entity.position, true));
                    return true;
                }
            }
            else if (entity is Explosion)
            {
                if (entity.time >= 900)
                {
                    return true;
                }
            }
            return false;
        }
        private void ObjectUpdate()
        {
            for (int i = 0; i < entityList.Count; i++)
            {
                if (CheckBounds(entityList[i]))
                {
                    cleanList.Add(i);
                }
            }
            for (int p = 0; p < entityList.Count; p++)
            {
                for (int i = 0; i < entityList.Count; i++)
                {
                    if (i != p)
                    {
                        if (Collision(entityList[p], entityList[i]))
                        {
                            int result = CollisionEventHandler(entityList[p], entityList[i]);
                            if (result == 1)
                            {
                                cleanList.Add(p);
                            }
                            else if (result == 2)
                            {
                                cleanList.Add(i);
                            }
                            else if (result == 3)
                            {
                                cleanList.Add(p);
                                cleanList.Add(i);
                            }
                        }
                    }
                }
                if (DestructionHandler(entityList[p]))
                {
                    cleanList.Add(p);
                }
                else
                {
                    entityList[p].Update();
                }
            }
            cleanList = cleanList.Distinct().ToList();
            cleanList.Sort();
            for (int i = cleanList.Count-1; i >= 0; i--)
            {
                entityList.RemoveAt(cleanList[i]);
            }
            cleanList.Clear();
        }
        private bool CheckBounds(Entity obj)
        {
            if (obj is Boss || obj is Bossbullet)
            {
                return false;
            }
            int offset = 50;
            Vector2 position = obj.position;
            Texture2D tex = obj.tex;

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
        public void CreateBullet(Vector2 direction, Vector2 position)
        {
            if (points > 1700)
            {
                weaponMode = 3;
            }
            else if (points > 800)
            {
                weaponMode = 2;
            }
            else
            {
                weaponMode = 1; 
            }
            if (weaponMode == 1)
            {
                entityList.Add(new Bullet(game, direction, new Vector2(0,0), position));
            }else if(weaponMode == 2){
                entityList.Add(new Bullet(game, direction, new Vector2(direction.Y * 0.2f * -1f, direction.X * 0.2f), position));
                entityList.Add(new Bullet(game, direction, new Vector2(direction.Y * 0.2f, direction.X * 0.2f * -1f), position));
            }
            else if (weaponMode == 3)
            {
                entityList.Add(new Bullet(game, direction, new Vector2(0, 0), position));
                entityList.Add(new Bullet(game, direction, new Vector2(direction.Y * 0.2f * -1f, direction.X * 0.2f), position));
                entityList.Add(new Bullet(game, direction, new Vector2(direction.Y * 0.2f, direction.X * 0.2f * -1f), position));
            }
        }
        public void CreateBossBullet(Vector2 position, Vector2 direction)
        {
            entityList.Add(new Bossbullet(game, position, direction));
        }
        public void CreateBomb(Vector2 direction)
        {
            entityList.Add(new Bomb(game, direction));
        }
        public void CreateAsteroid(int side, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                entityList.Add(new Asteroid(game, side));
            }
        }
        public void CreateBoss()
        {
            entityList.Add(new Boss(game));
        }
        public void Draw(SpriteBatch batch)
        {
            foreach (Entity item in entityList)
            {
                item.Draw(batch);
            }
            if (points < 0)
            {
                score = "You Lost, your highscore is " + highscore.ToString();
            }
            else
            {
                score = "Score = " + points.ToString();
            }
            batch.DrawString(verdana, countDown, new Vector2((game.GraphicsDevice.Viewport.Width / 2)-10, (game.GraphicsDevice.Viewport.Height / 3) * 2), Color.White);
            batch.DrawString(verdana, score, new Vector2((game.GraphicsDevice.Viewport.Width / 10), (game.GraphicsDevice.Viewport.Height / 10) * 2), Color.White);
            batch.DrawString(verdana, levelType, new Vector2((game.GraphicsDevice.Viewport.Width / 2) - 50, (game.GraphicsDevice.Viewport.Height / 11) * 8), Color.White);
        }
    }
}