using Microsoft.AspNetCore.Hosting;
using SupBlog.Web.Areas.Identity;

[assembly: HostingStartup(typeof(IdentityHostingStartup))]

namespace SupBlog.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => { });
        }
    }
}