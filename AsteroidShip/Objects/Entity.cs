using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsteroidShip
{
    public class Entity
    {
        public Game1 game;
        public Texture2D tex;
        public Vector2 position, speed, origin;
        public Rectangle rect, sourceRect;
        public float rotation, scale, multiplier;
        public int time, health;

        public Entity() { }
        public virtual void Update()
        {
        }
        public virtual void Draw(SpriteBatch batch) 
        {
            batch.Draw(tex, position, sourceRect, Color.White, rotation, origin, scale, SpriteEffects.None, 0f);
        }
    }
}