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

namespace Project3
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Model myterrain;
        Player player;
        static Vector3 campos = new Vector3(0, 60, 160);
        static Vector3 lookat = new Vector3(0, 50, 0);
        static Matrix view, proj;
        float camrot = MathHelper.ToRadians(90.0f);

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            player = new Player();
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
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            view = Matrix.CreateLookAt(campos, lookat, Vector3.Up);
            proj = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f), graphics.GraphicsDevice.Viewport.AspectRatio, 1.0f, 10000.0f);
            player.LoadContent(Content);
            myterrain = Content.Load<Model>("terrain");
            // TODO: use this.Content to load your game content here
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
            player.Update();
            // TODO: Add your update logic here



            campos = new Vector3(-160 * (float)Math.Cos(player.rot + camrot), 60, 160 * (float)Math.Sin(player.rot + camrot));
            view = Matrix.CreateLookAt(campos, lookat, Vector3.Up);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            player.Draw();
            drawmodel(myterrain, Vector3.Zero, 0, 0, 1);
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        public static void drawmodel(Model m, Vector3 pos, float rot, float otherrot, float scale)
        {
            foreach (ModelMesh mesh in m.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.PreferPerPixelLighting = true;
                    effect.World = Matrix.CreateFromYawPitchRoll(rot, otherrot, 0)
                    * Matrix.CreateTranslation(pos)
                    * Matrix.CreateScale(scale);
                    effect.Projection = proj;
                    effect.View = view;
                }

                mesh.Draw();
            }
        }
    }
}
