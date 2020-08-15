using System;
using System.Collections.Generic;
using System.Text;

namespace User.Domain.AggregatesModels.UserAgg.Factories
{
    public class UserFactory : IUserFactory
    {
        public User Create(string name, string email, int age)
        {
            var user = new User(name, email, age);

            return user;
        }
    }
}
