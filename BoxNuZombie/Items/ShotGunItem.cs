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
    class ShotGunItem
    {
        public Vector2 Position;
        public bool Active;
        Texture2D ShotGunTexture;
        SpriteFont Font;

        public ShotGunItem(ContentManager Content, Vector2 position)
        {
            ShotGunTexture = Content.Load<Texture2D>("ShotGun");
            this.Position = position;
            Active = true;
            Font = Content.Load<SpriteFont>("Font");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, "Shotgun", new Vector2(Position.X, Position.Y - 20), Color.White);
            spriteBatch.Draw(ShotGunTexture, Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

        public int Width
        {
            get { return ShotGunTexture.Width; }
        }
        public int Height
        {
            get { return ShotGunTexture.Height; }
        }



    }
}
