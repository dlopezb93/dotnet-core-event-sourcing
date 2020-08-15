using System;
using System.Collections.Generic;
using System.Text;
using User.Domain.SeedWork;

namespace User.Domain.AggregatesModels.UserAgg
{
    public interface IUserRepository : IRepository<User>
    {
    }
}
