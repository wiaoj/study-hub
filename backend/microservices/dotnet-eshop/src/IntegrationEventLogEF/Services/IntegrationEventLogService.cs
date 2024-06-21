namespace IntegrationEventLogEF.Services;
public class IntegrationEventLogService<TContext> : IIntegrationEventLogService, IDisposable
    where TContext : DbContext {
    private volatile Boolean disposedValue;
    private readonly TContext context;
    private readonly Type[] eventTypes;

    public IntegrationEventLogService(TContext context) {
        this.context = context;
        this.eventTypes = Assembly.Load(Assembly.GetEntryAssembly()!.FullName!)
            .GetTypes()
            .Where(x => x.Name.EndsWith(nameof(IntegrationEvent)))
            .ToArray();
    }

    public async Task<IEnumerable<IntegrationEventLogEntry>> RetrieveEventLogsPendingToPublishAsync(Guid transactionId) {
        List<IntegrationEventLogEntry> result =
            await this.context.Set<IntegrationEventLogEntry>()
                              .Where(e => e.TransactionId == transactionId && e.State == EventState.NotPublished)
                              .ToListAsync();

        if(result.Count != 0) {
            return result.OrderBy(o => o.CreationTime)
                .Select(x => x.DeserializeJsonContent(this.eventTypes.FirstOrDefault(t => t.Name == x.EventTypeShortName)));
        }

        return [];
    }

    public Task SaveEventAsync(IntegrationEvent @event, IDbContextTransaction transaction) {
        ArgumentNullException.ThrowIfNull(transaction);

        IntegrationEventLogEntry eventLogEntry = new(@event, transaction.TransactionId);

        this.context.Database.UseTransaction(transaction.GetDbTransaction());
        this.context.Set<IntegrationEventLogEntry>().Add(eventLogEntry);

        return this.context.SaveChangesAsync();
    }

    public Task MarkEventAsPublishedAsync(Guid eventId) {
        return UpdateEventStatus(eventId, EventState.Published);
    }

    public Task MarkEventAsInProgressAsync(Guid eventId) {
        return UpdateEventStatus(eventId, EventState.InProgress);
    }

    public Task MarkEventAsFailedAsync(Guid eventId) {
        return UpdateEventStatus(eventId, EventState.PublishedFailed);
    }

    private Task UpdateEventStatus(Guid eventId, EventState status) {
        IntegrationEventLogEntry eventLogEntry = this.context.Set<IntegrationEventLogEntry>().Single(ie => ie.EventId == eventId);
        eventLogEntry.State = status;

        if(status is EventState.InProgress)
            eventLogEntry.TimesSent++;

        return this.context.SaveChangesAsync();
    }

    protected virtual void Dispose(Boolean disposing) {
        if(!this.disposedValue) {
            if(disposing) {
                this.context.Dispose();
            }

            this.disposedValue = true;
        }
    }

    public void Dispose() {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}