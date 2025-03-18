namespace Specification.Specifications.Bases;
public class NotSpecification<T> : Specification<T> where T : class {
    private readonly Specification<T> specification;
    public NotSpecification(Specification<T> specification) {
        this.specification = specification;
    }

    public override Boolean IsSatisfiedBy(T item) {
        return !this.specification.IsSatisfiedBy(item);
    }
}