namespace Pipeline;
public interface IHandler<in TInput, out TOutput> {
    TOutput Process(TInput input);
}