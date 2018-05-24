using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Equipment
{
    public class HandEquipment : Core.Equipment
    {
        public static HandEquipment None()
        {
            return new HandEquipment { Name = "nothing" };
        }

        public static HandEquipment Dagger()
        {
            return new HandEquipment
            {
                Attack = 1,
                AttackChance = 10,
                Name = "Dagger",
                Speed = -2
            };
        }

        public static HandEquipment Sword()
        {
            return new HandEquipment
            {
                Attack = 1,
                AttackChance = 20,
                Name = "Sword"
            };
        }

        public static HandEquipment Axe()
        {
            return new HandEquipment
            {
                Attack = 2,
                AttackChance = 15,
                Name = "Axe",
                Speed = 1
            };
        }

        public static HandEquipment TwoHandedSword()
        {
            return new HandEquipment
            {
                Attack = 3,
                AttackChance = 30,
                Name = "2H Sword",
                Speed = 3
            };
        }
    }
}
