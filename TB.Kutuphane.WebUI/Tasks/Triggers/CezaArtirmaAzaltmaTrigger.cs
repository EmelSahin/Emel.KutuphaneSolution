using Quartz;
using Quartz.Impl;
using TB.Kutuphane.WebUI.Tasks.Jobs;

namespace TB.Kutuphane.WebUI.Tasks.Triggers
{
    public static class CezaArtirmaAzaltmaTrigger
    {
        public static void Baslat()
        {
            IScheduler zamanlayici = StdSchedulerFactory.GetDefaultScheduler();

            if (!zamanlayici.IsStarted)
            {
                zamanlayici.Start();
            }
            IJobDetail gorev = JobBuilder.Create<CezaArtirmaAzaltmaJob>().Build();
            ICronTrigger tetikleyici = (ICronTrigger)TriggerBuilder.Create()
                .WithIdentity("CezaArtirmaAzaltmaJob", "null")
                .WithCronSchedule("0 00 22 * * ? *")//saniye dakika saat gun ay gun yil
                .Build();

            zamanlayici.ScheduleJob(gorev, tetikleyici);
        }
    }
}