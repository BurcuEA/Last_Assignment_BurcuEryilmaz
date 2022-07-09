﻿using Last_Assignment.Core.DTOs;
using Last_Assignment.Core.Models;
using Last_Assignment.Core.Services;
using Microsoft.AspNetCore.Identity;
using SharedLibrary.Dtos;

namespace Last_Assignment.Service.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserApp> _userManager;

        public UserService(UserManager<UserApp> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Response<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto)
        {

            // PasswordHash alanı var ... // 37. video UserService 2:27 dk sn ... 
            //var user = new UserApp { Email = createUserDto.Email, UserName = createUserDto.UserName,PasswordHash };
            var user = new UserApp { Email = createUserDto.Email, UserName = createUserDto.UserName,City=createUserDto.City }; // 43. video Postman Endpoint  işlemi kısmında hata verdiği için City=createUserDto.City ekledik 

            var result = await _userManager.CreateAsync(user, createUserDto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList(); // aynı username alınmış olabilir mesela ...

                return Response<UserAppDto>.Fail(new ErrorDto(errors, true), 400);
            }

            return Response<UserAppDto>.Success(ObjectMapper.Mapper.Map<UserAppDto>(user), 200);
        }

        public async Task<Response<UserAppDto>> GetUserByNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return Response<UserAppDto>.Fail("UserName not found", 404, true);
            }

            return Response<UserAppDto>.Success(ObjectMapper.Mapper.Map<UserAppDto>(user), 200);
        }
    }
}
