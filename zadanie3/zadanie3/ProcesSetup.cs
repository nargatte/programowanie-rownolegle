using System;
using System.Threading;

namespace zadanie3
{
    public class ProcesSetup
    {
        public Barrier Barier { get; set; }
        public Random Random { get; set; }
        public ProcesInSystem[] Neightbours { get; set; }
        public string Name { get; set; } 
    }
}