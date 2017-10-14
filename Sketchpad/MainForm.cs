using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Sketchpad.IO;
using Sketchpad.Settings;
using Sketchpad.Utils;

namespace Sketchpad
{
    public partial class MainForm : Form
    {
        // Is it possible not to have this variable (canvasData) and use only 
        private CanvasData canvasData = new CanvasData();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            Graphics graphics = canvas.CreateGraphics();
            Point clickCoordinates = e.Location, vertexCoordinates = e.Location;

            HandleMouseClick(graphics, canvasData, clickCoordinates);
            PaintCanvas(graphics, canvasData.polygon);

            graphics.Dispose();
        }

        private void HandleMouseClick(Graphics graphics, CanvasData canvasData, Point clickCoordinates)
        {
            InputMode inputMode = InputMode.SingleLeftClick;
            if ((ModifierKeys == Keys.Control))
                inputMode = InputMode.CtrlPressed;

            canvasData = GetBehaviorAndCanvasData(canvasData, clickCoordinates, inputMode);
            CommandsList.GetCommand(canvasData).Change(canvasData);
            this.canvasData = canvasData;

            //if ((draggedVertexIndex = FindIfClickedNearVertex(clickCoordinates)) == -1)
            //{
            //    AddVertexToPolygon(polygon, clickCoordinates);
            //    PaintCanvas(graphics, polygon);
            //}
            //else
            //{
            //    dragVertex = true;
            //    return;
            //}

            //if ((CheckIfClickedInsideBoundingBox(GetBoundingBox(polygon), clickCoordinates)))
            //    dragPolygon = true;

        }


        private CanvasData GetBehaviorAndCanvasData(CanvasData canvasData, Point clickCoordinates, InputMode clickMode = InputMode.SingleLeftClick)
        {
            if (clickMode == InputMode.SingleLeftClick)
            {
                int clickedVertexIndex = -1;
                if ((clickedVertexIndex = FindIfClickedNearVertex(clickCoordinates)) == -1)
                    return new CanvasData(canvasData) { behaviourMode = BehaviourMode.VertexAdd, clickCoordinates = clickCoordinates };
                else
                    return new CanvasData(canvasData) { behaviourMode = BehaviourMode.VertexDelete, clickedVertexIndex = clickedVertexIndex };
            }

            if (clickMode == InputMode.CtrlPressed)
            {
                int clickedVertexIndex = -1;
                if ((clickedVertexIndex = FindIfClickedNearVertex(clickCoordinates)) == -1)
                {
                    if (CheckIfClickedInsideBoundingBox(GetBoundingBox(canvasData.polygon), clickCoordinates))
                        return new CanvasData(canvasData) { behaviourMode = BehaviourMode.PolygonMove, clickCoordinates = clickCoordinates };
                    else
                        return new CanvasData(canvasData) { behaviourMode = BehaviourMode.DoNothing };
                }
                else
                    return new CanvasData(canvasData) { behaviourMode = BehaviourMode.VertexMove, clickCoordinates = clickCoordinates, clickedVertexIndex = clickedVertexIndex };
            }

            return new CanvasData(canvasData) { behaviourMode = BehaviourMode.DoNothing };
        }

 

        void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            Graphics graphics = canvas.CreateGraphics();

            HandleMouseMove(canvasData, e.Location, graphics);
            //if (dragVertex)
            //    ChangeVertexPosition(draggedVertexIndex, e.Location);
            //else if (dragPolygon)
            //    ChangePolygonPosition(e.Location);
            //else
            //    return;

            graphics.Dispose();
        }

        private void HandleMouseMove(CanvasData canvasData, Point clickCoordinates, Graphics graphics)
        {
            if (canvasData.behaviourMode == BehaviourMode.VertexMove)
            {
                //canvasData = GetBehaviorAndCanvasData(canvasData, clickCoordinates);
                canvasData.clickCoordinates = clickCoordinates;
                CommandsList.GetCommand(canvasData).Change(canvasData);
                this.canvasData = canvasData;
            }
            else if(canvasData.behaviourMode == BehaviourMode.PolygonMove)
            {
                //canvasData.clickCoordinates = new Point(canvasData.clickCoordinates.X - clickCoordinates.X, canvasData.clickCoordinates.Y - clickCoordinates.Y);
                canvasData.moveCoordinates = clickCoordinates;
                CommandsList.GetCommand(canvasData).Change(canvasData);
                this.canvasData = canvasData;
            }
            PaintCanvas(graphics, canvasData.polygon);

        }

        private void canvas_MouseDoubleClick(object sender, MouseEventArgs e)
        {
        }


        private void ChangePolygonPosition(Point location)
        {
            throw new NotImplementedException();
        }

        //private void ChangeVertexPosition(int draggedVertexIndex, Point newVertex)
        //{
        //    polygon[draggedVertexIndex] = newVertex;
        //    //for(int i = 0; i < polygon.Count; i++)
        //    //{
        //    //    if (polygon[i].Equals(vertice))
        //    //        polygon[i] = newVertice;
        //    //}
        //    //foreach(var point in polygon.Where(p => p.X == vertice.X/* && p.Y == vertice.Y */))
        //    //{
        //    //    point.X = newVertice.X;
        //    //    point.Y = newVertice.Y;
        //    //}
        //    //List<Point> tmp_polygon = polygon.Where(p => p.X == vertice.X/* && p.Y == vertice.Y */).ToList();

        //    //    tmp_polygon.ForEach(p => /*{*/ p.X = newVertice.X/*; p.Y = newVertice.Y; }*/);
        //}

        void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            canvasData.behaviourMode = BehaviourMode.DoNothing;
        }

        private void canvas_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void PaintCanvas(Graphics graphics, List<Point> polygon)
        {
            graphics.Clear(Color.White);

            DrawPolygon(graphics, polygon);
            DrawBoundingBox(graphics, polygon);
        }



        private bool CheckIfClickedInsideBoundingBox(Rectangle boundingBox, Point clickCoordinates)
        {
            return boundingBox.Contains(clickCoordinates);
        }

        private Rectangle GetBoundingBox(List<Point> polygon)
        {
            if(polygon.Count == 0)
                return new Rectangle(0, 0, 0, 0);

            int maxX = polygon.Max(p => p.X), maxY = polygon.Max(p => p.Y), minX = polygon.Min(p => p.X), minY = polygon.Min(p => p.Y);
            Rectangle boundingBox = new Rectangle(minX - ProgramSettings.spaceBetweenPolygonAndBoundingBox / 2, minY - ProgramSettings.spaceBetweenPolygonAndBoundingBox / 2,
                maxX - minX + ProgramSettings.spaceBetweenPolygonAndBoundingBox, maxY - minY + ProgramSettings.spaceBetweenPolygonAndBoundingBox);

            return boundingBox;
        }

        private void DrawBoundingBox(Graphics graphics, List<Point> polygon)
        {
            System.Drawing.Rectangle boundingBox = GetBoundingBox(polygon);
            graphics.DrawRectangle(new Pen(ProgramSettings.boundingBoxColor), boundingBox);

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
            List<Point> vertexesNearClick = canvasData.polygon.FindAll((Point p) => { return Math.Abs(p.X - clickCoordinates.X) <= selectVertexAreaSize && Math.Abs(p.Y - clickCoordinates.Y) <= selectVertexAreaSize; });
            int indexForMinValue = 0;
            //Searches for vertex which is nearest to click
            for (int i = 0; i < vertexesNearClick.Count; i++)
            {
                if (vertexesNearClick[indexForMinValue].X - clickCoordinates.X + vertexesNearClick[indexForMinValue].Y - clickCoordinates.Y >
                    vertexesNearClick[i].X - clickCoordinates.X + vertexesNearClick[i].Y - clickCoordinates.Y)
                    indexForMinValue = i;
            }

            if (vertexesNearClick.Count > 0)
                return canvasData.polygon.IndexOf(vertexesNearClick[indexForMinValue]);
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
            Rectangle vertex = new Rectangle(vertexCoordinates, ProgramSettings.vertexSize);
            graphics.FillRectangle(ProgramSettings.vertexColor, vertex);
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {

        }

 

        //TODO Reimpelement with custom Bersenham algorithm
        private void MyDrawLine(Graphics graphics, Point p1, Point p2)
        {
            graphics.DrawLine(new Pen(ProgramSettings.lineColor), p1, p2);
        }

    }
}
