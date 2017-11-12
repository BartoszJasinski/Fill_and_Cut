using System;
using System.Drawing;
using System.Collections.Generic;

using FillCut.Data.AreaAlgorithms;

using FillCut.Utils;

namespace FillCut.Data.StatePattern
{
    class EdgeVertexAdd : IChangeCanvasData
    {
        public void Change(CanvasData canvasData)
        {
            AddVertexInTheMiddleOfEdge(canvasData);
        
        }



        private void AddVertexInTheMiddleOfEdge(CanvasData canvasData)
        {
            int x = (canvasData.polygon.vertices[canvasData.clickedEdge.Item1].X + canvasData.polygon.vertices[canvasData.clickedEdge.Item2].X) / 2;
            int y = (canvasData.polygon.vertices[canvasData.clickedEdge.Item1].Y + canvasData.polygon.vertices[canvasData.clickedEdge.Item2].Y) / 2;
            canvasData.polygon.vertices.Insert(canvasData.clickedEdge.Item2, new Point(x, y));
        }
       
    }
}
