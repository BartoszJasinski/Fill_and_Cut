﻿using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using FillCut.Settings;
using FillCut.Data;
using System;

namespace FillCut.Render
{
    static class Render
    {
        public static void PaintCanvas(PaintEventArgs e, CanvasData canvasData)
        {
            Graphics graphics = e.Graphics;
            //graphics.Clear(Color.White);


            //SetPixel(e, Brushes.Black, new Point(470, 234));
            //SetPixel(e, Brushes.Black, new Point(471, 234));
            //SetPixel(e, Brushes.Black, new Point(470, 235));
            //SetPixel(e, Brushes.Black, new Point(471, 235));

            LambertModel(e);

            //foreach (Polygon polygon in canvasData.polygons)
            //{
            //    DrawPolygon(e, polygon.vertices);
            //    DrawBoundingBox(e, polygon.vertices);
            //}
        }

        private static void LambertModel(PaintEventArgs e)
        {
            Color lightColor = Color.Green, objectColor = Color.Blue;

            Tuple<double, double, double> N = new Tuple<double, double, double>(0, 0, 1), L = new Tuple<double, double, double>(0, 0, 1);

            for (int x = e.ClipRectangle.Left; x < e.ClipRectangle.Right; x++)
                for (int y = e.ClipRectangle.Top; y < e.ClipRectangle.Bottom; y++)
                {
                    Brush brush = new SolidBrush(Color.FromArgb(255, LambertColor(lightColor.R, objectColor.R, N, L),
                        LambertColor(lightColor.G, objectColor.G, N, L), LambertColor(lightColor.B, objectColor.B, N, L)));
                    SetPixel(e, brush, new Point(x, y));
                }
        }

        private static double ConvertColorsFromIntToDouble(int colorComponent)
        {
            return (colorComponent - 127) / 127;
        }

        private static int ConvertColorsFromDoubleToInt(double colorComponent)
        {
            return (int)(colorComponent + 1) / 2 * 255;
        }

        private static int LambertColor(int lightColorComponent, int objectColorComponent, Tuple<double, double, double> normalVector, Tuple<double, double, double> lightUnitVector)
        {
            return ConvertColorsFromDoubleToInt(ConvertColorsFromIntToDouble(lightColorComponent) * ConvertColorsFromIntToDouble(objectColorComponent) * Cos(normalVector, lightUnitVector)); 
        }

        private static double Cos(Tuple<double, double, double> N, Tuple<double, double, double> L)
        {
            return N.Item1 * L.Item1 + N.Item2 + L.Item2 + N.Item3 * L.Item3;
        }

        private static void SetPixel(PaintEventArgs e, Brush brush, Point point)
        {
            e.Graphics.FillRectangle(brush, point.X, point.Y, 1, 1);
        }

        public static void DrawPolygon(PaintEventArgs e, List<Point> polygon)
        {

            for (int i = 0; i < polygon.Count; i++)
            {
                DrawVertex(e, polygon[i]);
                MyDrawLine(e, new Pen(ProgramSettings.lineColor), polygon[i], polygon[(i + 1) % polygon.Count]);
            }
        }

        public static void DrawBoundingBox(PaintEventArgs e, List<Point> polygon)
        {
            Rectangle boundingBox = Polygon.GetBoundingBox(polygon);
            e.Graphics.DrawRectangle(new Pen(ProgramSettings.boundingBoxColor), boundingBox);

        }


        
        private static void DrawIcon(PaintEventArgs e, Point startPoint, Point endPoint)
        {
            MyDrawLine(e, new Pen(ProgramSettings.iconColor), startPoint, endPoint);
        }

        public static void DrawVertex(PaintEventArgs e, Point vertexCoordinates)
        {
            Rectangle vertex = new Rectangle(vertexCoordinates, ProgramSettings.vertexSize);
            e.Graphics.FillRectangle(ProgramSettings.vertexColor, vertex);
        }

        public static void MyDrawLine(PaintEventArgs e, Pen pen,Point p1, Point p2)
        {

            //line(e, p1.X, p1.Y, p2.X, p2.Y, pen.Brush);
            e.Graphics.DrawLine(pen, p1, p2);
        }

        //Custom Bersenham algorithm
        public static void line(PaintEventArgs e, int x, int y, int x2, int y2, Brush brush)
        {
            int w = x2 - x;
            int h = y2 - y;
            int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
            if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
            if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
            if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
            int longest = Math.Abs(w);
            int shortest = Math.Abs(h);
            if (!(longest > shortest))
            {
                longest = Math.Abs(h);
                shortest = Math.Abs(w);
                if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
                dx2 = 0;
            }
            int numerator = longest >> 1;
            for (int i = 0; i <= longest; i++)
            {
                e.Graphics.FillRectangle(brush, x, y, 1, 1);
                numerator += shortest;
                if (!(numerator < longest))
                {
                    numerator -= longest;
                    x += dx1;
                    y += dy1;
                }
                else
                {
                    x += dx2;
                    y += dy2;
                }
            }
        }
    }
}
