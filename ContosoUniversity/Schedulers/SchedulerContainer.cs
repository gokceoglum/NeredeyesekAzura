using Quartz;
using Quartz.Impl;
using System;


namespace ContosoUniversity.Schedulers
{
    public class SchedulerContainer
    {
        public void RunJob()
        {
            try
            {
                ISchedulerFactory schedFact = new StdSchedulerFactory();
                IScheduler sched = schedFact.GetScheduler();
                if (!sched.IsStarted)
                    sched.Start();
                

                IJobDetail jobMonthly = JobBuilder.Create<MonthlyScheduler>().WithIdentity("MonthlyScheduler", null).Build();
                //ISimpleTrigger triggerMonthly = (ISimpleTrigger)TriggerBuilder.Create().WithIdentity("MonthlyScheduler").StartAt(DateTime.Now.AddMonths(2)).WithSimpleSchedule(x => x.WithIntervalInSeconds(100).RepeatForever()).Build();
                ISimpleTrigger triggerMonthly = (ISimpleTrigger)TriggerBuilder.Create().WithIdentity("MonthlyScheduler").WithSchedule(CronScheduleBuilder.MonthlyOnDayAndHourAndMinute(1, 1, 0)).Build();
                sched.ScheduleJob(jobMonthly, triggerMonthly);


                IJobDetail jobDaily = JobBuilder.Create<DailyScheduler>().WithIdentity("DailyScheduler", null).Build();
                ISimpleTrigger triggerDaily = (ISimpleTrigger)TriggerBuilder.Create().WithIdentity("DailyScheduler").WithCronSchedule("0 0 11 1,2,3,4,5,8,9,10,11,12,15,16,17,18,19,22,23,24,25,26,27 * ?").Build();
                sched.ScheduleJob(jobDaily, triggerDaily);
            }
            catch (Exception ex)
            {
            }
        }
    }
}