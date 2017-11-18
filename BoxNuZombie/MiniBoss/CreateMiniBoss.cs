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
    class CreateMiniBoss
    {
        ContentManager Content;

        Texture2D body;

        public Vector2 position;
        public Vector2 velocity;

        Animation miniBoss;

        public List<BulletBoss> bulletBoss;

        float elaspeMiniBossShoot = 0;
        float countTimeShootMini = 5000;

        string nameskill;
        public string movement;

        public float Angle = 1.57f;

        public bool Active = true;

        public CreateMiniBoss(int framewidth, int frameheight, float TimeChangeFrame, int x, int y)
        {
            miniBoss = new Animation(framewidth, frameheight, true, TimeChangeFrame, x, y);
        }

        public void Loadcontent(ContentManager content, string name, string nameskill)
        {
            this.nameskill = nameskill;
            bulletBoss = new List<BulletBoss>();
            body = content.Load<Texture2D>(name);
            miniBoss.Loadcontent(body);
            position = new Vector2(50, 50);
            this.Content = content;
        }



        void Move()
        {
            if (velocity.X >= 0 && velocity.Y >= 0)
            {
                if (velocity.Y > velocity.X)
                {
                    miniBoss.frameY = 0;
                    movement = "Front";
                }
                else
                {
                    miniBoss.frameY = 2;
                    movement = "Right";
                }
            }

            if (velocity.X >= 0 && velocity.Y < 0)
            {
                if (-velocity.Y > velocity.X)
                {
                    miniBoss.frameY = 3;
                    movement = "Back";
                }
                else
                {
                    miniBoss.frameY = 2;
                    movement = "Right";
                }
            }

            if (velocity.X < 0 && velocity.Y >= 0)
            {
                if (velocity.Y > -velocity.X)
                {
                    miniBoss.frameY = 0;
                    movement = "Front";
                }
                else
                {
                    miniBoss.frameY = 1;
                    movement = "Left";
                }
            }

            if (velocity.X < 0 && velocity.Y < 0)
            {
                if (-velocity.Y > -velocity.X)
                {
                    miniBoss.frameY = 3;
                    movement = "Back";
                }
                else
                {
                    miniBoss.frameY = 1;
                    movement = "Left";
                }
            }
        }

        public void Update(GameTime gametime)
        {
            position += velocity;
            miniBoss.Active = true;
            Move();
            miniBoss.Update(gametime, position);

            elaspeMiniBossShoot += (float)gametime.ElapsedGameTime.TotalMilliseconds;
            if (elaspeMiniBossShoot >= countTimeShootMini)
            {

                AddBulletBoss();
                elaspeMiniBossShoot = 0;

            }
            for (int i = 0; i < bulletBoss.Count; i++)
            {
                bulletBoss[i].Update(gametime);
            }

        }

        void AddBulletBoss()
        {
            if (bulletBoss.Count > 0)
            {
                bulletBoss.RemoveAt(0);
            }

            BulletBoss b = new BulletBoss();
            b.LoadContent(Content, velocity, nameskill);
            b.position = position;
            bulletBoss.Add(b);
        }

        public void CalculateAngle(Player player1, Player player2, List<CreateMiniBoss> createMiniBoss)
        {
            Vector2 direction;
            float destance;
            for (int i = 0; i < createMiniBoss.Count; i++)
            {
                createMiniBoss[i].velocity = Vector2.Zero;

                if (CalculateDistance(player1.position, createMiniBoss[i].position) < CalculateDistance(player2.position, createMiniBoss[i].position))
                {
                    destance = CalculateDistance(player1.position, createMiniBoss[i].position);
                    direction = player1.position - createMiniBoss[i].position;
                }
                else
                {
                    destance = CalculateDistance(player2.position, createMiniBoss[i].position);
                    direction = player2.position - createMiniBoss[i].position;
                }

                if (direction != Vector2.Zero)
                {
                    direction.Normalize();
                }

                if (destance < 1)
                {
                    createMiniBoss[i].velocity += direction * destance;
                }
                else
                {
                    createMiniBoss[i].velocity += direction * 0.5f;
                }

            }
        }

        float CalculateDistance(Vector2 P1, Vector2 P2)
        {
            P1 = new Vector2(Math.Abs(P1.X), Math.Abs(P1.Y));
            P2 = new Vector2(Math.Abs(P2.X), Math.Abs(P2.Y));
            float X_deff, Y_deff, distance;
            X_deff = P1.X - P2.X;
            Y_deff = P1.Y - P2.Y;
            distance = (float)Math.Abs(Math.Pow(X_deff, 2) + Math.Pow(Y_deff, 2));
            return distance;
        }


        public void Draw(SpriteBatch spritebatch)
        {
            for (int i = bulletBoss.Count - 1; i >= 0; i--)
            {
                bulletBoss[i].Draw(spritebatch);
                if (!bulletBoss[i].Active)
                {
                    bulletBoss.RemoveAt(i);
                }
            }
            miniBoss.Draw(spritebatch);
        }

        public int Width
        {
            get { return miniBoss.framewidth; }
        }

        public int Height
        {
            get { return miniBoss.frameheight; }
        }
    }
}
