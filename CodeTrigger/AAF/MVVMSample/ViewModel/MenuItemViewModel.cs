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
	public partial class MenuItemViewModel : IViewModelBase
	{
		/*WARNING - This class was generated by CodeTrigger. Changes to this file may cause incorrect behaviour and will be lost when the code is regenerated*/
		#region members
		private string _displayName = "MenuItem";
		private readonly BOMenuItem _searchBO = new BOMenuItem();
		private readonly ObservableCollection<BOMenuItem> _boMenuItems = new ObservableCollection<BOMenuItem>();
		private IList<BOMenuItem> _boMenuItemsCached = new List<BOMenuItem>();
		private IList<BOMenuItem> _boMenuItemsCachedDelete = new List<BOMenuItem>();
		private RelayCommand _deleteRowCommand;
		#endregion
		
		#region constructor
		public MenuItemViewModel()
		{		}
		#endregion
		
		#region methods
		public static IList<BOMenuItem> GetMenuItemList()
		{
			try
			{
				IList<BOMenuItem> listMenuItem = BOMenuItem.MenuItemCollection();
				return listMenuItem;
			}
			catch(Exception)
			{	/*rethrow or handle gracefully*/return null;	}
			finally	{	}
		}
		
		private void AddExpr(ref Criteria<BOMenuItem> crit, string propertyName, object propertyValue, string propertyValueText, Func<object, string> formatter = null)
		{
			bool wildcard = propertyValueText.Contains("%");
			crit.Add(wildcard
				? Expression.Like(propertyName, propertyValue, formatter)
				: Expression.Eq(propertyName, propertyValue, formatter));
		}

		public int GetLoadCount()
		{
			var crit = new Criteria<BOMenuItem>();
			try
			{
				if(_searchBO.Id.HasValue) AddExpr(ref crit, "Id", _searchBO.Id, "" );
				if(_searchBO.MenuId.HasValue) AddExpr(ref crit, "MenuId", _searchBO.MenuId, "" );
				if(_searchBO.ItemId.HasValue) AddExpr(ref crit, "ItemId", _searchBO.ItemId, "" );
				if(_searchBO.Quantity.HasValue) AddExpr(ref crit, "Quantity", _searchBO.Quantity, "" );
				if(_searchBO.StateId.HasValue) AddExpr(ref crit, "StateId", _searchBO.StateId, "" );
				int resultCount = crit.Count();
				return resultCount;
			}
			catch	{	throw;	}
			finally	{	}
		}
		
		public void LoadRecords()
		{
			var crit = new Criteria<BOMenuItem>();
			try
			{
				if(_searchBO.Id.HasValue) AddExpr(ref crit, "Id", _searchBO.Id, "" );
				if(_searchBO.MenuId.HasValue) AddExpr(ref crit, "MenuId", _searchBO.MenuId, "" );
				if(_searchBO.ItemId.HasValue) AddExpr(ref crit, "ItemId", _searchBO.ItemId, "" );
				if(_searchBO.Quantity.HasValue) AddExpr(ref crit, "Quantity", _searchBO.Quantity, "" );
				if(_searchBO.StateId.HasValue) AddExpr(ref crit, "StateId", _searchBO.StateId, "" );
				
				_boMenuItems.Clear();
				_boMenuItemsCachedDelete.Clear();
				_boMenuItemsCached = crit.List<BOMenuItem>();
				foreach(BOMenuItem boMenuItem in _boMenuItemsCached)
					_boMenuItems.Add(boMenuItem);
			}
			catch	{	throw;	}
			finally	{	}
		}
		
		public void SaveCurrentView()
		{
			try
			{
				foreach(BOMenuItem boMenuItem in _boMenuItems)
				{
					if(_boMenuItemsCached.Contains(boMenuItem))
					{
						if(!boMenuItem.IsDirty) continue;
						try{ boMenuItem.Update(); }
						catch(Exception ex)
						{ throw new Exception("Error updating record: CodeTrigger has detected a data exception. Possible invalid foreign key reference, missing fields or other error. Exception details follow below.\r\n\r\n" + ex.Message); }
					}
					else
					{
						try{ boMenuItem.SaveNew(); }
						catch(Exception ex)
						{ throw new Exception("Error saving new record: CodeTrigger has detected a data exception. Possible duplicate primary key, invalid foreign key reference, or missing fields. Exception details follow below.\r\n\r\n" + ex.Message); }
					}
				}
				foreach(BOMenuItem boMenuItem in _boMenuItemsCachedDelete)
				{
					if((boMenuItem == null)  || (!boMenuItem.Id.HasValue))
					continue;
				
					try{ boMenuItem.Delete(); }
					catch(Exception ex)
					{ throw new Exception("Error deleting record: CodeTrigger has detected a data exception. Possible existing foreign key reference or other error. Exception details follow below.\r\n\r\n" + ex.Message); }
				}
				_boMenuItemsCachedDelete.Clear();
				
				LoadRecords();
			}
			catch	{	throw;	}
			finally	{	}
		}
		
		public void DeleteRow(object state)
		{
			if(state is BOMenuItem)
			{
				_boMenuItems.Remove((BOMenuItem)state);
				_boMenuItemsCachedDelete.Add((BOMenuItem)state);
			}
		}
		#endregion
		
		#region properties
		public BOMenuItem Filter
		{
			get {	return _searchBO;	}
		}
		public ObservableCollection<BOMenuItem> Results
		{
			get {	return _boMenuItems;	}
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

