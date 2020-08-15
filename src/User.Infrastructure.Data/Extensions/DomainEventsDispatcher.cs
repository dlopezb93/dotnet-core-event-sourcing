using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Domain.SeedWork;

namespace Eccco.Data.Extensions
{
    public static class DomainEventsDispatcher
    {
        public static Task DispatchDomainEventsAsync<T>(this IMediator mediator, T ctx)
        {
            return Task.CompletedTask;
        }        
    }
}
