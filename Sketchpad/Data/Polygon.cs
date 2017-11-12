using System.Collections.Generic;
using System.Linq;
using System.Drawing;

using FillCut.Settings;

namespace FillCut.Data
{
    class Polygon
    {
        public List<Point> vertices { get; set; }

        public Polygon()
        {
            vertices = new List<Point>();
        }


        public static Rectangle GetBoundingBox(List<Point> polygon)
        {
            if (polygon.Count == 0)
                return new Rectangle(0, 0, 0, 0);

            int maxX = polygon.Max(p => p.X), maxY = polygon.Max(p => p.Y), minX = polygon.Min(p => p.X), minY = polygon.Min(p => p.Y);
            Rectangle boundingBox = new Rectangle(minX - ProgramSettings.spaceBetweenPolygonAndBoundingBox / 2, minY - ProgramSettings.spaceBetweenPolygonAndBoundingBox / 2,
                maxX - minX + ProgramSettings.spaceBetweenPolygonAndBoundingBox, maxY - minY + ProgramSettings.spaceBetweenPolygonAndBoundingBox);

            return boundingBox;
        }
    }
}
