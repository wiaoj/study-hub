namespace Specification.Specifications.Bases;
public class OrSpecification<T> : Specification<T> where T : class {
    private readonly Specification<T> leftSpecification;
    private readonly Specification<T> rightSpecification;
    public OrSpecification(Specification<T> leftSpecification, Specification<T> rightSpecification) {
        this.leftSpecification = leftSpecification;
        this.rightSpecification = rightSpecification;
    }

    public override Boolean IsSatisfiedBy(T item) {
        return this.leftSpecification.IsSatisfiedBy(item) || this.rightSpecification.IsSatisfiedBy(item);
    }
}