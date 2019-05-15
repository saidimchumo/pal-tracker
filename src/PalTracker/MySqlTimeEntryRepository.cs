using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PalTracker
{
    public class MySqlTimeEntryRepository : ITimeEntryRepository
    {
        private TimeEntryContext timeEntryContext;
        public MySqlTimeEntryRepository(TimeEntryContext _timeEntryContext)
        {
            timeEntryContext = _timeEntryContext;
        }
        public bool Contains(long id)
        {
            return timeEntryContext.TimeEntryRecords.AsNoTracking().Any(t => t.Id == id);   
        }

        public TimeEntry Create(TimeEntry timeEntry)
        {
            TimeEntryRecord recordToCreated = timeEntry.ToRecord();
            timeEntryContext.TimeEntryRecords.Add(recordToCreated);
            timeEntryContext.SaveChanges();
            
            return Find(recordToCreated.Id.Value);
        }

        public void Delete(long id)
        {
            timeEntryContext.TimeEntryRecords.Remove(FindByRecord(id));
            timeEntryContext.SaveChanges();            
        }

        private TimeEntryRecord FindByRecord(long id)
        {
            return timeEntryContext.TimeEntryRecords.AsNoTracking().Single(t => t.Id == id);
        }

        public TimeEntry Find(long id) 
        {
            return FindByRecord(id).ToEntity();
        }
        public IEnumerable<TimeEntry> List()
        {            
            var timeEntryRecordList = timeEntryContext.TimeEntryRecords.AsNoTracking().Select(t => t.ToEntity());

            return timeEntryRecordList;
        }

        public TimeEntry Update(long id, TimeEntry timeEntry)
        {
            TimeEntryRecord recordToCreated = timeEntry.ToRecord();
            recordToCreated.Id = id;

            timeEntryContext.TimeEntryRecords.Update(recordToCreated);
            timeEntryContext.SaveChanges();

            return Find(recordToCreated.Id.Value);
        }
    }
}