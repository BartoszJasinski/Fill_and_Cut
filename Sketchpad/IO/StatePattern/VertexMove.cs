using System.Drawing;
using System.Collections.Generic;
using System;

using Sketchpad.Data;
using Sketchpad.Utils;

namespace Sketchpad.Data.StatePattern
{
    class VertexMove: IChangeCanvasData
    {
        public void Change(CanvasData canvasData)
        {
            ChangeVertexPosition(canvasData);
        }

        private void ChangeVertexPosition(CanvasData canvasData)
        {
            List<Constraint> constraints = canvasData.constraints.FindAll(x => x.constrainedEdges.Exists(y => y.Item1 == canvasData.clickedVertexIndex
            || y.Item2 == canvasData.clickedVertexIndex));
            if (constraints.Count == 0)
            {
                canvasData.polygon.vertices[canvasData.clickedVertexIndex] = canvasData.clickCoordinates;
                return;
            }

            ApplyConstraints(canvasData, constraints);
            
        }

        private void ApplyConstraints(CanvasData canvasData, List<Constraint> constraints)
        {
            foreach (Constraint constraint in constraints)
            {
                int secondVertexIndex = constraint.constrainedEdges[0].Item2;
                if (secondVertexIndex == canvasData.clickedVertexIndex)
                    secondVertexIndex = constraint.constrainedEdges[0].Item1;

                if (constraint.constraintMode == ConstraintMode.VerticalEdge)
                    ChangeSingleConstrainedEdgePosition(canvasData, secondVertexIndex, new Point(canvasData.clickCoordinates.X, canvasData.polygon.vertices[secondVertexIndex].Y));
                else if (constraint.constraintMode == ConstraintMode.HorizontalEdge)
                    ChangeSingleConstrainedEdgePosition(canvasData, secondVertexIndex, new Point(canvasData.polygon.vertices[secondVertexIndex].X, canvasData.clickCoordinates.Y));
                else if (constraint.constraintMode == ConstraintMode.FixedAngle)
                {
                    if(canvasData.clickedVertexIndex == constraint.constrainedEdges[0].Item1)
                    {
                        int x1 = canvasData.polygon.vertices[constraint.constrainedEdges[0].Item1].X;
                        int y1 = canvasData.polygon.vertices[constraint.constrainedEdges[0].Item1].Y;
                        int x2 = canvasData.polygon.vertices[constraint.constrainedEdges[1].Item2].X;
                        int y2 = canvasData.polygon.vertices[constraint.constrainedEdges[1].Item2].Y;

                        System.Windows.Vector firstVector = new System.Windows.Vector(x1 - x2, y1 - y2),
                            secondVector = new System.Windows.Vector(x2 - canvasData.polygon.vertices[constraint.constrainedEdges[0].Item2].X,
                            y2 - canvasData.polygon.vertices[constraint.constrainedEdges[0].Item2].Y);


                        double angl = System.Windows.Vector.AngleBetween(firstVector, secondVector);
                        double alp1 = DegreeToRadian(angl), alp2 = DegreeToRadian((180 - angl - constraint.angle));
                        double u = x2 - x1, v = y2 - y1, a3 = Math.Sqrt(u * u + v * v);
                        double alp3 = Math.PI - alp1 - alp2;
                        double a2 = a3 * Math.Sin(alp2) / Math.Sin(alp3);
                        double RHS1 = x1 * u + y1 * v + a2 * a3 * Math.Cos(alp1);
                        double RHS2 = y2 * u - x2 * v + a2 * a3 * Math.Sin(alp1);
                        int x3 = (int)((1 / (a3 * a3)) * (u * RHS1 - v * RHS2));
                        int y3 = (int)((1 / (a3 * a3)) * (v * RHS1 + u * RHS2));
                        //canvasData.polygon.vertices[canvasData.clickedVertexIndex] = canvasData.clickCoordinates;

                        ChangeSingleConstrainedEdgePosition(canvasData, constraint.constrainedEdges[0].Item2, new Point(x3, y3));
                    }
                    else
                    {
                        int x1 = canvasData.polygon.vertices[constraint.constrainedEdges[0].Item2].X;
                        int y1 = canvasData.polygon.vertices[constraint.constrainedEdges[0].Item2].Y;
                        int x2 = canvasData.polygon.vertices[constraint.constrainedEdges[1].Item2].X;
                        int y2 = canvasData.polygon.vertices[constraint.constrainedEdges[1].Item2].Y;

                        double alp1 = DegreeToRadian((180 - constraint.angle) / 2), alp2 = DegreeToRadian((180 - constraint.angle) / 2);
                        double u = x2 - x1, v = y2 - y1, a3 = Math.Sqrt(u * u + v * v);
                        double alp3 = Math.PI - alp1 - alp2;
                        double a2 = a3 * Math.Sin(alp2) / Math.Sin(alp3);
                        double RHS1 = x1 * u + y1 * v + a2 * a3 * Math.Cos(alp1);
                        double RHS2 = y2 * u - x2 * v + a2 * a3 * Math.Sin(alp1);
                        int x3 = (int)((1 / (a3 * a3)) * (u * RHS1 - v * RHS2));
                        int y3 = (int)((1 / (a3 * a3)) * (v * RHS1 + u * RHS2));
                        ChangeSingleConstrainedEdgePosition(canvasData, constraint.constrainedEdges[0].Item1, new Point(x3, y3));
                        //canvasData.polygon.vertices[canvasData.clickedVertexIndex] = canvasData.clickCoordinates;

                    }

                }
            }
        }


        private double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        private void ChangeSingleConstrainedEdgePosition(CanvasData canvasData, int secondVertexIndex, Point secondVertexLocation)
        {
            canvasData.polygon.vertices[canvasData.clickedVertexIndex] = canvasData.clickCoordinates;
            canvasData.polygon.vertices[secondVertexIndex] = secondVertexLocation;
        }

        private void ChangeFixedConstrainedEdgesPosition(CanvasData canvasData, int secondVertexIndex, Point secondVertexLocation)
        {
            canvasData.polygon.vertices[secondVertexIndex] = secondVertexLocation;
        }

    }
}
