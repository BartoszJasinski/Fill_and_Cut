﻿namespace Sketchpad.IO.StatePattern
{
    class VertexMove : IChangeCanvasData
    {
        public void Change(CanvasData parameters)
        {
            ChangeVertexPosition(parameters);
        }

        private void ChangeVertexPosition(CanvasData parameters)
        {
            parameters.polygon[parameters.clickedVertexIndex] = parameters.clickCoordinates;
        }
    }
}