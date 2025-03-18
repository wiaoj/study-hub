namespace Iterator;
public interface IIterator<out T> {
    Boolean HasNext();
    T? Next();
}