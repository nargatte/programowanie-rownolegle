using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;

namespace zadanie3
{
    public class DistributedSystem : IDisposable
    {
        public DistributedSystem(int numOfProces, Tuple<int , int>[] relations, string[] names, Func<ProcesInSystem> procesFactory )
        {
            Process = new ProcesInSystem[numOfProces];
            ProcesSetup[] procesSetups = new ProcesSetup[numOfProces];
            Barrier = new Barrier(numOfProces);
            Random random = new Random();
            for (int i = 0; i < numOfProces; i++)
            {
                Process[i] = procesFactory();
            }

            for (int i = 0; i < numOfProces; i++)
            {
                procesSetups[i] = new ProcesSetup();
                procesSetups[i].Random = new Random(random.Next());
                procesSetups[i].Barier = Barrier;
                procesSetups[i].Name = names[i];
                procesSetups[i].Neightbours = relations.Where(t => t.Item1 == i).Select(t => Process[t.Item2])
                    .Concat(relations.Where(t => t.Item2 == i).Select(t => Process[t.Item1])).ToArray();

                Process[i].BaseSetup(procesSetups[i]);
            }

            for (int i = 0; i < numOfProces; i++)
            {
                Process[i].Run();
            }
        }

        private ProcesInSystem[] Process { get; }

        private Barrier Barrier { get;}

        public void Dispose()
        {
            Barrier.Dispose();
        }
    }
}