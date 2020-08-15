using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace User.Domain.SeedWork
{
    public interface IRepository<TEntity>
        where TEntity : IAggregateRoot
    {
        Task SaveAsync(TEntity entity);

        Task<TEntity> GetAsyncById<TId>(TId id);
    }
}
