using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Repository
{
    /// <summary>
    /// Framework Database context.
    /// </summary>
    public class FWDbContext : DbContext
    {
        // Configuring migrations
        // https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/powershell
        
        /// <summary>
        /// Gets the application connectionstring.json configuration.
        /// </summary>
        protected static IConfiguration Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    lock (_lock)
                    {
                        if (_configuration == null)
                        {
                            var builder = new ConfigurationBuilder();
                            builder.AddJsonFile("connectionstrings.json");
                            _configuration = builder.Build();
                        }
                    }
                }
                return _configuration;
            }
        }
        
        private static IConfiguration _configuration;
        private static object _lock = new object();
    }
}
