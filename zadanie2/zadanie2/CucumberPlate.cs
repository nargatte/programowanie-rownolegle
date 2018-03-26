using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace zadanie2
{
    class CucumberPlate
    {
        public const int MaxNumber = 3;

        public CucumberPlate(int id)
        {
            this.id = id;
        }

        public void Supply()
        {
            Monitor.Enter(m_cucumberNumber);
            cucumberNumber = MaxNumber;
            Monitor.Exit(m_cucumberNumber);
        }

        public bool IsEmpty()
        {
            bool result = false;
            Monitor.Enter(m_cucumberNumber);
            if (cucumberNumber == 0)
                result = true;
            Monitor.Exit(m_cucumberNumber);
            return result;
        }

        public void TakeOne()
        {
            Monitor.Enter(m_cucumberNumber);
            if (cucumberNumber == 0)
                Console.WriteLine("There are any cucumber left!!!!!!!!!!!!!!!!!!!!!");
            cucumberNumber--;
            Console.WriteLine($"{id} cucumber -- {cucumberNumber}");
            Monitor.Exit(m_cucumberNumber);
        }

        private int cucumberNumber = MaxNumber;

        private int id;

        private object m_cucumberNumber = new object();
    }
}
