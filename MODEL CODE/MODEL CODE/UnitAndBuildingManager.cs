using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL_CODE
{
    class UnitAndBuildingManager //finding and managing the units and buildings in a separate class, 
    {
        //helps the map to serve one purpose (this manages code, map displays it)

        //dictionary that stores a list of units using their faction as a key
        Dictionary<string, List<Unit>> units;

        //dictionary that stores a list of buildings using their faction as a key
        Dictionary<string, List<Building>> buildings;

        List<string> factions = new List<string>();

        public UnitAndBuildingManager()
        { //sets units to a new dictionary of string
            units = new Dictionary<string, List<Unit>>();
            buildings = new Dictionary<string, List<Building>>();

            foreach (string faction in factions)
            { //stores the actual value
                units[faction] = new List<Unit>();
                buildings[faction] = new List<Building>();
            }
        }

        /////////////

        public List<Unit> Units
        {
            get { return GetUnits(); }
        }

        public List<Building> Buildings
        {
            get { return GetBuildings(); }
        }

        /////////////

        public void AddFaction(string faction)
        {//adds each faction to the manager... checks the unit and its faction
            if (factions.Contains(faction))
            {//if it already has the faction, DONT ADD IT AGAIN
                return;
            }
            //units["A Team"] = new List<Units>    <--- units from a faction = list of those units
            factions.Add(faction);
            units[faction] = new List<Unit>();
            buildings[faction] = new List<Building>();
        }

        List<Unit> GetUnits() //adds everything to a list, which can be reduced to 0 if the game restarts, easily manageable 
        {
            List<Unit> allUnits = new List<Unit>();
            foreach (KeyValuePair<string, List<Unit>> factionUnits in units)
            {
                allUnits.AddRange(factionUnits.Value);
            }
            return allUnits;
        }

        List<Building> GetBuildings()
        {
            List<Building> allBuildings = new List<Building>();
            foreach (KeyValuePair<string, List<Building>> factionBuildings in buildings)
            {
                allBuildings.AddRange(factionBuildings.Value);
            }
            return allBuildings;
        }

        public void AddUnit(Unit unit)
        { //in units dictionary, get me the list belonging to that faction
            units[unit.Faction].Add(unit);
        }

        public void AddBuilding(Building building)
        { //in buildings dictionary, get me the list belonging to that fac
            buildings[building.Faction].Add(building);
        }

    }
}
