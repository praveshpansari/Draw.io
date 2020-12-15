using System;

namespace AssignmentASE
{
    /// <summary>
    /// This is the factory for the <see cref="Shape"/>
    /// </summary>
    /// <remarks>
    /// Allows to create an object of shapes below.
    /// <list type="bullet">
    /// <item>
    /// <term>Rectangle</term>
    /// <description><see cref="Rectangle"/></description>
    /// </item>
    /// <item>
    /// <term>Square</term>
    /// <description><see cref="Square"/></description>
    /// </item>
    /// <item>
    /// <term>Circle</term>
    /// <description><see cref="Circle"/></description>
    /// </item>
    /// <item>
    /// <term>Triangle</term>
    /// <description><see cref="Triangle"/></description>
    /// </item>
    /// </list>
    /// </remarks>
    public class ShapeFactory
    {
        /// <summary>
        /// Used to instantiate and get a specific Shape object
        /// </summary>
        /// <param name="shapeType">The shape type of the shape to be generated</param>
        /// <returns>An instance of the inherited classes of <see cref="Shape"/></returns>
        /// <exception cref="ArgumentException"></exception>
        public Shape GetShape(String shapeType)
        {
            shapeType = shapeType.ToUpper().Trim();

            if (shapeType.Equals("CIRCLE"))
            {
                return new Circle();
            }
            else if (shapeType.Equals("RECT"))
            {
                return new Rectangle();
            }
            else if (shapeType.Equals("SQUARE"))
            {
                return new Square();
            }
            else if (shapeType.Equals("TRIANGLE"))
            {
                return new Triangle();
            }
            else
            {
                //if we get here then what has been passed in is inkown so throw an appropriate exception
                System.ArgumentException argEx = new System.ArgumentException("Factory error: " + shapeType + " does not exist");
                throw argEx;
            }
        }
    }
}
