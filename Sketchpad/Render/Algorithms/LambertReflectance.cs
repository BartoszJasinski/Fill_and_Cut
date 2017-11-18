using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

using FillCut.Data;

namespace FillCut.Render.Algorithms
{
    class LambertReflectance
    {

        public static void LambertModel(PictureBox canvas, PaintEventArgs e, TextureData textureData)
        {
            //if (canvas.Image != null) canvas.Image.Dispose();
            //canvas.Image = new Bitmap(canvas.Width, canvas.Height);
            //canvas.Image = ProcessUsingLockbits((Bitmap)canvas.Image, e, textureData);



            Color lightColor = textureData.GetLightColor();


            for (int x = e.ClipRectangle.Left; x < e.ClipRectangle.Right - 300; x++)
                for (int y = e.ClipRectangle.Top; y < e.ClipRectangle.Bottom - 400; y++)
                {
                    Color objectColorAtGivenPoint = textureData.GetObjectColorAtGivenPoint(x, y);
                    Tuple<double, double, double> N_prim = CalculateNormalVectorWithDisorder(textureData, x, y);
                    Tuple<double, double, double> L = normalizeVector(textureData.GetLightVector(textureData.stopWatch.Elapsed));


                    Brush brush = new SolidBrush(Color.FromArgb(LambertColor(lightColor.R, objectColorAtGivenPoint.R, N_prim, L),
                        LambertColor(lightColor.G, objectColorAtGivenPoint.G, N_prim, L), LambertColor(lightColor.B, objectColorAtGivenPoint.B, N_prim, L)));
                    SetPixel(e, brush, new Point(x, y));
                }
        }


        private static Image ProcessUsingLockbits(Bitmap processedBitmap, PaintEventArgs e, TextureData textureData)
        {
            BitmapData bitmapData = processedBitmap.LockBits(new Rectangle(0, 0, processedBitmap.Width, processedBitmap.Height), ImageLockMode.ReadWrite, processedBitmap.PixelFormat);

            int bytesPerPixel = Bitmap.GetPixelFormatSize(processedBitmap.PixelFormat) / 8;
            int byteCount = bitmapData.Stride * processedBitmap.Height;
            byte[] pixels = new byte[byteCount];
            IntPtr ptrFirstPixel = bitmapData.Scan0;
            Marshal.Copy(ptrFirstPixel, pixels, 0, pixels.Length);
            int heightInPixels = bitmapData.Height;
            int widthInBytes = bitmapData.Width * bytesPerPixel;

            Color lightColor = textureData.GetLightColor();
            for (int y = 0, j = 0; y < heightInPixels; y++, j++)
            {
                int currentLine = y * bitmapData.Stride;
                for (int x = 0, i = 0; x < widthInBytes; x = x + bytesPerPixel, i++)
                {

                    Color objectColorAtGivenPoint = textureData.GetObjectColorAtGivenPoint(i, j);
                    Tuple<double, double, double> N_prim = CalculateNormalVectorWithDisorder(textureData, i, j);
                    Tuple<double, double, double> L = normalizeVector(textureData.GetLightVector(textureData.stopWatch.Elapsed));

                    Color c = Color.FromArgb(LambertColor(lightColor.R, objectColorAtGivenPoint.R, N_prim, L),
                                           LambertColor(lightColor.G, objectColorAtGivenPoint.G, N_prim, L), LambertColor(lightColor.B, objectColorAtGivenPoint.B, N_prim, L));


                    //Color objectColorAtGivenPoint = textureData.GetObjectColorAtGivenPoint(x, y);
                    //Tuple<double, double, double> N_prim = CalculateNormalVectorWithDisorder(textureData, x, y);
                    //Tuple<double, double, double> L = normalizeVector(textureData.GetLightVector(textureData.stopWatch.Elapsed));


                    ////Brush brush = new SolidBrush(Color.FromArgb(LambertColor(lightColor.R, objectColorAtGivenPoint.R, N_prim, L),
                    ////LambertColor(lightColor.G, objectColorAtGivenPoint.G, N_prim, L), LambertColor(lightColor.B, objectColorAtGivenPoint.B, N_prim, L)));

                    //Color c = Color.FromArgb(LambertColor(lightColor.R, objectColorAtGivenPoint.R, N_prim, L),
                    //                       LambertColor(lightColor.G, objectColorAtGivenPoint.G, N_prim, L), LambertColor(lightColor.B, objectColorAtGivenPoint.B, N_prim, L));




                    int oldBlue = pixels[currentLine + x];
                    int oldGreen = pixels[currentLine + x + 1];
                    int oldRed = pixels[currentLine + x + 2];

                    // calculate new pixel value
                    pixels[currentLine + x] = (byte)c.B;
                    pixels[currentLine + x + 1] = (byte)c.G;
                    pixels[currentLine + x + 2] = (byte)c.R;
                }
            }

            // copy modified bytes back
            Marshal.Copy(pixels, 0, ptrFirstPixel, pixels.Length);
            processedBitmap.UnlockBits(bitmapData);

            return processedBitmap as Image;
        }

        //private void ProcessUsingLockbitsAndUnsafeAndParallel(Bitmap processedBitmap)
        //{
        //    unsafe
        //    {
        //        BitmapData bitmapData = processedBitmap.LockBits(new Rectangle(0, 0, processedBitmap.Width, processedBitmap.Height), ImageLockMode.ReadWrite, processedBitmap.PixelFormat);

        //        int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(processedBitmap.PixelFormat) / 8;
        //        int heightInPixels = bitmapData.Height;
        //        int widthInBytes = bitmapData.Width * bytesPerPixel;
        //        byte* PtrFirstPixel = (byte*)bitmapData.Scan0;

        //        Parallel.For(0, heightInPixels, y =>
        //        {
        //            byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
        //            for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
        //            {
        //                int oldBlue = currentLine[x];
        //                int oldGreen = currentLine[x + 1];
        //                int oldRed = currentLine[x + 2];

        //                currentLine[x] = (byte)oldBlue;
        //                currentLine[x + 1] = (byte)oldGreen;
        //                currentLine[x + 2] = (byte)oldRed;
        //            }
        //        });
        //        processedBitmap.UnlockBits(bitmapData);
        //    }
        //}

        private static int LambertColor(int lightColorComponent, int objectColorComponent, Tuple<double, double, double> normalVector, Tuple<double, double, double> lightUnitVector)
        {
            double light = ConvertColorsFromIntToDouble(lightColorComponent);
            double obj = ConvertColorsFromIntToDouble(objectColorComponent);
            double cos = Cos(normalVector, lightUnitVector);
            int text = ConvertColorsFromDoubleToInt(light * obj * cos);

            return text;
        }

        private static Tuple<double, double, double> CalculateNormalVectorWithDisorder(TextureData textureData, int x, int y)
        {
            Color normalVectorColor = textureData.GetNormalVectorColorValueAtGivenPoint(x, y);
            Tuple<double, double, double> normalVector = new Tuple<double, double, double>((double)(normalVectorColor.R - 127) / 128, (double)(normalVectorColor.G - 127) / 128, (double)(normalVectorColor.B) / 255);
            normalVector = new Tuple<double, double, double>(normalVector.Item1 / normalVector.Item3, normalVector.Item2 / normalVector.Item3, normalVector.Item3 / normalVector.Item3);
            Tuple<double, double, double> D = CalculateDisorderVector(normalVector, textureData, x, y);

            Tuple<double, double, double> normalVectorWithDisorder = new Tuple<double, double, double>(normalVector.Item1 + D.Item1, normalVector.Item2 + D.Item2, normalVector.Item3 + D.Item3);

            return normalizeVector(normalVectorWithDisorder);

        }

        private static Tuple<double, double, double> CalculateDisorderVector(Tuple<double, double, double> normalVector, TextureData textureData, int x, int y)
        {
            double dh_x = ConvertColorsFromIntToDouble(textureData.GetHeightMapColorValueAtGivenPoint(x + 1, y).R - textureData.GetHeightMapColorValueAtGivenPoint(x, y).R);
            double dh_y = ConvertColorsFromIntToDouble(textureData.GetHeightMapColorValueAtGivenPoint(x, y + 1).R - textureData.GetHeightMapColorValueAtGivenPoint(x, y).R);
            Tuple<double, double, double> T = new Tuple<double, double, double>(1 * dh_x, 0, -(normalVector.Item1 * dh_x)), B = new Tuple<double, double, double>(0, 1 * dh_y, -(normalVector.Item2 * dh_y));

            return new Tuple<double, double, double>(T.Item1 + B.Item1, T.Item2 + B.Item2, T.Item3 + B.Item3);
        }

        private static Tuple<double, double, double> normalizeVector(Tuple<double, double, double> vector)
        {
            double vectorLength = vector.Item1 * vector.Item1 + vector.Item2 * vector.Item2 + vector.Item3 * vector.Item3;
            return new Tuple<double, double, double>(vector.Item1 / vectorLength, vector.Item2 / vectorLength, vector.Item3 / vectorLength);
        }

        //private static double ConvertColorsFromIntToDouble(int colorComponent)
        //{
        //    return (colorComponent - 127) / 127;
        //    //return ((double)(colorComponent) / 255);
        //}


        private static double ConvertColorsFromIntToDouble(int colorComponent)
        {
            //return (colorComponent - 127) / 127;
            return ((double)(colorComponent) / 255);
        }

        private static int ConvertColorsFromDoubleToInt(double colorComponent)
        {
            return (int)(colorComponent * 255);
        }


        private static double Cos(Tuple<double, double, double> N, Tuple<double, double, double> L)
        {
            return Math.Abs(N.Item1 * L.Item1 + N.Item2 + L.Item2 + N.Item3 * L.Item3);
        }

        private static void SetPixel(PaintEventArgs e, Brush brush, Point point)
        {
            e.Graphics.FillRectangle(brush, point.X, point.Y, 1, 1);
        }



    }
}
