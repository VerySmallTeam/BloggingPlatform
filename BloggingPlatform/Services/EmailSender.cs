using BloggingPlatform.Interfaces;
using BloggingPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggingPlatform.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(Message message)
        {
            throw new NotImplementedException();
        }
    }
}
