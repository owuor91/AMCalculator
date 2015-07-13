using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace AMCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            var system = ActorSystem.Create("calculator-system");
            IActorRef calculator = system.ActorOf<CalculatorActor>("calculator");
            var answer = calculator.Ask<Answer>(new Add(2, 9)).Result;
            Console.WriteLine("Answer: " + answer.Value);

            var answerSub = calculator.Ask<Answer>(new Subtract(78,29)).Result;
            Console.WriteLine("Answer: " +answerSub.Value);

            var lastAnswer = calculator.Ask<Answer>(GetLastAnswer.Instance).Result;
            Console.WriteLine("Last Answer: " + lastAnswer.Value);
            Console.ReadKey();
        }
    }

    public class Add
    {
        private readonly double _term1;
        private readonly double _term2;

        public Add(double term1, double term2)
        {
            _term1 = term1;
            _term2 = term2;
        }

        public double Term1 { get { return _term1; } }
        public double Term2 { get { return _term2; } }
    }


    public class Subtract
    {
        private readonly double _term1;
        private readonly double _term2;

        public Subtract(double term1, double term2)
        {
            _term1 = term1;
            _term2 = term2;
        }

        public double Term1 { get { return _term1; } }
        public double Term2 { get { return _term2; } }
    }


    public class Answer
    {
        private readonly double _value;
        public Answer(double value)
        {
            _value = value;
        }

        public double Value { get { return _value; } }
    }

    public class GetLastAnswer
    {
        private static readonly GetLastAnswer _instance = new GetLastAnswer();
        private GetLastAnswer() { }
        public static GetLastAnswer Instance { get { return _instance; } }
    }

    public class CalculatorActor : ReceiveActor
    {
        public CalculatorActor()
        {
            var answer = 0d;

            Receive<Add>(add =>
            {
                answer = add.Term1 + add.Term2;
                Sender.Tell(new Answer(answer));
            });

            Receive<Subtract>(sub =>
            {
                answer = sub.Term1 - sub.Term2;
                Sender.Tell(new Answer(answer));
            });

            Receive<GetLastAnswer>(last=> Sender.Tell(new Answer(answer)));
        }
    }
}
