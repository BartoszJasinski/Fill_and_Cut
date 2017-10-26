using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

using Sketchpad.Data;
using Sketchpad.Utils;
using Sketchpad.Data.AreaAlgorithms;

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

        private void horizontalEdgeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tuple<int, int> edge = AddSingleEdgeConstraint(canvasData, ConstraintMode.HorizontalEdge);

            if (edge.Equals(new Tuple<int, int>(-1, -1)))
                return;

            //VertexMove vertexMove = new VertexMove();
            //vertexMove.Change(canvasData);
            canvasData.polygon.vertices[edge.Item1] = canvasData.clickCoordinates;
            canvasData.polygon.vertices[edge.Item2] = new Point(canvasData.polygon.vertices[edge.Item2].X, canvasData.clickCoordinates.Y);

        }

        private void verticalEdgeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tuple<int, int> edge = AddSingleEdgeConstraint(canvasData, ConstraintMode.VerticalEdge);

            if (edge.Equals(new Tuple<int, int>(-1, -1)))
                return;

            //VertexMove vertexMove = new VertexMove();
            //vertexMove.Change(canvasData);
            canvasData.polygon.vertices[edge.Item1] = canvasData.clickCoordinates;
            canvasData.polygon.vertices[edge.Item2] = new Point(canvasData.clickCoordinates.X, canvasData.polygon.vertices[edge.Item2].Y);
        }

        private Tuple<int, int> AddSingleEdgeConstraint(CanvasData canvasData, ConstraintMode constraintMode)
        {
            Tuple<int, int> edge = new Tuple<int, int>(-1, -1);
            if (!(edge = Algorithms.FindIfClickedNearEdge(canvasData, canvasData.clickCoordinates)).Equals(new Tuple<int, int>(-1, -1)))
            {
                DeletePossibleEdgeConstraint(constraintMode, edge, -1);
                List<Tuple<int, int>> constrainedEdges = new List<Tuple<int, int>>();
                constrainedEdges.Add(edge);
                Constraint constraint = new Constraint(constraintMode, constrainedEdges);
                if (IfConstraintCanBeAdded(canvasData, constraint))
                    canvasData.constraints.Add(constraint);
                else
                    return new Tuple<int, int>(-1, -1);
            }

            return edge;
        }

        //BUGS HERE
        private bool IfConstraintCanBeAdded(CanvasData canvasData, Constraint constraint)
        {
            int oneVertexBefore = (constraint.constrainedEdges[0].Item1 - 1) % canvasData.polygon.vertices.Count,
                oneVertexAfter = (constraint.constrainedEdges[0].Item2 + 1) % canvasData.polygon.vertices.Count;

            if (oneVertexBefore > oneVertexAfter)
            {
                oneVertexBefore = (constraint.constrainedEdges[0].Item2 - 1) % canvasData.polygon.vertices.Count;
                oneVertexAfter = (constraint.constrainedEdges[0].Item1 + 1) % canvasData.polygon.vertices.Count;
            }

            List<Tuple<int, int>> edge = new List<Tuple<int, int>>();
            edge.Add(new Tuple<int, int>(oneVertexBefore, (oneVertexBefore + 1) % canvasData.polygon.vertices.Count));// FIX ME possibly have to check inverted version of edge
            Constraint forbiddenNeighbourConstraint = new Constraint(constraint.constraintMode, edge, -1);
            if (canvasData.constraints.Find(x => x.Equals(forbiddenNeighbourConstraint)) != null)
                return false;

            edge = new List<Tuple<int, int>>();
            edge.Add(new Tuple<int, int>((oneVertexBefore + 1) % canvasData.polygon.vertices.Count, oneVertexBefore));
            forbiddenNeighbourConstraint = new Constraint(constraint.constraintMode, edge, -1);
            if (canvasData.constraints.Find(x => x.Equals(forbiddenNeighbourConstraint)) != null)
                return false;

            edge = new List<Tuple<int, int>>();
            edge.Add(new Tuple<int, int>((oneVertexAfter - 1) % canvasData.polygon.vertices.Count, oneVertexAfter));
            forbiddenNeighbourConstraint = new Constraint(constraint.constraintMode, edge, -1);
            if (canvasData.constraints.Find(x => x.Equals(forbiddenNeighbourConstraint)) != null)
                return false;

            edge = new List<Tuple<int, int>>();
            edge.Add(new Tuple<int, int>(oneVertexAfter, (oneVertexAfter - 1) % canvasData.polygon.vertices.Count));
            forbiddenNeighbourConstraint = new Constraint(constraint.constraintMode, edge, -1);
            if (canvasData.constraints.Find(x => x.Equals(forbiddenNeighbourConstraint)) != null)
                return false;

            return true;

        }

        private void DeletePossibleEdgeConstraint(ConstraintMode constraintMode, Tuple<int, int> edge, double angle)
        {
            List<Tuple<int, int>> constrainedEdges = new List<Tuple<int, int>>();
            constrainedEdges.Add(edge);
            canvasData.constraints.Remove(new Constraint(constraintMode, constrainedEdges, angle));

            edge = new Tuple<int, int>(edge.Item2, edge.Item1);
            constrainedEdges = new List<Tuple<int, int>>();
            constrainedEdges.Add(edge);
            canvasData.constraints.Remove(new Constraint(constraintMode, constrainedEdges, angle));

        }


        private void fixAngleToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        private void deleteConstraintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tuple<int, int> edge = new Tuple<int, int>(-1, -1);
            if (!(edge = Algorithms.FindIfClickedNearEdge(canvasData, canvasData.clickCoordinates)).Equals(new Tuple<int, int>(-1, -1)))
            {
                DeletePossibleEdgeConstraint(ConstraintMode.VerticalEdge, edge, -1);
                DeletePossibleEdgeConstraint(ConstraintMode.HorizontalEdge, edge, -1);
                List<Tuple<int, int>> constrainedEdges = new List<Tuple<int, int>>();
            }

        }

        //private void PaintCanvas()
        //{
        //    Graphics graphics = canvas.CreateGraphics();
        //    Render.Render.PaintCanvas(graphics, canvasData.polygon.vertices);

        //    graphics.Dispose();
        //}

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            Render.Render.PaintCanvas(e.Graphics, canvasData);
        }

    }
}
