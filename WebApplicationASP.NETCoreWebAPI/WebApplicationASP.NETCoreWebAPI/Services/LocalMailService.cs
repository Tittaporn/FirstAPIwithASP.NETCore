using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationASP.NETCoreWebAPI.Services
{
    public class LocalMailService : IMailService
    {
        private readonly IConfiguration _configuration;
        // Do Not need this 2 lines because data is from Configuration
        //  private string _mailTo = "admin@mycompany.com";
        //  private string _mailFrom = "noreply@company.com";
        public LocalMailService(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public void Send(string subject, string message)
        {
            // send mail - output to debug window
            Debug.WriteLine($"Mail From {_configuration["mailSettings:mailFromAdress"]} to {_configuration["mailSettings:mailToAddress"]}, with LocalMailService.");
            Debug.WriteLine($"Subject: {subject}");
            Debug.WriteLine($"Message: {message}");
        }
    }
}
