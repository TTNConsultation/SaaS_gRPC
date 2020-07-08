using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCClient.Models;
using Newtonsoft.Json.Linq;
using Saas.gRPC.Test;

namespace MVCClient.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
      _logger = logger;
    }

    public IActionResult Index()
    {
      return View();
    }

    public IActionResult Privacy()
    {
      return View();
    }

    public IActionResult Logout()
    {
      return SignOut("Cookies", "oidc");
    }

    public async Task<IActionResult> CallApi()
    {
      var accessToken = await HttpContext.GetTokenAsync("access_token").ConfigureAwait(false);

      var headers = new Metadata
      {
        { "Authorization", $"Bearer {accessToken}" }
      };
      var channel = GrpcChannel.ForAddress("https://localhost:6001");
      var gRpcClient = new TestSvc.TestSvcClient(channel);

      var res = await gRpcClient.GetAsync(new Saas.Entity.Common.MsgEmpty(), headers);

      ViewBag.Json = res;
      return View("json");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}