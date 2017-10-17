using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

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

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                canvasData.clickCoordinates = e.Location;
                return;
            }

            Graphics graphics = canvas.CreateGraphics();
            Point clickCoordinates = e.Location, vertexCoordinates = e.Location;
            
            HandleMouseDown(graphics, canvasData, clickCoordinates);

            Render.Render.PaintCanvas(graphics, canvasData.polygon.vertices);

            graphics.Dispose();
        }

        private void HandleMouseDown(Graphics graphics, CanvasData canvasData, Point clickCoordinates)
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
            Graphics graphics = canvas.CreateGraphics();

            HandleMouseMove(canvasData, e.Location, graphics);

            Render.Render.PaintCanvas(graphics, canvasData.polygon.vertices);


            graphics.Dispose();
        }

        private void HandleMouseMove(CanvasData canvasData, Point clickCoordinates, Graphics graphics)
        {
            if (canvasData.behaviourMode == BehaviourMode.VertexMove)
                //canvasData = GetBehaviorAndCanvasData(canvasData, clickCoordinates);
                canvasData.clickCoordinates = clickCoordinates;
               
            else if(canvasData.behaviourMode == BehaviourMode.PolygonMove)
                //canvasData.clickCoordinates = new Point(canvasData.clickCoordinates.X - clickCoordinates.X, canvasData.clickCoordinates.Y - clickCoordinates.Y);
                canvasData.moveCoordinates = clickCoordinates;

            CommandsList.GetCommand(canvasData).Change(canvasData);
            this.canvasData = canvasData;

        }

        void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            canvasData.behaviourMode = BehaviourMode.DoNothing;
        }

        private void horizontalEdgeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddSingleEdgeConstraint(canvasData, ConstraintMode.HorizontalEdge);
        }

        private void verticalEdgeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddSingleEdgeConstraint(canvasData, ConstraintMode.VerticalEdge);
        }

        private void AddSingleEdgeConstraint(CanvasData canvasData, ConstraintMode constraintMode)
        {
            Tuple<int, int> edge = new Tuple<int, int>(-1, -1);
            if ((edge = Algorithms.FindIfClickedNearEdge(canvasData, canvasData.clickCoordinates)).Equals(new Tuple<int, int>(-1, -1)))
                return;
            else
            {
                List<Tuple<int, int>> constrainedEdges = new List<Tuple<int, int>>();
                constrainedEdges.Add(edge);
                canvasData.constraints.Add(new Constraint(constraintMode, constrainedEdges));
            }
        }
    }
}
