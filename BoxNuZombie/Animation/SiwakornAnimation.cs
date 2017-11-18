using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace BoxNuZombie
{
    class SiwakornAnimation
    {
        Texture2D spriteStrip;
        float scale;
        int elapseTime;
        int frameTime;
        int frameCount;
        int currentFrame;
        Color color;

        Rectangle sourceRect = new Rectangle();
        Rectangle destinationRect = new Rectangle();

        public int FrameWidth;
        public int FrameHeight;

        public bool Active;
        public bool Looping;

        public Vector2 Position;

        public SiwakornAnimation(Texture2D texture, Vector2 pos, int frameWidth, int frameHeight, int frameCount, int frameTime, Color color, float scale, bool looping)
        {
            this.color = color;
            this.FrameWidth = frameWidth;
            this.FrameHeight = frameHeight;
            this.frameCount = frameCount;
            this.frameTime = frameTime;
            this.scale = scale;
            this.Looping = looping;
            this.Position = pos;
            spriteStrip = texture;

            elapseTime = 0;
            currentFrame = 0;
            Active = true;

        }

        public void Update(GameTime gameTime)
        {
            if (!Active)
            {
                return;
            }
            elapseTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapseTime > frameTime)
            {
                // change frame
                currentFrame++;
                if (currentFrame == frameCount)
                {
                    currentFrame = 0;
                    if (!Looping)
                    {
                        Active = false;
                    }
                }
                elapseTime = 0;
            }
            sourceRect = new Rectangle(currentFrame * FrameWidth, 0, FrameWidth, FrameHeight);
            destinationRect = new Rectangle((int)Position.X - (int)(FrameWidth * scale / 2), (int)Position.Y - (int)(FrameHeight * scale / 2), (int)(FrameWidth * scale), (int)(FrameHeight * scale));

        }

        public void Draw(SpriteBatch sb)
        {
            if (Active)
            {
                sb.Draw(spriteStrip, destinationRect, sourceRect, color);
            }

        }

    }
}
