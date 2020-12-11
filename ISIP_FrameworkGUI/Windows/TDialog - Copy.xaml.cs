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
    public partial class tDialog : Window
    {
        MainWindow mainWindow;
        bool color;
        public tDialog(MainWindow w, bool color=true )
        {
            InitializeComponent();
            this.color = color;
            mainWindow = w;
        }

        private void Confirm_btn_Click(object sender, RoutedEventArgs e)
        {
            if (t_value.Text.Length != 0 )
            {
                if (color == true)
                {
                    mainWindow.settValue(float.Parse(t_value.Text));
                    mainWindow.Binarizare2DColorActivation();
                }
                else
                {

                    mainWindow.settValue(float.Parse(t_value.Text));
                    mainWindow.BinarizareActivation();
                }
                Close();
            }
        }
    }
}
