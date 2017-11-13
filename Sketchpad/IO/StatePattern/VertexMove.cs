using System.Drawing;
using System.Collections.Generic;
using System;

using FillCut.Data;
using FillCut.Utils;

namespace FillCut.Data.StatePattern
{
    class VertexMove: IChangeCanvasData
    {
        public void Change(CanvasData canvasData)
        {
            ChangeVertexPosition(canvasData);
        }

        private void ChangeVertexPosition(CanvasData canvasData)
        {
            canvasData.polygons[canvasData.activePolygon].vertices[canvasData.clickedVertexIndex] = canvasData.clickCoordinates;


        }


    }
}
