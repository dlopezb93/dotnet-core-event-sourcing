using MediatR;
using System;

namespace User.Domain.SeedWork
{
    public interface IDomainEvent<TAggregateId> : INotification
    {
        /// <summary>
        /// The event identifier
        /// </summary>
        Guid EventId { get; }

        /// <summary>
        /// The identifier of the aggregate which has generated the event
        /// </summary>
        TAggregateId AggregateId { get; }

        /// <summary>
        /// The version of the aggregate when the event has been generated
        /// </summary>
        //long AggregateVersion { get; }

        /// <summary>
        /// Withes the aggregate.
        /// </summary>
        /// <param name="TAggregateId">The t aggregate identifier.</param>
        void WithAggregateId(TAggregateId aggregateId);
    }
}
