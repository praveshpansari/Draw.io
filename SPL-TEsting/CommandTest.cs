using AssignmentASE;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SPL_Testing
{
    /// <summary>
    /// Tests for the parser class and command factory 
    /// </summary>
    [TestClass]
    public class CommandTest
    {
        Painter p;

        /// <summary>
        /// Tests the moveto method
        /// Tests if the cursor has moved to x and y
        /// Tests the parsecommand method
        /// Tests the painter class
        /// </summary>
        [TestMethod]
        public void TestParseCommandMoveTo()
        {
            p = new Painter();
            Parser parser = new Parser(p);
            parser.ParseCommand("moveto 50,50", 0);
            Assert.AreEqual(50, p.XPos);
            Assert.AreEqual(50, p.YPos);
        }
        /// <summary>
        /// Tests the drawto method
        /// Tests if cursor has drawn to x y
        /// Tests the parsecommand method
        /// Tests the painter class
        /// </summary>
        [TestMethod]
        public void TestParseCommandDrawTo()
        {
            p = new Painter();
            Parser parser = new Parser(p);
            parser.ParseCommand("drawto 120,90", 0);
            Assert.AreEqual(120, p.XPos);
            Assert.AreEqual(90, p.YPos);
        }

        /// <summary>
        /// Tests the reset method
        /// Tests whether the cursor is reset on top left
        /// Tests the parsecommand method
        /// Tests the move to method
        /// Tests the painter class
        /// </summary>
        [TestMethod]
        public void TestParseCommandReset()
        {
            p = new Painter();
            Parser parser = new Parser(p);
            parser.ParseCommand("moveto 80,50", 0);
            Assert.AreEqual(80, p.XPos);
            parser.ParseCommand("reset", 0);
            Assert.AreEqual(0, p.XPos);
            Assert.AreEqual(0, p.YPos);
        }

        /// <summary>
        /// Tests the pen method
        /// Tests if the pen color changed
        /// Tests the parsecommand method
        /// Tests the painter class
        /// </summary>
        [TestMethod]
        public void TestParseCommandColor()
        {
            p = new Painter();
            Parser parser = new Parser(p);
            parser.ParseCommand("pen blue", 0);
            Assert.AreEqual("blue", p.Pen.ToLower());
        }

        /// <summary>
        /// Tests the fill method
        /// Tests if fill is on
        /// Tests the parsecommand method
        /// Tests the painter class
        /// </summary>
        [TestMethod]
        public void TestParseCommandFillOn()
        {
            p = new Painter();
            Parser parser = new Parser(p);
            parser.ParseCommand("fill on", 0);
            Assert.IsTrue(p.Fill);
        }

        /// <summary>
        /// Tests the fill method
        /// Tests if the fill is off
        /// Tests the parsecommand method
        /// Tests the painter class
        /// </summary>
        [TestMethod]
        public void TestParseCommandFillOff()
        {
            p = new Painter();
            Parser parser = new Parser(p);
            parser.ParseCommand("fill off", 0);
            Assert.IsFalse(p.Fill);
        }

        /// <summary>
        /// Tests the variable command
        /// Tests the parseusinglexer method
        /// Tests the painter class
        /// Checks if the value has been assigned to variable
        /// </summary>
        [TestMethod]
        public void TestParseCommandVariable()
        {
            p = new Painter();
            Parser parser = new Parser(p);
            parser.ParseUsingLexer("num = 5", 0);
            Assert.IsTrue(5 == Int32.Parse(parser.Variables["num"]));
        }

        /// <summary>
        /// Tests the if command
        /// Tests the parseusingif method
        /// Tests the painter class
        /// Checks if the condition has been evaluated
        /// </summary>
        [TestMethod]
        public void TestParseCommandIf()
        {
            p = new Painter();
            Parser parser = new Parser(p);
            bool result = parser.ParseUsingIf("if 5 > 20", 0);
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Tests the method command
        /// Tests the parseEditor method
        /// Tests the painter class
        /// Checks if the method has been created
        /// </summary>
        [TestMethod]
        public void TestParseCommandMethod()
        {
            p = new Painter();
            Parser parser = new Parser(p);
            parser.ParseEditor("method example()\r\ncircle 20\r\nendmethod");
            Assert.IsTrue(parser.Variables.ContainsKey("example"));
        }

        /// <summary>
        /// Tests expressions
        /// Tests the parseUsingLexer method
        /// Tests the painter class
        /// Checks if the method has been created
        /// </summary>
        [TestMethod]
        public void TestParseExpression()
        {
            p = new Painter();
            Parser parser = new Parser(p);
            parser.ParseUsingLexer("x = 15", 0);
            parser.ParseUsingLexer("num = 5 + x *2", 0);
            Assert.AreEqual(40, Int32.Parse(parser.Variables["num"]));
        }


        /// <summary>
        /// Tests the whileLoop method
        /// Tests the parseEditor method
        /// Tests the painter class
        /// Checks if the while condition has been evaluated
        /// </summary>
        [TestMethod]
        public void TestParseCommandWhile()
        {
            p = new Painter();
            Parser parser = new Parser(p);
            parser.ParseEditor("num = 5\r\nwhile num > 3\r\nnum = num - 1\r\nendwhile");
            Assert.AreEqual(3, Int32.Parse(parser.Variables["num"]));
        }

    }
}
