namespace Specification.Specifications.Bases;
public interface ISpecification<in T> {
    Boolean IsSatisfiedBy(T item);
}