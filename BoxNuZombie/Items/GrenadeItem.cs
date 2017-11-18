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
    class GrenadeItem
    {
        public Vector2 Position;
        public bool Active;
        Texture2D GrenadeTexture;
        Texture2D GrenadePutTexture;
        SpriteFont Font;

        public GrenadeItem(ContentManager Content, Vector2 position)
        {
            GrenadeTexture = Content.Load<Texture2D>("Grenade");
            GrenadePutTexture = Content.Load<Texture2D>("GrenadePut");
            this.Position = position;
            Active = false;
            Font = Content.Load<SpriteFont>("Font");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, "Grenade", new Vector2(Position.X - 12, Position.Y - 20), Color.White);
            spriteBatch.Draw(GrenadeTexture, Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

        public void DrawGrenadePut(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GrenadePutTexture, Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

        public int Width
        {
            get { return GrenadeTexture.Width; }
        }
        public int Height
        {
            get { return GrenadeTexture.Height; }
        }


    }
}
