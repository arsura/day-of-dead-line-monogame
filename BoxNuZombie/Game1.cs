using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace BoxNuZombie
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player1, player2;
        SpriteFont Font;
        SpriteFont GameOverFont;


        KeyboardState kbstate;
        List<Bullet> bullet;

        Collision collision;

        Camera camera;

        HealthBar barplayer1, barplayer2;

        // Explosion // ----------------------------------------------------
        List<SiwakornAnimation> explosions;
        Texture2D explosionsTexture;
        // Explosion // ----------------------------------------------------

        // MedicineItem // -------------------------------------------------
        List<MedicineItem> medicine;
        TimeSpan medicineSpawnTime;
        TimeSpan previousMedicineSpawnTime;
        // MedicineItem // -------------------------------------------------

        // ShotGunItem // --------------------------------------------------
        List<ShotGunItem> shotgun;
        TimeSpan shotgunSpawnTime;
        TimeSpan previousShotgunSpawnTime;
        // ShotGunItem // --------------------------------------------------

        // GrenadeItem // --------------------------------------------------
        List<GrenadeItem> grenade;
        List<GrenadeItem> grenadePut;
        TimeSpan grenadeSpawnTime;
        TimeSpan previousGrenadeSpawnTime;
        public int canUseGrenade = 0;
        // GrenadeItem // --------------------------------------------------

        // ShotGun Bullet // -----------------------------------------------
        ShotGunBullet shotGunBullet;
        ShotGunBullet shotGunBullet2;
        // ShotGun Bullet // -----------------------------------------------

        // Keyboard and Mouse // -------------------------------------------
        MouseKeyboardInput Input;
        // Keyboard and Mouse // -------------------------------------------

        // Zombie // -------------------------------------------------------
        Level zombie;
        // Zombie // -------------------------------------------------------

        // MiniBoss // -----------------------------------------------------
        List<MiniBoss> miniBoss;
        List<CreateMiniBoss> createMiniBoss;
        List<HealthBar> healthBarMiniBoss;
        // MiniBoss // -----------------------------------------------------

        // LastBoss // -----------------------------------------------------
        List<MiniBoss> lastBoss;
        List<CreateMiniBoss> createLastBoss;
        List<HealthBar> healthBarLastBoss;
        // LastBoss // -----------------------------------------------------

        Level level;
        Song song;
        public int currentLevel = 1;       

        Texture2D backgroundTexture;

        float elaspe1;
        float countTimebl1;
        float elaspe2;
        float countTimebl2;

        Random random;
        Sound sound;

        // Game Over // -----------------------------------------------------
        Texture2D GameOver;
        Texture2D StartScreen;
        Texture2D WinnerScreen;
        bool StartScreenActive = true;
        bool Winner = false;
        // Game Over // -----------------------------------------------------

        



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.graphics.PreferredBackBufferWidth = 800;
            this.graphics.PreferredBackBufferHeight = 600;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Input = new MouseKeyboardInput();

            player1 = new Player(40, 60, 150);
            player2 = new Player(40, 60, 150);
            bullet = new List<Bullet>();
            
            barplayer1 = new HealthBar();
            barplayer2 = new HealthBar();

            camera = new Camera();
            collision = new Collision();

            medicine = new List<MedicineItem>();
            previousMedicineSpawnTime = TimeSpan.Zero;
            medicineSpawnTime = TimeSpan.FromSeconds(2.0f);

            shotgun = new List<ShotGunItem>();
            previousShotgunSpawnTime = TimeSpan.Zero;
            shotgunSpawnTime = TimeSpan.FromSeconds(10.0f);

            grenade = new List<GrenadeItem>();
            grenadePut = new List<GrenadeItem>();
            previousGrenadeSpawnTime = TimeSpan.Zero;
            grenadeSpawnTime = TimeSpan.FromSeconds(5.0f);

            explosions = new List<SiwakornAnimation>();

            countTimebl1 = 350;
            countTimebl2 = 350;
            elaspe1 = countTimebl1;
            elaspe2 = countTimebl2;

            shotGunBullet = new ShotGunBullet();
            shotGunBullet2 = new ShotGunBullet();

            shotGunBullet.Initialized(Content);
            shotGunBullet2.Initialized(Content);

            random = new Random();

            Font = Content.Load<SpriteFont>("Font");
            GameOverFont = Content.Load<SpriteFont>("LevelFont");
            sound = new Sound();
            collision.CollisionSoundLoadContent(Content);

            // Zombie // ------------------------------------
            zombie = new Level();
            zombie.ZombieInitialize(Content);
            // Zombie // ------------------------------------

            miniBoss = new List<MiniBoss>();
            lastBoss = new List<MiniBoss>();
            level = new Level();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            player1.Loadcontent(Content, "brian");
            player2.Loadcontent(Content, "dancer");

            //player1.Loadcontent(Content, "Player1");
            //player2.Loadcontent(Content, "Player2");


            player1.position = new Vector2(960, 540);
            player2.position = new Vector2(960 + 20, 540);
            barplayer1.LoadContentForPlayer(Content);
            barplayer2.LoadContentForPlayer(Content);

            backgroundTexture = Content.Load<Texture2D>("Map8BitTest");
            explosionsTexture = Content.Load<Texture2D>("explosion");

            GameOver = Content.Load<Texture2D>("GameOver");
            StartScreen = Content.Load<Texture2D>("MainMenu");
            WinnerScreen = Content.Load<Texture2D>("Winner");


            song = Content.Load<Song>("Morroc");
            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;

            TonLoadContentMiniBoss();
            TonLoadContentLastBoss();


            sound.LoadContent(Content);

            // Pause;  
            
        }

        void TonLoadContentMiniBoss()
        {
            MiniBoss mb = new MiniBoss(2, "firebolt2");
            mb.LoadContent(Content);
            miniBoss.Add(mb);
            createMiniBoss = miniBoss[0].miniBoss;
            healthBarMiniBoss = miniBoss[0].healthBarMiniBoss;
        }

        void TonLoadContentLastBoss()
        {
            MiniBoss lb = new MiniBoss(2, "firebolt3");
            lb.LoadContentLastBoss(Content);
            lastBoss.Add(lb);
            createLastBoss = lastBoss[0].miniBoss;
            healthBarLastBoss = lastBoss[0].healthBarMiniBoss;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 

        private bool switchScreen;
        void SwitchScreen()
        {
            var keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.F4) && !switchScreen)
            {
                graphics.ToggleFullScreen();
            }
            switchScreen = keyboardState.IsKeyDown(Keys.F4);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (StartScreenActive == true)
            {
                Input.Update();
                camera.Update(new Vector2(400, 240));
                if (Input.KeyPressed(Keys.Enter))
                {
                    StartScreenActive = false;
                }
            }
            if (StartScreenActive == false)
            {
                SwitchScreen();
                ChangeBackGround();

                kbstate = Keyboard.GetState();
                CheckKeys(gameTime);

                CheckCollision(gameTime);

                Player1Update(gameTime);
                Player2Update(gameTime);

                UpdateBullet();
                UpdateZombie(gameTime);

                UpdateShotGun(gameTime);
                UpdateMedecine(gameTime);
                UpdateGrenade(gameTime);

                UpdateExplosion(gameTime);
                LevelSet(gameTime);
                Input.Update();

                CameraUpdate();
            }

            base.Update(gameTime);
        }

        float elapsedTimeShowLevelLabel1;
        float elapsedTimeShowLevelLabel2;
        float elapsedTimeShowLevelLabel3;
        float elapsedTimeShowLevelLabel4;
        float elapsedTimeShowLevelLabel5;

        void LevelSet(GameTime gameTime)
        {
            if (currentLevel == 3 && collision.ZombieDieCount > 7)
            {
                UpdateMiniBoss(gameTime);
            }
            if (currentLevel == 4 && collision.ZombieDieCount > 10)
            {
                UpdateMiniBoss(gameTime);
            }
            if (currentLevel == 5 && collision.ZombieDieCount > 15)
            {
                UpdateMiniBoss(gameTime);
                UpdateLastBoss(gameTime);
            }

        }

        void LabelLevelAndReset(GameTime gameTime)
        {

            // Level 1 // ----------------------------------------------------------------------
            elapsedTimeShowLevelLabel1 += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (elapsedTimeShowLevelLabel1 <= 3.0f)
            {
                zombie.LevelShowMessage(Content, spriteBatch, player1, 1);
            }
            // Level 1 // ----------------------------------------------------------------------


            // Level 2 // ----------------------------------------------------------------------
            if (zombie.readyChange && currentLevel == 1)
            {
                elapsedTimeShowLevelLabel2 += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (elapsedTimeShowLevelLabel2 <= 3.0f)
                {
                    for (int i = zombie.zombie.Count - 1; i >= 0; i--)
                    {
                        zombie.zombie.RemoveAt(i);
                    }

                    for (int i = medicine.Count - 1; i >= 0; i--)
                    {
                        medicine.RemoveAt(i);
                    }

                    for (int i = shotgun.Count - 1; i >= 0; i--)
                    {
                        shotgun.RemoveAt(i);
                    }

                    for (int i = grenade.Count - 1; i >= 0; i--)
                    {
                        grenade.RemoveAt(i);
                    }

                    if (player1.Active == true)
                    {
                        player1.position.X = 960;
                        player1.position.Y = 760;
                    }
                    if (player2.Active == true)
                    {
                        player2.position.X = 960 + 40;
                        player2.position.Y = 760;
                    }

                    zombie.LevelShowMessage(Content, spriteBatch, player1, 2);                
                    if (elapsedTimeShowLevelLabel2 > 2.9f)
                    {
                        sound.levelUpSound.Play();
                        zombie.readyChange = false;
                        collision.ZombieDieCount = 0;
                        elapsedTimeShowLevelLabel2 = 0;
                        zombie.Level2Active = true;
                        currentLevel = 2;
                    }
                }
            }
            // Level 2 // ----------------------------------------------------------------------


            // Level 3 // ----------------------------------------------------------------------
            if (zombie.Level2Active == true && currentLevel == 2 && zombie.readyChange == true)
            {
                //zombie.Level2Active = false;
                elapsedTimeShowLevelLabel3 += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (elapsedTimeShowLevelLabel3 <= 3.0f)
                {
                    for (int i = zombie.zombie.Count - 1; i >= 0; i--)
                    {
                        zombie.zombie.RemoveAt(i);
                    }

                    for (int i = medicine.Count - 1; i >= 0; i--)
                    {
                        medicine.RemoveAt(i);
                    }

                    for (int i = shotgun.Count - 1; i >= 0; i--)
                    {
                        shotgun.RemoveAt(i);
                    }

                    for (int i = grenade.Count - 1; i >= 0; i--)
                    {
                        grenade.RemoveAt(i);
                    }

                    if (player1.Active == true)
                    {
                        player1.position.X = 960;
                        player1.position.Y = 760;
                    }
                    if (player2.Active == true)
                    {
                        player2.position.X = 960 + 40;
                        player2.position.Y = 760;
                    }

                    zombie.LevelShowMessage(Content, spriteBatch, player1, 3);
                    if (elapsedTimeShowLevelLabel3 > 2.9f)
                    {
                        sound.levelUpSound.Play();
                        zombie.readyChange = false;
                        collision.ZombieDieCount = 0;
                        elapsedTimeShowLevelLabel3 = 0;
                        zombie.Level3Active = true;
                        currentLevel = 3;
                    }
                }
            }
            // Level 3 // ----------------------------------------------------------------------


            // Level 4 // ----------------------------------------------------------------------
            if (zombie.Level3Active == true && currentLevel == 3 && zombie.readyChange == true && collision.MiniBossDieCount == 2)
            {
                for (int i = zombie.zombie.Count - 1; i >= 0; i--)
                {
                    zombie.zombie.RemoveAt(i);
                }

                for (int i = medicine.Count - 1; i >= 0; i--)
                {
                    medicine.RemoveAt(i);
                }

                for (int i = shotgun.Count - 1; i >= 0; i--)
                {
                    shotgun.RemoveAt(i);
                }

                for (int i = grenade.Count - 1; i >= 0; i--)
                {
                    grenade.RemoveAt(i);
                }

                if (player1.Active == true)
                {
                    player1.position.X = 960;
                    player1.position.Y = 760;
                }
                if (player2.Active == true)
                {
                    player2.position.X = 960 + 40;
                    player2.position.Y = 760;
                }

                elapsedTimeShowLevelLabel4 += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (elapsedTimeShowLevelLabel4 <= 3.0f)
                {
                    miniBoss.RemoveAt(0);
                    MiniBoss mb = new MiniBoss(3, "firebolt2");
                    mb.LoadContent(Content);
                    miniBoss.Add(mb);
                    createMiniBoss = miniBoss[0].miniBoss;
                    healthBarMiniBoss = miniBoss[0].healthBarMiniBoss;

                    zombie.LevelShowMessage(Content, spriteBatch, player1, 4);
                    if (elapsedTimeShowLevelLabel4 > 2.9f)
                    {
                        sound.levelUpSound.Play();
                        zombie.readyChange = false;
                        collision.ZombieDieCount = 0;
                        elapsedTimeShowLevelLabel4 = 0;
                        zombie.Level4Active = true;
                        collision.MiniBossDieCount = 0;
                        currentLevel = 4;
                    }
                }
            }
            // Level 4 // ----------------------------------------------------------------------


            // Level 5 // ----------------------------------------------------------------------
            if (zombie.Level4Active == true && currentLevel == 4 && zombie.readyChange == true && collision.MiniBossDieCount == 3)
            {
                for (int i = zombie.zombie.Count - 1; i >= 0; i--)
                {
                    zombie.zombie.RemoveAt(i);
                }

                for (int i = medicine.Count - 1; i >= 0; i--)
                {
                    medicine.RemoveAt(i);
                }

                for (int i = shotgun.Count - 1; i >= 0; i--)
                {
                    shotgun.RemoveAt(i);
                }

                for (int i = grenade.Count - 1; i >= 0; i--)
                {
                    grenade.RemoveAt(i);
                }

                if (player1.Active == true)
                {
                    player1.position.X = 960;
                    player1.position.Y = 760;
                }
                if (player2.Active == true)
                {
                    player2.position.X = 960 + 40;
                    player2.position.Y = 760;
                }

                elapsedTimeShowLevelLabel5 += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (elapsedTimeShowLevelLabel5 <= 3.0f)
                {
                    miniBoss.RemoveAt(0);
                    MiniBoss mb = new MiniBoss(2, "firebolt2");
                    mb.LoadContent(Content);
                    miniBoss.Add(mb);
                    createMiniBoss = miniBoss[0].miniBoss;
                    healthBarMiniBoss = miniBoss[0].healthBarMiniBoss;

                    zombie.LevelShowMessage(Content, spriteBatch, player1, 5);
                    if (elapsedTimeShowLevelLabel5 > 2.9f)
                    {
                        sound.levelUpSound.Play();
                        zombie.readyChange = false;
                        collision.ZombieDieCount = 0;
                        elapsedTimeShowLevelLabel5 = 0;
                        zombie.Level5Active = true;
                        collision.MiniBossDieCount = 0;
                        currentLevel = 5;
                    }
                }
            }
            // Level 5 // ----------------------------------------------------------------------
            if (currentLevel == 5 && zombie.readyChange == true)
            {
                Winner = true;
            }

        }

        void ChangeBackGround()
        {
            if (currentLevel == 1)
            {
                backgroundTexture = Content.Load<Texture2D>("StageOne");
            }
            if (currentLevel == 2)
            {
                backgroundTexture = Content.Load<Texture2D>("StageOne");
            }
            if (currentLevel == 3)
            {
                backgroundTexture = Content.Load<Texture2D>("StageTwo");
            }
            if (currentLevel == 4)
            {
                backgroundTexture = Content.Load<Texture2D>("StageTwo");
            }
            if (currentLevel == 5)
            {
                backgroundTexture = Content.Load<Texture2D>("StageThree");
            }
        }

        void CameraUpdate()
        {
            if (StartScreenActive)
            {
                camera.Update(new Vector2(400, 240));
            }

            if (player1.Active == false)
            {
                camera.Update(player2.position);
            }
            else
            {
                camera.Update(player1.position);
            }

            if (player1.Active == false && player2.Active == false)
            {
                camera.Update(new Vector2(400, 240));
            }

            if (player2.position.X > player1.position.X + 380 && player2.Active == true && player1.Active == true)
            {
                player2.position.X -= 2;
            }
            if (player2.position.X < player1.position.X - 380 && player2.Active == true && player1.Active == true)
            {
                player2.position.X += 2;
            }
            if (player2.position.Y > player1.position.Y + 320 && player2.Active == true && player1.Active == true)
            {
                player2.position.Y -= 2;
            }
            if (player2.position.Y + 200 < player1.position.Y && player2.Active == true && player1.Active == true)
            {
                player2.position.Y += 2;
            }
        }

        void CheckCollision(GameTime gameTime)
        {
            // Collision Zombie // -------------------------------------------------------------------------------------------------------
            collision.CheckCollisionPistolBulletsAndZombies(bullet, zombie.zombie, zombie.healthBarZombie, player1, player2);
            collision.CheckCollisionShotGunBulletAndZombieObjects(zombie.zombie, shotGunBullet, player1, player2, zombie.healthBarZombie);
            collision.CheckCollisionShotGunBulletAndZombieObjects(zombie.zombie, shotGunBullet2, player1, player2, zombie.healthBarZombie);
            collision.CheckCollisionZombiesAndPlayer(zombie.zombie, barplayer1, barplayer2, player1, player2);
            // Collision Zombie // -------------------------------------------------------------------------------------------------------

            // Collision MiniBoss // -----------------------------------------------------------------------------------------------------
            collision.CheckCollisionMiniBossAndPlayer(createMiniBoss, barplayer1, barplayer2, player1, player2);
            collision.CheckCollisionPistolBulletsAndMiniBoss(bullet, createMiniBoss, healthBarMiniBoss, player1, player2);       
            collision.CheckCollisionShotGunBulletAndMiniBoss(createMiniBoss, shotGunBullet, player1, player2, healthBarMiniBoss);
            collision.CheckCollisionShotGunBulletAndMiniBoss(createMiniBoss, shotGunBullet2, player1, player2, healthBarMiniBoss);
            collision.CheckCollisionBossFireAndPlayer(player1, player2, createMiniBoss, barplayer1, barplayer2, gameTime);
            // Collision MiniBoss // -----------------------------------------------------------------------------------------------------

            // Collision LastBoss // -----------------------------------------------------------------------------------------------------
            collision.CheckCollisionLastBossAndPlayer(createLastBoss, barplayer1, barplayer2, player1, player2);
            collision.CheckCollisionPistolBulletsAndLastBoss(bullet, createLastBoss, healthBarLastBoss, player1, player2);
            collision.CheckCollisionShotGunBulletAndLastBoss(createLastBoss, shotGunBullet, player1, player2, healthBarLastBoss);
            collision.CheckCollisionShotGunBulletAndLastBoss(createLastBoss, shotGunBullet2, player1, player2, healthBarLastBoss);
            collision.CheckCollisionLastBossFireAndPlayer(player1, player2, createLastBoss, barplayer1, barplayer2, gameTime);
            // Collision LastBoss // -----------------------------------------------------------------------------------------------------
        }
        void CheckKeys(GameTime gameTime)
        {
            //Player1
            KeyboardState previousKS = kbstate;
            kbstate = Keyboard.GetState();

            if (kbstate.IsKeyDown(Keys.W))
            {
                player1.Movement = "Back";
            }
            if (kbstate.IsKeyDown(Keys.S))
            {
                player1.Movement = "Front";
            }
            if (kbstate.IsKeyDown(Keys.A))
            {
                player1.Movement = "Left";
            }
            if (kbstate.IsKeyDown(Keys.D))
            {
                player1.Movement = "Right";
            }

            if (kbstate.IsKeyDown(Keys.F))
            {
                elaspe1 += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (elaspe1 >= countTimebl1)
                {
                    sound.pistolSound.Play();
                    AddBullet(player1.Movement, player1.position);
                    elaspe1 = 0;
                }
            }

            if (kbstate.IsKeyUp(Keys.F))
            {
                elaspe1 = countTimebl1;
            }

            //Player2
            if (kbstate.IsKeyDown(Keys.Up))
            {
                player2.Movement = "Back";
            }
            if (kbstate.IsKeyDown(Keys.Down))
            {
                player2.Movement = "Front";
            }
            if (kbstate.IsKeyDown(Keys.Left))
            {
                player2.Movement = "Left";
            }
            if (kbstate.IsKeyDown(Keys.Right))
            {
                player2.Movement = "Right";
            }

            if (kbstate.IsKeyDown(Keys.P))
            {
                elaspe2 += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (elaspe2 >= countTimebl2)
                {
                    sound.pistolSound.Play();
                    AddBullet(player2.Movement, player2.position);
                    elaspe2 = 0;
                }
            }

            if (kbstate.IsKeyUp(Keys.P))
            {
                elaspe2 = countTimebl2;
            }

        }

        void UpdateZombie(GameTime gameTime)
        {          
            zombie.CalculateAngle(player1, player2);
            zombie.ZombieUpdate(Content, gameTime, collision.ZombieDieCount);
        }

        void UpdateMiniBoss(GameTime gameTime)
        {
            for (int i = 0; i < createMiniBoss.Count; ++i)
            {
                createMiniBoss[i].CalculateAngle(player1, player2, createMiniBoss);
            }

            miniBoss[0].Update(gameTime);
            createMiniBoss = miniBoss[0].miniBoss;
            healthBarMiniBoss = miniBoss[0].healthBarMiniBoss;
        }

        void UpdateLastBoss(GameTime gameTime)
        {
            for (int i = 0; i < createLastBoss.Count; ++i)
            {
                createLastBoss[i].CalculateAngle(player1, player2, createLastBoss);
            }

            lastBoss[0].Update(gameTime);
            createLastBoss = lastBoss[0].miniBoss;
            healthBarLastBoss = lastBoss[0].healthBarMiniBoss;
        }

        void UpdateMedecine(GameTime gameTime)
        {
            if (gameTime.TotalGameTime - previousMedicineSpawnTime > medicineSpawnTime)
            {
                previousMedicineSpawnTime = gameTime.TotalGameTime;
                AddMedicine();
            }
            if (collision.CollisionMedicine(medicine, player1) && barplayer1.health > 102)
            {
                barplayer1.health -= 300;
            }
            if (collision.CollisionMedicine(medicine, player2) && barplayer2.health > 102)
            {
                barplayer2.health -= 300;
            }
        }
        void AddMedicine()
        {
            MedicineItem medicines = new MedicineItem(Content, new Vector2(random.Next(100, 1700), random.Next(600, 700)));
            medicine.Add(medicines);
        }

        void UpdateGrenade(GameTime gameTime)
        {
            if (gameTime.TotalGameTime - previousGrenadeSpawnTime > grenadeSpawnTime)
            {
                previousGrenadeSpawnTime = gameTime.TotalGameTime;
                AddGrenade();
            }
            if (collision.CollisionGrenade(grenade, player1) || collision.CollisionGrenade(grenade, player2))
            {
                ++canUseGrenade;
            }
            if (Input.KeyPressed(Keys.E) && canUseGrenade > 0)
            {
                sound.pickUpGunSound.Play();
                canUseGrenade -= 1;
                AddGrenadePut(player1);
            }
            if (Input.KeyPressed(Keys.L) && canUseGrenade > 0)
            {
                sound.pickUpGunSound.Play();
                canUseGrenade -= 1;
                AddGrenadePut(player2);
            }

            if (Input.KeyPressed(Keys.R) || Input.KeyPressed(Keys.I))
            {
                for (int i = grenadePut.Count - 1; i >= 0; i--)
                {
                    sound.explosionSound.Play();
                    AddExplosion(grenadePut[i].Position);
                    grenadePut.RemoveAt(i);
                    collision.CheckCollisionExplosionAndZombies(grenadePut, zombie.zombie, zombie.healthBarZombie);
                    collision.CheckCollisionExplosionAndMiniBoss(grenadePut, createMiniBoss, healthBarMiniBoss);
                    collision.CheckCollisionExplosionAndMiniBoss(grenadePut, createLastBoss, healthBarLastBoss);
                }
            }

        }
        void AddGrenade()
        {
            GrenadeItem grenades = new GrenadeItem(Content, new Vector2(random.Next(100, 1700), random.Next(600, 800)));
            grenade.Add(grenades);
        }
        void AddGrenadePut(Player playerObject)
        {
            GrenadeItem grenadesPut = new GrenadeItem(Content, new Vector2(playerObject.position.X - 40, playerObject.position.Y));
            grenadePut.Add(grenadesPut);
        }

        // Explosions // -------------------------------------------------------------------
        void UpdateExplosion(GameTime gameTime)
        {
            for (int i = explosions.Count - 1; i >= 0; --i)
            {
                explosions[i].Update(gameTime);
                if (explosions[i].Active == false)
                {
                    explosions.RemoveAt(i);
                }
            }
        }
        void AddExplosion(Vector2 position)
        {
            SiwakornAnimation explosion = new SiwakornAnimation(explosionsTexture, position, 192, 200, 25, 20, Color.White, 1f, false);
            explosions.Add(explosion);
        }
        // Explosions // -------------------------------------------------------------------

        void UpdateShotGun(GameTime gameTime)
        {
            if (Input.KeyPressed(Keys.G) && shotGunBullet.shotGunActive == true)
            {
                shotGunBullet.RotationAngle(Content, gameTime, player1.Movement, player1);
                shotGunBullet.ShotGunMagazine -= 1;
                sound.shotGunSound.Play();
            }
            shotGunBullet.Update(Content, gameTime, player1.Movement, player1);


            if (Input.KeyPressed(Keys.O) && shotGunBullet2.shotGunActive == true)
            {
                shotGunBullet2.RotationAngle(Content, gameTime, player2.Movement, player2);
                shotGunBullet2.ShotGunMagazine -= 1;
                sound.shotGunSound.Play();
            }
            shotGunBullet2.Update(Content, gameTime, player2.Movement, player2);

            if (gameTime.TotalGameTime - previousShotgunSpawnTime > shotgunSpawnTime)
            {
                previousShotgunSpawnTime = gameTime.TotalGameTime;
                AddShotGun();
            }

            if (collision.CollisionShotGun(shotgun, player1))
            {
                shotGunBullet.shotGunActive = true;
                shotGunBullet.ShotGunMagazine += 20;
            }

            if (shotGunBullet.ShotGunMagazine < 1)
            {
                shotGunBullet.shotGunActive = false;
            }

            if (collision.CollisionShotGun(shotgun, player2))
            {
                shotGunBullet2.shotGunActive = true;
                shotGunBullet2.ShotGunMagazine += 20;
            }

            if (shotGunBullet2.ShotGunMagazine < 1)
            {
                shotGunBullet2.shotGunActive = false;
            }
        }
        void AddShotGun()
        {
            ShotGunItem shotguns = new ShotGunItem(Content, new Vector2(random.Next(100, 1700), random.Next(600, 800)));
            shotgun.Add(shotguns);
        }

        void Player1Update(GameTime gameTime)
        {
            if (player1.Active && barplayer1.ChckPlayerDie() == false)
            {
                player1.Update(gameTime);
                barplayer1.Update(player1.position);
            }
            if (barplayer1.ChckPlayerDie() == true)
            {
                player1.Active = false;
                player1.position.X = 3000;
                player1.position.Y = 3000;
            }
            if (Input.KeyPressedHold(Keys.LeftShift))
            {
                player1.walk = 3;
                barplayer1.health += 5;
            }
            else
            {
                player1.walk = 2;
            }
        }
        void Player2Update(GameTime gameTime)
        {
            if (player2.Active && barplayer2.ChckPlayerDie() == false)
            {
                player2.Update(gameTime);
                barplayer2.Update(player2.position);
            }
            if (barplayer2.ChckPlayerDie() == true)
            {
                player2.Active = false;
                player2.position.X = 3000;
                player2.position.Y = 3000;
            }
            if (Input.KeyPressedHold(Keys.RightShift))
            {
                player2.walk = 3;
                barplayer2.health += 5;
            }
            else
            {
                player2.walk = 2;
            }
        }

        void UpdateBullet()
        {
            for (int i = 0; i < bullet.Count; i++)
            {
                bullet[i].Update();
            }
        }
        void AddBullet(string direction, Vector2 position)
        {

            Bullet b = new Bullet();
            b.LoadContent(Content);
            b.pos = position;
            b.movbullets = direction;
            bullet.Add(b);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null ,null, null, camera.ViewMatrix);
            spriteBatch.Draw(backgroundTexture, Vector2.Zero, Color.White);
        
            DrawUI();
            DrawItem();
            DrawPlayer();
            DrawZombie();
            DrawFront();
            DrawChangeLevel(gameTime);


            GameOverDraw(gameTime);

            StartScreenDraw();
            GameWinnerDrawn(gameTime);

            spriteBatch.End();        
            
                              
            base.Draw(gameTime);
        }

        void DrawUI()
        {
            if (shotGunBullet.shotGunActive) spriteBatch.DrawString(Font, "Shotgun: " + shotGunBullet.ShotGunMagazine,
                                                        new Vector2(player1.position.X - 35, player1.position.Y + 35), Color.White);
            if (shotGunBullet2.shotGunActive) spriteBatch.DrawString(Font, "Shotgun: " + shotGunBullet2.ShotGunMagazine,
                                                                    new Vector2(player2.position.X - 35, player2.position.Y + 35), Color.White);

            if (canUseGrenade > 0) spriteBatch.DrawString(Font, "Grenade: " + canUseGrenade,
                                                        new Vector2(player1.position.X - 32, player1.position.Y + 50), Color.White);
            if (canUseGrenade > 0) spriteBatch.DrawString(Font, "Grenade: " + canUseGrenade,
                                            new Vector2(player2.position.X - 32, player2.position.Y + 50), Color.White);


            //spriteBatch.DrawString(Font, "" +collision.ZombieDieCount, new Vector2(player1.position.X, player1.position.Y + 65), Color.White);
            //spriteBatch.DrawString(Font, "" +collision.MiniBossDieCount, new Vector2(player1.position.X , player1.position.Y + 80), Color.White);
            //spriteBatch.DrawString(Font, "" + zombie.readyChange, new Vector2(player1.position.X - 10, player1.position.Y + 90), Color.White);
            spriteBatch.DrawString(Font, "" +collision.LastBossDieCount, new Vector2(player1.position.X , player1.position.Y + 80), Color.White);

        }

        void DrawItem()
        {
            for (int i = 0; i < medicine.Count; ++i)
            {
                medicine[i].Draw(spriteBatch);
            }
            for (int i = 0; i < shotgun.Count; ++i)
            {
                shotgun[i].Draw(spriteBatch);
            }

            for (int i = 0; i < grenade.Count; ++i)
            {
                grenade[i].Draw(spriteBatch);
            }

            for (int i = 0; i < grenadePut.Count; ++i)
            {
                grenadePut[i].DrawGrenadePut(spriteBatch);
            }
        }

        void DrawPlayer()
        {
            if (player1.Active)
            {
                player1.Draw(spriteBatch);
            }

            if (player2.Active)
            {
                player2.Draw(spriteBatch);
            }

            for (int i = bullet.Count - 1; i >= 0; i--)
            {
                bullet[i].Draw(spriteBatch);
                if (!bullet[i].Active)
                {
                    bullet.RemoveAt(i);
                }
            }

            shotGunBullet.Draw(spriteBatch);
            shotGunBullet2.Draw(spriteBatch);

            if (!barplayer1.ChckPlayerDie())
            {
                barplayer1.Draw(spriteBatch);
            }
            if (!barplayer2.ChckPlayerDie())
            {
                barplayer2.Draw(spriteBatch);
            }


        }

        void DrawZombie()
        {
            miniBoss[0].Draw(spriteBatch);
            lastBoss[0].Draw(spriteBatch);

            for (int i = zombie.zombie.Count - 1; i >= 0; i--)
            {
                zombie.zombie[i].Draw(spriteBatch);
                zombie.healthBarZombie[i].Draw(spriteBatch);
                if (!zombie.zombie[i].Active)
                {
                    zombie.zombie.RemoveAt(i);
                }
            }
        }

        void DrawFront()
        {
            for (int i = 0; i < explosions.Count; ++i)
            {
                explosions[i].Draw(spriteBatch);
            }
        }

        void DrawChangeLevel(GameTime gameTime)
        {
            LabelLevelAndReset(gameTime);
        }


        float elapsedGameOver;
        void GameOverDraw(GameTime gameTime)
        {
            if (player1.Active == false && player2.Active == false)
            {
                elapsedGameOver += (float)gameTime.ElapsedGameTime.TotalSeconds;
                spriteBatch.Draw(GameOver, new Vector2(0, 0), Color.White);
                player1.Active = false;
                player2.Active = false;

                if (elapsedGameOver > 5.0f)
                {
                    Exit();
                }
            }
        }

        float elapsedWinnerTime;
        void GameWinnerDrawn(GameTime gameTime)
        {
            if (Winner == true)
            {
                elapsedWinnerTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                spriteBatch.Draw(WinnerScreen, new Vector2(0, 0), Color.White);
                player1.Active = false;
                player2.Active = false;
                if (elapsedWinnerTime > 5.0f)
                {
                    Exit();
                }
            }
        }

        void StartScreenDraw()
        {
            if (StartScreenActive == true)
            {
                spriteBatch.Draw(StartScreen, new Vector2(0, 0), Color.White);
            }
        }

    }
}
