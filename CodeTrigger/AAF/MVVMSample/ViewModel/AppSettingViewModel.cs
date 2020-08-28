/*************************************************************
** Class generated by CodeTrigger, Version 6.3.0.4
** This class was generated on 2020-08-01 3:54:27 AM
** Changes to this file may cause incorrect behaviour and will be lost if the code is regenerated
**************************************************************/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AAF.BusinessObjects;
using Expr = AAF.BusinessObjects;

namespace AAF.MVVMSample.ViewModel
{
	public partial class AppSettingViewModel : IViewModelBase
	{
		/*WARNING - This class was generated by CodeTrigger. Changes to this file may cause incorrect behaviour and will be lost when the code is regenerated*/
		#region members
		private string _displayName = "AppSetting";
		private readonly BOAppSetting _searchBO = new BOAppSetting();
		private readonly ObservableCollection<BOAppSetting> _boAppSettings = new ObservableCollection<BOAppSetting>();
		private IList<BOAppSetting> _boAppSettingsCached = new List<BOAppSetting>();
		private IList<BOAppSetting> _boAppSettingsCachedDelete = new List<BOAppSetting>();
		private RelayCommand _deleteRowCommand;
		#endregion
		
		#region constructor
		public AppSettingViewModel()
		{		}
		#endregion
		
		#region methods
		public static IList<BOAppSetting> GetAppSettingList()
		{
			try
			{
				IList<BOAppSetting> listAppSetting = BOAppSetting.AppSettingCollection();
				return listAppSetting;
			}
			catch(Exception)
			{	/*rethrow or handle gracefully*/return null;	}
			finally	{	}
		}
		
		private void AddExpr(ref Criteria<BOAppSetting> crit, string propertyName, object propertyValue, string propertyValueText, Func<object, string> formatter = null)
		{
			bool wildcard = propertyValueText.Contains("%");
			crit.Add(wildcard
				? Expression.Like(propertyName, propertyValue, formatter)
				: Expression.Eq(propertyName, propertyValue, formatter));
		}

		public int GetLoadCount()
		{
			var crit = new Criteria<BOAppSetting>();
			try
			{
				if(_searchBO.Id.HasValue) AddExpr(ref crit, "Id", _searchBO.Id, "" );
				if(_searchBO.NameKey.HasValue) AddExpr(ref crit, "NameKey", _searchBO.NameKey, "" );
				if(_searchBO.DescriptionKey.HasValue) AddExpr(ref crit, "DescriptionKey", _searchBO.DescriptionKey, "" );
				int resultCount = crit.Count();
				return resultCount;
			}
			catch	{	throw;	}
			finally	{	}
		}
		
		public void LoadRecords()
		{
			var crit = new Criteria<BOAppSetting>();
			try
			{
				if(_searchBO.Id.HasValue) AddExpr(ref crit, "Id", _searchBO.Id, "" );
				if(_searchBO.NameKey.HasValue) AddExpr(ref crit, "NameKey", _searchBO.NameKey, "" );
				if(_searchBO.DescriptionKey.HasValue) AddExpr(ref crit, "DescriptionKey", _searchBO.DescriptionKey, "" );
				
				_boAppSettings.Clear();
				_boAppSettingsCachedDelete.Clear();
				_boAppSettingsCached = crit.List<BOAppSetting>();
				foreach(BOAppSetting boAppSetting in _boAppSettingsCached)
					_boAppSettings.Add(boAppSetting);
			}
			catch	{	throw;	}
			finally	{	}
		}
		
		public void SaveCurrentView()
		{
			try
			{
				foreach(BOAppSetting boAppSetting in _boAppSettings)
				{
					if(_boAppSettingsCached.Contains(boAppSetting))
					{
						if(!boAppSetting.IsDirty) continue;
						try{ boAppSetting.Update(); }
						catch(Exception ex)
						{ throw new Exception("Error updating record: CodeTrigger has detected a data exception. Possible invalid foreign key reference, missing fields or other error. Exception details follow below.\r\n\r\n" + ex.Message); }
					}
					else
					{
						try{ boAppSetting.SaveNew(); }
						catch(Exception ex)
						{ throw new Exception("Error saving new record: CodeTrigger has detected a data exception. Possible duplicate primary key, invalid foreign key reference, or missing fields. Exception details follow below.\r\n\r\n" + ex.Message); }
					}
				}
				foreach(BOAppSetting boAppSetting in _boAppSettingsCachedDelete)
				{
					if((boAppSetting == null)  || (!boAppSetting.Id.HasValue))
					continue;
				
					try{ boAppSetting.Delete(); }
					catch(Exception ex)
					{ throw new Exception("Error deleting record: CodeTrigger has detected a data exception. Possible existing foreign key reference or other error. Exception details follow below.\r\n\r\n" + ex.Message); }
				}
				_boAppSettingsCachedDelete.Clear();
				
				LoadRecords();
			}
			catch	{	throw;	}
			finally	{	}
		}
		
		public void DeleteRow(object state)
		{
			if(state is BOAppSetting)
			{
				_boAppSettings.Remove((BOAppSetting)state);
				_boAppSettingsCachedDelete.Add((BOAppSetting)state);
			}
		}
		#endregion
		
		#region properties
		public BOAppSetting Filter
		{
			get {	return _searchBO;	}
		}
		public ObservableCollection<BOAppSetting> Results
		{
			get {	return _boAppSettings;	}
		}
		
		public string DisplayName
		{
			get {	return _displayName;	}
			set {	_displayName = value;	}
		}
		
		public RelayCommand DeleteRowCommand
		{
			get
			{
				if(_deleteRowCommand == null)
					_deleteRowCommand = new RelayCommand(DeleteRow);
				return _deleteRowCommand;
			}
		}
		#endregion
	}
}
