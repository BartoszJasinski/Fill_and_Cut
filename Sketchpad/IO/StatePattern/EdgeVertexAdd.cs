using System.Drawing;


using Sketchpad.Utils;

namespace Sketchpad.Data.StatePattern
{
    class EdgeVertexAdd : IChangeCanvasData
    {
        public void Change(CanvasData canvasData)
        {
            AddVertexInTheMiddleOfEdge(canvasData);
            CanvasData.DeletePossibleEdgeConstraint(canvasData, ConstraintMode.HorizontalEdge, canvasData.clickedEdge, -1);
            CanvasData.DeletePossibleEdgeConstraint(canvasData, ConstraintMode.VerticalEdge, canvasData.clickedEdge, -1);
        }

        private void AddVertexInTheMiddleOfEdge(CanvasData canvasData)
        {
            int x = (canvasData.polygon.vertices[canvasData.clickedEdge.Item1].X + canvasData.polygon.vertices[canvasData.clickedEdge.Item2].X) / 2;
            int y = (canvasData.polygon.vertices[canvasData.clickedEdge.Item1].Y + canvasData.polygon.vertices[canvasData.clickedEdge.Item2].Y) / 2;
            canvasData.polygon.vertices.Insert(canvasData.clickedEdge.Item2, new Point(x, y));
        }
       
    }
}
