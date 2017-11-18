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
    class ShotGunBullet
    {

        public List<Bullet> shotGunBullet240R;
        public List<Bullet> shotGunBullet255R;
        public List<Bullet> shotGunBullet270R;
        public List<Bullet> shotGunBullet285R;
        public List<Bullet> shotGunBullet300R;

        TimeSpan shotGunBulletTime;
        TimeSpan previousShotGunBulletTime;
        public int shotGunDamage;
        private int shotGunMagazine;
        public bool shotGunActive;
        
        public int ShotGunMagazine
        {
            get { return shotGunMagazine; }
            set { shotGunMagazine = value; }
        }

        public void Initialized(ContentManager Content)
        {
            previousShotGunBulletTime = TimeSpan.Zero;
            shotGunBulletTime = TimeSpan.FromSeconds(0.50f);
            shotGunDamage = 100;

            shotGunBullet240R = new List<Bullet>();
            shotGunBullet255R = new List<Bullet>();
            shotGunBullet270R = new List<Bullet>();
            shotGunBullet285R = new List<Bullet>();
            shotGunBullet300R = new List<Bullet>();
            shotGunActive = false;
            shotGunMagazine = 0;


        }

        public void RotationAngle(ContentManager Content, GameTime gameTime, string movement, Player playerObject)
        {
            Bullet shotGunBullets1 = new Bullet();
            shotGunBullets1.ShotGunBulletLoadContent(Content);
            if (movement == "Back") shotGunBullets1.shotGunBulletsVelocity = new Vector2((float)Math.Cos(240 * Math.PI / 180), (float)Math.Sin(240 * Math.PI / 180)) * 10f;
            if (movement == "Front") shotGunBullets1.shotGunBulletsVelocity = new Vector2((float)Math.Cos(60 * Math.PI / 180), (float)Math.Sin(60 * Math.PI / 180)) * 10f;
            if (movement == "Right") shotGunBullets1.shotGunBulletsVelocity = new Vector2((float)Math.Cos(330 * Math.PI / 180), (float)Math.Sin(330 * Math.PI / 180)) * 10f;
            if (movement == "Left") shotGunBullets1.shotGunBulletsVelocity = new Vector2((float)Math.Cos(150 * Math.PI / 180), (float)Math.Sin(150 * Math.PI / 180)) * 10f;
            shotGunBullets1.shotGunPosition.X = (playerObject.position.X + playerObject.Width / 2) - 30;
            shotGunBullets1.shotGunPosition.Y = (playerObject.position.Y + playerObject.Height / 2) - 30;
            shotGunBullets1.shotGunBulletsActive = true;

            Bullet shotGunBullets2 = new Bullet();
            shotGunBullets2.ShotGunBulletLoadContent(Content);
            if (movement == "Back") shotGunBullets2.shotGunBulletsVelocity = new Vector2((float)Math.Cos(255 * Math.PI / 180), (float)Math.Sin(255 * Math.PI / 180)) * 10f;
            if (movement == "Front") shotGunBullets2.shotGunBulletsVelocity = new Vector2((float)Math.Cos(75 * Math.PI / 180), (float)Math.Sin(75 * Math.PI / 180)) * 10f;
            if (movement == "Right") shotGunBullets2.shotGunBulletsVelocity = new Vector2((float)Math.Cos(315 * Math.PI / 180), (float)Math.Sin(315 * Math.PI / 180)) * 10f;
            if (movement == "Left") shotGunBullets2.shotGunBulletsVelocity = new Vector2((float)Math.Cos(165 * Math.PI / 180), (float)Math.Sin(165 * Math.PI / 180)) * 10f;
            shotGunBullets2.shotGunPosition.X = (playerObject.position.X + playerObject.Width / 2) - 30;
            shotGunBullets2.shotGunPosition.Y = (playerObject.position.Y + playerObject.Height / 2) - 30;
            shotGunBullets2.shotGunBulletsActive = true;

            Bullet shotGunBullets3 = new Bullet();
            shotGunBullets3.ShotGunBulletLoadContent(Content);
            if (movement == "Back") shotGunBullets3.shotGunBulletsVelocity = new Vector2((float)Math.Cos(270 * Math.PI / 180), (float)Math.Sin(270 * Math.PI / 180)) * 10f;
            if (movement == "Front") shotGunBullets3.shotGunBulletsVelocity = new Vector2((float)Math.Cos(90 * Math.PI / 180), (float)Math.Sin(90 * Math.PI / 180)) * 10f;
            if (movement == "Right") shotGunBullets3.shotGunBulletsVelocity = new Vector2((float)Math.Cos(0 * Math.PI / 180), (float)Math.Sin(0 * Math.PI / 180)) * 10f;
            if (movement == "Left") shotGunBullets3.shotGunBulletsVelocity = new Vector2((float)Math.Cos(180 * Math.PI / 180), (float)Math.Sin(180 * Math.PI / 180)) * 10f;
            shotGunBullets3.shotGunPosition.X = (playerObject.position.X + playerObject.Width / 2) - 30;
            shotGunBullets3.shotGunPosition.Y = (playerObject.position.Y + playerObject.Height / 2) - 30;
            shotGunBullets3.shotGunBulletsActive = true;

            Bullet shotGunBullets4 = new Bullet();
            shotGunBullets4.ShotGunBulletLoadContent(Content);
            if (movement == "Back") shotGunBullets4.shotGunBulletsVelocity = new Vector2((float)Math.Cos(285 * Math.PI / 180), (float)Math.Sin(285 * Math.PI / 180)) * 10f;
            if (movement == "Front") shotGunBullets4.shotGunBulletsVelocity = new Vector2((float)Math.Cos(105 * Math.PI / 180), (float)Math.Sin(105 * Math.PI / 180)) * 10f;
            if (movement == "Right") shotGunBullets4.shotGunBulletsVelocity = new Vector2((float)Math.Cos(15 * Math.PI / 180), (float)Math.Sin(15 * Math.PI / 180)) * 10f;
            if (movement == "Left") shotGunBullets4.shotGunBulletsVelocity = new Vector2((float)Math.Cos(195 * Math.PI / 180), (float)Math.Sin(195 * Math.PI / 180)) * 10f;
            shotGunBullets4.shotGunPosition.X = (playerObject.position.X + playerObject.Width / 2) - 30;
            shotGunBullets4.shotGunPosition.Y = (playerObject.position.Y + playerObject.Height / 2) - 30;
            shotGunBullets4.shotGunBulletsActive = true;

            Bullet shotGunBullets5 = new Bullet();
            shotGunBullets5.ShotGunBulletLoadContent(Content);
            if (movement == "Back") shotGunBullets5.shotGunBulletsVelocity = new Vector2((float)Math.Cos(300 * Math.PI / 180), (float)Math.Sin(300 * Math.PI / 180)) * 10f;
            if (movement == "Front") shotGunBullets5.shotGunBulletsVelocity = new Vector2((float)Math.Cos(120 * Math.PI / 180), (float)Math.Sin(120 * Math.PI / 180)) * 10f;
            if (movement == "Right") shotGunBullets5.shotGunBulletsVelocity = new Vector2((float)Math.Cos(30 * Math.PI / 180), (float)Math.Sin(30 * Math.PI / 180)) * 10f;
            if (movement == "Left") shotGunBullets5.shotGunBulletsVelocity = new Vector2((float)Math.Cos(210 * Math.PI / 180), (float)Math.Sin(210 * Math.PI / 180)) * 10f;
            shotGunBullets5.shotGunPosition.X = (playerObject.position.X + playerObject.Width / 2) - 30;
            shotGunBullets5.shotGunPosition.Y = (playerObject.position.Y + playerObject.Height / 2) - 30;
            shotGunBullets5.shotGunBulletsActive = true;

            if (gameTime.TotalGameTime - previousShotGunBulletTime > shotGunBulletTime)
            {
                previousShotGunBulletTime = gameTime.TotalGameTime;
                shotGunBullet240R.Add(shotGunBullets1);
                shotGunBullet255R.Add(shotGunBullets2);
                shotGunBullet270R.Add(shotGunBullets3);
                shotGunBullet285R.Add(shotGunBullets4);
                shotGunBullet300R.Add(shotGunBullets5);
            }
        }

        public void Update(ContentManager Content, GameTime gameTime, string movement, Player playerObject)
        {
              
            foreach (Bullet shotGun in shotGunBullet240R)
            {
                shotGun.shotGunPosition += shotGun.shotGunBulletsVelocity;
                if ((movement == "Front" && shotGun.shotGunPosition.Y > playerObject.position.Y + 150) ||
                    (movement == "Back" && shotGun.shotGunPosition.Y < playerObject.position.Y - 150) ||
                    (movement == "Left" && shotGun.shotGunPosition.X < playerObject.position.X - 150) ||
                    (movement == "Right" && shotGun.shotGunPosition.X > playerObject.position.X + 150))
                {
                    shotGun.shotGunBulletsActive = false;
                }
            }           

            foreach (Bullet shotGun2 in shotGunBullet255R)
            {
                shotGun2.shotGunPosition += shotGun2.shotGunBulletsVelocity;
                if ((movement == "Front" && shotGun2.shotGunPosition.Y > playerObject.position.Y + 150) ||
                    (movement == "Back" && shotGun2.shotGunPosition.Y < playerObject.position.Y - 150) ||
                    (movement == "Left" && shotGun2.shotGunPosition.X < playerObject.position.X - 150) ||
                    (movement == "Right" && shotGun2.shotGunPosition.X > playerObject.position.X + 150))
                {
                    shotGun2.shotGunBulletsActive = false;
                }
            }

            foreach (Bullet shotGun3 in shotGunBullet270R)
            {
                shotGun3.shotGunPosition += shotGun3.shotGunBulletsVelocity;
                if ((movement == "Front" && shotGun3.shotGunPosition.Y > playerObject.position.Y + 150) ||
                    (movement == "Back" && shotGun3.shotGunPosition.Y < playerObject.position.Y - 150) ||
                    (movement == "Left" && shotGun3.shotGunPosition.X < playerObject.position.X - 150) ||
                    (movement == "Right" && shotGun3.shotGunPosition.X > playerObject.position.X + 150))
                {
                    shotGun3.shotGunBulletsActive = false;
                }
            }

            foreach (Bullet shotGun4 in shotGunBullet285R)
            {
                shotGun4.shotGunPosition += shotGun4.shotGunBulletsVelocity;
                if ((movement == "Front" && shotGun4.shotGunPosition.Y > playerObject.position.Y + 150) ||
                    (movement == "Back" && shotGun4.shotGunPosition.Y < playerObject.position.Y - 150) ||
                    (movement == "Left" && shotGun4.shotGunPosition.X < playerObject.position.X - 150) ||
                    (movement == "Right" && shotGun4.shotGunPosition.X > playerObject.position.X + 150))
                {
                    shotGun4.shotGunBulletsActive = false;
                }
            }

            foreach (Bullet shotGun5 in shotGunBullet300R)
            {
                shotGun5.shotGunPosition += shotGun5.shotGunBulletsVelocity;
                if ((movement == "Front" && shotGun5.shotGunPosition.Y > playerObject.position.Y + 150) ||
                    (movement == "Back" && shotGun5.shotGunPosition.Y < playerObject.position.Y - 150) ||
                    (movement == "Left" && shotGun5.shotGunPosition.X < playerObject.position.X - 150) ||
                    (movement == "Right" && shotGun5.shotGunPosition.X > playerObject.position.X + 150))
                {
                    shotGun5.shotGunBulletsActive = false;
                }
            }

            for (int i = 0; i < shotGunBullet240R.Count; ++i)
            {
                if (!shotGunBullet240R[i].shotGunBulletsActive)
                {
                    shotGunBullet240R.RemoveAt(i);                   
                    --i;
                }
            }

            for (int i = 0; i < shotGunBullet255R.Count; ++i)
            {
                if (!shotGunBullet255R[i].shotGunBulletsActive)
                {
                    shotGunBullet255R.RemoveAt(i);
                    --i;
                }
            }

            for (int i = 0; i < shotGunBullet270R.Count; ++i)
            {
                if (!shotGunBullet270R[i].shotGunBulletsActive)
                {
                    shotGunBullet270R.RemoveAt(i);
                    --i;
                }
            }

            for (int i = 0; i < shotGunBullet285R.Count; ++i)
            {
                if (!shotGunBullet285R[i].shotGunBulletsActive)
                {
                    shotGunBullet285R.RemoveAt(i);
                    --i;
                }
            }

            for (int i = 0; i < shotGunBullet300R.Count; ++i)
            {
                if (!shotGunBullet300R[i].shotGunBulletsActive)
                {
                    shotGunBullet300R.RemoveAt(i);
                    --i;
                }
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            if (shotGunActive == true)
            {
                for (int i = 0; i < shotGunBullet240R.Count; i++)
                {
                    shotGunBullet240R[i].ShotGunBulletDraw(spriteBatch);
                }
                for (int i = 0; i < shotGunBullet255R.Count; i++)
                {
                    shotGunBullet255R[i].ShotGunBulletDraw(spriteBatch);
                }
                for (int i = 0; i < shotGunBullet270R.Count; i++)
                {
                    shotGunBullet270R[i].ShotGunBulletDraw(spriteBatch);
                }
                for (int i = 0; i < shotGunBullet285R.Count; i++)
                {
                    shotGunBullet285R[i].ShotGunBulletDraw(spriteBatch);
                }
                for (int i = 0; i < shotGunBullet300R.Count; i++)
                {
                    shotGunBullet300R[i].ShotGunBulletDraw(spriteBatch);
                }
            }
            
        }


    }
}
