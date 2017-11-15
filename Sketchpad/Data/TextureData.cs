using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using FillCut.Utils;

namespace FillCut.Data
{
    class TextureData
    {
        public Color lightColor { get; set; }

        public Color objectColor { get; set; }
        public Bitmap texture { get; set; }
        public PolygonColorMode polygonColorMode { get; set; }

        public Tuple<double, double, double> normalVector = new Tuple<double, double, double>(0, 0, 1);
        public Bitmap normalMap { get; set; }
        public NormalVectorMode normalVectorMode { get; set; }

        public Tuple<double, double, double> lightVector = new Tuple<double, double, double>(0, 0, 1);
        public double sphereRadius { get; set; }
        public LightVectorMode lightVectorMode { get; set; }

        public Tuple<double, double, double> D = new Tuple<double, double, double>(0, 0, 0);
        public Bitmap heightMap { get; set; }
        public VectorDisorderMode vectorDisorderMode { get; set; }


        public  Color GetLightColor()
        {
            return lightColor;
        }

        public Color GetObjectColorAtGivenPoint(int x, int y)
        {
            if (polygonColorMode == PolygonColorMode.ConstantColor)
                return objectColor;

            return texture.GetPixel(x, y);
        }

        public Tuple<double, double, double> GetNormalVectorAtGivenPoint(int x, int y)
        {
            if (lightVectorMode == LightVectorMode.ConstantLightVector)
                return lightVector;

            return new Tuple<double, double, double>();
        }


    }
}
