using System.Linq.Expressions;

namespace Repository;
public sealed class PersonRepository : IPersonRepository {
    private readonly ApplicationDbContext context;

    public PersonRepository(ApplicationDbContext context) {
        this.context = context;
    }

    public Int64 Count => this.context.People.LongCount();

    public void Add(Person person) {
        this.context.People.Add(person);
        this.context.SaveChanges();
    }

    public Person? Find(Expression<Func<Person, Boolean>> expression) {
        return this.context.People.FirstOrDefault(expression);
    }

    public List<Person> FindAll() {
        return this.context.People.ToList();
    }

    public List<Person> FindAll(Expression<Func<Person, Boolean>> expression) {
        return this.context.People.Where(expression).ToList();
    }

    public Person? FindByName(String name) {
        return this.context.People.FirstOrDefault(x => x.Name == name);
    }

    public void Remove(Person person) {
        context.People.Remove(person);
        context.SaveChanges();
    }

    public void Update(Person person) {
        context.People.Update(person);
        context.SaveChanges();
    }
}