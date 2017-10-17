using System.Drawing;
using System.Collections.Generic;

using Sketchpad.Data;
using Sketchpad.Utils;

namespace Sketchpad.IO.StatePattern
{
    class VertexMove : IChangeCanvasData
    {
        public void Change(CanvasData canvasData)
        {
            ChangeVertexPosition(canvasData);
        }

        private void ChangeVertexPosition(CanvasData canvasData)
        {
            List<Constraint> constraints = canvasData.constraints.FindAll(x => x.constrainedEdges.Exists(y => y.Item1 == canvasData.clickedVertexIndex || y.Item2 == canvasData.clickedVertexIndex));
            if (constraints.Count == 0)
            {
                canvasData.polygon.vertices[canvasData.clickedVertexIndex] = canvasData.clickCoordinates;
                return;
            }

            int secondVertexIndex = constraints[0].constrainedEdges[0].Item2;
            if (secondVertexIndex == canvasData.clickedVertexIndex)
                secondVertexIndex = constraints[0].constrainedEdges[0].Item1;

            if (constraints[0].constraintMode == ConstraintMode.VerticalEdge)
                ChangeSingleConstrainedEdgePosition(canvasData, secondVertexIndex, new Point(canvasData.clickCoordinates.X, canvasData.polygon.vertices[secondVertexIndex].Y));
            else if (constraints[0].constraintMode == ConstraintMode.HorizontalEdge)
                ChangeSingleConstrainedEdgePosition(canvasData, secondVertexIndex, new Point(canvasData.polygon.vertices[secondVertexIndex].Y, canvasData.clickCoordinates.Y));
            else
                ;
        }

        private void ChangeSingleConstrainedEdgePosition(CanvasData canvasData, int secondVertexIndex, Point secondVertexLocation)
        {
            canvasData.polygon.vertices[canvasData.clickedVertexIndex] = canvasData.clickCoordinates;
            canvasData.polygon.vertices[secondVertexIndex] = secondVertexLocation;
        }

    }
}
