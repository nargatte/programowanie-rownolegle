using System;
using System.Threading;

namespace zadanie2
{
    class WineGoblet
    {
        public const int BottleSize = 12;

        public WineGoblet(int id)
        {
            this.id = id;
        }

        public static void Supply()
        {
            Monitor.Enter(m_bottleContent);
            bottleContent = BottleSize;
            Monitor.Exit(m_bottleContent);
        }

        public bool IsEmpty()
        {
            bool result = false;
            Monitor.Enter(m_bottleContent);
            if (bottleContent == 0)
                result = true;
            Monitor.Exit(m_bottleContent);
            return result;
        }

        public void TakeOne()
        {
            Monitor.Enter(m_bottleContent);
            if (bottleContent == 0)
                Console.WriteLine("There are any wine left!!!!!!!!!!!!!!!!!!!!!");
            bottleContent--;
            Console.WriteLine($"{id} wine -- {bottleContent}");
            Monitor.Exit(m_bottleContent);
        }

        private static object m_bottleContent = new object();

        private int id;

        private static int bottleContent = BottleSize;
    }
}
