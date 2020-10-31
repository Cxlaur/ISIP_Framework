using Emgu.CV;
using Emgu.CV.Structure;
using System;
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

            double r0 = R0 / (R0+G0+B0);
            double g0 = G0 / (R0+G0+B0);
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
    }
}