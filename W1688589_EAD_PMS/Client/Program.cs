using W1688589_EAD_PMS.Client.Custom;
using W1688589_EAD_PMS.Client.Services;
using W1688589_EAD_PMS.Client.Services.Workspaces;
using W1688589_EAD_PMS.Client.Services.Members;
using W1688589_EAD_PMS.Client.Services.Projects;
using W1688589_EAD_PMS.Client.Services.Tickets;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace W1688589_EAD_PMS.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddHttpClient("W1688589_EAD_PMS.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("W1688589_EAD_PMS.ServerAPI"));


            //register the factory
            builder.Services.AddApiAuthorization()
    .AddAccountClaimsPrincipalFactory<CustomUserFactory>();

            //This method sets up the services required by the app to interact with the existing authorization system.
            builder.Services.AddApiAuthorization();
            // registering the services
            builder.Services.AddScoped<IWorkspaceService, WorkspaceService>();
            builder.Services.AddScoped<IMyService, MyService>();
            builder.Services.AddScoped<IUpdateLayoutService, UpdateLayoutService>();
            builder.Services.AddScoped<IProjectService, ProjectService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ITicketService, TicketService>();

            await builder.Build().RunAsync();
        }
    }
}
