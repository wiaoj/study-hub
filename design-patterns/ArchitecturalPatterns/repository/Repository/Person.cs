namespace Repository;
public sealed class Person {
    public Guid Id { get; set; }
    public String Name { get; set; }
    public String Surname { get; set; }
    public Int32 Age { get; set; }

    public Person(String name, String surname, Int32 age) {
        this.Id = Guid.NewGuid();
        this.Name = name;
        this.Surname = surname;
        this.Age = age;
    }
}