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
        Bitmap bitmap;
        Bitmap tempBit;
        Pen pen;
        Pen cursorPen = new Pen(Color.Red);
        System.Windows.Forms.TextBox logBox;
        bool fill;
        ShapeFactory shapes;
        int xPos, yPos;
        System.Drawing.SolidBrush brush;
        System.Windows.Forms.PictureBox outputWindow;

        public Painter(System.Windows.Forms.PictureBox outputWindow, System.Windows.Forms.TextBox logBox)
        {
            this.logBox = logBox;
            this.outputWindow = outputWindow;

            bitmap = new Bitmap(outputWindow.Width, outputWindow.Height);
            tempBit = new Bitmap(outputWindow.Width, outputWindow.Height);

            this.outputWindow.Image = bitmap;

            this.g = Graphics.FromImage(bitmap);

            xPos = yPos = 0;

            fill = false;

            shapes = new ShapeFactory();

            pen = new Pen(Color.Black, 1);

            brush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            DrawCursor();
        }

        public void Center()
        {
            outputWindow.Image = bitmap;
            g = Graphics.FromImage(outputWindow.Image);
            xPos = outputWindow.Width / 2;
            yPos = outputWindow.Height / 2;
            logBox.AppendText("[" + DateTime.Now.ToString("T") + "] " + "Cursor centered in the drawing area.\r\n");
            tempBit = new Bitmap(bitmap);
        }

        public void Clear()
        {
            outputWindow.Image = bitmap;
            g = Graphics.FromImage(outputWindow.Image);
            g.Clear(outputWindow.BackColor);
            logBox.AppendText("[" + DateTime.Now.ToString("T") + "] " + "Cleared the drawing area.\r\n");
            tempBit = new Bitmap(bitmap);
        }

        public void Reset()
        {
            outputWindow.Image = bitmap;
            g = Graphics.FromImage(outputWindow.Image);
            xPos = yPos = 0;
            logBox.AppendText("[" + DateTime.Now.ToString("T") + "] " + "Cursor position reset to top left.\r\n");
            tempBit = new Bitmap(bitmap);
        }

        public void MoveTo(int x, int y)
        {
            outputWindow.Image = bitmap;
            g = Graphics.FromImage(outputWindow.Image);
            xPos = x;
            yPos = y;
            logBox.AppendText("[" + DateTime.Now.ToString("T") + "] " + "Cursor moved to (" + xPos + "," + yPos + ").\r\n");
            tempBit = new Bitmap(bitmap);
        }

        public void SetColor(String color)
        {
            pen.Color = Color.FromName(color);
            brush.Color = Color.FromName(color);
            logBox.AppendText("[" + DateTime.Now.ToString("T") + "] " + "Pen and Brush color set to " + color + ".\r\n");
        }

        public void SetFill(String flag)
        {
            if (flag == "on")
            {
                fill = true;
                logBox.AppendText("[" + DateTime.Now.ToString("T") + "] " + "Set the drawing shapes to be filled.\r\n");
            }
            else
            {
                fill = false;
                logBox.AppendText("[" + DateTime.Now.ToString("T") + "] " + "Set the drawing shapes to be outlined.\r\n");
            }
        }

        public void DrawCursor()
        {
            outputWindow.Image = tempBit;
            g = Graphics.FromImage(outputWindow.Image);
            g.DrawLine(cursorPen, xPos + 6, yPos, xPos - 6, yPos);
            g.DrawLine(cursorPen, xPos, yPos + 6, xPos, yPos - 6);

        }

        public void DrawTo(int toX, int toY)
        {
            outputWindow.Image = bitmap;
            g = Graphics.FromImage(outputWindow.Image);
            g.DrawLine(pen, xPos, yPos, toX, toY);
            xPos = toX;
            yPos = toY;
            logBox.AppendText("[" + DateTime.Now.ToString("T") + "] " + "Drew a line to (" + xPos + "," + yPos + ").\r\n");
            tempBit = new Bitmap(bitmap);
        }

        public void DrawShape(string shapeType, params int[] p)
        {
            outputWindow.Image = bitmap;
            g = Graphics.FromImage(outputWindow.Image);
            int[] list = new int[p.Length + 2];
            list[0] = xPos;
            list[1] = yPos;
            for (int i = 2; i < list.Length; i++)
                list[i] = p[i - 2];
            Shape shape = shapes.getShape(shapeType);
            shape.set(Color.Black, list);
            shape.draw(g, fill, pen, brush);
            logBox.AppendText("[" + DateTime.Now.ToString("T") + "] " + "Drew a " + shape.GetType().Name + " at (" + xPos + "," + yPos + ").\r\n");
            tempBit = new Bitmap(bitmap);
        }

        public void WriteError(String error)
        {
            outputWindow.Image = bitmap;
            g = Graphics.FromImage(outputWindow.Image);
            logBox.AppendText(error);
            logBox.Refresh();
            tempBit = new Bitmap(bitmap);
        }
    }
}
