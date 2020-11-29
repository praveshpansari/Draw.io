using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentASE
{
    public class Painter
    {

        Graphics g;
        Pen pen;
        bool fill;
        ShapeFactory shapes;
        int xPos, yPos;
        System.Drawing.SolidBrush brush;
        public Painter(Graphics g)
        {
            this.g = g;
            xPos = yPos = 0;
            fill = false;
            shapes = new ShapeFactory();
            pen = new Pen(Color.Black, 1);
            brush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
        }

        public void clear()
        {
            g.Clear(Color.White);
        }

        public void SetColor(String color)
        {
            String col = color[0].ToString().ToUpper();
            col += color.Substring(1);
            pen.Color = Color.FromName(col);
            brush.Color = Color.FromName(col);
        }

        public void SetFill(String flag)
        {
            fill = (flag == "on");
        }

        public void DrawTo(int toX, int toY)
        {
            g.DrawLine(pen, xPos, yPos, toX, toY);
            xPos = toX;
            yPos = toY;
            Console.WriteLine("Line");
        }

        public void DrawTriangle(int width, int height)
        {
            if (!fill)
                g.DrawPolygon(pen, new Point[] {
                new Point(xPos, yPos),
                new Point(xPos + width, new Random().Next(yPos, yPos + height)),
                new Point(new Random().Next(xPos, xPos + width), yPos + height)
                });
            else
                g.FillPolygon(brush, new Point[] {
                new Point(xPos, yPos),
                new Point(xPos + width, new Random().Next(yPos, yPos + height)),
                new Point(new Random().Next(xPos, xPos + width), yPos + height)
                });
        }

        public void DrawShape(string shapeType, params int[] p)
        {
            int[] list = new int[p.Length + 2];
            list[0] = xPos;
            list[1] = yPos;
            for (int i = 2; i < list.Length; i++)
                list[i] = p[i - 2];
            Shape shape = shapes.getShape(shapeType);
            shape.set(Color.Black, list);
            shape.draw(g, fill, pen, brush);
        }

    }
}
