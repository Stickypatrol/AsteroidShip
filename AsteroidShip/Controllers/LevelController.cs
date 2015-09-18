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
        int level, diff, totalTime, timeSince, startTime, interval, side, amount, timeleft;
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
        }
        public void Update(GameTime _gameTime)
        {
            gameTime = _gameTime;
            totalTime = gameTime.TotalGameTime.Seconds;
            if (timerunning)
            {
                Timer();
                SpawnRate();
            }
        }
        public int CountDown()
        {
            return triggerTime - (totalTime - startTime);
        }
        public void StartTime()
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
            if (level == 5)
            {
                BossLevel();
            }
            else if (level == 0 || level % 2 == 0)
            {
                EasyLevel();
            }
            else
            {
                HardLevel();
            }
            level++;
        }
        private void EasyLevel()
        {
            leveltype = "Easy level";
            StartTime();
            triggerTime = 20;
            interval = 500;
            side = -1;
            amount = 1;
            game.objectController.isspawning = true;
        }
        private void HardLevel()
        {
            leveltype = "Hard level";
            StartTime();
            triggerTime = 10;
            interval = 100;
            side = rand.Next(0, 4);
            amount = 1;
            game.objectController.isspawning = false;
        }
        private void BossLevel()
        {
            leveltype = "Boss level";
            triggerTime = -1;
            timerunning = false;
            interval = 0;
            game.objectController.isspawning = false;
            game.objectController.CreateBoss();
        }
    }
}