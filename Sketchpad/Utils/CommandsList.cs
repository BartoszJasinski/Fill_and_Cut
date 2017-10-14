using System.Collections.Generic;
using Sketchpad.IO.StatePattern;
using Sketchpad.IO;

namespace Sketchpad.Utils
{
    class CommandsList
    {
        //WE DONT HAVE TO CHECK IF IT IS NULL THANKS TO THIS LIST
        private static Dictionary<BehaviourMode, IChangeCanvasData> commandDictionary = new Dictionary<BehaviourMode, IChangeCanvasData>
        {
            { BehaviourMode.VertexAdd, new VertexAdd() },
            { BehaviourMode.VertexMove, new VertexMove() },
            { BehaviourMode.PolygonMove, new PolygonMove() },
            { BehaviourMode.VertexDelete, new VertexDelete()},
            { BehaviourMode.EdgeVertexAdd, new EdgeVertexAdd() },
            { BehaviourMode.DoNothing, new DoNothing() }
        };

        public static IChangeCanvasData GetCommand(CanvasData commandName)
        {
            return commandDictionary[commandName.behaviourMode];
        }
    }
}
