using System;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Media.Imaging;
using System.IO;

using FillCut.Data;

namespace FillCut.Render.Algorithms
{
    class LambertReflectance
    {
        static int[] pixels;
        static int width, height, stride;


        private static void ProcessUsingLockbitsAndUnsafeAndParallel(Bitmap processedBitmap, TextureData textureData)
        {
            unsafe
            {
                BitmapData bitmapData = processedBitmap.LockBits(new Rectangle(0, 0, processedBitmap.Width, processedBitmap.Height), ImageLockMode.ReadWrite, processedBitmap.PixelFormat);

                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(processedBitmap.PixelFormat) / 8;
                int heightInPixels = bitmapData.Height;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* PtrFirstPixel = (byte*)bitmapData.Scan0;

                Color lightColor = textureData.GetLightColor();
                Tuple<double, double, double> L = normalizeVector(textureData.GetLightVector(textureData.stopWatch.Elapsed));
                Parallel.For(0, heightInPixels, y =>
                {
                    byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        Color objectColorAtGivenPoint = textureData.GetObjectColorAtGivenPoint(x, y);
                        Tuple<double, double, double> N_prim = CalculateNormalVectorWithDisorder(textureData, x, y);


                        Color color = Color.FromArgb(LambertColor(lightColor.R, objectColorAtGivenPoint.R, N_prim, L),
                        LambertColor(lightColor.G, objectColorAtGivenPoint.G, N_prim, L), LambertColor(lightColor.B, objectColorAtGivenPoint.B, N_prim, L));

                        //int oldBlue = currentLine[x];
                        //int oldGreen = currentLine[x + 1];
                        //int oldRed = currentLine[x + 2];

                        currentLine[x] = (byte)color.B;
                        currentLine[x + 1] = (byte)color.G;
                        currentLine[x + 2] = (byte)color.R;
                    }
                });
                processedBitmap.UnlockBits(bitmapData);
            }
        }


        private static void ProcessUsingLockbitsAndUnsafe(Bitmap processedBitmap, TextureData textureData)
        {
            unsafe
            {

                BitmapData bitmapData = processedBitmap.LockBits(new Rectangle(0, 0, processedBitmap.Width, processedBitmap.Height), ImageLockMode.ReadWrite, processedBitmap.PixelFormat);
                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(processedBitmap.PixelFormat) / 8;
                int heightInPixels = bitmapData.Height;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* ptrFirstPixel = (byte*)bitmapData.Scan0;

                Color lightColor = textureData.GetLightColor();
                Tuple<double, double, double> L = normalizeVector(textureData.GetLightVector(textureData.stopWatch.Elapsed));
                for (int y = 0; y < heightInPixels; y++)
                {
                    byte* currentLine = ptrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        Color objectColorAtGivenPoint = textureData.GetObjectColorAtGivenPoint(x, y);
                        Tuple<double, double, double> N_prim = CalculateNormalVectorWithDisorder(textureData, x, y);


                        Color color = Color.FromArgb(LambertColor(lightColor.R, objectColorAtGivenPoint.R, N_prim, L),
                        LambertColor(lightColor.G, objectColorAtGivenPoint.G, N_prim, L), LambertColor(lightColor.B, objectColorAtGivenPoint.B, N_prim, L));

                        //int oldBlue = currentLine[x];
                        //int oldGreen = currentLine[x + 1];
                        //int oldRed = currentLine[x + 2];

                        // calculate new pixel value
                        currentLine[x] = (byte)color.B;
                        currentLine[x + 1] = (byte)color.G;
                        currentLine[x + 2] = (byte)color.R;
                        currentLine[x + 3] = (byte)color.A;
                    }
                }
               processedBitmap.UnlockBits(bitmapData);

            }

        }

        public static void LambertModel(PictureBox canvas, PaintEventArgs e, TextureData textureData)
        {
            //width = e.ClipRectangle.Right - 2 - e.ClipRectangle.Left;
            //height = e.ClipRectangle.Bottom - 2 - e.ClipRectangle.Top;
            //stride = 4 * width;
            //pixels = new int[width * height];
            //canvas.Image = new Bitmap(width, height);

            //ProcessUsingLockbitsAndUnsafe((Bitmap)canvas.Image, textureData);


            //if (canvas.Image != null) canvas.Image.Dispose();
            //canvas.Image = new Bitmap(canvas.Width, canvas.Height);
            //canvas.Image = ProcessUsingLockbits((Bitmap)canvas.Image, e, textureData);

            //WriteableBitmapApi wba = new WriteableBitmapApi(new WriteableBitmap(1, 2, 96, 96, );
            Color lightColor = textureData.GetLightColor();
            //width = e.ClipRectangle.Right - 300 - e.ClipRectangle.Left;
            //height = e.ClipRectangle.Bottom - 400 - e.ClipRectangle.Top;
            //stride = 4 * width;
            //pixels = new int[width * height];

            Tuple<double, double, double> L = normalizeVector(textureData.GetLightVector(textureData.stopWatch.Elapsed));

            for (int x = e.ClipRectangle.Left; x < e.ClipRectangle.Right; x++)
            {
                for (int y = e.ClipRectangle.Top; y < e.ClipRectangle.Bottom; y++)
                {
                    Color objectColorAtGivenPoint = textureData.GetObjectColorAtGivenPoint(x, y);
                    Tuple<double, double, double> N_prim = CalculateNormalVectorWithDisorder(textureData, x, y);


                    //Color color = Color.FromArgb(LambertColor(lightColor.R, objectColorAtGivenPoint.R, N_prim, L),
                    //LambertColor(lightColor.G, objectColorAtGivenPoint.G, N_prim, L), LambertColor(lightColor.B, objectColorAtGivenPoint.B, N_prim, L));
                    //SetPixel(x, y, color);

                    Brush brush = new SolidBrush(Color.FromArgb(LambertColor(lightColor.R, objectColorAtGivenPoint.R, N_prim, L),
                    LambertColor(lightColor.G, objectColorAtGivenPoint.G, N_prim, L), LambertColor(lightColor.B, objectColorAtGivenPoint.B, N_prim, L)));

                    SetPixel(e, brush, x, y);
                }
            }
            //unsafe
            //{
            //    fixed (int* intPtr = &pixels[0])
            //    {
            //        canvas.Image = new Bitmap(width, height, stride, PixelFormat.Format32bppRgb, new IntPtr(intPtr));
            //    }
            //}


        }

        public static void SetPixel(int x, int y, Color color)
        {
            if (x < 0 || y < 0 || x >= width || y >= height)
                return;
            byte alpha = 255;
            int colorValue = ((alpha << 24) + (color.R << 16) + (color.G << 8) + color.B);
            pixels[y * (int)width + x] = colorValue;
        }

        private static Bitmap BitmapFromWriteableBitmap(WriteableBitmap writeBmp)
        {
            System.Drawing.Bitmap bmp;
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create((BitmapSource)writeBmp));
                enc.Save(outStream);
                bmp = new System.Drawing.Bitmap(outStream);
            }
            return bmp;
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


        private static int LambertColor(int lightColorComponent, int objectColorComponent, Tuple<double, double, double> normalVector, Tuple<double, double, double> lightUnitVector)
        {
            double light = ConvertColorsFromIntToDouble(lightColorComponent);
            double obj = ConvertColorsFromIntToDouble(objectColorComponent);
            double cos = Cos(normalVector, lightUnitVector);
            int result = ConvertColorsFromDoubleToInt(light * obj * cos);
            return result % 255;
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

        private static void SetPixel(PaintEventArgs e, Brush brush, int x, int y)
        {
            e.Graphics.FillRectangle(brush, x, y, 1, 1);
        }



    }
}
