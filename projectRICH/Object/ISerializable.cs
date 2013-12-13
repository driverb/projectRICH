using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectRICH.Object
{
    interface ISerializable
    {
        void ReadFrom(System.IO.Stream strm);
        void WriteTo(System.IO.Stream strm);
    }
}
