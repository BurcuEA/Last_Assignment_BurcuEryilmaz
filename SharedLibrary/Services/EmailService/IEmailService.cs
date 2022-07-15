﻿using SharedLibrary.Dtos;

namespace SharedLibrary.Services.EmailService
{
    public interface IEmailService
    {
        //Task SendEmailAsync(EmailDto request);
        Task<Response<NoDataDto>> SendEmailAsync(EmailDto request);
    }
}
