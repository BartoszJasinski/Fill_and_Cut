using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

using FillCut.Data;
using FillCut.Utils;
using FillCut.Data.AreaAlgorithms;

namespace FillCut
{
    public partial class MainForm : Form
    {
        private CanvasData canvasData = new CanvasData();

        private void InitCanvasData()
        {
            canvasData.polygon.vertices.Add(new Point(100, 100));
            canvasData.polygon.vertices.Add(new Point(150, 100));
            canvasData.polygon.vertices.Add(new Point(300, 200));
            canvasData.polygon.vertices.Add(new Point(200, 150));
            canvasData.polygon.vertices.Add(new Point(500, 500));
        }

        public MainForm()
        {
            InitializeComponent();
            InitCanvasData();
        }

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                canvasData.clickCoordinates = e.Location;
                return;
            }

            HandleMouseDown(canvasData, e.Location);

            //PaintCanvas();
            canvas.Invalidate();
        }

        private void HandleMouseDown(CanvasData canvasData, Point clickCoordinates)
        {
            InputMode inputMode = InputMode.SingleLeftClick;
            if ((ModifierKeys == Keys.Control))
                inputMode = InputMode.CtrlPressed;

            canvasData = GetBehaviorAndCanvasData(canvasData, clickCoordinates, inputMode);
            CommandsList.GetCommand(canvasData).Change(canvasData);
            this.canvasData = canvasData;

        }


        private CanvasData GetBehaviorAndCanvasData(CanvasData canvasData, Point clickCoordinates, InputMode clickMode = InputMode.SingleLeftClick)
        {
            if (clickMode == InputMode.SingleLeftClick)
            {
                int clickedVertexIndex = -1;
                if ((clickedVertexIndex = Algorithms.FindIfClickedNearVertex(canvasData, clickCoordinates)) == -1)
                {
                    Tuple<int, int> edge = new Tuple<int, int>(-1, -1);
                    if ((edge = Algorithms.FindIfClickedNearEdge(canvasData, clickCoordinates)).Equals(new Tuple<int, int>(-1, -1)))
                        return new CanvasData(canvasData) { behaviourMode = BehaviourMode.VertexAdd, clickCoordinates = clickCoordinates };
                    else
                        return new CanvasData(canvasData) { behaviourMode = BehaviourMode.EdgeVertexAdd, clickedEdge = edge };
                }
                else
                    return new CanvasData(canvasData) { behaviourMode = BehaviourMode.VertexDelete, clickedVertexIndex = clickedVertexIndex };
            }

            if (clickMode == InputMode.CtrlPressed)
            {
                int clickedVertexIndex = -1;
                if ((clickedVertexIndex = Algorithms.FindIfClickedNearVertex(canvasData, clickCoordinates)) == -1)
                {
                    if (Algorithms.CheckIfClickedInsideBoundingBox(Polygon.GetBoundingBox(canvasData.polygon.vertices), clickCoordinates))
                        return new CanvasData(canvasData) { behaviourMode = BehaviourMode.PolygonMove, clickCoordinates = clickCoordinates, moveCoordinates = clickCoordinates };
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
            HandleMouseMove(canvasData, e.Location);

            //PaintCanvas();
            canvas.Invalidate();
        }

        private void HandleMouseMove(CanvasData canvasData, Point clickCoordinates)
        {
            if (canvasData.behaviourMode == BehaviourMode.VertexMove)
            {
                canvasData.clickCoordinates = clickCoordinates;
                CommandsList.GetCommand(canvasData).Change(canvasData);
            }
            else if (canvasData.behaviourMode == BehaviourMode.PolygonMove)
            {
                canvasData.moveCoordinates = clickCoordinates;
                CommandsList.GetCommand(canvasData).Change(canvasData);
            }

            this.canvasData = canvasData;

        }

        void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            canvasData.behaviourMode = BehaviourMode.DoNothing;
        }
        
        //private void PaintCanvas()
        //{
        //    Graphics graphics = canvas.CreateGraphics();
        //    Render.Render.PaintCanvas(graphics, canvasData.polygon.vertices);

        //    graphics.Dispose();
        //}

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            
            Render.Render.PaintCanvas(e, canvasData);
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PolygonColorPictureBox_Click(object sender, EventArgs e)
        {
            PolygonColorColorDialog.ShowDialog();
            PolygonColorPictureBox.BackColor = PolygonColorColorDialog.Color;
        }

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

        private void ScanLineFillVertexSort(CanvasData canvasData)
        {
            List<Vertex> polygonVertexes = new List<Vertex>();
            for (int i = 0; i < canvasData.polygon.vertices.Count; i++)
                polygonVertexes.Add(new Vertex(canvasData.polygon.vertices[i], i));
            
            polygonVertexes.OrderBy(v => v.point.Y);
            int yMin = polygonVertexes[0].point.Y, yMax = polygonVertexes.Last().point.Y;

            for (int i = yMin; i < yMax; i++) ;// TODO IMPLEMENT
        }


        //Algorithm for clipping polygon
        private void SutherlandHodgmanAlgorithm()
        {
            if (CheckIfPolygonIsConvex(canvasData)) ;
        }

        private bool CheckIfPolygonIsConvex(CanvasData canvasData)
        {
            if (canvasData.polygon.vertices.Count < 3)
                throw new Utils.NoVertexesException("Too small amount of vertices");

            Point firstPoint = canvasData.polygon.vertices[0], secondPoint = canvasData.polygon.vertices[1], thirdPoint = canvasData.polygon.vertices[2];
            int dx1 = secondPoint.X - firstPoint.X, dy1 = secondPoint.Y - firstPoint.X, dx2 = thirdPoint.X - secondPoint.Y, dy2 = thirdPoint.Y - secondPoint.Y;

            System.Windows.Vector firstVector = new System.Windows.Vector(dx1, dy1), secondVector = new System.Windows.Vector(dx2, dy2);
            double crossProduct = System.Windows.Vector.CrossProduct(firstVector, secondVector);

            for(int i = 0; i < canvasData.polygon.vertices.Count; i++)
            {
                Point nextFirstPoint = canvasData.polygon.vertices[i], nextSecondPoint = canvasData.polygon.vertices[(i + 1) % canvasData.polygon.vertices.Count], 
                    nextThirdPoint = canvasData.polygon.vertices[(i + 2) % canvasData.polygon.vertices.Count];
                int nextDx1 = nextSecondPoint.X - nextFirstPoint.X, nextDy1 = nextSecondPoint.Y - nextFirstPoint.X, nextDx2 = nextThirdPoint.X - nextSecondPoint.Y, 
                    nextDy2 = nextThirdPoint.Y - nextSecondPoint.Y;

                System.Windows.Vector nextFirstVector = new System.Windows.Vector(nextDx1, nextDy1), nextSecondVector = new System.Windows.Vector(nextDx2, nextDy2);
                if (Math.Sign(crossProduct) != Math.Sign(System.Windows.Vector.CrossProduct(nextFirstVector, nextSecondVector)))
                    return false;
            }


            return true;
        }

    }
}
