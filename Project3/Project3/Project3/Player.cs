using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Project3
{
    class Player
    {
        public Model playerbase;
        public Model cannon;
        public Vector3 pos;
        public float rot;
        public float toprot;
        public float scale;

        public Player()
        {
            pos = new Vector3(0, 0, 0);
            rot = 0;
            toprot = 0;
            scale = 0.2f;
        }

        public void LoadContent(ContentManager content)
        {
            playerbase = content.Load<Model>("launcher_base");
            cannon = content.Load<Model>("launcher_head");
        }

        public void Update()
        {
            HandleInput();
  
        }

        public void HandleInput()
        {
            KeyboardState k = Keyboard.GetState();
            if (k.IsKeyDown(Keys.D))
                rot -= 0.1f;
            else if (k.IsKeyDown(Keys.A))
                rot += 0.1f;
            else if (k.IsKeyDown(Keys.W))
                toprot += 0.1f;
            else if (k.IsKeyDown(Keys.S))
                toprot -= 0.1f;
        }

        public void Draw()
        {
            Game1.drawmodel(playerbase, pos, 0.0f, 0, scale);
            Game1.drawmodel(cannon, pos + new Vector3(0, 100, -10), rot, toprot, scale);
        }

    }

}
