using Akka.TestKit.Xunit;
using Xunit;
using AMCalculator;
using Akka.Actor;

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

        [Fact]
        public void add_1_and_6_to_give_7()
        {
            var props = Props.Create<CalculatorActor>(TestActor);
            var actor = ActorOfAsTestActorRef<CalculatorActor>(props);
            actor.Tell(new Add(1, 7));
            CalculatorActor calculator = actor.UnderlyingActor;
            Assert.Equal(7, calculator.Answer);
        }
    }
}