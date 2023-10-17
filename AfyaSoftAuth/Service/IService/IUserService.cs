using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AfyaSoftAuth.Models.DTO;
using AfyaSoftAuth.Models.DTO.Request;
using AfyaSoftAuth.Models.DTO.Response;

namespace AfyaSoftAuth.Service.IService
{
    public interface IUserService
    {
        // Register user
        Task<string> RegisterUser(UserDTO registerRequestDTO);

        // Login user
        Task<LoginResponseDto> LoginUser(LoginRequestDto loginRequestDto);

        // update user role
        Task<string> UpdateUserRole(UpdateUserRole updateUserRole);

        // get all user
        Task<List<UserDTO>> GetAllUser();

        // get user by id
        Task<UserResponseDTO> GetUserById(string id);

        // get a users post
        Task<UserDTO> GetUsersPost(string id);
    }
}