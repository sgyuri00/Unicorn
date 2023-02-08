using System.Windows.Shapes;
using System.Windows;

namespace GUI_2022_23_01_BLG4MG.Logic
{
    public class Bullet
    {
        public double x { get; set; }
        public double y { get; set; }
        public int where { get; set; }

        public string type { get; set; }
        public Rectangle recta { get; set; }
        public Bullet(double xcord, double ycord, int to, Rectangle rec, string Type)
        {
            x = xcord;
            y = ycord;
            where = to;
            recta = rec;
            type = Type;
        }
    }
}
