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
	public partial class TableViewModel : IViewModelBase
	{
		/*WARNING - This class was generated by CodeTrigger. Changes to this file may cause incorrect behaviour and will be lost when the code is regenerated*/
		#region members
		private string _displayName = "Table";
		private readonly BOTable _searchBO = new BOTable();
		private readonly ObservableCollection<BOTable> _boTables = new ObservableCollection<BOTable>();
		private IList<BOTable> _boTablesCached = new List<BOTable>();
		private IList<BOTable> _boTablesCachedDelete = new List<BOTable>();
		private RelayCommand _deleteRowCommand;
		#endregion
		
		#region constructor
		public TableViewModel()
		{		}
		#endregion
		
		#region methods
		public static IList<BOTable> GetTableList()
		{
			try
			{
				IList<BOTable> listTable = BOTable.TableCollection();
				return listTable;
			}
			catch(Exception)
			{	/*rethrow or handle gracefully*/return null;	}
			finally	{	}
		}
		
		private void AddExpr(ref Criteria<BOTable> crit, string propertyName, object propertyValue, string propertyValueText, Func<object, string> formatter = null)
		{
			bool wildcard = propertyValueText.Contains("%");
			crit.Add(wildcard
				? Expression.Like(propertyName, propertyValue, formatter)
				: Expression.Eq(propertyName, propertyValue, formatter));
		}

		public int GetLoadCount()
		{
			var crit = new Criteria<BOTable>();
			try
			{
				if(_searchBO.Id.HasValue) AddExpr(ref crit, "Id", _searchBO.Id, "" );
				if(_searchBO.RestaurantId.HasValue) AddExpr(ref crit, "RestaurantId", _searchBO.RestaurantId, "" );
				if(_searchBO.NumberOfSeat.HasValue) AddExpr(ref crit, "NumberOfSeat", _searchBO.NumberOfSeat, "" );
				if(_searchBO.StateId.HasValue) AddExpr(ref crit, "StateId", _searchBO.StateId, "" );
				int resultCount = crit.Count();
				return resultCount;
			}
			catch	{	throw;	}
			finally	{	}
		}
		
		public void LoadRecords()
		{
			var crit = new Criteria<BOTable>();
			try
			{
				if(_searchBO.Id.HasValue) AddExpr(ref crit, "Id", _searchBO.Id, "" );
				if(_searchBO.RestaurantId.HasValue) AddExpr(ref crit, "RestaurantId", _searchBO.RestaurantId, "" );
				if(_searchBO.NumberOfSeat.HasValue) AddExpr(ref crit, "NumberOfSeat", _searchBO.NumberOfSeat, "" );
				if(_searchBO.StateId.HasValue) AddExpr(ref crit, "StateId", _searchBO.StateId, "" );
				
				_boTables.Clear();
				_boTablesCachedDelete.Clear();
				_boTablesCached = crit.List<BOTable>();
				foreach(BOTable boTable in _boTablesCached)
					_boTables.Add(boTable);
			}
			catch	{	throw;	}
			finally	{	}
		}
		
		public void SaveCurrentView()
		{
			try
			{
				foreach(BOTable boTable in _boTables)
				{
					if(_boTablesCached.Contains(boTable))
					{
						if(!boTable.IsDirty) continue;
						try{ boTable.Update(); }
						catch(Exception ex)
						{ throw new Exception("Error updating record: CodeTrigger has detected a data exception. Possible invalid foreign key reference, missing fields or other error. Exception details follow below.\r\n\r\n" + ex.Message); }
					}
					else
					{
						try{ boTable.SaveNew(); }
						catch(Exception ex)
						{ throw new Exception("Error saving new record: CodeTrigger has detected a data exception. Possible duplicate primary key, invalid foreign key reference, or missing fields. Exception details follow below.\r\n\r\n" + ex.Message); }
					}
				}
				foreach(BOTable boTable in _boTablesCachedDelete)
				{
					if((boTable == null)  || (!boTable.Id.HasValue))
					continue;
				
					try{ boTable.Delete(); }
					catch(Exception ex)
					{ throw new Exception("Error deleting record: CodeTrigger has detected a data exception. Possible existing foreign key reference or other error. Exception details follow below.\r\n\r\n" + ex.Message); }
				}
				_boTablesCachedDelete.Clear();
				
				LoadRecords();
			}
			catch	{	throw;	}
			finally	{	}
		}
		
		public void DeleteRow(object state)
		{
			if(state is BOTable)
			{
				_boTables.Remove((BOTable)state);
				_boTablesCachedDelete.Add((BOTable)state);
			}
		}
		#endregion
		
		#region properties
		public BOTable Filter
		{
			get {	return _searchBO;	}
		}
		public ObservableCollection<BOTable> Results
		{
			get {	return _boTables;	}
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
