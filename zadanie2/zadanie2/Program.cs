using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace zadanie2
{
    class Program
    {
        static void Main(string[] args)
        {
            Table.StartFeast();

            new Thread(new ThreadStart(() =>
            {
                while (true)
                {
                    TimeDealy.CucumberServeDelay();
                    Console.WriteLine("Cucumber is suppling");
                    Table.SupplyCucumbers();
                }

            })).Start();

            while(true)
            {
                TimeDealy.WineServeDelay();
                Console.WriteLine("Wine is suppling");
                Table.SupplyWine();
            }
        }
    }
}
