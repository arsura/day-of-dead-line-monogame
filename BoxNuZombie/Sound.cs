using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace BoxNuZombie
{
    class Sound
    {
        public SoundEffect pickUpGunSound;
        public SoundEffect shotGunSound;
        public SoundEffect restoreHealthSound;
        public SoundEffect pistolSound;
        public SoundEffect explosionSound;
        public SoundEffect punchSound;
        public SoundEffect zombieSound;
        public SoundEffect levelUpSound;

        public void LoadContent(ContentManager Content)
        {
            pickUpGunSound = Content.Load<SoundEffect>("GunPickUp");
            shotGunSound = Content.Load<SoundEffect>("ShotgunSound");
            pistolSound = Content.Load<SoundEffect>("Pistol");
            restoreHealthSound = Content.Load<SoundEffect>("RestoringHealth");
            explosionSound = Content.Load<SoundEffect>("ExplosionSound");
            punchSound = Content.Load<SoundEffect>("Punches");
            zombieSound = Content.Load<SoundEffect>("ZombieSound");
            levelUpSound = Content.Load<SoundEffect>("LevelUpSound");
        }
    }


}
