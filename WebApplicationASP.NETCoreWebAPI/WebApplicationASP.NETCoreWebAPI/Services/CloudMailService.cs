using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationASP.NETCoreWebAPI.Services
{
    public class CloudMailService : IMailService
    {
        private readonly IConfiguration _configuration;

        public CloudMailService(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public void Send(string subject, string message)
        {
            // send mail - output to debug window
            Debug.WriteLine($"Mail From {_configuration["mailSettings:mailFromAdress"]} to {_configuration["mailSettings:mailToAddress"]}, with CloudMailService.");
            Debug.WriteLine($"Subject: {subject}");
            Debug.WriteLine($"Message: {message}");
        }
    }
}