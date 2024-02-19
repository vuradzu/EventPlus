using EventPlus.Application.Jobs.Abstraction;
using Hangfire;

namespace EventPlus.Application.Jobs;

public class HelloWorldEveryMinuteJob : IJob
{
    public void Run()
    {
        RecurringJob.AddOrUpdate(
            recurringJobId: "hello-world",
            methodCall: () => Console.WriteLine("Hello World"),
            cronExpression: Cron.Minutely()
        );
    }
}