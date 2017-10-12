using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Shapes;

namespace Sketchpad
{
    public partial class MainForm : Form
    {
        private List<Point> polygon = new List<Point>();
        private Size vertexSize = new Size(3, 3);
        private Brush vertexColor = Brushes.Red, lineColor = Brushes.LightGreen, boundingBoxColor = Brushes.LightBlue;
        private int spaceBetweenPolygonAndBoundingBox = 10;

        private bool dragVertex = false; //REIMPLEMENT
        private int draggedVertexIndex = -1;
        private bool dragPolygon = false;
        private Point mouseDownCoordinates;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //this.canvas.MouseDown += this.canvas_MouseDown;
            //this.canvas.MouseUp += this.canvas_MouseUp;
            //this.canvas.MouseMove += this.canvas_MouseMove;
        }

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDownCoordinates = e.Location;

            Graphics graphics = canvas.CreateGraphics();
            Point clickCoordinates = e.Location, vertexCoordinates = e.Location;

            HandleClick(graphics, polygon, clickCoordinates);
            
            graphics.Dispose();
          
        }

        void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            Graphics graphics = canvas.CreateGraphics();
            if (dragVertex)
                ChangeVertexPosition(draggedVertexIndex, e.Location);
            else if(dragPolygon)
                ChangePolygonPosition(e.Location);
            else
                return;
            PaintCanvas(graphics, polygon);

            graphics.Dispose();
        }

        private void ChangePolygonPosition(Point location)
        {
            throw new NotImplementedException();
        }

        private void ChangeVertexPosition(int draggedVertexIndex, Point newVertex)
        {
            polygon[draggedVertexIndex] = newVertex;
            //for(int i = 0; i < polygon.Count; i++)
            //{
            //    if (polygon[i].Equals(vertice))
            //        polygon[i] = newVertice;
            //}
            //foreach(var point in polygon.Where(p => p.X == vertice.X/* && p.Y == vertice.Y */))
            //{
            //    point.X = newVertice.X;
            //    point.Y = newVertice.Y;
            //}
            //List<Point> tmp_polygon = polygon.Where(p => p.X == vertice.X/* && p.Y == vertice.Y */).ToList();

            //    tmp_polygon.ForEach(p => /*{*/ p.X = newVertice.X/*; p.Y = newVertice.Y; }*/);
        }

        void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            //this._mouseLocation = e.Location;
            dragVertex = false;
            dragPolygon = false;
        }

        //void canvas_MouseDown(object sender, MouseEventArgs e)
        //{
        //    //Check if you've left-clicked if you want
        //    this._mouseLocation = e.Location;
        //}

        private void canvas_MouseClick(object sender, MouseEventArgs e)
        {
         
        }

        private void PaintCanvas(Graphics graphics, List<Point> polygon)
        {
            graphics.Clear(Color.White);

            DrawPolygon(graphics, polygon);
            DrawBoundingBox(graphics, polygon);
        }

        private void HandleClick(Graphics graphics, List<Point> polygon, Point clickCoordinates)
        {
            if ((draggedVertexIndex = FindIfClickedNearVertex(clickCoordinates)) == -1)
            {
                AddVertexToPolygon(polygon, clickCoordinates);
                PaintCanvas(graphics, polygon);
            }
            else
            {
                dragVertex = true;
                return;
            }

            if ((CheckIfClickedInsideBoundingBox(GetBoundingBox(polygon), clickCoordinates)))
                dragPolygon = true;
            

        }

        private bool CheckIfClickedInsideBoundingBox(System.Drawing.Rectangle boundingBox, Point clickCoordinates)
        {
            return boundingBox.Contains(clickCoordinates);
        }

        private System.Drawing.Rectangle GetBoundingBox(List<Point> polygon)
        {

            int maxX = polygon.Max(p => p.X), maxY = polygon.Max(p => p.Y), minX = polygon.Min(p => p.X), minY = polygon.Min(p => p.Y);
            System.Drawing.Rectangle boundingBox = new System.Drawing.Rectangle(minX - spaceBetweenPolygonAndBoundingBox / 2, minY - spaceBetweenPolygonAndBoundingBox / 2,
                maxX - minX + spaceBetweenPolygonAndBoundingBox, maxY - minY + spaceBetweenPolygonAndBoundingBox);

            return boundingBox;
        }

        private void DrawBoundingBox(Graphics graphics, List<Point> polygon)
        {
            System.Drawing.Rectangle boundingBox = GetBoundingBox(polygon);
            graphics.DrawRectangle(new Pen(boundingBoxColor), boundingBox);

        }

        private void AddVertexToPolygon(List<Point> polygon, Point clickCoordinates)
        {
            if (!polygon.Any((Point p) => { return p.Equals(clickCoordinates); }))
                polygon.Add(clickCoordinates);
        }

        private int FindIfClickedNearVertex(Point clickCoordinates)
        {
            // what if at least two vertexes are <= selectVertexAreaSize
            int selectVertexAreaSize = 5; // variable to store size of area if clicked to select vertex in that area 
            List<Point> vertexesNearClick = polygon.FindAll((Point p) => { return Math.Abs(p.X - clickCoordinates.X) <= selectVertexAreaSize && Math.Abs(p.Y - clickCoordinates.Y) <= selectVertexAreaSize; });
            int indexForMinValue = 0;
            //Searches for vertex which is nearest to click
            for (int i = 0; i < vertexesNearClick.Count; i++)
            {
                if (vertexesNearClick[indexForMinValue].X - clickCoordinates.X + vertexesNearClick[indexForMinValue].Y - clickCoordinates.Y >
                    vertexesNearClick[i].X - clickCoordinates.X + vertexesNearClick[i].Y - clickCoordinates.Y)
                    indexForMinValue = i;
            }

            if (vertexesNearClick.Count > 0)
                return polygon.IndexOf(vertexesNearClick[indexForMinValue]);
            else
                return -1;
        }


        private void DrawPolygon(Graphics graphics, List<Point> polygon)
        {
            
            for (int i = 0; i < polygon.Count; i++)
            {
                DrawVertex(graphics, polygon[i]);
                MyDrawLine(graphics, polygon[i], polygon[(i + 1) % polygon.Count]);
            }
        }

        private void DrawVertex(Graphics graphics, Point vertexCoordinates)
        {
            System.Drawing.Rectangle vertex = new System.Drawing.Rectangle(vertexCoordinates, vertexSize);
            graphics.FillRectangle(vertexColor, vertex);
        }

        private void canvas_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            FindIfClickedNearVertex(new Point(e.X, e.Y));
        }

        //TODO Reimpelement with custom Bersenham algorithm
        private void MyDrawLine(Graphics graphics, Point p1, Point p2)
        {
            graphics.DrawLine(new Pen(lineColor), p1, p2);
        }

    }
}
