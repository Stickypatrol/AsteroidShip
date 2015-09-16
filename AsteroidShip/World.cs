using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsteroidShip
{
    public class World
    {
        Game1 game;
        Vector2 position;
        Texture2D tex;
        Background background;
        public Ship ship;
        PlayerControls playerControls;
        public Target target;

        public World(Game1 _game)
        {
            game = _game;
            position = new Vector2(0, 0);
            tex = game.Content.Load<Texture2D>("starBG");
            background = new Background(game);
            ship = new Ship(game, new Vector2(game.GraphicsDevice.Viewport.Width / 2, game.GraphicsDevice.Viewport.Height / 2), 1, 0f);
            playerControls = new PlayerControls(game, ship);
            target = new Target(game);
        }
        public void Update(GameTime gameTime)
        {//all gameobjects here
            playerControls.Update();
            background.Update();
            ship.Update();
            target.Update();
        }
        public void Draw(SpriteBatch batch)
        {//all gameobjects here
            background.Draw(batch);
            ship.Draw(batch);
            target.Draw(batch);
        }
    }
}