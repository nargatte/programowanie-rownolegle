using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace zadanie1
{
    static class TimeConfiguration
    {
        private const int minTime = 500;
        private const double timeScale = 3000;
        private const double factoryProductionTime = 1;
        private const double newAlchemistArriveTime = 1;
        private const double newCurseTime = 1;
        private const double wizardWorkTime = 10;

        private static readonly Random random = new Random();

        private static void Delay(double factor) => Thread.Sleep((int)(random.NextDouble() * timeScale * factor) + minTime);

        public static void FactoryProductionDelay() => Delay(factoryProductionTime);

        public static void NewAlchemistArriveDelay() => Delay(newAlchemistArriveTime);

        public static void NewCurseDelay() => Delay(newCurseTime);

        public static void WizardWorkDelay() => Delay(wizardWorkTime);

    }
}
