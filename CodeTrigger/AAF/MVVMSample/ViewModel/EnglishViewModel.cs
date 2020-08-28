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
	public partial class EnglishViewModel : IViewModelBase
	{
		/*WARNING - This class was generated by CodeTrigger. Changes to this file may cause incorrect behaviour and will be lost when the code is regenerated*/
		#region members
		private string _displayName = "English";
		private readonly BOEnglish _searchBO = new BOEnglish();
		private readonly ObservableCollection<BOEnglish> _boEnglishs = new ObservableCollection<BOEnglish>();
		private IList<BOEnglish> _boEnglishsCached = new List<BOEnglish>();
		private IList<BOEnglish> _boEnglishsCachedDelete = new List<BOEnglish>();
		private RelayCommand _deleteRowCommand;
		#endregion
		
		#region constructor
		public EnglishViewModel()
		{		}
		#endregion
		
		#region methods
		public static IList<BOEnglish> GetEnglishList()
		{
			try
			{
				IList<BOEnglish> listEnglish = BOEnglish.EnglishCollection();
				return listEnglish;
			}
			catch(Exception)
			{	/*rethrow or handle gracefully*/return null;	}
			finally	{	}
		}
		
		private void AddExpr(ref Criteria<BOEnglish> crit, string propertyName, object propertyValue, string propertyValueText, Func<object, string> formatter = null)
		{
			bool wildcard = propertyValueText.Contains("%");
			crit.Add(wildcard
				? Expression.Like(propertyName, propertyValue, formatter)
				: Expression.Eq(propertyName, propertyValue, formatter));
		}

		public int GetLoadCount()
		{
			var crit = new Criteria<BOEnglish>();
			try
			{
				if(_searchBO.Key.HasValue) AddExpr(ref crit, "Key", _searchBO.Key, "" );
				if(!string.IsNullOrEmpty(_searchBO.Val)) AddExpr(ref crit, "Val", _searchBO.Val, _searchBO.Val);
				int resultCount = crit.Count();
				return resultCount;
			}
			catch	{	throw;	}
			finally	{	}
		}
		
		public void LoadRecords()
		{
			var crit = new Criteria<BOEnglish>();
			try
			{
				if(_searchBO.Key.HasValue) AddExpr(ref crit, "Key", _searchBO.Key, "" );
				if(!string.IsNullOrEmpty(_searchBO.Val)) AddExpr(ref crit, "Val", _searchBO.Val, _searchBO.Val);
				
				_boEnglishs.Clear();
				_boEnglishsCachedDelete.Clear();
				_boEnglishsCached = crit.List<BOEnglish>();
				foreach(BOEnglish boEnglish in _boEnglishsCached)
					_boEnglishs.Add(boEnglish);
			}
			catch	{	throw;	}
			finally	{	}
		}
		
		public void SaveCurrentView()
		{
			try
			{
				foreach(BOEnglish boEnglish in _boEnglishs)
				{
					if(_boEnglishsCached.Contains(boEnglish))
					{
						if(!boEnglish.IsDirty) continue;
						try{ boEnglish.Update(); }
						catch(Exception ex)
						{ throw new Exception("Error updating record: CodeTrigger has detected a data exception. Possible invalid foreign key reference, missing fields or other error. Exception details follow below.\r\n\r\n" + ex.Message); }
					}
					else
					{
						try{ boEnglish.SaveNew(); }
						catch(Exception ex)
						{ throw new Exception("Error saving new record: CodeTrigger has detected a data exception. Possible duplicate primary key, invalid foreign key reference, or missing fields. Exception details follow below.\r\n\r\n" + ex.Message); }
					}
				}
				foreach(BOEnglish boEnglish in _boEnglishsCachedDelete)
				{
					if((boEnglish == null)  || (!boEnglish.Key.HasValue))
					continue;
				
					try{ boEnglish.Delete(); }
					catch(Exception ex)
					{ throw new Exception("Error deleting record: CodeTrigger has detected a data exception. Possible existing foreign key reference or other error. Exception details follow below.\r\n\r\n" + ex.Message); }
				}
				_boEnglishsCachedDelete.Clear();
				
				LoadRecords();
			}
			catch	{	throw;	}
			finally	{	}
		}
		
		public void DeleteRow(object state)
		{
			if(state is BOEnglish)
			{
				_boEnglishs.Remove((BOEnglish)state);
				_boEnglishsCachedDelete.Add((BOEnglish)state);
			}
		}
		#endregion
		
		#region properties
		public BOEnglish Filter
		{
			get {	return _searchBO;	}
		}
		public ObservableCollection<BOEnglish> Results
		{
			get {	return _boEnglishs;	}
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
