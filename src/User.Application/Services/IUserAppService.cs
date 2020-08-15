using System;
using System.Threading.Tasks;
using User.Application.Dtos;

namespace User.Application.Services
{
    public interface IUserAppService
    {
        Task<UserDto> InsertAsync(UserDto user);

        Task<UserDto> GetUserById(Guid id);

        Task UpdateEmailAsync(Guid id, string email);

        Task ChangeAgeAsync(Guid id, int age);
    }
}
