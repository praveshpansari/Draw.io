using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLEnvironment
{
    class Parser
    {

        public int currentIndex;
        public CommandCollection collection = new CommandCollection();
        public Node parseProgram(List<Token> t)
        {
            currentIndex = 0;
            Node node = null;
            Node ast = new Node("Program", new LinkedList<Node>());
            while (currentIndex < t.Count())
            {
                node = parseToken(t);
                ast.body.AddLast(node);
            }
            return ast;
        }




        public class Node
        {
            public String type;
            public String value;
            public LinkedList<Node> parameters = new LinkedList<Node>();
            public LinkedList<Node> body;

            public Node(String type, String value)
            {
                this.type = type;
                this.value = value;
            }

            public Node(String type, LinkedList<Node> node)
            {
                this.type = type;
                this.body = node;
            }

            public Node(String type, String value, bool isParam)
            {
                this.type = type;
                this.value = value;
                this.parameters = new LinkedList<Node>();
            }

            public LinkedList<Node> getParams()
            {
                return this.parameters;
            }

            public String getType()
            {
                return this.type;
            }

            public void addParam(Node n)
            {
                this.parameters.AddLast(n);
            }

            public String toString()
            {
                String temp = "Name: " + this.type;
                if (value != null) temp += "\nValue: " + this.value;
                if (body != null && body.Count != 0) { temp += "\nBody:\n"; foreach (Node t in body) temp += "\t" + t.toString(); }
                if (parameters != null && parameters.Count != 0) { temp += "\nParams:\n"; foreach (Node t in parameters) temp += "\t" + t.toString(); }
                return temp;
            }

        }

        Node parseNumber(List<Token> t)
        {
            currentIndex++;
            return new Node("NumericLiteral", t[currentIndex - 1].getValue());
        }

        Node parseString(List<Token> t)
        {
            currentIndex++;
            return new Node("StringLiteral", t[currentIndex - 1].getValue());
        }

        Node parseCondition(List<Token> t)
        {
            currentIndex++;
            Node node = new Node("ConditionExprIF", new LinkedList<Node>());
            node.body.AddLast(parseToken(t));
            return node;
        }

        Node parseExpression(List<Token> t)
        {
            currentIndex++;
            Node left = parseNumber(t);
            Token token = t[currentIndex];
            Node operation = new Node("Operation", token.getValue(), true);
            currentIndex++;
            Node right = parseNumber(t);
            operation.addParam(left);
            operation.addParam(right);
            token = t[currentIndex];
            if (token.getValue() == ")") currentIndex++;
            return operation;
        }

        Node parseBlock(List<Token> t)
        {
            Node node = new Node("BlockExpression", new LinkedList<Node>());
            Token token = t[currentIndex];
            while (!(token.getType() == Type.BRACKET && token.getValue() == "}") && currentIndex < t.Count - 1)
            {
                token = t[currentIndex];
                node.body.AddLast(parseExpression(t));
            }
            currentIndex++;
            return node;
        }

        Node parseIdentifier(List<Token> t)
        {
            currentIndex++;
            return new Node("Function", t[currentIndex - 1].getValue());
        }

        Node parseToken(List<Token> t)
        {
            Token token = t[currentIndex];

            if (token.getType() == Type.NUMBER)
            {
                return parseNumber(t);
            }
            if (token.getType() == Type.IF)
            {
                return parseCondition(t);
            }
            if (token.getType() == Type.BRACKET && token.getValue() == "(")
            {
                return parseExpression(t);

            }
            if (token.getType() == Type.BRACKET && token.getValue() == "{")
            {
                return parseBlock(t);
            }
            if (token.getType() == Type.IDENTIFIER && collection.containsCommand(token.getValue()))
            {
                return parseIdentifier(t);
            }
            throw new Exception(token.getType().ToString());
        }

    }
}
