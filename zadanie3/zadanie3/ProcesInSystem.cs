using System;
using System.Threading;

namespace zadanie3
{
    public abstract class ProcesInSystem
    {
        public void BaseSetup(ProcesSetup procesSetup)
        {
            Setup = procesSetup;
            ReceivedMessages = new object[Setup.Neightbours.Length];
            ToSendMessages = new object[Setup.Neightbours.Length];
        }

        public void Run()
        {
            Thread = new Thread(ThreadProcedure);
            Thread.Start();
        }

        protected abstract void Computation();

        protected abstract string DisplayStatus(string myName);

        protected object[] ReceivedMessages { get; set; }

        protected object[] ToSendMessages { get; set; }

        protected int RoundsCounter { get; private set; }

        protected int GetRandomNumber() => Setup.Random.Next();

        private ProcesSetup Setup { get; set; }

        private Thread Thread { get; set; }

        private void ThreadProcedure()
        {
            while (true)
            {
                ClearToSendMessages();
                Computation();
                Setup.Barier.SignalAndWait();
                Console.Write(DisplayStatus(Setup.Name));
                Setup.Barier.SignalAndWait();
                SendMessages();
                Setup.Barier.SignalAndWait();
                RoundsCounter++;
            }
        }

        private void SendMessages()
        {
            for (int x = 0; x < ToSendMessages.Length; x++)
            {
                Setup.Neightbours[x].ReceivedMessages[Array.IndexOf(Setup.Neightbours[x].Setup.Neightbours, this)] = ToSendMessages[x];
            }
        }

        private void ClearToSendMessages()
        {
            for (int i = 0; i < ToSendMessages.Length; i++)
            {
                ToSendMessages[i] = null;
            }
        }
    }
}