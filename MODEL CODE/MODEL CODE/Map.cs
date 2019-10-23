using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE6112___Task3
{
    class Map
    {
        public readonly int width = 20; //readonly constants for the map class custom size
        public readonly int height = 20; //readonlies can only be assigned here or in the constructor, 
        //these MUST be passed into the constructor

        string[,] map; //new string

        public Map(int width, int height)
        {

            this.width = width;
            this.height = height;
            map = new string[width, height]; //creates representative array string
        }

        public void UpdateMap(UnitAndBuildingManager manager) //manages all units and buildings
        {
            for (int y = 0; y < height; y++)
            { //height for y, width for x. map SIZE has been replaced
                for (int x = 0; x < width; x++)
                {
                    map[x, y] = "   "; //if the unit / building is visible, draw it. Cleans map of dead units
                }
            }

            foreach (Unit unit in manager.Units)
            { //each of these is singular purpose. One for unit, one for building
                if (unit.IsVisible)
                {
                    map[unit.X, unit.Y] = unit.Symbol + "|" + unit.Faction[0];
                }
            }

            foreach (Building building in manager.Buildings)
            {
                if (building.IsVisible)
                {
                    map[building.X, building.Y] = building.Symbol + "|" + building.Faction[0];
                }
            }
        }

        public string GetMapDisplay() //puts string array into oe single string, passes it back to the form
        {
            string mapString = "";
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    mapString += map[x, y];
                }
                mapString += "\n";
            }
            return mapString;
        }
    }
}
