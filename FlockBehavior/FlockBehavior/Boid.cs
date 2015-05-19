using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FlockBehavior
{
    class Boid
    {
        private Vector2 direction;
        private float speed;
        private Texture2D texture;
        private Vector2 location;
        private float angle;
        private Rectangle sourceRectangle;
        private Vector2 origin;
        private float maxTurnSpeed;

        public Boid(Texture2D texture)
        {
            direction = new Vector2(1, 0);
            speed = 0.1f;
            angle = 0;
            location = new Vector2(Constants.WINDOW_WIDTH / 2, Constants.WINDOW_HEIGHT / 2);
            maxTurnSpeed = 0.01f;

            this.texture = texture;

            sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public void Update(GameTime gameTime, MouseState mouse)
        {
            float angleToMouse = (float)(Math.Atan2(mouse.Y - location.Y, mouse.X - location.X));

            float angleAdjusted = (float)(angle - Math.PI / 2);


            if (mouse.LeftButton == ButtonState.Pressed) angle += maxTurnSpeed;
            if (mouse.RightButton == ButtonState.Pressed) angle -= maxTurnSpeed;

            float relationAngle = MathHelper.WrapAngle((float)(angle - Math.PI / 2));



            FlockBehavior.sendMessage("angleToMouse: " + (angleToMouse - relationAngle));
            FlockBehavior.sendMessage("angle: " + angle.ToString());
            FlockBehavior.sendMessage("angleAdjusted: " + relationAngle);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, location, sourceRectangle, Color.White, angle, origin, 1.0f, SpriteEffects.None, 1);
        }
    }
}
