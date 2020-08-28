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
	public partial class DAOAppSetting : zAAFConn_BaseData
	{
		#region member variables
		protected Int32? _id;
		protected Int32? _nameKey;
		protected Int32? _descriptionKey;
		protected Int32? _errorCode;
		#endregion

		#region class methods
		public DAOAppSetting()
		{
		}
		///<Summary>
		///Select one row by primary key(s)
		///This method returns one row from the table AppSetting based on the primary key(s)
		///</Summary>
		///<returns>
		///DAOAppSetting
		///</returns>
		///<parameters>
		///Int32? id
		///</parameters>
		public static DAOAppSetting SelectOne(Int32? id)
		{
			SqlCommand	command = new SqlCommand();
			command.CommandText = InlineProcs.ctprAppSetting_SelectOne;
			command.CommandType = CommandType.Text;
			SqlConnection staticConnection = StaticSqlConnection;
			command.Connection = staticConnection;

			DataTable dt = new DataTable("AppSetting");
			SqlDataAdapter sqlAdapter = new SqlDataAdapter(command);
			try
			{
				command.Parameters.Add(CtSqlParameter.Get("@Id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, (object)id?? (object)DBNull.Value));
				command.Parameters.Add(CtSqlParameter.Get("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, null));

				staticConnection.Open();
				sqlAdapter.Fill(dt);

				int errorCode = (Int32)command.Parameters["@ErrorCode"].Value;
				if(errorCode > 1)
					throw new Exception("procedure ctprAppSetting_SelectOne returned error code: " + errorCode );

				DAOAppSetting retObj = null;
				if(dt.Rows.Count > 0)
				{
					retObj = new DAOAppSetting();
					retObj._id					 = Convert.IsDBNull(dt.Rows[0]["Id"]) ? (Int32?)null : (Int32?)dt.Rows[0]["Id"];
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
		///this method allows the object to delete itself from the table AppSetting based on its primary key
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
			command.CommandText = InlineProcs.ctprAppSetting_DeleteOne;
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
					throw new Exception("procedure ctprAppSetting_DeleteOne returned error code: " + _errorCode );

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
		///Insert a new row
		///This method saves a new object to the table AppSetting
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
			command.CommandText = InlineProcs.ctprAppSetting_InsertOne;
			command.CommandType = CommandType.Text;
			command.Connection = _connectionProvider.Connection;
			command.Transaction = _connectionProvider.CurrentTransaction;

			try
			{
				command.Parameters.Add(CtSqlParameter.Get("@Id", SqlDbType.Int, 4, ParameterDirection.InputOutput, false, 10, 0, "", DataRowVersion.Proposed, (object)_id?? (object)DBNull.Value));
				command.Parameters.Add(CtSqlParameter.Get("@NameKey", SqlDbType.Int, 4, ParameterDirection.InputOutput, false, 10, 0, "", DataRowVersion.Proposed, (object)_nameKey?? (object)DBNull.Value));
				command.Parameters.Add(CtSqlParameter.Get("@DescriptionKey", SqlDbType.Int, 4, ParameterDirection.InputOutput, false, 10, 0, "", DataRowVersion.Proposed, (object)_descriptionKey?? (object)DBNull.Value));
				command.Parameters.Add(CtSqlParameter.Get("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

				command.ExecuteNonQuery();

				_errorCode = (Int32)command.Parameters["@ErrorCode"].Value;
				if(_errorCode > 1)
					throw new Exception("procedure ctprAppSetting_InsertOne returned error code: " + _errorCode );

				_id					 = Convert.IsDBNull(command.Parameters["@Id"].Value) ? (Int32?)null : (Int32?)command.Parameters["@Id"].Value;
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
		///This method returns all data rows in the table AppSetting
		///</Summary>
		///<returns>
		///IList-DAOAppSetting.
		///</returns>
		///<parameters>
		///
		///</parameters>
		public static IList<DAOAppSetting> SelectAll()
		{
			SqlCommand	command = new SqlCommand();
			command.CommandText = InlineProcs.ctprAppSetting_SelectAll;
			command.CommandType = CommandType.Text;
			SqlConnection staticConnection = StaticSqlConnection;
			command.Connection = staticConnection;

			DataTable dt = new DataTable("AppSetting");
			SqlDataAdapter sqlAdapter = new SqlDataAdapter(command);
			try
			{
				command.Parameters.Add(CtSqlParameter.Get("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, null));

				staticConnection.Open();
				sqlAdapter.Fill(dt);

				int errorCode = (Int32)command.Parameters["@ErrorCode"].Value;
				if(errorCode > 1)
					throw new Exception("procedure ctprAppSetting_SelectAll returned error code: " + errorCode );

				List<DAOAppSetting> objList = new List<DAOAppSetting>();
				if(dt.Rows.Count > 0)
				{
					foreach(DataRow row in dt.Rows)
					{
						DAOAppSetting retObj = new DAOAppSetting();
						retObj._id					 = Convert.IsDBNull(row["Id"]) ? (Int32?)null : (Int32?)row["Id"];
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
			command.CommandText = InlineProcs.ctprAppSetting_SelectAllCount;
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
					throw new Exception("procedure ctprAppSetting_SelectAllCount returned error code: " + errorCode );

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
		///This method returns specific fields of all data rows in the table using criteriaquery apiAppSetting
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
			command.CommandText = GetSelectionCriteria(InlineProcs.ctprAppSetting_SelectAllByCriteriaProjection, listProjection, listCriterion, listOrder, dataSkip, dataTake);
			command.CommandType = CommandType.Text;
			SqlConnection staticConnection = StaticSqlConnection;
			command.Connection = staticConnection;

			DataTable dt = new DataTable("AppSetting");
			SqlDataAdapter sqlAdapter = new SqlDataAdapter(command);
			try
			{
				command.Parameters.Add(CtSqlParameter.Get("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, null));

				staticConnection.Open();
				sqlAdapter.Fill(dt);

				int errorCode = (Int32)command.Parameters["@ErrorCode"].Value;
				if(errorCode > 1)
					throw new Exception("procedure ctprAppSetting_SelectAllByCriteriaProjection returned error code: " + errorCode );

				IDictionary<string, IList<object>> dict = new Dictionary<string, IList<object>>();
				foreach (IDataProjection projection in listProjection)
				{
					IList<object> lst = new List<object>();
					dict.Add(projection.Member, lst);
					foreach (DataRow row in dt.Rows)
					{
						if (string.Compare(projection.Member, "Id", true) == 0) lst.Add(Convert.IsDBNull(row["Id"]) ? (Int32?)null : (Int32?)row["Id"]);
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
		///This method returns all data rows in the table using criteriaquery api AppSetting
		///</Summary>
		///<returns>
		///IList-DAOAppSetting.
		///</returns>
		///<parameters>
		///IList<IDataCriterion> listCriterion, IList<IDataOrderBy> listOrder, IDataSkip dataSkip, IDataTake dataTake
		///</parameters>
		public static IList<DAOAppSetting> SelectAllByCriteria(IList<IDataCriterion> listCriterion, IList<IDataOrderBy> listOrder, IDataSkip dataSkip, IDataTake dataTake)
		{
			SqlCommand	command = new SqlCommand();
			command.CommandText = GetSelectionCriteria(InlineProcs.ctprAppSetting_SelectAllByCriteria, null, listCriterion, listOrder, dataSkip, dataTake);
			command.CommandType = CommandType.Text;
			SqlConnection staticConnection = StaticSqlConnection;
			command.Connection = staticConnection;

			DataTable dt = new DataTable("AppSetting");
			SqlDataAdapter sqlAdapter = new SqlDataAdapter(command);
			try
			{
				command.Parameters.Add(CtSqlParameter.Get("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, null));

				staticConnection.Open();
				sqlAdapter.Fill(dt);

				int errorCode = (Int32)command.Parameters["@ErrorCode"].Value;
				if(errorCode > 1)
					throw new Exception("procedure ctprAppSetting_SelectAllByCriteria returned error code: " + errorCode );

				List<DAOAppSetting> objList = new List<DAOAppSetting>();
				if(dt.Rows.Count > 0)
				{
					foreach(DataRow row in dt.Rows)
					{
						DAOAppSetting retObj = new DAOAppSetting();
						retObj._id					 = Convert.IsDBNull(row["Id"]) ? (Int32?)null : (Int32?)row["Id"];
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
		///This method returns all data rows in the table using criteriaquery api AppSetting
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
			command.CommandText = GetSelectionCriteria(InlineProcs.ctprAppSetting_SelectAllByCriteriaCount, null, listCriterion, null);
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
					throw new Exception("procedure ctprAppSetting_SelectAllByCriteriaCount returned error code: " + errorCode );

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
		///This method allows the object to update itself in the table AppSetting based on its primary key(s)
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
			command.CommandText = InlineProcs.ctprAppSetting_UpdateOne;
			command.CommandType = CommandType.Text;
			command.Connection = _connectionProvider.Connection;
			command.Transaction = _connectionProvider.CurrentTransaction;

			try
			{
				command.Parameters.Add(CtSqlParameter.Get("@Id", SqlDbType.Int, 4, ParameterDirection.InputOutput, false, 10, 0, "", DataRowVersion.Proposed, (object)_id?? (object)DBNull.Value));
				command.Parameters.Add(CtSqlParameter.Get("@NameKey", SqlDbType.Int, 4, ParameterDirection.InputOutput, false, 10, 0, "", DataRowVersion.Proposed, (object)_nameKey?? (object)DBNull.Value));
				command.Parameters.Add(CtSqlParameter.Get("@DescriptionKey", SqlDbType.Int, 4, ParameterDirection.InputOutput, false, 10, 0, "", DataRowVersion.Proposed, (object)_descriptionKey?? (object)DBNull.Value));
				command.Parameters.Add(CtSqlParameter.Get("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

				command.ExecuteNonQuery();

				_errorCode = (Int32)command.Parameters["@ErrorCode"].Value;
				if(_errorCode > 1)
					throw new Exception("procedure ctprAppSetting_UpdateOne returned error code: " + _errorCode );

				_id					 = Convert.IsDBNull(command.Parameters["@Id"].Value) ? (Int32?)null : (Int32?)command.Parameters["@Id"].Value;
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
		internal static string ctprAppSetting_SelectOne = @"
			-- Select one row based on the primary key(s)
			SET NOCOUNT ON
			-- selects all rows from the table
			-- returning the error code if any
			SELECT 
			[Id]
			,[NameKey]
			,[DescriptionKey]
			FROM [app].[AppSetting]
			WHERE 
			[Id] = @Id
			-- returning the error code if any
			SELECT @ErrorCode = @@ERROR
			";

		internal static string ctprAppSetting_DeleteOne = @"
			-- Delete a row based on the primary key(s)
			SET NOCOUNT ON
			-- delete all matching from the table
			-- returning the error code if any
			DELETE [app].[AppSetting]
			WHERE 
			[Id] = @Id
			-- returning the error code if any
			SELECT @ErrorCode = @@ERROR
			";

		internal static string ctprAppSetting_InsertOne = @"
			-- Insert a new row
			SET NOCOUNT ON
			-- inserts a new row into the table
			-- returning the error code if any, and the identity field, if any
			INSERT [app].[AppSetting]
			(
			[Id]
			,[NameKey]
			,[DescriptionKey]
			)
			VALUES
			(
			@Id
			,@NameKey
			,@DescriptionKey
			)
			SELECT 
			@Id = [Id]
			,@NameKey = [NameKey]
			,@DescriptionKey = [DescriptionKey]
			FROM [app].[AppSetting]
			WHERE 
			[Id] = @Id
			-- returning the error code if any
			SELECT @ErrorCode = @@ERROR
			";

		internal static string ctprAppSetting_SelectAll = @"
			-- Select All rows
			SET NOCOUNT ON
			-- selects all rows from the table
			-- returning the error code if any
			SELECT 
			[Id]
			,[NameKey]
			,[DescriptionKey]
			FROM [app].[AppSetting]
			-- returning the error code if any
			SELECT @ErrorCode = @@ERROR
			";

		internal static string ctprAppSetting_SelectAllCount = @"
			
			SET NOCOUNT ON
			-- selects count of all rows from the table
			-- returning the error code if any
			SELECT COUNT(*)
			FROM [app].[AppSetting]
			-- returning the error code if any
			SELECT @ErrorCode = @@ERROR
			";

		internal static string ctprAppSetting_SelectAllByCriteriaProjection = @"
			
			SET NOCOUNT ON
			-- selects all rows from the table by criteria
			-- returning the error code if any
			SELECT 
			##STARTFIELDS##
			##ENDFIELDS##
			FROM [app].[AppSetting]
			##CRITERIA##
			-- returning the error code if any
			SELECT @ErrorCode = @@ERROR
			";

		internal static string ctprAppSetting_SelectAllByCriteria = @"
			
			SET NOCOUNT ON
			-- selects all rows from the table by criteria
			-- returning the error code if any
			SELECT 
			[Id]
			,[NameKey]
			,[DescriptionKey]
			FROM [app].[AppSetting]
			##CRITERIA##
			-- returning the error code if any
			SELECT @ErrorCode = @@ERROR
			";

		internal static string ctprAppSetting_SelectAllByCriteriaCount = @"
			
			SET NOCOUNT ON
			-- selects count of all rows from the table according to criteria
			-- returning the error code if any
			SELECT COUNT(*)
			FROM [app].[AppSetting]
			##CRITERIA##
			-- returning the error code if any
			SELECT @ErrorCode = @@ERROR
			";

		internal static string ctprAppSetting_UpdateOne = @"
			-- Update one row based on the primary key(s)
			SET NOCOUNT ON
			-- updates a row in the table based on the primary key
			-- returning the error code if any, and the identity field, if any
			
			UPDATE [app].[AppSetting]
			SET
			[NameKey] = @NameKey
			,[DescriptionKey] = @DescriptionKey
			WHERE 
			[Id] = @Id
			SELECT 
			@Id = [Id]
			,@NameKey = [NameKey]
			,@DescriptionKey = [DescriptionKey]
			FROM [app].[AppSetting]
			WHERE 
			[Id] = @Id
			-- returning the error code if any
			SELECT @ErrorCode = @@ERROR
			";

	}
}
#endregion