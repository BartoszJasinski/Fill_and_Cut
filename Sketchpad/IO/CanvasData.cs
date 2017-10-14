using System.Collections.Generic;
using System.Drawing;
using Sketchpad.Utils;

namespace Sketchpad.IO
{
    class CanvasData
    {
        public BehaviourMode behaviourMode { get; set; }
        public List<Point> polygon { get; set; }
        public Point clickCoordinates { get; set; }
        public int clickedVertexIndex { get; set; }
        public Point moveCoordinates { get; set; }

        public CanvasData(BehaviourMode behaviourMode, List<Point> polygon, Point clickCoordinates, int draggedVertexIndex)
        {
            this.behaviourMode = behaviourMode;
            this.polygon = polygon;
            this.clickCoordinates = clickCoordinates;
            this.clickedVertexIndex = draggedVertexIndex;
        }

        public CanvasData(BehaviourMode behaviourMode, List<Point> polygon, Point clickCoordinates): this(behaviourMode, polygon, clickCoordinates, -1)
        { }

        public CanvasData(BehaviourMode behaviourMode, Point clickCoordinates) : this(behaviourMode, new List<Point>(), clickCoordinates, -1)
        { }

        public CanvasData(BehaviourMode doNothing): this(doNothing, new List<Point>(), new Point(-1, -1))
        { }

        public CanvasData(CanvasData canvasData)
        {
            behaviourMode = canvasData.behaviourMode;
            polygon = canvasData.polygon;
            clickCoordinates= canvasData.clickCoordinates;
            clickedVertexIndex = canvasData.clickedVertexIndex;
        }

        public CanvasData(): this(BehaviourMode.DoNothing)
        { }


        


    }
}
