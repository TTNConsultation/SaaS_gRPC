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
	public partial class DAOState : zAAFConn_BaseData
	{
		#region member variables
		protected byte? _id;
		protected string _name;
		protected Int32? _errorCode;
		#endregion

		#region class methods
		public DAOState()
		{
		}
		///<Summary>
		///Select one row by primary key(s)
		///This method returns one row from the table State based on the primary key(s)
		///</Summary>
		///<returns>
		///DAOState
		///</returns>
		///<parameters>
		///byte? id
		///</parameters>
		public static DAOState SelectOne(byte? id)
		{
			SqlCommand	command = new SqlCommand();
			command.CommandText = InlineProcs.ctprState_SelectOne;
			command.CommandType = CommandType.Text;
			SqlConnection staticConnection = StaticSqlConnection;
			command.Connection = staticConnection;

			DataTable dt = new DataTable("State");
			SqlDataAdapter sqlAdapter = new SqlDataAdapter(command);
			try
			{
				command.Parameters.Add(CtSqlParameter.Get("@Id", SqlDbType.TinyInt, 1, ParameterDirection.Input, false, 3, 0, "", DataRowVersion.Proposed, (object)id?? (object)DBNull.Value));
				command.Parameters.Add(CtSqlParameter.Get("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, null));

				staticConnection.Open();
				sqlAdapter.Fill(dt);

				int errorCode = (Int32)command.Parameters["@ErrorCode"].Value;
				if(errorCode > 1)
					throw new Exception("procedure ctprState_SelectOne returned error code: " + errorCode );

				DAOState retObj = null;
				if(dt.Rows.Count > 0)
				{
					retObj = new DAOState();
					retObj._id					 = Convert.IsDBNull(dt.Rows[0]["Id"]) ? (byte?)null : (byte?)dt.Rows[0]["Id"];
					retObj._name					 = Convert.IsDBNull(dt.Rows[0]["Name"]) ? null : (string)dt.Rows[0]["Name"];
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
		///this method allows the object to delete itself from the table State based on its primary key
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
			command.CommandText = InlineProcs.ctprState_DeleteOne;
			command.CommandType = CommandType.Text;
			command.Connection = _connectionProvider.Connection;
			command.Transaction = _connectionProvider.CurrentTransaction;

			try
			{
				command.Parameters.Add(CtSqlParameter.Get("@Id", SqlDbType.TinyInt, 1, ParameterDirection.Input, false, 3, 0, "", DataRowVersion.Proposed, (object)_id?? (object)DBNull.Value));
				command.Parameters.Add(CtSqlParameter.Get("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

				command.ExecuteNonQuery();

				_errorCode = (Int32)command.Parameters["@ErrorCode"].Value;
				if(_errorCode > 1)
					throw new Exception("procedure ctprState_DeleteOne returned error code: " + _errorCode );

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
		///This method saves a new object to the table State
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
			command.CommandText = InlineProcs.ctprState_InsertOne;
			command.CommandType = CommandType.Text;
			command.Connection = _connectionProvider.Connection;
			command.Transaction = _connectionProvider.CurrentTransaction;

			try
			{
				command.Parameters.Add(CtSqlParameter.Get("@Id", SqlDbType.TinyInt, 1, ParameterDirection.Output, false, 3, 0, "", DataRowVersion.Proposed, _id));
				command.Parameters.Add(CtSqlParameter.Get("@Name", SqlDbType.NVarChar, 50, ParameterDirection.InputOutput, false, 0, 0, "", DataRowVersion.Proposed, (object)_name?? (object)DBNull.Value));
				command.Parameters.Add(CtSqlParameter.Get("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

				command.ExecuteNonQuery();

				_errorCode = (Int32)command.Parameters["@ErrorCode"].Value;
				if(_errorCode > 1)
					throw new Exception("procedure ctprState_InsertOne returned error code: " + _errorCode );

				_id					 = Convert.IsDBNull(command.Parameters["@Id"].Value) ? (byte?)null : (byte?)command.Parameters["@Id"].Value;
				_name					 = Convert.IsDBNull(command.Parameters["@Name"].Value) ? null : (string)command.Parameters["@Name"].Value;

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
		///This method returns all data rows in the table State
		///</Summary>
		///<returns>
		///IList-DAOState.
		///</returns>
		///<parameters>
		///
		///</parameters>
		public static IList<DAOState> SelectAll()
		{
			SqlCommand	command = new SqlCommand();
			command.CommandText = InlineProcs.ctprState_SelectAll;
			command.CommandType = CommandType.Text;
			SqlConnection staticConnection = StaticSqlConnection;
			command.Connection = staticConnection;

			DataTable dt = new DataTable("State");
			SqlDataAdapter sqlAdapter = new SqlDataAdapter(command);
			try
			{
				command.Parameters.Add(CtSqlParameter.Get("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, null));

				staticConnection.Open();
				sqlAdapter.Fill(dt);

				int errorCode = (Int32)command.Parameters["@ErrorCode"].Value;
				if(errorCode > 1)
					throw new Exception("procedure ctprState_SelectAll returned error code: " + errorCode );

				List<DAOState> objList = new List<DAOState>();
				if(dt.Rows.Count > 0)
				{
					foreach(DataRow row in dt.Rows)
					{
						DAOState retObj = new DAOState();
						retObj._id					 = Convert.IsDBNull(row["Id"]) ? (byte?)null : (byte?)row["Id"];
						retObj._name					 = Convert.IsDBNull(row["Name"]) ? null : (string)row["Name"];
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
			command.CommandText = InlineProcs.ctprState_SelectAllCount;
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
					throw new Exception("procedure ctprState_SelectAllCount returned error code: " + errorCode );

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
		///This method returns specific fields of all data rows in the table using criteriaquery apiState
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
			command.CommandText = GetSelectionCriteria(InlineProcs.ctprState_SelectAllByCriteriaProjection, listProjection, listCriterion, listOrder, dataSkip, dataTake);
			command.CommandType = CommandType.Text;
			SqlConnection staticConnection = StaticSqlConnection;
			command.Connection = staticConnection;

			DataTable dt = new DataTable("State");
			SqlDataAdapter sqlAdapter = new SqlDataAdapter(command);
			try
			{
				command.Parameters.Add(CtSqlParameter.Get("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, null));

				staticConnection.Open();
				sqlAdapter.Fill(dt);

				int errorCode = (Int32)command.Parameters["@ErrorCode"].Value;
				if(errorCode > 1)
					throw new Exception("procedure ctprState_SelectAllByCriteriaProjection returned error code: " + errorCode );

				IDictionary<string, IList<object>> dict = new Dictionary<string, IList<object>>();
				foreach (IDataProjection projection in listProjection)
				{
					IList<object> lst = new List<object>();
					dict.Add(projection.Member, lst);
					foreach (DataRow row in dt.Rows)
					{
						if (string.Compare(projection.Member, "Id", true) == 0) lst.Add(Convert.IsDBNull(row["Id"]) ? (byte?)null : (byte?)row["Id"]);
						if (string.Compare(projection.Member, "Name", true) == 0) lst.Add(Convert.IsDBNull(row["Name"]) ? null : (string)row["Name"]);
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
		///This method returns all data rows in the table using criteriaquery api State
		///</Summary>
		///<returns>
		///IList-DAOState.
		///</returns>
		///<parameters>
		///IList<IDataCriterion> listCriterion, IList<IDataOrderBy> listOrder, IDataSkip dataSkip, IDataTake dataTake
		///</parameters>
		public static IList<DAOState> SelectAllByCriteria(IList<IDataCriterion> listCriterion, IList<IDataOrderBy> listOrder, IDataSkip dataSkip, IDataTake dataTake)
		{
			SqlCommand	command = new SqlCommand();
			command.CommandText = GetSelectionCriteria(InlineProcs.ctprState_SelectAllByCriteria, null, listCriterion, listOrder, dataSkip, dataTake);
			command.CommandType = CommandType.Text;
			SqlConnection staticConnection = StaticSqlConnection;
			command.Connection = staticConnection;

			DataTable dt = new DataTable("State");
			SqlDataAdapter sqlAdapter = new SqlDataAdapter(command);
			try
			{
				command.Parameters.Add(CtSqlParameter.Get("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, null));

				staticConnection.Open();
				sqlAdapter.Fill(dt);

				int errorCode = (Int32)command.Parameters["@ErrorCode"].Value;
				if(errorCode > 1)
					throw new Exception("procedure ctprState_SelectAllByCriteria returned error code: " + errorCode );

				List<DAOState> objList = new List<DAOState>();
				if(dt.Rows.Count > 0)
				{
					foreach(DataRow row in dt.Rows)
					{
						DAOState retObj = new DAOState();
						retObj._id					 = Convert.IsDBNull(row["Id"]) ? (byte?)null : (byte?)row["Id"];
						retObj._name					 = Convert.IsDBNull(row["Name"]) ? null : (string)row["Name"];
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
		///This method returns all data rows in the table using criteriaquery api State
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
			command.CommandText = GetSelectionCriteria(InlineProcs.ctprState_SelectAllByCriteriaCount, null, listCriterion, null);
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
					throw new Exception("procedure ctprState_SelectAllByCriteriaCount returned error code: " + errorCode );

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
		///This method allows the object to update itself in the table State based on its primary key(s)
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
			command.CommandText = InlineProcs.ctprState_UpdateOne;
			command.CommandType = CommandType.Text;
			command.Connection = _connectionProvider.Connection;
			command.Transaction = _connectionProvider.CurrentTransaction;

			try
			{
				command.Parameters.Add(CtSqlParameter.Get("@Id", SqlDbType.TinyInt, 1, ParameterDirection.InputOutput, false, 3, 0, "", DataRowVersion.Proposed, (object)_id?? (object)DBNull.Value));
				command.Parameters.Add(CtSqlParameter.Get("@Name", SqlDbType.NVarChar, 50, ParameterDirection.InputOutput, false, 0, 0, "", DataRowVersion.Proposed, (object)_name?? (object)DBNull.Value));
				command.Parameters.Add(CtSqlParameter.Get("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

				command.ExecuteNonQuery();

				_errorCode = (Int32)command.Parameters["@ErrorCode"].Value;
				if(_errorCode > 1)
					throw new Exception("procedure ctprState_UpdateOne returned error code: " + _errorCode );

				_id					 = Convert.IsDBNull(command.Parameters["@Id"].Value) ? (byte?)null : (byte?)command.Parameters["@Id"].Value;
				_name					 = Convert.IsDBNull(command.Parameters["@Name"].Value) ? null : (string)command.Parameters["@Name"].Value;

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

		public byte? Id
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

		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
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
		internal static string ctprState_SelectOne = @"
			-- Select one row based on the primary key(s)
			SET NOCOUNT ON
			-- selects all rows from the table
			-- returning the error code if any
			SELECT 
			[Id]
			,[Name]
			FROM [administrator].[State]
			WHERE 
			[Id] = @Id
			-- returning the error code if any
			SELECT @ErrorCode = @@ERROR
			";

		internal static string ctprState_DeleteOne = @"
			-- Delete a row based on the primary key(s)
			SET NOCOUNT ON
			-- delete all matching from the table
			-- returning the error code if any
			DELETE [administrator].[State]
			WHERE 
			[Id] = @Id
			-- returning the error code if any
			SELECT @ErrorCode = @@ERROR
			";

		internal static string ctprState_InsertOne = @"
			-- Insert a new row
			SET NOCOUNT ON
			-- inserts a new row into the table
			-- returning the error code if any, and the identity field, if any
			INSERT [administrator].[State]
			(
			[Name]
			)
			VALUES
			(
			@Name
			)
			SELECT 
			@Id = [Id]
			,@Name = [Name]
			FROM [administrator].[State]
			WHERE 
			[Id] = SCOPE_IDENTITY()
			-- returning the error code if any
			SELECT @ErrorCode = @@ERROR
			";

		internal static string ctprState_SelectAll = @"
			-- Select All rows
			SET NOCOUNT ON
			-- selects all rows from the table
			-- returning the error code if any
			SELECT 
			[Id]
			,[Name]
			FROM [administrator].[State]
			-- returning the error code if any
			SELECT @ErrorCode = @@ERROR
			";

		internal static string ctprState_SelectAllCount = @"
			
			SET NOCOUNT ON
			-- selects count of all rows from the table
			-- returning the error code if any
			SELECT COUNT(*)
			FROM [administrator].[State]
			-- returning the error code if any
			SELECT @ErrorCode = @@ERROR
			";

		internal static string ctprState_SelectAllByCriteriaProjection = @"
			
			SET NOCOUNT ON
			-- selects all rows from the table by criteria
			-- returning the error code if any
			SELECT 
			##STARTFIELDS##
			##ENDFIELDS##
			FROM [administrator].[State]
			##CRITERIA##
			-- returning the error code if any
			SELECT @ErrorCode = @@ERROR
			";

		internal static string ctprState_SelectAllByCriteria = @"
			
			SET NOCOUNT ON
			-- selects all rows from the table by criteria
			-- returning the error code if any
			SELECT 
			[Id]
			,[Name]
			FROM [administrator].[State]
			##CRITERIA##
			-- returning the error code if any
			SELECT @ErrorCode = @@ERROR
			";

		internal static string ctprState_SelectAllByCriteriaCount = @"
			
			SET NOCOUNT ON
			-- selects count of all rows from the table according to criteria
			-- returning the error code if any
			SELECT COUNT(*)
			FROM [administrator].[State]
			##CRITERIA##
			-- returning the error code if any
			SELECT @ErrorCode = @@ERROR
			";

		internal static string ctprState_UpdateOne = @"
			-- Update one row based on the primary key(s)
			SET NOCOUNT ON
			-- updates a row in the table based on the primary key
			-- returning the error code if any, and the identity field, if any
			
			UPDATE [administrator].[State]
			SET
			[Name] = @Name
			WHERE 
			[Id] = @Id
			SELECT 
			@Id = [Id]
			,@Name = [Name]
			FROM [administrator].[State]
			WHERE 
			[Id] = @Id
			-- returning the error code if any
			SELECT @ErrorCode = @@ERROR
			";

	}
}
#endregion
