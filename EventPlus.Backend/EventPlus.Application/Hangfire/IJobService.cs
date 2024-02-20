namespace EventPlus.Application.Hangfire;

public interface IJobService
{
    Task Process();
}