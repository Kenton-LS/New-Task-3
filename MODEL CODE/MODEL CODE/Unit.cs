using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL_CODE
{
    abstract class Unit : Target // units are targets
    {
        protected int x, y, health, maxHealth, speed, attack, attackRange;
        protected string nameUnit;
        protected char symbol; //Mellee or ranged
        protected bool isAttacking = false; //set to false by default, doesn't need a parameter
      
        public static Random random = new Random(); //to enable random in all classes

        public Unit(int x, int y, int health, int maxHealth, int speed, int attack, int attackRange, char symbol, string faction, string nameUnit) //CONSTRUCTOR
        {
            this.x = x; //Initialize everything
            this.y = y;

            this.health = health;
            maxHealth = health; //NB
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

        public int Speed { get { return speed; } }

        public char Symbol { get { return symbol; } }

        public string NameUnit { get { return nameUnit; } }

        ///////////////////
       
        public override void Destroy() //death method
        {
            Health = 0; //make certain health is 0
            isDestroyed = true;
            isAttacking = false;
            symbol = 'X';
        }

        public virtual bool Attack(Target target) ////returns true if target was destroyed
        {
            isAttacking = true;
            target.Health -= attack;

            if(target.Health <= 0)
            {
                target.Health = 0;
                target.Destroy();
                return true;
            }
            return false;
        }
        


        public virtual void Move(Target closestTarget) //this method has been moved here from the other inherited building classes, and it has been expanded
        {
            int moveX = closestTarget.X - X;
            int moveY = closestTarget.Y - Y;

            if(Math.Abs(moveX) > Math.Abs(moveY))
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
            else
            {
                y -= 1; //if random is 3, move y - 1
            }
        }

     
        public virtual bool IsInRange(Target target) //for calculating the closest unit
        {
            return GetDistance(target) <= attackRange;
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
