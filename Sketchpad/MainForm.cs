using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using Sketchpad.IO;
using Sketchpad.Utils;
using Sketchpad.IO.AreaAlgorithms;
using Sketchpad.Data;

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

            Render.Render.PaintCanvas(graphics, canvasData.polygon.vertices);

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
                if ((clickedVertexIndex = Algorithms.FindIfClickedNearVertex(canvasData, clickCoordinates)) == -1)
                {
                    Tuple<int, int> edge = new Tuple<int, int>(-1, -1);
                    if ((edge = Algorithms.FindIfClickedNearEdge(canvasData, clickCoordinates)).Equals(new Tuple<int, int>(-1, -1)))
                        return new CanvasData(canvasData) { behaviourMode = BehaviourMode.VertexAdd, clickCoordinates = clickCoordinates };
                    else
                        return new CanvasData(canvasData) { behaviourMode = BehaviourMode.EdgeVertexAdd, edge = edge };
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
            Render.Render.PaintCanvas(graphics, canvasData.polygon.vertices);

        }

        private void canvas_MouseDoubleClick(object sender, MouseEventArgs e)
        {
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


        

      

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {

        }




    }
}
