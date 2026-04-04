namespace RentCarServer.Domain.Abstractions;

public abstract class Entity
{

    protected Entity()
    {
        Id = new IdentityId(Guid.CreateVersion7());//v7 sirali guid olusturur, bu sayede veritabanında performans artışı sağlar
        IsActive = true;
    }
    public IdentityId Id { get; private set; } //value-object pattern uygulamak için struct yerine record struct kullanıldı,DDD
    public bool IsActive { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; } //DateTimeOffSet veri tabanına saat farkını işler
    public IdentityId CreatedBy { get; private set; } = default!; //kayit esnasında biz vericez
    public DateTimeOffset? UpdatedAt { get; private set; }
    public IdentityId? UpdatedBy { get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTimeOffset? DeletedAt { get; private set; }
    public IdentityId? DeletedBy { get; private set; }

    public void SetStatus(bool isActive)
    {
        IsActive = isActive;
    }

    public void Delete()
    {
        IsDeleted = true;
    }
}

public sealed record IdentityId(Guid Value)
{
    public static implicit operator Guid(IdentityId id) => id.Value;
    public static implicit operator string(IdentityId id) => id.Value.ToString();
}
