using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace baddie_class
{
    enum Screen
    {
        Title,
        House,
        End
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Screen screen; 
        Rectangle window; // the window

        MouseState mouseState, prevMouseState;
        KeyboardState keyboardState, prevKeyboardState;

        List<Texture2D> ghostTextures;
        Ghost ghost1;
        List<Ghost> ghosts;

        Texture2D titleBack, houseBack, endBack; // backgrounds
        Texture2D marioTexture;

        Random generator;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            screen = Screen.Title;

            window = new Rectangle(0, 0, 800, 500);
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.ApplyChanges();

            ghostTextures = new List<Texture2D>();
            generator = new Random();


            // TODO: Add your initialization logic here

            base.Initialize();

            //for (int i = 1; i <= 20; i++)
            //{
            //    int x, y;
            //    x = generator.Next(window.Width);
            //    y = generator.Next(window.Height);
            //    Rectangle g = new Rectangle(x, y, 40, 40);
            //    ghosts.Add(new Ghost(ghostTextures, g));
            //}
            ghost1 = (new Ghost(ghostTextures, new Rectangle(150, 200, 40, 40)));
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            marioTexture = Content.Load<Texture2D>("Images/mario");
            titleBack = Content.Load<Texture2D>("Images/haunted-title");
            houseBack = Content.Load<Texture2D>("Images/haunted-background");
            endBack = Content.Load<Texture2D>("Images/haunted-end-screen");
            ghostTextures.Add(Content.Load<Texture2D>("Images/boo-stopped"));
            for (int i = 1; i <= 8; i++)
            {
                ghostTextures.Add(Content.Load<Texture2D>($"Images/boo-move-{i}"));
            }
        }

        protected override void Update(GameTime gameTime)
        {
            prevMouseState = mouseState;
            mouseState = Mouse.GetState();

            switch (screen)
            {
                case Screen.Title:
                    if (keyboardState.IsKeyDown(Keys.Enter) && prevKeyboardState.IsKeyUp(Keys.Enter))
                        screen = Screen.House;
                    break;
                case Screen.House:
                    ghost1.Update(gameTime, mouseState);
                    if (ghost1.Contains(mouseState.Position))
                        screen = Screen.End;
                    //foreach (Ghost ghost in ghosts)
                    //{
                    //    ghost.Update(gameTime, mouseState);
                    //    if (ghost.Contains(mouseState.Position))
                    //        screen = Screen.End;
                    //}
                    break;
                case Screen.End:
                    break;
            }

            prevKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            switch (screen)
            {
                case Screen.Title:
                    _spriteBatch.Draw(titleBack, window, Color.White);
                    break;
                case Screen.House:
                    _spriteBatch.Draw(houseBack, window, Color.White);
                    ghost1.Draw(_spriteBatch);
                    //foreach (Ghost ghost in ghosts)
                    //    ghost.Draw(_spriteBatch);
                    break;
                case Screen.End:
                    _spriteBatch.Draw(endBack, window, Color.White);
                    break;
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
