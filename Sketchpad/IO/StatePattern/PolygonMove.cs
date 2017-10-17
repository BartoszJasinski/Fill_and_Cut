using System.Drawing;

namespace Sketchpad.IO.StatePattern
{
    class PolygonMove : IChangeCanvasData
    {
        public void Change(CanvasData canvasData)
        {
            MovePolygon(canvasData);
        }
        private void MovePolygon(CanvasData canvasData)
        {
            for (int i = 0; i < canvasData.polygon.vertices.Count; i++)
            {
                Point newVerticePosition = new Point(canvasData.polygon.vertices[i].X + canvasData.moveCoordinates.X - canvasData.clickCoordinates.X, canvasData.polygon.vertices[i].Y + canvasData.moveCoordinates.Y - canvasData.clickCoordinates.Y);
                canvasData.polygon.vertices[i] = newVerticePosition;

            }
            canvasData.clickCoordinates = canvasData.moveCoordinates;

        }
    }
}
