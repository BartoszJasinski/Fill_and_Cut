using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchpad.IO.StatePattern
{
    abstract class State
    {
        //protected 
        public abstract void Change(CanvasData canvasData);
    }
}
