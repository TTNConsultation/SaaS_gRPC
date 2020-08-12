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
	public partial class FrenchViewModel : IViewModelBase
	{
		/*WARNING - This class was generated by CodeTrigger. Changes to this file may cause incorrect behaviour and will be lost when the code is regenerated*/
		#region members
		private string _displayName = "French";
		private readonly BOFrench _searchBO = new BOFrench();
		private readonly ObservableCollection<BOFrench> _boFrenchs = new ObservableCollection<BOFrench>();
		private IList<BOFrench> _boFrenchsCached = new List<BOFrench>();
		private IList<BOFrench> _boFrenchsCachedDelete = new List<BOFrench>();
		private RelayCommand _deleteRowCommand;
		#endregion
		
		#region constructor
		public FrenchViewModel()
		{		}
		#endregion
		
		#region methods
		public static IList<BOFrench> GetFrenchList()
		{
			try
			{
				IList<BOFrench> listFrench = BOFrench.FrenchCollection();
				return listFrench;
			}
			catch(Exception)
			{	/*rethrow or handle gracefully*/return null;	}
			finally	{	}
		}
		
		private void AddExpr(ref Criteria<BOFrench> crit, string propertyName, object propertyValue, string propertyValueText, Func<object, string> formatter = null)
		{
			bool wildcard = propertyValueText.Contains("%");
			crit.Add(wildcard
				? Expression.Like(propertyName, propertyValue, formatter)
				: Expression.Eq(propertyName, propertyValue, formatter));
		}

		public int GetLoadCount()
		{
			var crit = new Criteria<BOFrench>();
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
			var crit = new Criteria<BOFrench>();
			try
			{
				if(_searchBO.Key.HasValue) AddExpr(ref crit, "Key", _searchBO.Key, "" );
				if(!string.IsNullOrEmpty(_searchBO.Val)) AddExpr(ref crit, "Val", _searchBO.Val, _searchBO.Val);
				
				_boFrenchs.Clear();
				_boFrenchsCachedDelete.Clear();
				_boFrenchsCached = crit.List<BOFrench>();
				foreach(BOFrench boFrench in _boFrenchsCached)
					_boFrenchs.Add(boFrench);
			}
			catch	{	throw;	}
			finally	{	}
		}
		
		public void SaveCurrentView()
		{
			try
			{
				foreach(BOFrench boFrench in _boFrenchs)
				{
					if(_boFrenchsCached.Contains(boFrench))
					{
						if(!boFrench.IsDirty) continue;
						try{ boFrench.Update(); }
						catch(Exception ex)
						{ throw new Exception("Error updating record: CodeTrigger has detected a data exception. Possible invalid foreign key reference, missing fields or other error. Exception details follow below.\r\n\r\n" + ex.Message); }
					}
					else
					{
						try{ boFrench.SaveNew(); }
						catch(Exception ex)
						{ throw new Exception("Error saving new record: CodeTrigger has detected a data exception. Possible duplicate primary key, invalid foreign key reference, or missing fields. Exception details follow below.\r\n\r\n" + ex.Message); }
					}
				}
				foreach(BOFrench boFrench in _boFrenchsCachedDelete)
				{
					if((boFrench == null)  || (!boFrench.Key.HasValue))
					continue;
				
					try{ boFrench.Delete(); }
					catch(Exception ex)
					{ throw new Exception("Error deleting record: CodeTrigger has detected a data exception. Possible existing foreign key reference or other error. Exception details follow below.\r\n\r\n" + ex.Message); }
				}
				_boFrenchsCachedDelete.Clear();
				
				LoadRecords();
			}
			catch	{	throw;	}
			finally	{	}
		}
		
		public void DeleteRow(object state)
		{
			if(state is BOFrench)
			{
				_boFrenchs.Remove((BOFrench)state);
				_boFrenchsCachedDelete.Add((BOFrench)state);
			}
		}
		#endregion
		
		#region properties
		public BOFrench Filter
		{
			get {	return _searchBO;	}
		}
		public ObservableCollection<BOFrench> Results
		{
			get {	return _boFrenchs;	}
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

