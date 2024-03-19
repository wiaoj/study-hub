namespace Proxy.Tests;
public class WizardTest {
    [Theory]
    [InlineData("Gandalf", "Gandalf")]
    [InlineData("Dumbledore", "Dumbledore")]
    [InlineData("Oz", "Oz")]
    [InlineData("Merlin", "Merlin")]
    public void TestToString(String wizardName, String expectedWizardName) {
        Wizard wizard = new(wizardName);
        Assert.Equal(expectedWizardName, wizard.ToString());
        Assert.Equal(expectedWizardName, wizard.Name);
    }
}