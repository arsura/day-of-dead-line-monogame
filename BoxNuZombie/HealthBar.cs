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
    class HealthBar
    {
        public int health = 100;
        int max_health = 100;
        //int width = 40;
        //int height = 20;
        bool Active;
        Texture2D BarHealth;
        Texture2D Empty;
        

        Rectangle rec1,rec2;

        Vector2 originEmpty;
        //Vector2 position;

        public void LoadContentForPlayer(ContentManager content)
        {
            BarHealth = content.Load<Texture2D>("HealthBar3");
            Empty = content.Load<Texture2D>("HealthBar4");
        }

        public void LoadContentForZombie(ContentManager content)
        {
            BarHealth = content.Load<Texture2D>("HealthBarForZombie");
            Empty = content.Load<Texture2D>("HealthBar4");
        }

        public void Update(Vector2 position)
        {
            rec1 = new Rectangle((int)position.X - BarHealth.Width / 2, (int)position.Y - 40, BarHealth.Width, BarHealth.Height);
            rec2 = new Rectangle((int)position.X - BarHealth.Width / 2, (int)position.Y - 40, BarHealth.Width - (health/max_health), BarHealth.Height);
        }

        public bool ChckPlayerDie()
        {
            if (BarHealth.Width - (health / max_health) < 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ChckZombieDie()
        {
            if (BarHealth.Width - (health / max_health) < 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            //spritebatch.Draw(Empty, rec1, Color.Red);
            //spritebatch.Draw(Empty, rec2, Color.Green);
            
            spritebatch.Draw(BarHealth, rec2, Color.White);
            spritebatch.Draw(Empty, rec1, Color.White);
        }


    }
}
