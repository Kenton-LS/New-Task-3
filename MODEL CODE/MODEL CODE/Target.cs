using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL_CODE //both unit and building inherit from here
{
    class Target //loops through an array of targets, instead of an array of buildings AND units. easier code
    {
        protected int x; //has an x and y
        protected int y; //these values are the ones shared by units and buildings
        protected int health;
        protected int maxHealth;
        protected bool isDestroyed;
        protected string faction;

        int hideChecksBeforeInvisible = 5;
        bool isVisible = true;

        public string Faction //initializations 
        {
            get { return faction; }
        }

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public bool IsDestroyed
        {
            get { return isDestroyed; }
        }
        public bool IsVisible
        {
            get { return isVisible; }
        }

        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        public int MaxHealth
        {
            get { return maxHealth; }
        }

        public double GetDistance(Target to)
        { //used in unit and building manager
            double xDistance = to.X - X;
            double yDistance = to.Y - Y;

            return Math.Sqrt(xDistance * xDistance + yDistance * yDistance);
        }

        public virtual void Destroy()
        {
            isDestroyed = true;
        }

        public void CheckHide()
        { //hide a unit that has already been dead FOR A WHILE now
            if (hideChecksBeforeInvisible == 0)
                return;

            hideChecksBeforeInvisible--;
            isVisible = hideChecksBeforeInvisible > 0;
        }
    }
}

