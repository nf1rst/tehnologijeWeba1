using AutoMapper;
using EncyclopaediaBisemutum.Application.DTOs.User;
using EncyclopaediaBisemutum.Application.Interfaces;
using Exceptions;
using HeavyArchives.Application.DTOs.Auth;
using HeavyArchives.Application.Interfaces;
using HeavyArchives.Domain.Entities;
using HeavyArchives.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncyclopaediaBisemutum.Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly IPasswordHasher passwordHasher;

        public AdminService(IUnitOfWork uow, IMapper mapper, IPasswordHasher passwordHasher)
        {
            this.passwordHasher= passwordHasher;
            this.uow = uow;
            this.mapper = mapper;
        }
        public async Task<UserWithDetailsDTO?> AddAdminUser(LoginDTO adminUserDTO)
        {
            User? user = await uow.Users.GetUserWithDetailsByUsernameAsync(adminUserDTO.Username);
            if (user is not null) throw new UserAlreadyExistsException($"User with username [{adminUserDTO.Username}] already exists");

            var(hash, salt)= passwordHasher.HashPassword(adminUserDTO.Password);

            user = new User()
            {
                Username = adminUserDTO.Username,
                PasswordHash=hash,
                PasswordSalt=salt,
                RoleID=1, //admin
                CreatedAt= DateTime.UtcNow,
            };

            await uow.Users.InsertAsync(user);
            await uow.SaveChangesAsync();
            return mapper.Map<UserWithDetailsDTO>(user);
        }

        public async Task DeleteUser(int id)
        {
            User? user = await uow.Users.GetByIdAsync(id);
            if (user is null) throw new EntityNotExistant($"User with id [{id}] does not exist");

            await uow.Users.DeleteAsync(user);
            await uow.SaveChangesAsync();
        }

        public async Task<ICollection<UserDTO>> GetAllUsersAsync()
        {
            ICollection<User> users = await uow.Users.GetAllUsersWithDetailsAsync();
            return mapper.Map<ICollection<UserDTO>>(users);
        }

        public async Task<UserWithDetailsDTO?> GetUserByIdAsync(int id)
        {
            User? user = await uow.Users.GetUserWithDetailsByIdAsync(id);
            if (user is null) throw new EntityNotExistant($"User with id [{id}] does not exist");
            return mapper.Map<UserWithDetailsDTO>(user);
        }

        public async Task<UserWithDetailsDTO?> GetUserByUsernameAsync(string username)
        {
            User? user = await uow.Users.GetUserWithDetailsByUsernameAsync(username);
            if (user is null) throw new EntityNotExistant($"User with username [{username}] does not exist");
            return mapper.Map<UserWithDetailsDTO>(user);
        }
    }
}
