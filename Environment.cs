using System;
using System.Drawing;
using System.Windows.Forms;

namespace AssignmentASE
{
    /// <summary>
    /// The main window with a command line, code editor, output window and a log.
    /// </summary>
    public partial class Environment : Form
    {

        // Instance variables for parser, painter and the output image
        Parser parser;
        Painter painter;
        Bitmap outputImage;

        /// <summary>
        /// Constructor which initializes form components and instance objects
        /// </summary>
        public Environment()
        {
            // initialize form component
            InitializeComponent();

            // Set the image dimensions to outputWindows dimensions
            outputImage = new Bitmap(outputWindow.Width, outputWindow.Height);
            painter = new Painter(outputWindow, logBox);
            parser = new Parser(painter);
        }

        /// <summary>
        /// Paint method for the output window. Draws the ouputImage on it;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void outputWindow_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImageUnscaled(outputImage, 0, 0);
        }

        /// <summary>
        /// Event handler when the user clicks on the run button.
        /// This method parses commands from the command line and code editor
        /// and executes them and displays them in the  ouputWindow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void runButton_Click(object sender, EventArgs e)
        {
            // Assign command from command line to a variable
            string input = commandLine.Text;

            // Draw the updated image on screen with the curson on it
            painter.updateImage();

            // If the command line is empty
            if (input == String.Empty)
            {
                // If the code editor is not empty
                if (codeEditor.Text != String.Empty)
                {
                    // Clear the artboard
                    parser.parseCommand("clear", 0);
                    // Clear the variables dictionary
                    parser.Variables.Clear();
                    // Parses the editor text
                    parser.parseEditor(codeEditor.Text);
                }
            }
            else
            {
                // If the command line command is not "run"
                if (!input.ToLower().Trim().Equals("run"))
                    // Parse the command line text
                    parser.parseCommand(input, 0);
                // Else parse the code editor text
                else
                {
                    // Clear the artboard
                    parser.parseCommand("clear", 0);
                    // Clear the variables dictionary
                    parser.Variables.Clear();
                    // Parses the editor text
                    parser.parseEditor(codeEditor.Text);
                }
            }

            // Write the log in the log window
            painter.WriteLog();
            // Copy the drawn image to a temporary image
            painter.storeTempImage();
            // Draw the cursor on the drawn image
            painter.DrawCursor();
            // Refresh the ouput window
            outputWindow.Refresh();
            // Display any errors if encountered in the log
            parser.displayError();
        }


        private void syntaxButton_Click(object sender, EventArgs e)
        {
            // Clear the logbox
            logBox.Clear();

            // If the editor is not empty
            if (codeEditor.Text != String.Empty)
            {
                // Clear the variables
                parser.Variables.Clear();
                // Parses the editor text
                parser.parseEditor(codeEditor.Text);
                // Clear the artboard
                parser.parseCommand("clear", 0);
                // Display any errors if encountered in the log
                parser.displayError();
                // If no errors encountered means syntax is correct
                if (logBox.Text.Equals(""))
                {
                    // Show relevant message
                    logBox.SelectionColor = Color.Green;
                    logBox.AppendText("[" + DateTime.Now.ToString("T") + "] Syntax Checked. No errors found.");
                    logBox.SelectionColor = Color.Black;
                }
            }
        }


        /// <summary>
        /// When enter key is pressed by the user after entering a command
        /// in the command line perform a run button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void commandLine_KeyDown(object sender, KeyEventArgs e)
        {
            // If the key pressed is 'enter'
            if (e.KeyCode == Keys.Enter)
            {
                // Perform a click on the run button
                runButton.PerformClick();
            }
        }

        /// <summary>
        /// Event handler for the save button click
        /// This method displays a save dialog box to user
        /// And allows them to save file in the rtf format
        /// From the code editor window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Displays a SaveFileDialog so the user can save the file
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Rich Text File | *.rtf",
                Title = "Save a Code Text File"
            };

            // Show the save file dialog
            saveFileDialog.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog.FileName != "")
            {
                // Saves the file in the rich text format
                // File type selected in the dialog box.
                this.codeEditor.SaveFile(saveFileDialog.FileName);
            }
        }

        /// <summary>
        /// Event handler for the open button click
        /// This method displays a open dialog box to user
        /// And allows them to open a rtf file in the code editor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //The result of dialogbox
            DialogResult result;
            // Get the result of the openfile dialogbox
            result = MessageBox.Show("Do you want to save your code before closing it?", "Open File", MessageBoxButtons.YesNoCancel);

            // If the user doesnt clicks cancel
            if (result != DialogResult.Cancel)
            {
                // If the user clicks yes
                if (result == DialogResult.Yes)
                    // Prompt user to save the file by clicking on the save button
                    this.saveToolStripMenuItem.PerformClick();

                // Open a new OpenFileDialog and set title and filter
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "Rich Text File | *.rtf",
                    Title = "Open a Code Text File"
                };

                // Show the open file dialog
                openFileDialog.ShowDialog();

                // If the user has selected a valid file
                if (openFileDialog.FileName != "")
                {
                    // Load the file selected by the user in the code editor
                    this.codeEditor.LoadFile(openFileDialog.FileName);
                }
            }
        }

        /// <summary>
        /// Event handler for the exit button click
        /// Exits the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // If a messageloop exists in the application
            if (Application.MessageLoop)
                // Exit the WinForms app
                Application.Exit();
            else
                // Exit the Console app
                System.Environment.Exit(1);
        }

        /// <summary>
        /// Event handle for the new file button click
        /// Clear the code editor and close the current file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // REsult of the dialog box
            DialogResult result;

            // Prompt the user to save the file
            result = MessageBox.Show("Do you want to save your code before closing it?", "New File", MessageBoxButtons.YesNoCancel);

            // If not cancel
            if (result != DialogResult.Cancel)
            {
                // If yes pressed
                if (result == DialogResult.Yes)
                    // Perform click on the save button
                    this.saveToolStripMenuItem.PerformClick();

                // Clear the editor
                this.codeEditor.Clear();
            }
        }

        /// <summary>
        /// Event handler for the about button click
        /// Shows the information about the program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Show the about message box
            MessageBox.Show("Simple Programming Environment 2020 is a program environement which contains simple commands for drawing in different shapes and colors.\n\nVersion 1.0.0\n\u00a9 2020 Pravesh Pansari.\nAll rights reserved.", "Simple Programming Environment", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Event handle for the close button "X"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            // If a messageloop exists in the application
            if (Application.MessageLoop)
                // Exit the WinForms app
                Application.Exit();
            else
                // Exit the Console app
                System.Environment.Exit(1);
        }


        /// <summary>
        /// Allows the window form app to be dragged by the mouse
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x84:
                    base.WndProc(ref m);
                    if ((int)m.Result == 0x1)
                        m.Result = (IntPtr)0x2;
                    return;
            }

            base.WndProc(ref m);
        }


    }
}
