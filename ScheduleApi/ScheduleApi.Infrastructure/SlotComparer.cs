using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using ScheduleApi.Infrastructure.Entitys;

namespace ScheduleApi.Infrastructure
{
    public class SlotComparer : IEqualityComparer<Slot>
    {
        public bool Equals([AllowNull] Slot x, [AllowNull] Slot y)
        {
            if (x.DateStart == y.DateStart && x.DateEnd == y.DateEnd)
            {
                return true;
            }
            return false;
        }

        public int GetHashCode([DisallowNull] Slot obj)
        {
            return obj.DateStart.GetHashCode() + obj.DateEnd.GetHashCode();
        }
    }
}
