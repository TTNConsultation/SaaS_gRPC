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
	///This is the definition of the class BOState.
	///</Summary>
	public partial class BOState : zAAFConn_BaseBusiness, IQueryableCollection
	{
		#region member variables
		protected byte? _id;
		protected string _name;
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
		public BOState()
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
		///byte id
		///</parameters>
		public BOState(byte id)
		{
			try
			{
				DAOState daoState = DAOState.SelectOne(id);
				_id = daoState.Id;
				_name = daoState.Name;
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
		///DAOState
		///</parameters>
		protected internal BOState(DAOState daoState)
		{
			try
			{
				_id = daoState.Id;
				_name = daoState.Name;
			}
			catch
			{
				throw;
			}
		}

		///<Summary>
		///SaveNew
		///This method persists a new State record to the store
		///</Summary>
		///<returns>
		///void
		///</returns>
		///<parameters>
		///
		///</parameters>
		public virtual void SaveNew()
		{
			DAOState daoState = new DAOState();
			RegisterDataObject(daoState);
			BeginTransaction("savenewBOState");
			try
			{
				daoState.Name = _name;
				daoState.Insert();
				CommitTransaction();
				
				_id = daoState.Id;
				_name = daoState.Name;
				_isDirty = false;
			}
			catch(Exception ex)
			{
				RollbackTransaction("savenewBOState");
				Handle(this, ex);
			}
		}
		
		///<Summary>
		///Update
		///This method updates one State record in the store
		///</Summary>
		///<returns>
		///void
		///</returns>
		///<parameters>
		///BOState
		///</parameters>
		public virtual void Update()
		{
			DAOState daoState = new DAOState();
			RegisterDataObject(daoState);
			BeginTransaction("updateBOState");
			try
			{
				daoState.Id = _id;
				daoState.Name = _name;
				daoState.Update();
				CommitTransaction();
				
				_id = daoState.Id;
				_name = daoState.Name;
				_isDirty = false;
			}
			catch(Exception ex)
			{
				RollbackTransaction("updateBOState");
				Handle(this, ex);
			}
		}
		///<Summary>
		///Delete
		///This method deletes one State record from the store
		///</Summary>
		///<returns>
		///void
		///</returns>
		///<parameters>
		///
		///</parameters>
		public virtual void Delete()
		{
			DAOState daoState = new DAOState();
			RegisterDataObject(daoState);
			BeginTransaction("deleteBOState");
			try
			{
				daoState.Id = _id;
				daoState.Delete();
				CommitTransaction();
			}
			catch(Exception ex)
			{
				RollbackTransaction("deleteBOState");
				Handle(this, ex);
			}
		}
		
		///<Summary>
		///StateCollection
		///This method returns the collection of BOState objects
		///</Summary>
		///<returns>
		///List[BOState]
		///</returns>
		///<parameters>
		///
		///</parameters>
		public static IList<BOState> StateCollection()
		{
			try
			{
				IList<BOState> boStateCollection = new List<BOState>();
				IList<DAOState> daoStateCollection = DAOState.SelectAll();
			
				foreach(DAOState daoState in daoStateCollection)
					boStateCollection.Add(new BOState(daoState));
			
				return boStateCollection;
			}
			catch(Exception ex)
			{
				Handle(null, ex);
				return null;
			}
		}
		
		
		///<Summary>
		///StateCollectionCount
		///This method returns the collection count of BOState objects
		///</Summary>
		///<returns>
		///Int32
		///</returns>
		///<parameters>
		///
		///</parameters>
		public static Int32 StateCollectionCount()
		{
			try
			{
				Int32 objCount = DAOState.SelectAllCount();
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
		///List<BOState>
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
				IDictionary<string, IList<object>> retObj = DAOState.SelectAllByCriteriaProjection(lstDataProjection, lstDataCriteria, lstDataOrder, dataSkip, dataTake);
				return retObj;
			}
			catch(Exception ex)
			{
				Handle(this, ex);
				return null;
			}
		}
		
		
		///<Summary>
		///StateCollection<T>
		///This method implements the IQueryable Collection<T> method, returning a collection of BOState objects, filtered by optional criteria
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
				IList<T> boStateCollection = new List<T>();
				IList<IDataCriterion> lstDataCriteria = (icriteria == null) ? null : icriteria.ListDataCriteria();
				IList<IDataOrderBy> lstDataOrder = (icriteria == null) ? null : icriteria.ListDataOrder();
				IDataTake dataTake = (icriteria == null) ? null : icriteria.DataTake();
				IDataSkip dataSkip = (icriteria == null) ? null : icriteria.DataSkip();
				IList<DAOState> daoStateCollection = DAOState.SelectAllByCriteria(lstDataCriteria, lstDataOrder, dataSkip, dataTake);
			
				foreach(DAOState resdaoState in daoStateCollection)
					boStateCollection.Add((T)(object)new BOState(resdaoState));
			
				return boStateCollection;
			}
			catch(Exception ex)
			{
				Handle(this, ex);
				return null;
			}
		}
		
		
		///<Summary>
		///StateCollectionCount
		///This method implements the IQueryable CollectionCount method, returning a collection count of BOState objects, filtered by optional criteria
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
				Int32 objCount = DAOState.SelectAllByCriteriaCount(lstDataCriteria);
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
		
		public virtual byte? Id
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