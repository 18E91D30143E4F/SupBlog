using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(SupBlog.Web.Areas.Identity.IdentityHostingStartup))]
namespace SupBlog.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}