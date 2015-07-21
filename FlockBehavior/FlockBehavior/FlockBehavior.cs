using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace FlockBehavior
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class FlockBehavior : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D boidTexture;
        List<Boid> boids = new List<Boid>();
        static List<string> messages = new List<string>();
        string textMessage = "";
        SpriteFont stdFont;
        Random rand = new Random();

        public FlockBehavior()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = Constants.WINDOW_HEIGHT;
            graphics.PreferredBackBufferWidth = Constants.WINDOW_WIDTH;
            this.IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            for (int i = 0; i < 25; i++ )
            {
                boids.Add(new Sparrow(boidTexture, new Vector2(rand.Next(Constants.WINDOW_WIDTH), rand.Next(Constants.WINDOW_HEIGHT)), Constants.BOID_SPEED_MIN + (float)(rand.NextDouble() * Constants.BOID_SPEED_RANGE), Constants.BOID_TURNSPEED, new Vector2(Constants.WINDOW_WIDTH, Constants.WINDOW_HEIGHT)));
            }
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            boidTexture = Content.Load<Texture2D>("boid");
            stdFont = Content.Load<SpriteFont>("StdFont");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            MouseState mouse = Mouse.GetState();
            foreach(Sparrow boid in boids)
            {
                boid.Update(gameTime, boids);
            }

            textMessage = "";
            foreach(string s in messages)
            {
                textMessage += s + "\r\n";
            }
            messages = new List<string>();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            // TODO: Add your drawing code here
            foreach (Boid boid in boids)
            {
                boid.Draw(spriteBatch);
            }

            spriteBatch.DrawString(stdFont, textMessage, new Vector2(20, 20), Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        public static void sendMessage(string message)
        {
            messages.Add(message);
        }
    }
}
