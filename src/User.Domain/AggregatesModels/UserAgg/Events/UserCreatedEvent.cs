using System;
using System.Collections.Generic;
using System.Text;
using User.Domain.SeedWork;

namespace User.Domain.AggregatesModels.UserAgg.Events
{
    public class UserCreatedEvent : DomainEvent<Guid>
    {
        public UserCreatedEvent(User user)
        {
            User = user;
        }

        public User User { get; }
    }
}
