/*************************************************************
** Class generated by CodeTrigger, Version 6.3.0.4
** This class was generated on 2020-08-01 3:54:27 AM
** Changes to this file may cause incorrect behaviour and will be lost if the code is regenerated
**************************************************************/
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using AAF.DataObjects;
using AAF.DataObjects.Interfaces;
using AAF.BusinessObjects.Interfaces;

namespace AAF.BusinessObjects
{
	///<Summary>
	///Class definition
	///This is the definition of the class BORestaurant.
	///It maintains a collection of BOItem,BOSchedule,BORestaurantLanguage,BORestaurantMenu,BOTable objects.
	///</Summary>
	public partial class BORestaurant : zAAFConn_BaseBusiness, IQueryableCollection
	{
		#region member variables
		protected Int32? _id;
		protected string _name;
		protected byte? _status;
		protected bool _isDirty = false;
		/*collection member objects*******************/
		List<BOItem> _boItemCollection;
		List<BOSchedule> _boScheduleCollection;
		List<BORestaurantLanguage> _boRestaurantLanguageCollection;
		List<BORestaurantMenu> _boRestaurantMenuCollection;
		List<BOTable> _boTableCollection;
		/*********************************************/
		#endregion

		#region class methods
		///<Summary>
		///Constructor
		///This is the default constructor
		///</Summary>
		///<returns>
		///void
		///</returns>
		///<parameters>
		///
		///</parameters>
		public BORestaurant()
		{
		}

		///<Summary>
		///Constructor
		///Constructor using primary key(s)
		///</Summary>
		///<returns>
		///void
		///</returns>
		///<parameters>
		///Int32 id
		///</parameters>
		public BORestaurant(Int32 id)
		{
			try
			{
				DAORestaurant daoRestaurant = DAORestaurant.SelectOne(id);
				_id = daoRestaurant.Id;
				_name = daoRestaurant.Name;
				_status = daoRestaurant.Status;
			}
			catch
			{
				throw;
			}
		}

		///<Summary>
		///Constructor
		///This constructor initializes the business object from its respective data object
		///</Summary>
		///<returns>
		///void
		///</returns>
		///<parameters>
		///DAORestaurant
		///</parameters>
		protected internal BORestaurant(DAORestaurant daoRestaurant)
		{
			try
			{
				_id = daoRestaurant.Id;
				_name = daoRestaurant.Name;
				_status = daoRestaurant.Status;
			}
			catch
			{
				throw;
			}
		}

		///<Summary>
		///SaveNew
		///This method persists a new Restaurant record to the store
		///</Summary>
		///<returns>
		///void
		///</returns>
		///<parameters>
		///
		///</parameters>
		public virtual void SaveNew()
		{
			DAORestaurant daoRestaurant = new DAORestaurant();
			RegisterDataObject(daoRestaurant);
			BeginTransaction("savenewBORestaurant");
			try
			{
				daoRestaurant.Name = _name;
				daoRestaurant.Status = _status;
				daoRestaurant.Insert();
				CommitTransaction();
				
				_id = daoRestaurant.Id;
				_name = daoRestaurant.Name;
				_status = daoRestaurant.Status;
				_isDirty = false;
			}
			catch(Exception ex)
			{
				RollbackTransaction("savenewBORestaurant");
				Handle(this, ex);
			}
		}
		
		///<Summary>
		///Update
		///This method updates one Restaurant record in the store
		///</Summary>
		///<returns>
		///void
		///</returns>
		///<parameters>
		///BORestaurant
		///</parameters>
		public virtual void Update()
		{
			DAORestaurant daoRestaurant = new DAORestaurant();
			RegisterDataObject(daoRestaurant);
			BeginTransaction("updateBORestaurant");
			try
			{
				daoRestaurant.Id = _id;
				daoRestaurant.Name = _name;
				daoRestaurant.Status = _status;
				daoRestaurant.Update();
				CommitTransaction();
				
				_id = daoRestaurant.Id;
				_name = daoRestaurant.Name;
				_status = daoRestaurant.Status;
				_isDirty = false;
			}
			catch(Exception ex)
			{
				RollbackTransaction("updateBORestaurant");
				Handle(this, ex);
			}
		}
		///<Summary>
		///Delete
		///This method deletes one Restaurant record from the store
		///</Summary>
		///<returns>
		///void
		///</returns>
		///<parameters>
		///
		///</parameters>
		public virtual void Delete()
		{
			DAORestaurant daoRestaurant = new DAORestaurant();
			RegisterDataObject(daoRestaurant);
			BeginTransaction("deleteBORestaurant");
			try
			{
				daoRestaurant.Id = _id;
				daoRestaurant.Delete();
				CommitTransaction();
			}
			catch(Exception ex)
			{
				RollbackTransaction("deleteBORestaurant");
				Handle(this, ex);
			}
		}
		
		///<Summary>
		///RestaurantCollection
		///This method returns the collection of BORestaurant objects
		///</Summary>
		///<returns>
		///List[BORestaurant]
		///</returns>
		///<parameters>
		///
		///</parameters>
		public static IList<BORestaurant> RestaurantCollection()
		{
			try
			{
				IList<BORestaurant> boRestaurantCollection = new List<BORestaurant>();
				IList<DAORestaurant> daoRestaurantCollection = DAORestaurant.SelectAll();
			
				foreach(DAORestaurant daoRestaurant in daoRestaurantCollection)
					boRestaurantCollection.Add(new BORestaurant(daoRestaurant));
			
				return boRestaurantCollection;
			}
			catch(Exception ex)
			{
				Handle(null, ex);
				return null;
			}
		}
		
		
		///<Summary>
		///RestaurantCollectionCount
		///This method returns the collection count of BORestaurant objects
		///</Summary>
		///<returns>
		///Int32
		///</returns>
		///<parameters>
		///
		///</parameters>
		public static Int32 RestaurantCollectionCount()
		{
			try
			{
				Int32 objCount = DAORestaurant.SelectAllCount();
				return objCount;
			}
			catch(Exception ex)
			{
				Handle(null, ex);
				return -1;
			}
		}
		
		///<Summary>
		///ItemCollection
		///This method returns its collection of BOItem objects
		///</Summary>
		///<returns>
		///IList[BOItem]
		///</returns>
		///<parameters>
		///BORestaurant
		///</parameters>
		public virtual IList<BOItem> ItemCollection()
		{
			try
			{
				if(_boItemCollection == null)
					LoadItemCollection();
				
				return _boItemCollection.AsReadOnly();
			}
			catch(Exception ex)
			{
				Handle(this, ex);
				return null;
			}
		}
		
		///<Summary>
		///ScheduleCollection
		///This method returns its collection of BOSchedule objects
		///</Summary>
		///<returns>
		///IList[BOSchedule]
		///</returns>
		///<parameters>
		///BORestaurant
		///</parameters>
		public virtual IList<BOSchedule> ScheduleCollection()
		{
			try
			{
				if(_boScheduleCollection == null)
					LoadScheduleCollection();
				
				return _boScheduleCollection.AsReadOnly();
			}
			catch(Exception ex)
			{
				Handle(this, ex);
				return null;
			}
		}
		
		///<Summary>
		///RestaurantLanguageCollection
		///This method returns its collection of BORestaurantLanguage objects
		///</Summary>
		///<returns>
		///IList[BORestaurantLanguage]
		///</returns>
		///<parameters>
		///BORestaurant
		///</parameters>
		public virtual IList<BORestaurantLanguage> RestaurantLanguageCollection()
		{
			try
			{
				if(_boRestaurantLanguageCollection == null)
					LoadRestaurantLanguageCollection();
				
				return _boRestaurantLanguageCollection.AsReadOnly();
			}
			catch(Exception ex)
			{
				Handle(this, ex);
				return null;
			}
		}
		
		///<Summary>
		///RestaurantMenuCollection
		///This method returns its collection of BORestaurantMenu objects
		///</Summary>
		///<returns>
		///IList[BORestaurantMenu]
		///</returns>
		///<parameters>
		///BORestaurant
		///</parameters>
		public virtual IList<BORestaurantMenu> RestaurantMenuCollection()
		{
			try
			{
				if(_boRestaurantMenuCollection == null)
					LoadRestaurantMenuCollection();
				
				return _boRestaurantMenuCollection.AsReadOnly();
			}
			catch(Exception ex)
			{
				Handle(this, ex);
				return null;
			}
		}
		
		///<Summary>
		///TableCollection
		///This method returns its collection of BOTable objects
		///</Summary>
		///<returns>
		///IList[BOTable]
		///</returns>
		///<parameters>
		///BORestaurant
		///</parameters>
		public virtual IList<BOTable> TableCollection()
		{
			try
			{
				if(_boTableCollection == null)
					LoadTableCollection();
				
				return _boTableCollection.AsReadOnly();
			}
			catch(Exception ex)
			{
				Handle(this, ex);
				return null;
			}
		}
		
		
		///<Summary>
		///Projections
		///This method returns the collection of projections, ordered and filtered by optional criteria
		///</Summary>
		///<returns>
		///List<BORestaurant>
		///</returns>
		///<parameters>
		///ICriteria icriteria
		///</parameters>
		public virtual IDictionary<string, IList<object>> Projections(object o)
		{
			try
			{
				ICriteria icriteria = (ICriteria)o;
				IList<IDataProjection> lstDataProjection = (icriteria == null) ? null : icriteria.ListDataProjection();
				IList<IDataCriterion> lstDataCriteria = (icriteria == null) ? null : icriteria.ListDataCriteria();
				IList<IDataOrderBy> lstDataOrder = (icriteria == null) ? null : icriteria.ListDataOrder();
				IDataTake dataTake = (icriteria == null) ? null : icriteria.DataTake();
				IDataSkip dataSkip = (icriteria == null) ? null : icriteria.DataSkip();
				IDictionary<string, IList<object>> retObj = DAORestaurant.SelectAllByCriteriaProjection(lstDataProjection, lstDataCriteria, lstDataOrder, dataSkip, dataTake);
				return retObj;
			}
			catch(Exception ex)
			{
				Handle(this, ex);
				return null;
			}
		}
		
		
		///<Summary>
		///RestaurantCollection<T>
		///This method implements the IQueryable Collection<T> method, returning a collection of BORestaurant objects, filtered by optional criteria
		///</Summary>
		///<returns>
		///IList<T>
		///</returns>
		///<parameters>
		///object o
		///</parameters>
		public virtual IList<T> Collection<T>(object o)
		{
			try
			{
				ICriteria icriteria = (ICriteria)o;
				IList<T> boRestaurantCollection = new List<T>();
				IList<IDataCriterion> lstDataCriteria = (icriteria == null) ? null : icriteria.ListDataCriteria();
				IList<IDataOrderBy> lstDataOrder = (icriteria == null) ? null : icriteria.ListDataOrder();
				IDataTake dataTake = (icriteria == null) ? null : icriteria.DataTake();
				IDataSkip dataSkip = (icriteria == null) ? null : icriteria.DataSkip();
				IList<DAORestaurant> daoRestaurantCollection = DAORestaurant.SelectAllByCriteria(lstDataCriteria, lstDataOrder, dataSkip, dataTake);
			
				foreach(DAORestaurant resdaoRestaurant in daoRestaurantCollection)
					boRestaurantCollection.Add((T)(object)new BORestaurant(resdaoRestaurant));
			
				return boRestaurantCollection;
			}
			catch(Exception ex)
			{
				Handle(this, ex);
				return null;
			}
		}
		
		
		///<Summary>
		///RestaurantCollectionCount
		///This method implements the IQueryable CollectionCount method, returning a collection count of BORestaurant objects, filtered by optional criteria
		///</Summary>
		///<returns>
		///Int32
		///</returns>
		///<parameters>
		///object o
		///</parameters>
		public virtual Int32 CollectionCount(object o)
		{
			try
			{
				ICriteria icriteria = (ICriteria)o;
				IList<IDataCriterion> lstDataCriteria = (icriteria == null) ? null : icriteria.ListDataCriteria();
				Int32 objCount = DAORestaurant.SelectAllByCriteriaCount(lstDataCriteria);
				return objCount;
			}
			catch(Exception ex)
			{
				Handle(this, ex);
				return -1;
			}
		}
		
		///<Summary>
		///LoadItemCollection
		///This method loads the internal collection of BOItem objects from storage. 
		///Call this if you need to ensure the collection is up-to-date (concurrency)
		///</Summary>
		///<returns>
		///void
		///</returns>
		///<parameters>
		///
		///</parameters>
		public virtual void LoadItemCollection()
		{
			try
			{
				_boItemCollection = new List<BOItem>();
				IList<DAOItem> daoItemCollection = DAOItem.SelectAllByRestaurantId(_id.Value);
				
				foreach(DAOItem daoItem in daoItemCollection)
					_boItemCollection.Add(new BOItem(daoItem));
			}
			catch(Exception ex)
			{
				Handle(this, ex);
			}
		}
		
		///<Summary>
		///AddItem
		///This method persists a BOItem object to the database collection
		///</Summary>
		///<returns>
		///void
		///</returns>
		///<parameters>
		///BOItem
		///</parameters>
		public virtual void AddItem(BOItem boItem)
		{
			DAOItem daoItem = new DAOItem();
			RegisterDataObject(daoItem);
			BeginTransaction("addItem");
			try
			{
				daoItem.Id = boItem.Id;
				daoItem.Price = boItem.Price;
				daoItem.IsExtra = boItem.IsExtra;
				daoItem.StateId = boItem.StateId;
				daoItem.NameKey = boItem.NameKey;
				daoItem.DescriptionKey = boItem.DescriptionKey;
				daoItem.RestaurantId = _id.Value;
				daoItem.Insert();
				CommitTransaction();
				
				/*pick up any primary keys, computed values etc*/
				boItem = new BOItem(daoItem);
				if(_boItemCollection != null)
					_boItemCollection.Add(boItem);
			}
			catch(Exception ex)
			{
				RollbackTransaction("addItem");
				Handle(this, ex);
			}
		}
		
		///<Summary>
		///DeleteAllItem
		///This method deletes all BOItem objects from its collection
		///</Summary>
		///<returns>
		///void
		///</returns>
		///<parameters>
		///
		///</parameters>
		public virtual void DeleteAllItem()
		{
			RegisterDataObject(null);
			BeginTransaction("deleteAllItem");
			try
			{
				DAOItem.DeleteAllByRestaurantId(ConnectionProvider, _id.Value);
				CommitTransaction();
				if(_boItemCollection != null)
				{
					_boItemCollection.Clear();
					_boItemCollection = null;
				}
			}
			catch(Exception ex)
			{
				RollbackTransaction("deleteAllItem");
				Handle(this, ex);
			}
		}
		
		///<Summary>
		///LoadScheduleCollection
		///This method loads the internal collection of BOSchedule objects from storage. 
		///Call this if you need to ensure the collection is up-to-date (concurrency)
		///</Summary>
		///<returns>
		///void
		///</returns>
		///<parameters>
		///
		///</parameters>
		public virtual void LoadScheduleCollection()
		{
			try
			{
				_boScheduleCollection = new List<BOSchedule>();
				IList<DAOSchedule> daoScheduleCollection = DAOSchedule.SelectAllByRestaurantId(_id.Value);
				
				foreach(DAOSchedule daoSchedule in daoScheduleCollection)
					_boScheduleCollection.Add(new BOSchedule(daoSchedule));
			}
			catch(Exception ex)
			{
				Handle(this, ex);
			}
		}
		
		///<Summary>
		///AddSchedule
		///This method persists a BOSchedule object to the database collection
		///</Summary>
		///<returns>
		///void
		///</returns>
		///<parameters>
		///BOSchedule
		///</parameters>
		public virtual void AddSchedule(BOSchedule boSchedule)
		{
			DAOSchedule daoSchedule = new DAOSchedule();
			RegisterDataObject(daoSchedule);
			BeginTransaction("addSchedule");
			try
			{
				daoSchedule.MondayStart = boSchedule.MondayStart;
				daoSchedule.MondayEnd = boSchedule.MondayEnd;
				daoSchedule.TuesdayStart = boSchedule.TuesdayStart;
				daoSchedule.TuesdayEnd = boSchedule.TuesdayEnd;
				daoSchedule.WednesdayStart = boSchedule.WednesdayStart;
				daoSchedule.WednesdayEnd = boSchedule.WednesdayEnd;
				daoSchedule.ThursdayStart = boSchedule.ThursdayStart;
				daoSchedule.ThursdayEnd = boSchedule.ThursdayEnd;
				daoSchedule.FridayStart = boSchedule.FridayStart;
				daoSchedule.FridayEnd = boSchedule.FridayEnd;
				daoSchedule.SaturdayStart = boSchedule.SaturdayStart;
				daoSchedule.SaturdayEnd = boSchedule.SaturdayEnd;
				daoSchedule.SundayStart = boSchedule.SundayStart;
				daoSchedule.SundayEnd = boSchedule.SundayEnd;
				daoSchedule.RestaurantId = _id.Value;
				daoSchedule.Insert();
				CommitTransaction();
				
				/*pick up any primary keys, computed values etc*/
				boSchedule = new BOSchedule(daoSchedule);
				if(_boScheduleCollection != null)
					_boScheduleCollection.Add(boSchedule);
			}
			catch(Exception ex)
			{
				RollbackTransaction("addSchedule");
				Handle(this, ex);
			}
		}
		
		///<Summary>
		///DeleteAllSchedule
		///This method deletes all BOSchedule objects from its collection
		///</Summary>
		///<returns>
		///void
		///</returns>
		///<parameters>
		///
		///</parameters>
		public virtual void DeleteAllSchedule()
		{
			RegisterDataObject(null);
			BeginTransaction("deleteAllSchedule");
			try
			{
				DAOSchedule.DeleteAllByRestaurantId(ConnectionProvider, _id.Value);
				CommitTransaction();
				if(_boScheduleCollection != null)
				{
					_boScheduleCollection.Clear();
					_boScheduleCollection = null;
				}
			}
			catch(Exception ex)
			{
				RollbackTransaction("deleteAllSchedule");
				Handle(this, ex);
			}
		}
		
		///<Summary>
		///LoadRestaurantLanguageCollection
		///This method loads the internal collection of BORestaurantLanguage objects from storage. 
		///Call this if you need to ensure the collection is up-to-date (concurrency)
		///</Summary>
		///<returns>
		///void
		///</returns>
		///<parameters>
		///
		///</parameters>
		public virtual void LoadRestaurantLanguageCollection()
		{
			try
			{
				_boRestaurantLanguageCollection = new List<BORestaurantLanguage>();
				IList<DAORestaurantLanguage> daoRestaurantLanguageCollection = DAORestaurantLanguage.SelectAllByRestaurantId(_id.Value);
				
				foreach(DAORestaurantLanguage daoRestaurantLanguage in daoRestaurantLanguageCollection)
					_boRestaurantLanguageCollection.Add(new BORestaurantLanguage(daoRestaurantLanguage));
			}
			catch(Exception ex)
			{
				Handle(this, ex);
			}
		}
		
		///<Summary>
		///AddRestaurantLanguage
		///This method persists a BORestaurantLanguage object to the database collection
		///</Summary>
		///<returns>
		///void
		///</returns>
		///<parameters>
		///BORestaurantLanguage
		///</parameters>
		public virtual void AddRestaurantLanguage(BORestaurantLanguage boRestaurantLanguage)
		{
			DAORestaurantLanguage daoRestaurantLanguage = new DAORestaurantLanguage();
			RegisterDataObject(daoRestaurantLanguage);
			BeginTransaction("addRestaurantLanguage");
			try
			{
				daoRestaurantLanguage.Id = boRestaurantLanguage.Id;
				daoRestaurantLanguage.LanguageId = boRestaurantLanguage.LanguageId;
				daoRestaurantLanguage.State = boRestaurantLanguage.State;
				daoRestaurantLanguage.RestaurantId = _id.Value;
				daoRestaurantLanguage.Insert();
				CommitTransaction();
				
				/*pick up any primary keys, computed values etc*/
				boRestaurantLanguage = new BORestaurantLanguage(daoRestaurantLanguage);
				if(_boRestaurantLanguageCollection != null)
					_boRestaurantLanguageCollection.Add(boRestaurantLanguage);
			}
			catch(Exception ex)
			{
				RollbackTransaction("addRestaurantLanguage");
				Handle(this, ex);
			}
		}
		
		///<Summary>
		///DeleteAllRestaurantLanguage
		///This method deletes all BORestaurantLanguage objects from its collection
		///</Summary>
		///<returns>
		///void
		///</returns>
		///<parameters>
		///
		///</parameters>
		public virtual void DeleteAllRestaurantLanguage()
		{
			RegisterDataObject(null);
			BeginTransaction("deleteAllRestaurantLanguage");
			try
			{
				DAORestaurantLanguage.DeleteAllByRestaurantId(ConnectionProvider, _id.Value);
				CommitTransaction();
				if(_boRestaurantLanguageCollection != null)
				{
					_boRestaurantLanguageCollection.Clear();
					_boRestaurantLanguageCollection = null;
				}
			}
			catch(Exception ex)
			{
				RollbackTransaction("deleteAllRestaurantLanguage");
				Handle(this, ex);
			}
		}
		
		///<Summary>
		///LoadRestaurantMenuCollection
		///This method loads the internal collection of BORestaurantMenu objects from storage. 
		///Call this if you need to ensure the collection is up-to-date (concurrency)
		///</Summary>
		///<returns>
		///void
		///</returns>
		///<parameters>
		///
		///</parameters>
		public virtual void LoadRestaurantMenuCollection()
		{
			try
			{
				_boRestaurantMenuCollection = new List<BORestaurantMenu>();
				IList<DAORestaurantMenu> daoRestaurantMenuCollection = DAORestaurantMenu.SelectAllByRestaurantId(_id.Value);
				
				foreach(DAORestaurantMenu daoRestaurantMenu in daoRestaurantMenuCollection)
					_boRestaurantMenuCollection.Add(new BORestaurantMenu(daoRestaurantMenu));
			}
			catch(Exception ex)
			{
				Handle(this, ex);
			}
		}
		
		///<Summary>
		///AddRestaurantMenu
		///This method persists a BORestaurantMenu object to the database collection
		///</Summary>
		///<returns>
		///void
		///</returns>
		///<parameters>
		///BORestaurantMenu
		///</parameters>
		public virtual void AddRestaurantMenu(BORestaurantMenu boRestaurantMenu)
		{
			DAORestaurantMenu daoRestaurantMenu = new DAORestaurantMenu();
			RegisterDataObject(daoRestaurantMenu);
			BeginTransaction("addRestaurantMenu");
			try
			{
				daoRestaurantMenu.Id = boRestaurantMenu.Id;
				daoRestaurantMenu.StateId = boRestaurantMenu.StateId;
				daoRestaurantMenu.RestaurantId = _id.Value;
				daoRestaurantMenu.Insert();
				CommitTransaction();
				
				/*pick up any primary keys, computed values etc*/
				boRestaurantMenu = new BORestaurantMenu(daoRestaurantMenu);
				if(_boRestaurantMenuCollection != null)
					_boRestaurantMenuCollection.Add(boRestaurantMenu);
			}
			catch(Exception ex)
			{
				RollbackTransaction("addRestaurantMenu");
				Handle(this, ex);
			}
		}
		
		///<Summary>
		///DeleteAllRestaurantMenu
		///This method deletes all BORestaurantMenu objects from its collection
		///</Summary>
		///<returns>
		///void
		///</returns>
		///<parameters>
		///
		///</parameters>
		public virtual void DeleteAllRestaurantMenu()
		{
			RegisterDataObject(null);
			BeginTransaction("deleteAllRestaurantMenu");
			try
			{
				DAORestaurantMenu.DeleteAllByRestaurantId(ConnectionProvider, _id.Value);
				CommitTransaction();
				if(_boRestaurantMenuCollection != null)
				{
					_boRestaurantMenuCollection.Clear();
					_boRestaurantMenuCollection = null;
				}
			}
			catch(Exception ex)
			{
				RollbackTransaction("deleteAllRestaurantMenu");
				Handle(this, ex);
			}
		}
		
		///<Summary>
		///LoadTableCollection
		///This method loads the internal collection of BOTable objects from storage. 
		///Call this if you need to ensure the collection is up-to-date (concurrency)
		///</Summary>
		///<returns>
		///void
		///</returns>
		///<parameters>
		///
		///</parameters>
		public virtual void LoadTableCollection()
		{
			try
			{
				_boTableCollection = new List<BOTable>();
				IList<DAOTable> daoTableCollection = DAOTable.SelectAllByRestaurantId(_id.Value);
				
				foreach(DAOTable daoTable in daoTableCollection)
					_boTableCollection.Add(new BOTable(daoTable));
			}
			catch(Exception ex)
			{
				Handle(this, ex);
			}
		}
		
		///<Summary>
		///AddTable
		///This method persists a BOTable object to the database collection
		///</Summary>
		///<returns>
		///void
		///</returns>
		///<parameters>
		///BOTable
		///</parameters>
		public virtual void AddTable(BOTable boTable)
		{
			DAOTable daoTable = new DAOTable();
			RegisterDataObject(daoTable);
			BeginTransaction("addTable");
			try
			{
				daoTable.Id = boTable.Id;
				daoTable.NumberOfSeat = boTable.NumberOfSeat;
				daoTable.StateId = boTable.StateId;
				daoTable.RestaurantId = _id.Value;
				daoTable.Insert();
				CommitTransaction();
				
				/*pick up any primary keys, computed values etc*/
				boTable = new BOTable(daoTable);
				if(_boTableCollection != null)
					_boTableCollection.Add(boTable);
			}
			catch(Exception ex)
			{
				RollbackTransaction("addTable");
				Handle(this, ex);
			}
		}
		
		///<Summary>
		///DeleteAllTable
		///This method deletes all BOTable objects from its collection
		///</Summary>
		///<returns>
		///void
		///</returns>
		///<parameters>
		///
		///</parameters>
		public virtual void DeleteAllTable()
		{
			RegisterDataObject(null);
			BeginTransaction("deleteAllTable");
			try
			{
				DAOTable.DeleteAllByRestaurantId(ConnectionProvider, _id.Value);
				CommitTransaction();
				if(_boTableCollection != null)
				{
					_boTableCollection.Clear();
					_boTableCollection = null;
				}
			}
			catch(Exception ex)
			{
				RollbackTransaction("deleteAllTable");
				Handle(this, ex);
			}
		}
		
		#endregion

		#region member properties
		
		public virtual Int32? Id
		{
			get
			{
				 return _id;
			}
			set
			{
				_id = value;
				_isDirty = true;
			}
		}
		
		public virtual string Name
		{
			get
			{
				 return _name;
			}
			set
			{
				_name = value;
				_isDirty = true;
			}
		}
		
		public virtual byte? Status
		{
			get
			{
				 return _status;
			}
			set
			{
				_status = value;
				_isDirty = true;
			}
		}
		
		[XmlIgnore]
		public virtual object Repository
		{
			get {	return null;	}
			set	{	}
		}
		
		public virtual bool IsDirty
		{
			get
			{
				 return _isDirty;
			}
			set
			{
				_isDirty = value;
			}
		}
		#endregion
	}
}