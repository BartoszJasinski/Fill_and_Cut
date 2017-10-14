using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchpad.Settings
{
    class ProgramSettings
    {
        public Size vertexSize { get; } = new Size(3, 3);
        public Brush vertexColor { get; } = Brushes.Red;
        public Brush lineColor { get; } = Brushes.LightGreen;
        public Brush boundingBoxColor { get; } = Brushes.LightBlue;
        public int spaceBetweenPolygonAndBoundingBox { get; } = 10;



    }
}
