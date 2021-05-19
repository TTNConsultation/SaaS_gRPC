using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using Utility;

namespace Pkh.Message.Client
{
  public partial class Customer
  {
    public bool Compare(string text) => (this.IdentityProvider == 1) ? new Hasher().Compare(this.NameIdentifier, text)
                                                                     : this.NameIdentifier.IsEqual(text);
  }

  public partial class Registration
  {
    public string Encode() => new Hasher().Encode(this.Credential);
  }
}