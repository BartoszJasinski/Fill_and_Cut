using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using FillCut.Data;

namespace FillCut.Render.Algorithms
{
    class PolygonFilling
    {
        public class Vertex
        {
            public Point point { get; set; }
            public int index { get; set; }

            public Vertex(Point point, int index)
            {
                this.point = point;
                this.index = index;
            }

        }

        public class AETData
        {
            public int yMax { get; set; }
            public double x { get; set; }
            public double mInverse { get; set; }
            public int firstVertexIndex { get; set; }
            public int secondVertexIndex { get; set; }
        }

        public static void ScanLineFillVertexSort(PaintEventArgs e, CanvasData canvasData)
        {
            List<Vertex> polygonVertexes = new List<Vertex>();
            for (int i = 0; i < canvasData.polygons[canvasData.activePolygon].vertices.Count; i++)
                polygonVertexes.Add(new Vertex(canvasData.polygons[canvasData.activePolygon].vertices[i], i));

            polygonVertexes = polygonVertexes.OrderBy(v => v.point.Y).ToList();
            int yMin = polygonVertexes[0].point.Y, yMax = polygonVertexes.Last().point.Y;

            List<AETData> AET = new List<AETData>();

            for (int y = yMin + 1; y < yMax; y++)
            {
                List<Vertex> vertexesLyingOnScanLine = polygonVertexes.FindAll(vertex => vertex.point.Y == (y - 1));
                foreach (Vertex vertex in vertexesLyingOnScanLine)
                {
                    int previousVertexIndex = MathMod(vertex.index - 1, polygonVertexes.Count);
                    Vertex previousVertex = polygonVertexes.Find(x => x.index == previousVertexIndex);
                    if (previousVertex.point.Y >= vertex.point.Y)
                    {
                        double mInverse = ((double)previousVertex.point.X - (double)vertex.point.X) / ((double)previousVertex.point.Y - (double)vertex.point.Y);
                        //mInverse = double.IsInfinity(mInverse) ? 0 : mInverse;
                        AET.Add(new AETData {yMax = previousVertex.point.Y, x = vertex.point.X, mInverse = mInverse, firstVertexIndex = previousVertexIndex, secondVertexIndex = vertex.index});
                    }
                    else
                        AET.RemoveAll(x => (x.firstVertexIndex == vertex.index && x.secondVertexIndex == previousVertexIndex) || (x.firstVertexIndex == previousVertexIndex && x.secondVertexIndex == vertex.index));

                    int nextVertexIndex = MathMod(vertex.index + 1, polygonVertexes.Count);
                    Vertex nextVertex = polygonVertexes.Find(x => x.index == nextVertexIndex);
                    if (nextVertex.point.Y >= vertex.point.Y)
                    {
                        double mInverse = ((double)nextVertex.point.X - (double)vertex.point.X) / ((double)nextVertex.point.Y - (double)vertex.point.Y);
                        //mInverse = double.IsInfinity(mInverse) ? 0 : mInverse;
                        AET.Add(new AETData { yMax = nextVertex.point.Y, x = vertex.point.X, mInverse = mInverse, firstVertexIndex = nextVertexIndex, secondVertexIndex = vertex.index });
                    }
                    else
                        AET.RemoveAll(x => (x.firstVertexIndex == vertex.index && x.secondVertexIndex == nextVertexIndex) || (x.firstVertexIndex == nextVertexIndex && x.secondVertexIndex == vertex.index));
                }

                AET.OrderBy(x => x.x);
                for (int i = 0; i < AET.Count / 2; i++)
                    e.Graphics.DrawLine(new Pen(Color.Yellow), new Point((int)AET[2 * i].x, y), new Point((int)AET[2 * i + 1].x, y));

                for (int i = 0; i < AET.Count; i++)
                {
                   
                    AET[i].x += AET[i].mInverse;
                    if (double.IsInfinity(AET[i].mInverse))
                        AET[i].x = polygonVertexes[AET[i].secondVertexIndex].point.X;
                }

            }
        }



        private static void SetPixel(PaintEventArgs e, Brush brush, Point point)
        {
            e.Graphics.FillRectangle(brush, point.X, point.Y, 1, 1);
        }

        static int MathMod(int a, int b)
        {
            return (Math.Abs(a * b) + a) % b;
        }
    }
}
