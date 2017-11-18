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
    class AnimationFromBoss
    {
        Animation Skill;

        Vector2 position;

        public bool Active;

        float elaspe;
        float ChangeSkill;

        public void LoadContent(ContentManager content, string nameOfskill)
        {
            Skill = new Animation(200, 200, true, 150, 5, 5);
            Skill.Loadcontent(content.Load<Texture2D>(nameOfskill));

        }

        public void Update(GameTime gameTime, Vector2 position)
        {
            Skill.Active = true;
            Skill.Update(gameTime, position);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Skill.Draw(spriteBatch);
        }

    }
}
