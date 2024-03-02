namespace Command;
public abstract class Target {
    private Size size;
    private Visibility visibility;

    public Size Size => this.size;
    public Visibility Visibility => this.visibility;

    protected Target(Size size, Visibility visibility) {
        this.size = size;
        this.visibility = visibility;
    }

    public void ChangeSize() {
        Size oldSize = this.size == Size.SMALL ? Size.NORMAL : Size.SMALL;
        this.size = oldSize;
    }

    public void ChangeVisibility() {
        Visibility visible = this.visibility == Visibility.VISIBLE
                ? Visibility.INVISIBLE : Visibility.VISIBLE;
        this.visibility = visible;
    }

    public String Status() {
        return $"{this}, [size={this.size}] [visibility={this.visibility}]";
    }

    public sealed override String ToString() {
        return GetType().Name;
    }
}