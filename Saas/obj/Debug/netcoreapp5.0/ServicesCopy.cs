// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Protos/services - Copy.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Saas.gRPC {

  /// <summary>Holder for reflection information generated from Protos/services - Copy.proto</summary>
  public static partial class ServicesCopyReflection {

    #region Descriptor
    /// <summary>File descriptor for Protos/services - Copy.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static ServicesCopyReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChxQcm90b3Mvc2VydmljZXMgLSBDb3B5LnByb3RvEghTZXJ2aWNlcxoTUHJv",
            "dG9zL2NvbW1vbi5wcm90bxoQUHJvdG9zL2FwcC5wcm90bxoaUHJvdG9zL2Fk",
            "bWluaXN0cmF0b3IucHJvdG8ytAIKDVJlc3RhdXJhbnRTdmMSPAoDR2V0Eg4u",
            "Q29tbW9uLk1zZ0ludBolLkFkbWluaXN0cmF0b3IuUmVzdGF1cmFudHMuUmVz",
            "dGF1cmFudBI3CgZMb29rdXASES5Db21tb24uTXNnU3RyaW5nGhouQWRtaW5p",
            "c3RyYXRvci5SZXN0YXVyYW50cxI/CgZDcmVhdGUSJS5BZG1pbmlzdHJhdG9y",
            "LlJlc3RhdXJhbnRzLlJlc3RhdXJhbnQaDi5Db21tb24uTXNnSW50EkAKBlVw",
            "ZGF0ZRIlLkFkbWluaXN0cmF0b3IuUmVzdGF1cmFudHMuUmVzdGF1cmFudBoP",
            "LkNvbW1vbi5Nc2dCb29sEikKBkRlbGV0ZRIOLkNvbW1vbi5Nc2dJbnQaDy5D",
            "b21tb24uTXNnQm9vbDKSAgoIVGFibGVTdmMSMgoDR2V0Eg4uQ29tbW9uLk1z",
            "Z0ludBobLkFkbWluaXN0cmF0b3IuVGFibGVzLlRhYmxlEjgKD0dldEJ5UmVz",
            "dGF1cmFudBIOLkNvbW1vbi5Nc2dJbnQaFS5BZG1pbmlzdHJhdG9yLlRhYmxl",
            "cxI1CgZDcmVhdGUSGy5BZG1pbmlzdHJhdG9yLlRhYmxlcy5UYWJsZRoOLkNv",
            "bW1vbi5Nc2dJbnQSNgoGVXBkYXRlEhsuQWRtaW5pc3RyYXRvci5UYWJsZXMu",
            "VGFibGUaDy5Db21tb24uTXNnQm9vbBIpCgZEZWxldGUSDi5Db21tb24uTXNn",
            "SW50Gg8uQ29tbW9uLk1zZ0Jvb2wymwIKB0l0ZW1TdmMSMAoDR2V0Eg4uQ29t",
            "bW9uLk1zZ0ludBoZLkFkbWluaXN0cmF0b3IuSXRlbXMuSXRlbRI3Cg9HZXRC",
            "eVJlc3RhdXJhbnQSDi5Db21tb24uTXNnSW50GhQuQWRtaW5pc3RyYXRvci5J",
            "dGVtcxI7ChNHZXRCeVJlc3RhdXJhbnRNZW51Eg4uQ29tbW9uLk1zZ0ludBoU",
            "LkFkbWluaXN0cmF0b3IuSXRlbXMSMwoGQ3JlYXRlEhkuQWRtaW5pc3RyYXRv",
            "ci5JdGVtcy5JdGVtGg4uQ29tbW9uLk1zZ0ludBIzCgZVcGRhdGUSGS5BZG1p",
            "bmlzdHJhdG9yLkl0ZW1zLkl0ZW0aDi5Db21tb24uTXNnSW50MtgCChFSZXN0",
            "YXVyYW50TWVudVN2YxJECgNHZXQSDi5Db21tb24uTXNnSW50Gi0uQWRtaW5p",
            "c3RyYXRvci5SZXN0YXVyYW50TWVudXMuUmVzdGF1cmFudE1lbnUSQQoPR2V0",
            "QnlSZXN0YXVyYW50Eg4uQ29tbW9uLk1zZ0ludBoeLkFkbWluaXN0cmF0b3Iu",
            "UmVzdGF1cmFudE1lbnVzEkcKBkNyZWF0ZRItLkFkbWluaXN0cmF0b3IuUmVz",
            "dGF1cmFudE1lbnVzLlJlc3RhdXJhbnRNZW51Gg4uQ29tbW9uLk1zZ0ludBJH",
            "CgZVcGRhdGUSLS5BZG1pbmlzdHJhdG9yLlJlc3RhdXJhbnRNZW51cy5SZXN0",
            "YXVyYW50TWVudRoOLkNvbW1vbi5Nc2dJbnQSKAoGRGVsZXRlEg4uQ29tbW9u",
            "Lk1zZ0ludBoOLkNvbW1vbi5Nc2dJbnQyxQIKB01lbnVTdmMSMAoDR2V0Eg4u",
            "Q29tbW9uLk1zZ0ludBoZLkFkbWluaXN0cmF0b3IuTWVudXMuTWVudRI7ChNH",
            "ZXRCeVJlc3RhdXJhbnRNZW51Eg4uQ29tbW9uLk1zZ0ludBoULkFkbWluaXN0",
            "cmF0b3IuTWVudXMSNwoPR2V0QnlSZXN0YXVyYW50Eg4uQ29tbW9uLk1zZ0lu",
            "dBoULkFkbWluaXN0cmF0b3IuTWVudXMSMwoGQ3JlYXRlEhkuQWRtaW5pc3Ry",
            "YXRvci5NZW51cy5NZW51Gg4uQ29tbW9uLk1zZ0ludBIzCgZVcGRhdGUSGS5B",
            "ZG1pbmlzdHJhdG9yLk1lbnVzLk1lbnUaDi5Db21tb24uTXNnSW50EigKBkRl",
            "bGV0ZRIOLkNvbW1vbi5Nc2dJbnQaDi5Db21tb24uTXNnSW50Mu8CCgtNZW51",
            "SXRlbVN2YxI4CgNHZXQSDi5Db21tb24uTXNnSW50GiEuQWRtaW5pc3RyYXRv",
            "ci5NZW51SXRlbXMuTWVudUl0ZW0SNQoJR2V0QnlNZW51Eg4uQ29tbW9uLk1z",
            "Z0ludBoYLkFkbWluaXN0cmF0b3IuTWVudUl0ZW1zEjUKCUdldEJ5SXRlbRIO",
            "LkNvbW1vbi5Nc2dJbnQaGC5BZG1pbmlzdHJhdG9yLk1lbnVJdGVtcxJRChBH",
            "ZXRCeU1lbnVBbmRJdGVtEhouQWRtaW5pc3RyYXRvci5NZW51SXRlbUlkcxoh",
            "LkFkbWluaXN0cmF0b3IuTWVudUl0ZW1zLk1lbnVJdGVtEjsKBkNyZWF0ZRIh",
            "LkFkbWluaXN0cmF0b3IuTWVudUl0ZW1zLk1lbnVJdGVtGg4uQ29tbW9uLk1z",
            "Z0ludBIoCgZEZWxldGUSDi5Db21tb24uTXNnSW50Gg4uQ29tbW9uLk1zZ0lu",
            "dDKRAQoKQXBwRGF0YVN2YxItCglMYW5ndWFnZXMSEC5Db21tb24uTXNnRW1w",
            "dHkaDi5BcHAuTGFuZ3VhZ2VzEicKBlN0YXRlcxIQLkNvbW1vbi5Nc2dFbXB0",
            "eRoLLkFwcC5TdGF0ZXMSKwoIS2V5VHlwZXMSEC5Db21tb24uTXNnRW1wdHka",
            "DS5BcHAuS2V5VHlwZXNCDKoCCVNhYXMuZ1JQQ2IGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::Saas.Entity.Common.CommonReflection.Descriptor, global::Saas.Entity.App.AppReflection.Descriptor, global::Saas.Entity.Administrator.AdministratorReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, null));
    }
    #endregion

  }
}

#endregion Designer generated code