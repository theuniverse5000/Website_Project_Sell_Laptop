using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;
using System;

public class JobFactory : IJobFactory
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IServiceScopeFactory _scopeFactory;

    public JobFactory(IServiceProvider serviceProvider, IServiceScopeFactory scopeFactory)
    {
        _serviceProvider = serviceProvider;
        _scopeFactory = scopeFactory;
    }

    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
        try
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var jobDetail = bundle.JobDetail;
                var jobType = jobDetail.JobType;

                // Sử dụng DI từ scope mới để tạo một instance của công việc
                var job = (IJob)scope.ServiceProvider.GetRequiredService(jobType);

                return job;
            }
        }
        catch (Exception ex)
        {
            throw new SchedulerException($"Problem while instantiating job '{bundle.JobDetail.Key}' from the JobFactory.", ex);
        }
    }

    public void ReturnJob(IJob job)
    {
        // Không cần quan tâm đến việc trả lại job vào pool trong DI
        // Đối với DI, chúng ta để DI container quản lý vòng đời của các đối tượng
    }
}
