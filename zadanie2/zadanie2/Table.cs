using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zadanie2
{
    static class Table
    {
        public const int numberOfPairs = 2;

        private static CucumberPlate[] cucumberPlates = new CucumberPlate[numberOfPairs];

        private static WineGoblet[] wineGoblets = new WineGoblet[numberOfPairs];

        private static Knight[] knights = new Knight[numberOfPairs * 2];

        public static void EatTestAll()
        {
            foreach (var k in knights)
            {
                k.TestEat();
            }
        }

        static Table()
        {
            for (int i = 0; i < numberOfPairs; i++)
            {
                if (i == 0)
                    knights[2 * i] = new Knight(4 * i, true);
                else
                    knights[2 * i] = new Knight(4 * i, false);

                wineGoblets[i] = new WineGoblet(4 * i + 1);

                knights[2 * i + 1] = new Knight(4 * i + 2, false);

                cucumberPlates[i] = new CucumberPlate(4 * i + 3);
            }

            for (int i = 0; i < numberOfPairs * 2; i++)
            {
                knights[i].Neighbours[0] = knights[Mod(i - 1, numberOfPairs * 2)];
                knights[i].Neighbours[1] = knights[Mod(i + 1, numberOfPairs * 2)];

                knights[i].WineGoblet = wineGoblets[i/2];

                knights[i].CucumberPlate = cucumberPlates[Mod(i/2-1, numberOfPairs)];

                knights[i].MyKing = knights[0];
            }
        }

        public static void StartFeast()
        {
            foreach (var k in knights)
            {
                k.StartFeast();
            }
        }

        public static void SupplyWine()
        {
            WineGoblet.Supply();
            EatTestAll();
        }

        public static void SupplyCucumbers()
        {
            foreach (var cp in cucumberPlates)
            {
                cp.Supply();
            }
            EatTestAll();
        }

        public static object m_table = new object();

        private static int Mod(int k, int n) => ((k %= n) < 0) ? k + n : k;
    }
}
