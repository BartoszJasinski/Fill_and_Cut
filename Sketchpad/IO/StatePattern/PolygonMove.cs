using System.Drawing;

namespace FillCut.Data.StatePattern
{
    class PolygonMove : IChangeCanvasData
    {
        public void Change(CanvasData canvasData)
        {
            MovePolygon(canvasData);
        }
        private void MovePolygon(CanvasData canvasData)
        {
            for (int i = 0; i < canvasData.polygons[canvasData.activePolygon].vertices.Count; i++)
            {
                Point newVerticePosition = new Point(canvasData.polygons[canvasData.activePolygon].vertices[i].X + canvasData.moveCoordinates.X 
                    - canvasData.clickCoordinates.X, canvasData.polygons[canvasData.activePolygon].vertices[i].Y + canvasData.moveCoordinates.Y - canvasData.clickCoordinates.Y);
                canvasData.polygons[canvasData.activePolygon].vertices[i] = newVerticePosition;

            }
            canvasData.clickCoordinates = canvasData.moveCoordinates;

        }
    }
}
