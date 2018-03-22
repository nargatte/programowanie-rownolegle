using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace zadanie1
{
    class Factory
    {
        public static void StartFactories()
        {
            foreach (Feedstocks feedstock in Enum.GetValues(typeof(Feedstocks)))
            {
                Factory factory = new Factory(feedstock);
                factories.Add(feedstock, factory);
                factory.StartWorking();
            }
        }

        public static bool TestStoreState(Feedstocks feedstock)
        {
            bool result = true;
            foreach (Feedstocks f in Enum.GetValues(typeof(Feedstocks)).Cast<Enum>().Where(item => feedstock.HasFlag(item)))
            {
                if (!factories[f].HasAnyResorces())
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        public static void TakeFromStore(Feedstocks feedstock)
        {
            Console.WriteLine($"{feedstock} --");
            foreach (Feedstocks f in Enum.GetValues(typeof(Feedstocks)).Cast<Enum>().Where(item => feedstock.HasFlag(item)))
            {
                factories[f].DecreaseResource();
            }
        }

        public static void ThrowCourse()
        {
            factories[factories.Keys.ElementAt(random.Next(0, factories.Count))].IncreaseCurse();
        }

        public static void RemoveCourses()
        {
            foreach (Feedstocks f in Enum.GetValues(typeof(Feedstocks)))
            {
                factories[f].DecreaseCurse();
            }
        }

        private static Random random = new Random();

        private static Dictionary<Feedstocks, Factory> factories = new Dictionary<Feedstocks, Factory>();

        private Feedstocks feedstock;

        private int storeCount = 0;

        private int curseCount = 0;

        private SemaphoreSlim s_storeEmpty = new SemaphoreSlim(2);

        private SemaphoreSlim s_doesNotCursed = new SemaphoreSlim(1);

        private SemaphoreSlim s_storeCount = new SemaphoreSlim(1);

        private SemaphoreSlim s_curseCount = new SemaphoreSlim(1);

        private Factory(Feedstocks feedstock)
        {
            this.feedstock = feedstock;
        }

        private void StartWorking() => new Thread(new ThreadStart(Work)).Start();

        private void Work()
        {
            while (true)
            {
                s_doesNotCursed.Wait();
                s_doesNotCursed.Release();

                s_storeEmpty.Wait();
                Console.WriteLine($"Production {feedstock}");

                TimeConfiguration.FactoryProductionDelay();

                IncteaseResource();

                AlchemistGuild.InformAboutNewResource();
            }
        }

        private bool HasAnyResorces()
        {
            bool result = false;

            s_storeCount.Wait();
            if (storeCount != 0) result = true;
            s_storeCount.Release();

            return result;
        }

        private void IncteaseResource()
        {
            s_storeCount.Wait();
            storeCount++;
            Console.WriteLine($"Factory {feedstock} ++ {storeCount}");
            s_storeCount.Release();
        }

        private void DecreaseResource()
        {
            s_storeCount.Wait();
            if(storeCount == 0)
                Console.WriteLine($"Factory doesn't have {feedstock}!!!!!!!!!!!!!!!!!!!!");

            storeCount--;
            Console.WriteLine($"Factory {feedstock} -- {storeCount}");
            s_storeCount.Release();

            s_storeEmpty.Release();
        }

        private void IncreaseCurse()
        {
            s_curseCount.Wait();
            if (curseCount == 0)
                s_doesNotCursed.Wait();
            curseCount++;
            Console.WriteLine($"Factory {feedstock} curse ++ {curseCount}");
            s_curseCount.Release();
        }

        private void DecreaseCurse()
        {
            s_curseCount.Wait();
            if (curseCount != 0)
            {
                curseCount--;
                if (curseCount == 0)
                    s_doesNotCursed.Release();
                Console.WriteLine($"Factory {feedstock} curse -- {curseCount}");
            }
            s_curseCount.Release();
        }

    }
}
