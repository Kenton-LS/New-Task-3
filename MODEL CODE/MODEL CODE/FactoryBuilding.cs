using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MODEL_CODE
{
    class FactoryBuilding : Building
    {
        ResourceBuilding rb;
        enum FactoryType //spawning specific types of factories
        {
            MELEE,
            RANGED
        }

        private FactoryType factoryType;
        private int productionSpeed;
        private int spawnPoint; //we already have the x, we just need the y for the factory

        public FactoryBuilding(int x, int y, string faction) : base(x, y, 10, /*10,*/ 'F', faction/*, "FACTORY BUILDING", 0, 0, 0, "", 6, 0*/)
        {
            if (y >= Map.mapSize - 1)
            {
                spawnPoint = y - 1;
            }
            else
            {
                spawnPoint = y + 1;
            }
            factoryType = (FactoryType)GameEngine.random.Next(0, 2);
            productionSpeed = GameEngine.random.Next(3, 7);
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

        public Unit CreateUnit() //declare unit variable
        {
    
            Unit unit;

           /* if (rb.resourcesGenerated == 50)
            {
                rb.resourcesGenerated -= 50;
            }*/

            if (factoryType == FactoryType.MELEE)
            {
                unit = new MeleeUnit(x, spawnPoint, faction); //moved here from map and program classes, efficiency
            }
            else
            {
                unit = new RangedUnit(x, spawnPoint, faction);
            }
            return unit;
        }

       /* public Unit CreateUnit() //declare unit variable //ATTEMPTED FACTORY RESOURCE SPAWN
        {
            Unit unit;
            if (rb.resourcesGenerated == 50)
            {
                if (factoryType == FactoryType.MELEE)
                {
                    unit = new MeleeUnit(x, spawnPoint, faction); //moved here from map and program classes, efficiency
                }
                else
                {
                    unit = new RangedUnit(x, spawnPoint, faction);
                }
                return unit;
            }
            return rb.resourcesGenerated;
        }
        */
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
