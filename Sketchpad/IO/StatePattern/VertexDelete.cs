using Sketchpad.Data.AreaAlgorithms;

namespace Sketchpad.Data.StatePattern
{
    class VertexDelete: IChangeCanvasData
    {
        public void Change(CanvasData canvasData)
        {
            DeleteVertex(canvasData);
            CanvasData.DeletePossibleEdgeConstraint(canvasData, Utils.ConstraintMode.HorizontalEdge,
                new System.Tuple<int, int>(canvasData.clickedVertexIndex, (canvasData.clickedVertexIndex + 1) % canvasData.polygon.vertices.Count), -1);
            CanvasData.DeletePossibleEdgeConstraint(canvasData, Utils.ConstraintMode.VerticalEdge, 
                new System.Tuple<int, int>(canvasData.clickedVertexIndex, (canvasData.clickedVertexIndex + 1) % canvasData.polygon.vertices.Count), -1);
            CanvasData.DeletePossibleEdgeConstraint(canvasData, Utils.ConstraintMode.HorizontalEdge,
                new System.Tuple<int, int>(canvasData.clickedVertexIndex, Algorithms.mod(canvasData.clickedVertexIndex - 1, canvasData.polygon.vertices.Count)), -1);
            CanvasData.DeletePossibleEdgeConstraint(canvasData, Utils.ConstraintMode.VerticalEdge,
                new System.Tuple<int, int>(canvasData.clickedVertexIndex, Algorithms.mod(canvasData.clickedVertexIndex - 1, canvasData.polygon.vertices.Count)), -1);
        }

        private void DeleteVertex(CanvasData canvasData)
        {
            canvasData.polygon.vertices.RemoveAt(canvasData.clickedVertexIndex);

        }
    }
}
