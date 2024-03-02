using Xunit.Abstractions;

namespace Command.Tests;
public class CommandTests(ITestOutputHelper output) {
    private const String GOBLIN = "Goblin";

    [Fact]
    public void TestCommand() {
        Wizard wizard = new();
        Goblin goblin = new();

        wizard.CastSpell(goblin.ChangeSize);
        VerifyGoblin(goblin, GOBLIN, Size.SMALL, Visibility.VISIBLE);

        wizard.CastSpell(goblin.ChangeVisibility);
        VerifyGoblin(goblin, GOBLIN, Size.SMALL, Visibility.INVISIBLE);

        wizard.UndoLastSpell();
        VerifyGoblin(goblin, GOBLIN, Size.SMALL, Visibility.VISIBLE);

        wizard.UndoLastSpell();
        VerifyGoblin(goblin, GOBLIN, Size.NORMAL, Visibility.VISIBLE);

        wizard.RedoLastSpell();
        VerifyGoblin(goblin, GOBLIN, Size.SMALL, Visibility.VISIBLE);

        wizard.RedoLastSpell();
        VerifyGoblin(goblin, GOBLIN, Size.SMALL, Visibility.INVISIBLE);
    }

    private void VerifyGoblin(Goblin goblin, String expectedName, Size expectedSize, Visibility expectedVisibility) {
        Assert.Equal(expectedName, goblin.ToString());
        Assert.Equal(expectedSize, goblin.Size);
        Assert.Equal(expectedVisibility, goblin.Visibility);
        output.WriteLine(goblin.Status());
    }
}