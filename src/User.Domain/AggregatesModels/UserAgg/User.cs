using System;
using User.Domain.AggregatesModels.UserAgg.Events;
using User.Domain.SeedWork;

namespace User.Domain.AggregatesModels.UserAgg
{
    public class User : Entity<Guid>, IAggregateRoot
    {
        public User()
        {
        }

        public User(string name, string email, int age)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Age = age;

            RaiseEvent(new UserCreatedEvent(this));
        }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public int Age { get; private set; }

        public void ChangeAge(int age)
        {
            if (age == 0)
                throw new Exception("Age is not valid");

            RaiseEvent(new AgeChangedEvent(age));
        }

        public void ChangeEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException(nameof(email));

            RaiseEvent(new EmailChangedEvent(email));
        }

        public void Apply(AgeChangedEvent @event)
        {
            Age = @event.Age;
        }

        public void Apply(EmailChangedEvent @event)
        {
            Email = @event.Email;
        }

        public void Apply(UserCreatedEvent @event)
        {
            this.Email = @event.User.Email;
            this.Name = @event.User.Name;
            this.Age = @event.User.Age;
            this.Id = @event.User.Id;
        }
    }
}
