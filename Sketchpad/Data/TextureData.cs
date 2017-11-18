﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;

using FillCut.Utils;

namespace FillCut.Data
{
    class TextureData
    {
        public Color lightColor { get; set; }

        public Color objectColor { get; set; }
        public Bitmap texture { get; set; }
        public PolygonColorMode polygonColorMode { get; set; } // Mayby we should put watchdog here to observe changes in objectCloro or texture and change polygonColorMode appropriately. Same with other fiels

        public Color constantNormalVector = Color.FromArgb(127, 127, 255);
        public Bitmap normalMap { get; set; }
        public NormalVectorMode normalVectorMode { get; set; }

        public Tuple<double, double, double> constantLightVector = new Tuple<double, double, double>(0, 0, 1);
        public double sphereRadius { get; set; }
        public LightVectorMode lightVectorMode { get; set; }

        public Color D = Color.FromArgb(0, 0, 0);
        public Bitmap heightMap { get; set; }
        public VectorDisorderMode vectorDisorderMode { get; set; }

        public Stopwatch stopWatch = Stopwatch.StartNew();



        public Color GetLightColor()
        {
            return lightColor;
        }

        public Color GetObjectColorAtGivenPoint(int x, int y)
        {
            if (polygonColorMode == PolygonColorMode.ConstantColor)
                return objectColor;
            else
                return texture.GetPixel(x % texture.Width, y % texture.Height);
        }

        public Tuple<double, double, double> GetLightVector(TimeSpan time)
        {
            if (lightVectorMode == LightVectorMode.ConstantLightVector)
                return constantLightVector;

            double omega = 1, a = 0, b = 0;
            double xCoordinate = a + sphereRadius * Math.Cos(omega * time.TotalSeconds);
            double yCoordinate = b + sphereRadius * Math.Sin(omega * time.TotalSeconds);
            double zCoordinate = 1;

            return new Tuple<double, double, double>(xCoordinate, yCoordinate, zCoordinate);
        }

        public Color GetNormalVectorColorValueAtGivenPoint(int x, int y)
        {
            if (normalVectorMode == NormalVectorMode.ConstantNormalVector)
                return constantNormalVector;
            else
                return normalMap.GetPixel(x % normalMap.Width, y % normalMap.Height);
        }

        public Color GetHeightMapColorValueAtGivenPoint(int x, int y)
        {
            if (vectorDisorderMode == VectorDisorderMode.LackOfDisorder)
                return D;

            if (x > heightMap.Size.Width && y > heightMap.Size.Height)
                return heightMap.GetPixel(heightMap.Size.Width - 1, heightMap.Size.Height - 1); //CHECK IF normalMap.Size.Width - 1, normalMap.Size.Height - 1 or normalMap.Size.Width, normalMap.Size.Height 
            else if (x > heightMap.Size.Width)
                return heightMap.GetPixel(heightMap.Size.Width - 1, y % heightMap.Height);
            else if (y > heightMap.Size.Height)
                return heightMap.GetPixel(x % heightMap.Width, heightMap.Size.Height - 1);

            return heightMap.GetPixel(x % heightMap.Width, y % heightMap.Height);
        }


    }
}
