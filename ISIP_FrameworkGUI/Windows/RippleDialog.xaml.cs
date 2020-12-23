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
    /// Interaction logic for RippleDialog.xaml
    /// </summary>
    public partial class RippleDialog : Window
    {

        MainWindow Main;
        public RippleDialog(MainWindow w)
        {
            Main = w;
            InitializeComponent();
        }

        private void Confirm_btn_Click(object sender, RoutedEventArgs e)
        {
            if (tx_value.Text.Length != 0 && ty_value.Text.Length != 0 && ax_value.Text.Length != 0 && ay_value.Text.Length != 0)
            {
                Main.settxValue(double.Parse(tx_value.Text));
                Main.settyValue(double.Parse(ty_value.Text));
                Main.setaxValue(double.Parse(ax_value.Text));
                Main.setayValue(double.Parse(ay_value.Text));
                Main.RippleActivation();
                Close();
            }
        }
    }
}
