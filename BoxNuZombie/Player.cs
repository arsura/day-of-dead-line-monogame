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
    class Player
    {
        Texture2D body;
        public Vector2 position;
        public AnimationFromBoss animation;

        Animation players;

        string mov = "Front";
        public int walk = 2;
        public int health = 10;
        public bool Active = true;      

        
        public Player(int framewidth, int frameheight,float TimeChangeFrame)
        {
            players = new Animation(framewidth, frameheight, false, TimeChangeFrame, 4, 4);
        }

        public void Loadcontent(ContentManager content, string name)
        {
            body = content.Load<Texture2D>(name);
            animation = new AnimationFromBoss();
            animation.LoadContent(content, "effect1");
            players.Loadcontent(body);
        }

        public void Update(GameTime gametime)
        {            
            players.Update(gametime,position);

            if (animation.Active)
            {
                animation.Update(gametime, position);
            }

            position.X = MathHelper.Clamp(position.X, players.framewidth, 1920 - players.framewidth);
            position.Y = MathHelper.Clamp(position.Y, players.frameheight, 1080 - players.frameheight);

        }

        public string Movement
        {
            set
            {
                if (value == "Left")
                {
                    players.frameY = 1;
                    players.Active = true;
                    position.X -= walk;
                    mov = value;
                }
                if (value == "Right")
                {
                    players.frameY = 2;
                    players.Active = true;
                    position.X += walk;
                    mov = value;
                }
                if (value == "Back")
                {
                    players.frameY = 3;
                    players.Active = true;
                    position.Y -= walk;
                    mov = value;
                }
                if (value == "Front")
                {
                    players.frameY = 0;
                    players.Active = true;
                    position.Y += walk;
                    mov = value;
                }

            }
            get { return mov; }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            players.Draw(spritebatch);
            if (animation.Active)
            {
                animation.Draw(spritebatch);
            }
        }

        public int Width
        {
            get { return players.framewidth; }
        }

        public int Height
        {
            get { return players.frameheight; }
        }
    }
}
