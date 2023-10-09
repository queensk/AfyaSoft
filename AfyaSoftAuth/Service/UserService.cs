using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AfyaSoftAuth.Data;
using AfyaSoftAuth.Models;
using AfyaSoftAuth.Models.DTO;
using AfyaSoftAuth.Models.DTO.Request;
using AfyaSoftAuth.Models.DTO.Response;
using AfyaSoftAuth.Service.IService;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AfyaSoftAuth.Service
{
    public class UserService: IUserService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJWtTokenGenerator _jwtTokenGenerator;

        private readonly IMapper _mapper;
        
        public UserService(AppDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper, IJWtTokenGenerator jWtToken)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _jwtTokenGenerator = jWtToken;
        }
        public Task<List<UserDTO>> GetAllUser()
        {
            throw new NotImplementedException();
        }

        public async Task<UserResponseDTO> GetUserById(string id)
        {
            var user =  await _userManager.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
            var userDTO = _mapper.Map<UserResponseDTO>(user);
            return userDTO;
        }

        public Task<UserDTO> GetUsersPost(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<LoginResponseDto> LoginUser(LoginRequestDto loginRequestDto)
        {
            var user = _userManager.FindByEmailAsync(loginRequestDto.Email).Result;
            var validPassword = _userManager.CheckPasswordAsync(user, loginRequestDto.Password).Result;
            if (user != null && validPassword)
            {
                var roles = _userManager.GetRolesAsync(user).Result;
                var token = _jwtTokenGenerator.GenerateToken(user, roles);
                return new LoginResponseDto()
                {
                    user = _mapper.Map<UserResponseDTO>(user),
                    Token = "Bearer " +token
                };
            }
            return new LoginResponseDto();
        }  

        public async Task<string> RegisterUser(UserDTO userDTO)
        {
            try
            {
                var user = _mapper.Map<ApplicationUser>(userDTO);
                user.UserName = userDTO.Email;
                var result = await _userManager.CreateAsync(user, userDTO.Password);
                return result.Succeeded ? "User created successfully" : result.Errors.FirstOrDefault().Description;;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }


        public async Task<string> UpdateUserRole(UpdateUserRole updateUserRole)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == updateUserRole.UserId);
                var role = await _roleManager.RoleExistsAsync(updateUserRole.Role);
                if (user != null && !role)
                {
                    // create the role
                    await _roleManager.CreateAsync(new IdentityRole(updateUserRole.Role));
                    await _userManager.AddToRoleAsync(user, updateUserRole.Role);
                    return "Role updated successfully";
                }
                else if (user != null && role)
                {
                    await _userManager.AddToRoleAsync(user, updateUserRole.Role);
                    return "Role updated successfully";
                }
                else
                {
                    return "User not found";
                }

            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}