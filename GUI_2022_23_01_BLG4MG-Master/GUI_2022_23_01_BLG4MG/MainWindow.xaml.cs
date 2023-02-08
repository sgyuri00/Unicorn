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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI_2022_23_01_BLG4MG
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            GameWindow game = new GameWindow();
            App.Current.MainWindow.Hide();
            game.Show();
        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        private void How_to_play(object sender, RoutedEventArgs e)
        {
            HowToPlay htp = new HowToPlay();
            App.Current.MainWindow.Hide();
            htp.Show();
        }
    }
}
