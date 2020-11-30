using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;

using System.IO;
using Emgu.CV;
using Emgu.CV.Structure;
using ISIP_UserControlLibrary;

using ISIP_Algorithms.Tools;
using ISIP_FrameworkHelpers;

namespace ISIP_FrameworkGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>


    public partial class MainWindow : Window
    {
        //private Windows.Grafica dialog;
        Windows.Magnifyer MagnifWindow;
        Windows.GLine RowDisplay;
        Windows.EmDialog Em;
        Windows.TDialog Tdialog;
        Windows.tDialog tdialog;


        bool Magif_SHOW = false;
        bool GL_ROW_SHOW = false;
        bool Em_SHOW = false;
        bool T_SHOW = false;

        System.Windows.Point lastClick = new System.Windows.Point(0, 0);
        System.Windows.Point upClick = new System.Windows.Point(0, 0);


        private double m;
        private float E;
        private double T;
        private float t;

        public MainWindow()
        {
            InitializeComponent();
            mainControl.OriginalImageCanvas.MouseDown += new MouseButtonEventHandler(OriginalImageCanvas_MouseDown);
        }

        void OriginalImageCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lastClick = Mouse.GetPosition(mainControl.OriginalImageCanvas);
            DrawHelper.RemoveAllLines(mainControl.OriginalImageCanvas);
            DrawHelper.RemoveAllRectangles(mainControl.OriginalImageCanvas);
            DrawHelper.RemoveAllLines(mainControl.ProcessedImageCanvas);
            DrawHelper.RemoveAllRectangles(mainControl.ProcessedImageCanvas);
            if (GL_ROW_ON.IsChecked)
            {
                DrawHelper.DrawAndGetLine(mainControl.OriginalImageCanvas, new System.Windows.Point(0, lastClick.Y),
                     new System.Windows.Point(mainControl.OriginalImageCanvas.Width - 1, lastClick.Y), System.Windows.Media.Brushes.Red, 1);
                if (mainControl.ProcessedGrayscaleImage != null)
                {
                    DrawHelper.DrawAndGetLine(mainControl.ProcessedImageCanvas, new System.Windows.Point(0, lastClick.Y),
                     new System.Windows.Point(mainControl.ProcessedImageCanvas.Width - 1, lastClick.Y), System.Windows.Media.Brushes.Red, 1);
                }
                if (mainControl.OriginalGrayscaleImage != null) RowDisplay.Redraw((int)lastClick.Y);

            }
            if (Magnifyer_ON.IsChecked)
            {
                DrawHelper.DrawAndGetLine(mainControl.OriginalImageCanvas, new System.Windows.Point(0, lastClick.Y),
                    new System.Windows.Point(mainControl.OriginalImageCanvas.Width - 1, lastClick.Y), System.Windows.Media.Brushes.Red, 1);
                DrawHelper.DrawAndGetLine(mainControl.OriginalImageCanvas, new System.Windows.Point(lastClick.X, 0),
                    new System.Windows.Point(lastClick.X, mainControl.OriginalImageCanvas.Height - 1), System.Windows.Media.Brushes.Red, 1);
                DrawHelper.DrawAndGetRectangle(mainControl.OriginalImageCanvas, new System.Windows.Point(lastClick.X - 4, lastClick.Y - 4),
                    new System.Windows.Point(lastClick.X + 4, lastClick.Y + 4), System.Windows.Media.Brushes.Red);
                if (mainControl.ProcessedGrayscaleImage != null)
                {
                    DrawHelper.DrawAndGetLine(mainControl.ProcessedImageCanvas, new System.Windows.Point(0, lastClick.Y),
                    new System.Windows.Point(mainControl.ProcessedImageCanvas.Width - 1, lastClick.Y), System.Windows.Media.Brushes.Red, 1);
                    DrawHelper.DrawAndGetLine(mainControl.ProcessedImageCanvas, new System.Windows.Point(lastClick.X, 0),
                        new System.Windows.Point(lastClick.X, mainControl.ProcessedImageCanvas.Height - 1), System.Windows.Media.Brushes.Red, 1);
                    DrawHelper.DrawAndGetRectangle(mainControl.ProcessedImageCanvas, new System.Windows.Point(lastClick.X - 4, lastClick.Y - 4),
                        new System.Windows.Point(lastClick.X + 4, lastClick.Y + 4), System.Windows.Media.Brushes.Red);
                }
                if (mainControl.OriginalGrayscaleImage != null) MagnifWindow.RedrawMagnifyer(lastClick);
            }
        }

        private void openGrayscaleImageMenuItem_Click(object sender, RoutedEventArgs e)
        {
            mainControl.LoadImageDialog(ImageType.Grayscale);
            Magnifyer_ON.IsEnabled = true;
            GL_ROW_ON.IsEnabled = true;

        }

        private void openColorImageMenuItem_Click(object sender, RoutedEventArgs e)
        {
            mainControl.LoadImageDialog(ImageType.Color);
            Magnifyer_ON.IsEnabled = true;
            GL_ROW_ON.IsEnabled = true;
        }

        private void saveProcessedImageMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (!mainControl.SaveProcessedImageToDisk())
            {
                MessageBox.Show("Processed image not available!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void saveAsOriginalMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (mainControl.ProcessedGrayscaleImage != null)
            {
                mainControl.OriginalGrayscaleImage = mainControl.ProcessedGrayscaleImage;
            }
            else if (mainControl.ProcessedColorImage != null)
            {
                mainControl.OriginalColorImage = mainControl.ProcessedColorImage;
            }
        }

        private void Invert_Click(object sender, RoutedEventArgs e)
        {
            if (mainControl.OriginalGrayscaleImage != null)
            {

                mainControl.ProcessedGrayscaleImage = Tools.Invert(mainControl.OriginalGrayscaleImage);
            }

        }
        private void Mirror_Click(object sender, RoutedEventArgs e)
        {
            if (mainControl.OriginalGrayscaleImage != null)
            {

                mainControl.ProcessedGrayscaleImage = Tools.Mirror(mainControl.OriginalGrayscaleImage);
            }

        }
        private void Contrast_Click(object sender, RoutedEventArgs e)
        {
            if (mainControl.OriginalGrayscaleImage != null)
            {
                if (Em_SHOW)
                {
                    Em_SHOW = false;
                    Em.Close();
                }
                else
                {
                    Em = new Windows.EmDialog(this);
                    Em.Show();
                    Em_SHOW = true;
                }
                // mainControl.ProcessedGrayscaleImage=Tools.Mirror(mainControl.OriginalGrayscaleImage);
            }

        }

        private void Magnifyer_ON_Click(object sender, RoutedEventArgs e)
        {
            if (mainControl.OriginalGrayscaleImage != null)
            {
                if (Magif_SHOW == true)
                {
                    Magif_SHOW = false;
                    MagnifWindow.Close();
                    DrawHelper.RemoveAllLines(mainControl.OriginalImageCanvas);
                    DrawHelper.RemoveAllRectangles(mainControl.OriginalImageCanvas);
                    DrawHelper.RemoveAllLines(mainControl.ProcessedImageCanvas);
                    DrawHelper.RemoveAllRectangles(mainControl.ProcessedImageCanvas);

                }
                else Magif_SHOW = true;
                if (Magif_SHOW == true)
                {
                    MagnifWindow = new Windows.Magnifyer(mainControl.OriginalGrayscaleImage, mainControl.ProcessedGrayscaleImage);
                    MagnifWindow.Show();
                    MagnifWindow.RedrawMagnifyer(lastClick);
                }
            }

        }

        private void GL_ROW_ON_Click(object sender, RoutedEventArgs e)
        {
            if (mainControl.OriginalGrayscaleImage != null)
            {
                if (GL_ROW_SHOW == true)
                {
                    GL_ROW_SHOW = false;
                    RowDisplay.Close();
                    DrawHelper.RemoveAllLines(mainControl.OriginalImageCanvas);
                    DrawHelper.RemoveAllLines(mainControl.ProcessedImageCanvas);
                }
                else GL_ROW_SHOW = true;

                if (GL_ROW_SHOW == true)
                {
                    RowDisplay = new Windows.GLine(mainControl.OriginalGrayscaleImage, mainControl.ProcessedGrayscaleImage);

                    RowDisplay.Show();
                    RowDisplay.Redraw((int)lastClick.Y);

                }
            }
        }

        private void Bin_3D_Click(object sender, RoutedEventArgs e)
        {

            if (mainControl.OriginalColorImage != null)
            {
                if (T_SHOW)
                {
                    T_SHOW = false;
                    Tdialog.Close();
                }
                else
                {
                    Tdialog = new Windows.TDialog(this);
                    Tdialog.Show();
                    T_SHOW = true;
                }
                // mainControl.ProcessedGrayscaleImage=Tools.Mirror(mainControl.OriginalGrayscaleImage);
            }
        }

        private void Bin_2D_Click(object sender, RoutedEventArgs e)
        {
            if (mainControl.OriginalColorImage != null)
            {
                if (T_SHOW)
                {
                    T_SHOW = false;
                    tdialog.Close();
                }
                else
                {
                    tdialog = new Windows.tDialog(this);
                    tdialog.Show();
                    T_SHOW = true;
                }
                // mainControl.ProcessedGrayscaleImage=Tools.Mirror(mainControl.OriginalGrayscaleImage);
            }
        }

        private void Kuwahara_Click(object sender, RoutedEventArgs e)
        {
            if (mainControl.OriginalGrayscaleImage != null)
            {
               mainControl.ProcessedGrayscaleImage=Tools.Kuwahara(mainControl.OriginalGrayscaleImage);
            }
        }
        
        private void Sobel_Click(object sender, RoutedEventArgs e)
        {
            if (mainControl.OriginalGrayscaleImage != null)
            {
                Tdialog = new Windows.TDialog(this,true);
                Tdialog.Show();
            }
        }


        public void setTValue(double Tvalue)
        {
            T = Tvalue;
        }
        public void settValue(float tvalue)
        {
            t = tvalue;
        }

        public void setmValue(double newM)
        {
            m = newM;
        }

        public void setEValue(float newE)
        {
            E = newE;
        }

        public void ContrastActivation()
        {
            mainControl.ProcessedGrayscaleImage = Tools.Contrast(mainControl.OriginalGrayscaleImage, m, E);
        }


        public void Binarizare3DActivation()
        {
            mainControl.ProcessedColorImage = Tools.Bin3D(mainControl.OriginalColorImage, T, lastClick);
        }
        
        public void Binarizare2DActivation()
        {
            mainControl.ProcessedColorImage = Tools.Bin2D(mainControl.OriginalColorImage, t, lastClick);
        }
        public void SobelActivation()
        {
            mainControl.ProcessedGrayscaleImage = Tools.Sobel(mainControl.OriginalGrayscaleImage, T);
        }



    }
}
