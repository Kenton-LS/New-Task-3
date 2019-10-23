using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL_CODE
{
    abstract class Unit
    {
        protected int x, y, health, maxHealth, speed, attack, attackRange;
        protected string faction, nameUnit;
        protected char symbol; //Mellee or ranged
        protected bool isAttacking = false; //set to false by default, doesn't need a parameter
        protected bool isDestroyed = false;
        public static Random random = new Random(); //to enable random in all classes

        public Unit(int x, int y, int health, int maxHealth, int speed, int attack, int attackRange, char symbol, string faction, string nameUnit) //CONSTRUCTOR
        {
            this.x = x; //Initialize everything
            this.y = y;
            this.health = health;
            this.maxHealth = health; //NB
            this.speed = speed;
            this.attack = attack;
            this.attackRange = attackRange;
            this.faction = faction;
            this.nameUnit = nameUnit;
            this.symbol = symbol;
        }

        public Unit(string values)
        {
            string[] parameters = values.Split(','); //split strings into array of parameters

            x = int.Parse(parameters[1]);

            y = int.Parse(parameters[2]); //pass everything to int

            health = int.Parse(parameters[3]);

            maxHealth = int.Parse(parameters[4]);

            speed = int.Parse(parameters[5]);

            attack = int.Parse(parameters[6]);

            attackRange = int.Parse(parameters[7]);

            faction = parameters[8];

            symbol = parameters[9][0]; //symbol is a char, returns the first character of the symbol 'string'

            nameUnit = parameters[10];

            isDestroyed = parameters[11] == "True" ? true : false; //makes sure are units are still dead during the reload
        }

        public abstract string SaveGame();

        public int X { get { return x; } set { x = value; } }

        public int Y { get { return y; } set { y = value; } }

        public int Health { get { return health; } set { health = value; } }

        public int MaxHealth { get { return maxHealth; } }

        public string Faction { get { return faction; } }

        public char Symbol { get { return symbol; } }

        public bool IsDestroyed { get { return isDestroyed; } }

        public string NameUnit { get { return nameUnit; } }

        ///////////////////
       
        public virtual void Destroy() //death method
        {
            isDestroyed = true;
            isAttacking = false;
            symbol = 'X';
        }

        public virtual void Attack(Unit otherUnit)
        {
            isAttacking = true;
            otherUnit.Health -= attack;

            if(otherUnit.Health <= 0)
            {
                otherUnit.Health = 0;
                otherUnit.Destroy();
            }
        }

        public virtual void AttackBuilding(Building otherBuilding) //new
        {
            isAttacking = true;
            otherBuilding.Health -= attack;

            if (otherBuilding.Health <= 0)
            {
                otherBuilding.Health = 0;
                otherBuilding.Destroy();
            }
        }


        public virtual void Move(Unit closestUnit) //this method has been moved here from the other inherited building classes, and it has been expanded
        {
            int moveX = closestUnit.X - X;

            int moveY = closestUnit.Y - Y;

            if(Math.Abs(moveX) > Math.Abs(moveY))
            {
                x += Math.Sign(moveX);
            }
            else
            {
                y += Math.Sign(moveY);
            }
        }

        public virtual void MoveB(Building closestBuilding) //new
        {
            int moveX = closestBuilding.X - X;

            int moveY = closestBuilding.Y - Y;

            if (Math.Abs(moveX) > Math.Abs(moveY))
            {
                x += Math.Sign(moveX);
            }
            else
            {
                y += Math.Sign(moveY);
            }
        }

        public virtual void Run()
        {
            int direction = random.Next(0, 4); //random movement
            if(direction == 0)
            {
                x += 1; //if random is 0, move x + 1
            }
            else if (direction == 1)
            {
                x -= 1; //if random is 1, move x - 1
            }
            else if (direction == 2)
            {
                y += 1; //if random is 2, move y + 1
            }
            else if (direction == 3)
            {
                y -= 1; //if random is 3, move y - 1
            }
        }

     

        public virtual Unit GetClosestUnit(Unit[] units)
        {
            double closestDistance = int.MaxValue;
            Unit closestUnit = null;

            foreach(Unit otherUnit in units) //avoid attacking own units
            {
                if (otherUnit == this || otherUnit.Faction == faction || otherUnit.IsDestroyed)
                {
                    continue;
                }

                double distance = GetDistance(otherUnit);
                if(distance < closestDistance)
                {
                    closestDistance = distance;
                    closestUnit = otherUnit;
                }
            }
            return closestUnit;
        }
       
        public virtual Building GetClosestBuilding(Building[] buildings) //new
        {
            double closestDistanceBuilding = int.MaxValue;
            Building closestBuilding = null;

            foreach(Building otherBuilding in buildings)
            {
                if(otherBuilding.Faction == faction || otherBuilding.IsDestroyed)
                {
                    continue;
                }

                double distanceBuilding = GetDistanceBuilding(otherBuilding);
                if(distanceBuilding < closestDistanceBuilding)
                {
                    closestDistanceBuilding = distanceBuilding;
                    closestBuilding = otherBuilding;
                }
            }
            return closestBuilding;
        }

        public virtual bool IsInRange(Unit otherUnit) //for calculating the closest unit
        {
            return GetDistance(otherUnit) <= attackRange;
        }

        public virtual bool IsInRangeBuilding(Building otherBuilding) //new
        {
            return GetDistanceBuilding(otherBuilding) <= attackRange;
        }

        protected double GetDistance(Unit otherUnit) //helper method
        {
            double xDistance;
            double yDistance;
            if (otherUnit != null)
            {
                xDistance = otherUnit.X - X; //get x distance
                yDistance = otherUnit.Y - Y; //get y distance
               
            }
            else
            {
                xDistance = 1; //
                yDistance = 1;
            }
            return Math.Sqrt(xDistance * xDistance + yDistance * yDistance); //more efficient than Math.Pow
        }

        protected double GetDistanceBuilding(Building otherBuilding) //new
        {
            double xDistanceB = otherBuilding.X - X;
            double yDistanceB = otherBuilding.Y - Y;
            return Math.Sqrt(xDistanceB * xDistanceB + yDistanceB * yDistanceB);
        }

        public override string ToString() //Rather place ToString here instead of dupicating it in other classes (Ranged & Melee)
        {
            return nameUnit + "\n" +
                   "X: " + x + " Y: " + y + "\n" +
                   "HP:  " + health + " / " + maxHealth + "\n" +
                   "FACTION:  " + faction[0] + "\nSYMBOL:  " + symbol + "\n";
        }
    }
}
