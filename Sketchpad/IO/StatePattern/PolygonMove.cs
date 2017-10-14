using System.Drawing;

namespace Sketchpad.IO.StatePattern
{
    class PolygonMove : IChangeCanvasData
    {
        public void Change(CanvasData canvasData)
        {
            for(int i = 0; i < canvasData.polygon.Count; i++)
            {
                Point newVerticePosition = new Point(canvasData.polygon[i].X + canvasData.moveCoordinates.X - canvasData.clickCoordinates.X, canvasData.polygon[i].Y + canvasData.moveCoordinates.Y - canvasData.clickCoordinates.Y);
                canvasData.polygon[i] = newVerticePosition;

            }
            canvasData.clickCoordinates = canvasData.moveCoordinates;
        }
    }
}
