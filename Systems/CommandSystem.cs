﻿using System;
using System.Text;
using RogueSharp;
using Rogue.Core;
using RogueSharp.DiceNotation;
using Rogue.Interfaces;
using Rogue.Monsters;
using Rogue.Equipment;

namespace Rogue.Systems
{
    public class CommandSystem
    {
        public bool IsPlayerTurn { get; set; }

        public void EndPlayerTurn()
        {
            IsPlayerTurn = false;
        }

        // Happens when monster is created and sees player
        public void ActivateMonsters()
        {
            IScheduleable scheduleable = Game.SchedulingSystem.Get();
            if (scheduleable is Player)
            {
                IsPlayerTurn = true;
                Game.SchedulingSystem.Add(Game.Player);
            }
            else
            {
                Monster monster = scheduleable as Monster;

                if (monster != null)
                {
                    monster.PerformAction(this);
                    Game.SchedulingSystem.Add(monster);
                }

                ActivateMonsters();
            }
        }

        //Moves the monster towards the player, pathfinding
        public void MoveMonster(Monster monster, Cell cell)
        {
            if (!Game.DungeonMap.SetActorPosition(monster, cell.X, cell.Y))
            {
                if (Game.Player.X == cell.X && Game.Player.Y == cell.Y)
                {
                    Attack(monster, Game.Player);
                }
            }
        }

        // Handles the key presses of the player
        public bool MovePlayer( Direction direction )
        {
            int x = Game.Player.X;
            int y = Game.Player.Y;

            if (direction == Direction.Up)
            {
                y = Game.Player.Y - 1;
            }
            else if (direction == Direction.Down)
            {
                y = Game.Player.Y + 1;
            }
            else if (direction == Direction.Left)
            {
                x = Game.Player.X - 1;
            }
            else if (direction == Direction.Right)
            {
                x = Game.Player.X + 1;
            }
            else return false;

            if (Game.DungeonMap.SetActorPosition(Game.Player, x, y))
            {
                return true;
            }

            Monster monster = Game.DungeonMap.GetMonsterAt(x, y);

            if (monster != null)
            {
                Attack(Game.Player, monster);
                return true;
            }

            return false;
        }

        // Handles an attack by the player or monster
        public void Attack(Actor attacker, Actor defender)
        {
            StringBuilder attackMessage = new StringBuilder();
            StringBuilder defenseMessage = new StringBuilder();

            int hits = ResolveAttack(attacker, defender, attackMessage);

            int blocks = ResolveDefense(defender, hits, attackMessage, defenseMessage);

            Game.MessageLog.Add(attackMessage.ToString());
            if (!string.IsNullOrWhiteSpace(defenseMessage.ToString()))
            {
                Game.MessageLog.Add(defenseMessage.ToString());
            }

            int damage = hits - blocks;

            ResolveDamage(defender, damage);
        }

        // The attacker rolls based on his stats to see if he gets any hits
        private static int ResolveAttack(Actor attacker, Actor defender, StringBuilder attackMessage)
        {
            int hits = 0;

            attackMessage.AppendFormat("{0} attacks {1} and rolls: ", attacker.Name, defender.Name);

            // Roll a number of 100-sided dice equal to the Attack value of the attacking actor
            DiceExpression attackDice = new DiceExpression().Dice(attacker.Attack, 100);
            DiceResult attackResult = attackDice.Roll();

            // Look at the face value of each single die that was rolled
            foreach (TermResult termResult in attackResult.Results)
            {
                attackMessage.Append(termResult.Value + ", ");
                // Compare the value to 100 minus the attack chance and add a hit if it's greater
                if (termResult.Value >= 100 - attacker.AttackChance)
                {
                    hits++;
                }
            }

            return hits;
        }

        // The defender rolls based on his stats to see if he blocks any of the hits from the attacker
        private static int ResolveDefense(Actor defender, int hits, StringBuilder attackMessage, StringBuilder defenseMessage)
        {
            int blocks = 0;

            if (hits > 0)
            {
                attackMessage.AppendFormat("scoring {0} hits.", hits);
                defenseMessage.AppendFormat("  {0} defends and rolls: ", defender.Name);

                // Roll a number of 100-sided dice equal to the Defense value of the defendering actor
                DiceExpression defenseDice = new DiceExpression().Dice(defender.Defense, 100);
                DiceResult defenseRoll = defenseDice.Roll();

                // Look at the face value of each single die that was rolled
                foreach (TermResult termResult in defenseRoll.Results)
                {
                    defenseMessage.Append(termResult.Value + ", ");
                    // Compare the value to 100 minus the defense chance and add a block if it's greater
                    if (termResult.Value >= 100 - defender.DefenseChance)
                    {
                        blocks++;
                    }
                }
                defenseMessage.AppendFormat("resulting in {0} blocks.", blocks);
            }
            else
            {
                attackMessage.Append("and misses completely.");
            }

            return blocks;
        }

        // Apply any damage that wasn't blocked to the defender
        private static void ResolveDamage(Actor defender, int damage)
        {
            if (damage > 0)
            {
                defender.Health = defender.Health - damage;

                Game.MessageLog.Add($"  {defender.Name} was hit for {damage} damage");

                if (defender.Health <= 0)
                {
                    ResolveDeath(defender);
                }
            }
            else
            {
                Game.MessageLog.Add($"  {defender.Name} blocked all damage");
            }
        }

        // Remove the defender from the map and add some messages upon death.
        private static void ResolveDeath(Actor defender)
        {
            if (defender is Player)
            {
                defender.Health = 0;
                Game.MessageLog.Add($"  {defender.Name} was killed, GAME OVER MAN!");
                //Temporary code for now, add splash screen later?
                Environment.Exit(0);
            }
            else if (defender is Monster)
            {
                Game.DungeonMap.RemoveMonster((Monster)defender);
                Game.MessageLog.Add($"  {defender.Name} was slashed to bits!!");
                Game.MessageLog.Add($" {defender.Name} dropped {defender.Gold} gold!");
                Game.Player.Gold += defender.Gold;

                HandEquipment chance = returnChance(defender);

                Game.MessageLog.Add($" {defender.Name} dropped {chance.Name}!");
                Game.Player.Hand = chance;
            }
        }

        // Calculates whether or not the monster has an item on them, and delivers that to the player
        public static HandEquipment returnChance(Actor defender)
        {
            int chance = Dice.Roll("10D10");
            HandEquipment hand = HandEquipment.None();
            if (!(defender is Mimic)) {
                if (chance <= 20)
                {
                    hand = HandEquipment.Dagger();
                }
                else if (chance > 20 && chance <= 30)
                {
                    hand = HandEquipment.Sword();
                }
                else if (chance >= 70 && chance <= 80)
                {
                    hand = HandEquipment.Axe();
                }
                else if (chance >= 95)
                {
                    hand = HandEquipment.TwoHandedSword();
                }
                else
                {
                    hand = HandEquipment.None();
                }
            }
            return hand;
        }

    }
}