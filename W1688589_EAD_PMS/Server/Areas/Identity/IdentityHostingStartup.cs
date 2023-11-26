using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using W1688589_EAD_PMS.Server.Data;
using W1688589_EAD_PMS.Shared.Models;

[assembly: HostingStartup(typeof(W1688589_EAD_PMS.Server.Areas.Identity.IdentityHostingStartup))]
namespace W1688589_EAD_PMS.Server.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}