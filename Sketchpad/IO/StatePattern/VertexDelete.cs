using FillCut.Data.AreaAlgorithms;

namespace FillCut.Data.StatePattern
{
    class VertexDelete: IChangeCanvasData
    {
        public void Change(CanvasData canvasData)
        {
            DeleteVertex(canvasData);
        }

        private void DeleteVertex(CanvasData canvasData)
        {
            canvasData.polygons[canvasData.activePolygon].vertices.RemoveAt(canvasData.clickedVertexIndex);

        }
    }
}
