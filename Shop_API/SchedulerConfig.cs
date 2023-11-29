using Quartz;
using Quartz.Impl;
using Shop_API.Service;
using System.Threading.Tasks;

namespace Shop_API
{
    public static class SchedulerConfig
    {
        public static async Task ConfigureJobs(IScheduler scheduler)
        {
            await Task.Run(async () =>
            {
                // Tạo một công việc cập nhật trạng thái và lập lịch nó để chạy định kỳ
                IJobDetail job = JobBuilder.Create<VoucherStatusUpdateJob>().Build();
                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity("voucherStatusUpdateTrigger", "group1")
                    .StartNow()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(10) // Cập nhật mỗi 1 phút (thay đổi theo yêu cầu)
                        .RepeatForever())
                    .Build();

                await scheduler.ScheduleJob(job, trigger);
            });
        }
    }


}
