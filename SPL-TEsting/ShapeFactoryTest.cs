using AssignmentASE;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SPL_Testing
{
    /// <summary>
    /// Tests for the shape factory
    /// </summary>
    [TestClass]
    public class ShapeFactoryTest
    {
        ShapeFactory shapes;

        /// <summary>
        /// Tests the shapefactory rectangle
        /// Tests shape class
        /// </summary>
        [TestMethod]
        public void TestShapeFactoryRectangle()
        {
            shapes = new ShapeFactory();
            Shape shape = shapes.GetShape("rect");
            Assert.AreEqual(shape.GetType().Name, "Rectangle");
        }

        /// <summary>
        /// Tests the shapefactory circle
        /// Tests shape class
        /// </summary>
        [TestMethod]
        public void TestShapeFactoryCircle()
        {
            ShapeFactory shapes = new ShapeFactory();
            Shape shape = shapes.GetShape("circle");
            Assert.AreEqual(shape.GetType().Name, "Circle");
        }

        /// <summary>
        /// Tests the shapefactory triangle
        /// Tests shape class
        /// </summary>
        [TestMethod]
        public void TestShapeFactoryTriangle()
        {
            ShapeFactory shapes = new ShapeFactory();
            Shape shape = shapes.GetShape("triangle");
            Assert.AreEqual(shape.GetType().Name, "Triangle");
        }

        /// <summary>
        /// Tests the shapefactory square
        /// Tests shape class
        /// </summary>
        [TestMethod]
        public void TestShapeFactorySquare()
        {
            ShapeFactory shapes = new ShapeFactory();
            Shape shape = shapes.GetShape("square");
            Assert.AreEqual(shape.GetType().Name, "Square");
        }
    }
}
