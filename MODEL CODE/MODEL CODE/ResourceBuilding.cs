  using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MODEL_CODE
{
    public enum ResourceType
    {
        TWIGS,
        GRASS,
        ROCKS,
        LOGS
    }
    class ResourceBuilding : Building
    {
        private string DetermineResourceType()
        {
            return new string[] { "TWIGS", "GRASS", "ROCKS", "LOGS"}[(int)resourceType];
        }

        private ResourceType resourceType;
        public int resourcesGenerated;
        private int resourcesPerRound;
        private int resourcePoolRemaining;

        public ResourceBuilding(int x, int y, string faction) : base(x, y, 15, '$', faction)
        {
            resourceType = (ResourceType)GameEngine.random.Next(0, 4); //pass the position and faction to make it easier for map class to read
            resourcesGenerated = 0;
            resourcesPerRound = GameEngine.random.Next(1, 6);
            resourcePoolRemaining = GameEngine.random.Next(100, 200);
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
        
        public int ResourcesGenerated
        {
            get { return resourcesGenerated; }
            set { resourcesGenerated = value; }
        }

        public int ResourcePoolRemaining
        {
            get { return resourcePoolRemaining; }
            set { resourcePoolRemaining = value; }
        }

        public override void Destroy()//death method to change unit symbol and true the death boolean
        {
            isDestroyed = true;
            resourcesGenerated = 0;
            symbol = '_';
        }



        public void IncreaseResourceAmount()
        {
            if (isDestroyed)
                return;

            if(resourcePoolRemaining > 0)
            {
                int totalResources = Math.Min(resourcePoolRemaining, resourcesPerRound); //avoids getting a negative pool (always has to be more than ( > ) the pool)
                resourcesGenerated += totalResources;
                resourcePoolRemaining -= totalResources;
            }
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
//updated