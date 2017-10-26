using System.Collections.Generic;
using System.Drawing;

using Sketchpad.Settings;
using Sketchpad.Data;
using System;

namespace Sketchpad.Render
{
    static class Render
    {
        public static void PaintCanvas(Graphics graphics, CanvasData canvasData)
        {
            graphics.Clear(Color.White);
            
            DrawPolygon(graphics, canvasData.polygon.vertices);
            DrawBoundingBox(graphics, canvasData.polygon.vertices);
            DrawConstraintsIcons(graphics, canvasData);
        }
        public static void DrawPolygon(Graphics graphics, List<Point> polygon)
        {

            for (int i = 0; i < polygon.Count; i++)
            {
                DrawVertex(graphics, polygon[i]);
                MyDrawLine(graphics, new Pen(ProgramSettings.lineColor), polygon[i], polygon[(i + 1) % polygon.Count]);
            }
        }

        public static void DrawBoundingBox(Graphics graphics, List<Point> polygon)
        {
            Rectangle boundingBox = Polygon.GetBoundingBox(polygon);
            graphics.DrawRectangle(new Pen(ProgramSettings.boundingBoxColor), boundingBox);

        }

        private static void DrawConstraintsIcons(Graphics graphics, CanvasData canvasData)
        {
            foreach(Constraint constraint in canvasData.constraints)
            {
                if (constraint.constraintMode == Utils.ConstraintMode.FixedAngle)
                    continue;

                int point1 = constraint.constrainedEdges[0].Item1, point2 = constraint.constrainedEdges[0].Item2;
                int x1 = canvasData.polygon.vertices[point1].X, x2 = canvasData.polygon.vertices[point2].X, y1 = canvasData.polygon.vertices[point1].Y, y2 = canvasData.polygon.vertices[point2].Y;
                Point drawPoint = new Point((x1 + x2) / 2, (y1 + y2) / 2);

                if (constraint.constraintMode == Utils.ConstraintMode.HorizontalEdge)
                    DrawIcon(graphics, new Point(drawPoint.X + 5, drawPoint.Y - 10), new Point(drawPoint.X + 15, drawPoint.Y - 10));
                if (constraint.constraintMode == Utils.ConstraintMode.VerticalEdge)
                    DrawIcon(graphics, new Point(drawPoint.X + 10, drawPoint.Y - 5), new Point(drawPoint.X + 10, drawPoint.Y + 5));

            }
        }
        
        private static void DrawIcon(Graphics graphics, Point startPoint, Point endPoint)
        {
            MyDrawLine(graphics, new Pen(ProgramSettings.iconColor), startPoint, endPoint);
        }

        public static void DrawVertex(Graphics graphics, Point vertexCoordinates)
        {
            Rectangle vertex = new Rectangle(vertexCoordinates, ProgramSettings.vertexSize);
            graphics.FillRectangle(ProgramSettings.vertexColor, vertex);
        }

        //TODO Reimpelement with custom Bersenham algorithm
        public static void MyDrawLine(Graphics graphics, Pen pen,Point p1, Point p2)
        {
            graphics.DrawLine(pen, p1, p2);
        }
    }
}
