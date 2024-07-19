using AutoMapper;
using BellBankingApp.Core.Application.DTOs.User;
using BellBankingApp.Core.Application.Interfaces.Services;
using BellBankingApp.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellBankingApp.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserManagerService _userManager;
        private readonly IMapper _mapper;

        public UserService(IUserManagerService accountService, IMapper mapper)
        {
            _userManager = accountService;
            _mapper = mapper;
        }
        public async Task<CreateUserResponse> CreateUser(SaveUserViewModel userRequest)
        {
            CreateUserRequest createUserRequest = _mapper.Map<CreateUserRequest>(userRequest);
            return await _userManager.CreateUser(createUserRequest);
        }

        public Task<DeleteUserResponse> DeleteUser(string Id)
        {
            return _userManager.DeleteUser(Id);
        }

        public async Task<List<UserViewModel>> GetAll()
        {
            var allUser = await _userManager.GetAll();
            return _mapper.Map<List<UserViewModel>>(allUser.users);
        }

        public async Task<UserViewModel> GetById(string id)
        {
            var user = await _userManager.GetById(id);
            return _mapper.Map<UserViewModel>(user);
        }

        public async Task<SaveUserViewModel> GetSaveVMById(string id)
        {
            var user = await _userManager.GetById(id);
            return _mapper.Map<SaveUserViewModel>(user);
        }

        public async Task<UpdateUserViewModel> GetUpdateVMById(string id)
        {
            var user = await _userManager.GetById(id);
            return _mapper.Map<UpdateUserViewModel>(user);
        }

        public async Task<UserViewModel> GetByUserName(string userName)
        {
            var user = await _userManager.GetByUserName(userName);
            return _mapper.Map<UserViewModel>(user);
        }

        public async Task<UpdateUserResponse> UpdateUser(UpdateUserViewModel userRequest)
        {
            UpdateUserRequest updateUserRequest = _mapper.Map<UpdateUserRequest>(userRequest);
            return await _userManager.UpdateUser(updateUserRequest);
        }

        public async Task UpdateUserStatus(UserViewModel user)
        {
            var updateUserRequest = _mapper.Map<UpdateUserRequest>(user);
            await _userManager.UpdateUser(updateUserRequest);
        }
    }
}
