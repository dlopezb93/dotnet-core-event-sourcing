using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace User.Domain.SeedWork
{
    public abstract class Entity<TId>
    {
        private ICollection<IDomainEvent<TId>> _uncommittedEvents;
        private long _version;

        public Entity()
        {
            _uncommittedEvents = new Collection<IDomainEvent<TId>>();
        }

        public TId Id { get; set; }

        public long Version => _version;

        public IEnumerable<IDomainEvent<TId>> UncommitedEvents => _uncommittedEvents.AsEnumerable();

        protected void RaiseEvent(IDomainEvent<TId> domainEvent)
        {
            domainEvent.WithAggregateId(this.Id);
            ApplyEvent(domainEvent, _version + 1);

            _uncommittedEvents.Add(domainEvent);
        }

        public void ApplyEvent(IDomainEvent<TId> @event, long version)
        {
            if (!_uncommittedEvents.Any(x => Equals(x.EventId, @event.EventId)))
            {
                ((dynamic)this).Apply((dynamic)@event);
                _version = version;
            }
        }

        public void ClearUncommitedEvents()
        {
            _uncommittedEvents.Clear();
        }
    }
}
