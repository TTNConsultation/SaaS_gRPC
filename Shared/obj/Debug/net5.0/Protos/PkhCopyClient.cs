// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Protos/pkh - Copy.client.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Pkh.message.client {

  /// <summary>Holder for reflection information generated from Protos/pkh - Copy.client.proto</summary>
  public static partial class PkhCopyClientReflection {

    #region Descriptor
    /// <summary>File descriptor for Protos/pkh - Copy.client.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static PkhCopyClientReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Ch5Qcm90b3MvcGtoIC0gQ29weS5jbGllbnQucHJvdG8SDmNsaWVudF9tZXNz",
            "YWdlImMKCEN1c3RvbWVyEgoKAmlkGAEgASgFEgwKBG5hbWUYAiABKAkSDQoF",
            "ZW1haWwYAyABKAkSGgoSaWRlbnRpdHlQcm92aWRlcklkGAQgASgFEhIKCmNy",
            "ZWRlbnRpYWwYBSABKAkiOgoMUmVnaXN0cmF0aW9uEioKCGN1c3RvbWVyGAEg",
            "ASgLMhguY2xpZW50X21lc3NhZ2UuQ3VzdG9tZXJCFaoCElBraC5tZXNzYWdl",
            "LmNsaWVudGIGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Pkh.message.client.Customer), global::Pkh.message.client.Customer.Parser, new[]{ "Id", "Name", "Email", "IdentityProviderId", "Credential" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Pkh.message.client.Registration), global::Pkh.message.client.Registration.Parser, new[]{ "Customer" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class Customer : pb::IMessage<Customer>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<Customer> _parser = new pb::MessageParser<Customer>(() => new Customer());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Customer> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Pkh.message.client.PkhCopyClientReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Customer() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Customer(Customer other) : this() {
      id_ = other.id_;
      name_ = other.name_;
      email_ = other.email_;
      identityProviderId_ = other.identityProviderId_;
      credential_ = other.credential_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Customer Clone() {
      return new Customer(this);
    }

    /// <summary>Field number for the "id" field.</summary>
    public const int IdFieldNumber = 1;
    private int id_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Id {
      get { return id_; }
      set {
        id_ = value;
      }
    }

    /// <summary>Field number for the "name" field.</summary>
    public const int NameFieldNumber = 2;
    private string name_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Name {
      get { return name_; }
      set {
        name_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "email" field.</summary>
    public const int EmailFieldNumber = 3;
    private string email_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Email {
      get { return email_; }
      set {
        email_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "identityProviderId" field.</summary>
    public const int IdentityProviderIdFieldNumber = 4;
    private int identityProviderId_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int IdentityProviderId {
      get { return identityProviderId_; }
      set {
        identityProviderId_ = value;
      }
    }

    /// <summary>Field number for the "credential" field.</summary>
    public const int CredentialFieldNumber = 5;
    private string credential_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Credential {
      get { return credential_; }
      set {
        credential_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Customer);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Customer other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Id != other.Id) return false;
      if (Name != other.Name) return false;
      if (Email != other.Email) return false;
      if (IdentityProviderId != other.IdentityProviderId) return false;
      if (Credential != other.Credential) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Id != 0) hash ^= Id.GetHashCode();
      if (Name.Length != 0) hash ^= Name.GetHashCode();
      if (Email.Length != 0) hash ^= Email.GetHashCode();
      if (IdentityProviderId != 0) hash ^= IdentityProviderId.GetHashCode();
      if (Credential.Length != 0) hash ^= Credential.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (Id != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(Id);
      }
      if (Name.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Name);
      }
      if (Email.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(Email);
      }
      if (IdentityProviderId != 0) {
        output.WriteRawTag(32);
        output.WriteInt32(IdentityProviderId);
      }
      if (Credential.Length != 0) {
        output.WriteRawTag(42);
        output.WriteString(Credential);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (Id != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(Id);
      }
      if (Name.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Name);
      }
      if (Email.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(Email);
      }
      if (IdentityProviderId != 0) {
        output.WriteRawTag(32);
        output.WriteInt32(IdentityProviderId);
      }
      if (Credential.Length != 0) {
        output.WriteRawTag(42);
        output.WriteString(Credential);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Id != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Id);
      }
      if (Name.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
      }
      if (Email.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Email);
      }
      if (IdentityProviderId != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(IdentityProviderId);
      }
      if (Credential.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Credential);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(Customer other) {
      if (other == null) {
        return;
      }
      if (other.Id != 0) {
        Id = other.Id;
      }
      if (other.Name.Length != 0) {
        Name = other.Name;
      }
      if (other.Email.Length != 0) {
        Email = other.Email;
      }
      if (other.IdentityProviderId != 0) {
        IdentityProviderId = other.IdentityProviderId;
      }
      if (other.Credential.Length != 0) {
        Credential = other.Credential;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            Id = input.ReadInt32();
            break;
          }
          case 18: {
            Name = input.ReadString();
            break;
          }
          case 26: {
            Email = input.ReadString();
            break;
          }
          case 32: {
            IdentityProviderId = input.ReadInt32();
            break;
          }
          case 42: {
            Credential = input.ReadString();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 8: {
            Id = input.ReadInt32();
            break;
          }
          case 18: {
            Name = input.ReadString();
            break;
          }
          case 26: {
            Email = input.ReadString();
            break;
          }
          case 32: {
            IdentityProviderId = input.ReadInt32();
            break;
          }
          case 42: {
            Credential = input.ReadString();
            break;
          }
        }
      }
    }
    #endif

  }

  public sealed partial class Registration : pb::IMessage<Registration>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<Registration> _parser = new pb::MessageParser<Registration>(() => new Registration());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Registration> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Pkh.message.client.PkhCopyClientReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Registration() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Registration(Registration other) : this() {
      customer_ = other.customer_ != null ? other.customer_.Clone() : null;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Registration Clone() {
      return new Registration(this);
    }

    /// <summary>Field number for the "customer" field.</summary>
    public const int CustomerFieldNumber = 1;
    private global::Pkh.message.client.Customer customer_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Pkh.message.client.Customer Customer {
      get { return customer_; }
      set {
        customer_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Registration);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Registration other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(Customer, other.Customer)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (customer_ != null) hash ^= Customer.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (customer_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(Customer);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (customer_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(Customer);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (customer_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Customer);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(Registration other) {
      if (other == null) {
        return;
      }
      if (other.customer_ != null) {
        if (customer_ == null) {
          Customer = new global::Pkh.message.client.Customer();
        }
        Customer.MergeFrom(other.Customer);
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            if (customer_ == null) {
              Customer = new global::Pkh.message.client.Customer();
            }
            input.ReadMessage(Customer);
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 10: {
            if (customer_ == null) {
              Customer = new global::Pkh.message.client.Customer();
            }
            input.ReadMessage(Customer);
            break;
          }
        }
      }
    }
    #endif

  }

  #endregion

}

#endregion Designer generated code
