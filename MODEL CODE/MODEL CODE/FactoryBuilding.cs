using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MODEL_CODE
{
    enum FactoryType //spawning specific types of factories
    {
        MELEE,
        RANGED
    }

    class FactoryBuilding : Building
    {
        private FactoryType factoryType; //using an int or string is also fine
        private int productionSpeed;
        private int spawnPoint; //we already have the x, we just need the y for the factory
        private int spawnCost;
        private int lastProducedRound = 0;

        public FactoryBuilding(int x, int y, string faction, int mapHeight) : base(x, y, 100 /*could be 10*/, 'F', faction)
        {
            if (y >= mapHeight - 1)
            {
                spawnPoint = y - 1;
            }
            else
            {
                spawnPoint = y + 1;
            }
            factoryType = (FactoryType)GameEngine.random.Next(0, 2);
            productionSpeed = GameEngine.random.Next(3, 7);
            spawnCost = GameEngine.random.Next(15, 25); //consume resources to generate a unit
        }

        ////////////////////////////////////////////////////////

        public FactoryBuilding(string values) //for loading
        {
            string[] parameters = values.Split(','); //split strings into array of parameters

            x = int.Parse(parameters[1]);
            y = int.Parse(parameters[2]); //pass everything to int
            health = int.Parse(parameters[3]);
            maxHealth = int.Parse(parameters[4]);
            factoryType = (FactoryType)int.Parse(parameters[5]); //parse to int THEN resourceType
            productionSpeed = int.Parse(parameters[6]);
            spawnPoint = int.Parse(parameters[7]);
            faction = parameters[9];
            symbol = parameters[10][0]; //symbol is a char, returns the first character of the symbol 'string'
            isDestroyed = parameters[11] == "True" ? true : false; //makes sure are units are still dead during the reload
        }

        public override void Destroy()
        {
            isDestroyed = true;
            symbol = 'X';
        }


        public int ProductionSpeed
        {
            get { return productionSpeed; } //expose this for game engine spawn unit method
        }

        public int SpawnCost
        {
            get { return spawnCost; }
        }

        public bool CanProduce(int round) //check to see if a unit may be created
        {
            int roundsSinceProduced = round - lastProducedRound;
            return roundsSinceProduced >= productionSpeed;
        }

        public Unit CreateUnit(int round) //declare unit variable
        {   
            lastProducedRound = round;
            Unit unit;
            if (factoryType == FactoryType.MELEE)
            {
                unit = new MeleeUnit(x, spawnPoint, faction);
            }
            else
            {
                unit = new RangedUnit(x, spawnPoint, faction);
            }
            return unit;
        }

        private string GetFactoryTypeName()
        {
            return new string[] { "MELEE", "RANGED" }[(int)factoryType];
        }

        //////////////////////////////

        public override string SaveGame() //everything converted into a string, separated by commas
        {
            return string.Format(
                $"FACTORY, {x}, {y}, {health} / {maxHealth}, $ -> {(int)factoryType}" + //using $ shortcuts for x, y, etc
                $"PROD SPEED: {productionSpeed}, SPAWN: {spawnPoint}" +
                $"{faction}, {symbol}, {isDestroyed}");
        }

        public override string ToString() //data to dispay in the rich text box
        {
            return "FACTORY (" + symbol + "/" + faction[0] + ")" + "\n" +
                    "X: " + x + "Y: " + y + "\n" +
                    "HP: " + health + " / " + maxHealth + "\n" +
                    "$ -> " + factoryType + " :    " + "PROD SPEED: " + productionSpeed  + "\n" +
                    "SPAWN:  " + spawnPoint + "\n";
        }
    }
}
