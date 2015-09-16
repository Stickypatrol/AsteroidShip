using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsteroidShip
{
    class Background
    {
        Game1 game;
        List<BackgroundStar> bgList;
        Random rand = new Random();

        public Background(Game1 _game)
        {
            game = _game;
            bgList = new List<BackgroundStar>();
            for (int i = 0; i < 30; i++)
            {
                bgList.Add(new BackgroundStar(game, new Vector2(rand.Next(0, game.GraphicsDevice.Viewport.Width),
                                                                rand.Next(0, game.GraphicsDevice.Viewport.Height)), rand.Next(0, 4), rand));
            }
        }
        public void Update()
        {
            for (int i = bgList.Count-1; i > 0; i--)
            {
                bgList[i].Update();
            }
        }
        public void Draw(SpriteBatch batch)
        {
            for (int i = 0; i < bgList.Count; i++)
            {
                bgList[i].Draw(batch);
            }
        }
    }
}
