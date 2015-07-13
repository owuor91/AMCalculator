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
    }
}