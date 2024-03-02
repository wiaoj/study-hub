namespace Command;
public class Wizard {
    private readonly LinkedList<Action> undoStack = new();
    private readonly LinkedList<Action> redoStack = new();

    public void CastSpell(Action spell) {
        spell.Invoke();
        this.undoStack.AddLast(spell);
    }

    public void UndoLastSpell() {
        if(this.undoStack.Count > 0) {
            Action previousSpell = this.undoStack.Last!.Value;
            this.undoStack.RemoveLast();
            this.redoStack.AddLast(previousSpell);
            previousSpell.Invoke();
        }
    }

    public void RedoLastSpell() {
        if(this.redoStack.Count > 0) {
            Action previousSpell = this.redoStack.Last!.Value;
            this.redoStack.RemoveLast();
            this.undoStack.AddLast(previousSpell);
            previousSpell.Invoke();
        }
    }

    public override String ToString() {
        return nameof(Wizard);
    }
}