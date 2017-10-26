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

            canvasData.polygon.vertices[edge.Item2] = new Point(canvasData.polygon.vertices[edge.Item2].X, canvasData.polygon.vertices[edge.Item1].Y);

        }

        private void verticalEdgeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tuple<int, int> edge = AddSingleEdgeConstraint(canvasData, ConstraintMode.VerticalEdge);

            if (edge.Equals(new Tuple<int, int>(-1, -1)))
                return;

            canvasData.polygon.vertices[edge.Item2] = new Point(canvasData.polygon.vertices[edge.Item1].X, canvasData.polygon.vertices[edge.Item2].Y);
        }

        private Tuple<int, int> AddSingleEdgeConstraint(CanvasData canvasData, ConstraintMode constraintMode)
        {
            Tuple<int, int> edge = new Tuple<int, int>(-1, -1);
            if (!(edge = Algorithms.FindIfClickedNearEdge(canvasData, canvasData.clickCoordinates)).Equals(new Tuple<int, int>(-1, -1)))
            {
                CanvasData.DeletePossibleEdgeConstraint(canvasData, constraintMode, edge, -1);
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


            ////REFACTOR
            //List<Tuple<int, int>> edge = new List<Tuple<int, int>>();
            //edge.Add(new Tuple<int, int>(oneVertexBefore, (oneVertexBefore + 1) % canvasData.polygon.vertices.Count));// FIX ME possibly have to check inverted version of edge
            //Constraint forbiddenNeighbourConstraint = new Constraint(constraint.constraintMode, edge, -1);
            //if (canvasData.constraints.Find(x => x.Equals(forbiddenNeighbourConstraint)) != null)
            //    return false;

            if (IfConstraintExistsInNeighbour(canvasData, oneVertexBefore, (oneVertexBefore + 1) % canvasData.polygon.vertices.Count, constraint.constraintMode, -1)
                || IfConstraintExistsInNeighbour(canvasData, (oneVertexBefore + 1) % canvasData.polygon.vertices.Count, oneVertexBefore, constraint.constraintMode, -1)
                || IfConstraintExistsInNeighbour(canvasData, (oneVertexAfter - 1) % canvasData.polygon.vertices.Count, oneVertexAfter, constraint.constraintMode, -1)
                || IfConstraintExistsInNeighbour(canvasData, oneVertexAfter, (oneVertexAfter - 1) % canvasData.polygon.vertices.Count, constraint.constraintMode, -1))
                return false;

            return true;

        }

        private bool IfConstraintExistsInNeighbour(CanvasData canvasData, int firstVertexOfEdge, int secondVertexOfEdge, ConstraintMode constraintMode, double angle)
        {
            List<Tuple<int, int>> edge = new List<Tuple<int, int>>();
            edge.Add(new Tuple<int, int>(firstVertexOfEdge, secondVertexOfEdge));
            Constraint forbiddenNeighbourConstraint = new Constraint(constraintMode, edge, -1);
            if (canvasData.constraints.Find(x => x.Equals(forbiddenNeighbourConstraint)) != null)
                return true;

            return false;
        }



        private void fixAngleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int clickedVertexIndex = -1;
            if ((clickedVertexIndex = Algorithms.FindIfClickedNearVertex(canvasData, canvasData.clickCoordinates)) == -1)
                return;

            int firstNeighbourOfClickedVertexIndex = Algorithms.mod((clickedVertexIndex - 1), canvasData.polygon.vertices.Count), 
                secondNeighbourOfClickedVertexIndex = (clickedVertexIndex + 1) % canvasData.polygon.vertices.Count;

            Point clickedVertex = canvasData.polygon.vertices[clickedVertexIndex], 
                secondNeighbourOfClickedVertex = canvasData.polygon.vertices[firstNeighbourOfClickedVertexIndex],
                firstNeighbourOfClickedVertex = canvasData.polygon.vertices[secondNeighbourOfClickedVertexIndex];

            System.Windows.Vector firstVector = new System.Windows.Vector(clickedVertex.X - secondNeighbourOfClickedVertex.X,
                clickedVertex.Y - secondNeighbourOfClickedVertex.Y), secondVector = new System.Windows.Vector(firstNeighbourOfClickedVertex.X - clickedVertex.X,
                firstNeighbourOfClickedVertex.Y - clickedVertex.Y);

            //Tuple<int, int> edge = new Tuple<int, int>(-1, -1);
            //if (!(edge = Algorithms.FindIfClickedNearEdge(canvasData, canvasData.clickCoordinates)).Equals(new Tuple<int, int>(-1, -1)))
            //{
            //    CanvasData.DeletePossibleEdgeConstraint(canvasData, constraintMode, edge, -1);
            //    List<Tuple<int, int>> constrainedEdges = new List<Tuple<int, int>>();
            //    constrainedEdges.Add(edge);
            //    Constraint constraint = new Constraint(constraintMode, constrainedEdges);
            //    if (IfConstraintCanBeAdded(canvasData, constraint))
            //        canvasData.constraints.Add(constraint);
            //    else
            //        return new Tuple<int, int>(-1, -1);
            //}

            //return edge;

            //CanvasData.DeletePossibleEdgeConstraint(canvasData, constraintMode, edge, -1);
            List<Tuple<int, int>> constrainedEdges = new List<Tuple<int, int>>();
            constrainedEdges.Add(new Tuple<int, int>(clickedVertexIndex, firstNeighbourOfClickedVertexIndex));
            constrainedEdges.Add(new Tuple<int, int>(clickedVertexIndex, secondNeighbourOfClickedVertexIndex));
            double angle = System.Windows.Vector.AngleBetween(firstVector, secondVector);
            Constraint constraint = new Constraint(ConstraintMode.FixedAngle, constrainedEdges, 180 - Math.Abs(angle));
            if (IfConstraintCanBeAddedFixed(canvasData, constraint))
                canvasData.constraints.Add(constraint);
            else
                return;

        }



        private bool IfConstraintCanBeAddedFixed(CanvasData canvasData, Constraint constraint)
        {
            int oneVertexBefore = (constraint.constrainedEdges[0].Item1 - 1) % canvasData.polygon.vertices.Count,
                oneVertexAfter = (constraint.constrainedEdges[0].Item2 + 1) % canvasData.polygon.vertices.Count;

            if (oneVertexBefore > oneVertexAfter)
            {
                oneVertexBefore = (constraint.constrainedEdges[0].Item2 - 1) % canvasData.polygon.vertices.Count;
                oneVertexAfter = (constraint.constrainedEdges[0].Item1 + 1) % canvasData.polygon.vertices.Count;
            }

            int oneVertexBefore2 = (constraint.constrainedEdges[1].Item1 - 1) % canvasData.polygon.vertices.Count,
            oneVertexAfter2 = (constraint.constrainedEdges[1].Item2 + 1) % canvasData.polygon.vertices.Count;

            if (oneVertexBefore > oneVertexAfter)
            {
                oneVertexBefore = (constraint.constrainedEdges[1].Item2 - 1) % canvasData.polygon.vertices.Count;
                oneVertexAfter = (constraint.constrainedEdges[1].Item1 + 1) % canvasData.polygon.vertices.Count;
            }

            ////REFACTOR
            //List<Tuple<int, int>> edge = new List<Tuple<int, int>>();
            //edge.Add(new Tuple<int, int>(oneVertexBefore, (oneVertexBefore + 1) % canvasData.polygon.vertices.Count));// FIX ME possibly have to check inverted version of edge
            //Constraint forbiddenNeighbourConstraint = new Constraint(constraint.constraintMode, edge, -1);
            //if (canvasData.constraints.Find(x => x.Equals(forbiddenNeighbourConstraint)) != null)
            //    return false;

            if (IfConstraintExistsInNeighbour(canvasData, oneVertexBefore, (oneVertexBefore + 1) % canvasData.polygon.vertices.Count, constraint.constraintMode, -1)
                || IfConstraintExistsInNeighbour(canvasData, (oneVertexBefore + 1) % canvasData.polygon.vertices.Count, oneVertexBefore, constraint.constraintMode, -1)
                || IfConstraintExistsInNeighbour(canvasData, Algorithms.mod(oneVertexAfter - 1, canvasData.polygon.vertices.Count), oneVertexAfter, constraint.constraintMode, -1)
                || IfConstraintExistsInNeighbour(canvasData, oneVertexAfter, Algorithms.mod(oneVertexAfter - 1, canvasData.polygon.vertices.Count), constraint.constraintMode, -1))
                return false;

            if (IfConstraintExistsInNeighbour(canvasData, oneVertexBefore2, (oneVertexBefore2 + 1) % canvasData.polygon.vertices.Count, constraint.constraintMode, -1)
                || IfConstraintExistsInNeighbour(canvasData, (oneVertexBefore2 + 1) % canvasData.polygon.vertices.Count, oneVertexBefore2, constraint.constraintMode, -1)
                || IfConstraintExistsInNeighbour(canvasData, Algorithms.mod(oneVertexAfter2 - 1, canvasData.polygon.vertices.Count), oneVertexAfter2, constraint.constraintMode, -1)
                || IfConstraintExistsInNeighbour(canvasData, oneVertexAfter2, Algorithms.mod(oneVertexAfter2 - 1, canvasData.polygon.vertices.Count), constraint.constraintMode, -1))
                return false;

            if (IfConstraintExistsInNeighbour(canvasData, oneVertexBefore, (oneVertexBefore + 1) % canvasData.polygon.vertices.Count, ConstraintMode.HorizontalEdge, -1)
                || IfConstraintExistsInNeighbour(canvasData, (oneVertexBefore + 1) % canvasData.polygon.vertices.Count, oneVertexBefore, ConstraintMode.HorizontalEdge, -1)
                || IfConstraintExistsInNeighbour(canvasData, Algorithms.mod(oneVertexAfter - 1, canvasData.polygon.vertices.Count), oneVertexAfter, ConstraintMode.HorizontalEdge, -1)
                || IfConstraintExistsInNeighbour(canvasData, oneVertexAfter, Algorithms.mod(oneVertexAfter - 1, canvasData.polygon.vertices.Count), ConstraintMode.HorizontalEdge, -1))
                return false;

            if (IfConstraintExistsInNeighbour(canvasData, oneVertexBefore2, (oneVertexBefore2 + 1) % canvasData.polygon.vertices.Count, ConstraintMode.HorizontalEdge, -1)
                || IfConstraintExistsInNeighbour(canvasData, (oneVertexBefore2 + 1) % canvasData.polygon.vertices.Count, oneVertexBefore2, ConstraintMode.HorizontalEdge, -1)
                || IfConstraintExistsInNeighbour(canvasData, Algorithms.mod(oneVertexAfter2 - 1, canvasData.polygon.vertices.Count), oneVertexAfter2, ConstraintMode.HorizontalEdge, -1)
                || IfConstraintExistsInNeighbour(canvasData, oneVertexAfter2, Algorithms.mod(oneVertexAfter2 - 1, canvasData.polygon.vertices.Count), ConstraintMode.HorizontalEdge, -1))
                return false;

            if (IfConstraintExistsInNeighbour(canvasData, oneVertexBefore, (oneVertexBefore + 1) % canvasData.polygon.vertices.Count, ConstraintMode.VerticalEdge, -1)
                || IfConstraintExistsInNeighbour(canvasData, (oneVertexBefore + 1) % canvasData.polygon.vertices.Count, oneVertexBefore, ConstraintMode.VerticalEdge, -1)
                || IfConstraintExistsInNeighbour(canvasData, Algorithms.mod(oneVertexAfter - 1, canvasData.polygon.vertices.Count), oneVertexAfter, ConstraintMode.VerticalEdge, -1)
                || IfConstraintExistsInNeighbour(canvasData, oneVertexAfter, Algorithms.mod(oneVertexAfter - 1, canvasData.polygon.vertices.Count), ConstraintMode.VerticalEdge, -1))
                return false;

            if (IfConstraintExistsInNeighbour(canvasData, oneVertexBefore2, (oneVertexBefore2 + 1) % canvasData.polygon.vertices.Count, ConstraintMode.VerticalEdge, -1)
                || IfConstraintExistsInNeighbour(canvasData, (oneVertexBefore2 + 1) % canvasData.polygon.vertices.Count, oneVertexBefore2, ConstraintMode.VerticalEdge, -1)
                || IfConstraintExistsInNeighbour(canvasData, Algorithms.mod(oneVertexAfter2 - 1, canvasData.polygon.vertices.Count), oneVertexAfter2, ConstraintMode.VerticalEdge, -1)
                || IfConstraintExistsInNeighbour(canvasData, oneVertexAfter2, Algorithms.mod(oneVertexAfter2 - 1, canvasData.polygon.vertices.Count), ConstraintMode.VerticalEdge, -1))
                return false;

            return true;
        }

        private void deleteConstraintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tuple<int, int> edge = new Tuple<int, int>(-1, -1);
            if (!(edge = Algorithms.FindIfClickedNearEdge(canvasData, canvasData.clickCoordinates)).Equals(new Tuple<int, int>(-1, -1)))
            {
                CanvasData.DeletePossibleEdgeConstraint(canvasData, ConstraintMode.VerticalEdge, edge, -1);
                CanvasData.DeletePossibleEdgeConstraint(canvasData, ConstraintMode.HorizontalEdge, edge, -1);
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
            
            Render.Render.PaintCanvas(e, canvasData);
        }

    }
}
