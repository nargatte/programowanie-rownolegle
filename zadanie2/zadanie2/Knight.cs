using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace zadanie2
{
    class Knight
    {
        public Knight(int id, bool king)
        {
            IsKing = king;
            this.id = id;

            m_eat.Wait();
            m_narrate.Wait();
        }

        public bool IsKing { get; } 

        public void StartFeast() => new Thread(new ThreadStart(Feast)).Start();

        public CucumberPlate CucumberPlate { get; set; }

        public WineGoblet WineGoblet { get; set; }

        public Knight[] Neighbours { get; set; } = new Knight[2];

        public Knight MyKing { get; set; }

        public void TestEat()
        {
            Monitor.Enter(Table.m_table);
            if (Neighbours[0].state != State.Eating && Neighbours[1].state != State.Eating &&
                state == State.Hungry && !WineGoblet.IsEmpty() && !CucumberPlate.IsEmpty())
            {
                state = State.Eating;
                m_eat.Release();
            }
            Monitor.Exit(Table.m_table);
        }

        public void TestNarrate()
        {
            Monitor.Enter(Table.m_table);
            if(Neighbours[0].state != State.Narrate && Neighbours[1].state != State.Narrate &&
                MyKing.state != State.Narrate && state == State.HaveStory)
            {
                state = State.Narrate;
                m_narrate.Release();
            }
            Monitor.Exit(Table.m_table);
        }

        private void Sleep()
        {
            Console.WriteLine($"Knight {id} is going to sleep");
            ChangeState(State.Sleep);

            Neighbours[0].TestNarrate();
            Neighbours[1].TestNarrate();

            TimeDealy.SleepDelay();
        }

        private void Eat()
        {
            Console.WriteLine($"Knight {id} is wake up, now is hungry");
            ChangeState(State.Hungry);
            TestEat();

            m_eat.Wait();

            Monitor.Enter(Table.m_table);
            Console.WriteLine($"Knight {id} eating");
            CucumberPlate.TakeOne();
            WineGoblet.TakeOne();
            Monitor.Exit(Table.m_table);
        }

        private void Narrate()
        {
            Console.WriteLine($"Knight {id} ate, now he want tell a story");
            ChangeState(State.HaveStory);
            TestNarrate();

            m_narrate.Wait();

            Console.WriteLine($"Knight {id} is starting a story");
            TimeDealy.NarrateDelay();
            Console.WriteLine($"Knight {id} ended a story");
        }

        private enum State
        {
            Sleep,
            Hungry,
            Eating,
            HaveStory,
            Narrate,
        }

        private State state = State.Sleep;

        private void ChangeState(State s)
        {
            Monitor.Enter(Table.m_table);
            state = s;
            Monitor.Exit(Table.m_table);
        }

        private int id;

        private void Feast()
        {
            Sleep();
            Eat();

            for (int i = 1; i < 10; i++)
            {
                Narrate();
                Sleep();
                Eat();
            }

            Console.WriteLine($"Knight {id} fell");
            ChangeState(State.Sleep);
        }

        // Using semaphore with 1 becaouse of 
        // https://stackoverflow.com/questions/21404144/synchronizationlockexception-on-monitor-exit-when-using-await

        private SemaphoreSlim m_eat = new SemaphoreSlim(1);

        private SemaphoreSlim m_narrate = new SemaphoreSlim(1);
    }
}
