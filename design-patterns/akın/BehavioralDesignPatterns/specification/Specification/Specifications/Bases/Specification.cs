namespace Specification.Specifications.Bases;
public abstract class Specification<T> : ISpecification<T> where T : class {
    public abstract Boolean IsSatisfiedBy(T item);
    public Specification<T> And(Specification<T> other) {
        ArgumentNullException.ThrowIfNull(other);
        return new AndSpecification<T>(this, other);
    }

    public Specification<T> Or(Specification<T> other) {
        ArgumentNullException.ThrowIfNull(other);
        return new OrSpecification<T>(this, other);
    }

    public Specification<T> Not() {
        return new NotSpecification<T>(this);
    }
}