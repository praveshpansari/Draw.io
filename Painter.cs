using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace PLEnvironment
{
    public class Painter
    {

        Graphics g;
        Pen pen;
        int xPos, yPos;

        public Painter(Graphics g)
        {
            this.g = g;
            xPos = yPos = 0;
            pen = new Pen(Color.Black, 1);
        }

        public void DrawTo(int toX, int toY)
        {
            g.DrawLine(pen, xPos, yPos, toX, toY);
            xPos = toX;
            yPos = toY;
            Console.WriteLine("Line");
        }

        public void DrawSquare(int width)
        {
            g.DrawRectangle(pen, xPos, yPos, width, width);
        }

    }
}
