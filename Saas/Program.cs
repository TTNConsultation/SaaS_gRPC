using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.Extensions.Hosting;
using System.Security.Authentication;

namespace Saas
{
  public class Program
  {
    public static void Main(string[] args)
    {
      CreateHostBuilder(args).Build().Run();
    }

    // Additional configuration is required to successfully run gRPC on macOS.
    // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
    public static IHostBuilder CreateHostBuilder(string[] args) =>
      Host.CreateDefaultBuilder(args)
          .UseWindowsService()
          .ConfigureWebHostDefaults(webBuilder =>
          {
            webBuilder.ConfigureKestrel(kestrelOptions =>
            {
              kestrelOptions.ConfigureEndpointDefaults(listenOptions => listenOptions.Protocols = HttpProtocols.Http2);
              kestrelOptions.ConfigureHttpsDefaults(httpsOptions =>
              {
                httpsOptions.ClientCertificateMode = ClientCertificateMode.AllowCertificate;
                httpsOptions.SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls13;
              });
            });
            webBuilder.UseStartup<Startup>();
          });
  }
}