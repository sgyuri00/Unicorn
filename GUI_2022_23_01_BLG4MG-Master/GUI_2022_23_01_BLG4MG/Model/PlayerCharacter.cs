using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_2022_23_01_BLG4MG.Model
{
    public class PlayerCharacter
    {
        private int hp;
        public int Hp { get => hp; set => hp = value; }

        private IWeapon CurrentWeapon;

        public PlayerCharacter(int hp)
        {
            this.hp = hp;
        }

        public void TakeDamage(int dmg)
        {
            this.hp-= dmg;
        }

        public void EquipWeapon(IWeapon weapon)
        {
            this.CurrentWeapon = weapon;
        }


    }
}
