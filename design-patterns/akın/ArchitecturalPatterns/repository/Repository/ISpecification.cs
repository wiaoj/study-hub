using System.Linq.Expressions;

namespace Repository;

public interface ISpecification<T> {
    Expression<Func<T, Boolean>> ToExpression();
}
