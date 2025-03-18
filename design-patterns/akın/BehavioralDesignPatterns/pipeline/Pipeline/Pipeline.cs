namespace Pipeline;
public sealed class Pipeline<TInput, TOutput> {
    private readonly Func<TInput, TOutput> currentHandler;

    public Pipeline(Func<TInput, TOutput> currentHandler) {
        this.currentHandler = currentHandler;
    }

    public Pipeline(IHandler<TInput, TOutput> currentHandler) : this(x => currentHandler.Process(x)) { }

    public Pipeline<TInput, TNextOutput> Then<TNextOutput>(Func<TOutput, TNextOutput> nextHandler) {
        Func<TInput, TNextOutput> combinedHandler = input => nextHandler(this.currentHandler(input));
        return new Pipeline<TInput, TNextOutput>(combinedHandler);
    }

    public Pipeline<TInput, TNextOutput> Then<TNextOutput>(IHandler<TOutput, TNextOutput> nextHandler) {
        Func<TInput, TNextOutput> combinedHandler = input => nextHandler.Process(this.currentHandler(input));
        return new Pipeline<TInput, TNextOutput>(combinedHandler);
    }

    public TOutput Execute(TInput input) {
        return this.currentHandler(input);
    }
}