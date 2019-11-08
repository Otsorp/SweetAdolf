using System;
using System.Collections.Generic;
using System.Text;

namespace Ishak
{
    class Warrior
    {
        protected Random rand = new Random();
        //Характеритики
        protected string race;
        protected int hp;
        protected int hp_max;
        protected int hp_reg;
        protected int dmg;
        protected int dmg_max;
        protected int dmg_max_base;
        protected int dmg_min;
        protected int dmg_min_base;
        protected int armor;
        protected int armor_base;       
        //Состояния
        protected bool im_is_cast;
        protected bool im_have_bleed;
        protected bool im_have_stun;
        protected bool target_is_bleed;
        protected bool target_is_stun;
        protected int bleed_tik_get;
        protected int bleed_tik_have;
        protected int bleed_dmg_get;
        protected int bleed_dmg_have;
        protected int stun_tik_get;
        protected int stun_tik_have;

        public int Hp
        {
            get
            {
                return hp;
            }
        }
        public int Dmg
        {
            get
            {
                return dmg;
            }
        }
        public int Bleed_tik
        {
            get
            {
                return bleed_tik_get;
            }
        }
        public int Stun_tik_get
        {
            get
            {
                return stun_tik_get;
            }
        }
        
        protected void regeneration_hp()
        {
            if (this.hp < this.hp_max)
            {
                this.hp += this.hp_reg;
                Console.WriteLine(race + " регенерирует (+" + this.hp_reg + " ХП)");
            }
        }
    }
}
