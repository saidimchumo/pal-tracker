using System;
using System.Collections.Generic;

namespace PalTracker
{
    public interface IOperationCounter<T>
    {
        void Increment(TrackedOperation trackedOperation);
        IDictionary<TrackedOperation, int> GetCounts{get;}
        string Name {get;}
    }
}