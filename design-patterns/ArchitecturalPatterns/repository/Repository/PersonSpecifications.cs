using System.Linq.Expressions;

namespace Repository;
public static class PersonSpecifications {
    public class AgeBetweenSpec : ISpecification<Person> {
        private readonly Int32 from;
        private readonly Int32 to;

        public AgeBetweenSpec(Int32 from, Int32 to) {
            this.from = from;
            this.to = to;
        }

        public Expression<Func<Person, Boolean>> ToExpression() {
            return person => person.Age >= this.from && person.Age <= this.to;
        }

        public static implicit operator Expression<Func<Person, Boolean>>(AgeBetweenSpec spec) {
            return spec.ToExpression();
        }
    }

    public class NameEqualSpec : ISpecification<Person> {
        private readonly String name;

        public NameEqualSpec(String name) {
            this.name = name;
        }

        public Expression<Func<Person, Boolean>> ToExpression() {
            return person => person.Name == this.name;
        }
    }
}