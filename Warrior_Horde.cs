using System;
using System.Collections.Generic;
using System.Text;

namespace Ishak
{
    class Warrior_Horde : Warrior
    {

        protected int crit = 0;

        protected int rage = 0;
        protected int rage_hit = 0;
        protected int rage_crit = 0;

        protected int bleed_tik_base = 2;
        protected int bleed_dmg_base = 15;
        protected int bleed_rage_cost = 20;


        protected bool fury = false;

        protected int stun_tik_base = 2;
        protected int stun_cd_tik = 0;
        protected int stun_cd_tik_base = 7;
        protected int stun_rage_cost = 20;
        public Warrior_Horde() : base()
        {
            //Характеристики
            race = "Shrek";
            hp_max = 130;
            hp = 130;
            hp_reg = 2;
            rage = 0;
            rage_hit = 10;
            rage_crit = 20;
            dmg = 0;
            dmg_max = 30;
            dmg_min = 20;
            armor = 3;
            armor_base = 3;
            crit = 20;
            bleed_tik_get = 0;
            bleed_tik_have = 0;
            bleed_dmg_get = 0;
            bleed_dmg_have = 0;
            stun_tik_get = 0;
            stun_tik_have = 0;
            target_is_bleed = false;
            target_is_stun = false;
        }
        public int Bleed_tik_get
        {
            get
            {
                return bleed_tik_get;
            }
        }
        public int Bleed_dmg_get
        {
            get
            {
                return bleed_dmg_get;
            }
        }
        //Способности
        protected void cast_bleed(bool im_is_bleed)
        {
            this.target_is_bleed = im_is_bleed;
            if (this.target_is_bleed == false && this.rage >= this.bleed_rage_cost)
            {
                bleed_tik_get = this.bleed_tik_base;
                bleed_dmg_get = this.bleed_dmg_base;
                this.rage -= this.bleed_rage_cost;
                im_is_cast = true;
                Console.WriteLine(this.race + " нанёс кровотечение за " + this.bleed_rage_cost + " ярости, на " + this.bleed_tik_get + " цикл. по " + this.bleed_dmg_get + " урона");
            }
        }
        protected void cast_stun()
        {
            this.stun_tik_get = 0;
            if (this.stun_cd_tik == 0 && this.rage >= this.stun_rage_cost && this.im_is_cast == false)
            {
                this.rage -= this.stun_rage_cost;
                Console.WriteLine(this.race + " Дает Леща!");
                this.stun_cd_tik = this.stun_cd_tik_base;
                this.stun_tik_get = this.stun_tik_base;
                this.im_is_cast = true;
            }
        }       
        //Эффекты
        protected void stun_cd()
        {
            if (this.stun_cd_tik > 0)
            {
                this.stun_cd_tik -= 1;
                Console.WriteLine(race + " До отката Леща: " + this.stun_cd_tik + " цикл.");
            }
            else
            {
                Console.WriteLine(race + " Лещ готов ");
            }
        }
        protected void check_fury()
        {
            if (this.hp <= (this.hp_max / 5))
            {
                fury = true;
                Console.WriteLine(this.race + " Впадает в бешенство, урон х2, броня = 0");
            }
            else
            {
                fury = false;
            }
        }
        protected void give_dmg()
        {
            if (this.im_have_stun == false)
            {
                if (this.im_is_cast == false)
                {
                    if (this.crit >= rand.Next(1, 101))
                    {
                        this.dmg = rand.Next(this.dmg_min, this.dmg_max) * 2;
                        this.rage += this.rage_crit;
                        if (fury == true)
                        {
                            this.dmg *= 2;
                        }
                        Console.WriteLine(this.race + " наносит критический урон " + this.dmg + " и получает " + this.rage_crit + " ярости");
                    }
                    else
                    {
                        this.dmg = rand.Next(this.dmg_min, this.dmg_max);
                        this.rage += this.rage_hit;
                        if (fury == true)
                        {
                            this.dmg *= 2;
                        }
                        Console.WriteLine(this.race + " наносит урон " + this.dmg + " и получает " + this.rage_hit + " ярости");
                    }                    
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
            if (fury == true)
            {
                this.armor = 0;
            }
            this.dmg -= this.armor;
            if (this.dmg < 0)
            {
                this.dmg = 0;
            }
            else
            Console.WriteLine(race + " получил урон " + this.dmg + " (отражено бронёй " + this.armor + ')');
            this.hp -= this.dmg;
            if (this.hp < 0)
            {
                this.hp = 0;
            }
        }
        protected void status()
        {
            Console.WriteLine(race + " ХП = " + hp);
            Console.WriteLine(race + " Ярость = " + rage);
            check_fury();
            regeneration_hp();
            if (fury == true)
            {
                Console.WriteLine(race + " Жаждет смерти (x2 урон, броня = 0, до конца боя)");
            }
            stun_cd();       
        }
        protected void behavior(bool target_is_bleed)
            {
            cast_stun();
            cast_bleed(target_is_bleed);            
            }
        public void fight(int dmg, bool target_is_bleed)
        {
            get_dmg(dmg);
            if (hp != 0)
            {

                status();
                behavior(target_is_bleed);
                give_dmg();
            }
            else
            {
                Console.WriteLine(this.race + " Погиб");
            }
        } 
    }    
}
