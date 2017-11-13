using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillCut.Utils
{
    class NoVertexesException: Exception
    {
        public NoVertexesException(string message): base(message)
        {
            
        }
    }
}
