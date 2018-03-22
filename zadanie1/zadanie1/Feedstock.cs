using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zadanie1
{
#if ABSTRACT_FEEDSTOCKS
    [Flags]
    enum Feedstocks
    {
        A = 1,
        B = 2,
        C = 4
    }
#else
    [Flags]
    enum Feedstocks
    {
        Lead = 1,
        Sulphur = 2,
        Mercury = 4
    }
#endif
}
