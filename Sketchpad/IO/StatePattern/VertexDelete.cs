
namespace Sketchpad.IO.StatePattern
{
    class VertexDelete: IChangeCanvasData
    {
        public void Change(CanvasData canvasData)
        {
            DeleteVertex(canvasData);
        }

        private void DeleteVertex(CanvasData canvasData)
        {
            canvasData.polygon.vertices.RemoveAt(canvasData.clickedVertexIndex);

        }
    }
}
