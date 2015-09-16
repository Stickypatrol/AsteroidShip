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
        
        public LevelController(Game1 _game)
        {
            game = _game;
            level = 0;
            startTime = -1;
            triggerTime = 0;
            diff = 1;
            rand = new Random();
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
        private void SpawnRate()
        {
            if (interval != 0 && gameTime.TotalGameTime.Milliseconds % interval == 0)
            {
                game.objectController.CreateAsteroid(side, amount);
            }
        }
        private void Timer()
        {
            if (totalTime - startTime > triggerTime)
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
            Console.WriteLine("easy");
            StartTime();
            triggerTime = 20;
            diff++;
            interval = 500;
            side = -1;
            amount = 1;
            game.objectController.isspawning = true;
        }
        private void HardLevel()
        {
            Console.WriteLine("hard");
            StartTime();
            triggerTime = 10;
            diff++;
            interval = 100;
            side = rand.Next(0, 4);
            amount = 1;
            game.objectController.isspawning = false;
        }
        private void BossLevel()
        {
            Console.WriteLine("boss");
            triggerTime = int.MaxValue;
            timerunning = false;
            interval = 0;
            game.objectController.isspawning = false;
            game.objectController.CreateBoss();
        }
        public void StartTime()
        {
            startTime = totalTime;
        }
    }
}