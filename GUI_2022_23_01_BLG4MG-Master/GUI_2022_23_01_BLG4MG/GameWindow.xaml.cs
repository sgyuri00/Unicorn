using GUI_2022_23_01_BLG4MG.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml.Linq;

namespace GUI_2022_23_01_BLG4MG
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        DispatcherTimer dt = new DispatcherTimer();
        GameLogic logic = new GameLogic();
        Rectangle Player;
        double[] cursorPos = new double[2];
        bool labelsadded = false;

        int lastButton = 1;
        string currentWeapon = "";

        int movedir = 0;
        int spawncounter = 20;
        public GameWindow()
        {
            InitializeComponent();
            

        }


        void Window_Closed(object sender, EventArgs e)
        {
            App.Current.MainWindow.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            logic.StartOfGame();
            //TEMP
            Player = new Rectangle
            {
                Tag = "Player",
                Width = 50,
                Height = 50,
                Fill = Brushes.Red
            };
            Player.Fill = new ImageBrush(new BitmapImage(new Uri("Images/dwarf.png", UriKind.RelativeOrAbsolute)));
            double left = 1000;
            Canvas.SetLeft(Player, left);
            
            double bottom = 500;
            Canvas.SetBottom(Player, bottom);
            //TEMP VEGE
            GameScreen.Children.Add(Player);
            dt.Interval = TimeSpan.FromMilliseconds(16);
            dt.Tick += GameLoop;
            dt.Start();



            
        }



        private void GameOver()
        {
            dt.Stop();
            MessageBox.Show("Game Over");
            this.Close();


        }
        private void GameLoop(object? sender, EventArgs e)
        {
            if (logic.Dwarf.Hp == 0) GameOver();
            if (movedir != 0) logic.MovePlayer(movedir, Player, 10);
            
            try
            {
                foreach (Rectangle enemy in GameScreen.Children.OfType<Rectangle>())
                {
                    if ((string)enemy.Tag == "Enemy")
                    {
                        logic.Move(enemy, Player, 2);
                        if (logic.Collides(enemy, Player)) logic.PlayeCollidedWithEnemy(Player, enemy, logic.Dwarf);
                        logic.EnemyvsBullet(enemy, this);


                    }
                }
                foreach (Rectangle bullet in GameScreen.Children.OfType<Rectangle>())
                {

                    if ((string)bullet.Tag == "Player")
                    {
                        logic.BulletPhysics(logic.bullets, this);
                    }

                }
                //score delete
                foreach (Label label in GameScreen.Children.OfType<Label>())
                {
                    if (label.Name == "Score")
                    {
                        label.Content = "Score: " + logic.score;
                    }
                    if (label.Name == "playerHp")
                    {
                        label.Content = "Hp: " + logic.Dwarf.Hp;
                    }

                }
            }
            catch (Exception)
            {

            }
            if (logic.score == 0 && labelsadded == false)
            {
                //new score
                Label lb_score = new Label() { Content = "Score: " + logic.score.ToString(), Name = "Score", FontFamily = new FontFamily("Times New Roman"), FontSize = 25 };
                Canvas.SetLeft(lb_score, 10);
                Canvas.SetTop(lb_score, 10);
                GameScreen.Children.Add(lb_score);
            }
            if (logic.Dwarf.Hp == 500 && labelsadded == false)
            {
                //new score
                Label lb_score = new Label() { Content = "Hp: " + logic.Dwarf.Hp, Name = "playerHp" , FontFamily = new FontFamily("Times New Roman"), FontSize = 25};
                Canvas.SetLeft(lb_score, 10);
                Canvas.SetTop(lb_score, 30);
                GameScreen.Children.Add(lb_score);
                labelsadded = true;
            }
            if (spawncounter == 50)
            {
                logic.SpawnEnemy(this);
                spawncounter=0;

            }
            spawncounter++;

        }
        //ROTATION OF PLAYERCHARACTER
        void GameScreen_MouseMove(object sender, MouseEventArgs e)
        {
            cursorPos[0] = Mouse.GetPosition(GameScreen).X;
            cursorPos[1] = Mouse.GetPosition(GameScreen).Y;

            double playerX = Canvas.GetLeft(Player) + Player.Width / 2;
            double playerY = Canvas.GetBottom(Player) + Player.Height / 2;

            double b = cursorPos[0] - playerX;
            double a = cursorPos[1] - playerY;
            double c = Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));

            double szamlalo = Math.Pow(c, 2) - Math.Pow(b, 2) + Math.Pow(a, 2);
            double nevezo = 2 * c * a;
            double ertek = szamlalo / nevezo;

            double res = Math.Acos(ertek) * 180 / Math.PI;
            res += 180;
            if (cursorPos[0] > playerX) { res = 360 - res; }

            Player.RenderTransform = new RotateTransform(res, Player.Width / 2, Player.Height / 2);
            if (cursorPos[0] < 960)
            {
            Player.Fill = new ImageBrush(new BitmapImage(new Uri("Images/dwarfleft.png", UriKind.RelativeOrAbsolute)));

            }
            else
            {
                Player.Fill = new ImageBrush(new BitmapImage(new Uri("Images/dwarf.png", UriKind.RelativeOrAbsolute)));
            }
        }
        //MOVEMENT
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left) { movedir = 3; }
            if (e.Key == Key.Right) { movedir = 4; }
            if (e.Key == Key.Up) { movedir = 1; }
            if (e.Key == Key.Down) { movedir = 2; }

            if (e.Key == Key.A) 
            {
                lastButton = 1;
                Check();
            }
            if (e.Key == Key.D) 
            { 
                lastButton = 2;
                Check();
            }
            if (e.Key == Key.W)
            {
                lastButton = 3;
                Check();
            }
            if (e.Key == Key.S)
            {
                lastButton = 4;
                Check();
            }

            if (e.Key == Key.H)
            {
                currentWeapon = "Pistol";
            }
            if (e.Key == Key.F)
            {
                currentWeapon = "Sniper";
            }
            if (e.Key == Key.G)
            {
                currentWeapon = "Rifle";
            }
        }
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left) { movedir = 0; }
            if (e.Key == Key.Right) { movedir = 0; }
            if (e.Key == Key.Up) { movedir = 0; }
            if (e.Key == Key.Down) { movedir = 0; }
        }
        //MOVEMENT END
        private void Check()
        {
            if (currentWeapon == "Pistol")
            {
                Rectangle newBullet = new Rectangle
                {
                    Height = 6,
                    Width = 6,
                    Fill = Brushes.Black,
                };
                Bullet newb = new Bullet(Canvas.GetLeft(Player), Canvas.GetBottom(Player), lastButton, newBullet, "Pistol");
                logic.bullets.Add(newb);

                Canvas.SetLeft(newb.recta, newb.x);
                Canvas.SetBottom(newb.recta, newb.y);

                GameScreen.Children.Add(newBullet);
            }
            else if (currentWeapon == "Sniper")
            {
                Rectangle newBullet = new Rectangle
                {
                    Height = 7,
                    Width = 7,
                    Fill = Brushes.Red,
                };
                Bullet newb = new Bullet(Canvas.GetLeft(Player), Canvas.GetBottom(Player), lastButton, newBullet, "Sniper");
                logic.bullets.Add(newb);

                Canvas.SetLeft(newb.recta, newb.x);
                Canvas.SetBottom(newb.recta, newb.y);

                GameScreen.Children.Add(newBullet);
            }
            else
            {
                Rectangle newBullet = new Rectangle
                {
                    Height = 4,
                    Width = 4,
                    Fill = Brushes.White,
                };
                Bullet newb = new Bullet(Canvas.GetLeft(Player), Canvas.GetBottom(Player), lastButton, newBullet, "Rifle");
                logic.bullets.Add(newb);

                Canvas.SetLeft(newb.recta, newb.x);
                Canvas.SetBottom(newb.recta, newb.y);

                GameScreen.Children.Add(newBullet);
            }
        }
    }
}
