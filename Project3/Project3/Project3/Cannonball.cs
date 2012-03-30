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
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Cannonball
    {
        public Vector3 position;
        public Vector3 velocity;
        float gravity = -0.1f;
        float friction = 0.8f;
        float dampen = 0.4f;
        Model model;
        float scale = 0.03f;
        float groundlevel = 0;
        float theta1, theta2;
        float force;
        const float cannonlength = 100;
        float time;
        float tipx, tipy, tipz;
        Vector3 initps;


        public Cannonball(Vector3 initpos, Model mdl, float outbnd, float out2, float f)
           
        {
            position = initpos;
            initps = position;
            model = mdl;
            force = f;
            velocity = Vector3.Zero;
            theta1 = outbnd;
            theta2 = out2;
            time = 0;
            float tiltrads = theta2 / 360.0f * 2 * (float)Math.PI;
            tipy = cannonlength * (float)Math.Sin(tiltrads);
            float projection = cannonlength * (float)Math.Cos(tiltrads);
            tipx = projection * (float)Math.Cos(MathHelper.ToRadians(theta1));
            tipz = projection * (float)Math.Sin(MathHelper.ToRadians(theta1));
            float compx = tipx - 0;
            float compy = tipy - 0;
            float compz = tipz - 0;
            float directioncosinex = compx / cannonlength;
            float directioncosiney = compy / cannonlength;
            float directioncosinez = compz / cannonlength;
            velocity.X = directioncosinex * force;
            velocity.Y = directioncosiney * force;
            velocity.Z = directioncosinez * force;
           // velocity.Z = (float)Math.Cos
        }


        public void Update()
        {
           // float 
            //velocity.Y += gravity;
            position.X = initps.X + velocity.X * time;
            position.Y = initps.Y + (velocity.Y * time) + (0.5f * gravity * time * time);
            position.Z = initps.Z + velocity.Z * time;
            time += 3;
        }

        public void Draw()
        {
            Game1.drawmodel(model, position, 0, 0, scale);
        }
    }
}
