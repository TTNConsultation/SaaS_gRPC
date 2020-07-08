using IdentityModel.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;

using Saas.gRPC.Test;
using Grpc.Net.Client;
using Grpc.Core;

namespace Client
{
  internal class Program
  {
    private static async Task Main()
    {
      // discover endpoints from metadata
      var client = new HttpClient();

      var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");
      if (disco.IsError)
      {
        Console.WriteLine(disco.Error);
        return;
      }

      // request token
      var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
      {
        Address = disco.TokenEndpoint,
        ClientId = "client",
        ClientSecret = "secret",

        Scope = "aaf_scope"
      }).ConfigureAwait(false);

      if (tokenResponse.IsError)
      {
        Console.WriteLine(tokenResponse.Error);
        return;
      }

      Console.WriteLine(tokenResponse.Json);
      Console.WriteLine("\n\n");

      //call gRpc
      var headers = new Metadata
      {
        { "Authorization", $"Bearer {tokenResponse.AccessToken}" }
      };
      var channel = GrpcChannel.ForAddress("https://localhost:6001");
      var gRpcClient = new TestSvc.TestSvcClient(channel);

      var res = await gRpcClient.GetAsync(new Saas.Entity.Common.MsgEmpty(), headers);

      Console.WriteLine(res);
      Console.ReadKey();
    }
  }
}