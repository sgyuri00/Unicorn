using GUI_2022_23_01_BLG4MG.Model;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;
using Vector = System.Windows.Vector;

namespace GUI_2022_23_01_BLG4MG.Logic
{
    internal class GameLogic
    {
        public List<Bullet> bullets = new List<Bullet>();
        public int score { get; set; }

        public PlayerCharacter Dwarf;
        static Random r = new Random();

        //At the start of the game creates the player
        public void StartOfGame()
        {
            this.Dwarf = new PlayerCharacter(500);
        }
        //spawns an enemy at one of the sides of the field (random, always outside of gamewindow)
        public void SpawnEnemy(GameWindow gw)
        {
            Rectangle newEnemy = new Rectangle
            {
                Tag = "Enemy",
                Width = 120,
                Height = 120,
                Fill = Brushes.Red,
                DataContext = new Unicorn(10, 10)
                
            };
            
            gw.GameScreen.Children.Add(newEnemy);
            SpawnLocation(newEnemy);
        }
        //Helper method for SpawnEnemy, randomly puts the object to a side
        private void SpawnLocation(Rectangle enemy)
        {
            int vel = r.Next(0, 4); //0..3
            switch (vel)
            {
                case 0:
                    //LEFT
                    Canvas.SetLeft(enemy, r.Next(-200, 0));
                    Canvas.SetBottom(enemy, r.Next(0, 1920));

                    break;
                case 1:
                    //RIGHT
                    Canvas.SetLeft(enemy, r.Next(1920, 2220));
                    Canvas.SetBottom(enemy, r.Next(0, 1080));
                    break;
                case 2:
                    //TOP
                    Canvas.SetLeft(enemy, r.Next(0, 1920));
                    Canvas.SetBottom(enemy, r.Next(1920, 2120));
                    break;
                case 3:
                    //BOTTOM
                    Canvas.SetLeft(enemy, r.Next(0, 1920));
                    Canvas.SetBottom(enemy, r.Next(-200, 0));
                    break;
                default:
                    break;
            }
            if (Canvas.GetLeft(enemy) < 960)
            {
                enemy.Fill = new ImageBrush(new BitmapImage(new Uri("Images/UnicornRight.png", UriKind.RelativeOrAbsolute)));
            }
            else
            {
                enemy.Fill = new ImageBrush(new BitmapImage(new Uri("Images/unicorn.png", UriKind.RelativeOrAbsolute)));
            }
        }

        //Returns True if obj1 collides with obj2
        public bool Collides(Rectangle obj1, Rectangle obj2)
        {
            Rect obj1hitbox = new Rect(Canvas.GetLeft(obj1), Canvas.GetBottom(obj1), obj1.Width, obj1.Height);
            Rect obj2hitbox = new Rect(Canvas.GetLeft(obj2), Canvas.GetBottom(obj2), obj2.Width, obj2.Height);
            if (obj1hitbox.IntersectsWith(obj2hitbox))
            {
                return true;
            }
            return false;
        }

        public void EnemyvsBullet(Rectangle enemy, GameWindow gw)
        {
            Rect obj1hitbox = new Rect(Canvas.GetLeft(enemy), Canvas.GetBottom(enemy), enemy.Width, enemy.Height);
            foreach (var item in bullets)
            {
                if (item.type == "Sniper")
                {
                    Rectangle sBullet = new Rectangle
                    {
                        Height = 7,
                        Width = 7,
                        Fill = Brushes.Red,
                    };

                    Rect obj2hitbox = new Rect(item.x, item.y, sBullet.Width, sBullet.Height);
                    if (obj1hitbox.IntersectsWith(obj2hitbox))
                    {
                        
                        gw.GameScreen.Children.Remove(item.recta);
                        bullets.Remove(item);
                        var uni = (Unicorn)enemy.DataContext;
                        uni.Hp -= 5;
                        enemy.DataContext = uni;
                        //remove enemy
                        if (uni.Hp <= 0)
                        {
                            gw.GameScreen.Children.Remove(enemy);
                            score += 100;
                        }
                        
                    }
                }
                if (item.type == "Pistol")
                {
                    Rectangle sBullet = new Rectangle
                    {
                        Height = 7,
                        Width = 7,
                        Fill = Brushes.Red,
                    };

                    Rect obj2hitbox = new Rect(item.x, item.y, sBullet.Width, sBullet.Height);
                    if (obj1hitbox.IntersectsWith(obj2hitbox))
                    {

                        gw.GameScreen.Children.Remove(item.recta);
                        bullets.Remove(item);
                        var uni = (Unicorn)enemy.DataContext;
                        uni.Hp -= 2;
                        enemy.DataContext = uni;
                        //remove enemy
                        if (uni.Hp <= 0)
                        {
                            gw.GameScreen.Children.Remove(enemy);
                            score += 100;
                        }

                    }
                }
                if (item.type == "Rifle")
                {
                    Rectangle sBullet = new Rectangle
                    {
                        Height = 7,
                        Width = 7,
                        Fill = Brushes.Red,
                    };

                    Rect obj2hitbox = new Rect(item.x, item.y, sBullet.Width, sBullet.Height);
                    if (obj1hitbox.IntersectsWith(obj2hitbox))
                    {

                        gw.GameScreen.Children.Remove(item.recta);
                        bullets.Remove(item);
                        var uni = (Unicorn)enemy.DataContext;
                        uni.Hp -= 3;
                        enemy.DataContext = uni;
                        //remove enemy
                        if (uni.Hp <= 0)
                        {
                            gw.GameScreen.Children.Remove(enemy);
                            score += 100;
                        }

                    }
                }
            }
        }

        //MovesObject from pointA towards pointB with Xspeed
        public void Move(Rectangle Ojb1, Rectangle Obj2, int speed)
        {

            double ex = Canvas.GetLeft(Ojb1);
            double ey = Canvas.GetBottom(Ojb1);
            Point EnemyPoint = new Point(ex, ey);

            double px = Canvas.GetLeft(Obj2);
            double py = Canvas.GetBottom(Obj2);
            Point PlayerPoint = new Point(px, py);

            Vector Obj1To2Vector = new Vector(px - ex, py - ey);
            Obj1To2Vector.Normalize();

            Canvas.SetLeft(Ojb1, ex + Obj1To2Vector.X * speed);
            Canvas.SetBottom(Ojb1, ey + Obj1To2Vector.Y * speed);



        }

        //obj1 knocks back obj2 by X distance
        private void KnockBack(Rectangle Ojb1, Rectangle Obj2, int distance)
        {

            double ex = Canvas.GetLeft(Ojb1);
            double ey = Canvas.GetBottom(Ojb1);
            Point EnemyPoint = new Point(ex, ey);

            double px = Canvas.GetLeft(Obj2);
            double py = Canvas.GetBottom(Obj2);
            Point PlayerPoint = new Point(px, py);

            Vector Obj1To2Vector = new Vector(px - ex, py - ey);
            Obj1To2Vector.Normalize();
            Obj1To2Vector.Negate();

            Canvas.SetLeft(Ojb1, ex + Obj1To2Vector.X * distance);
            Canvas.SetBottom(Ojb1, ey + Obj1To2Vector.Y * distance);
        }

        //Moves the player based on the input direction (1:UP,2:DOWN,3:LEFT,4:RIGHT)
        public void MovePlayer(int direction, Rectangle player, int speed)
        {

            switch (direction)
            {
                //UP
                case 1:
                    Canvas.SetBottom(player, Canvas.GetBottom(player) + speed);
                    break;

                //DOWN
                case 2:
                    Canvas.SetBottom(player, Canvas.GetBottom(player) - speed);
                    break;

                //LEFT
                case 3:
                    Canvas.SetLeft(player, Canvas.GetLeft(player) - speed);
                    break;

                //RIGHT
                case 4:
                    Canvas.SetLeft(player, Canvas.GetLeft(player) + speed);
                    break;
            }
        }

        public void PlayeCollidedWithEnemy(Rectangle playerRectangle, Rectangle enemy, PlayerCharacter PlayerCharacter)
        {
            var enemydata = (Unicorn)enemy.DataContext;
            PlayerCharacter.TakeDamage(enemydata.Damage);
            this.KnockBack(playerRectangle, enemy, 2);
        }

        public void BulletPhysics(List<Bullet> bullets, GameWindow gw)
        {
            try
            {
                //delete bullet if outside
                for (int i = 0; i < bullets.Count; i++)
                {
                    if (bullets[i].x <= 0 || bullets[i].y <= 0 || bullets[i].x > 1520 || bullets[i].y > 900)
                    {
                        gw.GameScreen.Children.Remove(bullets[i].recta);
                        bullets.Remove(bullets[i]);
                    }
                }
            }
            catch (Exception)
            {

            }
            foreach (var bullet in bullets)
            {
                //left
                if (bullet.where == 1)
                {
                    if (bullet.type == "Sniper")
                    {
                        //speed
                        bullet.x -= 4;
                    }
                    else
                    {
                        //speed
                        bullet.x -= 2;
                    }


                    Canvas.SetLeft(bullet.recta, bullet.x);
                    Canvas.SetBottom(bullet.recta, bullet.y);
                }
                //right
                if (bullet.where == 2)
                {
                    if (bullet.type == "Sniper")
                    {
                        //speed
                        bullet.x += 4;
                    }
                    else
                    {
                        //speed
                        bullet.x += 2;
                    }


                    Canvas.SetLeft(bullet.recta, bullet.x);
                    Canvas.SetBottom(bullet.recta, bullet.y);
                }
                //up
                if (bullet.where == 3)
                {
                    if (bullet.type == "Sniper")
                    {
                        //speed
                        bullet.y += 4;
                    }
                    else
                    {
                        //speed
                        bullet.y += 2;
                    }


                    Canvas.SetLeft(bullet.recta, bullet.x);
                    Canvas.SetBottom(bullet.recta, bullet.y);
                }
                //down
                if (bullet.where == 4)
                {
                    if (bullet.type == "Sniper")
                    {
                        //speed
                        bullet.y -= 4;
                    }
                    else
                    {
                        //speed
                        bullet.y -= 2;
                    }


                    Canvas.SetLeft(bullet.recta, bullet.x);
                    Canvas.SetBottom(bullet.recta, bullet.y);
                }
            }
        }
    }
}
