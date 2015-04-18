using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    class Attack
    {
        public int attackType;
        public int damage;

        public Attack(int attackType, int damage)
        {
            this.attackType = attackType;
            this.damage = damage;
        }
    }
}
