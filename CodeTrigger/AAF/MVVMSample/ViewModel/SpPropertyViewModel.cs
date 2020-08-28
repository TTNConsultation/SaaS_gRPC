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
	public partial class SpPropertyViewModel : IViewModelBase
	{
		/*WARNING - This class was generated by CodeTrigger. Changes to this file may cause incorrect behaviour and will be lost when the code is regenerated*/
		#region members
		private string _displayName = "SpProperty";
		private readonly BOSpProperty _searchBO = new BOSpProperty();
		private readonly ObservableCollection<BOSpProperty> _boSpPropertys = new ObservableCollection<BOSpProperty>();
		private IList<BOSpProperty> _boSpPropertysCached = new List<BOSpProperty>();
		private IList<BOSpProperty> _boSpPropertysCachedDelete = new List<BOSpProperty>();
		private RelayCommand _deleteRowCommand;
		#endregion
		
		#region constructor
		public SpPropertyViewModel()
		{		}
		#endregion
		
		#region methods
		public static IList<BOSpProperty> GetSpPropertyList()
		{
			try
			{
				IList<BOSpProperty> listSpProperty = BOSpProperty.SpPropertyCollection();
				return listSpProperty;
			}
			catch(Exception)
			{	/*rethrow or handle gracefully*/return null;	}
			finally	{	}
		}
		
		private void AddExpr(ref Criteria<BOSpProperty> crit, string propertyName, object propertyValue, string propertyValueText, Func<object, string> formatter = null)
		{
			bool wildcard = propertyValueText.Contains("%");
			crit.Add(wildcard
				? Expression.Like(propertyName, propertyValue, formatter)
				: Expression.Eq(propertyName, propertyValue, formatter));
		}

		public int GetLoadCount()
		{
			var crit = new Criteria<BOSpProperty>();
			try
			{
				if(_searchBO.Id.HasValue) AddExpr(ref crit, "Id", _searchBO.Id, "" );
				if(!string.IsNullOrEmpty(_searchBO.FullName)) AddExpr(ref crit, "FullName", _searchBO.FullName, _searchBO.FullName);
				if(!string.IsNullOrEmpty(_searchBO.Schema)) AddExpr(ref crit, "Schema", _searchBO.Schema, _searchBO.Schema);
				if(!string.IsNullOrEmpty(_searchBO.Type)) AddExpr(ref crit, "Type", _searchBO.Type, _searchBO.Type);
				if(!string.IsNullOrEmpty(_searchBO.Op)) AddExpr(ref crit, "Op", _searchBO.Op, _searchBO.Op);
				int resultCount = crit.Count();
				return resultCount;
			}
			catch	{	throw;	}
			finally	{	}
		}
		
		public void LoadRecords()
		{
			var crit = new Criteria<BOSpProperty>();
			try
			{
				if(_searchBO.Id.HasValue) AddExpr(ref crit, "Id", _searchBO.Id, "" );
				if(!string.IsNullOrEmpty(_searchBO.FullName)) AddExpr(ref crit, "FullName", _searchBO.FullName, _searchBO.FullName);
				if(!string.IsNullOrEmpty(_searchBO.Schema)) AddExpr(ref crit, "Schema", _searchBO.Schema, _searchBO.Schema);
				if(!string.IsNullOrEmpty(_searchBO.Type)) AddExpr(ref crit, "Type", _searchBO.Type, _searchBO.Type);
				if(!string.IsNullOrEmpty(_searchBO.Op)) AddExpr(ref crit, "Op", _searchBO.Op, _searchBO.Op);
				
				_boSpPropertys.Clear();
				_boSpPropertysCachedDelete.Clear();
				_boSpPropertysCached = crit.List<BOSpProperty>();
				foreach(BOSpProperty boSpProperty in _boSpPropertysCached)
					_boSpPropertys.Add(boSpProperty);
			}
			catch	{	throw;	}
			finally	{	}
		}
		
		public void SaveCurrentView()
		{
			try
			{
				foreach(BOSpProperty boSpProperty in _boSpPropertys)
				{
					if(_boSpPropertysCached.Contains(boSpProperty))
					{
						if(!boSpProperty.IsDirty) continue;
						/*cannot update as there is no primary key defined or is a view
						try{ boSpProperty.Update(); }
						catch(Exception ex)
						{ throw new Exception("Error updating record: CodeTrigger has detected a data exception. Possible invalid foreign key reference, missing fields or other error. Exception details follow below.\r\n\r\n" + ex.Message); }
						*/
					}
					else
					{
						/*cannot save as there is no primary key defined or is a view
						try{ boSpProperty.SaveNew(); }
						catch(Exception ex)
						{ throw new Exception("Error saving new record: CodeTrigger has detected a data exception. Possible duplicate primary key, invalid foreign key reference, or missing fields. Exception details follow below.\r\n\r\n" + ex.Message); }
						*/
					}
				}
				/*cannot delete as there is no primary key defined
				foreach(BOSpProperty boSpProperty in _boSpPropertysCachedDelete)
				{
					if((boSpProperty == null) )
					continue;
				
					try{ boSpProperty.Delete(); }
					catch(Exception ex)
					{ throw new Exception("Error deleting record: CodeTrigger has detected a data exception. Possible existing foreign key reference or other error. Exception details follow below.\r\n\r\n" + ex.Message); }
				}
				_boSpPropertysCachedDelete.Clear();
				*/
				
				LoadRecords();
			}
			catch	{	throw;	}
			finally	{	}
		}
		
		public void DeleteRow(object state)
		{
			if(state is BOSpProperty)
			{
				_boSpPropertys.Remove((BOSpProperty)state);
				_boSpPropertysCachedDelete.Add((BOSpProperty)state);
			}
		}
		#endregion
		
		#region properties
		public BOSpProperty Filter
		{
			get {	return _searchBO;	}
		}
		public ObservableCollection<BOSpProperty> Results
		{
			get {	return _boSpPropertys;	}
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
