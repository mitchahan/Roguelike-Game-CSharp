using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rogue.Core;
using RLNET;
using RogueSharp;
using RogueSharp.DiceNotation;
using Rogue.Equipment;

namespace Rogue.Monsters
{
    public class Kobold : Monster
    {
        public static Kobold Create(int level)
        {
            int health = Dice.Roll("2D5");
            //HandEquipment hand = HandEquipment.Dagger(); // For Testing
            return new Kobold
            {
                Attack = Dice.Roll("1D3") + level / 3,
                AttackChance = Dice.Roll("25D3"),
                Awareness = 10,
                Color = Colors.KoboldColor,
                Defense = Dice.Roll("1D3") + level / 3,
                DefenseChance = Dice.Roll("10D4"),
                Gold = Dice.Roll("5D5"),
                Health = health,
                MaxHealth = health * level,
                Name = "Kobold",
                Speed = 14,
                Symbol = 'k',
            };
        }
    }
}
