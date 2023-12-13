namespace EventPlus.Domain.Entities.Base;

public interface ISoftDeletable
{
    DateTime? Deleted { get; set; }
}