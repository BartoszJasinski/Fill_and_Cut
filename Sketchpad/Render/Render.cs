using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using Sketchpad.Settings;
using Sketchpad.Data;

namespace Sketchpad.Render
{
    static class Render
    {
        public static void PaintCanvas(Graphics graphics, List<Point> polygon)
        {
            graphics.Clear(Color.White);

            DrawPolygon(graphics, polygon);
            DrawBoundingBox(graphics, polygon);
        }

        public static void DrawBoundingBox(Graphics graphics, List<Point> polygon)
        {
            Rectangle boundingBox = Polygon.GetBoundingBox(polygon);
            graphics.DrawRectangle(new Pen(ProgramSettings.boundingBoxColor), boundingBox);

        }

        public static void DrawPolygon(Graphics graphics, List<Point> polygon)
        {

            for (int i = 0; i < polygon.Count; i++)
            {
                DrawVertex(graphics, polygon[i]);
                MyDrawLine(graphics, polygon[i], polygon[(i + 1) % polygon.Count]);
            }
        }

        public static void DrawVertex(Graphics graphics, Point vertexCoordinates)
        {
            Rectangle vertex = new Rectangle(vertexCoordinates, ProgramSettings.vertexSize);
            graphics.FillRectangle(ProgramSettings.vertexColor, vertex);
        }

        //TODO Reimpelement with custom Bersenham algorithm
        public static void MyDrawLine(Graphics graphics, Point p1, Point p2)
        {
            graphics.DrawLine(new Pen(ProgramSettings.lineColor), p1, p2);
        }
    }
}
