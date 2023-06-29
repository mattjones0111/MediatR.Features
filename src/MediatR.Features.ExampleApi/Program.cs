using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace MediatR.Features.ExampleApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(c =>
                {
                    c.UseStartup<Startup>();
                })
                .Build()
                .Run();
        }
    }
}
