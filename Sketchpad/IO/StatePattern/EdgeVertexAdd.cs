using System;
using System.Drawing;
using System.Collections.Generic;

using Sketchpad.Data.AreaAlgorithms;

using Sketchpad.Utils;

namespace Sketchpad.Data.StatePattern
{
    class EdgeVertexAdd : IChangeCanvasData
    {
        public void Change(CanvasData canvasData)
        {
            AddVertexInTheMiddleOfEdge(canvasData);
            CanvasData.DeletePossibleEdgeConstraint(canvasData, ConstraintMode.HorizontalEdge, canvasData.clickedEdge, -1);
            CanvasData.DeletePossibleEdgeConstraint(canvasData, ConstraintMode.VerticalEdge, canvasData.clickedEdge, -1);
            CanvasData.DeletePossibleEdgeConstraint(canvasData, ConstraintMode.HorizontalEdge, new System.Tuple<int, int>(canvasData.clickedEdge.Item2, canvasData.clickedEdge.Item1), -1);
            CanvasData.DeletePossibleEdgeConstraint(canvasData, ConstraintMode.VerticalEdge, new System.Tuple<int, int>(canvasData.clickedEdge.Item2, canvasData.clickedEdge.Item1), -1);

            //DeletePossibleEdgeConstraint(canvasData, ConstraintMode.HorizontalEdge, canvasData.clickedEdge, -1);
            //DeletePossibleEdgeConstraint(canvasData, ConstraintMode.VerticalEdge, canvasData.clickedEdge, -1);
            //DeletePossibleEdgeConstraint(canvasData, ConstraintMode.HorizontalEdge, new System.Tuple<int, int>(canvasData.clickedEdge.Item2, canvasData.clickedEdge.Item1), -1);
            //DeletePossibleEdgeConstraint(canvasData, ConstraintMode.VerticalEdge, new System.Tuple<int, int>(canvasData.clickedEdge.Item2, canvasData.clickedEdge.Item1), -1);

        }

        private void DeletePossibleEdgeConstraint(CanvasData canvasData, ConstraintMode constraintMode, Tuple<int, int> edge, double angle)
        {
            List<Tuple<int, int>> constrainedEdges = new List<Tuple<int, int>>();
            constrainedEdges.Add(edge);
            constrainedEdges.Add(new Tuple<int, int>(Algorithms.mod(edge.Item1 - 1, canvasData.polygon.vertices.Count), edge.Item1));
            canvasData.constraints.Remove(new Constraint(constraintMode, constrainedEdges));

            constrainedEdges = new List<Tuple<int, int>>();
            constrainedEdges.Add(new Tuple<int, int>(Algorithms.mod(edge.Item1 - 1, canvasData.polygon.vertices.Count), edge.Item1));
            constrainedEdges.Add(edge);
            canvasData.constraints.Remove(new Constraint(constraintMode, constrainedEdges));

            edge = new Tuple<int, int>(edge.Item2, edge.Item1);
            constrainedEdges = new List<Tuple<int, int>>();
            constrainedEdges.Add(edge);
            constrainedEdges.Add(new Tuple<int, int>(Algorithms.mod(edge.Item1 - 1, canvasData.polygon.vertices.Count), edge.Item1));
            canvasData.constraints.Remove(new Constraint(constraintMode, constrainedEdges));
        }

        private void AddVertexInTheMiddleOfEdge(CanvasData canvasData)
        {
            int x = (canvasData.polygon.vertices[canvasData.clickedEdge.Item1].X + canvasData.polygon.vertices[canvasData.clickedEdge.Item2].X) / 2;
            int y = (canvasData.polygon.vertices[canvasData.clickedEdge.Item1].Y + canvasData.polygon.vertices[canvasData.clickedEdge.Item2].Y) / 2;
            canvasData.polygon.vertices.Insert(canvasData.clickedEdge.Item2, new Point(x, y));
        }
       
    }
}
