using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchpad.IO
{
    interface IChangeCanvasData
    {
        void Apply(Parameters parameters);
    }
}
