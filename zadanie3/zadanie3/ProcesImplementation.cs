using System;
using System.Linq;
using System.Threading;

namespace zadanie3
{
    public class ProcesImplementation : ProcesInSystem
    {
        private bool off;
        private bool play;
        private int randomNum;
        protected override void Computation()
        {
            Thread.Sleep(100);

            if (RoundsCounter % 20 == 0) // co 20 obiegów następuje reset, każdy obieg trwa 0,1s wiec runda gry w której na początku ustalani są grający trwa 2s 
            {
                off = false;
            }

            if(off)
                return;

            if (RoundsCounter % 2 == 0) // rundy parzyste, wysyłanie losowego numeru, otrzymanie informacji od zwycięzców
            {
                if (ReceivedMessages.Any(o => o != null))
                {
                    // dostałem info o tym że ktoś gra, w tej rundzie nie zagram
                    off = true;
                    return;
                }

                randomNum = GetRandomNumber();

                for (int i = 0; i < ToSendMessages.Length; i++)
                {
                    ToSendMessages[i] = randomNum;
                }
            }
            else // rundy nieparzyste, wysyłanie informacji o zwycięzcach, otrhumanie informacji o numerach
            {
                if (ReceivedMessages.Where(o => o != null).All(o => (int) o < randomNum))
                {
                    // mogę grać
                    off = true;
                    play = true;
                    for (int i = 0; i < ToSendMessages.Length; i++)
                    {
                        ToSendMessages[i] = true;
                    }
                }
            }
        }

        protected override string DisplayStatus(string myName)
        {
            //string sep = ", ";
            //return $"Runda {RoundsCounter}, Jankiel {myName}, wysłał {String.Join(sep, ToSendMessages.Select(o => o?.ToString()??"").ToArray() )}, otrzymał  {String.Join(sep, ReceivedMessages.Select(o => o?.ToString()??"").ToArray())}\n" ;

            if (play)
            {
                play = false;
                return $"{myName} {RoundsCounter / 20}\n";
            }

            return "";
        }
    }
}