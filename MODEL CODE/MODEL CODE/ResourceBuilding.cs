  using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MODEL_CODE
{
    class ResourceBuilding : Building
    {
        public enum ResourceType
        {
            TWIGS,
            GRASS,
            ROCKS,
            LOGS
        }

        private string DetermineResourceType()
        {
            return new string[] { "TWIGS", "GRASS", "ROCKS", "LOGS"}[(int)resourceType];
        }

        private ResourceType resourceType;
        public int resourcesGenerated;
        private int resourcesPerRound;
        private int resourcePoolRemaining;
        private int spawnPoint; //for resourceBuildings spawning units

        public ResourceBuilding(int x, int y, string faction) : base(x, y, 15, /*15,*/ '$', faction/*, "RESOURCE BUILDING", 0, 250, 25, "", 0, 0*/)
        {
            resourceType = (ResourceType)GameEngine.random.Next(0, 4); //pass the position and faction to make it easier for map class to read
            resourcesGenerated = 0;
            resourcesPerRound = GameEngine.random.Next(1, 6);
            resourcePoolRemaining = GameEngine.random.Next(100, 200);

            if (y >= Map.mapSize - 1)
            {
                spawnPoint = y - 1;
            }
            else
            {
                spawnPoint = y + 1;
            }
        }

        public ResourceBuilding(string values)
        {
            string[] parameters = values.Split(','); //split strings into array of parameters

            x = int.Parse(parameters[1]);

            y = int.Parse(parameters[2]); //pass everything to int

            health = int.Parse(parameters[3]);

            maxHealth = int.Parse(parameters[4]);

            resourceType = (ResourceType)int.Parse(parameters[5]); //parse to int THEN resourceType

            resourcesPerRound = int.Parse(parameters[6]);

            resourcePoolRemaining = int.Parse(parameters[7]);

            faction = parameters[9];

            symbol = parameters[10][0]; //symbol is a char, returns the first character of the symbol 'string'

            isDestroyed = parameters[11] == "True" ? true : false; //makes sure are units are still dead during the reload
        }

        /////////////////////////
        
        public override void Destroy() //death method to change unit symbol and true the death boolean
        {
            isDestroyed = true;
            symbol = 'X';
        }

        public void IncreaseResourceAmount()
        {
            if (isDestroyed == true)
                return;

            if(resourcePoolRemaining > 0)
            {
                int totalResources = Math.Min(resourcePoolRemaining, resourcesPerRound); //avoids getting a negative pool (always has to be more than ( > ) the pool)
                resourcesGenerated += totalResources;
                resourcePoolRemaining -= totalResources;
            }

            if(resourcesGenerated == 50) //when reaching 50 of a resource, it is consumed in exchange for creating a unit
            {
                CreateResourceUnit();
                resourcesGenerated -= 50;
            }
        }

        public Unit CreateResourceUnit() //Resource Building makes a unit at 100 rss
        {
            Unit unit;
            int decider = GameEngine.random.Next(0, 2);
            if (decider == 0)
            {
                unit = new MeleeUnit(x, spawnPoint, faction); //moved here from map and program classes, efficiency
            }
            else
            {
                unit = new RangedUnit(x, spawnPoint, faction);
            }
            return unit;
        }

        public override string ToString() //data to dispay in the rich text box
        {
            return "RESOURCE (" + symbol + "/" + faction[0] + ")" + "\n" +
                    "X: " + x + "Y: " + y + "\n" +
                    "HP: " + health + " / " + maxHealth + "\n" +
                    "$ -> " + resourceType + " :    " + resourcesGenerated + "/" +  resourcePoolRemaining + "\n" +
                    "RSS Per Round: " + resourcesPerRound + "\n";
        }

        public override string SaveGame() //everything converted into a string, separated by commas
        {
            return string.Format(
                $"RESOURCE, {x}, {y}, {health} / {maxHealth}, $ -> {(int)resourceType}" + //using $ shortcuts for x, y, etc
                $"{resourcesPerRound}, {resourcesGenerated} / {resourcePoolRemaining}" +
                $"{faction}, {symbol}, {isDestroyed}");
        }
    }
}
