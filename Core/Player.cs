using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rogue.Core;
using Rogue.Systems;
using Rogue.Interfaces;
using RLNET;


namespace Rogue.Core
{
    public class Player : Actor
    {
        public Player()
        {
            Attack = 2;
            AttackChance = 50;
            Awareness = 15;
            Color = Colors.Player;
            Defense = 2;
            DefenseChance = 40;
            Gold = 0;
            Health = 100;
            MaxHealth = 100;
            Name = "Rogue";
            Speed = 10;
            Symbol = '@';
            Level = 1;
        }

        public void DrawStats(RLConsole statConsole, int _statWidth, int _statHeight)
        {
            //statConsole.SetBackColor(0, 0, _statWidth, _statHeight, Swatch.DbOldStone);
            statConsole.Print(1, 1, "Stats", Colors.TextHeading);
            statConsole.Print(1, 3, $"Name:    {Name}", Colors.Text);
            statConsole.Print(1, 5, $"Level:   {Level}", Colors.Text);
            statConsole.Print(1, 7, $"Health:  {Health}/{MaxHealth}", Colors.Text);
            statConsole.Print(1, 9, $"Attack:  {Attack} ({AttackChance}%)", Colors.Text);
            statConsole.Print(1, 11, $"Defense: {Defense} ({DefenseChance}%)", Colors.Text);
            statConsole.Print(1, 13, $"Gold:   {Gold}", Colors.Gold);
        }
    }
}
