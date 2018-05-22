using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rogue.Core;
using RLNET;
using RogueSharp;
using RogueSharp.DiceNotation;

namespace Rogue.Monsters
{
    public class Goblin : Monster
    {
        public static Goblin Create(int level)
        {
            int health = Dice.Roll("2D5");
            return new Goblin
            {
                Attack = Dice.Roll("1D4") + level / 3,
                AttackChance = Dice.Roll("25D3"),
                Awareness = 14,
                Color = Colors.GoblinColor,
                Defense = Dice.Roll("1D4") + level / 3,
                DefenseChance = Dice.Roll("10D4"),
                Gold = Dice.Roll("5D6"),
                Health = health,
                MaxHealth = health * level + level,
                Name = "Goblin",
                Speed = 14,
                Symbol = 'G'
            };
        }
    }
}
