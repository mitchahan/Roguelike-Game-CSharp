using Rogue.Core;
using RogueSharp.DiceNotation;

namespace Rogue.Monsters
{
    public class Kobold : Monster
    {
        // Creates a new Kobold of specific map level
        public static Kobold Create(int level)
        {
            int health = Dice.Roll("2D5");
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
