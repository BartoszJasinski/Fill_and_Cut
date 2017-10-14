using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchpad.IO.StatePattern
{
    class VertexAdd: State
    {
        public override void Change(CanvasData parameters)
        {
            AddVertexToPolygon(parameters.polygon, parameters.clickCoordinates);
        }

        private void AddVertexToPolygon(List<Point> polygon, Point clickCoordinates)
        {
            if (!polygon.Any((Point p) => { return p.Equals(clickCoordinates); }))
                polygon.Add(clickCoordinates);
        }
    }
}
