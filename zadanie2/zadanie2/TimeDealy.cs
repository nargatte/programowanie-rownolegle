using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace zadanie2
{
    static class TimeDealy
    {
        private const int minTime = 500;
        private const double timeScale = 1000;
        private const double sleepTime = 10;
        private const double narrateTime = 10;
        private const double wineServeTime = 100;
        private const double cucumberServeTime = 50;

        private static Random random = new Random();

        private static void Delay(double factor) => Thread.Sleep((int)(factor*timeScale*random.NextDouble())+minTime);

        public static void SleepDelay() => Delay(sleepTime);
        public static void NarrateDelay() => Delay(narrateTime);
        public static void WineServeDelay() => Delay(wineServeTime);
        public static void CucumberServeDelay() => Delay(cucumberServeTime);
    }
}
