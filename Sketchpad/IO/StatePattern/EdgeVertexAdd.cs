using System.Drawing;

namespace Sketchpad.IO.StatePattern
{
    class EdgeVertexAdd : IChangeCanvasData
    {
        public void Change(CanvasData canvasData)
        {
            AddVertexInTheMiddleOfEdge(canvasData);
        }

        private void AddVertexInTheMiddleOfEdge(CanvasData canvasData)
        {
            int x = (canvasData.polygon.vertices[canvasData.edge.Item1].X + canvasData.polygon.vertices[canvasData.edge.Item2].X) / 2;
            int y = (canvasData.polygon.vertices[canvasData.edge.Item1].Y + canvasData.polygon.vertices[canvasData.edge.Item2].Y) / 2;
            canvasData.polygon.vertices.Insert(canvasData.edge.Item1, new Point(x, y));
        }
    }
}
