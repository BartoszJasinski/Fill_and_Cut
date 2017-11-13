using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace FillCut.Data.StatePattern
{
    class VertexAdd: IChangeCanvasData
    {
        public void Change(CanvasData canvasData)
        {
            AddVertexToPolygon(canvasData.polygons[canvasData.activePolygon].vertices, canvasData.clickCoordinates);
        }

        private void AddVertexToPolygon(List<Point> polygon, Point clickCoordinates)
        {
            if (!polygon.Any((Point p) => { return p.Equals(clickCoordinates); }))
                polygon.Add(clickCoordinates);
        }
    }
}
