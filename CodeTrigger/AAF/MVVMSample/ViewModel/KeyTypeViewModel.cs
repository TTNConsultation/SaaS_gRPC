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
	public partial class KeyTypeViewModel : IViewModelBase
	{
		/*WARNING - This class was generated by CodeTrigger. Changes to this file may cause incorrect behaviour and will be lost when the code is regenerated*/
		#region members
		private string _displayName = "KeyType";
		private readonly BOKeyType _searchBO = new BOKeyType();
		private readonly ObservableCollection<BOKeyType> _boKeyTypes = new ObservableCollection<BOKeyType>();
		private IList<BOKeyType> _boKeyTypesCached = new List<BOKeyType>();
		private IList<BOKeyType> _boKeyTypesCachedDelete = new List<BOKeyType>();
		private RelayCommand _deleteRowCommand;
		#endregion
		
		#region constructor
		public KeyTypeViewModel()
		{		}
		#endregion
		
		#region methods
		public static IList<BOKeyType> GetKeyTypeList()
		{
			try
			{
				IList<BOKeyType> listKeyType = BOKeyType.KeyTypeCollection();
				return listKeyType;
			}
			catch(Exception)
			{	/*rethrow or handle gracefully*/return null;	}
			finally	{	}
		}
		
		private void AddExpr(ref Criteria<BOKeyType> crit, string propertyName, object propertyValue, string propertyValueText, Func<object, string> formatter = null)
		{
			bool wildcard = propertyValueText.Contains("%");
			crit.Add(wildcard
				? Expression.Like(propertyName, propertyValue, formatter)
				: Expression.Eq(propertyName, propertyValue, formatter));
		}

		public int GetLoadCount()
		{
			var crit = new Criteria<BOKeyType>();
			try
			{
				if(_searchBO.Id.HasValue) AddExpr(ref crit, "Id", _searchBO.Id, "" );
				if(!string.IsNullOrEmpty(_searchBO.Name)) AddExpr(ref crit, "Name", _searchBO.Name, _searchBO.Name);
				if(_searchBO.MaxLen.HasValue) AddExpr(ref crit, "MaxLen", _searchBO.MaxLen, "" );
				int resultCount = crit.Count();
				return resultCount;
			}
			catch	{	throw;	}
			finally	{	}
		}
		
		public void LoadRecords()
		{
			var crit = new Criteria<BOKeyType>();
			try
			{
				if(_searchBO.Id.HasValue) AddExpr(ref crit, "Id", _searchBO.Id, "" );
				if(!string.IsNullOrEmpty(_searchBO.Name)) AddExpr(ref crit, "Name", _searchBO.Name, _searchBO.Name);
				if(_searchBO.MaxLen.HasValue) AddExpr(ref crit, "MaxLen", _searchBO.MaxLen, "" );
				
				_boKeyTypes.Clear();
				_boKeyTypesCachedDelete.Clear();
				_boKeyTypesCached = crit.List<BOKeyType>();
				foreach(BOKeyType boKeyType in _boKeyTypesCached)
					_boKeyTypes.Add(boKeyType);
			}
			catch	{	throw;	}
			finally	{	}
		}
		
		public void SaveCurrentView()
		{
			try
			{
				foreach(BOKeyType boKeyType in _boKeyTypes)
				{
					if(_boKeyTypesCached.Contains(boKeyType))
					{
						if(!boKeyType.IsDirty) continue;
						try{ boKeyType.Update(); }
						catch(Exception ex)
						{ throw new Exception("Error updating record: CodeTrigger has detected a data exception. Possible invalid foreign key reference, missing fields or other error. Exception details follow below.\r\n\r\n" + ex.Message); }
					}
					else
					{
						try{ boKeyType.SaveNew(); }
						catch(Exception ex)
						{ throw new Exception("Error saving new record: CodeTrigger has detected a data exception. Possible duplicate primary key, invalid foreign key reference, or missing fields. Exception details follow below.\r\n\r\n" + ex.Message); }
					}
				}
				foreach(BOKeyType boKeyType in _boKeyTypesCachedDelete)
				{
					if((boKeyType == null)  || (!boKeyType.Id.HasValue))
					continue;
				
					try{ boKeyType.Delete(); }
					catch(Exception ex)
					{ throw new Exception("Error deleting record: CodeTrigger has detected a data exception. Possible existing foreign key reference or other error. Exception details follow below.\r\n\r\n" + ex.Message); }
				}
				_boKeyTypesCachedDelete.Clear();
				
				LoadRecords();
			}
			catch	{	throw;	}
			finally	{	}
		}
		
		public void DeleteRow(object state)
		{
			if(state is BOKeyType)
			{
				_boKeyTypes.Remove((BOKeyType)state);
				_boKeyTypesCachedDelete.Add((BOKeyType)state);
			}
		}
		#endregion
		
		#region properties
		public BOKeyType Filter
		{
			get {	return _searchBO;	}
		}
		public ObservableCollection<BOKeyType> Results
		{
			get {	return _boKeyTypes;	}
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
