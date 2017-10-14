using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Sketchpad.IO.StatePattern
{
    class VertexMove : State
    {
        public override void Change(CanvasData parameters)
        {
            ChangeVertexPosition(parameters);
        }

        private void ChangeVertexPosition(CanvasData parameters)
        {
            parameters.polygon[parameters.draggedVertexIndex] = parameters.clickCoordinates;
            //for(int i = 0; i < polygon.Count; i++)
            //{
            //    if (polygon[i].Equals(vertice))
            //        polygon[i] = newVertice;
            //}
            //foreach(var point in polygon.Where(p => p.X == vertice.X/* && p.Y == vertice.Y */))
            //{
            //    point.X = newVertice.X;
            //    point.Y = newVertice.Y;
            //}
            //List<Point> tmp_polygon = polygon.Where(p => p.X == vertice.X/* && p.Y == vertice.Y */).ToList();

            //    tmp_polygon.ForEach(p => /*{*/ p.X = newVertice.X/*; p.Y = newVertice.Y; }*/);
        }
    }
}
