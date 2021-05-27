using Google.Protobuf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utility;

namespace Pkh.Message.App
{
  public interface IReferenceData
  {
    public int RefId();

    public string RefCode();

    public bool Equals(int id) => RefId() == id;

    public bool Equals(string code) => RefCode().IsEqual(code);
  }

  public partial class IdentityProvider : IReferenceData
  {
    public string RefCode() => this.Code;

    public int RefId() => this.Id;
  }

  public partial class Status : IReferenceData
  {
    public string RefCode() => this.Code;

    public int RefId() => this.Id;
  }

  public partial class Languages : IReferenceData
  {
    public string RefCode() => this.Code;

    public int RefId() => this.Id;
  }
}