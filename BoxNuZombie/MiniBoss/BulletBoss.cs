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
    class BulletBoss
    {
        Texture2D bullet;

        public Vector2 position;
        Vector2 velocity;

        Animation bulletBoss;

        public bool Active;

        public BulletBoss()
        {

        }

        public void LoadContent(ContentManager content, Vector2 velocity, string name)
        {
            bullet = content.Load<Texture2D>(name);
            bulletBoss = new Animation(50, 60, true, 200, 3, 4);
            bulletBoss.Loadcontent(bullet);
            this.velocity = velocity;
            Active = true;
        }



        public void Update(GameTime gameTime)
        {
            position += velocity * 5;
            bulletBoss.Active = true;
            Move();
            bulletBoss.Update(gameTime, position);
        }

        void Move()
        {
            if (velocity.X >= 0 && velocity.Y >= 0)
            {
                if (velocity.Y > velocity.X)
                {
                    bulletBoss.frameY = 0;
                }
                else
                {
                    bulletBoss.frameY = 2;
                }
            }

            if (velocity.X >= 0 && velocity.Y < 0)
            {
                if (-velocity.Y > velocity.X)
                {
                    bulletBoss.frameY = 3;
                }
                else
                {
                    bulletBoss.frameY = 2;
                }
            }

            if (velocity.X < 0 && velocity.Y >= 0)
            {
                if (velocity.Y > -velocity.X)
                {
                    bulletBoss.frameY = 0;
                }
                else
                {
                    bulletBoss.frameY = 1;
                }
            }

            if (velocity.X < 0 && velocity.Y < 0)
            {
                if (-velocity.Y > -velocity.X)
                {
                    bulletBoss.frameY = 3;
                }
                else
                {
                    bulletBoss.frameY = 1;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            bulletBoss.Draw(spriteBatch);
        }

    }
}
