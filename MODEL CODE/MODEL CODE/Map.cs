using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MODEL_CODE
{
    class Map
    {
        public const int mapSize = 20;
        public int mapX = frm2.mapX;
        public int mapY = frm2.mapY;
        public int randomNumberOfUnits;
        public int numberOfBuildings;

        Unit[] units;
        Building[] buildings;

        string[,] map; //map stored in a string to hsve 3 characters represent a block
        string[] factions = { "BLUE", "RED", "WHITE" };


        public Map(int randomNumberOfUnits, int numberOfBuildings) //adds number of buildings to units
        {
            this.randomNumberOfUnits = randomNumberOfUnits; //pass the number of units
            this.numberOfBuildings = numberOfBuildings;
            Reset(); //sets size of map to a size constant, calls initialize units and buildings and updates map
        }

        public Unit[] Units
        {
            get { return units; }
        }

        public Building[] Buildings
        {
            get { return buildings; }
        }

        public int Size
        {
            get { return mapSize; }
        }

        public string DisplayMap() //building and returning a string
        {
            string mapString = ""; //building a string and returning it
            for(int y = 0; y < mapSize; y++)
            {
                for(int x = 0; x < mapSize; x++)
                {
                    mapString += map[x, y];
                }
                mapString += "\n";
            }
            return mapString;
        }

        public void Reset()
        {
            map = new string[mapSize, mapSize]; //initialize map
            InitializeUnits(); //calling methods
            InitializeBuildings();
            UpdateDisplay();
            
        }

        public void Clear()
        {
            units = new Unit[0]; //when loading the map, clear it (helps the game engine)
            buildings = new Building[0];
        }

        ///
        ///
        public void AddUnit(Unit unit) //without array.resize
        {
            Unit[] resizeUnitArray = new Unit[units.Length + 1]; //make an array with 1 extra element

            for(int i = 0; i < units.Length; i++)
            {
                resizeUnitArray[i] = units[i];
            }
            resizeUnitArray[resizeUnitArray.Length - 1] = unit;
            units = resizeUnitArray;
        }

        public void AddBuilding(Building building) //make an array with 1 extra element
        {
            Array.Resize(ref buildings, buildings.Length + 1);
            buildings[buildings.Length - 1] = building;
        }

        /// 
        ///

        public void UpdateDisplay() //clears the map, sets everything to dots, nothing much changed here
        {
            for(int y = 0; y < mapSize; y++)
            {
                for(int x = 0; x < mapSize; x++)
                {
                    map[x, y] = "   "; //changed from dots to blank
                }
            }

            foreach(Unit unit in units)
            {
                map[unit.X, unit.Y] = unit.Symbol + "/" + unit.Faction[0];
            }

            foreach(Building building in buildings)
            {
                map[building.X, building.Y] = building.Symbol + "/" + building.Faction[0];
            }
        }

        ///
      
        public void InitializeUnits()
        {
            units = new Unit[randomNumberOfUnits];

            for(int i = 0;i < units.Length; i++) //for assigning random positions
            {
                int x = GameEngine.random.Next(0, mapSize); //generate x and y values
                int y = GameEngine.random.Next(0, mapSize);
                int factionIndex = GameEngine.random.Next(0, 2); //decides blue or red team
                int nameIndex = GameEngine.random.Next(0, 2); //CHANGED from 5 to 2
                int unitType = GameEngine.random.Next(0, 3); //decides ranged or melee

                while(map[x, y] != null)
                {
                    x = GameEngine.random.Next(0, mapSize);
                    y = GameEngine.random.Next(0, mapSize); //makes sure map is unoccupied
                }

                if(unitType == 0)
                {
                    units[i] = new MeleeUnit(x, y, factions[factionIndex] /*nameUnits1[nameIndex]*/);
                }
                else if(unitType == 1)
                {
                    units[i] = new RangedUnit(x, y, factions[factionIndex] /*nameUnits2[nameIndex]*/);
                }
                else
                {
                    units[i] = new WizardUnit(x, y, factions[2]);
                }
                map[x, y] = units[i].Faction[0] + "/" + units[i].Symbol; //returns the team and the unit type
            }
        }
   

        public void InitializeBuildings()
        {
            buildings = new Building[numberOfBuildings];

            for (int i = 0; i < buildings.Length; i++)
            {
                int x = GameEngine.random.Next(0, mapSize);
                int y = GameEngine.random.Next(0, mapSize);
                int factionIndex = GameEngine.random.Next(0, 2);
                int buildingType = GameEngine.random.Next(0, 2);

                while (map[x, y] != null)
                {
                    x = GameEngine.random.Next(0, mapSize);
                    y = GameEngine.random.Next(0, mapSize);
                }

                if (buildingType == 0)
                {
                    buildings[i] = new ResourceBuilding(x, y, factions[factionIndex]);
                }
                else
                {
                    buildings[i] = new FactoryBuilding(x, y, factions[factionIndex]);  //not many changes needed in this section
                }
                
                map[x, y] = buildings[i].Faction[0] + "/" + buildings[i].Symbol;
            }
        }

    }
}
