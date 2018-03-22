using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace zadanie1
{
    class Kingdom
    {
        private const int WarlockNumber = 3;
        private const int WizardNumber = 3;

        static void Main(string[] args)
        {
            Factory.StartFactories();

            for (int x = 0; x < WarlockNumber; x++)
                new Thread(new ThreadStart(() =>
                {
                    while (true)
                    {
                        TimeConfiguration.NewCurseDelay();
                        Factory.ThrowCourse();
                    }
                })).Start();

            for (int x = 0; x < WizardNumber; x++)
                new Thread(new ThreadStart(() =>
                {
                    while (true)
                    {
                        TimeConfiguration.WizardWorkDelay();
                        Factory.RemoveCourses();
                    }
                })).Start();

            while (true)
            {
                TimeConfiguration.NewAlchemistArriveDelay();
                AlchemistGuild.StartNewAlchemist();
            }
        }
    }
}

