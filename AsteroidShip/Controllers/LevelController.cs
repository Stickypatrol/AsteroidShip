using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace AsteroidShip
{
    public class LevelController
    {
        Game1 game;
        GameTime gameTime;
        Random rand;
        int level, totalTime, startTime, interval, side, amount;
        int triggerTime { get; set; }
        bool timerunning = true;
        public string leveltype { get; set; }
        
        public LevelController(Game1 _game)
        {
            game = _game;
            level = 0;
            startTime = 0;
            triggerTime = 0;
            rand = new Random();
            leveltype = "Get Ready!";
            totalTime = 0;
        }
        public void Update(GameTime _gameTime)
        {
            gameTime = _gameTime;
            totalTime = (int)gameTime.TotalGameTime.TotalSeconds;
            if (timerunning)
            {
                Timer();
                SpawnRate();
            }
            Console.WriteLine(totalTime);
        }
        public int CountDown()
        {
            return triggerTime - (totalTime - startTime);
        }
        private void StartTime()
        {
            startTime = totalTime;
        }
        private void SpawnRate()
        {
            if (interval != 0 && gameTime.TotalGameTime.Milliseconds % interval == 0)
            {
                game.objectController.CreateAsteroid(side, amount);
            }
        }
        private void Timer()
        {
            if (totalTime - startTime == triggerTime)
            {//event happens after the triggertime
                triggerTime = int.MaxValue;
                NewLevel();
            }
        }
        private void NewLevel()
        {
            if (level == 0)
            {
                BeginLevel();
            }
            else if (level == 6)
            {
                BossLevel();
            }
            else if (level% 2 == 0)
            {
                HardLevel();
            }
            else
            {
                EasyLevel();
            }
            level++;
        }
        private void BeginLevel()
        {
            leveltype = "Get Ready!";
            Console.WriteLine("begin");
            StartTime();
            triggerTime = 5;
            interval = 0;
            side = -1;
            amount = 0;
            game.objectController.isspawning = false;
        }
        private void EasyLevel()
        {
            leveltype = "Easy level";
            Console.WriteLine("easy");
            StartTime();
            triggerTime = 1;
            interval = 500;
            side = -1;
            amount = 1;
            game.objectController.isspawning = true;
        }
        private void HardLevel()
        {
            leveltype = "Hard level";
            Console.WriteLine("hard");
            StartTime();
            triggerTime = 1;
            interval = 100;
            side = rand.Next(0, 4);
            amount = 1;
            game.objectController.isspawning = false;
        }
        private void BossLevel()
        {
            leveltype = "Boss level";
            Console.WriteLine("boss");
            triggerTime = -1;
            timerunning = false;
            interval = 0;
            game.objectController.isspawning = false;
            game.objectController.CreateBoss();
        }
    }
}