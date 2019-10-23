using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MODEL_CODE
{
    class RangedUnit : Unit
    {
        public RangedUnit(int x, int y, /*int health, int maxHealth, int speed, int attack, int attackRange, char symbol,*/ string faction/*, string nameUnit*/)
              : base(x, y, 5, 5, 1, 2, 3, '?', faction, "Ranged") { } //use the base keyword

        public RangedUnit(string values) : base(values) { } //shortened due to unit class having these fused


        public override string SaveGame()
        {
            return string.Format(
                $"Ranged, {x}, {y}, {health}, {maxHealth}, {speed}, {attack}, {attackRange}, " + $"{faction}, {symbol}, {nameUnit}, {IsDestroyed}");
        }
    
    }
}