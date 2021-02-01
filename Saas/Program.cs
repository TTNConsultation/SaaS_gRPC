using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Security.Authentication;

namespace Shared
{
  public class Program
  {
    public static readonly string SocketPath = Path.Combine(Path.GetTempPath(), "socket.tmp");

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

              //if (File.Exists(SocketPath))
              //  File.Delete(SocketPath);
              //kestrelOptions.ListenUnixSocket(SocketPath);
            });
            webBuilder.UseStartup<Startup>();
          });
  }
}