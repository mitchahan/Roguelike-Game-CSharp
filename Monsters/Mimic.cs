using Rogue.Core;
using RogueSharp.DiceNotation;

namespace Rogue.Monsters
{
    public class Mimic : Monster
    {
        // Creates a new Mimic of specific map level
        public static Mimic Create(int level)
        {

            int health = Dice.Roll("2D5");
            return new Mimic
            {
                Attack = Dice.Roll("2D4"),
                AttackChance = Dice.Roll("15D3"),
                Awareness = 5,
                Color = Colors.Floor,
                Defense = Dice.Roll("2D4") + level / 3,
                DefenseChance = Dice.Roll("8D4"),
                Gold = Dice.Roll("8D6"),
                Health = health,
                MaxHealth = health * level + level,
                Name = "Mimic",
                Speed = 20,
                Symbol = 'M',
            };
        }
    }
}
