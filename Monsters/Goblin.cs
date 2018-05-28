using Rogue.Core;
using RogueSharp.DiceNotation;

namespace Rogue.Monsters
{
    public class Goblin : Monster
    {
        // Creates a new Goblin of specific map level
        public static Goblin Create(int level)
        {

            int health = Dice.Roll("2D6");
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
                Symbol = 'G',
            };
        }
    }
}
