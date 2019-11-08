using System;

namespace Ishak
{
    class Program
    {
        static void Main(string[] args)
        {
            Warrior_Alliance A = new Warrior_Alliance();
            Warrior_Horde H = new Warrior_Horde();
            int Dmg = 0;
            int Bleed_tik_get;
            int Bleed_dmg_get;
            int Stun_tik_get;
            bool Im_have_bleed = false;

            while (H.Hp != 0 && A.Hp != 0)
            {
                H.fight(Dmg, Im_have_bleed);
                Console.WriteLine("-----");
                Dmg = H.Dmg;
                Bleed_tik_get = H.Bleed_tik_get;
                Bleed_dmg_get = H.Bleed_dmg_get;
                Stun_tik_get = H.Stun_tik_get;
                if ( H.Hp == 0)
                {
                    break;
                }
                A.fight(Dmg, Bleed_tik_get, Bleed_dmg_get, Stun_tik_get);
                Dmg = A.Dmg;
                Im_have_bleed = A.Im_have_bleed;
                Console.WriteLine("--------------------------------------------------------------------------------");
            }
            Console.ReadKey();
        }
    }
}
