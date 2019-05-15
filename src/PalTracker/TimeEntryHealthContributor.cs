using System;
using System.Linq;
using Steeltoe.Common.HealthChecks;
using static Steeltoe.Common.HealthChecks.HealthStatus;

namespace PalTracker
{
    public class TimeEntryHealthContributor : IHealthContributor
    {
        private ITimeEntryRepository _timeEntryRepo;
        public const int MaxTimeEntries = 5;
         public TimeEntryHealthContributor(ITimeEntryRepository timeEntryRepo)
         {
            _timeEntryRepo = timeEntryRepo;
         }
        public string Id {get;} = "timeEntry";
       
        public HealthCheckResult Health()
        {
            var timeEntryCount = _timeEntryRepo.List().Count();
            string heathstatus;

            HealthCheckResult health;

            if(timeEntryCount >= MaxTimeEntries) {
                health = new HealthCheckResult { Status = DOWN };
                heathstatus = "DOWN";
            } else {
                health = new HealthCheckResult { Status = UP };
                heathstatus = "UP";
            }

            health.Details.Add("status", heathstatus);
            // health.Details.Add("Status", "DOWN");
            health.Details.Add("count", timeEntryCount);
            health.Details.Add("threshold", MaxTimeEntries);
            

            return health;
        }

    }
}