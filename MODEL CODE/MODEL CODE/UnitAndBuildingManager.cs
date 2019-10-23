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

        public virtual Target GetClosestTarget(Unit unit, string[] ignoreFactions, bool includeUnits = true, bool includeBuildings = true)
        {
            double closestDistance = int.MaxValue;
            Target closestTarget = null;
            //the below code creates a list of possible targets
            List<Target> targets = GetPossibleTargets(ignoreFactions, includeUnits, includeBuildings);

            //no need to check if we are attacking units or buildings of our own faction < NO FRIENDLY FIRE
            //since we now only have a list of targets from other factions
            foreach (Target target in targets)
            {
                if (target.IsDestroyed)
                {
                    continue;
                }

                double distance = unit.GetDistance(target);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = target;
                }
            }

            return closestTarget;
        }

        public List<Target> GetTargetsInArea(Target closestTarget, string[] ignoreFactions, bool includeUnits = true, bool includeBuildings = true)
        {

            List<Target> targets = new List<Target>();
            targets.Add(closestTarget);

            List<Target> possibleTargets = GetPossibleTargets(ignoreFactions, includeUnits, includeBuildings);

            foreach (Target target in possibleTargets)
            {
                //skip target if it is destroyed
                if (target.IsDestroyed || target == closestTarget)
                {
                    continue;
                }
                //skip target if it falls out of x range
                if (target.X < closestTarget.X - 1 || target.X > closestTarget.X + 1)
                {
                    continue;
                }
                //skip target if it falls out of y range
                if (target.Y < closestTarget.Y - 1 || target.Y > closestTarget.Y + 1)
                {
                    continue;
                }
                //if we're including units and this target is a unit
                if (target is Unit && includeUnits)
                {
                    targets.Add(target);
                }
                //if we're including buildings and this target is a building
                if (target is Building && includeBuildings)
                {
                    targets.Add(target);
                }
            }
            return targets;
        }
        //if target is a unit, bool include the unit, include the building if true. else if false, ignore them
        private List<Target> GetPossibleTargets(string[] ignoreFactions, bool includeUnits, bool includeBuildings)
        {
            //create a list of all the targets not in ignored factions
            List<Target> targets = new List<Target>();

            foreach (string faction in factions)
            {
                //targets in the ignore list are skipped
                if (Array.IndexOf(ignoreFactions, faction) >= 0)
                { //in ignoreFactions, it will return 'faction'
                    //e.g. cont. x = [1, 5, 6, 10, 3]........Array.IndexOf(x, 6) --->  returns 2
                    //therefore, ignoreFac means if value is <= 0, ignore it as it is not equal to array
                    continue;
                }
                //include units in list if includeUnits is set to true
                if (includeUnits)
                {
                    targets.AddRange(units[faction]);
                }
                //include buildings in list if includeUnits is set to true
                if (includeBuildings)
                {
                    targets.AddRange(buildings[faction]);
                }
            }
            return targets; //return the ones that are actually targets
        }

        public List<Unit> GetUnitsByFaction(string faction)
        {
            return units[faction]; //find all units in a specific faction
        }

        public List<Building> GetBuildingsByFaction(string faction)
        {
            return buildings[faction]; //find all buidlings in a specific faction
        }

        public int GetUnitsAliveCountByFaction(string faction)
        {
            int count = 0;
            foreach (Unit unit in units[faction])
            {
                if (!unit.IsDestroyed)
                {
                    count++;
                }
            }
            return count; //kill count units
        }

        public int GetBuildingsAliveCountByFaction(string faction)
        {
            int count = 0;
            foreach (Building building in buildings[faction])
            {
                if (!building.IsDestroyed)
                {
                    count++;
                }
            }
            return count; //kill count buildings
        }

        public bool AllUnitsDestroyed(string faction)
        {
            foreach (Unit unit in units[faction])
            {
                if (!unit.IsDestroyed)
                {
                    return false;
                }
            }

            return true;
        }
    }
}

