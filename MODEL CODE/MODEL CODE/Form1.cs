using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
//using Microsoft.VisualBasic;

namespace MODEL_CODE
{
    public partial class frm1 : Form
    { 

        GameEngine engine;
        Timer timer;
        Condition condition = Condition.PAUSED;


        public frm1()
        {
            InitializeComponent();
            engine = new GameEngine(); //new game engine object
            UpdateInterface();

            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += RunningGame; //runs through every time the timer ticks, runs loop of the game
        }

        private void RunningGame(object sender, EventArgs e) //no changes
        {
            engine.GameLoop();
            UpdateInterface();

            if (engine.GameOver) //checks targets left. if 0 , then game ends and displays winner text
            {
                timer.Stop();
                UpdateInterface();
                lblMap.Text = engine.Winning + " IS VICTORIOUS\n" + lblMap.Text;
                condition = Condition.ENDED;
                btnStart.Text = "RESTART";
            }

      
        }

        private void UpdateInterface() //set unit display and update the round, as well as the UI overall
        {
            lblMap.Text = engine.MapDisplay;
            rtbUnitInfo.Text = engine.UnitInformation();
            rtbUnitInfo2.Text = engine.BuildingInformation();
            lblUnits.Text = "UNITS:  (" + engine.RandomNumberOfUnits + ")";
            lblBuildings.Text = "BUILDINGS:  (" + engine.NumberOfBuildings + ")";
            lblRound.Text = "ROUND: " + engine.Round;
        }

        /// 

        private void frm1_Load(object sender, EventArgs e)
        {
         
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (condition == Condition.RUNNING)
            {
                timer.Stop();
                condition = Condition.PAUSED;
                btnStart.Text = "START";
            }
            else
            {
                if (condition == Condition.ENDED) //resets all before running 
                {
                    engine.Reset();
                }
                timer.Start(); //runs game again
                condition = Condition.RUNNING;
                btnStart.Text = "PAUSE";
            }
        }

        private void btnSave_Click(object sender, EventArgs e) ////////////new
        {
            engine.SaveGame2();
            lblMap.Text = "LOADING 100%\n" + engine.MapDisplay;

            
        }

        private void btnRead_Click(object sender, EventArgs e)
        {

        }

        private void lblMap_Click(object sender, EventArgs e)
        {

        }
    }
    public enum Condition
    {
        RUNNING, PAUSED, ENDED
    }
}
