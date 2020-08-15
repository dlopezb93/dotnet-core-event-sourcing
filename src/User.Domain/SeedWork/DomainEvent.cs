using System;
using System.Collections.Generic;
using System.Text;

namespace User.Domain.SeedWork
{
    public class DomainEvent<TAggregateId> : IDomainEvent<TAggregateId>
    {
        public DomainEvent()
        {
            EventId = Guid.NewGuid();
        }

        public Guid EventId { get; private set; }

        public TAggregateId AggregateId { get; private set; }

        public void WithAggregateId(TAggregateId aggregateId)
        {
            AggregateId = aggregateId;
        }
    }
}
