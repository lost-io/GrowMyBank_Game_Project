﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;

namespace Rts_project_base
{
    public partial class GameForm : Form
    {
        #region Fields
        private GameWorld gm;
        private Graphics dc;
        private Rectangle displayRectangle;
        private GameObject destinationObject;
        private GameObject selectedWorker;
        private GameObject selectedObject;
        public static bool runGame;
        #endregion
#region Properties
        public Graphics DC
        {
            get { return dc; }
        }

        internal GameObject SelectedObject { get { return selectedWorker; } set { selectedWorker = value; } }
        #endregion

        public GameForm()
        {
            InitializeComponent();
            //initialize a new thread to run the Gameloop

            //initialize the Gameworld
            displayRectangle = new Rectangle(0, 0, 1300, 900);


        }
        private void Form1_Load_1(object sender, EventArgs e)
        {
            if (dc == null)
            {
                dc = CreateGraphics();
            }
            //gm = GameWorld.Instance;
            SetupUi();
            gm = new GameWorld(CreateGraphics(), displayRectangle);
            //initialize the game loop
            initLoop();
        }
        private void initLoop()
        {
            Thread looperThread = new Thread(gamelooper);
            looperThread.IsBackground = true;
            looperThread.Start();
        }
        private void gamelooper()
        {
            ///<remarks>CurrentThread = looperThread</remarks>
            while (runGame)
            {
                //MessageBox.Show("Hello from Loop Thread");
                //Thread.Sleep(5);
                gm.Gameloop();
                Cursormovement();
            }
        }
        private void SetupUi()
        {
            button1.Text = "Show text";
            button2.Text = "Buy Worker";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Thread t1 = new Thread(HelloFromTheOhterSide);
            t1.Start();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //buy worker Button

            Worker worker = (new Worker(new System.Numerics.Vector2(300, 200), @"Images\worker_test..png", 0.2f, "john"));
            GameWorld.AddGameObject.Add(worker);
            
        }

        private void label1_Click(object sender, EventArgs e)
        {
            label1.Text = "Whaa";
        }
      
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void HelloFromTheOhterSide()
        {
            //test code
            label1.Invoke((MethodInvoker)delegate { label1.Text = "Get out off here "; });
        }
        public void Cursormovement()
        {
            label1.Invoke((MethodInvoker)delegate { label1.Text = Cursor.Position.X.ToString(); });
            label2.Invoke((MethodInvoker)delegate { label2.Text = Cursor.Position.Y.ToString(); });

        }
        private void GameForm_MouseClick(object sender, MouseEventArgs e)
        {
            if (selectedWorker != null)
            {
                label3.Invoke((MethodInvoker)delegate { label3.Text = selectedWorker.ObjectName; });
            }
            else
            {
                label3.Invoke((MethodInvoker)delegate { label3.Text = GameForm.MousePosition.X.ToString(); });
            }

            label4.Invoke((MethodInvoker)delegate { label4.Text = GameForm.MousePosition.Y.ToString(); });

            string showString = "X" + Cursor.Position.X + " : " + "Y" + Cursor.Position.Y;
            //MessageBox.Show(showString);
            // If a worker is selected check if the new mouse click is on a building.
            if (selectedWorker != null)
            {
                if (selectedWorker is Worker)
                {
                    foreach (GameObject item in gm.GameObjectList)
                    {
                        if (item.CheckCords(Cursor.Position.X, Cursor.Position.Y))
                        {
                            if (item is Mine || item is Bank)
                            {
                                destinationObject = item;
                                HandleWorker(selectedWorker as Worker);
                                // MessageBox.Show(selectedWorker.ObjectName + " go to" + destinationObject.ObjectName);
                                break;
                            }

                        }
                        else if (item is Worker)
                        {
                            break;
                        }
                        else
                        {
                            HandleWorker(selectedWorker as Worker, Cursor.Position.X, Cursor.Position.Y);
                            break;
                        }
                    }
                }
            }
            foreach (GameObject item in gm.GameObjectList)
            {
                if (item.CheckCords(Cursor.Position.X, Cursor.Position.Y))
                {
                    if (item is Worker)
                    {
                        selectedWorker = item;
                        //MessageBox.Show(selectedObject.ObjectName);
                    }
                    else
                    {
                        selectedObject = item;
                    }

                }
            }
        }

        private void HandleWorker(Worker worker)
        {
            worker.CurrentBuilding = destinationObject as Mine;
            worker.Working = true;
            worker.Destination = new System.Numerics.Vector2(destinationObject.Position.X, destinationObject.Position.Y);
            selectedWorker = null;
            destinationObject = null;
        }
        private void HandleWorker(Worker worker, float x, float y)
        {
            //Moves worker to location when no building is given
            worker.Destination = new System.Numerics.Vector2(x, y);
            worker.Moving = true;
            //bugged
            SelectedObject = null;

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

    }
}
