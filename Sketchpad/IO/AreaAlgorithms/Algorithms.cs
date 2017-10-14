using System;
using System.Collections.Generic;
using System.Drawing;

namespace Sketchpad.IO.AreaAlgorithms
{
    static class Algorithms
    {
        public static int FindIfClickedNearVertex(CanvasData canvasData, Point clickCoordinates)
        {
            // what if at least two vertexes are <= selectVertexAreaSize
            int selectVertexAreaSize = 5; // variable to store size of area if clicked to select vertex in that area 
            List<Point> vertexesNearClick = canvasData.polygon.vertices.FindAll((Point p) => { return Math.Abs(p.X - clickCoordinates.X) <= selectVertexAreaSize && Math.Abs(p.Y - clickCoordinates.Y) <= selectVertexAreaSize; });
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

        public static bool CheckIfClickedInsideBoundingBox(Rectangle boundingBox, Point clickCoordinates)
        {
            return boundingBox.Contains(clickCoordinates);
        }
    }
}
