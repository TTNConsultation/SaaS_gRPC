﻿using System;
using System.Collections.Generic;
using System.Linq;

using StoreProcedure;
using StoreProcedure.Interface;

using Saas.Message.Language;

namespace Saas.Message.Reference
{
  public partial class States
  {
    public int DeleteId => Values.First(s => s.Name.IsEqual("Delete")).Id;
    public int EnableId => Values.First(s => s.Name.IsEqual("Enable")).Id;
    public int DisableId => Values.First(s => s.Name.IsEqual("Disable")).Id;

    public States(IEnumerable<State> values) => Values.AddRange(values);
  }

  public partial class KeyTypes
  {
    public KeyTypes(ICollection<KeyType> values)
    {
      Values.AddRange(values);
    }
  }

  public partial class References
  {
    internal readonly AppSetting AppSetting;

    public References(IDbContext context)
    {
      using var appSettingCtx = context.ReferenceData<AppSetting>();
      AppSetting = (appSettingCtx.IsReady) ? appSettingCtx.Read().First() : throw new NotSupportedException();

      using var stateCtx = context.ReferenceData<State>();
      States = (stateCtx.IsReady) ? new States(stateCtx.Read()) : throw new NotSupportedException();

      using var codeLanguageCtx = context.ReferenceData<CodeLanguage>();
      Languages = (codeLanguageCtx.IsReady) ? new SupportedLanguages(codeLanguageCtx.Read()) : throw new NotSupportedException();

      using var keyTypeCtx = context.ReferenceData<KeyType>();
      KeyTypes = (keyTypeCtx.IsReady) ? new KeyTypes(keyTypeCtx.Read()) : throw new NotSupportedException();
    }
  }
}