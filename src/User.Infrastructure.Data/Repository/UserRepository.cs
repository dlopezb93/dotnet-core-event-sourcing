using Amazon.DynamoDBv2.DataModel;
using MediatR;
using System;
using User.Domain.AggregatesModels.UserAgg;

namespace User.Infrastructure.Data.Repository
{
    public class UserRepository : BaseRepository<Domain.AggregatesModels.UserAgg.User, Guid>,
                                  IUserRepository
    {
        public UserRepository(IDynamoDBContext dynamoDB, IMediator mediator) : base(dynamoDB, mediator)
        {
        }
    }
}
