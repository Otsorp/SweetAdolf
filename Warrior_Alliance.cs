using System;
using System.Collections.Generic;
using System.Text;

namespace Ishak
{
    class Warrior_Alliance : Warrior
    {

        protected int block_chance = 20;
        protected int block = 20;

        protected int mana_max = 100;
        protected int mana = 100;
        protected int mana_reg = 5;

        protected int WP_tik = 0;
        protected int WP_tik_base = 5;
        protected int WP_mana_cost = 25;
        protected int WP_armor_bonus = 7;
        protected bool im_have_WP = false;

        protected int heal_min = 40;
        protected int heal_max = 60;
        protected int heal = 0;
        protected int heal_mana_cost = 40;


        public Warrior_Alliance() : base()
        {
            //Характеристики
            race = "Human";
            hp_max = 100;
            hp = 100;
            hp_reg = 1;
            dmg_min = 15;
            dmg_min_base = 15;
            dmg_max = 20;
            dmg_max_base = 20;
            dmg = 0;
            armor = 7;
            armor_base = 7;
            bleed_tik_get = 0;
            bleed_tik_have = 0;
            bleed_dmg_get = 0;
            bleed_dmg_have = 0;
            stun_tik_get = 0;
            stun_tik_have = 0;
            im_have_bleed = false;
            im_have_stun = false;
            //Для скилов
            im_is_cast = false;
            im_have_WP = false;
        }
        public bool Im_have_bleed
        {
            get
            {
                return im_have_bleed;
            }
        }
        // Способности
        protected void сast_white_power()
        {
            if (this.im_have_WP == false && this.mana >= this.WP_mana_cost && this.im_is_cast == false)
            {
                this.mana -= this.WP_mana_cost;
                this.WP_tik = this.WP_tik_base;
                this.dmg_min = this.dmg_max;
                this.armor += this.WP_armor_bonus;
                this.im_have_WP = true;
                this.im_is_cast = true;
                Console.WriteLine(this.race + " Cмачно зигует! За " + this.WP_mana_cost + " маны получает " + this.WP_armor_bonus + " армора и макс. урон на " + this.WP_tik + " цикл.");
            }
        }
        protected void cast_heal()
        {
            if (this.hp <= (this.hp_max - this.heal_max) && this.mana >= this.heal_mana_cost && this.im_is_cast == false)
            {
                this.heal = rand.Next(heal_min, heal_max + 1);
                this.hp += this.heal;
                this.mana -= this.heal_mana_cost;
                this.im_is_cast = true;
                Console.WriteLine(this.race + " кастует исцеление за " + this.heal_mana_cost + " маны, получилось на " + this.heal + " ХП");
            }
        }
        // Эффекты        
        protected void regeneration_mana()
        {
            if (this.mana < this.mana_max)
            {
                this.mana += this.mana_reg;
                Console.WriteLine(race + " регенерирует (+" + this.mana_reg + "Маны)");
            }
        }
        protected void check_bleed(int bleed_tik_get, int bleed_dmg_get)
        {           
            if (this.im_have_bleed == false && bleed_tik_get > 0)
            {
                this.bleed_tik_have = bleed_tik_get;
                this.bleed_dmg_have = bleed_dmg_get;
                this.im_have_bleed = true;                
            }
            if (this.bleed_tik_have > 0)
            {
                this.hp -= this.bleed_dmg_have;
                if (this.hp < 0)
                {
                    this.hp = 0;
                }
                else
                {
                Console.WriteLine(this.race + " истекает кровью ещё " + this.bleed_tik_have + " цикл. по " + this.bleed_dmg_have + " ХП");
                this.bleed_tik_have -= 1;
                if (this.bleed_tik_have == 0)
                {
                    this.im_have_bleed = false;
                    this.bleed_dmg_have = 0;
                }
                }

            }
        }
        protected void check_stun(int stun_tik_get)
        {
            if (this.stun_tik_have == 0)
            {
                this.stun_tik_have = stun_tik_get;
            }
            if (this.stun_tik_have > 0)
            {
                this.im_have_stun = true;
                Console.WriteLine(this.race + " Ещё пробудет в стане " + this.stun_tik_have + " цикл.");
                this.stun_tik_have -= 1;
            }
            else
            {
                this.im_have_stun = false;
            }
        }
        protected void WP_on_me()
        {
            if(this.im_have_WP == true)
            {
                this.WP_tik -= 1;
                if( this.WP_tik > 0)
                {
                    Console.WriteLine(this.race + " WP Баф продлится ещё " + this.WP_tik + " цикл. (+" + this.WP_armor_bonus + " брони и максимальный урон)");
                }
                else
                {                   
                    this.WP_tik = 0;
                    this.im_have_WP = false;
                    this.dmg_min = this.dmg_min_base;
                    this.armor -= this.WP_armor_bonus;
                    Console.WriteLine(this.race + " WP прекращает действовать");
                }
            }
        }
        protected void give_dmg()
        {
            if (this.im_have_stun == false)
            {
                if (this.im_is_cast == false)
                {
                    this.dmg = rand.Next(this.dmg_min, this.dmg_max);
                    Console.WriteLine(this.race + " наносит урон " + this.dmg);
                }
                else
                {
                    this.dmg = 0;
                }
            }
            else
            {
                this.dmg = 0;
            }            
        }
        protected void get_dmg(int dmg)
        {
            this.im_is_cast = false;
            this.dmg = dmg;
            if (this.block >= rand.Next(1, 101) && this.dmg > 0)
            {
                Console.WriteLine(this.race + " Блокировал удар");
                this.dmg = 0;
            }
            else
            {
                this.dmg -= this.armor;
                if (this.dmg < 0)
                {
                    this.dmg = 0;
                }
                Console.WriteLine(race + " получил урон " + this.dmg + " (отражено бронёй " + this.armor + ')');
            }
            this.hp -= this.dmg;
            if (this.hp < 0)
            {
                this.hp = 0;
            }
        }
        protected void behavior()
        {
            if(this.im_have_stun == false)
            {
                cast_heal();
                сast_white_power();
            }
        }      
        protected void status(int bleed_tik_get, int bleed_dmg_get, int stun_tik_get)
        {
            Console.WriteLine(race + " ХП = " + hp);
            Console.WriteLine(this.race + " Mана = " + this.mana);
            regeneration_hp();
            regeneration_mana();
            check_bleed(bleed_tik_get, bleed_dmg_get);
            if (hp != 0)
            {
                WP_on_me();
                check_stun(stun_tik_get);
                give_dmg();
            }
            else
            {
                Console.WriteLine(this.race + " Погиб от кровотечения");
            }

        }
        public void fight(int dmg, int bleed_tik_get, int bleed_dmg_get, int stun_tik_get)
        {
            get_dmg(dmg);
            if (hp != 0)
            {                
                status(bleed_tik_get, bleed_dmg_get, stun_tik_get);
                behavior();
            }
            else
            {
                Console.WriteLine(this.race + " Погиб");
            }
        }
    }
}
