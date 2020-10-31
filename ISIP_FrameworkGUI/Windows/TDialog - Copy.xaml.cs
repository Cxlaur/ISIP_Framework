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
        public tDialog(MainWindow w)
        {
            InitializeComponent();
            mainWindow = w;
        }

        private void Confirm_btn_Click(object sender, RoutedEventArgs e)
        {
            if (t_value.Text.Length != 0 )
            {
                mainWindow.settValue(float.Parse(t_value.Text));
                mainWindow.Binarizare2DActivation();
                Close();
            }
        }
    }
}
