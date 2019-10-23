using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL_CODE
{
    class WizardUnit : Unit
    {
        public WizardUnit(int x, int y, string faction)
               : base(x, y, 5, 5, 1, 1, 1, 'W', faction, "Wizard") { }

        public WizardUnit(string values) : base(values) { }

        public override string SaveGame()
        {
            return string.Format(
                $"Wizard, {x}, {y}, {health}, {maxHealth}, {speed}, {attack}, {attackRange}, " + $"{faction}, {symbol}, {nameUnit}, {IsDestroyed}");
        }
    }
}

