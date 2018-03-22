using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace zadanie1
{
    class AlchemistGuild
    {
        public static void StartNewAlchemist()
        {
            alchemistGuilds[random.Next(0, alchemistGuilds.Count)].SendAlchemistToFacroty();
        }

        public static void InformAboutNewResource()
        {
            foreach(AlchemistGuild gulid in alchemistGuilds)
            {
                if (gulid.GuildTest())
                    break;
            }
        }

        private static List<AlchemistGuild> alchemistGuilds = new List<AlchemistGuild>()
        {
#if ABSTRACT_FEEDSTOCKS
            new AlchemistGuild(Feedstocks.A | Feedstocks.B | Feedstocks.C),
            new AlchemistGuild(Feedstocks.A | Feedstocks.B),
            new AlchemistGuild(Feedstocks.A | Feedstocks.C),
            new AlchemistGuild(Feedstocks.B | Feedstocks.C)
#else
            new AlchemistGuild(Feedstocks.Lead | Feedstocks.Mercury | Feedstocks.Sulphur),
            new AlchemistGuild(Feedstocks.Lead | Feedstocks.Mercury),
            new AlchemistGuild(Feedstocks.Lead | Feedstocks.Sulphur),
            new AlchemistGuild(Feedstocks.Mercury | Feedstocks.Sulphur)
#endif
        };

        private static Random random = new Random();

        private static SemaphoreSlim s_factoryAccess = new SemaphoreSlim(1);

        private static int globalAlchemistCounter = 0;

        private static int GetAlchemistNumber() => ++globalAlchemistCounter;

        private Feedstocks feedstocks;

        private int waitingAlchemistCounter = 0;

        private SemaphoreSlim s_waitingCounterAvaliable = new SemaphoreSlim(1);

        private SemaphoreSlim s_feedstocksAvaliable = new SemaphoreSlim(0);

        private AlchemistGuild(Feedstocks feedstocks)
        {
            this.feedstocks = feedstocks;
        }

        private void SendAlchemistToFacroty() => new Thread(new ThreadStart(Alchemist)).Start();

        private void Alchemist()
        {
            int alchemistNumber = GetAlchemistNumber();
            Console.WriteLine($"Alchemist nr. {alchemistNumber}, from {feedstocks} guild ++");

            s_waitingCounterAvaliable.Wait();
            waitingAlchemistCounter++;
            s_waitingCounterAvaliable.Release();

            GuildTest();

            s_feedstocksAvaliable.Wait();

            s_factoryAccess.Wait();
            Factory.TakeFromStore(feedstocks);
            s_factoryAccess.Release();

            s_waitingCounterAvaliable.Wait();
            waitingAlchemistCounter--;
            s_waitingCounterAvaliable.Release();

            Console.WriteLine($"Alchemist nr. {alchemistNumber}, from {feedstocks} guild --");
        }

        private bool GuildTest()
        {
            bool noAlchemistWait = false;
            s_waitingCounterAvaliable.Wait();
            if (waitingAlchemistCounter == 0) noAlchemistWait = true;
            s_waitingCounterAvaliable.Release();
            if (noAlchemistWait) return false;

            s_factoryAccess.Wait();
            bool result = Factory.TestStoreState(feedstocks);
            if (result) s_feedstocksAvaliable.Release();
            s_factoryAccess.Release();

            return result;
        }
    }
}
