using Akka.TestKit.Xunit;
using Xunit;
using AMCalculator;

namespace AMCalculator.Tests
{
    public class CalculatorUnitTests : TestKit
    {
        [Fact]
        public void Answer_should_initially_be_0()
        {
            var calculatorRef = ActorOfAsTestActorRef<CalculatorActor>("calculator");
            var calculator = calculatorRef.UnderlyingActor;
            Assert.Equal(0, calculator.Answer);
        }

        public void add_1_and_6_to_give_7()
        {
            TestActorRef<CalculatorActor> calculatorRef = ActorOfAsTestActorRef<CalculatorActor>("calculator");
            calculatorRef.Tell(new Add(1, 7));
            CalculatorActor calculator = calculatorRef.UnderlyingActor;
            Assert.Equal(7, calculator.Answer);
        }
    }
}