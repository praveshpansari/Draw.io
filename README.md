# PLEnvironment
A simple programming language enivronment created with C#

## Documentation ##
Documentation is present in the document folder along with test summary, xml documentation and word documentation.

## Examples
![image](https://user-images.githubusercontent.com/25385289/198564112-8cb3ac44-ff4b-42a7-8c99-8b4ea4334599.png)
![image](https://user-images.githubusercontent.com/25385289/198564174-443387fe-b160-4767-8327-c4aaca0197b8.png)


## About

### Features
 - Can create and save functions
 - Can load and save programs
 - Can view logs
 - Create complex shapes

### Interface
This program creates a simple programming environment, where commands can be used to manipulate shapes and lines on an artboard. The artboard is the place where all drawings and the cursor are rendered. The menu allows access to different file operations, exit, and the about information. The current position of the cursor on the artboard is represented by the red cross. The command line runs single commands while the code editor can run multiple commands where every command is on a new line. Commands can be executed using the run button. The output box displays the log information and error information for the commands. The syntax button analyzes the code in the code editor and displays any errors it found in the log box.

### Functions
Four types of shapes can be drawn: rectangle, square, circle and triangle. And a line can be drawn given a destination position. The shapes can be drawn outlined or filled. The color of shapes and lines can be changed. The cursor can be reset to top left, moved to center or any position in the artboard. The board can be cleared, which removes all drawings on it. The file menu allows the user to create new file, save the code, open a file and exit the program. The help menu consists of information about the program.

Variables can be declared in this program. They must contain only letters and can be assigned to integers only. Complex operations can be performed in the assignment statement including 5 types of mathematical operations. Commands can be run conditionally using if statements. They can be used as single line or a complete block of code. While loops can be used to redo commands until a stop condition. Methods can be declared and be called. Both parameterized and parameter less methods can be implemented. Then they can be called by passing appropriate parameters.

### Commands
Commands:

- **drawto** *int x, int y* – Draws a line from current position to (x, y)
- **moveto** *int x, int y* – Moves the cursor to (x, y)
- **pen** *string color* – Changes the Pen color to color
- **fill** *string input* – Sets the fill of the shapes (on or off)
- **rect** *int width, int height* – Draws a rectangle of width and height
- **square** *int size* – Draws a square of length size
- **circle** *int radius* – Draws a circle of radius
- **triangle** *int base, int height* – Draws a triangle with base and height
- **clear** – Clears the drawing area
- **reset** – Reset the circle position to (0, 0)
- **tocenter** – Moves the cursor to center
- **string** test = int value* – Creates a variable named test and assigns value to it
- **if** *var* **op** *int x* – Compares var against x using comparison operator op
- **while** *var* **op** *int x* – Loops till the condition var against x is true using op
- **method** *example (param)* – Creates a function example with parameter param

