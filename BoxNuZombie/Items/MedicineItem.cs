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
    class MedicineItem
    {
        public Vector2 Position;
        //float Speed;
        public bool Active;
        Texture2D MedicineTexture;
        Random random;
        SpriteFont Font;

        public MedicineItem(ContentManager Content, Vector2 position)
        {
            this.Position = position;
            MedicineTexture = Content.Load<Texture2D>("Medicine");
            Active = true;
            random = new Random();
            Font = Content.Load<SpriteFont>("Font");
        }

        public void Update()
        {
            //Position = new Vector2(random.Next(800), random.Next(600));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, "Medicine", new Vector2(Position.X - 20, Position.Y - 20), Color.White);
            spriteBatch.Draw(MedicineTexture, Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

        }


        public int Width
        {
            get { return MedicineTexture.Width; }
        }
        public int Height
        {
            get { return MedicineTexture.Height; }
        }



    }
}
