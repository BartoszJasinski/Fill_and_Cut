using System.Drawing;

namespace Sketchpad.Settings
{
    static class ProgramSettings
    {
        public static Size vertexSize { get; } = new Size(3, 3);
        public static Brush vertexColor { get; } = Brushes.Red;
        public static Brush lineColor { get; } = Brushes.LightGreen;
        public static Brush boundingBoxColor { get; } = Brushes.LightBlue;
        public static int spaceBetweenPolygonAndBoundingBox { get; } = 10;



    }
}
