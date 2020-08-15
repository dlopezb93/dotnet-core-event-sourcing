using System;
using System.Collections.Generic;
using System.Text;

namespace User.Domain.AggregatesModels.UserAgg.Factories
{
    public interface IUserFactory
    {
        User Create(string name, string email, int age);
    }
}
