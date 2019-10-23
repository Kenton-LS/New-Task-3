 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL_CODE
{
    abstract class Building : Target //buildings are targets
    {
        protected char symbol;

        public Building(int x, int y, int health, char symbol, string faction)
        {
            this.x = x; 
            this.y = y;
            this.health = health;
            this.maxHealth = health; //initializing everything
            this.faction = faction;
            this.symbol = symbol;

        }

        public Building() { }

        public char Symbol { get { return symbol; } }

        public abstract string SaveGame(); //replaced the save void instead of abstract ToString(), the method is already virtual

        public override string ToString() //the properties on both the base and inherited classes
        {
            return "X: " + x + " Y: " + y + "\n" +
                   "HP:  " + health + " / " + maxHealth + "\n" +
                   "FACTION:  " + faction + "\nSYMBOL:  " + symbol + "\n";
                  // "RSS GAINED >> " + resourcesGenerated + " / " + resourcePoolRemaining + " << LEFTOVER RSS" + "\n" +
                   //"RSS PER ROUND:  " + resourcesPerRound + "\n";
        }


        public override void Destroy() //death method
        {
            isDestroyed = true;
            symbol = 'X';
        }


    }
}
