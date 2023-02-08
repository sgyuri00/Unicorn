using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_2022_23_01_BLG4MG.Model
{
    public interface IWeapon
    {
        public string WeaponName { get; set; }
        public int BulletDamage { get; set; }
        public int RateOfFire { get; set; }
    }
}
