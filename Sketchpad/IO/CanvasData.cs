using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Sketchpad.Utils;

namespace Sketchpad.IO
{
    class CanvasData
    {
        public BehaviourMode behaviourMode { get; set; }
        public List<Point> polygon { get; set; }
        public Point clickCoordinates { get; set; }
        public int draggedVertexIndex { get; set; }
        public Point moveCoordinates { get; set; }

        public CanvasData(BehaviourMode behaviourMode, List<Point> polygon, Point clickCoordinates, int draggedVertexIndex)
        {
            this.behaviourMode = behaviourMode;
            this.polygon = polygon;
            this.clickCoordinates = clickCoordinates;
            this.draggedVertexIndex = draggedVertexIndex;
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
            draggedVertexIndex = canvasData.draggedVertexIndex;
        }

        public CanvasData(): this(BehaviourMode.DoNothing)
        { }

        public CanvasData Copy()
        {
            return new CanvasData(this);
        }
        
        //OBSOLATE
        public static CanvasData GetUpdatedCanvasData(CanvasData oldCanvasData, CanvasData newCanvasData)
        {
            return new CanvasData(newCanvasData.behaviourMode, oldCanvasData.polygon, newCanvasData.clickCoordinates, newCanvasData.draggedVertexIndex);
        }

    }
}
