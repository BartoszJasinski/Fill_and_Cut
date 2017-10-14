using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchpad.IO.StatePattern
{
    class DoNothing: State
    {
        public override void Change(CanvasData parameters)
        {
            return;
        }
    }
}
