using System;
using System.Collections.Generic;
using System.Text;
using User.Domain.SeedWork;

namespace User.Domain.AggregatesModels.UserAgg.Events
{
    public class AgeChangedEvent : DomainEvent<Guid>
    {
        public AgeChangedEvent(int age)
        {
            Age = age;
        }

        public int Age { get; }
    }
}
