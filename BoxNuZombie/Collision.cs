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
    class Collision
    {
        Sound sound;
        public int ZombieDieCount = 0;
        public int MiniBossDieCount = 0;
        public int LastBossDieCount = 0;
        float deltaTime;

        public void CollisionSoundLoadContent(ContentManager Content)
        {
            sound = new Sound();
            sound.LoadContent(Content);
        }

        public bool CollisionMedicine(List<MedicineItem> MedecineObject, Player PlayerObject)
        {
            for (int i = MedecineObject.Count - 1; i >= 0; i--)
            {
                if (chckIntersects((int)PlayerObject.position.X, (int)PlayerObject.position.Y, (int)PlayerObject.Width, (int)PlayerObject.Height,
                                   (int)MedecineObject[i].Position.X, (int)MedecineObject[i].Position.Y, (int)MedecineObject[i].Width, (int)MedecineObject[i].Height))
                {
                    sound.restoreHealthSound.Play();
                    MedecineObject.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public bool CollisionShotGun(List<ShotGunItem> ShotGunObject, Player PlayerObject)
        {
            for (int i = ShotGunObject.Count - 1; i >= 0; i--)
            {
                if (chckIntersects((int)PlayerObject.position.X, (int)PlayerObject.position.Y, (int)PlayerObject.Width, (int)PlayerObject.Height,
                                   (int)ShotGunObject[i].Position.X, (int)ShotGunObject[i].Position.Y, (int)ShotGunObject[i].Width, (int)ShotGunObject[i].Height))
                {
                    sound.pickUpGunSound.Play();
                    ShotGunObject.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public bool CollisionGrenade(List<GrenadeItem> GrenadeObject, Player PlayerObject)
        {
            for (int i = GrenadeObject.Count - 1; i >= 0; i--)
            {
                if (chckIntersects((int)PlayerObject.position.X, (int)PlayerObject.position.Y, (int)PlayerObject.Width, (int)PlayerObject.Height,
                                   (int)GrenadeObject[i].Position.X, (int)GrenadeObject[i].Position.Y, (int)GrenadeObject[i].Width, (int)GrenadeObject[i].Height))
                {
                    sound.pickUpGunSound.Play();
                    GrenadeObject.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public void CheckCollisionZombiesAndPlayer(List<Zombie> zombie, HealthBar barplayer1, HealthBar barplayer2, Player player1, Player player2)
        {

            Rectangle rec1, rec2, rec3;

            for (int i = 0; i < zombie.Count; i++)
            {
                rec1 = new Rectangle((int)zombie[i].position.X - 10, (int)zombie[i].position.Y - 20, 20, 40);
                rec2 = new Rectangle((int)player1.position.X - 10, (int)player1.position.Y - 20, 20, 40);
                rec3 = new Rectangle((int)player2.position.X - 10, (int)player2.position.Y - 20, 20, 40);

                if (rec1.Intersects(rec2))
                {
                    sound.punchSound.Play();
                    barplayer1.health += 300;
                    if (player1.Movement == "Front")
                    {
                        player1.position.Y -= 70;
                    }
                    if (player1.Movement == "Back")
                    {
                        player1.position.Y += 70;
                    }
                    if (player1.Movement == "Right")
                    {
                        player1.position.X -= 70;
                    }
                    if (player1.Movement == "Left")
                    {
                        player1.position.X += 70;
                    }
                }

                if (rec1.Intersects(rec3))
                {
                    sound.punchSound.Play();
                    barplayer2.health += 300;
                    if (player2.Movement == "Front")
                    {
                        player2.position.Y -= 70;
                    }
                    if (player2.Movement == "Back")
                    {
                        player2.position.Y += 70;
                    }
                    if (player2.Movement == "Right")
                    {
                        player2.position.X -= 70;
                    }
                    if (player2.Movement == "Left")
                    {
                        player2.position.X += 70;
                    }
                }
            }

        }

        public void CheckCollisionShotGunBulletAndZombieObjects(List<Zombie> ZombieObject, ShotGunBullet shotGunBullet, Player player1, Player player2, List<HealthBar> healthBarZombieObject)
        {
            for (int i = 0; i < shotGunBullet.shotGunBullet240R.Count; ++i)
            {
                for (int j = 0; j < ZombieObject.Count; ++j)
                {
                    if (chckIntersects((int)shotGunBullet.shotGunBullet240R[i].shotGunPosition.X, (int)shotGunBullet.shotGunBullet240R[i].shotGunPosition.Y,
                                       (int)shotGunBullet.shotGunBullet240R[i].ShotGunTextureWidth, (int)shotGunBullet.shotGunBullet240R[i].ShotGunTextureHeight,
                                       (int)ZombieObject[j].position.X, (int)ZombieObject[j].position.Y, (int)ZombieObject[j].Width / 2, (int)ZombieObject[j].Height / 2))
                    {
                        sound.punchSound.Play();
                        healthBarZombieObject[j].health += shotGunBullet.shotGunDamage;

                        if (player1.Movement == "Left" && player1.position.X > ZombieObject[j].position.X &&
                           (player1.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player1.position.Y + player1.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X -= 6;
                        }
                        else if (player1.Movement == "Right" && player1.position.X < ZombieObject[j].position.X &&
                                (player1.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player1.position.Y + player1.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X += 6;
                        }
                        else if (player1.Movement == "Front" && player1.position.Y < ZombieObject[j].position.Y &&
                                (player1.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player1.position.X + player1.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y += 6;
                        }
                        else if (player1.Movement == "Back" && player1.position.Y > ZombieObject[j].position.Y &&
                                (player1.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player1.position.X + player1.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y -= 6;
                        }


                        if (player2.Movement == "Left" && player2.position.X > ZombieObject[j].position.X &&
                            (player2.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player2.position.Y + player2.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X -= 6;
                        }
                        else if (player2.Movement == "Right" && player2.position.X < ZombieObject[j].position.X &&
                                (player2.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player2.position.Y + player2.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X += 6;
                        }
                        else if (player2.Movement == "Front" && player2.position.Y < ZombieObject[j].position.Y &&
                                (player2.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player2.position.X + player2.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y += 6;
                        }
                        else if (player2.Movement == "Back" && player2.position.Y > ZombieObject[j].position.Y &&
                                (player2.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player2.position.X + player2.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y -= 6;
                        }

                        shotGunBullet.shotGunBullet240R[i].Active = false;
                        if (healthBarZombieObject[j].ChckZombieDie())
                        {
                            ZombieDieCount += 1;
                            ZombieObject.RemoveAt(j);
                            healthBarZombieObject.RemoveAt(j);
                        }
                    }
                }
            }


            for (int i = 0; i < shotGunBullet.shotGunBullet255R.Count; ++i)
            {
                for (int j = 0; j < ZombieObject.Count; ++j)
                {
                    if (chckIntersects((int)shotGunBullet.shotGunBullet255R[i].shotGunPosition.X, (int)shotGunBullet.shotGunBullet255R[i].shotGunPosition.Y,
                                       (int)shotGunBullet.shotGunBullet255R[i].ShotGunTextureWidth, (int)shotGunBullet.shotGunBullet255R[i].ShotGunTextureHeight,
                                       (int)ZombieObject[j].position.X, (int)ZombieObject[j].position.Y, (int)ZombieObject[j].Width / 2, (int)ZombieObject[j].Height / 2))
                    {
                        healthBarZombieObject[j].health += shotGunBullet.shotGunDamage;

                        if (player1.Movement == "Left" && player1.position.X > ZombieObject[j].position.X &&
                           (player1.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player1.position.Y + player1.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X -= 6;
                        }
                        else if (player1.Movement == "Right" && player1.position.X < ZombieObject[j].position.X &&
                                (player1.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player1.position.Y + player1.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X += 6;
                        }
                        else if (player1.Movement == "Front" && player1.position.Y < ZombieObject[j].position.Y &&
                                (player1.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player1.position.X + player1.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y += 6;
                        }
                        else if (player1.Movement == "Back" && player1.position.Y > ZombieObject[j].position.Y &&
                                (player1.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player1.position.X + player1.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y -= 6;
                        }

                        if (player2.Movement == "Left" && player2.position.X > ZombieObject[j].position.X &&
                            (player2.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player2.position.Y + player2.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X -= 6;
                        }
                        else if (player2.Movement == "Right" && player2.position.X < ZombieObject[j].position.X &&
                                (player2.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player2.position.Y + player2.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X += 6;
                        }
                        else if (player2.Movement == "Front" && player2.position.Y < ZombieObject[j].position.Y &&
                                (player2.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player2.position.X + player2.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y += 6;
                        }
                        else if (player2.Movement == "Back" && player2.position.Y > ZombieObject[j].position.Y &&
                                (player2.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player2.position.X + player2.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y -= 6;
                        }

                        shotGunBullet.shotGunBullet255R[i].Active = false;
                        if (healthBarZombieObject[j].ChckZombieDie())
                        {
                            ZombieDieCount += 1;
                            ZombieObject.RemoveAt(j);
                            healthBarZombieObject.RemoveAt(j);
                        }
                    }
                }
            }

            for (int i = 0; i < shotGunBullet.shotGunBullet270R.Count; ++i)
            {
                for (int j = 0; j < ZombieObject.Count; ++j)
                {
                    if (chckIntersects((int)shotGunBullet.shotGunBullet270R[i].shotGunPosition.X, (int)shotGunBullet.shotGunBullet270R[i].shotGunPosition.Y,
                                       (int)shotGunBullet.shotGunBullet270R[i].ShotGunTextureWidth, (int)shotGunBullet.shotGunBullet270R[i].ShotGunTextureHeight,
                                       (int)ZombieObject[j].position.X, (int)ZombieObject[j].position.Y, (int)ZombieObject[j].Width / 2, (int)ZombieObject[j].Height / 2))
                    {
                        healthBarZombieObject[j].health += shotGunBullet.shotGunDamage;

                        if (player1.Movement == "Left" && player1.position.X > ZombieObject[j].position.X &&
                           (player1.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player1.position.Y + player1.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X -= 6;
                        }
                        else if (player1.Movement == "Right" && player1.position.X < ZombieObject[j].position.X &&
                                (player1.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player1.position.Y + player1.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X += 6;
                        }
                        else if (player1.Movement == "Front" && player1.position.Y < ZombieObject[j].position.Y &&
                                (player1.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player1.position.X + player1.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y += 6;
                        }
                        else if (player1.Movement == "Back" && player1.position.Y > ZombieObject[j].position.Y &&
                                (player1.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player1.position.X + player1.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y -= 6;
                        }

                        if (player2.Movement == "Left" && player2.position.X > ZombieObject[j].position.X &&
                            (player2.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player2.position.Y + player2.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X -= 6;
                        }
                        else if (player2.Movement == "Right" && player2.position.X < ZombieObject[j].position.X &&
                                (player2.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player2.position.Y + player2.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X += 6;
                        }
                        else if (player2.Movement == "Front" && player2.position.Y < ZombieObject[j].position.Y &&
                                (player2.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player2.position.X + player2.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y += 6;
                        }
                        else if (player2.Movement == "Back" && player2.position.Y > ZombieObject[j].position.Y &&
                                (player2.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player2.position.X + player2.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y -= 6;
                        }

                        shotGunBullet.shotGunBullet270R[i].Active = false;
                        if (healthBarZombieObject[j].ChckZombieDie())
                        {
                            ZombieDieCount += 1;
                            ZombieObject.RemoveAt(j);
                            healthBarZombieObject.RemoveAt(j);
                        }
                    }
                }
            }

            for (int i = 0; i < shotGunBullet.shotGunBullet285R.Count; ++i)
            {
                for (int j = 0; j < ZombieObject.Count; ++j)
                {
                    if (chckIntersects((int)shotGunBullet.shotGunBullet285R[i].shotGunPosition.X, (int)shotGunBullet.shotGunBullet285R[i].shotGunPosition.Y,
                                       (int)shotGunBullet.shotGunBullet285R[i].ShotGunTextureWidth, (int)shotGunBullet.shotGunBullet285R[i].ShotGunTextureHeight,
                                       (int)ZombieObject[j].position.X, (int)ZombieObject[j].position.Y, (int)ZombieObject[j].Width / 2, (int)ZombieObject[j].Height / 2))
                    {
                        healthBarZombieObject[j].health += shotGunBullet.shotGunDamage;

                        if (player1.Movement == "Left" && player1.position.X > ZombieObject[j].position.X &&
                           (player1.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player1.position.Y + player1.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X -= 6;
                        }
                        else if (player1.Movement == "Right" && player1.position.X < ZombieObject[j].position.X &&
                                (player1.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player1.position.Y + player1.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X += 6;
                        }
                        else if (player1.Movement == "Front" && player1.position.Y < ZombieObject[j].position.Y &&
                                (player1.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player1.position.X + player1.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y += 6;
                        }
                        else if (player1.Movement == "Back" && player1.position.Y > ZombieObject[j].position.Y &&
                                (player1.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player1.position.X + player1.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y -= 6;
                        }

                        if (player2.Movement == "Left" && player2.position.X > ZombieObject[j].position.X &&
                            (player2.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player2.position.Y + player2.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X -= 6;
                        }
                        else if (player2.Movement == "Right" && player2.position.X < ZombieObject[j].position.X &&
                                (player2.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player2.position.Y + player2.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X += 6;
                        }
                        else if (player2.Movement == "Front" && player2.position.Y < ZombieObject[j].position.Y &&
                                (player2.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player2.position.X + player2.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y += 6;
                        }
                        else if (player2.Movement == "Back" && player2.position.Y > ZombieObject[j].position.Y &&
                                (player2.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player2.position.X + player2.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y -= 6;
                        }

                        shotGunBullet.shotGunBullet285R[i].Active = false;
                        if (healthBarZombieObject[j].ChckZombieDie())
                        {
                            ZombieDieCount += 1;
                            ZombieObject.RemoveAt(j);
                            healthBarZombieObject.RemoveAt(j);
                        }
                    }
                }
            }

            for (int i = 0; i < shotGunBullet.shotGunBullet300R.Count; ++i)
            {
                for (int j = 0; j < ZombieObject.Count; ++j)
                {
                    if (chckIntersects((int)shotGunBullet.shotGunBullet300R[i].shotGunPosition.X, (int)shotGunBullet.shotGunBullet300R[i].shotGunPosition.Y,
                                       (int)shotGunBullet.shotGunBullet300R[i].ShotGunTextureWidth, (int)shotGunBullet.shotGunBullet300R[i].ShotGunTextureHeight,
                                       (int)ZombieObject[j].position.X, (int)ZombieObject[j].position.Y, (int)ZombieObject[j].Width / 2, (int)ZombieObject[j].Height / 2))
                    {
                        healthBarZombieObject[j].health += shotGunBullet.shotGunDamage;

                        if (player1.Movement == "Left" && player1.position.X > ZombieObject[j].position.X &&
                           (player1.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player1.position.Y + player1.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X -= 6;
                        }
                        else if (player1.Movement == "Right" && player1.position.X < ZombieObject[j].position.X &&
                                (player1.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player1.position.Y + player1.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X += 6;
                        }
                        else if (player1.Movement == "Front" && player1.position.Y < ZombieObject[j].position.Y &&
                                (player1.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player1.position.X + player1.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y += 6;
                        }
                        else if (player1.Movement == "Back" && player1.position.Y > ZombieObject[j].position.Y &&
                                (player1.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player1.position.X + player1.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y -= 6;
                        }

                        if (player2.Movement == "Left" && player2.position.X > ZombieObject[j].position.X &&
                            (player2.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player2.position.Y + player2.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X -= 6;
                        }
                        else if (player2.Movement == "Right" && player2.position.X < ZombieObject[j].position.X &&
                                (player2.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player2.position.Y + player2.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X += 6;
                        }
                        else if (player2.Movement == "Front" && player2.position.Y < ZombieObject[j].position.Y &&
                                (player2.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player2.position.X + player2.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y += 6;
                        }
                        else if (player2.Movement == "Back" && player2.position.Y > ZombieObject[j].position.Y &&
                                (player2.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player2.position.X + player2.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y -= 6;
                        }

                        shotGunBullet.shotGunBullet300R[i].Active = false;
                        if (healthBarZombieObject[j].ChckZombieDie())
                        {
                            ZombieDieCount += 1;
                            ZombieObject.RemoveAt(j);
                            healthBarZombieObject.RemoveAt(j);
                        }
                    }
                }
            }
        }

        public void CheckCollisionExplosionAndZombies(List<GrenadeItem> Explosion, List<Zombie> ZombieObject, List<HealthBar> healthBarZombieObject)
        {

            for (int i = 0; i < Explosion.Count; ++i)
            {
                for (int j = 0; j < ZombieObject.Count; ++j)
                {
                    if (chckIntersects((int)Explosion[i].Position.X - 50, (int)Explosion[i].Position.Y - 50, 190 + 100, 200 + 100,
                                       (int)ZombieObject[j].position.X, (int)ZombieObject[j].position.Y, (int)ZombieObject[j].Width / 2, (int)ZombieObject[j].Height / 2))
                    {
                        sound.punchSound.Play();
                        healthBarZombieObject[j].health += 600;

                        if (ZombieObject[j].movement == "Front")
                        {
                            ZombieObject[j].position.Y -= 50;
                        }
                        else if (ZombieObject[j].movement == "Left")
                        {
                            ZombieObject[j].position.X += 50;
                        }
                        else if (ZombieObject[j].movement == "Right")
                        {
                            ZombieObject[j].position.X -= 50;
                        }
                        else if (ZombieObject[j].movement == "Back")
                        {
                            ZombieObject[j].position.Y += 50;
                        }

                        Explosion[i].Active = false;
                        if (healthBarZombieObject[j].ChckZombieDie())
                        {
                            ZombieDieCount += 1;
                            ZombieObject.RemoveAt(j);
                            healthBarZombieObject.RemoveAt(j);
                        }
                    }
                }
            }
        }

        public void CheckCollisionPistolBulletsAndZombies(List<Bullet> bullet, List<Zombie> zombie, List<HealthBar> healthBarZombie, Player player1, Player player2)
        {

            for (int i = 0; i < bullet.Count; ++i)
            {
                for (int j = 0; j < zombie.Count; ++j)
                {
                    if (chckIntersects((int)bullet[i].pos.X, (int)bullet[i].pos.Y, bullet[i].Width, bullet[i].Height,
                                       (int)zombie[j].position.X, (int)zombie[j].position.Y, (int)zombie[j].Width / 2, (int)zombie[j].Height / 2))
                    {
                        sound.punchSound.Play();
                        healthBarZombie[j].health += 300;

                        if (player1.Movement == "Left" && player1.position.X > zombie[j].position.X &&
                           (player1.position.Y <= zombie[j].position.Y + zombie[j].Height && player1.position.Y + player1.Height >= zombie[j].position.Y))
                        {
                            zombie[j].position.X -= 5;
                        }
                        else if (player1.Movement == "Right" && player1.position.X < zombie[j].position.X &&
                                (player1.position.Y <= zombie[j].position.Y + zombie[j].Height && player1.position.Y + player1.Height >= zombie[j].position.Y))
                        {
                            zombie[j].position.X += 5;
                        }
                        else if (player1.Movement == "Front" && player1.position.Y < zombie[j].position.Y &&
                                (player1.position.X <= zombie[j].position.X + zombie[j].Width && player1.position.X + player1.Width >= zombie[j].position.X))
                        {
                            zombie[j].position.Y += 5;
                        }
                        else if (player1.Movement == "Back" && player1.position.Y > zombie[j].position.Y &&
                                (player1.position.X <= zombie[j].position.X + zombie[j].Width && player1.position.X + player1.Width >= zombie[j].position.X))
                        {
                            zombie[j].position.Y -= 5;
                        }

                        if (player2.Movement == "Left" && player2.position.X > zombie[j].position.X &&
                            (player2.position.Y <= zombie[j].position.Y + zombie[j].Height && player2.position.Y + player2.Height >= zombie[j].position.Y))
                        {
                            zombie[j].position.X -= 5;
                        }
                        else if (player2.Movement == "Right" && player2.position.X < zombie[j].position.X &&
                                (player2.position.Y <= zombie[j].position.Y + zombie[j].Height && player2.position.Y + player2.Height >= zombie[j].position.Y))
                        {
                            zombie[j].position.X += 5;
                        }
                        else if (player2.Movement == "Front" && player2.position.Y < zombie[j].position.Y &&
                                (player2.position.X <= zombie[j].position.X + zombie[j].Width && player2.position.X + player2.Width >= zombie[j].position.X))
                        {
                            zombie[j].position.Y += 5;
                        }
                        else if (player2.Movement == "Back" && player2.position.Y > zombie[j].position.Y &&
                                (player2.position.X <= zombie[j].position.X + zombie[j].Width && player2.position.X + player2.Width >= zombie[j].position.X))
                        {
                            zombie[j].position.Y -= 5;
                        }


                        bullet[i].Active = false;
                        if (healthBarZombie[j].ChckZombieDie())
                        {
                            ZombieDieCount += 1;
                            zombie.RemoveAt(j);
                            healthBarZombie.RemoveAt(j);
                        }
                    }
                }
            }
        }


        public void CheckCollisionMiniBossAndPlayer(List<CreateMiniBoss> createMiniBoss, HealthBar barplayer1, HealthBar barplayer2, Player player1, Player player2)
        {
            Rectangle rec1, rec2, rec3;

            for (int i = 0; i < createMiniBoss.Count; i++)
            {
                rec1 = new Rectangle((int)createMiniBoss[i].position.X - 10, (int)createMiniBoss[i].position.Y - 20, 20, 40);
                rec2 = new Rectangle((int)player1.position.X - 10, (int)player1.position.Y - 20, 20, 40);
                rec3 = new Rectangle((int)player2.position.X - 10, (int)player2.position.Y - 20, 20, 40);


                if (rec1.Intersects(rec2))
                {
                    sound.punchSound.Play();
                    barplayer1.health += 500;
                    if (player1.Movement == "Front")
                    {
                        player1.position.Y -= 90;
                    }
                    if (player1.Movement == "Back")
                    {
                        player1.position.Y += 90;
                    }
                    if (player1.Movement == "Right")
                    {
                        player1.position.X -= 90;
                    }
                    if (player1.Movement == "Left")
                    {
                        player1.position.X += 90;
                    }
                }

                if (rec1.Intersects(rec3))
                {
                    sound.punchSound.Play();
                    barplayer2.health += 500;
                    if (player2.Movement == "Front")
                    {
                        player2.position.Y -= 90;
                    }
                    if (player2.Movement == "Back")
                    {
                        player2.position.Y += 90;
                    }
                    if (player2.Movement == "Right")
                    {
                        player2.position.X -= 90;
                    }
                    if (player2.Movement == "Left")
                    {
                        player2.position.X += 90;
                    }
                }
            }
        }

        public void CheckCollisionPistolBulletsAndMiniBoss(List<Bullet> bullet, List<CreateMiniBoss> createMiniBoss, List<HealthBar> healthBarMiniBoss, Player player1, Player player2)
        {

            for (int i = 0; i < bullet.Count; ++i)
            {
                for (int j = 0; j < createMiniBoss.Count; ++j)
                {
                    if (chckIntersects((int)bullet[i].pos.X, (int)bullet[i].pos.Y, bullet[i].Width, bullet[i].Height,
                                       (int)createMiniBoss[j].position.X, (int)createMiniBoss[j].position.Y, (int)createMiniBoss[j].Width / 2, (int)createMiniBoss[j].Height / 2))
                    {
                        sound.punchSound.Play();
                        healthBarMiniBoss[j].health += 50;

                        if (player1.Movement == "Left" && player1.position.X > createMiniBoss[j].position.X &&
                           (player1.position.Y <= createMiniBoss[j].position.Y + createMiniBoss[j].Height && player1.position.Y + player1.Height >= createMiniBoss[j].position.Y))
                        {
                            createMiniBoss[j].position.X -= 1;
                        }
                        else if (player1.Movement == "Right" && player1.position.X < createMiniBoss[j].position.X &&
                                (player1.position.Y <= createMiniBoss[j].position.Y + createMiniBoss[j].Height && player1.position.Y + player1.Height >= createMiniBoss[j].position.Y))
                        {
                            createMiniBoss[j].position.X += 1;
                        }
                        else if (player1.Movement == "Front" && player1.position.Y < createMiniBoss[j].position.Y &&
                                (player1.position.X <= createMiniBoss[j].position.X + createMiniBoss[j].Width && player1.position.X + player1.Width >= createMiniBoss[j].position.X))
                        {
                            createMiniBoss[j].position.Y += 1;
                        }
                        else if (player1.Movement == "Back" && player1.position.Y > createMiniBoss[j].position.Y &&
                                (player1.position.X <= createMiniBoss[j].position.X + createMiniBoss[j].Width && player1.position.X + player1.Width >= createMiniBoss[j].position.X))
                        {
                            createMiniBoss[j].position.Y -= 1;
                        }

                        if (player2.Movement == "Left" && player2.position.X > createMiniBoss[j].position.X &&
                            (player2.position.Y <= createMiniBoss[j].position.Y + createMiniBoss[j].Height && player2.position.Y + player2.Height >= createMiniBoss[j].position.Y))
                        {
                            createMiniBoss[j].position.X -= 1;
                        }
                        else if (player2.Movement == "Right" && player2.position.X < createMiniBoss[j].position.X &&
                                (player2.position.Y <= createMiniBoss[j].position.Y + createMiniBoss[j].Height && player2.position.Y + player2.Height >= createMiniBoss[j].position.Y))
                        {
                            createMiniBoss[j].position.X += 1;
                        }
                        else if (player2.Movement == "Front" && player2.position.Y < createMiniBoss[j].position.Y &&
                                (player2.position.X <= createMiniBoss[j].position.X + createMiniBoss[j].Width && player2.position.X + player2.Width >= createMiniBoss[j].position.X))
                        {
                            createMiniBoss[j].position.Y += 1;
                        }
                        else if (player2.Movement == "Back" && player2.position.Y > createMiniBoss[j].position.Y &&
                                (player2.position.X <= createMiniBoss[j].position.X + createMiniBoss[j].Width && player2.position.X + player2.Width >= createMiniBoss[j].position.X))
                        {
                            createMiniBoss[j].position.Y -= 1;
                        }


                        bullet[i].Active = false;
                        if (healthBarMiniBoss[j].ChckZombieDie())
                        {
                            MiniBossDieCount += 1;
                            createMiniBoss.RemoveAt(j);
                            healthBarMiniBoss.RemoveAt(j);
                        }
                    }
                }
            }
        }

        public void CheckCollisionExplosionAndMiniBoss(List<GrenadeItem> Explosion, List<CreateMiniBoss> createMiniBoss, List<HealthBar> healthBarMiniBoss)
        {

            for (int i = 0; i < Explosion.Count; ++i)
            {
                for (int j = 0; j < createMiniBoss.Count; ++j)
                {
                    if (chckIntersects((int)Explosion[i].Position.X - 50, (int)Explosion[i].Position.Y - 50, 190 + 100, 200 + 100,
                                       (int)createMiniBoss[j].position.X, (int)createMiniBoss[j].position.Y, (int)createMiniBoss[j].Width / 2, (int)createMiniBoss[j].Height / 2))
                    {
                        sound.punchSound.Play();
                        healthBarMiniBoss[j].health += 300;

                        if (createMiniBoss[j].movement == "Front")
                        {
                            createMiniBoss[j].position.Y -= 20;
                        }
                        else if (createMiniBoss[j].movement == "Left")
                        {
                            createMiniBoss[j].position.X += 20;
                        }
                        else if (createMiniBoss[j].movement == "Right")
                        {
                            createMiniBoss[j].position.X -= 20;
                        }
                        else if (createMiniBoss[j].movement == "Back")
                        {
                            createMiniBoss[j].position.Y += 20;
                        }

                        Explosion[i].Active = false;
                        if (healthBarMiniBoss[j].ChckZombieDie())
                        {
                            MiniBossDieCount += 1;
                            createMiniBoss.RemoveAt(j);
                            healthBarMiniBoss.RemoveAt(j);
                        }
                    }
                }
            }
        }

        public void CheckCollisionShotGunBulletAndMiniBoss(List<CreateMiniBoss> ZombieObject, ShotGunBullet shotGunBullet, Player player1, Player player2, List<HealthBar> healthBarZombieObject)
        {
            for (int i = 0; i < shotGunBullet.shotGunBullet240R.Count; ++i)
            {
                for (int j = 0; j < ZombieObject.Count; ++j)
                {
                    if (chckIntersects((int)shotGunBullet.shotGunBullet240R[i].shotGunPosition.X, (int)shotGunBullet.shotGunBullet240R[i].shotGunPosition.Y,
                                       (int)shotGunBullet.shotGunBullet240R[i].ShotGunTextureWidth, (int)shotGunBullet.shotGunBullet240R[i].ShotGunTextureHeight,
                                       (int)ZombieObject[j].position.X, (int)ZombieObject[j].position.Y, (int)ZombieObject[j].Width / 2, (int)ZombieObject[j].Height / 2))
                    {
                        sound.punchSound.Play();
                        healthBarZombieObject[j].health += 70;

                        if (player1.Movement == "Left" && player1.position.X > ZombieObject[j].position.X &&
                           (player1.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player1.position.Y + player1.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X -= 3;
                        }
                        else if (player1.Movement == "Right" && player1.position.X < ZombieObject[j].position.X &&
                                (player1.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player1.position.Y + player1.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X += 3;
                        }
                        else if (player1.Movement == "Front" && player1.position.Y < ZombieObject[j].position.Y &&
                                (player1.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player1.position.X + player1.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y += 3;
                        }
                        else if (player1.Movement == "Back" && player1.position.Y > ZombieObject[j].position.Y &&
                                (player1.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player1.position.X + player1.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y -= 3;
                        }


                        if (player2.Movement == "Left" && player2.position.X > ZombieObject[j].position.X &&
                            (player2.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player2.position.Y + player2.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X -= 3;
                        }
                        else if (player2.Movement == "Right" && player2.position.X < ZombieObject[j].position.X &&
                                (player2.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player2.position.Y + player2.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X += 3;
                        }
                        else if (player2.Movement == "Front" && player2.position.Y < ZombieObject[j].position.Y &&
                                (player2.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player2.position.X + player2.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y += 3;
                        }
                        else if (player2.Movement == "Back" && player2.position.Y > ZombieObject[j].position.Y &&
                                (player2.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player2.position.X + player2.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y -= 3;
                        }

                        shotGunBullet.shotGunBullet240R[i].Active = false;
                        if (healthBarZombieObject[j].ChckZombieDie())
                        {
                            MiniBossDieCount += 1;
                            ZombieObject.RemoveAt(j);
                            healthBarZombieObject.RemoveAt(j);
                        }
                    }
                }
            }


            for (int i = 0; i < shotGunBullet.shotGunBullet255R.Count; ++i)
            {
                for (int j = 0; j < ZombieObject.Count; ++j)
                {
                    if (chckIntersects((int)shotGunBullet.shotGunBullet255R[i].shotGunPosition.X, (int)shotGunBullet.shotGunBullet255R[i].shotGunPosition.Y,
                                       (int)shotGunBullet.shotGunBullet255R[i].ShotGunTextureWidth, (int)shotGunBullet.shotGunBullet255R[i].ShotGunTextureHeight,
                                       (int)ZombieObject[j].position.X, (int)ZombieObject[j].position.Y, (int)ZombieObject[j].Width / 2, (int)ZombieObject[j].Height / 2))
                    {
                        healthBarZombieObject[j].health += 70;

                        if (player1.Movement == "Left" && player1.position.X > ZombieObject[j].position.X &&
                           (player1.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player1.position.Y + player1.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X -= 3;
                        }
                        else if (player1.Movement == "Right" && player1.position.X < ZombieObject[j].position.X &&
                                (player1.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player1.position.Y + player1.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X += 3;
                        }
                        else if (player1.Movement == "Front" && player1.position.Y < ZombieObject[j].position.Y &&
                                (player1.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player1.position.X + player1.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y += 3;
                        }
                        else if (player1.Movement == "Back" && player1.position.Y > ZombieObject[j].position.Y &&
                                (player1.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player1.position.X + player1.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y -= 3;
                        }

                        if (player2.Movement == "Left" && player2.position.X > ZombieObject[j].position.X &&
                            (player2.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player2.position.Y + player2.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X -= 3;
                        }
                        else if (player2.Movement == "Right" && player2.position.X < ZombieObject[j].position.X &&
                                (player2.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player2.position.Y + player2.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X += 3;
                        }
                        else if (player2.Movement == "Front" && player2.position.Y < ZombieObject[j].position.Y &&
                                (player2.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player2.position.X + player2.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y += 3;
                        }
                        else if (player2.Movement == "Back" && player2.position.Y > ZombieObject[j].position.Y &&
                                (player2.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player2.position.X + player2.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y -= 3;
                        }

                        shotGunBullet.shotGunBullet255R[i].Active = false;
                        if (healthBarZombieObject[j].ChckZombieDie())
                        {
                            MiniBossDieCount += 1;
                            ZombieObject.RemoveAt(j);
                            healthBarZombieObject.RemoveAt(j);
                        }
                    }
                }
            }

            for (int i = 0; i < shotGunBullet.shotGunBullet270R.Count; ++i)
            {
                for (int j = 0; j < ZombieObject.Count; ++j)
                {
                    if (chckIntersects((int)shotGunBullet.shotGunBullet270R[i].shotGunPosition.X, (int)shotGunBullet.shotGunBullet270R[i].shotGunPosition.Y,
                                       (int)shotGunBullet.shotGunBullet270R[i].ShotGunTextureWidth, (int)shotGunBullet.shotGunBullet270R[i].ShotGunTextureHeight,
                                       (int)ZombieObject[j].position.X, (int)ZombieObject[j].position.Y, (int)ZombieObject[j].Width / 2, (int)ZombieObject[j].Height / 2))
                    {
                        healthBarZombieObject[j].health += 70;

                        if (player1.Movement == "Left" && player1.position.X > ZombieObject[j].position.X &&
                           (player1.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player1.position.Y + player1.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X -= 3;
                        }
                        else if (player1.Movement == "Right" && player1.position.X < ZombieObject[j].position.X &&
                                (player1.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player1.position.Y + player1.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X += 3;
                        }
                        else if (player1.Movement == "Front" && player1.position.Y < ZombieObject[j].position.Y &&
                                (player1.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player1.position.X + player1.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y += 3;
                        }
                        else if (player1.Movement == "Back" && player1.position.Y > ZombieObject[j].position.Y &&
                                (player1.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player1.position.X + player1.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y -= 3;
                        }

                        if (player2.Movement == "Left" && player2.position.X > ZombieObject[j].position.X &&
                            (player2.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player2.position.Y + player2.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X -= 3;
                        }
                        else if (player2.Movement == "Right" && player2.position.X < ZombieObject[j].position.X &&
                                (player2.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player2.position.Y + player2.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X += 3;
                        }
                        else if (player2.Movement == "Front" && player2.position.Y < ZombieObject[j].position.Y &&
                                (player2.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player2.position.X + player2.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y += 3;
                        }
                        else if (player2.Movement == "Back" && player2.position.Y > ZombieObject[j].position.Y &&
                                (player2.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player2.position.X + player2.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y -= 3;
                        }

                        shotGunBullet.shotGunBullet270R[i].Active = false;
                        if (healthBarZombieObject[j].ChckZombieDie())
                        {
                            MiniBossDieCount += 1;
                            ZombieObject.RemoveAt(j);
                            healthBarZombieObject.RemoveAt(j);
                        }
                    }
                }
            }

            for (int i = 0; i < shotGunBullet.shotGunBullet285R.Count; ++i)
            {
                for (int j = 0; j < ZombieObject.Count; ++j)
                {
                    if (chckIntersects((int)shotGunBullet.shotGunBullet285R[i].shotGunPosition.X, (int)shotGunBullet.shotGunBullet285R[i].shotGunPosition.Y,
                                       (int)shotGunBullet.shotGunBullet285R[i].ShotGunTextureWidth, (int)shotGunBullet.shotGunBullet285R[i].ShotGunTextureHeight,
                                       (int)ZombieObject[j].position.X, (int)ZombieObject[j].position.Y, (int)ZombieObject[j].Width / 2, (int)ZombieObject[j].Height / 2))
                    {
                        healthBarZombieObject[j].health += 70;

                        if (player1.Movement == "Left" && player1.position.X > ZombieObject[j].position.X &&
                           (player1.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player1.position.Y + player1.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X -= 3;
                        }
                        else if (player1.Movement == "Right" && player1.position.X < ZombieObject[j].position.X &&
                                (player1.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player1.position.Y + player1.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X += 3;
                        }
                        else if (player1.Movement == "Front" && player1.position.Y < ZombieObject[j].position.Y &&
                                (player1.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player1.position.X + player1.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y += 3;
                        }
                        else if (player1.Movement == "Back" && player1.position.Y > ZombieObject[j].position.Y &&
                                (player1.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player1.position.X + player1.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y -= 3;
                        }

                        if (player2.Movement == "Left" && player2.position.X > ZombieObject[j].position.X &&
                            (player2.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player2.position.Y + player2.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X -= 3;
                        }
                        else if (player2.Movement == "Right" && player2.position.X < ZombieObject[j].position.X &&
                                (player2.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player2.position.Y + player2.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X += 3;
                        }
                        else if (player2.Movement == "Front" && player2.position.Y < ZombieObject[j].position.Y &&
                                (player2.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player2.position.X + player2.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y += 3;
                        }
                        else if (player2.Movement == "Back" && player2.position.Y > ZombieObject[j].position.Y &&
                                (player2.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player2.position.X + player2.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y -= 3;
                        }

                        shotGunBullet.shotGunBullet285R[i].Active = false;
                        if (healthBarZombieObject[j].ChckZombieDie())
                        {
                            MiniBossDieCount += 1;
                            ZombieObject.RemoveAt(j);
                            healthBarZombieObject.RemoveAt(j);
                        }
                    }
                }
            }

            for (int i = 0; i < shotGunBullet.shotGunBullet300R.Count; ++i)
            {
                for (int j = 0; j < ZombieObject.Count; ++j)
                {
                    if (chckIntersects((int)shotGunBullet.shotGunBullet300R[i].shotGunPosition.X, (int)shotGunBullet.shotGunBullet300R[i].shotGunPosition.Y,
                                       (int)shotGunBullet.shotGunBullet300R[i].ShotGunTextureWidth, (int)shotGunBullet.shotGunBullet300R[i].ShotGunTextureHeight,
                                       (int)ZombieObject[j].position.X, (int)ZombieObject[j].position.Y, (int)ZombieObject[j].Width / 2, (int)ZombieObject[j].Height / 2))
                    {
                        healthBarZombieObject[j].health += 70;

                        if (player1.Movement == "Left" && player1.position.X > ZombieObject[j].position.X &&
                           (player1.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player1.position.Y + player1.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X -= 3;
                        }
                        else if (player1.Movement == "Right" && player1.position.X < ZombieObject[j].position.X &&
                                (player1.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player1.position.Y + player1.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X += 3;
                        }
                        else if (player1.Movement == "Front" && player1.position.Y < ZombieObject[j].position.Y &&
                                (player1.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player1.position.X + player1.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y += 3;
                        }
                        else if (player1.Movement == "Back" && player1.position.Y > ZombieObject[j].position.Y &&
                                (player1.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player1.position.X + player1.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y -= 3;
                        }

                        if (player2.Movement == "Left" && player2.position.X > ZombieObject[j].position.X &&
                            (player2.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player2.position.Y + player2.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X -= 3;
                        }
                        else if (player2.Movement == "Right" && player2.position.X < ZombieObject[j].position.X &&
                                (player2.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player2.position.Y + player2.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X += 3;
                        }
                        else if (player2.Movement == "Front" && player2.position.Y < ZombieObject[j].position.Y &&
                                (player2.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player2.position.X + player2.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y += 3;
                        }
                        else if (player2.Movement == "Back" && player2.position.Y > ZombieObject[j].position.Y &&
                                (player2.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player2.position.X + player2.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y -= 3;
                        }

                        shotGunBullet.shotGunBullet300R[i].Active = false;
                        if (healthBarZombieObject[j].ChckZombieDie())
                        {
                            MiniBossDieCount += 1;
                            ZombieObject.RemoveAt(j);
                            healthBarZombieObject.RemoveAt(j);
                        }
                    }
                }
            }
        }
       
        public void CheckCollisionBossFireAndPlayer(Player player1, Player player2, List<CreateMiniBoss> CreateMini, HealthBar barplayer1, HealthBar barplayer2, GameTime gameTime)
        {
            Rectangle rec1, rec2, rec3;
            rec1 = new Rectangle((int)player1.position.X - 10, (int)player1.position.Y - 20, 20, 40);
            rec2 = new Rectangle((int)player2.position.X - 10, (int)player2.position.Y - 20, 20, 40);

            //deltaTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            for (int i = 0; i < CreateMini.Count; i++)
            {
                for (int j = 0; j < CreateMini[i].bulletBoss.Count; j++)
                {
                    rec3 = new Rectangle((int)CreateMini[i].bulletBoss[j].position.X - 30, (int)CreateMini[i].bulletBoss[j].position.Y - 20, 60, 40);

                    if (rec3.Intersects(rec1))
                    {
                        sound.punchSound.Play();
                        player1.animation.Active = true;
                        CreateMini[i].bulletBoss[0].Active = false;
                    }
                    if (rec3.Intersects(rec2))
                    {
                        sound.punchSound.Play();
                        player2.animation.Active = true;
                        CreateMini[i].bulletBoss[0].Active = false;
                    }
                }

            }

            if (player1.animation.Active)
            {
                deltaTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (deltaTime >= 3.0f)
                {
                    player1.animation.Active = false;
                    deltaTime = 0;
                }
                if (deltaTime <= 3.0f)
                {
                    barplayer1.health += 5;
                }
            }

            if (player2.animation.Active)
            {
                deltaTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (deltaTime >= 3.0f)
                {
                    player2.animation.Active = false;
                    deltaTime = 0;
                }
                if (deltaTime <= 3.0f)
                {
                    barplayer2.health += 5;
                }
            }
        }


        public void CheckCollisionLastBossAndPlayer(List<CreateMiniBoss> createMiniBoss, HealthBar barplayer1, HealthBar barplayer2, Player player1, Player player2)
        {
            Rectangle rec1, rec2, rec3;

            for (int i = 0; i < createMiniBoss.Count; i++)
            {
                rec1 = new Rectangle((int)createMiniBoss[i].position.X - 10, (int)createMiniBoss[i].position.Y - 20, 20, 40);
                rec2 = new Rectangle((int)player1.position.X - 10, (int)player1.position.Y - 20, 20, 40);
                rec3 = new Rectangle((int)player2.position.X - 10, (int)player2.position.Y - 20, 20, 40);


                if (rec1.Intersects(rec2))
                {
                    sound.punchSound.Play();
                    barplayer1.health += 900;
                    if (player1.Movement == "Front")
                    {
                        player1.position.Y -= 120;
                    }
                    if (player1.Movement == "Back")
                    {
                        player1.position.Y += 120;
                    }
                    if (player1.Movement == "Right")
                    {
                        player1.position.X -= 120;
                    }
                    if (player1.Movement == "Left")
                    {
                        player1.position.X += 120;
                    }
                }

                if (rec1.Intersects(rec3))
                {
                    sound.punchSound.Play();
                    barplayer2.health += 900;
                    if (player2.Movement == "Front")
                    {
                        player2.position.Y -= 120;
                    }
                    if (player2.Movement == "Back")
                    {
                        player2.position.Y += 120;
                    }
                    if (player2.Movement == "Right")
                    {
                        player2.position.X -= 120;
                    }
                    if (player2.Movement == "Left")
                    {
                        player2.position.X += 120;
                    }
                }
            }
        }

        public void CheckCollisionPistolBulletsAndLastBoss(List<Bullet> bullet, List<CreateMiniBoss> createMiniBoss, List<HealthBar> healthBarMiniBoss, Player player1, Player player2)
        {

            for (int i = 0; i < bullet.Count; ++i)
            {
                for (int j = 0; j < createMiniBoss.Count; ++j)
                {
                    if (chckIntersects((int)bullet[i].pos.X, (int)bullet[i].pos.Y, bullet[i].Width, bullet[i].Height,
                                       (int)createMiniBoss[j].position.X, (int)createMiniBoss[j].position.Y, (int)createMiniBoss[j].Width / 2, (int)createMiniBoss[j].Height / 2))
                    {
                        sound.punchSound.Play();
                        healthBarMiniBoss[j].health += 20;

                        if (player1.Movement == "Left" && player1.position.X > createMiniBoss[j].position.X &&
                           (player1.position.Y <= createMiniBoss[j].position.Y + createMiniBoss[j].Height && player1.position.Y + player1.Height >= createMiniBoss[j].position.Y))
                        {
                            createMiniBoss[j].position.X -= 1;
                        }
                        else if (player1.Movement == "Right" && player1.position.X < createMiniBoss[j].position.X &&
                                (player1.position.Y <= createMiniBoss[j].position.Y + createMiniBoss[j].Height && player1.position.Y + player1.Height >= createMiniBoss[j].position.Y))
                        {
                            createMiniBoss[j].position.X += 1;
                        }
                        else if (player1.Movement == "Front" && player1.position.Y < createMiniBoss[j].position.Y &&
                                (player1.position.X <= createMiniBoss[j].position.X + createMiniBoss[j].Width && player1.position.X + player1.Width >= createMiniBoss[j].position.X))
                        {
                            createMiniBoss[j].position.Y += 1;
                        }
                        else if (player1.Movement == "Back" && player1.position.Y > createMiniBoss[j].position.Y &&
                                (player1.position.X <= createMiniBoss[j].position.X + createMiniBoss[j].Width && player1.position.X + player1.Width >= createMiniBoss[j].position.X))
                        {
                            createMiniBoss[j].position.Y -= 1;
                        }

                        if (player2.Movement == "Left" && player2.position.X > createMiniBoss[j].position.X &&
                            (player2.position.Y <= createMiniBoss[j].position.Y + createMiniBoss[j].Height && player2.position.Y + player2.Height >= createMiniBoss[j].position.Y))
                        {
                            createMiniBoss[j].position.X -= 1;
                        }
                        else if (player2.Movement == "Right" && player2.position.X < createMiniBoss[j].position.X &&
                                (player2.position.Y <= createMiniBoss[j].position.Y + createMiniBoss[j].Height && player2.position.Y + player2.Height >= createMiniBoss[j].position.Y))
                        {
                            createMiniBoss[j].position.X += 1;
                        }
                        else if (player2.Movement == "Front" && player2.position.Y < createMiniBoss[j].position.Y &&
                                (player2.position.X <= createMiniBoss[j].position.X + createMiniBoss[j].Width && player2.position.X + player2.Width >= createMiniBoss[j].position.X))
                        {
                            createMiniBoss[j].position.Y += 1;
                        }
                        else if (player2.Movement == "Back" && player2.position.Y > createMiniBoss[j].position.Y &&
                                (player2.position.X <= createMiniBoss[j].position.X + createMiniBoss[j].Width && player2.position.X + player2.Width >= createMiniBoss[j].position.X))
                        {
                            createMiniBoss[j].position.Y -= 1;
                        }


                        bullet[i].Active = false;
                        if (healthBarMiniBoss[j].ChckZombieDie())
                        {
                            LastBossDieCount += 1;
                            createMiniBoss.RemoveAt(j);
                            healthBarMiniBoss.RemoveAt(j);
                        }
                    }
                }
            }
        }

        public void CheckCollisionExplosionAndLastBoss(List<GrenadeItem> Explosion, List<CreateMiniBoss> createMiniBoss, List<HealthBar> healthBarMiniBoss)
        {

            for (int i = 0; i < Explosion.Count; ++i)
            {
                for (int j = 0; j < createMiniBoss.Count; ++j)
                {
                    if (chckIntersects((int)Explosion[i].Position.X - 50, (int)Explosion[i].Position.Y - 50, 190 + 100, 200 + 100,
                                       (int)createMiniBoss[j].position.X, (int)createMiniBoss[j].position.Y, (int)createMiniBoss[j].Width / 2, (int)createMiniBoss[j].Height / 2))
                    {
                        sound.punchSound.Play();
                        healthBarMiniBoss[j].health += 200;

                        if (createMiniBoss[j].movement == "Front")
                        {
                            createMiniBoss[j].position.Y -= 15;
                        }
                        else if (createMiniBoss[j].movement == "Left")
                        {
                            createMiniBoss[j].position.X += 15;
                        }
                        else if (createMiniBoss[j].movement == "Right")
                        {
                            createMiniBoss[j].position.X -= 15;
                        }
                        else if (createMiniBoss[j].movement == "Back")
                        {
                            createMiniBoss[j].position.Y += 15;
                        }

                        Explosion[i].Active = false;
                        if (healthBarMiniBoss[j].ChckZombieDie())
                        {
                            LastBossDieCount += 1;
                            createMiniBoss.RemoveAt(j);
                            healthBarMiniBoss.RemoveAt(j);
                        }
                    }
                }
            }
        }

        public void CheckCollisionShotGunBulletAndLastBoss(List<CreateMiniBoss> ZombieObject, ShotGunBullet shotGunBullet, Player player1, Player player2, List<HealthBar> healthBarZombieObject)
        {
            for (int i = 0; i < shotGunBullet.shotGunBullet240R.Count; ++i)
            {
                for (int j = 0; j < ZombieObject.Count; ++j)
                {
                    if (chckIntersects((int)shotGunBullet.shotGunBullet240R[i].shotGunPosition.X, (int)shotGunBullet.shotGunBullet240R[i].shotGunPosition.Y,
                                       (int)shotGunBullet.shotGunBullet240R[i].ShotGunTextureWidth, (int)shotGunBullet.shotGunBullet240R[i].ShotGunTextureHeight,
                                       (int)ZombieObject[j].position.X, (int)ZombieObject[j].position.Y, (int)ZombieObject[j].Width / 2, (int)ZombieObject[j].Height / 2))
                    {
                        sound.punchSound.Play();
                        healthBarZombieObject[j].health += 20;

                        if (player1.Movement == "Left" && player1.position.X > ZombieObject[j].position.X &&
                           (player1.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player1.position.Y + player1.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X -= 2;
                        }
                        else if (player1.Movement == "Right" && player1.position.X < ZombieObject[j].position.X &&
                                (player1.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player1.position.Y + player1.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X += 2;
                        }
                        else if (player1.Movement == "Front" && player1.position.Y < ZombieObject[j].position.Y &&
                                (player1.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player1.position.X + player1.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y += 2;
                        }
                        else if (player1.Movement == "Back" && player1.position.Y > ZombieObject[j].position.Y &&
                                (player1.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player1.position.X + player1.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y -= 2;
                        }


                        if (player2.Movement == "Left" && player2.position.X > ZombieObject[j].position.X &&
                            (player2.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player2.position.Y + player2.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X -= 2;
                        }
                        else if (player2.Movement == "Right" && player2.position.X < ZombieObject[j].position.X &&
                                (player2.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player2.position.Y + player2.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X += 2;
                        }
                        else if (player2.Movement == "Front" && player2.position.Y < ZombieObject[j].position.Y &&
                                (player2.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player2.position.X + player2.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y += 2;
                        }
                        else if (player2.Movement == "Back" && player2.position.Y > ZombieObject[j].position.Y &&
                                (player2.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player2.position.X + player2.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y -= 2;
                        }

                        shotGunBullet.shotGunBullet240R[i].Active = false;
                        if (healthBarZombieObject[j].ChckZombieDie())
                        {
                            LastBossDieCount += 1;
                            ZombieObject.RemoveAt(j);
                            healthBarZombieObject.RemoveAt(j);
                        }
                    }
                }
            }


            for (int i = 0; i < shotGunBullet.shotGunBullet255R.Count; ++i)
            {
                for (int j = 0; j < ZombieObject.Count; ++j)
                {
                    if (chckIntersects((int)shotGunBullet.shotGunBullet255R[i].shotGunPosition.X, (int)shotGunBullet.shotGunBullet255R[i].shotGunPosition.Y,
                                       (int)shotGunBullet.shotGunBullet255R[i].ShotGunTextureWidth, (int)shotGunBullet.shotGunBullet255R[i].ShotGunTextureHeight,
                                       (int)ZombieObject[j].position.X, (int)ZombieObject[j].position.Y, (int)ZombieObject[j].Width / 2, (int)ZombieObject[j].Height / 2))
                    {
                        healthBarZombieObject[j].health += 20;

                        if (player1.Movement == "Left" && player1.position.X > ZombieObject[j].position.X &&
                           (player1.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player1.position.Y + player1.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X -= 2;
                        }
                        else if (player1.Movement == "Right" && player1.position.X < ZombieObject[j].position.X &&
                                (player1.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player1.position.Y + player1.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X += 2;
                        }
                        else if (player1.Movement == "Front" && player1.position.Y < ZombieObject[j].position.Y &&
                                (player1.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player1.position.X + player1.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y += 2;
                        }
                        else if (player1.Movement == "Back" && player1.position.Y > ZombieObject[j].position.Y &&
                                (player1.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player1.position.X + player1.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y -= 2;
                        }

                        if (player2.Movement == "Left" && player2.position.X > ZombieObject[j].position.X &&
                            (player2.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player2.position.Y + player2.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X -= 2;
                        }
                        else if (player2.Movement == "Right" && player2.position.X < ZombieObject[j].position.X &&
                                (player2.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player2.position.Y + player2.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X += 2;
                        }
                        else if (player2.Movement == "Front" && player2.position.Y < ZombieObject[j].position.Y &&
                                (player2.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player2.position.X + player2.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y += 2;
                        }
                        else if (player2.Movement == "Back" && player2.position.Y > ZombieObject[j].position.Y &&
                                (player2.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player2.position.X + player2.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y -= 2;
                        }

                        shotGunBullet.shotGunBullet255R[i].Active = false;
                        if (healthBarZombieObject[j].ChckZombieDie())
                        {
                            LastBossDieCount += 1;
                            ZombieObject.RemoveAt(j);
                            healthBarZombieObject.RemoveAt(j);
                        }
                    }
                }
            }

            for (int i = 0; i < shotGunBullet.shotGunBullet270R.Count; ++i)
            {
                for (int j = 0; j < ZombieObject.Count; ++j)
                {
                    if (chckIntersects((int)shotGunBullet.shotGunBullet270R[i].shotGunPosition.X, (int)shotGunBullet.shotGunBullet270R[i].shotGunPosition.Y,
                                       (int)shotGunBullet.shotGunBullet270R[i].ShotGunTextureWidth, (int)shotGunBullet.shotGunBullet270R[i].ShotGunTextureHeight,
                                       (int)ZombieObject[j].position.X, (int)ZombieObject[j].position.Y, (int)ZombieObject[j].Width / 2, (int)ZombieObject[j].Height / 2))
                    {
                        healthBarZombieObject[j].health += 20;

                        if (player1.Movement == "Left" && player1.position.X > ZombieObject[j].position.X &&
                           (player1.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player1.position.Y + player1.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X -= 2;
                        }
                        else if (player1.Movement == "Right" && player1.position.X < ZombieObject[j].position.X &&
                                (player1.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player1.position.Y + player1.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X += 2;
                        }
                        else if (player1.Movement == "Front" && player1.position.Y < ZombieObject[j].position.Y &&
                                (player1.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player1.position.X + player1.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y += 2;
                        }
                        else if (player1.Movement == "Back" && player1.position.Y > ZombieObject[j].position.Y &&
                                (player1.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player1.position.X + player1.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y -= 2;
                        }

                        if (player2.Movement == "Left" && player2.position.X > ZombieObject[j].position.X &&
                            (player2.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player2.position.Y + player2.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X -= 2;
                        }
                        else if (player2.Movement == "Right" && player2.position.X < ZombieObject[j].position.X &&
                                (player2.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player2.position.Y + player2.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X += 2;
                        }
                        else if (player2.Movement == "Front" && player2.position.Y < ZombieObject[j].position.Y &&
                                (player2.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player2.position.X + player2.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y += 2;
                        }
                        else if (player2.Movement == "Back" && player2.position.Y > ZombieObject[j].position.Y &&
                                (player2.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player2.position.X + player2.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y -= 2;
                        }

                        shotGunBullet.shotGunBullet270R[i].Active = false;
                        if (healthBarZombieObject[j].ChckZombieDie())
                        {
                            LastBossDieCount += 1;
                            ZombieObject.RemoveAt(j);
                            healthBarZombieObject.RemoveAt(j);
                        }
                    }
                }
            }

            for (int i = 0; i < shotGunBullet.shotGunBullet285R.Count; ++i)
            {
                for (int j = 0; j < ZombieObject.Count; ++j)
                {
                    if (chckIntersects((int)shotGunBullet.shotGunBullet285R[i].shotGunPosition.X, (int)shotGunBullet.shotGunBullet285R[i].shotGunPosition.Y,
                                       (int)shotGunBullet.shotGunBullet285R[i].ShotGunTextureWidth, (int)shotGunBullet.shotGunBullet285R[i].ShotGunTextureHeight,
                                       (int)ZombieObject[j].position.X, (int)ZombieObject[j].position.Y, (int)ZombieObject[j].Width / 2, (int)ZombieObject[j].Height / 2))
                    {
                        healthBarZombieObject[j].health += 20;

                        if (player1.Movement == "Left" && player1.position.X > ZombieObject[j].position.X &&
                           (player1.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player1.position.Y + player1.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X -= 2;
                        }
                        else if (player1.Movement == "Right" && player1.position.X < ZombieObject[j].position.X &&
                                (player1.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player1.position.Y + player1.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X += 2;
                        }
                        else if (player1.Movement == "Front" && player1.position.Y < ZombieObject[j].position.Y &&
                                (player1.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player1.position.X + player1.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y += 2;
                        }
                        else if (player1.Movement == "Back" && player1.position.Y > ZombieObject[j].position.Y &&
                                (player1.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player1.position.X + player1.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y -= 2;
                        }

                        if (player2.Movement == "Left" && player2.position.X > ZombieObject[j].position.X &&
                            (player2.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player2.position.Y + player2.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X -= 2;
                        }
                        else if (player2.Movement == "Right" && player2.position.X < ZombieObject[j].position.X &&
                                (player2.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player2.position.Y + player2.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X += 2;
                        }
                        else if (player2.Movement == "Front" && player2.position.Y < ZombieObject[j].position.Y &&
                                (player2.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player2.position.X + player2.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y += 2;
                        }
                        else if (player2.Movement == "Back" && player2.position.Y > ZombieObject[j].position.Y &&
                                (player2.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player2.position.X + player2.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y -= 2;
                        }

                        shotGunBullet.shotGunBullet285R[i].Active = false;
                        if (healthBarZombieObject[j].ChckZombieDie())
                        {
                            LastBossDieCount += 1;
                            ZombieObject.RemoveAt(j);
                            healthBarZombieObject.RemoveAt(j);
                        }
                    }
                }
            }

            for (int i = 0; i < shotGunBullet.shotGunBullet300R.Count; ++i)
            {
                for (int j = 0; j < ZombieObject.Count; ++j)
                {
                    if (chckIntersects((int)shotGunBullet.shotGunBullet300R[i].shotGunPosition.X, (int)shotGunBullet.shotGunBullet300R[i].shotGunPosition.Y,
                                       (int)shotGunBullet.shotGunBullet300R[i].ShotGunTextureWidth, (int)shotGunBullet.shotGunBullet300R[i].ShotGunTextureHeight,
                                       (int)ZombieObject[j].position.X, (int)ZombieObject[j].position.Y, (int)ZombieObject[j].Width / 2, (int)ZombieObject[j].Height / 2))
                    {
                        healthBarZombieObject[j].health += 20;

                        if (player1.Movement == "Left" && player1.position.X > ZombieObject[j].position.X &&
                           (player1.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player1.position.Y + player1.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X -= 2;
                        }
                        else if (player1.Movement == "Right" && player1.position.X < ZombieObject[j].position.X &&
                                (player1.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player1.position.Y + player1.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X += 2;
                        }
                        else if (player1.Movement == "Front" && player1.position.Y < ZombieObject[j].position.Y &&
                                (player1.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player1.position.X + player1.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y += 2;
                        }
                        else if (player1.Movement == "Back" && player1.position.Y > ZombieObject[j].position.Y &&
                                (player1.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player1.position.X + player1.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y -= 2;
                        }

                        if (player2.Movement == "Left" && player2.position.X > ZombieObject[j].position.X &&
                            (player2.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player2.position.Y + player2.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X -= 2;
                        }
                        else if (player2.Movement == "Right" && player2.position.X < ZombieObject[j].position.X &&
                                (player2.position.Y <= ZombieObject[j].position.Y + ZombieObject[j].Height && player2.position.Y + player2.Height >= ZombieObject[j].position.Y))
                        {
                            ZombieObject[j].position.X += 2;
                        }
                        else if (player2.Movement == "Front" && player2.position.Y < ZombieObject[j].position.Y &&
                                (player2.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player2.position.X + player2.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y += 2;
                        }
                        else if (player2.Movement == "Back" && player2.position.Y > ZombieObject[j].position.Y &&
                                (player2.position.X <= ZombieObject[j].position.X + ZombieObject[j].Width && player2.position.X + player2.Width >= ZombieObject[j].position.X))
                        {
                            ZombieObject[j].position.Y -= 2;
                        }

                        shotGunBullet.shotGunBullet300R[i].Active = false;
                        if (healthBarZombieObject[j].ChckZombieDie())
                        {
                            LastBossDieCount += 1;
                            ZombieObject.RemoveAt(j);
                            healthBarZombieObject.RemoveAt(j);
                        }
                    }
                }
            }
        }

        public void CheckCollisionLastBossFireAndPlayer(Player player1, Player player2, List<CreateMiniBoss> CreateMini, HealthBar barplayer1, HealthBar barplayer2, GameTime gameTime)
        {
            Rectangle rec1, rec2, rec3;
            rec1 = new Rectangle((int)player1.position.X - 10, (int)player1.position.Y - 20, 20, 40);
            rec2 = new Rectangle((int)player2.position.X - 10, (int)player2.position.Y - 20, 20, 40);

            //deltaTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            for (int i = 0; i < CreateMini.Count; i++)
            {
                for (int j = 0; j < CreateMini[i].bulletBoss.Count; j++)
                {
                    rec3 = new Rectangle((int)CreateMini[i].bulletBoss[j].position.X - 30, (int)CreateMini[i].bulletBoss[j].position.Y - 20, 60, 40);

                    if (rec3.Intersects(rec1))
                    {
                        sound.punchSound.Play();
                        player1.animation.Active = true;
                        CreateMini[i].bulletBoss[0].Active = false;
                    }
                    if (rec3.Intersects(rec2))
                    {
                        sound.punchSound.Play();
                        player2.animation.Active = true;
                        CreateMini[i].bulletBoss[0].Active = false;
                    }
                }

            }

            if (player1.animation.Active)
            {
                deltaTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (deltaTime >= 3.0f)
                {
                    player1.animation.Active = false;
                    deltaTime = 0;
                }
                if (deltaTime <= 3.0f)
                {
                    barplayer1.health += 7;
                }
            }

            if (player2.animation.Active)
            {
                deltaTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (deltaTime >= 3.0f)
                {
                    player2.animation.Active = false;
                    deltaTime = 0;
                }
                if (deltaTime <= 3.0f)
                {
                    barplayer2.health += 7;
                }
            }
        }





        // Intersects // 
        bool chckIntersects(int xPosition1, int yPosition1, int widthSize1, int heightSize1,
            int xPosition2, int yPosition2, int widthSize2, int heightSize2)
        {
            Rectangle rectangle1, rectangle2;
            rectangle1 = new Rectangle(xPosition1, yPosition1, widthSize1, heightSize1);
            rectangle2 = new Rectangle(xPosition2, yPosition2, widthSize2, heightSize2);

            if (rectangle1.Intersects(rectangle2))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        // Intersects // 



    }
}
