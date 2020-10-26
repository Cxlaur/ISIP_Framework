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
                    Result.Data[y, InputImage.Width - x-1, 0] = (byte)( InputImage.Data[y, x, 0]);
                }
            }
            return Result;
        }
        public static Image<Gray, byte> Contrast(Image<Gray, byte> InputImage,double m, float E)
        {
            double[] LUT = new double[256];
            double c= Math.Pow(m,E)/(255*(Math.Pow(255,E)+Math.Pow(m,E)));

            for (int index=0;index<256;index++)
            {
                LUT[index] =255*((Math.Pow(index,E)/(Math.Pow(index,E)+Math.Pow(m,E)))+(c*index));
            }

            Image<Gray, byte> Result = new Image<Gray, byte>(InputImage.Size);
            for (int y = 0; y < InputImage.Height; y++)
            {
                for (int x = 0; x < InputImage.Width; x++)
                {
                    Result.Data[y, x, 0] = (byte)LUT[( InputImage.Data[y, x, 0])];
                }
            }
            return Result;
        }
    }
}
