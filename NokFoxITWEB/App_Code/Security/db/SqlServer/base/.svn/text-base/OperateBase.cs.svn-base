using System;
using System.Data;
using System.Data.Common;

using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.ObjectBuilder;

namespace FIH.Security.db{
	/// <summary>
	///
	/// </summary>
	public abstract class OperateBase{
		
		/// <summary>
		/// If operate is exist in database.
		/// </summary>
		/// <param name="operate">Business entity representing the operate.</param>
		/// <returns>bool. If exist return true, else return false.</returns>
		public bool isExist(OperateInfo operate){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_usyOperateIsExist");
			
			db.AddInParameter(command, "OperCode", DbType.String,operate.OperCode);
			
			int result = 0;
			result = db.ExecuteNonQuery(command);
			
			if( result > 0 ){
				return true;
			}else{
				return false;
			} 
		}
		
		/// <summary>
		/// Insert a operate to Database.
		/// </summary>
		/// <param name="order">Business entity representing the operate</param>
		/// <returns>OrderId</returns>
		public int Insert(OperateInfo operate){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_usyOperateInsert");
			db.AddInParameter(command, "OperCode", DbType.String,operate.OperCode);
			db.AddInParameter(command, "OperName", DbType.String,operate.OperName);
			return db.ExecuteNonQuery(command);
		}
		
		/// <summary>
		/// Method to Update an operate header
		/// </summary>
		/// <param name="order">Business entity representing the operate</param>
		public int Update(OperateInfo operate){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_usyOperateUpdate");
			db.AddInParameter(command, "OperCode", DbType.String,operate.OperCode);
			db.AddInParameter(command, "OperName", DbType.String,operate.OperName);
			return db.ExecuteNonQuery(command);
		}
		
		/// <summary>
		/// Method to delete an operate header
		/// </summary>
		/// <param name="order">Business entity representing the operate</param>
		public int Delete(OperateInfo operate){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_usyOperateDelete");
			db.AddInParameter(command, "OperCode", DbType.String,operate.OperCode);
			return db.ExecuteNonQuery(command);
		}
		
		public IDataReader getALL(){
			Database db = DatabaseFactory.CreateDatabase();
            IDataReader dr = null;
            DbCommand command = db.GetStoredProcCommand("usp_usyOperateGetAll");
			
            dr = db.ExecuteReader(command);
			
			return dr;
		}
		
		/// <summary>
		/// Reads the operate information for a primary key
		/// </summary>
		/// <returns>Business entity representing the operate</returns>
		
		public OperateInfo getOperate(System.String operCode){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_usyOperateGet");
			db.AddInParameter(command, "OperCode", DbType.String,operCode);
			
			OperateInfo detail = new OperateInfo();

            using( IDataReader dr = db.ExecuteReader(command) ) {
                
                if( dr.Read() ){
					if (!dr.IsDBNull(0)) detail.OperCode = dr.GetString(0);
					if (!dr.IsDBNull(1)) detail.OperName = dr.GetString(1);
                }
            }
			
			return detail;
		}
		
		
		public IDataReader find(
			System.String operCode, 
			System.String operName, 
			int startRowIndex, 
			int maximumRows){
			Database db = DatabaseFactory.CreateDatabase();
			IDataReader dr = null;
            DbCommand command = db.GetStoredProcCommand("usp_usyOperateFind");
			db.AddParameter(command, "OperCode", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, operCode);
			db.AddParameter(command, "OperName", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, operName);
			db.AddInParameter(command, "startRowIndex", DbType.Int32, startRowIndex);
			db.AddInParameter(command, "maximumRows", DbType.Int32, maximumRows);
			dr = db.ExecuteReader(command);
			return dr;
		}
		
		public IDataReader find(OperateInfo operate, int startRowIndex, int maximumRows){
			return find(
				operate.OperCode,  
				operate.OperName,  
				startRowIndex, 
				maximumRows);
		}
		
		public int findCount(
			System.String operCode, 
			System.String operName, 
			int startRowIndex, 
			int maximumRows){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_usyOperateFindCount");
			db.AddParameter(command, "OperCode", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, operCode);
			db.AddParameter(command, "OperName", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, operName);
			db.AddInParameter(command, "startRowIndex", DbType.Int32, startRowIndex);
			db.AddInParameter(command, "maximumRows", DbType.Int32, maximumRows);
			db.AddParameter( command, "ReturnValue", DbType.Int32, ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Default, null );
			
			db.ExecuteNonQuery(command);
			
			int recordCount = (int)db.GetParameterValue( command, "ReturnValue" );
			return recordCount;
		}
		
		public int findCount(OperateInfo operate, int startRowIndex, int maximumRows){
			return findCount(
				operate.OperCode, 
				operate.OperName, 
				startRowIndex, 
				maximumRows);
		}
	}
}