using BellBankingApp.Core.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellBankingApp.Core.Application.Interfaces.Services
{
    public interface IUserManagerService
    {
        Task<GetAllUserResponse> GetAll();
        Task<GetUserResponse> GetById(string id);
        Task<GetUserResponse> GetByUserName(string userName);
        Task<CreateUserResponse> CreateUser(CreateUserRequest userRequest);
        Task<UpdateUserResponse> UpdateUser(UpdateUserRequest userRequest);
        Task<DeleteUserResponse> DeleteUser(string Id);
    }
}
