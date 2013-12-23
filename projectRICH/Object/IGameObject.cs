using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectRICH.Object
{
    public interface IGameObject
    {
        void Execute(string command);

        void Execute<T1>(string command, T1 t1);
        void Execute<T1, T2>(string command, T1 t1, T2 t2);
        void Execute<T1, T2, T3>(string command, T1 t1, T2 t2, T3 t3);
        void Execute<T1, T2, T3, T4>(string command, T1 t1, T2 t2, T3 t3, T4 t4);

        R Execute<R>(string command);
        R Execute<T1, R>(string command, T1 t1);
        R Execute<T1, T2, R>(string command, T1 t1, T2 t2);
        R Execute<T1, T2, T3, R>(string command, T1 t1, T2 t2, T3 t3);
        R Execute<T1, T2, T3, T4, R>(string command, T1 t1, T2 t2, T3 t3, T4 t4);
    }
}
