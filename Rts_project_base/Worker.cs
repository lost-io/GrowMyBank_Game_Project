﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;

namespace Rts_project_base
{
    class Worker : GameObject
    {
        #region Fields
        private float speed;
        private bool working;
        private bool carryingResource;
        #endregion
        #region Property
       
        public float Speed { get { return speed; } set { speed = value; } }
        #endregion
        #region Constructor
        public Worker(Vector2 position,string spritePath, float scaleFactor,string name): base(position,spritePath,scaleFactor, name)
        {
        }
        #endregion
        #region Methods

        private void InitWorkerThread()
        {
            Thread WorkerThread = new Thread(Work);

            WorkerThread.Start();
        }
        public void Work()
        {
            while (working)
            {
                
            }
        }   
        public void Mine()
        {
            Thread.Sleep(3000);//simulates the worker mining
            carryingResource = true;
        }
        public void MoveTo(float x, float y)
        {
            position.X = x;
            position.Y = y;
        }
        #endregion

    }
}
