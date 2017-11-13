using System;
using System.Collections.Generic;
using System.Drawing;

using FillCut.Utils;

namespace FillCut.Data
{
    class CanvasData
    {
        public BehaviourMode behaviourMode { get; set; }
        public List<Polygon> polygons { get; set; } = new List<Polygon>();
        public Point clickCoordinates { get; set; }
        public int clickedVertexIndex { get; set; }
        public Point moveCoordinates { get; set; }
        public Tuple<int, int> clickedEdge { get; set; }
        public int activePolygon { get; set; } = 0; 

        public CanvasData(BehaviourMode behaviourMode, List<Polygon> polygon, Point clickCoordinates, int draggedVertexIndex)
        {
            this.behaviourMode = behaviourMode;
            this.polygons = polygon;
            this.clickCoordinates = clickCoordinates;
            this.clickedVertexIndex = draggedVertexIndex;
        }


        public CanvasData(BehaviourMode behaviourMode, Point clickCoordinates) : this(behaviourMode, new List<Polygon>(new Polygon[] { new Polygon() }), clickCoordinates, -1)
        { }

        public CanvasData(BehaviourMode doNothing): this(doNothing, new List<Polygon>(new Polygon[] { new Polygon() }), new Point(-1, -1), -1)
        { }

        public CanvasData(CanvasData canvasData)
        {
            behaviourMode = canvasData.behaviourMode;
            polygons = canvasData.polygons;
            clickCoordinates= canvasData.clickCoordinates;
            clickedVertexIndex = canvasData.clickedVertexIndex;

        }

        public CanvasData(): this(BehaviourMode.DoNothing)
        { }





    }
}
