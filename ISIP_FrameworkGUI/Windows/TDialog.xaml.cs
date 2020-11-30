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
using System.Windows.Shapes;

namespace ISIP_FrameworkGUI.Windows
{
    /// <summary>
    /// Interaction logic for TDialog.xaml
    /// </summary>
    public partial class TDialog : Window
    {
        MainWindow mainWindow;
        Boolean sobel;
        public TDialog(MainWindow w,Boolean sobel=false)
        {
            InitializeComponent();
            mainWindow = w;
            this.sobel = sobel;
        }

        private void Confirm_btn_Click(object sender, RoutedEventArgs e)
        {
            if (T_value.Text.Length != 0)
            {
                if (sobel == false)
                {
                    mainWindow.setTValue(int.Parse(T_value.Text));
                    mainWindow.Binarizare3DActivation();
                    Close();
                }
                else
                {
                    mainWindow.setTValue(double.Parse(T_value.Text));
                    mainWindow.SobelActivation();
                    Close();
                }
            }
        }
    }
}
