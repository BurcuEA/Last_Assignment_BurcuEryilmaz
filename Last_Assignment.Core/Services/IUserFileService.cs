﻿using Last_Assignment.Core.DTOs;
using Last_Assignment.Core.Models;
using SharedLibrary.Dtos;

namespace Last_Assignment.Core.Services
{
    public interface IUserFileService
    {
        Task<UserFile> CreateUserFileAsync(string userId);
        Task<Response<UserFileDto>> GetFilesAsync(string userName);
    }
}