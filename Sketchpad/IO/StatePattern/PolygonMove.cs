using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Sketchpad.IO.StatePattern
{
    class PolygonMove : State
    {
        public override void Change(CanvasData canvasData)
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
