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
        String log;
        Pen cursorPen = new Pen(Color.Red);
        System.Windows.Forms.RichTextBox logBox;
        public bool fill { get; set; }
        ShapeFactory shapes;
        CommandFactory commands;
        public int xPos { get; set; }
        public int yPos { get; set; }
        System.Drawing.SolidBrush brush;
        System.Windows.Forms.PictureBox outputWindow;

        public String Pen
        {
            get { return pen.Color.Name; }
        }

        public Painter()
        {
            xPos = yPos = 0;
            log = "";
            pen = new Pen(Color.Black, 1);
            brush = new SolidBrush(Color.Black);
            commands = new CommandFactory();
            g = Graphics.FromImage(new Bitmap(400, 400));
        }

        public Painter(System.Windows.Forms.PictureBox outputWindow, System.Windows.Forms.RichTextBox logBox)
        {
            this.logBox = logBox;
            this.outputWindow = outputWindow;

            bitmap = new Bitmap(outputWindow.Width, outputWindow.Height);
            tempBit = new Bitmap(outputWindow.Width, outputWindow.Height);

            this.outputWindow.Image = bitmap;

            this.g = Graphics.FromImage(bitmap);

            xPos = yPos = 0;
            this.log = "";
            fill = false;

            shapes = new ShapeFactory();
            commands = new CommandFactory();
            pen = new Pen(Color.Black, 1);

            brush = new SolidBrush(Color.Black);
            DrawCursor();
        }

        public void Center()
        {
            xPos = outputWindow.Width / 2;
            yPos = outputWindow.Height / 2;
            log += "[" + DateTime.Now.ToString("T") + "] " + "Cursor centered in the drawing area.\r\n";
        }


        public void SetColor(String color)
        {
            bool isColorValid = false;
            foreach (KnownColor _color in Enum.GetValues(typeof(KnownColor)))
            {
                if (_color.ToString().ToUpper().Equals(color.ToUpper()))
                {
                    isColorValid = true;
                    break;
                }
            }
            if (isColorValid)
            {
                pen.Color = Color.FromName(color);
                brush.Color = Color.FromName(color);
                log += "[" + DateTime.Now.ToString("T") + "] " + "Pen and Brush color set to " + color + ".\r\n";
            }
            else
            {
                throw new Exception("Color not found");
            }
        }

        public void SetFill(String flag)
        {
            if (flag == "on")
            {
                fill = true;
                log += "[" + DateTime.Now.ToString("T") + "] " + "Set the drawing shapes to be filled.\r\n";
            }
            else if (flag == "off")
            {
                fill = false;
                log += "[" + DateTime.Now.ToString("T") + "] " + "Set the drawing shapes to be outlined.\r\n";
            }
            else
            {
                throw new Exception("Parameter mus be either 'on' or 'off'.");
            }
        }

        public void DrawCursor()
        {
            outputWindow.Image = tempBit;
            g = Graphics.FromImage(outputWindow.Image);
            g.DrawLine(cursorPen, xPos + 6, yPos, xPos - 6, yPos);
            g.DrawLine(cursorPen, xPos, yPos + 6, xPos, yPos - 6);
        }

        public void updateImage()
        {
            outputWindow.Image = bitmap;
            g = Graphics.FromImage(outputWindow.Image);
        }

        public void storeTempImage()
        {
            tempBit = new Bitmap(bitmap);
        }

        public void ExecuteCommand(string commandType, params int[] p)
        {
            int[] list = new int[p.Length + 2];
            list[0] = xPos;
            list[1] = yPos;
            for (int i = 2; i < list.Length; i++)
                list[i] = p[i - 2];
            Command command = commands.getCommand(commandType);
            try
            {
                command.set(g, pen, list);
            }
            catch (IndexOutOfRangeException)
            {
                throw new IndexOutOfRangeException();
            }
            command.execute();
            this.xPos = command.X;
            this.yPos = command.Y;
            log += command.getLog();
        }

        public void WhileLoop()
        {

        }

        public void Varaible(string var, int x)
        {

        }

        public void DrawShape(string shapeType, params int[] p)
        {
            int[] list = new int[p.Length + 2];
            list[0] = xPos;
            list[1] = yPos;
            for (int i = 2; i < list.Length; i++)
                list[i] = p[i - 2];
            Shape shape = shapes.getShape(shapeType);
            try
            {
                shape.set(Color.Black, list);
            }
            catch (IndexOutOfRangeException)
            {
                throw new IndexOutOfRangeException();
            }
            shape.draw(g, fill, pen, brush);
            log += "[" + DateTime.Now.ToString("T") + "] " + "Drew a " + shape.GetType().Name + " at (" + xPos + "," + yPos + ").\r\n";
        }

        public void WriteLog()
        {
            logBox.AppendText(log);
            log = "";
        }

        public void WriteError(String error)
        {
            logBox.SelectionColor = Color.Red;
            logBox.AppendText(error);
            logBox.SelectionColor = Color.Black;
        }
    }
}
