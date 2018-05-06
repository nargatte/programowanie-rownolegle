using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zadanie3
{
    class Program
    {
        private const string data = "6 0 1 2 1 3 4 2 2 5 1 4 3";
        static void Main(string[] args)
        {
            int[] dataNumbers = data.Split(' ').Select(s => Int32.Parse(s)).ToArray();
            int totalNumber = dataNumbers[0];
            Tuple<int, int>[] pos = new Tuple<int, int>[totalNumber];
            string[] nameStrings = new string[totalNumber];
            for (int i = 1; i < dataNumbers.Length; i+=2)
            {
                pos[i / 2] = new Tuple<int, int>(dataNumbers[i], dataNumbers[i+1]);
            }

            for (int i = 0; i < totalNumber; i++)
            {
                //nameStrings[i] = $"({pos[i].Item1},{pos[i].Item2})";
                nameStrings[i] = i.ToString();
            }
            List<Tuple<int, int>> Connections = new List<Tuple<int, int>>();
            for (int i = 0; i < totalNumber-1; i++)
            {
                for (int j = i+1; j < totalNumber; j++)
                {
                    int dx = Math.Abs(pos[i].Item1 - pos[j].Item1);
                    int dy = Math.Abs(pos[i].Item2 - pos[j].Item2);

                    if(Math.Sqrt(dx*dx + dy*dy) <= 3)
                        Connections.Add(new Tuple<int, int>(i, j));
                }
            }

            new DistributedSystem(totalNumber, Connections.ToArray(), nameStrings, () => new ProcesImplementation());
        }
    }
}
