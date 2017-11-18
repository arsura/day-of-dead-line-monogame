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
    class MiniBoss
    {
        ContentManager Content;

        int NumOfMiniBoss;

        List<string> NameOfMiniBoss;

        public List<CreateMiniBoss> miniBoss;
        public List<HealthBar> healthBarMiniBoss;

        float elaspeMiniBoss;
        float countTimeMB = 3000f;
        string nameskill;

        int y1 = -200;
        int y2 = 600 + 200;
        Random random;

        public MiniBoss(int NumOfMiniBoss, string nameskill)
        {
            this.NumOfMiniBoss = NumOfMiniBoss;
            this.nameskill = nameskill;
        }

        public void LoadContent(ContentManager Content)
        {
            this.Content = Content;
            NameOfMiniBoss = new List<string>() { "MiniBoss1", "MiniBoss2" };
            miniBoss = new List<CreateMiniBoss>();
            healthBarMiniBoss = new List<HealthBar>();
            random = new Random();
        }

        public void LoadContentLastBoss(ContentManager Content)
        {
            this.Content = Content;
            NameOfMiniBoss = new List<string>() { "FinalBoss" };
            //NameOfMiniBoss = new List<string>() { "LastBossTest" };
            miniBoss = new List<CreateMiniBoss>();
            healthBarMiniBoss = new List<HealthBar>();
            random = new Random();
        }


        public void Update(GameTime gameTime)
        {
            if (NumOfMiniBoss > 0)
            {
                TimeOfZombie(gameTime);
            }
            UpdateZombies(gameTime);
        }

        void UpdateZombies(GameTime gameTime)
        {
            for (int i = 0; i < miniBoss.Count; i++)
            {
                miniBoss[i].Update(gameTime);
                healthBarMiniBoss[i].Update(miniBoss[i].position);
            }
        }

        void AddZombie()
        {
            CreateMiniBoss z = new CreateMiniBoss(40, 60, 150, 3, 4);

            z.Loadcontent(Content, NameOfMiniBoss[random.Next(0, NameOfMiniBoss.Count)], nameskill);
            int y = random.Next(1, 2);
            if (y == 1)
            {
                z.position = new Vector2(-1, 760);
            }
            else
            {
                z.position = new Vector2(1950, 760);
            }
            miniBoss.Add(z);

            HealthBar healthBarMB = new HealthBar();
            healthBarMB.LoadContentForZombie(Content);
            healthBarMiniBoss.Add(healthBarMB);
        }

        void TimeOfZombie(GameTime gameTime)
        {
            elaspeMiniBoss += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (elaspeMiniBoss >= countTimeMB)
            {
                AddZombie();
                NumOfMiniBoss--;
                elaspeMiniBoss = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = miniBoss.Count - 1; i >= 0; i--)
            {
                miniBoss[i].Draw(spriteBatch);
                healthBarMiniBoss[i].Draw(spriteBatch);
                if (miniBoss[i].Active == false)
                {
                    miniBoss.RemoveAt(i);
                    healthBarMiniBoss.RemoveAt(i);
                }
            }

        }
    }
}
