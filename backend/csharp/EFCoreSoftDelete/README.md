# Soft Delete Implementation in .NET

## Interface Definition

This interface is used to mark entities that support soft deletion. The `IsDeleted` property indicates whether the entity has been marked as deleted.


```csharp
public interface ISoftDeletable {
    bool IsDeleted { get; }
}
```

## Entity Configuration
This configuration sets up a query filter to exclude soft deleted entities from the results. It also creates an index on the IsDeleted property to optimize queries that filter by this property.

```csharp
builder.HasQueryFilter(p => !p.IsDeleted);
builder.HasIndex(p => p.IsDeleted).HasFilter("is_deleted = 0");
```

## Override SaveChangesAsync or Write an Interceptor
In this method, we override the SaveChangesAsync method to handle soft deletions. Instead of deleting the entity from the database, we set the IsDeleted property to true and change the entity state to Modified. This ensures that the entity is marked as deleted without actually being removed from the database.

```csharp
public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
{
    var softDeleteEntries = ChangeTracker
        .Entries<ISoftDeletable>()
        .Where(e => e.State == EntityState.Deleted);

    foreach (var entityEntry in softDeleteEntries)
    {
        entityEntry.Property(nameof(ISoftDeletable.IsDeleted)).CurrentValue = true;
        entityEntry.State = EntityState.Modified;
    }

    return result;
}
```