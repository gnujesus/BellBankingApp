using AutoMapper;
using Azure.Core;
using BellBankingApp.Core.Application.DTOs.Account;
using BellBankingApp.Core.Application.DTOs.User;
using BellBankingApp.Core.Application.Enums;
using BellBankingApp.Core.Application.Interfaces.Services;
using BellBankingApp.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellBankingApp.Infrastructure.Identity.Services
{
    public class UserManagerService : IUserManagerService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public UserManagerService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }
        public async Task<CreateUserResponse> CreateUser(CreateUserRequest userRequest)
        {
            CreateUserResponse response = new()
            {
                HasError = false
            };

            var userWithSameUserName = await _userManager.FindByNameAsync(userRequest.UserName);
            if (userWithSameUserName != null)
            {
                response.HasError = true;
                response.Error = $"username '{userRequest.UserName}' is already taken.";
                return response;
            }

            var userWithSameEmail = await _userManager.FindByEmailAsync(userRequest.Email);
            if (userWithSameEmail != null)
            {
                response.HasError = true;
                response.Error = $"Email '{userRequest.Email}' is already registered.";
                return response;
            }

            var user = _mapper.Map<ApplicationUser>(userRequest);

            var result = await _userManager.CreateAsync(user, userRequest.Password);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"An error occurred trying to register the user.";
                return response;

            }

            await _userManager.AddToRoleAsync(user, userRequest.Rol.ToString());

            return response;
        }

        public async Task<DeleteUserResponse> DeleteUser(string Id)
        {
            DeleteUserResponse userDeleteResponse = new();

            var userToDelete = await _userManager.FindByIdAsync(Id);

            if (userToDelete == null)
            {
                userDeleteResponse.HasError = true;
                userDeleteResponse.Error = "No user found with this Id";
                return userDeleteResponse;
            }

            var result = await _userManager.DeleteAsync(userToDelete);

            if (!result.Succeeded)
            {
                userDeleteResponse.HasError = true;
                userDeleteResponse.Error = result.Errors.First().ToString();
                return userDeleteResponse;
            }

            return userDeleteResponse;
        }

        public async Task<GetAllUserResponse> GetAll()
        {
            GetAllUserResponse allUsers = new();

            var userlist = _userManager.Users.ToList();

            allUsers.users = _mapper.Map<List<GetUserResponse>>(userlist);

            allUsers.users = userlist.Select(user => new GetUserResponse() 
            {
                Id = user.Id,
                IsActive = user.IsActive,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                NationalId = user.NationalId,
                UserName = user.UserName
            }).ToList();


            return allUsers;
        }

        public async Task<GetUserResponse> GetById(string id)
        {
            GetUserResponse getUser = new();

            var user = await _userManager.FindByIdAsync(id);

            if(user == null) 
            {
                getUser.HasError = true;
                getUser.Error = "No user found with this Id";
                return getUser;
            }

            //getUser = _mapper.Map<GetUserResponse>(user);

            getUser = new()
            {
                Id = user.Id,
                IsActive = user.IsActive,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                NationalId = user.NationalId,
                UserName = user.UserName
            };

            IList<string> roles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            getUser.Rol = roles.First();

            return getUser;
        }

        public async Task<GetUserResponse> GetByUserName(string userName)
        {
            GetUserResponse getUser = new();

            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                getUser.HasError = true;
                getUser.Error = "No user found with this Username";
                return getUser;
            }

            getUser = _mapper.Map<GetUserResponse>(user);

            IList<string> roles = await _userManager.GetRolesAsync(user);
            getUser.Rol = roles.First();

            return getUser;
        }

        public async Task<UpdateUserResponse> UpdateUser(UpdateUserRequest userRequest)
        {
            UpdateUserResponse userDeleteResponse = new();

            var userToUpdate = _mapper.Map<ApplicationUser>(userRequest);

            var result = await _userManager.UpdateAsync(userToUpdate);

            if (!result.Succeeded)
            {
                userDeleteResponse.HasError = true;
                userDeleteResponse.Error = result.Errors.First().ToString();
                return userDeleteResponse;
            }

            return userDeleteResponse;
        }
    }
}
