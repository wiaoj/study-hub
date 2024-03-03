namespace TemplateMethod;
public class SubtleMethod : StealingMethod {
    protected internal override String PickTarget() {
        return "Select an unsuspecting merchant";
    }

    protected internal override void ConfuseTarget(String target) {
        Logger.Info("Distract the {0} with a captivating story!", target);
    }

    protected internal override void StealTheItem(String target) {
        Logger.Info("While the {0} is distracted, relieve them of their valuables.", target);
    } 
}