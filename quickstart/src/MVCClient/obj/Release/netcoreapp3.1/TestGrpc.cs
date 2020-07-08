// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Protos/test.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace Saas.gRPC.Test {
  public static partial class TestSvc
  {
    static readonly string __ServiceName = "Services.TestSvc";

    static readonly grpc::Marshaller<global::Saas.Entity.Common.MsgEmpty> __Marshaller_Common_MsgEmpty = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Saas.Entity.Common.MsgEmpty.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Saas.Entity.Common.MsgString> __Marshaller_Common_MsgString = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Saas.Entity.Common.MsgString.Parser.ParseFrom);

    static readonly grpc::Method<global::Saas.Entity.Common.MsgEmpty, global::Saas.Entity.Common.MsgString> __Method_Get = new grpc::Method<global::Saas.Entity.Common.MsgEmpty, global::Saas.Entity.Common.MsgString>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Get",
        __Marshaller_Common_MsgEmpty,
        __Marshaller_Common_MsgString);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::Saas.gRPC.Test.TestReflection.Descriptor.Services[0]; }
    }

    /// <summary>Client for TestSvc</summary>
    public partial class TestSvcClient : grpc::ClientBase<TestSvcClient>
    {
      /// <summary>Creates a new client for TestSvc</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public TestSvcClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for TestSvc that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public TestSvcClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected TestSvcClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected TestSvcClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      public virtual global::Saas.Entity.Common.MsgString Get(global::Saas.Entity.Common.MsgEmpty request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Get(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::Saas.Entity.Common.MsgString Get(global::Saas.Entity.Common.MsgEmpty request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_Get, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::Saas.Entity.Common.MsgString> GetAsync(global::Saas.Entity.Common.MsgEmpty request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::Saas.Entity.Common.MsgString> GetAsync(global::Saas.Entity.Common.MsgEmpty request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_Get, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override TestSvcClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new TestSvcClient(configuration);
      }
    }

  }
}
#endregion