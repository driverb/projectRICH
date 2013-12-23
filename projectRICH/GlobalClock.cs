using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectRICH
{
    public static class GlobalClock
    {
        private static long timer;

        public static void Reset(long time)
        {
            timer = time; 
        }

        public static void Tick(long elapsed)
        {
            timer += elapsed;
        }

        public static long Now { get { return timer; } }
    }
}
