﻿using System.Collections.Generic;
using System.Drawing;

using Sketchpad.Utils;
using Sketchpad.Data;
using System;

namespace Sketchpad.IO
{
    class CanvasData
    {
        public BehaviourMode behaviourMode { get; set; }
        public Polygon polygon { get; set; }
        public Point clickCoordinates { get; set; }
        public int clickedVertexIndex { get; set; }
        public Point moveCoordinates { get; set; }
        public Tuple<int, int> edge { get; set; }

        public CanvasData(BehaviourMode behaviourMode, Polygon polygon, Point clickCoordinates, int draggedVertexIndex)
        {
            this.behaviourMode = behaviourMode;
            this.polygon = polygon;
            this.clickCoordinates = clickCoordinates;
            this.clickedVertexIndex = draggedVertexIndex;
        }

        public CanvasData(BehaviourMode behaviourMode, Polygon polygon, Point clickCoordinates): this(behaviourMode, polygon, clickCoordinates, -1)
        { }

        public CanvasData(BehaviourMode behaviourMode, Point clickCoordinates) : this(behaviourMode, new Polygon(), clickCoordinates, -1)
        { }

        public CanvasData(BehaviourMode doNothing): this(doNothing, new Polygon(), new Point(-1, -1))
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
