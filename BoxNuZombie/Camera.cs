﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BoxNuZombie
{
    class Camera
    {
        Vector2 position;
        Matrix viewMatrix;
        
        public Matrix ViewMatrix
        {
            get { return viewMatrix; }
        }

        public int ScreenWidth
        {
            get { return GraphicsDeviceManager.DefaultBackBufferWidth; }
        }

        public int ScreenHeight
        {
            get { return GraphicsDeviceManager.DefaultBackBufferHeight; }
        }

        public void Update(Vector2 playerPosition)
        {
            position.X = playerPosition.X - (ScreenWidth / 2);
            position.Y = playerPosition.Y - (ScreenHeight / 2);

            //if (position.X < 0)
            //{
            //    position.X = 0;
            //}

            //if (position.Y < 0)
            //{
            //    position.Y = 0;
            //}

            viewMatrix = Matrix.CreateTranslation(new Vector3(-position, 0));

        }
        
        
        
         
    }
}
