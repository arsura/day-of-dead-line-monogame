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
    class Level
    {
        public List<Zombie> zombie;
        public List<HealthBar> healthBarZombie;
        float elaspedZombie;
        Sound sound = new Sound();
        float countTimeZB;
        float LinearZombieSpawnTime;

        List<string> NameOfZombies;

        public bool readyChange = false;

        public bool Level2Active = false;
        public bool Level3Active = false;
        public bool Level4Active = false;
        public bool Level5Active = false;

        // Zombie TimeSound // -----------------------------------------------
        TimeSpan zombieTimeSound;
        TimeSpan previousZombieTimeSound;
        // Zombie TimeSound // -----------------------------------------------

        Random random;

        // Zombie // ---------------------------------------------------------
        public void ZombieInitialize(ContentManager Content)
        {
            zombie = new List<Zombie>();
            NameOfZombies = new List<string>() { "zombieKakmark", "zombieKakmark1", "zombieKakmark2" };
            healthBarZombie = new List<HealthBar>();
            sound.LoadContent(Content);
            zombieTimeSound = TimeSpan.FromSeconds(5.67f);
            previousZombieTimeSound = TimeSpan.Zero;
            countTimeZB = 4000;
            LinearZombieSpawnTime = 0;
            random = new Random();
        }

        public void ZombieUpdate(ContentManager Content, GameTime gameTime, int StopSpawn)
        {
            TimeOfZombie(Content, gameTime, StopSpawn);
            for (int i = 0; i < zombie.Count; i++)
            {
                zombie[i].Update(gameTime);
                healthBarZombie[i].Update(zombie[i].position);
            }

            if (zombie.Count > 0)
            {
                if (gameTime.TotalGameTime - previousZombieTimeSound > zombieTimeSound)
                {
                    previousZombieTimeSound = gameTime.TotalGameTime;
                    sound.zombieSound.Play();
                }
            }
        }

        void TimeOfZombie(ContentManager Content, GameTime gameTime, int StopSpawn)
        {
            elaspedZombie += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (elaspedZombie >= countTimeZB - LinearZombieSpawnTime)
            {       
                if (StopSpawn < InRoundSpawn())
                {
                    AddZombie(Content);
                }

                if (StopSpawn > InRoundSpawn() - 1)
                {
                    readyChange = true;
                }

                elaspedZombie = 0;
                LinearZombieSpawnTime += 5;
            }
        }

        void AddZombie(ContentManager Content)
        {
            Zombie z = new Zombie(40, 60, 150, 4, 4);
            Vector2 newPosition = new Vector2();
            Random random = new Random();

            if (Level2Active == false && Level3Active == false && Level4Active == false && Level5Active == false && random.Next(1, 3) == 1)
            {
                newPosition = new Vector2(900, 200);
            }
            if (Level2Active == false && Level3Active == false && Level4Active == false && Level5Active == false && random.Next(1, 3) == 2)
            {
                newPosition = new Vector2(900, 1000);
            }

            if (Level2Active == true && Level3Active == false && Level4Active == false && Level5Active == false && random.Next(1, 3) == 1)
            {
                newPosition = new Vector2(900, 200);
            }
            if (Level2Active == true && Level3Active == false && Level4Active == false && Level5Active == false && random.Next(1, 3) == 2)
            {
                newPosition = new Vector2(900, 1000);
            }

            if (Level2Active == true && Level3Active == true && Level4Active == false && Level5Active == false && random.Next(1, 3) == 1)
            {
                newPosition = new Vector2(120, 760);
            }
            if (Level2Active == true && Level3Active == true && Level4Active == false && Level5Active == false && random.Next(1, 3) == 2)
            {
                newPosition = new Vector2(1700, 760);
            }

            if (Level2Active == true && Level3Active == true && Level4Active == true && Level5Active == false && random.Next(1, 3) == 1)
            {
                newPosition = new Vector2(120, 760);
            }
            if (Level2Active == true && Level3Active == true && Level4Active == true && Level5Active == false && random.Next(1, 3) == 2)
            {
                newPosition = new Vector2(1700, 760);
            }

            if (Level2Active == true && Level3Active == true && Level4Active == true && Level5Active == true && random.Next(1, 3) == 1)
            {
                newPosition = new Vector2(-72, 800);
            }
            if (Level2Active == true && Level3Active == true && Level4Active == true && Level5Active == true && random.Next(1, 3) == 2)
            {
                newPosition = new Vector2(1900, 800);
            }

            z.Loadcontent(Content, NameOfZombies[random.Next(0, NameOfZombies.Count)], newPosition);
            zombie.Add(z);

            HealthBar healthBarZombies = new HealthBar();
            healthBarZombies.LoadContentForZombie(Content);
            healthBarZombie.Add(healthBarZombies);
        }

        public void CalculateAngle(Player player1, Player player2)
        {
            Vector2 direction;
            float destance;
            for (int i = 0; i < zombie.Count; i++)
            {
                zombie[i].velocity = Vector2.Zero;

                if (CalculateDistance(player1.position, zombie[i].position) < CalculateDistance(player2.position, zombie[i].position))
                {
                    destance = CalculateDistance(player1.position, zombie[i].position);
                    direction = player1.position - zombie[i].position;
                }
                else
                {
                    destance = CalculateDistance(player2.position, zombie[i].position);
                    direction = player2.position - zombie[i].position;
                }


                if (direction != Vector2.Zero)
                {
                    direction.Normalize();
                }

                if (destance < 1)
                {
                    zombie[i].velocity += direction * destance;
                }
                else
                {
                    zombie[i].velocity += direction * 0.5f;
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

        int InRoundSpawn()
        {
            if (Level5Active)
            {
                return 20;
            }
            if (Level4Active)
            {
                return 15;
            }
            if (Level3Active)
            {
                return 10;
            }
            if (Level2Active)
            {
                return 8;
            }
            else
            {
                return 5;
            }
        }

        // Zombie // ---------------------------------------------------------





        SpriteFont Font;
        public void LevelShowMessage(ContentManager Content, SpriteBatch spriteBatch, Player playerObject, int currentLevel)
        {
            Font = Content.Load<SpriteFont>("LevelFont");
            spriteBatch.DrawString(Font, "Level " + currentLevel, new Vector2(playerObject.position.X - 250, playerObject.position.Y - 150), Color.White);
        }

    }
}
