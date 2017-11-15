using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace FillCut.Data
{
    class TextureData
    {
        public Color lightColor { get; set; }
        public Color objectColor { get; set; }
        public Bitmap colorFromTexture { get; set; }
        public Tuple<double, double, double> normalVector = new Tuple<double, double, double>(0, 0, 1);
        public Bitmap normalMap { get; set; }
        public Tuple<double, double, double> lightVector = new Tuple<double, double, double>(0, 0, 1);
        public double sphereRadius { get; set; }
        public Tuple<double, double, double> D = new Tuple<double, double, double>(0, 0, 0);
        public Bitmap heightMap { get; set; }


    }
}
