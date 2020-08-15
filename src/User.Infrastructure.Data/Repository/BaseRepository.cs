using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using EventSourcingCQRS.Domain.EventStore;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;
using User.Domain.AggregatesModels.UserAgg.Events;
using User.Domain.SeedWork;
using User.Infrastructure.Data.Models;

namespace User.Infrastructure.Data.Repository
{
    public abstract class BaseRepository<TEntity, TId> : IRepository<TEntity>
        where TEntity : Entity<TId> ,IAggregateRoot
    {
        private readonly IDynamoDBContext _dynamoDB;
        private readonly IMediator mediator;
        private JsonSerializerSettings _jsonSettings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        };

        public BaseRepository(IDynamoDBContext dynamoDB, IMediator mediator)
        {
            _dynamoDB = dynamoDB ?? throw new ArgumentNullException(nameof(dynamoDB));
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<TEntity> GetAsyncById<TId1>(TId1 id)
        {
            var data = _dynamoDB.QueryAsync<EventStream>(id.ToString());
            var entity = Activator.CreateInstance(typeof(TEntity)) as Entity<TId>;

            while (!data.IsDone)
            {
                var results = await data.GetRemainingAsync();

                foreach (var item in results)
                {
                    if (item != null)
                    {
                        JsonSerializerSettings settings = new JsonSerializerSettings { ContractResolver = new PrivateSetterContractResolver() };
                        var domainEvent = JsonConvert.DeserializeObject(item.Payload, Type.GetType(item.EventType), settings) as IDomainEvent<TId>;

                        entity.ApplyEvent(domainEvent, item.EventNumber);
                    }
                }
            }

            return (TEntity)entity;
        }

        public async Task SaveAsync(TEntity entity)
        {
            var uncommitedEvents = entity.UncommitedEvents;

            if (uncommitedEvents.Any())
            {
                foreach (var @event in uncommitedEvents)
                {
                    var stream = new EventStream();

                    stream.EventNumber = entity.Version;
                    stream.StreamId = @event.AggregateId.ToString();
                    stream.EventType = @event.GetType().AssemblyQualifiedName;
                    stream.CreateAt = DateTime.UtcNow;
                    stream.Payload = JsonConvert.SerializeObject(@event, _jsonSettings);

                    await _dynamoDB.SaveAsync(stream);
                }

                entity.ClearUncommitedEvents();
            }
        }
    }
}
