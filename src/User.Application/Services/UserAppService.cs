using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using User.Application.Dtos;
using User.Domain.AggregatesModels.UserAgg;
using User.Domain.AggregatesModels.UserAgg.Factories;

namespace User.Application.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserFactory _userFactory;

        public UserAppService(
                IUserRepository userRepository,
                IUserFactory userFactory)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _userFactory = userFactory ?? throw new ArgumentNullException(nameof(userFactory));
        }

        public async Task<UserDto> GetUserById(Guid id)
        {
            var user = await _userRepository.GetAsyncById(id);

            // Better with AutoMapper
            return MapToUserDto(user);
        }

        public async Task<UserDto> InsertAsync(UserDto userDto)
        {
            var user = _userFactory.Create(userDto.Name, userDto.Email, userDto.Age);

            await _userRepository.SaveAsync(user);

            // Better with AutoMapper
            return MapToUserDto(user);
        }

        public async Task UpdateEmailAsync(Guid id, string email)
        {
            var user = await _userRepository.GetAsyncById(id);

            if (user == null) throw new KeyNotFoundException($"User does not exist.");

            user.ChangeEmail(email);

            await _userRepository.SaveAsync(user);
        }

        public async Task ChangeAgeAsync(Guid id, int age)
        {
            var user = await _userRepository.GetAsyncById(id);

            if (user == null) throw new KeyNotFoundException($"User does not exist.");

            user.ChangeAge(age);

            await _userRepository.SaveAsync(user);
        }

        private static UserDto MapToUserDto(Domain.AggregatesModels.UserAgg.User user)
        {
            return new UserDto()
            {
                Id = user.Id,
                Age = user.Age,
                Email = user.Email,
                Name = user.Name,
            };
        }
    }
}
