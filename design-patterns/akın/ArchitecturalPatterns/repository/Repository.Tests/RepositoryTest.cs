using Microsoft.EntityFrameworkCore;

namespace Repository.Tests;
public class RepositoryTest {
    private readonly DbContextOptions<ApplicationDbContext> contextOptions;

    public RepositoryTest() {
        this.contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using ApplicationDbContext context = new(this.contextOptions);
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
    }

    private List<Person> People => [
        new("Peter", "Sagan", 17),
        new("Nasta", "Kuzminova", 25),
        new("John", "Lawrence", 35),
        new("Terry", "Law", 36)
    ];

    private async Task InitializeTestData(ApplicationDbContext context) {
        await context.People.AddRangeAsync(this.People);
        await context.SaveChangesAsync();
    }

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

    [Fact]
    public async Task TestFindOneByNameEqualSpec() {
        using ApplicationDbContext context = new(this.contextOptions);
        await InitializeTestData(context);
        PersonRepository repository = new(context);

        Person? terry = repository.Find(new PersonSpecifications.NameEqualSpec("Terry").ToExpression());
        Person expected = context.People.First(x => x.Name == "Terry");
        Assert.NotNull(terry);
        Assert.Equal(expected, terry);
    }

    [Fact]
    public async Task TestFindAllByAgeBetweenSpec() {
        using ApplicationDbContext context = new(this.contextOptions);
        await InitializeTestData(context);
        PersonRepository repository = new(context);

        List<Person> persons = repository.FindAll(new PersonSpecifications.AgeBetweenSpec(20, 40).ToExpression());

        Assert.Equal(3, persons.Count);
        Assert.All(persons, item => Assert.True(item.Age is > 20 and < 40));
    }
}