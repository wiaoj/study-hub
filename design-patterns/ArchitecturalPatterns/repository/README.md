---
title: Repository
category: Architectural
language: tr
tag:
 - Data access
---

## Amaç

Repository katmanı, alan ve veri eşleme katmanları arasına eklenir ve alan nesnelerini veritabanı 
erişim kodunun ayrıntılarından izole eder ve sorgu kodunun dağılmasını ve tekrarını en aza indirir. 
Repository deseni, alan sınıflarının sayısının büyük olduğu veya yoğun sorgulama yapıldığı sistemlerde 
özellikle kullanışlıdır.

## Açıklama

Gerçek dünya örneği

> Diyelim ki kişiler için kalıcı bir veri deposuna ihtiyacımız var. Yeni kişiler eklemek ve farklı 
> kriterlere göre onları aramak kolay olmalıdır.

Basit bir ifadeyle

> Repository mimari deseni, CRUD işlemleri için kullanılabilecek veri depolarının bir birim katmanı 
> oluşturur.

[Microsoft documentation](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-design) says

> Repositoryler, veri kaynaklarına erişmek için gereken mantığı kapsayan sınıflar veya bileşenlerdir. 
> Ortak veri erişim işlevselliğini merkezileştirir ve daha iyi bakım sağlar, ayrıca veritabanına erişmek 
> için kullanılan altyapı veya teknolojiyi etki alan model katmanından ayırır.

**Programatik Örnek**

Öncelikle, kalıcı hale getirmemiz gereken kişi varlığına bakalım.

```csharp
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
```

EF Core kullanarak `PersonRepository` oluşturuyoruz, bu yüzden gerçekten basit hale geliyor.

```csharp
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
```

Ayrıca, özellik sorguları için bir yardımcı sınıf olan `PersonSpecifications`'ı tanımlarız.

```csharp
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

        /*
        public static implicit operator Expression<Func<Person, Boolean>>(AgeBetweenSpec spec) {
            return spec.ToExpression();
        }
        */
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
```

Ve işte örnek olarak repository kullanımı.

```csharp
[Fact]
public async Task TestFindAll() {
    using ApplicationDbContext context = new(this.contextOptions);
    await InitializeTestData(context);

    PersonRepository repository = new(context);
    List<Person> actuals = repository.FindAll();

    Assert.Equal(4, actuals.Count);
}

[Fact]
public async Task TestAdd() {
    using ApplicationDbContext context = new(this.contextOptions);
    await InitializeTestData(context);
    PersonRepository repository = new(context);
    Person person = new("Test", "Person", 23);

    repository.Add(person);

    Person? testPerson = repository.FindByName("Test");
    Assert.NotNull(testPerson);
    Assert.Equal("Test", testPerson.Name);
    Assert.Equal("Person", testPerson.Surname);
    Assert.Equal(23, testPerson.Age);
    Assert.Equal(5, repository.Count);
}

[Fact]
public async Task TestUpdate() {
    using ApplicationDbContext context = new(this.contextOptions);
    await InitializeTestData(context);
    PersonRepository repository = new(context);
    Person terry = repository.FindByName("Terry")!;

    terry.Surname = "Lee";
    terry.Age = 47;
    repository.Update(terry);

    terry = repository.FindByName("Terry")!;
    Assert.Equal("Lee", terry.Surname);
    Assert.Equal(47, terry.Age);
}

[Fact]
public async Task TestRemove() {
    using ApplicationDbContext context = new(this.contextOptions);
    await InitializeTestData(context);
    PersonRepository repository = new(context);
    Person terry = repository.FindByName("Terry")!;

    repository.Remove(terry);

    Assert.Equal(3, repository.Count);
    Assert.Null(repository.FindByName("Terry"));
}
```

## Class diagram

"-"

## Uygulanabilirlik

Repository desenini aşağıdaki durumlarda kullanın:

* Alan nesnelerin sayısı fazladır.
* Sorgu kodunun tekrarını önlemek istersiniz.
* Veritabanı sorgulama kodunu tek bir yerde tutmak istersiniz.
* Birden fazla veri kaynağınız vardır.