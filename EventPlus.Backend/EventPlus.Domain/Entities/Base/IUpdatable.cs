namespace EventPlus.Domain.Entities.Base;

public interface IUpdatable
{
    DateTime? Updated { get; set; }
    long? UpdatedBy { get; set; }
}