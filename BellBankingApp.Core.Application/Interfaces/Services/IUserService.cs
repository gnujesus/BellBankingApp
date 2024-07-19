﻿using BellBankingApp.Core.Application.DTOs.User;
using BellBankingApp.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellBankingApp.Core.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<List<UserViewModel>> GetAll();
        Task<UserViewModel> GetById(string id);
        Task<SaveUserViewModel> GetSaveVMById(string id);
        Task<UpdateUserViewModel> GetUpdateVMById(string id);
        Task<UserViewModel> GetByUserName(string userName);
        Task<CreateUserResponse> CreateUser(SaveUserViewModel userRequest);
        Task<UpdateUserResponse> UpdateUser(UpdateUserViewModel userRequest);
        Task<DeleteUserResponse> DeleteUser(string Id);
        Task UpdateUserStatus(UserViewModel user);
    }
}
