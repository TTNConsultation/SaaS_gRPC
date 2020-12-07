// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Protos/SpProperty.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Saas.Dal.SpProperty {

  /// <summary>Holder for reflection information generated from Protos/SpProperty.proto</summary>
  public static partial class SpPropertyReflection {

    #region Descriptor
    /// <summary>File descriptor for Protos/SpProperty.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static SpPropertyReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChdQcm90b3MvU3BQcm9wZXJ0eS5wcm90bxIKU3BQcm9wZXJ0eSJUCgpTcFBy",
            "b3BlcnR5EgoKAklkGAEgASgFEhAKCEZ1bGxOYW1lGAIgASgJEg4KBlNjaGVt",
            "YRgDIAEoCRIMCgRUeXBlGAQgASgJEgoKAk9wGAUgASgJIrABCgtTcFBhcmFt",
            "ZXRlchIOCgZTcE5hbWUYASABKAkSDAoEU3BJZBgCIAEoBRIMCgROYW1lGAMg",
            "ASgJEgwKBFR5cGUYBCABKAkSEQoJTWF4TGVuZ3RoGAUgASgFEhEKCVByZWNp",
            "c2lvbhgGIAEoBRINCgVTY2FsZRgHIAEoBRINCgVPcmRlchgIIAEoBRIQCghJ",
            "c091dHB1dBgJIAEoCBIRCglDb2xsYXRpb24YCiABKAlCFqoCE1NhYXMuRGFs",
            "LlNwUHJvcGVydHliBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Saas.Dal.SpProperty.SpProperty), global::Saas.Dal.SpProperty.SpProperty.Parser, new[]{ "Id", "FullName", "Schema", "Type", "Op" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Saas.Dal.SpProperty.SpParameter), global::Saas.Dal.SpProperty.SpParameter.Parser, new[]{ "SpName", "SpId", "Name", "Type", "MaxLength", "Precision", "Scale", "Order", "IsOutput", "Collation" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class SpProperty : pb::IMessage<SpProperty>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<SpProperty> _parser = new pb::MessageParser<SpProperty>(() => new SpProperty());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<SpProperty> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Saas.Dal.SpProperty.SpPropertyReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SpProperty() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SpProperty(SpProperty other) : this() {
      id_ = other.id_;
      fullName_ = other.fullName_;
      schema_ = other.schema_;
      type_ = other.type_;
      op_ = other.op_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SpProperty Clone() {
      return new SpProperty(this);
    }

    /// <summary>Field number for the "Id" field.</summary>
    public const int IdFieldNumber = 1;
    private int id_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Id {
      get { return id_; }
      set {
        id_ = value;
      }
    }

    /// <summary>Field number for the "FullName" field.</summary>
    public const int FullNameFieldNumber = 2;
    private string fullName_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string FullName {
      get { return fullName_; }
      set {
        fullName_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "Schema" field.</summary>
    public const int SchemaFieldNumber = 3;
    private string schema_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Schema {
      get { return schema_; }
      set {
        schema_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "Type" field.</summary>
    public const int TypeFieldNumber = 4;
    private string type_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Type {
      get { return type_; }
      set {
        type_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "Op" field.</summary>
    public const int OpFieldNumber = 5;
    private string op_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Op {
      get { return op_; }
      set {
        op_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as SpProperty);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(SpProperty other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Id != other.Id) return false;
      if (FullName != other.FullName) return false;
      if (Schema != other.Schema) return false;
      if (Type != other.Type) return false;
      if (Op != other.Op) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Id != 0) hash ^= Id.GetHashCode();
      if (FullName.Length != 0) hash ^= FullName.GetHashCode();
      if (Schema.Length != 0) hash ^= Schema.GetHashCode();
      if (Type.Length != 0) hash ^= Type.GetHashCode();
      if (Op.Length != 0) hash ^= Op.GetHashCode();
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
      if (FullName.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(FullName);
      }
      if (Schema.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(Schema);
      }
      if (Type.Length != 0) {
        output.WriteRawTag(34);
        output.WriteString(Type);
      }
      if (Op.Length != 0) {
        output.WriteRawTag(42);
        output.WriteString(Op);
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
      if (FullName.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(FullName);
      }
      if (Schema.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(Schema);
      }
      if (Type.Length != 0) {
        output.WriteRawTag(34);
        output.WriteString(Type);
      }
      if (Op.Length != 0) {
        output.WriteRawTag(42);
        output.WriteString(Op);
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
      if (FullName.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(FullName);
      }
      if (Schema.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Schema);
      }
      if (Type.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Type);
      }
      if (Op.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Op);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(SpProperty other) {
      if (other == null) {
        return;
      }
      if (other.Id != 0) {
        Id = other.Id;
      }
      if (other.FullName.Length != 0) {
        FullName = other.FullName;
      }
      if (other.Schema.Length != 0) {
        Schema = other.Schema;
      }
      if (other.Type.Length != 0) {
        Type = other.Type;
      }
      if (other.Op.Length != 0) {
        Op = other.Op;
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
            FullName = input.ReadString();
            break;
          }
          case 26: {
            Schema = input.ReadString();
            break;
          }
          case 34: {
            Type = input.ReadString();
            break;
          }
          case 42: {
            Op = input.ReadString();
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
            FullName = input.ReadString();
            break;
          }
          case 26: {
            Schema = input.ReadString();
            break;
          }
          case 34: {
            Type = input.ReadString();
            break;
          }
          case 42: {
            Op = input.ReadString();
            break;
          }
        }
      }
    }
    #endif

  }

  public sealed partial class SpParameter : pb::IMessage<SpParameter>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<SpParameter> _parser = new pb::MessageParser<SpParameter>(() => new SpParameter());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<SpParameter> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Saas.Dal.SpProperty.SpPropertyReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SpParameter() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SpParameter(SpParameter other) : this() {
      spName_ = other.spName_;
      spId_ = other.spId_;
      name_ = other.name_;
      type_ = other.type_;
      maxLength_ = other.maxLength_;
      precision_ = other.precision_;
      scale_ = other.scale_;
      order_ = other.order_;
      isOutput_ = other.isOutput_;
      collation_ = other.collation_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SpParameter Clone() {
      return new SpParameter(this);
    }

    /// <summary>Field number for the "SpName" field.</summary>
    public const int SpNameFieldNumber = 1;
    private string spName_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string SpName {
      get { return spName_; }
      set {
        spName_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "SpId" field.</summary>
    public const int SpIdFieldNumber = 2;
    private int spId_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int SpId {
      get { return spId_; }
      set {
        spId_ = value;
      }
    }

    /// <summary>Field number for the "Name" field.</summary>
    public const int NameFieldNumber = 3;
    private string name_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Name {
      get { return name_; }
      set {
        name_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "Type" field.</summary>
    public const int TypeFieldNumber = 4;
    private string type_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Type {
      get { return type_; }
      set {
        type_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "MaxLength" field.</summary>
    public const int MaxLengthFieldNumber = 5;
    private int maxLength_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int MaxLength {
      get { return maxLength_; }
      set {
        maxLength_ = value;
      }
    }

    /// <summary>Field number for the "Precision" field.</summary>
    public const int PrecisionFieldNumber = 6;
    private int precision_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Precision {
      get { return precision_; }
      set {
        precision_ = value;
      }
    }

    /// <summary>Field number for the "Scale" field.</summary>
    public const int ScaleFieldNumber = 7;
    private int scale_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Scale {
      get { return scale_; }
      set {
        scale_ = value;
      }
    }

    /// <summary>Field number for the "Order" field.</summary>
    public const int OrderFieldNumber = 8;
    private int order_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Order {
      get { return order_; }
      set {
        order_ = value;
      }
    }

    /// <summary>Field number for the "IsOutput" field.</summary>
    public const int IsOutputFieldNumber = 9;
    private bool isOutput_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool IsOutput {
      get { return isOutput_; }
      set {
        isOutput_ = value;
      }
    }

    /// <summary>Field number for the "Collation" field.</summary>
    public const int CollationFieldNumber = 10;
    private string collation_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Collation {
      get { return collation_; }
      set {
        collation_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as SpParameter);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(SpParameter other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (SpName != other.SpName) return false;
      if (SpId != other.SpId) return false;
      if (Name != other.Name) return false;
      if (Type != other.Type) return false;
      if (MaxLength != other.MaxLength) return false;
      if (Precision != other.Precision) return false;
      if (Scale != other.Scale) return false;
      if (Order != other.Order) return false;
      if (IsOutput != other.IsOutput) return false;
      if (Collation != other.Collation) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (SpName.Length != 0) hash ^= SpName.GetHashCode();
      if (SpId != 0) hash ^= SpId.GetHashCode();
      if (Name.Length != 0) hash ^= Name.GetHashCode();
      if (Type.Length != 0) hash ^= Type.GetHashCode();
      if (MaxLength != 0) hash ^= MaxLength.GetHashCode();
      if (Precision != 0) hash ^= Precision.GetHashCode();
      if (Scale != 0) hash ^= Scale.GetHashCode();
      if (Order != 0) hash ^= Order.GetHashCode();
      if (IsOutput != false) hash ^= IsOutput.GetHashCode();
      if (Collation.Length != 0) hash ^= Collation.GetHashCode();
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
      if (SpName.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(SpName);
      }
      if (SpId != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(SpId);
      }
      if (Name.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(Name);
      }
      if (Type.Length != 0) {
        output.WriteRawTag(34);
        output.WriteString(Type);
      }
      if (MaxLength != 0) {
        output.WriteRawTag(40);
        output.WriteInt32(MaxLength);
      }
      if (Precision != 0) {
        output.WriteRawTag(48);
        output.WriteInt32(Precision);
      }
      if (Scale != 0) {
        output.WriteRawTag(56);
        output.WriteInt32(Scale);
      }
      if (Order != 0) {
        output.WriteRawTag(64);
        output.WriteInt32(Order);
      }
      if (IsOutput != false) {
        output.WriteRawTag(72);
        output.WriteBool(IsOutput);
      }
      if (Collation.Length != 0) {
        output.WriteRawTag(82);
        output.WriteString(Collation);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (SpName.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(SpName);
      }
      if (SpId != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(SpId);
      }
      if (Name.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(Name);
      }
      if (Type.Length != 0) {
        output.WriteRawTag(34);
        output.WriteString(Type);
      }
      if (MaxLength != 0) {
        output.WriteRawTag(40);
        output.WriteInt32(MaxLength);
      }
      if (Precision != 0) {
        output.WriteRawTag(48);
        output.WriteInt32(Precision);
      }
      if (Scale != 0) {
        output.WriteRawTag(56);
        output.WriteInt32(Scale);
      }
      if (Order != 0) {
        output.WriteRawTag(64);
        output.WriteInt32(Order);
      }
      if (IsOutput != false) {
        output.WriteRawTag(72);
        output.WriteBool(IsOutput);
      }
      if (Collation.Length != 0) {
        output.WriteRawTag(82);
        output.WriteString(Collation);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (SpName.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(SpName);
      }
      if (SpId != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(SpId);
      }
      if (Name.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
      }
      if (Type.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Type);
      }
      if (MaxLength != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(MaxLength);
      }
      if (Precision != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Precision);
      }
      if (Scale != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Scale);
      }
      if (Order != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Order);
      }
      if (IsOutput != false) {
        size += 1 + 1;
      }
      if (Collation.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Collation);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(SpParameter other) {
      if (other == null) {
        return;
      }
      if (other.SpName.Length != 0) {
        SpName = other.SpName;
      }
      if (other.SpId != 0) {
        SpId = other.SpId;
      }
      if (other.Name.Length != 0) {
        Name = other.Name;
      }
      if (other.Type.Length != 0) {
        Type = other.Type;
      }
      if (other.MaxLength != 0) {
        MaxLength = other.MaxLength;
      }
      if (other.Precision != 0) {
        Precision = other.Precision;
      }
      if (other.Scale != 0) {
        Scale = other.Scale;
      }
      if (other.Order != 0) {
        Order = other.Order;
      }
      if (other.IsOutput != false) {
        IsOutput = other.IsOutput;
      }
      if (other.Collation.Length != 0) {
        Collation = other.Collation;
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
            SpName = input.ReadString();
            break;
          }
          case 16: {
            SpId = input.ReadInt32();
            break;
          }
          case 26: {
            Name = input.ReadString();
            break;
          }
          case 34: {
            Type = input.ReadString();
            break;
          }
          case 40: {
            MaxLength = input.ReadInt32();
            break;
          }
          case 48: {
            Precision = input.ReadInt32();
            break;
          }
          case 56: {
            Scale = input.ReadInt32();
            break;
          }
          case 64: {
            Order = input.ReadInt32();
            break;
          }
          case 72: {
            IsOutput = input.ReadBool();
            break;
          }
          case 82: {
            Collation = input.ReadString();
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
            SpName = input.ReadString();
            break;
          }
          case 16: {
            SpId = input.ReadInt32();
            break;
          }
          case 26: {
            Name = input.ReadString();
            break;
          }
          case 34: {
            Type = input.ReadString();
            break;
          }
          case 40: {
            MaxLength = input.ReadInt32();
            break;
          }
          case 48: {
            Precision = input.ReadInt32();
            break;
          }
          case 56: {
            Scale = input.ReadInt32();
            break;
          }
          case 64: {
            Order = input.ReadInt32();
            break;
          }
          case 72: {
            IsOutput = input.ReadBool();
            break;
          }
          case 82: {
            Collation = input.ReadString();
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
