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
	public partial class LanguageViewModel : IViewModelBase
	{
		/*WARNING - This class was generated by CodeTrigger. Changes to this file may cause incorrect behaviour and will be lost when the code is regenerated*/
		#region members
		private string _displayName = "Language";
		private readonly BOLanguage _searchBO = new BOLanguage();
		private readonly ObservableCollection<BOLanguage> _boLanguages = new ObservableCollection<BOLanguage>();
		private IList<BOLanguage> _boLanguagesCached = new List<BOLanguage>();
		private IList<BOLanguage> _boLanguagesCachedDelete = new List<BOLanguage>();
		private RelayCommand _deleteRowCommand;
		#endregion
		
		#region constructor
		public LanguageViewModel()
		{		}
		#endregion
		
		#region methods
		public static IList<BOLanguage> GetLanguageList()
		{
			try
			{
				IList<BOLanguage> listLanguage = BOLanguage.LanguageCollection();
				return listLanguage;
			}
			catch(Exception)
			{	/*rethrow or handle gracefully*/return null;	}
			finally	{	}
		}
		
		private void AddExpr(ref Criteria<BOLanguage> crit, string propertyName, object propertyValue, string propertyValueText, Func<object, string> formatter = null)
		{
			bool wildcard = propertyValueText.Contains("%");
			crit.Add(wildcard
				? Expression.Like(propertyName, propertyValue, formatter)
				: Expression.Eq(propertyName, propertyValue, formatter));
		}

		public int GetLoadCount()
		{
			var crit = new Criteria<BOLanguage>();
			try
			{
				if(_searchBO.Id.HasValue) AddExpr(ref crit, "Id", _searchBO.Id, "" );
				if(!string.IsNullOrEmpty(_searchBO.Code)) AddExpr(ref crit, "Code", _searchBO.Code, _searchBO.Code);
				if(!string.IsNullOrEmpty(_searchBO.Value)) AddExpr(ref crit, "Value", _searchBO.Value, _searchBO.Value);
				int resultCount = crit.Count();
				return resultCount;
			}
			catch	{	throw;	}
			finally	{	}
		}
		
		public void LoadRecords()
		{
			var crit = new Criteria<BOLanguage>();
			try
			{
				if(_searchBO.Id.HasValue) AddExpr(ref crit, "Id", _searchBO.Id, "" );
				if(!string.IsNullOrEmpty(_searchBO.Code)) AddExpr(ref crit, "Code", _searchBO.Code, _searchBO.Code);
				if(!string.IsNullOrEmpty(_searchBO.Value)) AddExpr(ref crit, "Value", _searchBO.Value, _searchBO.Value);
				
				_boLanguages.Clear();
				_boLanguagesCachedDelete.Clear();
				_boLanguagesCached = crit.List<BOLanguage>();
				foreach(BOLanguage boLanguage in _boLanguagesCached)
					_boLanguages.Add(boLanguage);
			}
			catch	{	throw;	}
			finally	{	}
		}
		
		public void SaveCurrentView()
		{
			try
			{
				foreach(BOLanguage boLanguage in _boLanguages)
				{
					if(_boLanguagesCached.Contains(boLanguage))
					{
						if(!boLanguage.IsDirty) continue;
						try{ boLanguage.Update(); }
						catch(Exception ex)
						{ throw new Exception("Error updating record: CodeTrigger has detected a data exception. Possible invalid foreign key reference, missing fields or other error. Exception details follow below.\r\n\r\n" + ex.Message); }
					}
					else
					{
						try{ boLanguage.SaveNew(); }
						catch(Exception ex)
						{ throw new Exception("Error saving new record: CodeTrigger has detected a data exception. Possible duplicate primary key, invalid foreign key reference, or missing fields. Exception details follow below.\r\n\r\n" + ex.Message); }
					}
				}
				foreach(BOLanguage boLanguage in _boLanguagesCachedDelete)
				{
					if((boLanguage == null)  || (!boLanguage.Id.HasValue))
					continue;
				
					try{ boLanguage.Delete(); }
					catch(Exception ex)
					{ throw new Exception("Error deleting record: CodeTrigger has detected a data exception. Possible existing foreign key reference or other error. Exception details follow below.\r\n\r\n" + ex.Message); }
				}
				_boLanguagesCachedDelete.Clear();
				
				LoadRecords();
			}
			catch	{	throw;	}
			finally	{	}
		}
		
		public void DeleteRow(object state)
		{
			if(state is BOLanguage)
			{
				_boLanguages.Remove((BOLanguage)state);
				_boLanguagesCachedDelete.Add((BOLanguage)state);
			}
		}
		#endregion
		
		#region properties
		public BOLanguage Filter
		{
			get {	return _searchBO;	}
		}
		public ObservableCollection<BOLanguage> Results
		{
			get {	return _boLanguages;	}
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
