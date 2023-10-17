using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AfyaSoftAuth.Models;
using AfyaSoftAuth.Models.DTO;
using AfyaSoftAuth.Models.DTO.Response;
using AutoMapper;

namespace AfyaSoftAuth.AuthProfile
{
    public class AuthProfiles: Profile
    {
        public AuthProfiles()
        {
            // CreateMap<UserDTO, ApplicationUser>().ForMember(u => u.UserName, opt => opt.MapFrom(u => u.Email));
            CreateMap<UserDTO, ApplicationUser>().ReverseMap();
            CreateMap<ApplicationUser, UserResponseDTO>().ReverseMap();
            CreateMap<ApplicationUser, UserDTO>().ReverseMap();

        }
    }
}