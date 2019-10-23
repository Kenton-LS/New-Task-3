using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MODEL_CODE
{
    class GameEngine 
    {
        public static Random random = new Random();

        Map map; //filed for map
        UnitAndBuildingManager manager;

        bool gameOver = false; //set to true if game ends
        string winning = ""; //winning faction string
        int round = 0;
        string[] factions = { A_TEAM, B_TEAM, WIZARDS };

        const string A_TEAM = "A-Team";
        const string B_TEAM = "B-Team";
        const string WIZARDS = "Wizards";

        //Share a single Random object across all classes.
        //eg. GameEngine.random.Next(5);

        int loadedMapWidth; //for loading map width;
        int loadedMapHeight; //for loading map height;


        const string UNITS_FILENAME = "units.txt";
        const string BUIDLINGS_FILENAME = "buildings.txt";
        const string ROUND_FILENAME = "rounds.txt";


        //////////


        public GameEngine(int width, int height) //pass variables for map class through constructor, 
                                                 //cont. pass the constants in the map class
        {
            Reset(width, height);
            //map = new Map(width, height);
            //manager = new UnitAndBuildingManager();
            //CreateUnitsAndBuildings();
        }

        public bool GameOver
        {
            get { return gameOver; }
        }

        public string Winning
        {
            get { return winning; }
        }

        public int Round
        {
            get { return round; }
        }

        public int RandomNumberOfUnits
        {
            get { return map.Units.Length; }
        }

        public int NumberOfBuildings
        {
            get { return map.Buildings.Length; }
        }

        public string MapDisplay
        {
            get { return map.DisplayMap(); }
        }

        public int LoadedMapWidth
        {
            get { return loadedMapWidth; }
        }

        public int LoadedMapHeight
        {
            get { return loadedMapHeight; }
        }

        public void Reset(int width, int height)
        {
            map = new Map(width, height);
            manager = new UnitAndBuildingManager();

            CreateUnitsAndBuildings();
            map.UpdateMap(manager);

            gameOver = false;
            round = 0;
        }

        public void SaveGame()
        {
            Save(UNITS_FILENAME, manager.Units.ToArray());
            Save(BUIDLINGS_FILENAME, manager.Buildings.ToArray());
            SaveSettings();
        }

        public void LoadGame()
        {
            LoadSettings();

            map = new Map(loadedMapWidth, loadedMapHeight);
            manager = new UnitAndBuildingManager();
            foreach (string faction in factions)
            {
                manager.AddFaction(faction);
            }

            Load(UNITS_FILENAME);
            Load(BUIDLINGS_FILENAME);

            map.UpdateMap(manager);
        }

        private void Load(string filename)
        {
            FileStream inFile = new FileStream(filename, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(inFile);

            string recordIn;
            recordIn = reader.ReadLine();
            while(recordIn != null)
            {
                int length = recordIn.IndexOf(","); //finds first occurrence of a comma
                string firstField = recordIn.Substring(0, length); //from which index you want to copy, and for how long?
                switch (firstField)
                {
                    case "Melee": map.AddUnit(new MeleeUnit(recordIn)); break; //adds the string, which gets chopped up by commas, adds them in
                    case "Ranged": map.AddUnit(new RangedUnit(recordIn)); break;
                    case "Factory": map.AddBuilding(new FactoryBuilding(recordIn)); break;
                    case "Resource": map.AddBuilding(new ResourceBuilding(recordIn)); break;
                    case "Wizard": map.AddUnit(new WizardUnit(recordIn)); break;
                }
                recordIn = reader.ReadLine(); //readline. if it is not null, it will carry on reading until there are no lines left
            }
            reader.Close();
            inFile.Close();
        }

        private void Save(string filename, object[] objects)
        {
            FileStream outFile = new FileStream(filename, FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(outFile);
            foreach (object o in objects)
            {
                if (o is Unit) //if object is a unit
                {
                    Unit unit = (Unit)o;
                    writer.WriteLine(unit.SaveGame());
                }
                else if (o is Building) //if object is a building
                {
                    Building unit = (Building)o;
                    writer.WriteLine(unit.SaveGame());
                }
            }
            writer.Close();
            outFile.Close();
        }

        private void SaveSettings()
        {
            FileStream outFile = new FileStream(ROUND_FILENAME, FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(outFile);
            writer.WriteLine(round);
            writer.WriteLine(map.width);
            writer.WriteLine(map.height);
            writer.Close();
            outFile.Close();
        }

        private void LoadSettings()
        {
            FileStream inFile = new FileStream(ROUND_FILENAME, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(inFile);
            round = int.Parse(reader.ReadLine());
            loadedMapWidth = int.Parse(reader.ReadLine());
            loadedMapHeight = int.Parse(reader.ReadLine());
            reader.Close();
            inFile.Close();
        }

        ///

        public void GameLoop()
        {
            UpdateUnits(); //break it into sizeable chunks
            UpdateBuildings();
            map.UpdateMap(manager);
            round++;
        }

        /// 
        /// 
        /// 
        ///

        void UpdateUnits()
        {

            foreach (string faction in factions)
            {

                string[] ignoreFactions = GetFactionsToIgnore(faction);

                foreach (Unit unit in manager.Units)
                {
                    //ignore this unit if it is destroyed
                    if (unit.IsDestroyed)
                    {
                        unit.CheckHide();
                        continue;
                    }

                    //unit has to wait a number of rounds equal to speed before it can take action
                    if (round % unit.Speed != 0)
                    {
                        continue;
                    }

                    Target closestTarget = manager.GetClosestTarget(unit, ignoreFactions);

                    if (faction == WIZARDS)
                    {
                        //wizards only target units
                        closestTarget = manager.GetClosestTarget(unit, ignoreFactions, true, false);
                    }

                    if (closestTarget == null)
                    {
                        //if a unit has no target it means the game has ended
                        gameOver = true;
                        winning = unit.Faction;

                        map.UpdateMap(manager);
                        return;
                    }

                    double healthPercentage = unit.Health / (double)unit.MaxHealth;
                    bool isWizard = unit is WizardUnit;

                    //if not a wizard, run away if health is below 25%
                    if (healthPercentage <= 0.25 && !isWizard)
                    { //if health is 25 percent and not wizard, RUN AWAY
                        unit.Run();

                    }
                    //if a wizard and health is below 50%, run away
                    else if (healthPercentage <= 0.5 && isWizard)
                    { //if health 50 and is wizard, RUN BOY RUUUUUUUUN
                        unit.Run();

                    }
                    //if target is in range and this unit is not a wizard, attack closest target
                    else if (unit.IsInRange(closestTarget) && !isWizard)
                    {
                        if (unit.Attack(closestTarget))
                        {
                            //if killed unit, add to resource pool of all faction resource buildings
                            AddToResourcePoolByFaction(unit.Faction); //pass faction name, adds 1 resource to each building of 
                            //cont. that faction
                        }

                    }
                    //if target is in range and  this unit is a wizard, attack all targets in range
                    else if (unit.IsInRange(closestTarget) && isWizard)
                    { //finds which units fall in wizard's square, so that he knows 
                        //cont. if he can attack them (finds all possible targets, if it is not destroyed and x y cords are gucci
                        //find all units close to the target we are attacking
                        foreach (Target targetInArea in manager.GetTargetsInArea(closestTarget, ignoreFactions, true, false))
                        {
                            unit.Attack(targetInArea);
                        }

                    }
                    //if none of the above just move the unit
                    else
                    {
                        unit.Move(closestTarget); //if nothing else, just move, whilst staying in bounds
                    }

                    StayInBounds(unit, map.width, map.height);
                }
            }
        }

        void UpdateBuildings()
        {
            foreach (string faction in factions)
            {
                //new resources are only considered at the beginning of the next round
                int resources = GetResourcesTotalByFaction(faction);
                int usedResources = 0;

                foreach (Building building in manager.GetBuildingsByFaction(faction))
                {
                    //ignore destroyed buildings
                    if (building.IsDestroyed)
                    {
                        building.CheckHide();
                        continue;
                    }

                    if (building is FactoryBuilding)
                    {
                        FactoryBuilding factoryBuilding = (FactoryBuilding)building;

                        if (factoryBuilding.CanProduce(round) && factoryBuilding.SpawnCost <= resources)
                        {
                            resources -= factoryBuilding.SpawnCost;
                            usedResources += factoryBuilding.SpawnCost;

                            Unit newUnit = factoryBuilding.CreateUnit(round);
                            manager.AddUnit(newUnit);
                        }
                    }
                    else if (building is ResourceBuilding)
                    {
                        ResourceBuilding resourceBuilding = (ResourceBuilding)building;
                        resourceBuilding.IncreaseResourceAmount();
                    }
                }

                //remove used resources from faction's available resource buildings
                UseResourcesFromFaction(faction, usedResources);
            }
        }

        //calculate how much resources a faction has at it's disposal
        int GetResourcesTotalByFaction(string faction)
        {
            int totalResources = 0;

            foreach (Building building in manager.GetBuildingsByFaction(faction))
            {
                //we are interested in resource buildings that have not been destroyed
                if (building is ResourceBuilding && !building.IsDestroyed)
                {
                    ResourceBuilding resourceBuilding = (ResourceBuilding)building;
                    totalResources += resourceBuilding.resourcesGenerated;
                }
            }
            return totalResources;
        }

        void UseResourcesFromFaction(string faction, int usedResources)
        {
            foreach (Building building in manager.GetBuildingsByFaction(faction))
            {
                if (building is ResourceBuilding && !building.IsDestroyed)
                {
                    ResourceBuilding resourceBuilding = (ResourceBuilding)building;

                    //this buildings has not resources left, let's skip it
                    if (resourceBuilding.resourcesGenerated <= 0)
                    {
                        continue;
                    }

                    //determine how many resources can be used from this buildings
                    int resourcesToUse = Math.Min(usedResources, resourceBuilding.resourcesGenerated);

                    //subtract resources from total used by faction
                    usedResources -= resourcesToUse;
                    //subtract resources from actual building
                    resourceBuilding.resourcesGenerated -= resourcesToUse;

                    //if we've subtracted all the resources use by this faction
                    if (usedResources <= 0)
                    {
                        return;
                    }
                }
            }
        }

        void AddToResourcePoolByFaction(string faction)
        { //gets all buildings by faction, if is rss and not destroyed, add 1
            foreach (Building building in manager.GetBuildingsByFaction(faction))
            {
                if (building is ResourceBuilding && !building.IsDestroyed)
                {
                    ResourceBuilding resourceBuilding = (ResourceBuilding)building;
                    resourceBuilding.ResourcePoolRemaining += 1;
                }
            }
        }

        string[] GetFactionsToIgnore(string faction)
        {

            bool canAttackWizards = false;
            if (faction == A_TEAM)
            {
                canAttackWizards = manager.AllUnitsDestroyed(B_TEAM);
            }
            else if (faction == B_TEAM)
            {
                canAttackWizards = manager.AllUnitsDestroyed(A_TEAM);
            }

            string[] ignoreFactions = new string[] { faction, WIZARDS };
            if (faction == WIZARDS || canAttackWizards)
            {
                //wizards ignore only their own faction 
                //and other units stop ignoring wizards if there are no units alive in the other team
                ignoreFactions = new string[] { faction };
            }

            return ignoreFactions;
        }

        public int NumUnits
        {
            get { return manager.Units.Count; }
        }

        public int NumBuildings
        {
            get { return manager.Buildings.Count; }
        }

        public int NumUnitsAlive
        {
            get
            {
                int alive = 0;
                foreach (Unit unit in manager.Units)
                {
                    if (!unit.IsDestroyed)
                    {
                        alive++;
                    }
                }
                return alive;
            }
        }

        public int NumBuildingsAlive
        {
            get
            {
                int alive = 0;
                foreach (Building building in manager.Buildings)
                {
                    if (!building.IsDestroyed)
                    {
                        alive++;
                    }
                }
                return alive;
            }
        }

        public string MapDisplay
        {
            get { return map.GetMapDisplay(); }
        }

        public string GetUnitInfo()
        {
            string unitInfo = "";
            foreach (Unit unit in manager.Units)
            {
                unitInfo += unit + Environment.NewLine;
            }
            return unitInfo;
        }

        public string GetBuildingsInfo()
        {
            string buildingsInfo = "";
            foreach (Building building in manager.Buildings)
            {
                buildingsInfo += building + Environment.NewLine;
            }
            return buildingsInfo;
        }

        public string GetDetails()
        {
            string details = "";

            foreach (string faction in factions)
            {
                details += faction + Environment.NewLine;
                details += "------------------" + Environment.NewLine;
                details += "Units: " +
                    manager.GetUnitsAliveCountByFaction(faction) + "/" +
                    manager.GetUnitsByFaction(faction).Count + Environment.NewLine;
                details += "Buildings: " +
                    manager.GetBuildingsAliveCountByFaction(faction) + "/" +
                    manager.GetBuildingsByFaction(faction).Count + Environment.NewLine;
                details += Environment.NewLine;
            }

            return details;
        }

        /// 
        /// 
        /// 
        /// 

        private void SaveRound()
        {
            FileStream outFile = new FileStream(ROUND_FILENAME, FileMode.Create, FileAccess.Write); //saving the round
            StreamWriter writer = new StreamWriter(outFile);
            writer.WriteLine(round);
            writer.Close();
            outFile.Close();
        }

        private void LoadRound()
        {
            FileStream inFile = new FileStream(ROUND_FILENAME, FileMode.Open, FileAccess.Read); //loading the round
            StreamReader reader = new StreamReader(inFile);
            round = int.Parse(reader.ReadLine());
            reader.Close();
            inFile.Close();
        }
        

        private void MapBoundary(Unit unit, int mapSize)
        {
            if(unit.X < 0)
            {
                unit.X = 0;
            }
            else if(unit.X >= mapSize)
            {
                unit.X = mapSize - 1;
            }

            if(unit.Y < 0)
            {
                unit.Y = 0;
            }
            else if(unit.Y >= mapSize)
            {
                unit.Y = mapSize - 1;
            }
        }

        public string UnitInformation()
        {
            string unitInfo = "";
            foreach (Unit unit in map.Units)
            {
                unitInfo += unit + "\n";
            }
            return unitInfo;
        }

        public string BuildingInformation()
        {
            string buildingInfo = "";
            foreach (Building building in map.Buildings)
            {
                buildingInfo += building + "\n";
            }
            return buildingInfo;
        }


        /// 
        /// 
        /// 
        /// 
        /// 
       

        private void CreateUnitsAndBuildings()
        {
            foreach (string faction in factions)
            {
                manager.AddFaction(faction);

                if (faction != WIZARDS)
                {
                    AddUnits(random.Next(5, 10), faction);
                    AddBuildings(random.Next(10, 15), faction);
                }
                else
                {
                    AddUnits(random.Next(5, 10), faction);
                }
            }
        }

        private void AddUnits(int numUnits, string faction)
        {
            for (int i = 0; i < numUnits; i++)
            {
                int x = random.Next(0, map.width);
                int y = random.Next(0, map.height);

                Unit unit;
                if (faction != WIZARDS)
                {
                    int unitType = random.Next(0, 2);
                    if (unitType == 0)
                    {
                        unit = new MeleeUnit(x, y, faction);
                    }
                    else
                    {
                        unit = new RangedUnit(x, y, faction);
                    }
                }
                else
                {
                    unit = new WizardUnit(x, y, faction);
                }

                manager.AddUnit(unit);
            }
        }

        private void AddBuildings(int numBuildings, string faction)
        {
            for (int i = 0; i < numBuildings; i++)
            {
                int x = random.Next(0, map.width);
                int y = random.Next(0, map.height);
                int buildingType = random.Next(0, 2);

                Building building;
                if (buildingType == 0)
                {
                    building = new FactoryBuilding(x, y, faction, map.height);
                }
                else
                {
                    building = new ResourceBuilding(x, y, faction);
                }

                manager.AddBuilding(building);
            }
        }

        private void StayInBounds(Unit unit, int width, int height) //called using map.width and map.height so that these map 
                                                                    //class variables may be accessable, but not changed Q3.5
        {
            if (unit.X < 0)
            {
                unit.X = 0;
            }
            else if (unit.X >= width)
            {
                unit.X = width - 1;
            }

            if (unit.Y < 0)
            {
                unit.Y = 0;
            }
            else if (unit.Y >= height)
            {
                unit.Y = height - 1;
            }
        }
    }
}
