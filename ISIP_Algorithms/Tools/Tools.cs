using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections;
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

        public static Image<Bgr, byte> Bin3D(Image<Bgr, byte> InputImage, double T, Point InitColor)
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

            Image<Gray, byte> Result = InputImage.Clone();
            for (int y = 2; y < InputImage.Height - 3; y++)
            {
                for (int x = 2; x < InputImage.Width - 3; x++)
                {
                    Result.Data[y, x, 0] = (byte)MediaVariatieMinima(InputImage, y, x);
                }
            }
            return Result;
        }

        private static float Sy(Image<Gray, byte> InputImage, int y, int x)
        {
            float Result = InputImage.Data[y + 1, x - 1, 0] - InputImage.Data[y - 1, x - 1, 0] +
                2 * InputImage.Data[y + 1, x, 0] - 2 * InputImage.Data[y - 1, x, 0] +
                InputImage.Data[y + 1, x + 1, 0] - InputImage.Data[y - 1, x + 1, 0];

            return Result;
        }

        private static float Sx(Image<Gray, byte> InputImage, int y, int x)
        {
            float Result = InputImage.Data[y - 1, x + 1, 0] - InputImage.Data[y - 1, x - 1, 0] +
                2 * InputImage.Data[y, x + 1, 0] - 2 * InputImage.Data[y, x - 1, 0] +
                InputImage.Data[y + 1, x + 1, 0] - InputImage.Data[y + 1, x - 1, 0];

            return Result;
        }


        public static Image<Gray, byte> Sobel(Image<Gray, byte> InputImage, double T)
        {

            Image<Gray, byte> Result = new Image<Gray, byte>(InputImage.Size);
            for (int y = 2; y < InputImage.Height - 3; y++)
            {
                for (int x = 2; x < InputImage.Width - 3; x++)
                {
                    double Fx = Sx(InputImage, y, x);
                    double Fy = Sy(InputImage, y, x);
                    double gradient = Math.Sqrt(Math.Pow(Fx, 2) + Math.Pow(Fy, 2));

                    if (gradient < T)
                        Result.Data[y, x, 0] = (byte)0;
                    else
                    {
                        double theta = (180 / Math.PI) * Math.Atan(Fy / Fx);
                        if ((theta >= -95 && theta <= -85) || (theta >= 85 && theta <= 95))
                        {
                            Result.Data[y, x, 0] = (byte)255;
                        }
                        else
                        {
                            Result.Data[y, x, 0] = (byte)0;
                        }
                    }
                }
            }
            return Result;
        }

        public static Image<Gray, byte> XOR(Image<Gray, byte> InputImage)
        {
            Image<Gray, byte> aux = InputImage.Clone();
            for (int y = 1; y < InputImage.Height - 1; y++)
            {
                for (int x = 1; x < InputImage.Width - 1; x++)
                {
                    if (InputImage.Data[y - 1, x - 1, 0] == 255 || InputImage.Data[y, x - 1, 0] == 255 || InputImage.Data[y + 1, x - 1, 0] == 255
                        || InputImage.Data[y - 1, x, 0] == 255 || InputImage.Data[y, x, 0] == 255 || InputImage.Data[y + 1, x, 0] == 255
                        || InputImage.Data[y - 1, x + 1, 0] == 255 || InputImage.Data[y, x + 1, 0] == 255 || InputImage.Data[y + 1, x + 1, 0] == 255)
                    {
                        aux.Data[y, x, 0] = (byte)255;
                    }
                    else
                    {

                        aux.Data[y, x, 0] = (byte)0;
                    }

                }
            }

            Image<Gray, byte> Result = new Image<Gray, byte>(InputImage.Size);
            for (int y = 1; y < InputImage.Height - 1; y++)
            {
                for (int x = 1; x < InputImage.Width - 1; x++)
                {
                    if ((InputImage.Data[y, x, 0] == 0 && aux.Data[y, x, 0] == 0) || (InputImage.Data[y, x, 0] == 255 && aux.Data[y, x, 0] == 255))
                    {
                        Result.Data[y, x, 0] = (byte)0;
                    }
                    else
                    {

                        Result.Data[y, x, 0] = (byte)255;
                    }

                }
            }

            return Result;
        }

        public static Image<Gray, byte> Binarizare(Image<Gray, byte> InputImage, float t)
        {
            int val = (int)t;

            Image<Gray, byte> Result = new Image<Gray, byte>(InputImage.Size);
            for (int y = 0; y < InputImage.Height; y++)
            {
                for (int x = 0; x < InputImage.Width; x++)
                {
                    if (InputImage.Data[y, x, 0] > val)
                    {
                        Result.Data[y, x, 0] = 255;
                    }
                    else
                    {
                        Result.Data[y, x, 0] = 0;
                    }
                }
            }
            return Result;
        }

        //private static Image<Gray, byte> draw(Image<Gray, byte> InputImage)
        //{
        //    Image<Gray, byte> current = new Image<Gray, byte>(InputImage.Size);
        //    for (int i = 1; i < InputImage.Height; i++)
        //        for (int j = 1; j < InputImage.Width; j++)
        //        {
        //            current.Data[i, j, 0] = (byte)((InputImage.Data[i - 1, j, 0] + 
        //                InputImage.Data[i + 1, j, 0] +
        //                InputImage.Data[i , j-1, 0]+ 
        //                InputImage.Data[i , j+1, 0])/2- 
        //                InputImage.Data[i, j, 0]);
        //        }
        //    return current;
        //}
        public static Image<Gray, byte> Ripple(Image<Gray, byte> InputImage, double tx, double ty, double ax, double ay)
        {
            Image<Gray, byte> current = InputImage.Clone();
            Image<Gray, byte> Result = new Image<Gray, byte>(InputImage.Size);
            double Tx;
            double Ty;
            for (int i = 0; i < InputImage.Height; i++)
                for (int j = 0; j < InputImage.Width; j++)
                {
                    double sin = Math.Sin((2 * Math.PI * j) / tx);
                    Tx = i + ax * sin;
                    double cos = Math.Sin((2 * Math.PI * i) / ty);
                    Ty = j + ay * cos;
                    Tx = Math.Round(Tx);
                    Ty = Math.Round(Ty);
                    if (Tx >= InputImage.Height)
                    {
                        Tx = InputImage.Height - 1;
                    }
                    else if (Tx < 0)
                    {
                        Tx = 0;
                    }
                    if (Ty >= InputImage.Width)
                    {
                        Ty = InputImage.Width - 1;
                    }
                    else if (Ty < 0)
                    {
                        Ty = 0;
                    }

                    current.Data[i, j, 0] = (byte)(InputImage.Data[(int)Tx, (int)Ty, 0]);
                }
            return current;
        }
        public static Image<Gray, byte> Kluster(Image<Gray, byte> InputImage, double k)
        {
            Image<Gray, byte> current = InputImage.Clone();
            Image<Gray, byte> Result = new Image<Gray, byte>(InputImage.Size);
            int nrIntervale = (int)(k);
            List<int> centre = new List<int>();
            List<int> updateCentre = new List<int>();
            List<List<int>> valPtrCentre = new List<List<int>>();
            var rand = new Random();
            for (int index = 0; index < nrIntervale; index++)
            {
                int numar;
                do
                {
                    numar = rand.Next(0, 255);
                }while (centre.Contains(numar)==true);
                centre.Add(numar);
                updateCentre.Add(0);
                valPtrCentre.Add(new List<int>());
            }
            centre.Sort();
            for (int index = 0; index < nrIntervale; index++)
            {
                if (index == 0)
                {
                    valPtrCentre[index].Add(0);
                    valPtrCentre[index].Add((int)((centre[0] + centre[1]) / 2));
                }
                else if (index < nrIntervale - 1)
                {
                    valPtrCentre[index].Add(valPtrCentre[index - 1][1]);
                    valPtrCentre[index].Add((int)((centre[index] + centre[index + 1]) / 2));
                }
                else
                {
                    valPtrCentre[index].Add(valPtrCentre[index - 1][1]);
                    valPtrCentre[index].Add(256);
                }

            }
            while(!updateCentre.Equals(centre))
            {

                centre.Sort();
                updateCentre = centre;
                for (int i = 0; i < InputImage.Height; i++)
                {
                    for (int j = 0; j < InputImage.Width; j++)
                    {
                        for (int index = 0; index < nrIntervale; index++)
                        {
                            if (InputImage.Data[i, j, 0] >= valPtrCentre[index][0] && InputImage.Data[i, j, 0] < valPtrCentre[index][1])
                            {
                                if(valPtrCentre[index].Contains(InputImage.Data[i, j, 0])==false)
                                valPtrCentre[index].Add(InputImage.Data[i, j, 0]);
                                //InputImage.Data[i, j, 0] = (byte)centre[index];
                            }
                        }
                    }
                }

                for (int index = 0; index < nrIntervale; index++)
                {
                    int S = 0;
                    for (int index2 = 0; index2 < valPtrCentre[index].Count; index2++)
                    {
                        S += valPtrCentre[index][index2];
                    }

                    centre[index]=((int)(S / valPtrCentre[index].Count));
                }
            }

            for (int i = 0; i < InputImage.Height; i++)
            {
                for (int j = 0; j < InputImage.Width; j++)
                {
                    for (int index = 0; index < nrIntervale; index++)
                    {
                        if (InputImage.Data[i, j, 0] >= valPtrCentre[index][0] && InputImage.Data[i, j, 0] < valPtrCentre[index][1])
                        {
                            Result.Data[i, j, 0] = (byte)centre[index];
                        }
                    }
                }
            }
            return Result;
        }
    }
}