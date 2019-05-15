using System;
using Steeltoe.Management.Endpoint.Info;

namespace PalTracker
{
    public class TimeEntryInfoContributor : IInfoContributor
    {
        private readonly IOperationCounter<TimeEntry> _opCounter;
        public TimeEntryInfoContributor(IOperationCounter<TimeEntry> opCounter)
        {
            _opCounter = opCounter;
        }

        public void Contribute(IInfoBuilder builder)
        {
            builder.WithInfo(
                _opCounter.Name,
                _opCounter.GetCounts
            );
        }
    }
} 