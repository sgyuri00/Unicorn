using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GUI_2022_23_01_BLG4MG.Model
{
    internal class Unicorn
    {
        private int hp;
        private int damage;
        public int Hp { get => hp; set => hp = value; }
        public int Damage { get => damage; set => damage = value; }

        public Unicorn(int hp, int damage)
        {
            this.hp = hp;
            this.damage = damage;
        }
    }
}
