using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsteroidShip
{
    class Gun : Entity
    {
        Vector2 muzzlelocation;
        public Gun(Game1 _game, Vector2 _position)
        {
            game = _game;
            tex = game.Content.Load<Texture2D>("gun");
            position = _position;
            muzzlelocation = new Vector2(0, 0);
            scale = 1f;
            sourceRect = new Rectangle(0, 0, tex.Width, tex.Height);
            origin = new Vector2(tex.Width/2, tex.Height/2);
        }
        public override void Update()
        {
            //update muzzlelocation here
            rotation = (float)Math.Atan2(game.playerControls.shootdirection.Y, game.playerControls.shootdirection.X) + (float)Math.PI*0.5f;
        }
    }
}
