using System;
using System.Drawing;

namespace AssignmentASE
{
    /// <summary>
    /// Responsible for drawing on a graphics object and write log
    /// </summary>
    /// <remarks>
    /// Contains methods for drawing shapes, lines and setting pen and cursor
    /// </remarks>
    public class Painter
    {
        // The graphics object
        Graphics g;
        // The image the shapes are to be drawn on
        Bitmap bitmap;
        // The temporary image before the cursor is drawn
        Bitmap tempBit;
        // The Pen used for drawing
        Pen pen;
        // The log of every action performed
        String log;
        // The pen for drawing the cursor
        Pen cursorPen = new Pen(Color.Red);
        // The output box where the log is written
        System.Windows.Forms.RichTextBox logBox;

        /// <summary>
        /// The fill flag for the drawing
        /// </summary>
        public bool fill { get; set; }

        // The shape factory for generating shapes
        ShapeFactory shapes;

        // The command factory for generating commands
        CommandFactory commands;

        /// <summary>
        /// The x-coordinate of the cursor
        /// </summary>
        public int xPos { get; set; }

        /// <summary>
        /// The y-coordinate of the cursor
        /// </summary>
        public int yPos { get; set; }

        // The brush used to draw filled shapes
        SolidBrush brush;
        // The output window where the image is drawn
        System.Windows.Forms.PictureBox outputWindow;

        /// <summary>
        /// Returns the color name of the pen
        /// </summary>
        public String Pen
        {
            get { return pen.Color.Name; }
        }

        /// <summary>
        /// Constructor with no paramters which initializes only the the drawing tools
        /// </summary>
        public Painter()
        {
            xPos = yPos = 0;
            log = "";
            pen = new Pen(Color.Black, 1);
            brush = new SolidBrush(Color.Black);
            commands = new CommandFactory();
            g = Graphics.FromImage(new Bitmap(400, 400));
        }

        /// <summary>
        /// The parameterized constructor for initializing the drawing tools, images and the cursor
        /// </summary>
        /// <param name="outputWindow">The picturebox where the image is to be drawn</param>
        /// <param name="logBox">The rich text box where the log and error of the program is displayed</param>
        /// <remarks>
        /// Sets cursor to top left initially and creates two images of ouput window size
        /// </remarks>
        public Painter(System.Windows.Forms.PictureBox outputWindow, System.Windows.Forms.RichTextBox logBox)
        {
            this.logBox = logBox;

            this.outputWindow = outputWindow;

            // Images initialized to of size of the ouput window
            bitmap = new Bitmap(outputWindow.Width, outputWindow.Height);
            tempBit = new Bitmap(outputWindow.Width, outputWindow.Height);

            // Set the image of the output window
            this.outputWindow.Image = bitmap;

            // Set the graphics object from the image
            this.g = Graphics.FromImage(bitmap);

            xPos = yPos = 0;
            this.log = "";
            fill = false;

            shapes = new ShapeFactory();
            commands = new CommandFactory();
            pen = new Pen(Color.Black, 1);

            brush = new SolidBrush(Color.Black);

            // Draw the cursor at current xPos and yPos
            DrawCursor();
        }

        /// <summary>
        /// The command to center the cursor position in the output window
        /// </summary>
        public void Center()
        {
            // Set the xpos and ypos to output width / 2 and height / 2
            xPos = outputWindow.Width / 2;
            yPos = outputWindow.Height / 2;
            log += "[" + DateTime.Now.ToString("T") + "] " + "Cursor centered in the drawing area.\r\n";
        }

        /// <summary>
        /// The command to set the color of the pen and brush
        /// </summary>
        /// <param name="color">The color which the pen is to be set</param>
        /// <remarks>Also sets the color of vrush used to draw filled shapes</remarks>
        /// <exception cref="ArgumentException"></exception>
        public void SetColor(String color)
        {
            // flag whether the color is valid
            bool isColorValid = false;

            // Look fo all colors in KnownColor enum
            foreach (KnownColor _color in Enum.GetValues(typeof(KnownColor)))
            {
                // If the color exists
                if (_color.ToString().ToUpper().Equals(color.ToUpper()))
                {
                    // Set the flag to true and break from the loop
                    isColorValid = true;
                    break;
                }
            }

            // if the color is valid
            if (isColorValid)
            {
                // Set the pen and brush color
                pen.Color = Color.FromName(color);
                brush.Color = Color.FromName(color);
                log += "[" + DateTime.Now.ToString("T") + "] " + "Pen and Brush color set to " + color + ".\r\n";
            }
            else
                // Else throw color not found error
                throw new ArgumentException("Color not found");
        }

        /// <summary>
        /// The command to set whether the shapes drawn are filled or not
        /// </summary>
        /// <param name="flag">'On' for fill and 'off' for no fill</param>
        /// <exception cref="ArgumentException"></exception>
        public void SetFill(String flag)
        {
            // If the flag is "on"
            if (flag == "on")
            {
                // Set the fill to true
                fill = true;
                log += "[" + DateTime.Now.ToString("T") + "] " + "Set the drawing shapes to be filled.\r\n";
            }
            // If the flag is "off"
            else if (flag == "off")
            {
                // Set the fill to false
                fill = false;
                log += "[" + DateTime.Now.ToString("T") + "] " + "Set the drawing shapes to be outlined.\r\n";
            }
            else
            {
                // Else throw paramter wrong error
                throw new ArgumentException("Parameter mus be either 'on' or 'off'.");
            }
        }

        /// <summary>
        /// Draws the cursor on the temporary image
        /// </summary>
        /// <remarks>To be called after every method</remarks>
        public void DrawCursor()
        {
            // Set the output window image as the temporary image
            outputWindow.Image = tempBit;
            // Get the graphics object from the current image in the output window
            g = Graphics.FromImage(outputWindow.Image);
            // Draw the cursor
            g.DrawLine(cursorPen, xPos + 6, yPos, xPos - 6, yPos);
            g.DrawLine(cursorPen, xPos, yPos + 6, xPos, yPos - 6);
        }

        /// <summary>
        /// Updates the output window image to be the original image
        /// </summary>
        /// <remarks>To be called before every method</remarks>
        public void updateImage()
        {
            // Set the output window image as the original image
            outputWindow.Image = bitmap;
            // Get the graphics object from the current image in the output window
            g = Graphics.FromImage(outputWindow.Image);
        }

        /// <summary>
        /// Copies the original image to a temporary image
        /// </summary>
        /// <remarks>To be used before <see cref="DrawCursor"/> and after <see cref="updateImage"/></remarks>
        public void storeTempImage()
        {
            // Create a clone of the original image
            tempBit = new Bitmap(bitmap);
        }

        /// <summary>
        /// Responsible for executing the following commands present in <see cref="CommandFactory"/>
        /// </summary>
        /// <param name="commandType">The command to be executed</param>
        /// <param name="p">The list of arguments for the respective commands</param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public void ExecuteCommand(string commandType, params int[] p)
        {
            // List of parameters of length the argument list + 2
            int[] list = new int[p.Length + 2];
            // Set the 1st and 2nd element of the list as the xpos and ypos
            list[0] = xPos;
            list[1] = yPos;

            // Add the elements from the argument list
            for (int i = 2; i < list.Length; i++)
                list[i] = p[i - 2];

            // Generate an command using the command factory
            Command command = commands.getCommand(commandType);

            try
            {
                // Try to set the command using the provided arguments
                command.set(g, pen, list);
            }
            catch (IndexOutOfRangeException)
            {
                // Catch and Throw Exception if parameters length invalid
                throw new IndexOutOfRangeException();
            }

            // Execute the  command
            command.execute();

            // Set the coordinates of the cursor after execution
            this.xPos = command.X;
            this.yPos = command.Y;

            // Add the log
            log += command.getLog();
        }

        /// <summary>
        /// Responsible for drawing the shapes present in <see cref="ShapeFactory"/>
        /// </summary>
        /// <param name="shapeType">The shape type to be drawn</param>
        /// <param name="p">The list of parameters for the respective shape</param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public void DrawShape(string shapeType, params int[] p)
        {
            // List of parameters of length the argument list + 2
            int[] list = new int[p.Length + 2];
            // Set the 1st and 2nd element of the list as the xpos and ypos
            list[0] = xPos;
            list[1] = yPos;

            // Add the elements from the argument list
            for (int i = 2; i < list.Length; i++)
                list[i] = p[i - 2];

            // Generate an shape from the shape factory
            Shape shape = shapes.getShape(shapeType);

            try
            {
                // Try to set the shape with given arguments
                shape.set(list);
            }
            catch (IndexOutOfRangeException)
            {
                // Catch and Throw exception if invalid parameters
                throw new IndexOutOfRangeException();
            }

            // Draw the shape 
            shape.draw(g, fill, pen, brush);

            // Log the info
            log += "[" + DateTime.Now.ToString("T") + "] " + "Drew a " + shape.GetType().Name + " at (" + xPos + "," + yPos + ").\r\n";
        }

        /// <summary>
        /// Write the log in the logbox
        /// </summary>
        public void WriteLog()
        {
            logBox.AppendText(log);
            log = "";
        }

        /// <summary>
        /// The while loop command for the while loop
        /// </summary>
        /// <param name="var">The value for which the condition is to bec checked</param>
        /// <param name="op">The type of check to be done</param>
        /// <param name="value">The value against which to check the operation</param>
        public void WhileLoop(int var, char op, int value)
        {
            // TODO: Implement while loop
        }

        /// <summary>
        /// The variable assignment command
        /// </summary>
        /// <param name="var">The variable name to which the value is assigned</param>
        /// <param name="x">The value to be assigned to the variable</param>
        public void Varaible(string var, int x)
        {
            // TODO: Implement the var command
        }

        /// <summary>
        /// Write the error in the logbox
        /// </summary>
        /// <param name="error"></param>
        public void WriteError(String error)
        {
            logBox.SelectionColor = Color.Red;
            logBox.AppendText(error);
            logBox.SelectionColor = Color.Black;
        }
    }
}
