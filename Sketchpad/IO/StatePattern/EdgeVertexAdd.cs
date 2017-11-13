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
            int x = (canvasData.polygons[canvasData.activePolygon].vertices[canvasData.clickedEdge.Item1].X + canvasData.polygons[canvasData.activePolygon].vertices[canvasData.clickedEdge.Item2].X) / 2;
            int y = (canvasData.polygons[canvasData.activePolygon].vertices[canvasData.clickedEdge.Item1].Y + canvasData.polygons[canvasData.activePolygon].vertices[canvasData.clickedEdge.Item2].Y) / 2;
            canvasData.polygons[canvasData.activePolygon].vertices.Insert(canvasData.clickedEdge.Item2, new Point(x, y));
        }
       
    }
}
