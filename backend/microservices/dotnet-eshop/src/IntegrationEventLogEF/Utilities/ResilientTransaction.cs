namespace IntegrationEventLogEF.Utilities;
public class ResilientTransaction {
    private readonly DbContext context;
    private ResilientTransaction(DbContext context) {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public static ResilientTransaction New(DbContext context) {
        return new(context);
    }

    public async Task ExecuteAsync(Func<Task> action) {
        //Use of an EF Core resiliency strategy when using multiple DbContexts within an explicit BeginTransaction():
        //See: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency
        IExecutionStrategy strategy = this.context.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () => {
            await using IDbContextTransaction transaction = await this.context.Database.BeginTransactionAsync();
            await action();
            await transaction.CommitAsync();
        });
    }
}