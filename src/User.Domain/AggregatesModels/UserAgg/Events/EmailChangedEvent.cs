using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using User.Domain.SeedWork;

namespace User.Domain.AggregatesModels.UserAgg.Events
{
    public class EmailChangedEvent : DomainEvent<Guid>
    {
        public EmailChangedEvent(string email)
        {
            Email = email;
        }

        public string Email { get; }
    }
}
