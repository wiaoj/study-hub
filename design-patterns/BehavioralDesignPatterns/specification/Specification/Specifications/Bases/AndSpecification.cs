namespace Specification.Specifications.Bases;
public class AndSpecification<T> : Specification<T> where T : class {
    private readonly Specification<T> leftSpecification;
    private readonly Specification<T> rightSpecification;
    public AndSpecification(Specification<T> leftSpecification, Specification<T> rightSpecification) {
        this.leftSpecification = leftSpecification;
        this.rightSpecification = rightSpecification;
    }

    public override bool IsSatisfiedBy(T item) {
        return leftSpecification.IsSatisfiedBy(item) && rightSpecification.IsSatisfiedBy(item);
    }
}