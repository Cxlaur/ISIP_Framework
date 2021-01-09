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
    /// Interaction logic for KMean.xaml
    /// </summary>
    public partial class KMean : Window
    {
        MainWindow window;
        public KMean(MainWindow w)
        {
            InitializeComponent();
            this.window = w;
        }
        private void Confirm_btn_Click(object sender, RoutedEventArgs e)
        {
            if (k_value.Text.Length != 0)
            {
                window.setkValue(double.Parse(k_value.Text));
                window.KlusterActivation();
                Close();
            }
        }
    }
}
