using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Sketchpad.IO.StatePattern
{
    class VertexAdd: IChangeCanvasData
    {
        public void Change(CanvasData parameters)
        {
            AddVertexToPolygon(parameters.polygon.vertices, parameters.clickCoordinates);
        }

        private void AddVertexToPolygon(List<Point> polygon, Point clickCoordinates)
        {
            if (!polygon.Any((Point p) => { return p.Equals(clickCoordinates); }))
                polygon.Add(clickCoordinates);
        }
    }
}
