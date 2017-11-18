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
    class Zombie
    {
        Texture2D body;
        public Vector2 position;
        public Vector2 velocity;

        Animation zombies;

        public string movement;
        //float vx,vy;
        public float Angle = 1.57f;

        public bool Active = true;

        public Zombie(int framewidth, int frameheight, float TimeChangeFrame, int x, int y)
        {
            zombies = new Animation(framewidth, frameheight, true, TimeChangeFrame, x, y);
        }

        public void Loadcontent(ContentManager content, string name, Vector2 pos)
        {
            body = content.Load<Texture2D>(name);
            zombies.Loadcontent(body);
            position = pos;
        }

        Vector2 RandomPositionZombie()
        {
            Random random = new Random();
            Vector2 newPosition;
            int Rand = random.Next(1, 3);
            if (Rand == 1)
            {
                newPosition = new Vector2(200, 200);
            }
            else
            {
                newPosition = new Vector2(600, 600);
            }
            return newPosition;

            //if (currentLevel == 1 && Rand == 1)
            //{
            //    return new Vector2(900, 1000);
            //}
            //else if (currentLevel == 1 && Rand == 2)
            //{
            //    return new Vector2(900, 200);
            //}

            //if (currentLevel == 2 && Rand == 1)
            //{
            //    return new Vector2(900, 1000);
            //}
            //if (currentLevel == 2 && Rand == 2)
            //{
            //    return new Vector2(900, 200);
            //}

            //if (currentLevel == 3 && Rand == 1)
            //{
            //    return new Vector2(100, 790);
            //}
            //if (currentLevel == 3 && Rand == 2)
            //{
            //    return new Vector2(1800, 790);
            //}
            //else
            //{
            //    return Vector2.Zero;
            //}
        }

        public void Update(GameTime gametime)
        {
            position += velocity;
            zombies.Active = true;
            Move();
            zombies.Update(gametime, position);
        }

        public void Draw(SpriteBatch spritebatch)
        {
            zombies.Draw(spritebatch);
        }

        public int Width
        {
            get { return zombies.framewidth; }
        }

        public int Height
        {
            get { return zombies.frameheight; }
        }

        void Move()
        {
            if (velocity.X >= 0 && velocity.Y >= 0)
            {
                if (velocity.Y > velocity.X)
                {
                    zombies.frameY = 0;
                    movement = "Front";
                }
                else
                {
                    zombies.frameY = 2;
                    movement = "Right";
                }
            }

            if (velocity.X >= 0 && velocity.Y < 0)
            {
                if (-velocity.Y > velocity.X)
                {
                    zombies.frameY = 3;
                    movement = "Back";
                }
                else
                {
                    zombies.frameY = 2;
                    movement = "Right";
                }
            }

            if (velocity.X < 0 && velocity.Y >= 0)
            {
                if (velocity.Y > -velocity.X)
                {
                    zombies.frameY = 0;
                    movement = "Front";
                }
                else
                {
                    zombies.frameY = 1;
                    movement = "Left";
                }
            }

            if (velocity.X < 0 && velocity.Y < 0)
            {
                if (-velocity.Y > -velocity.X)
                {
                    zombies.frameY = 3;
                    movement = "Back";
                }
                else
                {
                    zombies.frameY = 1;
                    movement = "Left";
                }
            }
        }

    }
}
