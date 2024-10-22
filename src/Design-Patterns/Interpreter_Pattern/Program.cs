namespace Interpreter_Pattern
{
    // 抽象表达式
    public abstract class Expression
    {
        public abstract int Interpret(Dictionary<string, int> context);
    }

    // 终结符表达式（变量）
    public class TerminalExpression : Expression
    {
        private string _variable;

        public TerminalExpression(string variable)
        {
            _variable = variable;
        }

        public override int Interpret(Dictionary<string, int> context)
        {
            return context[_variable];
        }
    }

    // 非终结符表达式（运算）
    public abstract class NonterminalExpression : Expression
    {
        protected Expression _left;
        protected Expression _right;

        public NonterminalExpression(Expression left, Expression right)
        {
            _left = left;
            _right = right;
        }
    }

    // 加法运算
    public class AddExpression : NonterminalExpression
    {
        public AddExpression(Expression left, Expression right)
            : base(left, right) { }

        public override int Interpret(Dictionary<string, int> context)
        {
            return _left.Interpret(context) + _right.Interpret(context);
        }
    }

    // 减法运算
    public class SubExpression : NonterminalExpression
    {
        public SubExpression(Expression left, Expression right)
            : base(left, right) { }

        public override int Interpret(Dictionary<string, int> context)
        {
            return _left.Interpret(context) - _right.Interpret(context);
        }
    }

    // 乘法运算
    public class MulExpression : NonterminalExpression
    {
        public MulExpression(Expression left, Expression right)
            : base(left, right) { }

        public override int Interpret(Dictionary<string, int> context)
        {
            return _left.Interpret(context) * _right.Interpret(context);
        }
    }

    // 除法运算
    public class DivExpression : NonterminalExpression
    {
        public DivExpression(Expression left, Expression right)
            : base(left, right) { }

        public override int Interpret(Dictionary<string, int> context)
        {
            int rightValue = _right.Interpret(context);
            if (rightValue == 0)
            {
                throw new DivideByZeroException();
            }
            return _left.Interpret(context) / rightValue;
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            // 定义变量
            var variableA = new TerminalExpression("A");
            var variableB = new TerminalExpression("B");

            // 定义表达式：A + B * 2
            var two = new TerminalExpression("2"); // 常量也可以视为终结符
            var mul = new MulExpression(variableB, two);
            var expr = new AddExpression(variableA, mul);

            // 上下文
            var context = new Dictionary<string, int>
        {
            { "A", 5 },
            { "B", 3 },
            { "2", 2 } // 虽然2是常量，但这里为了演示也放入了上下文
        };

            // 解释和执行
            int result = expr.Interpret(context);
            Console.WriteLine($"结果：{result}");
        }
    }
}
