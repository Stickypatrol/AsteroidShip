﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AsteroidShip
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public ObjectController objectController;
        public InputController inputController;
        public LevelController levelController;
        public PlayerControls playerControls;
        //public SoundController soundController;
        public World world;
        Texture2D cursorTex;
        Vector2 cursorPos;
        MouseState mouseState;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = false;
        }
        protected override void Initialize()
        {
            this.IsMouseVisible = true;
            base.Initialize();
        }
        protected override void LoadContent()
        {
            cursorTex = Content.Load<Texture2D>("cursortex");
            //initiate controllers and the world
            objectController = new ObjectController(this);
            inputController = new InputController(this);
            playerControls = new PlayerControls(this);
            levelController = new LevelController(this);
            //soundController = new SoundController(this);
            world = new World(this);
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }
        protected override void Update(GameTime gameTime)
        {
            playerControls.Update();
            mouseState = Mouse.GetState();
            cursorPos = new Vector2(mouseState.X, mouseState.Y);
            objectController.Update();
            inputController.Update();
            levelController.Update(gameTime);
            world.Update(gameTime);
            base.Update(gameTime);
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
                return;
            };
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            world.Draw(spriteBatch);
            objectController.Draw(spriteBatch);
            spriteBatch.Draw(cursorTex, new Vector2(cursorPos.X - cursorTex.Width/2, cursorPos.Y - cursorTex.Height/2), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
