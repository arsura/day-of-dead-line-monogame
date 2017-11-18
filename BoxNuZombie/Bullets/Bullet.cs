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
    class Bullet
    {
        Texture2D bullets;        
        public Vector2 pos;
        int speedbullets;
        public string movbullets;
        public bool Active = true;

        // ShotGun Bullets // ----------------------------------------
        Texture2D shotGunBulletsTexture;
        public Vector2 shotGunPosition;
        public Vector2 shotGunBulletsVelocity;
        public bool shotGunBulletsActive;
        Vector2 Origin;
        // ShotGun Bullets // ----------------------------------------


        // ShotGun Bullets Method // ----------------------------------------
        public void ShotGunBulletLoadContent(ContentManager Content)
        {
            shotGunBulletsTexture = Content.Load<Texture2D>("bullet3");
            Origin.X = shotGunBulletsTexture.Width / 2;
            Origin.Y = shotGunBulletsTexture.Height / 2;
            shotGunBulletsActive = false;

        }

        public void ShotGunBulletDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(shotGunBulletsTexture, shotGunPosition, null, Color.White, 0f, Origin, 1f, SpriteEffects.None, 0);
        }
 

        // ShotGun Bullets Method // ----------------------------------------



        public void LoadContent(ContentManager content)
        {
            bullets = content.Load<Texture2D>("bullet2");
            speedbullets = 6;
        }

        public void Update()
        {
            if (pos.Y + bullets.Height < 0 || pos.X + bullets.Width < 0 || pos.Y > 1080 || pos.X > 1920)
            {
                Active = false;
            }

            if (movbullets == "Back")
            {
                pos.Y -= speedbullets;
            }
            if (movbullets == "Front")
            {
                pos.Y += speedbullets;
            }
            if (movbullets == "Left")
            {
                pos.X -= speedbullets;
            }
            if (movbullets == "Right")
            {
                pos.X += speedbullets;
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            if (movbullets == "Back")
            {
                spritebatch.Draw(bullets, new Rectangle((int)pos.X, (int)pos.Y, 8, 4), new Rectangle(0, 0, bullets.Width, bullets.Height), Color.White, -1.5705f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0.0f);
            }
            if (movbullets == "Front")
            {
                spritebatch.Draw(bullets, new Rectangle((int)pos.X, (int)pos.Y, 8, 4),
                   new Rectangle(0, 0, bullets.Width, bullets.Height), Color.White
                   , 1.5705f , Vector2.Zero, SpriteEffects.FlipHorizontally, 0.0f);
            }
            if (movbullets == "Left")
            {
                spritebatch.Draw(bullets, new Rectangle((int)pos.X, (int)pos.Y, 8, 4), 
                    new Rectangle(0, 0, bullets.Width, bullets.Height), Color.White
                    , 0.0f, Vector2.Zero,SpriteEffects.FlipHorizontally,0.0f);

            }
            if (movbullets == "Right")
            {
                spritebatch.Draw(bullets, new Rectangle((int)pos.X, (int)pos.Y, 8,4), Color.White);
            }
        }

        public int ShotGunTextureWidth
        {
            get { return shotGunBulletsTexture.Width; }
        }
        public int ShotGunTextureHeight
        {
            get { return shotGunBulletsTexture.Height; }
        }

        public int Width
        {
            get { return bullets.Width; }
        }
        public int Height
        {
            get { return bullets.Height; }
        }
    }
}
