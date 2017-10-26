using System;
using System.Collections.Generic;
using System.Drawing;

using Sketchpad.Settings;

namespace Sketchpad.Data.AreaAlgorithms
{
    static class Algorithms
    {
        public static int FindIfClickedNearVertex(CanvasData canvasData, Point clickCoordinates)
        {
            // what if at least two vertexes are <= selectVertexAreaSize
            List<Point> vertexesNearClick = canvasData.polygon.vertices.FindAll((Point p) => { return Math.Abs(p.X - clickCoordinates.X) <= ProgramSettings.selectVertexAreaSize && Math.Abs(p.Y - clickCoordinates.Y) <= ProgramSettings.selectVertexAreaSize; });
            int indexForMinValue = 0;
            //Searches for vertex which is nearest to click
            for (int i = 0; i < vertexesNearClick.Count; i++)
            {
                if (vertexesNearClick[indexForMinValue].X - clickCoordinates.X + vertexesNearClick[indexForMinValue].Y - clickCoordinates.Y >
                    vertexesNearClick[i].X - clickCoordinates.X + vertexesNearClick[i].Y - clickCoordinates.Y)
                    indexForMinValue = i;
            }

            if (vertexesNearClick.Count > 0)
                return canvasData.polygon.vertices.IndexOf(vertexesNearClick[indexForMinValue]);
            else
                return -1;
        }

        public static Tuple<int, int> FindIfClickedNearEdge(CanvasData canvasData, Point clickCoordinates)
        { 
            if(canvasData.polygon.vertices.Count < 2)
                return new Tuple<int, int>(-1, -1);

            // what if 2 or more edges are <= selectEdgeAreaSize
            List<Point> vertices = canvasData.polygon.vertices;

            for (int i = 0; i < vertices.Count; i++)
            {
                int x0 = clickCoordinates.X, x1 = vertices[i].X, x2 = vertices[(i + 1) % vertices.Count].X, 
                    y0 = clickCoordinates.Y, y1 = vertices[i].Y, y2 = vertices[(i + 1) % vertices.Count].Y;
                double distance = Math.Abs((y2 - y1) * x0 - (x2 - x1) * y0 + x2 * y1 - y2 * x1) / Math.Sqrt((y2 - y1) * (y2 - y1) + (x2 - x1) * (x2 - x1));

                if (distance < ProgramSettings.selectEdgeAreaSize)
                    return new Tuple<int, int>(i, (i + 1) % vertices.Count);
            }

            return new Tuple<int, int>(-1, -1);
        }

        public static bool CheckIfClickedInsideBoundingBox(Rectangle boundingBox, Point clickCoordinates)
        {
            return boundingBox.Contains(clickCoordinates);
        }

        public static int mod(int x, int m)
        {
            return (x % m + m) % m;
        }
    }
}
