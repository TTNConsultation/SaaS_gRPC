/*************************************************************
** Class generated by CodeTrigger, Version 6.3.0.4
** This class was generated on 2020-08-01 3:54:27 AM
** Changes to this file may cause incorrect behaviour and will be lost if the code is regenerated
**************************************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using AAF.DataObjects.Interfaces;

namespace AAF.DataObjects
{
	public partial class DAOItem : zAAFConn_BaseData
	{
		#region member variables
		protected Int32? _id;
		protected Int32? _restaurantId;
		protected decimal? _price;
		protected bool? _isExtra;
		protected byte? _stateId;
		protected Int32? _nameKey;
		protected Int32? _descriptionKey;
		protected Int32? _errorCode;
		#endregion

		#region class methods
		public DAOItem()
		{
		}
		///<Summary>
		///Select one row by primary key(s)
		///This method returns one row from the table Item based on the primary key(s)
		///</Summary>
		///<returns>
		///DAOItem
		///</returns>
		///<parameters>
		///Int32? id
		///</parameters>
		public static DAOItem SelectOne(Int32? id)
		{
			SqlCommand	command = new SqlCommand();
			command.CommandText = InlineProcs.ctprItem_SelectOne;
			command.CommandType = CommandType.Text;
			SqlConnection staticConnection = StaticSqlConnection;
			command.Connection = staticConnection;

			DataTable dt = new DataTable("Item");
			SqlDataAdapter sqlAdapter = new SqlDataAdapter(command);
			try
			{
				command.Parameters.Add(CtSqlParameter.Get("@Id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, (object)id?? (object)DBNull.Value));
				command.Parameters.Add(CtSqlParameter.Get("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, null));

				staticConnection.Open();
				sqlAdapter.Fill(dt);

				int errorCode = (Int32)command.Parameters["@ErrorCode"].Value;
				if(errorCode > 1)
					throw new Exception("procedure ctprItem_SelectOne returned error code: " + errorCode );

				DAOItem retObj = null;
				if(dt.Rows.Count > 0)
				{
					retObj = new DAOItem();
					retObj._id					 = Convert.IsDBNull(dt.Rows[0]["Id"]) ? (Int32?)null : (Int32?)dt.Rows[0]["Id"];
					retObj._restaurantId					 = Convert.IsDBNull(dt.Rows[0]["RestaurantId"]) ? (Int32?)null : (Int32?)dt.Rows[0]["RestaurantId"];
					retObj._price					 = Convert.IsDBNull(dt.Rows[0]["Price"]) ? (decimal?)null : (decimal?)dt.Rows[0]["Price"];
					retObj._isExtra					 = Convert.IsDBNull(dt.Rows[0]["IsExtra"]) ? (bool?)null : (bool?)dt.Rows[0]["IsExtra"];
					retObj._stateId					 = Convert.IsDBNull(dt.Rows[0]["StateId"]) ? (byte?)null : (byte?)dt.Rows[0]["StateId"];
					retObj._nameKey					 = Convert.IsDBNull(dt.Rows[0]["NameKey"]) ? (Int32?)null : (Int32?)dt.Rows[0]["NameKey"];
					retObj._descriptionKey					 = Convert.IsDBNull(dt.Rows[0]["DescriptionKey"]) ? (Int32?)null : (Int32?)dt.Rows[0]["DescriptionKey"];
				}
				return retObj;
			}
			catch(Exception ex)
			{
				Handle(null, ex);
				return null;
			}
			finally
			{
				staticConnection.Close();
				command.Dispose();
			}
		}

		///<Summary>
		///Delete one row by primary key(s)
		///this method allows the object to delete itself from the table Item based on its primary key
		///</Summary>
		///<returns>
		///void
		///</returns>
		///<parameters>
		///
		///</parameters>
		public virtual void Delete()
		{
			SqlCommand	command = new SqlCommand();
			command.CommandText = InlineProcs.ctprItem_DeleteOne;
			command.CommandType = CommandType.Text;
			command.Connection = _connectionProvider.Connection;
			command.Transaction = _connectionProvider.CurrentTransaction;

			try
			{
				command.Parameters.Add(CtSqlParameter.Get("@Id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, (object)_id?? (object)DBNull.Value));
				command.Parameters.Add(CtSqlParameter.Get("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

				command.ExecuteNonQuery();

				_errorCode = (Int32)command.Parameters["@ErrorCode"].Value;
				if(_errorCode > 1)
					throw new Exception("procedure ctprItem_DeleteOne returned error code: " + _errorCode );

			}
			catch(Exception ex)
			{
				Handle(this, ex);
			}
			finally
			{
				command.Dispose();
			}
		}

		///<Summary>
		///Select all rows by foreign key
		///This method returns all data rows in the table Item based on a foreign key
		///</Summary>
		///<returns>
		///IList-DAOItem.
		///</returns>
		///<parameters>
		///Int32? restaurantId
		///</parameters>
		public static IList<DAOItem> SelectAllByRestaurantId(Int32? restaurantId)
		{
			SqlCommand	command = new SqlCommand();
			command.CommandText = InlineProcs.ctprItem_SelectAllByRestaurantId;
			command.CommandType = CommandType.Text;
			SqlConnection staticConnection = StaticSqlConnection;
			command.Connection = staticConnection;

			DataTable dt = new DataTable("Item");
			SqlDataAdapter sqlAdapter = new SqlDataAdapter(command);
			try
			{
				command.Parameters.Add(CtSqlParameter.Get("@RestaurantId", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, (object)restaurantId?? (object)DBNull.Value));
				command.Parameters.Add(CtSqlParameter.Get("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, null));

				staticConnection.Open();
				sqlAdapter.Fill(dt);

				int errorCode = (Int32)command.Parameters["@ErrorCode"].Value;
				if(errorCode > 1)
					throw new Exception("procedure ctprItem_SelectAllByRestaurantId returned error code: " + errorCode );

				List<DAOItem> objList = new List<DAOItem>();
				if(dt.Rows.Count > 0)
				{
					foreach(DataRow row in dt.Rows)
					{
						DAOItem retObj = new DAOItem();
						retObj._id					 = Convert.IsDBNull(row["Id"]) ? (Int32?)null : (Int32?)row["Id"];
						retObj._restaurantId					 = Convert.IsDBNull(row["RestaurantId"]) ? (Int32?)null : (Int32?)row["RestaurantId"];
						retObj._price					 = Convert.IsDBNull(row["Price"]) ? (decimal?)null : (decimal?)row["Price"];
						retObj._isExtra					 = Convert.IsDBNull(row["IsExtra"]) ? (bool?)null : (bool?)row["IsExtra"];
						retObj._stateId					 = Convert.IsDBNull(row["StateId"]) ? (byte?)null : (byte?)row["StateId"];
						retObj._nameKey					 = Convert.IsDBNull(row["NameKey"]) ? (Int32?)null : (Int32?)row["NameKey"];
						retObj._descriptionKey					 = Convert.IsDBNull(row["DescriptionKey"]) ? (Int32?)null : (Int32?)row["DescriptionKey"];
						objList.Add(retObj);
					}
				}
				return objList;
			}
			catch(Exception ex)
			{
				Handle(null, ex);
				return null;
			}
			finally
			{
				staticConnection.Close();
				command.Dispose();
			}
		}

		///<Summary>
		///</Summary>
		///<returns>
		///Int32
		///</returns>
		///<parameters>
		///Int32? restaurantId
		///</parameters>
		public static Int32 SelectAllByRestaurantIdCount(Int32? restaurantId)
		{
			SqlCommand	command = new SqlCommand();
			command.CommandText = InlineProcs.ctprItem_SelectAllByRestaurantIdCount;
			command.CommandType = CommandType.Text;
			SqlConnection staticConnection = StaticSqlConnection;
			command.Connection = staticConnection;

			try
			{
				command.Parameters.Add(CtSqlParameter.Get("@RestaurantId", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, (object)restaurantId?? (object)DBNull.Value));
				command.Parameters.Add(CtSqlParameter.Get("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, null));

				staticConnection.Open();
				Int32 retCount = (Int32)command.ExecuteScalar();

				int errorCode = (Int32)command.Parameters["@ErrorCode"].Value;
				if(errorCode > 1)
					throw new Exception("procedure ctprItem_SelectAllByRestaurantIdCount returned error code: " + errorCode );

				return retCount;
			}
			catch(Exception ex)
			{
				Handle(null, ex);
				return -1;
			}
			finally
			{
				staticConnection.Close();
				command.Dispose();
			}
		}

		///<Summary>
		///Delete all by foreign key
		///This method deletes all rows in the table Item with a given foreign key
		///</Summary>
		///<returns>
		///void
		///</returns>
		///<parameters>
		///zAAFConn_TxConnectionProvider connectionProvider, Int32? restaurantId
		///</parameters>
		public static void DeleteAllByRestaurantId(zAAFConn_TxConnectionProvider connectionProvider, Int32? restaurantId)
		{
			SqlCommand	command = new SqlCommand();
			command.CommandText = InlineProcs.ctprItem_DeleteAllByRestaurantId;
			command.CommandType = CommandType.Text;
			command.Connection = connectionProvider.Connection;
			command.Transaction = connectionProvider.CurrentTransaction;

			try
			{
				command.Parameters.Add(CtSqlParameter.Get("@RestaurantId", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, (object)restaurantId?? (object)DBNull.Value));
				command.Parameters.Add(CtSqlParameter.Get("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, null));

				command.ExecuteNonQuery();

				int errorCode = (Int32)command.Parameters["@ErrorCode"].Value;
				if(errorCode > 1)
					throw new Exception("procedure ctprItem_DeleteAllByRestaurantId returned error code: " + errorCode );

			}
			catch(Exception ex)
			{
				Handle(null, ex);
			}
			finally
			{
				command.Dispose();
			}
		}

		///<Summary>
		///Insert a new row
		///This method saves a new object to the table Item
		///</Summary>
		///<returns>
		///void
		///</returns>
		///<parameters>
		///
		///</parameters>
		public virtual void Insert()
		{
			SqlCommand	command = new SqlCommand();
			command.CommandText = InlineProcs.ctprItem_InsertOne;
			command.CommandType = CommandType.Text;
			command.Connection = _connectionProvider.Connection;
			command.Transaction = _connectionProvider.CurrentTransaction;

			try
			{
				command.Parameters.Add(CtSqlParameter.Get("@Id", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _id));
				command.Parameters.Add(CtSqlParameter.Get("@RestaurantId", SqlDbType.Int, 4, ParameterDirection.InputOutput, false, 10, 0, "", DataRowVersion.Proposed, (object)_restaurantId?? (object)DBNull.Value));
				command.Parameters.Add(CtSqlParameter.Get("@Price", SqlDbType.Money, 8, ParameterDirection.InputOutput, false, 19, 4, "", DataRowVersion.Proposed, (object)_price?? (object)DBNull.Value));
				command.Parameters.Add(CtSqlParameter.Get("@IsExtra", SqlDbType.Bit, 1, ParameterDirection.InputOutput, false, 0, 0, "", DataRowVersion.Proposed, (object)_isExtra?? (object)DBNull.Value));
				command.Parameters.Add(CtSqlParameter.Get("@StateId", SqlDbType.TinyInt, 1, ParameterDirection.InputOutput, false, 3, 0, "", DataRowVersion.Proposed, (object)_stateId?? (object)DBNull.Value));
				command.Parameters.Add(CtSqlParameter.Get("@NameKey", SqlDbType.Int, 4, ParameterDirection.InputOutput, false, 10, 0, "", DataRowVersion.Proposed, (object)_nameKey?? (object)DBNull.Value));
				command.Parameters.Add(CtSqlParameter.Get("@DescriptionKey", SqlDbType.Int, 4, ParameterDirection.InputOutput, false, 10, 0, "", DataRowVersion.Proposed, (object)_descriptionKey?? (object)DBNull.Value));
				command.Parameters.Add(CtSqlParameter.Get("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

				command.ExecuteNonQuery();

				_errorCode = (Int32)command.Parameters["@ErrorCode"].Value;
				if(_errorCode > 1)
					throw new Exception("procedure ctprItem_InsertOne returned error code: " + _errorCode );

				_id					 = Convert.IsDBNull(command.Parameters["@Id"].Value) ? (Int32?)null : (Int32?)command.Parameters["@Id"].Value;
				_restaurantId					 = Convert.IsDBNull(command.Parameters["@RestaurantId"].Value) ? (Int32?)null : (Int32?)command.Parameters["@RestaurantId"].Value;
				_price					 = Convert.IsDBNull(command.Parameters["@Price"].Value) ? (decimal?)null : (decimal?)command.Parameters["@Price"].Value;
				_isExtra					 = Convert.IsDBNull(command.Parameters["@IsExtra"].Value) ? (bool?)null : (bool?)command.Parameters["@IsExtra"].Value;
				_stateId					 = Convert.IsDBNull(command.Parameters["@StateId"].Value) ? (byte?)null : (byte?)command.Parameters["@StateId"].Value;
				_nameKey					 = Convert.IsDBNull(command.Parameters["@NameKey"].Value) ? (Int32?)null : (Int32?)command.Parameters["@NameKey"].Value;
				_descriptionKey					 = Convert.IsDBNull(command.Parameters["@DescriptionKey"].Value) ? (Int32?)null : (Int32?)command.Parameters["@DescriptionKey"].Value;

			}
			catch(Exception ex)
			{
				Handle(this, ex);
			}
			finally
			{
				command.Dispose();
			}
		}

		///<Summary>
		///Select all rows
		///This method returns all data rows in the table Item
		///</Summary>
		///<returns>
		///IList-DAOItem.
		///</returns>
		///<parameters>
		///
		///</parameters>
		public static IList<DAOItem> SelectAll()
		{
			SqlCommand	command = new SqlCommand();
			command.CommandText = InlineProcs.ctprItem_SelectAll;
			command.CommandType = CommandType.Text;
			SqlConnection staticConnection = StaticSqlConnection;
			command.Connection = staticConnection;

			DataTable dt = new DataTable("Item");
			SqlDataAdapter sqlAdapter = new SqlDataAdapter(command);
			try
			{
				command.Parameters.Add(CtSqlParameter.Get("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, null));

				staticConnection.Open();
				sqlAdapter.Fill(dt);

				int errorCode = (Int32)command.Parameters["@ErrorCode"].Value;
				if(errorCode > 1)
					throw new Exception("procedure ctprItem_SelectAll returned error code: " + errorCode );

				List<DAOItem> objList = new List<DAOItem>();
				if(dt.Rows.Count > 0)
				{
					foreach(DataRow row in dt.Rows)
					{
						DAOItem retObj = new DAOItem();
						retObj._id					 = Convert.IsDBNull(row["Id"]) ? (Int32?)null : (Int32?)row["Id"];
						retObj._restaurantId					 = Convert.IsDBNull(row["RestaurantId"]) ? (Int32?)null : (Int32?)row["RestaurantId"];
						retObj._price					 = Convert.IsDBNull(row["Price"]) ? (decimal?)null : (decimal?)row["Price"];
						retObj._isExtra					 = Convert.IsDBNull(row["IsExtra"]) ? (bool?)null : (bool?)row["IsExtra"];
						retObj._stateId					 = Convert.IsDBNull(row["StateId"]) ? (byte?)null : (byte?)row["StateId"];
						retObj._nameKey					 = Convert.IsDBNull(row["NameKey"]) ? (Int32?)null : (Int32?)row["NameKey"];
						retObj._descriptionKey					 = Convert.IsDBNull(row["DescriptionKey"]) ? (Int32?)null : (Int32?)row["DescriptionKey"];
						objList.Add(retObj);
					}
				}
				return objList;
			}
			catch(Exception ex)
			{
				Handle(null, ex);
				return null;
			}
			finally
			{
				staticConnection.Close();
				command.Dispose();
			}
		}

		///<Summary>
		///</Summary>
		///<returns>
		///Int32
		///</returns>
		///<parameters>
		///
		///</parameters>
		public static Int32 SelectAllCount()
		{
			SqlCommand	command = new SqlCommand();
			command.CommandText = InlineProcs.ctprItem_SelectAllCount;
			command.CommandType = CommandType.Text;
			SqlConnection staticConnection = StaticSqlConnection;
			command.Connection = staticConnection;

			try
			{
				command.Parameters.Add(CtSqlParameter.Get("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, null));

				staticConnection.Open();
				Int32 retCount = (Int32)command.ExecuteScalar();

				int errorCode = (Int32)command.Parameters["@ErrorCode"].Value;
				if(errorCode > 1)
					throw new Exception("procedure ctprItem_SelectAllCount returned error code: " + errorCode );

				return retCount;
			}
			catch(Exception ex)
			{
				Handle(null, ex);
				return -1;
			}
			finally
			{
				staticConnection.Close();
				command.Dispose();
			}
		}

		///<Summary>
		///Select specific fields of all rows using criteriaquery api
		///This method returns specific fields of all data rows in the table using criteriaquery apiItem
		///</Summary>
		///<returns>
		///IDictionary-string, IList-object..
		///</returns>
		///<parameters>
		///IList<IDataProjection> listProjection, IList<IDataCriterion> listCriterion, IList<IDataOrderBy> listOrder, IDataSkip dataSkip, IDataTake dataTake
		///</parameters>
		public static IDictionary<string, IList<object>> SelectAllByCriteriaProjection(IList<IDataProjection> listProjection, IList<IDataCriterion> listCriterion, IList<IDataOrderBy> listOrder, IDataSkip dataSkip, IDataTake dataTake)
		{
			SqlCommand	command = new SqlCommand();
			command.CommandText = GetSelectionCriteria(InlineProcs.ctprItem_SelectAllByCriteriaProjection, listProjection, listCriterion, listOrder, dataSkip, dataTake);
			command.CommandType = CommandType.Text;
			SqlConnection staticConnection = StaticSqlConnection;
			command.Connection = staticConnection;

			DataTable dt = new DataTable("Item");
			SqlDataAdapter sqlAdapter = new SqlDataAdapter(command);
			try
			{
				command.Parameters.Add(CtSqlParameter.Get("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, null));

				staticConnection.Open();
				sqlAdapter.Fill(dt);

				int errorCode = (Int32)command.Parameters["@ErrorCode"].Value;
				if(errorCode > 1)
					throw new Exception("procedure ctprItem_SelectAllByCriteriaProjection returned error code: " + errorCode );

				IDictionary<string, IList<object>> dict = new Dictionary<string, IList<object>>();
				foreach (IDataProjection projection in listProjection)
				{
					IList<object> lst = new List<object>();
					dict.Add(projection.Member, lst);
					foreach (DataRow row in dt.Rows)
					{
						if (string.Compare(projection.Member, "Id", true) == 0) lst.Add(Convert.IsDBNull(row["Id"]) ? (Int32?)null : (Int32?)row["Id"]);
						if (string.Compare(projection.Member, "RestaurantId", true) == 0) lst.Add(Convert.IsDBNull(row["RestaurantId"]) ? (Int32?)null : (Int32?)row["RestaurantId"]);
						if (string.Compare(projection.Member, "Price", true) == 0) lst.Add(Convert.IsDBNull(row["Price"]) ? (decimal?)null : (decimal?)row["Price"]);
						if (string.Compare(projection.Member, "IsExtra", true) == 0) lst.Add(Convert.IsDBNull(row["IsExtra"]) ? (bool?)null : (bool?)row["IsExtra"]);
						if (string.Compare(projection.Member, "StateId", true) == 0) lst.Add(Convert.IsDBNull(row["StateId"]) ? (byte?)null : (byte?)row["StateId"]);
						if (string.Compare(projection.Member, "NameKey", true) == 0) lst.Add(Convert.IsDBNull(row["NameKey"]) ? (Int32?)null : (Int32?)row["NameKey"]);
						if (string.Compare(projection.Member, "DescriptionKey", true) == 0) lst.Add(Convert.IsDBNull(row["DescriptionKey"]) ? (Int32?)null : (Int32?)row["DescriptionKey"]);
					}
				}
				return dict;
			}
			catch(Exception ex)
			{
				Handle(null, ex);
				return null;
			}
			finally
			{
				staticConnection.Close();
				command.Dispose();
			}
		}

		///<Summary>
		///Select all rows by filter criteria
		///This method returns all data rows in the table using criteriaquery api Item
		///</Summary>
		///<returns>
		///IList-DAOItem.
		///</returns>
		///<parameters>
		///IList<IDataCriterion> listCriterion, IList<IDataOrderBy> listOrder, IDataSkip dataSkip, IDataTake dataTake
		///</parameters>
		public static IList<DAOItem> SelectAllByCriteria(IList<IDataCriterion> listCriterion, IList<IDataOrderBy> listOrder, IDataSkip dataSkip, IDataTake dataTake)
		{
			SqlCommand	command = new SqlCommand();
			command.CommandText = GetSelectionCriteria(InlineProcs.ctprItem_SelectAllByCriteria, null, listCriterion, listOrder, dataSkip, dataTake);
			command.CommandType = CommandType.Text;
			SqlConnection staticConnection = StaticSqlConnection;
			command.Connection = staticConnection;

			DataTable dt = new DataTable("Item");
			SqlDataAdapter sqlAdapter = new SqlDataAdapter(command);
			try
			{
				command.Parameters.Add(CtSqlParameter.Get("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, null));

				staticConnection.Open();
				sqlAdapter.Fill(dt);

				int errorCode = (Int32)command.Parameters["@ErrorCode"].Value;
				if(errorCode > 1)
					throw new Exception("procedure ctprItem_SelectAllByCriteria returned error code: " + errorCode );

				List<DAOItem> objList = new List<DAOItem>();
				if(dt.Rows.Count > 0)
				{
					foreach(DataRow row in dt.Rows)
					{
						DAOItem retObj = new DAOItem();
						retObj._id					 = Convert.IsDBNull(row["Id"]) ? (Int32?)null : (Int32?)row["Id"];
						retObj._restaurantId					 = Convert.IsDBNull(row["RestaurantId"]) ? (Int32?)null : (Int32?)row["RestaurantId"];
						retObj._price					 = Convert.IsDBNull(row["Price"]) ? (decimal?)null : (decimal?)row["Price"];
						retObj._isExtra					 = Convert.IsDBNull(row["IsExtra"]) ? (bool?)null : (bool?)row["IsExtra"];
						retObj._stateId					 = Convert.IsDBNull(row["StateId"]) ? (byte?)null : (byte?)row["StateId"];
						retObj._nameKey					 = Convert.IsDBNull(row["NameKey"]) ? (Int32?)null : (Int32?)row["NameKey"];
						retObj._descriptionKey					 = Convert.IsDBNull(row["DescriptionKey"]) ? (Int32?)null : (Int32?)row["DescriptionKey"];
						objList.Add(retObj);
					}
				}
				return objList;
			}
			catch(Exception ex)
			{
				Handle(null, ex);
				return null;
			}
			finally
			{
				staticConnection.Close();
				command.Dispose();
			}
		}

		///<Summary>
		///Select count of all rows using criteriaquery api
		///This method returns all data rows in the table using criteriaquery api Item
		///</Summary>
		///<returns>
		///Int32
		///</returns>
		///<parameters>
		///IList<IDataCriterion> listCriterion
		///</parameters>
		public static Int32 SelectAllByCriteriaCount(IList<IDataCriterion> listCriterion)
		{
			SqlCommand	command = new SqlCommand();
			command.CommandText = GetSelectionCriteria(InlineProcs.ctprItem_SelectAllByCriteriaCount, null, listCriterion, null);
			command.CommandType = CommandType.Text;
			SqlConnection staticConnection = StaticSqlConnection;
			command.Connection = staticConnection;

			try
			{
				command.Parameters.Add(CtSqlParameter.Get("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, null));

				staticConnection.Open();
				Int32 retCount = (Int32)command.ExecuteScalar();

				int errorCode = (Int32)command.Parameters["@ErrorCode"].Value;
				if(errorCode > 1)
					throw new Exception("procedure ctprItem_SelectAllByCriteriaCount returned error code: " + errorCode );

				return retCount;
			}
			catch(Exception ex)
			{
				Handle(null, ex);
				return -1;
			}
			finally
			{
				staticConnection.Close();
				command.Dispose();
			}
		}

		///<Summary>
		///Update one row by primary key(s)
		///This method allows the object to update itself in the table Item based on its primary key(s)
		///</Summary>
		///<returns>
		///void
		///</returns>
		///<parameters>
		///
		///</parameters>
		public virtual void Update()
		{
			SqlCommand	command = new SqlCommand();
			command.CommandText = InlineProcs.ctprItem_UpdateOne;
			command.CommandType = CommandType.Text;
			command.Connection = _connectionProvider.Connection;
			command.Transaction = _connectionProvider.CurrentTransaction;

			try
			{
				command.Parameters.Add(CtSqlParameter.Get("@Id", SqlDbType.Int, 4, ParameterDirection.InputOutput, false, 10, 0, "", DataRowVersion.Proposed, (object)_id?? (object)DBNull.Value));
				command.Parameters.Add(CtSqlParameter.Get("@RestaurantId", SqlDbType.Int, 4, ParameterDirection.InputOutput, false, 10, 0, "", DataRowVersion.Proposed, (object)_restaurantId?? (object)DBNull.Value));
				command.Parameters.Add(CtSqlParameter.Get("@Price", SqlDbType.Money, 8, ParameterDirection.InputOutput, false, 19, 4, "", DataRowVersion.Proposed, (object)_price?? (object)DBNull.Value));
				command.Parameters.Add(CtSqlParameter.Get("@IsExtra", SqlDbType.Bit, 1, ParameterDirection.InputOutput, false, 0, 0, "", DataRowVersion.Proposed, (object)_isExtra?? (object)DBNull.Value));
				command.Parameters.Add(CtSqlParameter.Get("@StateId", SqlDbType.TinyInt, 1, ParameterDirection.InputOutput, false, 3, 0, "", DataRowVersion.Proposed, (object)_stateId?? (object)DBNull.Value));
				command.Parameters.Add(CtSqlParameter.Get("@NameKey", SqlDbType.Int, 4, ParameterDirection.InputOutput, false, 10, 0, "", DataRowVersion.Proposed, (object)_nameKey?? (object)DBNull.Value));
				command.Parameters.Add(CtSqlParameter.Get("@DescriptionKey", SqlDbType.Int, 4, ParameterDirection.InputOutput, false, 10, 0, "", DataRowVersion.Proposed, (object)_descriptionKey?? (object)DBNull.Value));
				command.Parameters.Add(CtSqlParameter.Get("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

				command.ExecuteNonQuery();

				_errorCode = (Int32)command.Parameters["@ErrorCode"].Value;
				if(_errorCode > 1)
					throw new Exception("procedure ctprItem_UpdateOne returned error code: " + _errorCode );

				_id					 = Convert.IsDBNull(command.Parameters["@Id"].Value) ? (Int32?)null : (Int32?)command.Parameters["@Id"].Value;
				_restaurantId					 = Convert.IsDBNull(command.Parameters["@RestaurantId"].Value) ? (Int32?)null : (Int32?)command.Parameters["@RestaurantId"].Value;
				_price					 = Convert.IsDBNull(command.Parameters["@Price"].Value) ? (decimal?)null : (decimal?)command.Parameters["@Price"].Value;
				_isExtra					 = Convert.IsDBNull(command.Parameters["@IsExtra"].Value) ? (bool?)null : (bool?)command.Parameters["@IsExtra"].Value;
				_stateId					 = Convert.IsDBNull(command.Parameters["@StateId"].Value) ? (byte?)null : (byte?)command.Parameters["@StateId"].Value;
				_nameKey					 = Convert.IsDBNull(command.Parameters["@NameKey"].Value) ? (Int32?)null : (Int32?)command.Parameters["@NameKey"].Value;
				_descriptionKey					 = Convert.IsDBNull(command.Parameters["@DescriptionKey"].Value) ? (Int32?)null : (Int32?)command.Parameters["@DescriptionKey"].Value;

			}
			catch(Exception ex)
			{
				Handle(this, ex);
			}
			finally
			{
				command.Dispose();
			}
		}

		#endregion

		#region member properties

		public Int32? Id
		{
			get
			{
				return _id;
			}
			set
			{
				_id = value;
			}
		}

		public Int32? RestaurantId
		{
			get
			{
				return _restaurantId;
			}
			set
			{
				_restaurantId = value;
			}
		}

		public decimal? Price
		{
			get
			{
				return _price;
			}
			set
			{
				_price = value;
			}
		}

		public bool? IsExtra
		{
			get
			{
				return _isExtra;
			}
			set
			{
				_isExtra = value;
			}
		}

		public byte? StateId
		{
			get
			{
				return _stateId;
			}
			set
			{
				_stateId = value;
			}
		}

		public Int32? NameKey
		{
			get
			{
				return _nameKey;
			}
			set
			{
				_nameKey = value;
			}
		}

		public Int32? DescriptionKey
		{
			get
			{
				return _descriptionKey;
			}
			set
			{
				_descriptionKey = value;
			}
		}

		public Int32? ErrorCode
		{
			get
			{
				return _errorCode;
			}
		}

		#endregion
	}
}

#region inline sql procs
namespace AAF.DataObjects
{
	public partial class InlineProcs
	{
		internal static string ctprItem_SelectOne = @"
			-- Select one row based on the primary key(s)
			SET NOCOUNT ON
			-- selects all rows from the table
			-- returning the error code if any
			SELECT 
			[Id]
			,[RestaurantId]
			,[Price]
			,[IsExtra]
			,[StateId]
			,[NameKey]
			,[DescriptionKey]
			FROM [administrator].[Item]
			WHERE 
			[Id] = @Id
			-- returning the error code if any
			SELECT @ErrorCode = @@ERROR
			";

		internal static string ctprItem_DeleteOne = @"
			-- Delete a row based on the primary key(s)
			SET NOCOUNT ON
			-- delete all matching from the table
			-- returning the error code if any
			DELETE [administrator].[Item]
			WHERE 
			[Id] = @Id
			-- returning the error code if any
			SELECT @ErrorCode = @@ERROR
			";

		internal static string ctprItem_SelectAllByRestaurantId = @"
			-- Select all rows based on a foreign key
			SET NOCOUNT ON
			-- selects all rows from the table
			-- returning the error code if any
			SELECT 
			[Id]
			,[RestaurantId]
			,[Price]
			,[IsExtra]
			,[StateId]
			,[NameKey]
			,[DescriptionKey]
			FROM [administrator].[Item]
			WHERE 
			[RestaurantId] = @RestaurantId OR ([RestaurantId] IS NULL AND @RestaurantId IS NULL)
			-- returning the error code if any
			SELECT @ErrorCode = @@ERROR
			";

		internal static string ctprItem_SelectAllByRestaurantIdCount = @"
			-- Get count of rows returnable by this query
			SET NOCOUNT ON
			-- selects count of all rows from the table
			-- returning the error code if any
			SELECT COUNT(*)
			FROM [administrator].[Item]
			WHERE 
			[RestaurantId] = @RestaurantId OR ([RestaurantId] IS NULL AND @RestaurantId IS NULL)
			-- returning the error code if any
			SELECT @ErrorCode = @@ERROR
			";

		internal static string ctprItem_DeleteAllByRestaurantId = @"
			
			SET NOCOUNT ON
			-- delete all matching from the table
			-- returning the error code if any
			DELETE [administrator].[Item]
			WHERE 
			[RestaurantId] = @RestaurantId OR ([RestaurantId] IS NULL AND @RestaurantId IS NULL)
			-- returning the error code if any
			SELECT @ErrorCode = @@ERROR
			";

		internal static string ctprItem_InsertOne = @"
			-- Insert a new row
			SET NOCOUNT ON
			-- inserts a new row into the table
			-- returning the error code if any, and the identity field, if any
			INSERT [administrator].[Item]
			(
			[RestaurantId]
			,[Price]
			,[IsExtra]
			,[StateId]
			,[NameKey]
			,[DescriptionKey]
			)
			VALUES
			(
			@RestaurantId
			,@Price
			,@IsExtra
			,@StateId
			,@NameKey
			,@DescriptionKey
			)
			SELECT 
			@Id = [Id]
			,@RestaurantId = [RestaurantId]
			,@Price = [Price]
			,@IsExtra = [IsExtra]
			,@StateId = [StateId]
			,@NameKey = [NameKey]
			,@DescriptionKey = [DescriptionKey]
			FROM [administrator].[Item]
			WHERE 
			[Id] = SCOPE_IDENTITY()
			-- returning the error code if any
			SELECT @ErrorCode = @@ERROR
			";

		internal static string ctprItem_SelectAll = @"
			-- Select All rows
			SET NOCOUNT ON
			-- selects all rows from the table
			-- returning the error code if any
			SELECT 
			[Id]
			,[RestaurantId]
			,[Price]
			,[IsExtra]
			,[StateId]
			,[NameKey]
			,[DescriptionKey]
			FROM [administrator].[Item]
			-- returning the error code if any
			SELECT @ErrorCode = @@ERROR
			";

		internal static string ctprItem_SelectAllCount = @"
			
			SET NOCOUNT ON
			-- selects count of all rows from the table
			-- returning the error code if any
			SELECT COUNT(*)
			FROM [administrator].[Item]
			-- returning the error code if any
			SELECT @ErrorCode = @@ERROR
			";

		internal static string ctprItem_SelectAllByCriteriaProjection = @"
			
			SET NOCOUNT ON
			-- selects all rows from the table by criteria
			-- returning the error code if any
			SELECT 
			##STARTFIELDS##
			##ENDFIELDS##
			FROM [administrator].[Item]
			##CRITERIA##
			-- returning the error code if any
			SELECT @ErrorCode = @@ERROR
			";

		internal static string ctprItem_SelectAllByCriteria = @"
			
			SET NOCOUNT ON
			-- selects all rows from the table by criteria
			-- returning the error code if any
			SELECT 
			[Id]
			,[RestaurantId]
			,[Price]
			,[IsExtra]
			,[StateId]
			,[NameKey]
			,[DescriptionKey]
			FROM [administrator].[Item]
			##CRITERIA##
			-- returning the error code if any
			SELECT @ErrorCode = @@ERROR
			";

		internal static string ctprItem_SelectAllByCriteriaCount = @"
			
			SET NOCOUNT ON
			-- selects count of all rows from the table according to criteria
			-- returning the error code if any
			SELECT COUNT(*)
			FROM [administrator].[Item]
			##CRITERIA##
			-- returning the error code if any
			SELECT @ErrorCode = @@ERROR
			";

		internal static string ctprItem_UpdateOne = @"
			-- Update one row based on the primary key(s)
			SET NOCOUNT ON
			-- updates a row in the table based on the primary key
			-- returning the error code if any, and the identity field, if any
			
			UPDATE [administrator].[Item]
			SET
			[RestaurantId] = @RestaurantId
			,[Price] = @Price
			,[IsExtra] = @IsExtra
			,[StateId] = @StateId
			,[NameKey] = @NameKey
			,[DescriptionKey] = @DescriptionKey
			WHERE 
			[Id] = @Id
			SELECT 
			@Id = [Id]
			,@RestaurantId = [RestaurantId]
			,@Price = [Price]
			,@IsExtra = [IsExtra]
			,@StateId = [StateId]
			,@NameKey = [NameKey]
			,@DescriptionKey = [DescriptionKey]
			FROM [administrator].[Item]
			WHERE 
			[Id] = @Id
			-- returning the error code if any
			SELECT @ErrorCode = @@ERROR
			";

	}
}
#endregion