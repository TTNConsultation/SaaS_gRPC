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
	///This is the definition of the class BOLocation.
	///</Summary>
	public partial class BOLocation : zAAFConn_BaseBusiness, IQueryableCollection
	{
		#region member variables
		protected Int32? _id;
		protected Int32? _unit;
		protected string _postalCode;
		protected string _streetName;
		protected string _link;
		protected string _map;
		protected string _latLong;
		protected bool _isDirty = false;
		/*collection member objects*******************/
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
		public BOLocation()
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
		public BOLocation(Int32 id)
		{
			try
			{
				DAOLocation daoLocation = DAOLocation.SelectOne(id);
				_id = daoLocation.Id;
				_unit = daoLocation.Unit;
				_postalCode = daoLocation.PostalCode;
				_streetName = daoLocation.StreetName;
				_link = daoLocation.Link;
				_map = daoLocation.Map;
				_latLong = daoLocation.LatLong;
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
		///DAOLocation
		///</parameters>
		protected internal BOLocation(DAOLocation daoLocation)
		{
			try
			{
				_id = daoLocation.Id;
				_unit = daoLocation.Unit;
				_postalCode = daoLocation.PostalCode;
				_streetName = daoLocation.StreetName;
				_link = daoLocation.Link;
				_map = daoLocation.Map;
				_latLong = daoLocation.LatLong;
			}
			catch
			{
				throw;
			}
		}

		///<Summary>
		///SaveNew
		///This method persists a new Location record to the store
		///</Summary>
		///<returns>
		///void
		///</returns>
		///<parameters>
		///
		///</parameters>
		public virtual void SaveNew()
		{
			DAOLocation daoLocation = new DAOLocation();
			RegisterDataObject(daoLocation);
			BeginTransaction("savenewBOLocation");
			try
			{
				daoLocation.Unit = _unit;
				daoLocation.PostalCode = _postalCode;
				daoLocation.StreetName = _streetName;
				daoLocation.Link = _link;
				daoLocation.Map = _map;
				daoLocation.LatLong = _latLong;
				daoLocation.Insert();
				CommitTransaction();
				
				_id = daoLocation.Id;
				_unit = daoLocation.Unit;
				_postalCode = daoLocation.PostalCode;
				_streetName = daoLocation.StreetName;
				_link = daoLocation.Link;
				_map = daoLocation.Map;
				_latLong = daoLocation.LatLong;
				_isDirty = false;
			}
			catch(Exception ex)
			{
				RollbackTransaction("savenewBOLocation");
				Handle(this, ex);
			}
		}
		
		///<Summary>
		///Update
		///This method updates one Location record in the store
		///</Summary>
		///<returns>
		///void
		///</returns>
		///<parameters>
		///BOLocation
		///</parameters>
		public virtual void Update()
		{
			DAOLocation daoLocation = new DAOLocation();
			RegisterDataObject(daoLocation);
			BeginTransaction("updateBOLocation");
			try
			{
				daoLocation.Id = _id;
				daoLocation.Unit = _unit;
				daoLocation.PostalCode = _postalCode;
				daoLocation.StreetName = _streetName;
				daoLocation.Link = _link;
				daoLocation.Map = _map;
				daoLocation.LatLong = _latLong;
				daoLocation.Update();
				CommitTransaction();
				
				_id = daoLocation.Id;
				_unit = daoLocation.Unit;
				_postalCode = daoLocation.PostalCode;
				_streetName = daoLocation.StreetName;
				_link = daoLocation.Link;
				_map = daoLocation.Map;
				_latLong = daoLocation.LatLong;
				_isDirty = false;
			}
			catch(Exception ex)
			{
				RollbackTransaction("updateBOLocation");
				Handle(this, ex);
			}
		}
		///<Summary>
		///Delete
		///This method deletes one Location record from the store
		///</Summary>
		///<returns>
		///void
		///</returns>
		///<parameters>
		///
		///</parameters>
		public virtual void Delete()
		{
			DAOLocation daoLocation = new DAOLocation();
			RegisterDataObject(daoLocation);
			BeginTransaction("deleteBOLocation");
			try
			{
				daoLocation.Id = _id;
				daoLocation.Delete();
				CommitTransaction();
			}
			catch(Exception ex)
			{
				RollbackTransaction("deleteBOLocation");
				Handle(this, ex);
			}
		}
		
		///<Summary>
		///LocationCollection
		///This method returns the collection of BOLocation objects
		///</Summary>
		///<returns>
		///List[BOLocation]
		///</returns>
		///<parameters>
		///
		///</parameters>
		public static IList<BOLocation> LocationCollection()
		{
			try
			{
				IList<BOLocation> boLocationCollection = new List<BOLocation>();
				IList<DAOLocation> daoLocationCollection = DAOLocation.SelectAll();
			
				foreach(DAOLocation daoLocation in daoLocationCollection)
					boLocationCollection.Add(new BOLocation(daoLocation));
			
				return boLocationCollection;
			}
			catch(Exception ex)
			{
				Handle(null, ex);
				return null;
			}
		}
		
		
		///<Summary>
		///LocationCollectionCount
		///This method returns the collection count of BOLocation objects
		///</Summary>
		///<returns>
		///Int32
		///</returns>
		///<parameters>
		///
		///</parameters>
		public static Int32 LocationCollectionCount()
		{
			try
			{
				Int32 objCount = DAOLocation.SelectAllCount();
				return objCount;
			}
			catch(Exception ex)
			{
				Handle(null, ex);
				return -1;
			}
		}
		
		
		///<Summary>
		///Projections
		///This method returns the collection of projections, ordered and filtered by optional criteria
		///</Summary>
		///<returns>
		///List<BOLocation>
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
				IDictionary<string, IList<object>> retObj = DAOLocation.SelectAllByCriteriaProjection(lstDataProjection, lstDataCriteria, lstDataOrder, dataSkip, dataTake);
				return retObj;
			}
			catch(Exception ex)
			{
				Handle(this, ex);
				return null;
			}
		}
		
		
		///<Summary>
		///LocationCollection<T>
		///This method implements the IQueryable Collection<T> method, returning a collection of BOLocation objects, filtered by optional criteria
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
				IList<T> boLocationCollection = new List<T>();
				IList<IDataCriterion> lstDataCriteria = (icriteria == null) ? null : icriteria.ListDataCriteria();
				IList<IDataOrderBy> lstDataOrder = (icriteria == null) ? null : icriteria.ListDataOrder();
				IDataTake dataTake = (icriteria == null) ? null : icriteria.DataTake();
				IDataSkip dataSkip = (icriteria == null) ? null : icriteria.DataSkip();
				IList<DAOLocation> daoLocationCollection = DAOLocation.SelectAllByCriteria(lstDataCriteria, lstDataOrder, dataSkip, dataTake);
			
				foreach(DAOLocation resdaoLocation in daoLocationCollection)
					boLocationCollection.Add((T)(object)new BOLocation(resdaoLocation));
			
				return boLocationCollection;
			}
			catch(Exception ex)
			{
				Handle(this, ex);
				return null;
			}
		}
		
		
		///<Summary>
		///LocationCollectionCount
		///This method implements the IQueryable CollectionCount method, returning a collection count of BOLocation objects, filtered by optional criteria
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
				Int32 objCount = DAOLocation.SelectAllByCriteriaCount(lstDataCriteria);
				return objCount;
			}
			catch(Exception ex)
			{
				Handle(this, ex);
				return -1;
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
		
		public virtual Int32? Unit
		{
			get
			{
				 return _unit;
			}
			set
			{
				_unit = value;
				_isDirty = true;
			}
		}
		
		public virtual string PostalCode
		{
			get
			{
				 return _postalCode;
			}
			set
			{
				_postalCode = value;
				_isDirty = true;
			}
		}
		
		public virtual string StreetName
		{
			get
			{
				 return _streetName;
			}
			set
			{
				_streetName = value;
				_isDirty = true;
			}
		}
		
		public virtual string Link
		{
			get
			{
				 return _link;
			}
			set
			{
				_link = value;
				_isDirty = true;
			}
		}
		
		public virtual string Map
		{
			get
			{
				 return _map;
			}
			set
			{
				_map = value;
				_isDirty = true;
			}
		}
		
		public virtual string LatLong
		{
			get
			{
				 return _latLong;
			}
			set
			{
				_latLong = value;
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