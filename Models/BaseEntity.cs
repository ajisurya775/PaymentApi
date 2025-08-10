using System;

public abstract class BaseEntity
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public BaseEntity()
    {
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    // Opsional: method untuk update UpdatedAt saat data diubah
    public void Touch()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}
