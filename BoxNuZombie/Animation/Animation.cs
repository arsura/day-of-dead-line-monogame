using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace BoxNuZombie
{
    class Animation
    {
        Texture2D body;
        
        Vector2 origin;

        Rectangle desRec;
        Rectangle sourceRec;

        public int framewidth, frameheight;
        public int frameX, frameY;
        int inFramewidth, inFrameheight;
        int framecountX, framecountY;

        float TimeChangeFrame;
        float ElaspeTime;

        bool looping;
        public bool Active;


        public Animation(int framewidth, int frameheight,
            bool looping,float TimeChangeFrame,int framecountX,int framecountY)

        {
            this.framewidth = framewidth;
            this.frameheight = frameheight;
            this.framecountX = framecountX;
            this.framecountY = framecountY;
            this.looping = looping;
            this.TimeChangeFrame = TimeChangeFrame;
        }

        public void Loadcontent(Texture2D body)
        {
            this.body = body;
            inFramewidth = body.Width / framecountX;
            inFrameheight = body.Height / framecountY;
            origin = new Vector2(inFramewidth, inFrameheight) / 2;
            frameX = 0;
            frameY = 0;
            ElaspeTime = 0;
            Active = false;
        
        }

        public void Update(GameTime gameTime, Vector2 position)
        {
            
            if (Active)
            {
                ElaspeTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (ElaspeTime >= TimeChangeFrame)
                {
                    if (frameX >= framecountX - 1)
                    {
                        if (!looping)
                        {
                            Active = false;
                        }
                        frameX = 0;
                    }
                    else
                    {
                        frameX++;
                    }
                    ElaspeTime = 0;
                }
            }
            desRec = new Rectangle((int)position.X, (int)position.Y, framewidth, frameheight);
            sourceRec = new Rectangle(frameX*inFramewidth, frameY*inFrameheight, inFramewidth, inFrameheight);

        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(body, desRec, sourceRec, Color.White, 0.0f, origin, SpriteEffects.None
                , 0.0f);
        }
    }
        
}
