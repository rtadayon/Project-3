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
using BEPUphysics;
using BEPUphysics.Collidables;
using BEPUphysics.CollisionShapes;
using BEPUphysics.DataStructures;
using BEPUphysics.Entities;
using BEPUphysics.Entities.Prefabs;

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
        static Vector3 cam2pos = new Vector3(0, 60, -160);

        static Matrix view, proj;
        static Matrix view2, proj2;
        static Matrix activeview, activeproj;
        float camrot = MathHelper.ToRadians(90.0f);
        float camrot2 = MathHelper.ToRadians(90.0f);
        Viewport leftview;
        Viewport rightview;
        Viewport totalview;
        Box ground;
        Sphere cannon;
        Cannonball ball;
        List<Cannonball> balls;

        Space spc;

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
            balls = new List<Cannonball>();
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            view = Matrix.CreateLookAt(campos, lookat, Vector3.Up);
            proj = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (1.0f / 2.0f), 1.0f, 10000.0f);
            view2 = Matrix.CreateLookAt(cam2pos, lookat, Vector3.Up);
            proj2 = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (1.0f / 2.0f), 1.0f, 10000.0f);
            player.LoadContent(Content);
            ball = new Cannonball(new Vector3(0, 700, 0), Content.Load<Model>("cannonBall"), -90, 0, 40);
            myterrain = Content.Load<Model>("terrain");
            totalview = GraphicsDevice.Viewport;
            leftview = totalview;
            rightview = totalview;
            leftview.Width = leftview.Width / 2;
            rightview.Width = rightview.Width / 2;
            rightview.X = leftview.Width;

            spc = new Space();
            ground = new Box(Vector3.Zero, 30, 1, 30);
            cannon = new Sphere(new Vector3(0, 70, 0), 30, 10); 
            spc.Add(ground);
            spc.ForceUpdater.Gravity = new Vector3(0, -9.81f, 0);
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
            foreach (Cannonball ball in balls)
            {
                ball.Update();
                
            }
            spc.Update();
            // TODO: Add your update logic here



            campos = new Vector3(-160 * (float)Math.Cos(player.rot + camrot), 60, 160 * (float)Math.Sin(player.rot + camrot));
            view = Matrix.CreateLookAt(campos, lookat, Vector3.Up);
            view2 = Matrix.CreateLookAt(cam2pos, lookat, Vector3.Up);
            base.Update(gameTime);

            KeyboardState k = Keyboard.GetState();
            if(k.IsKeyDown(Keys.Space))
                balls.Add(new Cannonball(new Vector3(0, 700, -100), Content.Load<Model>("cannonBall"), -MathHelper.ToDegrees(player.rot) - 90, MathHelper.ToDegrees(player.toprot), 40));
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Viewport = totalview;
            GraphicsDevice.Clear(Color.CornflowerBlue);

            GraphicsDevice.Viewport = leftview;
            activeview = view;
            activeproj = proj;
            player.Draw();
            foreach (Cannonball ball in balls)
                ball.Draw();
            
            drawmodel(myterrain, Vector3.Zero, 0, 0, 1);

            GraphicsDevice.Viewport = rightview;
            activeview = view2;
            activeproj = proj2;
            player.Draw();
            foreach (Cannonball ball in balls)
                ball.Draw();
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
                    effect.Projection = activeproj;
                    effect.View = activeview;
                }

                mesh.Draw();
            }

        }
    }
}
