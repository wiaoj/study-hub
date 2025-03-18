using NSubstitute;

namespace TemplateMethod.Tests;
public class HalflingThiefTest {
    [Fact]
    public void TestSteal() {
        StealingMethod method = Substitute.For<StealingMethod>();
        HalflingThief thief = new(method);

        thief.Steal();
        method.Received().Steal();
        String target = method.Received().PickTarget();
        method.Received().ConfuseTarget(target);
        method.Received().StealTheItem(target);

        method.Received(1).Steal();
    }

    [Fact]
    public void TestChangeMethod() {
        StealingMethod initialMethod = Substitute.For<StealingMethod>();
        HalflingThief thief = new(initialMethod);

        thief.Steal();
        initialMethod.Received(1).Steal();
        initialMethod.ClearReceivedCalls();

        StealingMethod newMethod = Substitute.For<StealingMethod>();
        thief.ChangeMethod(newMethod);

        thief.Steal();
        newMethod.Received(1).Steal();

        initialMethod.DidNotReceive().Steal();
    }
}