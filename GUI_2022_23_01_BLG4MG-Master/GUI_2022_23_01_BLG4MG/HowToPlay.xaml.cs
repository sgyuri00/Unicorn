using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GUI_2022_23_01_BLG4MG
{
    /// <summary>
    /// Interaction logic for HowToPlay.xaml
    /// </summary>
    public partial class HowToPlay : Window
    {
        public HowToPlay()
        {
            InitializeComponent();
            helpText.Content = "Movement: \n     Left arrow - move left \n     Right arrow - move right \n     Up arrow - move up \n     Down arrow - move down \n\n" +
                "Change weapons: \n     F - Change to sniper \n     G - Change to rifle \n     H - Change to pistol\n\n" +
                "Shoot: \n     W - shoot up \n     A - shoot left \n     S - shoot down \n     D - shoot right";
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            App.Current.MainWindow.Show();
            this.Close();
        }
    }
}
