// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Protos/service.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Saas.gRPC {

  /// <summary>Holder for reflection information generated from Protos/service.proto</summary>
  public static partial class ServiceReflection {

    #region Descriptor
    /// <summary>File descriptor for Protos/service.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static ServiceReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChRQcm90b3Mvc2VydmljZS5wcm90bxIIU2VydmljZXMaHGdvb2dsZS9wcm90",
            "b2J1Zi9zdHJ1Y3QucHJvdG8aFVByb3Rvcy9sYW5ndWFnZS5wcm90bxoWUHJv",
            "dG9zL3JlZmVyZW5jZS5wcm90bxoaUHJvdG9zL2FkbWluaXN0cmF0b3IucHJv",
            "dG8yuwIKDVJlc3RhdXJhbnRTdmMSOAoDR2V0EhYuZ29vZ2xlLnByb3RvYnVm",
            "LlZhbHVlGhkuQWRtaW5pc3RyYXRvci5SZXN0YXVyYW50EjwKBkxvb2t1cBIW",
            "Lmdvb2dsZS5wcm90b2J1Zi5WYWx1ZRoaLkFkbWluaXN0cmF0b3IuUmVzdGF1",
            "cmFudHMSOwoGQ3JlYXRlEhkuQWRtaW5pc3RyYXRvci5SZXN0YXVyYW50GhYu",
            "Z29vZ2xlLnByb3RvYnVmLlZhbHVlEjsKBlVwZGF0ZRIZLkFkbWluaXN0cmF0",
            "b3IuUmVzdGF1cmFudBoWLmdvb2dsZS5wcm90b2J1Zi5WYWx1ZRI4CgZEZWxl",
            "dGUSFi5nb29nbGUucHJvdG9idWYuVmFsdWUaFi5nb29nbGUucHJvdG9idWYu",
            "VmFsdWUyqwIKCFRhYmxlU3ZjEjMKA0dldBIWLmdvb2dsZS5wcm90b2J1Zi5W",
            "YWx1ZRoULkFkbWluaXN0cmF0b3IuVGFibGUSQAoPR2V0QnlSZXN0YXVyYW50",
            "EhYuZ29vZ2xlLnByb3RvYnVmLlZhbHVlGhUuQWRtaW5pc3RyYXRvci5UYWJs",
            "ZXMSNgoGQ3JlYXRlEhQuQWRtaW5pc3RyYXRvci5UYWJsZRoWLmdvb2dsZS5w",
            "cm90b2J1Zi5WYWx1ZRI2CgZVcGRhdGUSFC5BZG1pbmlzdHJhdG9yLlRhYmxl",
            "GhYuZ29vZ2xlLnByb3RvYnVmLlZhbHVlEjgKBkRlbGV0ZRIWLmdvb2dsZS5w",
            "cm90b2J1Zi5WYWx1ZRoWLmdvb2dsZS5wcm90b2J1Zi5WYWx1ZTKxAgoHSXRl",
            "bVN2YxIyCgNHZXQSFi5nb29nbGUucHJvdG9idWYuVmFsdWUaEy5BZG1pbmlz",
            "dHJhdG9yLkl0ZW0SPwoPR2V0QnlSZXN0YXVyYW50EhYuZ29vZ2xlLnByb3Rv",
            "YnVmLlZhbHVlGhQuQWRtaW5pc3RyYXRvci5JdGVtcxJDChNHZXRCeVJlc3Rh",
            "dXJhbnRNZW51EhYuZ29vZ2xlLnByb3RvYnVmLlZhbHVlGhQuQWRtaW5pc3Ry",
            "YXRvci5JdGVtcxI1CgZDcmVhdGUSEy5BZG1pbmlzdHJhdG9yLkl0ZW0aFi5n",
            "b29nbGUucHJvdG9idWYuVmFsdWUSNQoGVXBkYXRlEhMuQWRtaW5pc3RyYXRv",
            "ci5JdGVtGhYuZ29vZ2xlLnByb3RvYnVmLlZhbHVlMtgCChFSZXN0YXVyYW50",
            "TWVudVN2YxI8CgNHZXQSFi5nb29nbGUucHJvdG9idWYuVmFsdWUaHS5BZG1p",
            "bmlzdHJhdG9yLlJlc3RhdXJhbnRNZW51EkkKD0dldEJ5UmVzdGF1cmFudBIW",
            "Lmdvb2dsZS5wcm90b2J1Zi5WYWx1ZRoeLkFkbWluaXN0cmF0b3IuUmVzdGF1",
            "cmFudE1lbnVzEj8KBkNyZWF0ZRIdLkFkbWluaXN0cmF0b3IuUmVzdGF1cmFu",
            "dE1lbnUaFi5nb29nbGUucHJvdG9idWYuVmFsdWUSPwoGVXBkYXRlEh0uQWRt",
            "aW5pc3RyYXRvci5SZXN0YXVyYW50TWVudRoWLmdvb2dsZS5wcm90b2J1Zi5W",
            "YWx1ZRI4CgZEZWxldGUSFi5nb29nbGUucHJvdG9idWYuVmFsdWUaFi5nb29n",
            "bGUucHJvdG9idWYuVmFsdWUy6wIKB01lbnVTdmMSMgoDR2V0EhYuZ29vZ2xl",
            "LnByb3RvYnVmLlZhbHVlGhMuQWRtaW5pc3RyYXRvci5NZW51EkMKE0dldEJ5",
            "UmVzdGF1cmFudE1lbnUSFi5nb29nbGUucHJvdG9idWYuVmFsdWUaFC5BZG1p",
            "bmlzdHJhdG9yLk1lbnVzEj8KD0dldEJ5UmVzdGF1cmFudBIWLmdvb2dsZS5w",
            "cm90b2J1Zi5WYWx1ZRoULkFkbWluaXN0cmF0b3IuTWVudXMSNQoGQ3JlYXRl",
            "EhMuQWRtaW5pc3RyYXRvci5NZW51GhYuZ29vZ2xlLnByb3RvYnVmLlZhbHVl",
            "EjUKBlVwZGF0ZRITLkFkbWluaXN0cmF0b3IuTWVudRoWLmdvb2dsZS5wcm90",
            "b2J1Zi5WYWx1ZRI4CgZEZWxldGUSFi5nb29nbGUucHJvdG9idWYuVmFsdWUa",
            "Fi5nb29nbGUucHJvdG9idWYuVmFsdWUygQMKC01lbnVJdGVtU3ZjEjYKA0dl",
            "dBIWLmdvb2dsZS5wcm90b2J1Zi5WYWx1ZRoXLkFkbWluaXN0cmF0b3IuTWVu",
            "dUl0ZW0SPQoJR2V0QnlNZW51EhYuZ29vZ2xlLnByb3RvYnVmLlZhbHVlGhgu",
            "QWRtaW5pc3RyYXRvci5NZW51SXRlbXMSPQoJR2V0QnlJdGVtEhYuZ29vZ2xl",
            "LnByb3RvYnVmLlZhbHVlGhguQWRtaW5pc3RyYXRvci5NZW51SXRlbXMSRwoQ",
            "R2V0QnlNZW51QW5kSXRlbRIaLkFkbWluaXN0cmF0b3IuTWVudUl0ZW1JZHMa",
            "Fy5BZG1pbmlzdHJhdG9yLk1lbnVJdGVtEjkKBkNyZWF0ZRIXLkFkbWluaXN0",
            "cmF0b3IuTWVudUl0ZW0aFi5nb29nbGUucHJvdG9idWYuVmFsdWUSOAoGRGVs",
            "ZXRlEhYuZ29vZ2xlLnByb3RvYnVmLlZhbHVlGhYuZ29vZ2xlLnByb3RvYnVm",
            "LlZhbHVlMr8CCgpBcHBEYXRhU3ZjEkoKElN1cHBvcnRlZExhbmd1YWdlcxIW",
            "Lmdvb2dsZS5wcm90b2J1Zi5WYWx1ZRocLkxhbmd1YWdlLlN1cHBvcnRlZExh",
            "bmd1YWdlcxIzCgZTdGF0ZXMSFi5nb29nbGUucHJvdG9idWYuVmFsdWUaES5S",
            "ZWZlcmVuY2UuU3RhdGVzEjcKCEtleVR5cGVzEhYuZ29vZ2xlLnByb3RvYnVm",
            "LlZhbHVlGhMuUmVmZXJlbmNlLktleVR5cGVzEjsKClJlZmVyZW5jZXMSFi5n",
            "b29nbGUucHJvdG9idWYuVmFsdWUaFS5SZWZlcmVuY2UuUmVmZXJlbmNlcxI6",
            "CgpEaWN0aW9uYXJ5EhYuTGFuZ3VhZ2UuQ29kZUxhbmd1YWdlGhQuTGFuZ3Vh",
            "Z2UuRGljdGlvbmFyeUIMqgIJU2Fhcy5nUlBDYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::Google.Protobuf.WellKnownTypes.StructReflection.Descriptor, global::Saas.Message.Language.LanguageReflection.Descriptor, global::Saas.Message.Reference.ReferenceReflection.Descriptor, global::Saas.Message.Administrator.AdministratorReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, null));
    }
    #endregion

  }
}

#endregion Designer generated code