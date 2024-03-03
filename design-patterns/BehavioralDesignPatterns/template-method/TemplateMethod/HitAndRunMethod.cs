namespace TemplateMethod;
public class HitAndRunMethod : StealingMethod {
    protected internal override String PickTarget() {
        return "Identify a wealthy traveler";
    }

    protected internal override void ConfuseTarget(String target) {
        Logger.Info("Create a diversion to disorient the {0}.", target);
    }

    protected internal override void StealTheItem(String target) {
        Logger.Info("Seize the opportune moment to snatch the {0}'s belongings and disappear!", target);
    }
}