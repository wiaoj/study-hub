using NSubstitute;

namespace TemplateMethod.Tests;
public class StealingMethodTests {
    [Fact]
    public void Steal_InvokesAllRequiredMethods() {
        MockStealingMethod mockMethod = Substitute.ForPartsOf<MockStealingMethod>();

        mockMethod.Steal();

        mockMethod.Received(1).PickTarget();
        mockMethod.ReceivedWithAnyArgs(1).ConfuseTarget(Arg.Any<String>());
        mockMethod.ReceivedWithAnyArgs(1).StealTheItem(Arg.Any<String>());
    }

    public class MockStealingMethod : StealingMethod {
        protected internal override String PickTarget() => "Test Target";
        protected internal override void ConfuseTarget(String target) { }
        protected internal override void StealTheItem(String target) { }
    }
}