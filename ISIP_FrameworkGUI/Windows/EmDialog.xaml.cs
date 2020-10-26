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
    /// Interaction logic for EmDialog.xaml
    /// </summary>
    public partial class EmDialog : Window
    {

        MainWindow Main;
        public EmDialog(MainWindow w)
        {
            Main = w;
            InitializeComponent();
        }

        private void Confirm_btn_Click(object sender, RoutedEventArgs e)
        {
            if(E_value.Text.Length!=0 && m_value.Text.Length!=0)
            {
                Main.setmValue(double.Parse(m_value.Text));
                Main.setEValue(float.Parse(E_value.Text));
                Main.ContrastActivation();
                Close();
            }
        }

    }
}
