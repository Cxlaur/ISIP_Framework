using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Windows;
namespace ISIP_Algorithms.Tools
{
    public class Tools
    {
        public static Image<Gray, byte> Invert(Image<Gray, byte> InputImage)
        {
            Image<Gray, byte> Result = new Image<Gray, byte>(InputImage.Size);
            for (int y = 0; y < InputImage.Height; y++)
            {
                for (int x = 0; x < InputImage.Width; x++)
                {
                    Result.Data[y, x, 0] = (byte)(255 - InputImage.Data[y, x, 0]);
                }
            }
            return Result;
        }
        public static Image<Gray, byte> Mirror(Image<Gray, byte> InputImage)
        {
            Image<Gray, byte> Result = new Image<Gray, byte>(InputImage.Size);
            for (int y = 0; y < InputImage.Height; y++)
            {
                for (int x = 0; x < InputImage.Width; x++)
                {
                    Result.Data[y, InputImage.Width - x - 1, 0] = (byte)(InputImage.Data[y, x, 0]);
                }
            }
            return Result;
        }
        public static Image<Gray, byte> Contrast(Image<Gray, byte> InputImage, double m, float E)
        {
            double[] LUT = new double[256];
            double c = Math.Pow(m, E) / (255 * (Math.Pow(255, E) + Math.Pow(m, E)));

            for (int index = 0; index < 256; index++)
            {
                LUT[index] = 255 * ((Math.Pow(index, E) / (Math.Pow(index, E) + Math.Pow(m, E))) + (c * index));
            }

            Image<Gray, byte> Result = new Image<Gray, byte>(InputImage.Size);
            for (int y = 0; y < InputImage.Height; y++)
            {
                for (int x = 0; x < InputImage.Width; x++)
                {
                    Result.Data[y, x, 0] = (byte)LUT[(InputImage.Data[y, x, 0])];
                }
            }
            return Result;
        }

        public static Image<Bgr, byte> Bin3D(Image<Bgr, byte> InputImage, int T, Point InitColor)
        {
            double R, G, B, d;

            int initX = (int)InitColor.X;
            int initY = (int)InitColor.Y;

            double R0 = InputImage.Data[initY, initX, 0];
            double G0 = InputImage.Data[initY, initX, 1];
            double B0 = InputImage.Data[initY, initX, 2];

            Image<Bgr, byte> Result = new Image<Bgr, byte>(InputImage.Size);
            for (int y = 0; y < InputImage.Height; y++)
            {
                for (int x = 0; x < InputImage.Width; x++)
                {
                    R = Math.Pow(InputImage.Data[y, x, 0] - R0, 2);
                    G = Math.Pow(InputImage.Data[y, x, 1] - G0, 2);
                    B = Math.Pow(InputImage.Data[y, x, 2] - B0, 2);
                    d = Math.Sqrt(R + G + B);
                    if (d > T)
                    {
                        Result.Data[y, x, 0] = 0;
                        Result.Data[y, x, 1] = 0;
                        Result.Data[y, x, 2] = 0;
                    }
                    else
                    {
                        Result.Data[y, x, 0] = 255;
                        Result.Data[y, x, 1] = 255;
                        Result.Data[y, x, 2] = 255;
                    }
                }
            }
            return Result;
        }

        public static Image<Bgr, byte> Bin2D(Image<Bgr, byte> InputImage, float t, Point InitColor)
        {
            double R, G, d;

            int initX = (int)InitColor.X;
            int initY = (int)InitColor.Y;


            double R0 = InputImage.Data[initY, initX, 0];
            double G0 = InputImage.Data[initY, initX, 1];
            double B0 = InputImage.Data[initY, initX, 2];

            double r0 = R0 / (R0 + G0 + B0);
            double g0 = G0 / (R0 + G0 + B0);
            //double b0 = InputImage.Data[initY, initX, 2];

            Image<Bgr, byte> Result = new Image<Bgr, byte>(InputImage.Size);
            for (int y = 0; y < InputImage.Height; y++)
            {
                for (int x = 0; x < InputImage.Width; x++)
                {
                    R0 = InputImage.Data[y, x, 0];
                    G0 = InputImage.Data[y, x, 1];
                    B0 = InputImage.Data[y, x, 2];

                    double r = R0 / (R0 + G0 + B0);
                    double g = G0 / (R0 + G0 + B0);

                    R = Math.Pow((r - r0), 2);
                    G = Math.Pow((g - g0), 2);
                    d = Math.Sqrt(R + G);
                    if (d > t)
                    {
                        Result.Data[y, x, 0] = 0;
                        Result.Data[y, x, 1] = 0;
                        Result.Data[y, x, 2] = 0;
                    }
                    else
                    {
                        Result.Data[y, x, 0] = 255;
                        Result.Data[y, x, 1] = 255;
                        Result.Data[y, x, 2] = 255;
                    }
                }
            }
            return Result;
        }

        private static int Suma(Image<Gray, byte> InputImage, int y, int x)
        {
            int sum = InputImage.Data[y, x, 0] + InputImage.Data[y - 1, x - 1, 0] + InputImage.Data[y, x - 1, 0] + InputImage.Data[y + 1, x - 1, 0]
                    + InputImage.Data[y - 1, x, 0] + InputImage.Data[y + 1, x, 0] + InputImage.Data[y - 1, x + 1, 0] + InputImage.Data[y, x + 1, 0]
                    + InputImage.Data[y + 1, x + 1, 0];

            return sum;
        }
        private static double SumaVar(Image<Gray, byte> InputImage, int y, int x, float media)
        {
            double sum = Math.Pow(InputImage.Data[y, x, 0] - media, 2) + Math.Pow(InputImage.Data[y - 1, x - 1, 0] - media, 2) + Math.Pow(InputImage.Data[y, x - 1, 0] - media, 2)
                + Math.Pow(InputImage.Data[y + 1, x - 1, 0] - media, 2) + Math.Pow(InputImage.Data[y - 1, x, 0] - media, 2) + Math.Pow(InputImage.Data[y + 1, x, 0] - media, 2)
                + Math.Pow(InputImage.Data[y - 1, x + 1, 0] - media, 2) + Math.Pow(InputImage.Data[y, x + 1, 0] - media, 2) + Math.Pow(InputImage.Data[y + 1, x + 1, 0] - media, 2);

            return sum;
        }
        private static double MediaVariatieMinima(Image<Gray, byte> InputImage, int y, int x)
        {
            double m;
            float[] medi = new float[5];
            double[] variatie = new double[5];

            medi[0] = Suma(InputImage, y - 1, x - 1) / 9;
            medi[1] = Suma(InputImage, y + 1, x - 1) / 9;
            medi[2] = Suma(InputImage, y - 1, x + 1) / 9;
            medi[3] = Suma(InputImage, y + 1, x + 1) / 9;
            medi[4] = Suma(InputImage, y, x) / 9;

            variatie[0] = SumaVar(InputImage, y - 1, x - 1, medi[0]) / 9;
            variatie[1] = SumaVar(InputImage, y + 1, x - 1, medi[1]) / 9;
            variatie[2] = SumaVar(InputImage, y - 1, x + 1, medi[2]) / 9;
            variatie[3] = SumaVar(InputImage, y + 1, x + 1, medi[3]) / 9;
            variatie[4] = SumaVar(InputImage, y, x, medi[4]) / 9;

            double v = variatie[0];
            m = medi[0];
            for (int i = 1; i < 5; i++)
            {
                if (v > variatie[i])
                {
                    v = variatie[i];
                    m = medi[i];
                }
            }
            return m;
        }

        public static Image<Gray, byte> Kuwahara(Image<Gray, byte> InputImage)
        {

            Image<Gray, byte> Result = new Image<Gray, byte>(InputImage.Size);
            for (int y = 2; y < InputImage.Height - 3; y++)
            {
                for (int x = 2; x < InputImage.Width - 3; x++)
                {
                    Result.Data[y, x, 0] = (byte)MediaVariatieMinima(InputImage,y,x);
                }
            }
            return Result;
        }
    }
}