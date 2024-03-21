using System.Linq.Expressions;

namespace Repository;
public interface IPersonRepository {
    public void Add(Person person);
    public void Remove(Person person);
    public void Update(Person person);
    public Person? FindByName(String name);
    public Person? Find(Expression<Func<Person, Boolean>> expression);
    public List<Person> FindAll();
    public Int64 Count { get; }
}