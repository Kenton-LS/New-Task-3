 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL_CODE
{
    abstract class Building
    {
        protected int x, y, health, maxHealth;
        protected string faction;
        protected char symbol;
        protected bool isDestroyed = false;


        public Building(int x, int y, int health, /*int maxHealth, */char symbol, string faction/*, 
                        string resourceType, int resourcesGenerated, int resourcePoolRemaining, int resourcesPerRound,
                        string factoryUnitType, int productionSpeed, int spawnPoint*/)
        {
            this.x = x; 
            this.y = y;
            this.health = health;
            this.maxHealth = health; //initializing everything
            this.faction = faction;
            this.symbol = symbol;

        }

        public Building() { }

        public int X { get { return x; } } //other classess are supposed to change the variable, ONLY THEN do you add a 'set'for exposing.

        public int Y { get { return y; } } //removed the 'abstract' properties

        public int Health { get { return health; } set { health = value; } }

        public char Symbol { get { return symbol; } }

        public string Faction { get { return faction; } }

       // public abstract void Destroy(); //moved up here, removed bool

        public abstract string SaveGame(); //replaced the save void instead of abstract ToString(), the method is already virtual

        public bool IsDestroyed { get { return isDestroyed; } }
        ///

        public override string ToString() //the properties on both the base and inherited classes
        {
            return "X: " + x + " Y: " + y + "\n" +
                   "HP:  " + health + " / " + maxHealth + "\n" +
                   "FACTION:  " + faction + "\nSYMBOL:  " + symbol + "\n";
                  // "RSS GAINED >> " + resourcesGenerated + " / " + resourcePoolRemaining + " << LEFTOVER RSS" + "\n" +
                   //"RSS PER ROUND:  " + resourcesPerRound + "\n";
        }

        public virtual void Destroy() //death method
        {
            isDestroyed = true;
            symbol = 'X';
        }


    }
}
