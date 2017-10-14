using System.Collections.Generic;
using Sketchpad.IO.StatePattern;
using Sketchpad.IO;

namespace Sketchpad.Utils
{
    class CommandsList
    {
        //WE DONT HAVE TO CHECK IF IT IS NULL THANKS TO THIS LIST
        private static Dictionary<BehaviourMode, State> commandDictionary = new Dictionary<BehaviourMode, State>
        {
            { BehaviourMode.VertexAdd, new VertexAdd() },
            { BehaviourMode.VertexMove, new VertexMove() },
            { BehaviourMode.PolygonMove, new PolygonMove() },
            { BehaviourMode.DoNothing, new DoNothing() },
            //{ "", },
        };

        public static State GetCommand(CanvasData commandName)
        {
            return commandDictionary[commandName.behaviourMode];
        }
    }
}
