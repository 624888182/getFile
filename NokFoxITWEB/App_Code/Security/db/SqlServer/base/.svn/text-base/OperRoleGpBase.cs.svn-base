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
	public abstract class OperRoleGpBase{
		
		/// <summary>
		/// If operRoleGp is exist in database.
		/// </summary>
		/// <param name="operRoleGp">Business entity representing the operRoleGp.</param>
		/// <returns>bool. If exist return true, else return false.</returns>
		public bool isExist(OperRoleGpInfo operRoleGp){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_usyOperRoleGpIsExist");
			
			db.AddInParameter(command, "OperRoleGpCode", DbType.String,operRoleGp.OperRoleGpCode);
			
			int result = 0;
			result = db.ExecuteNonQuery(command);
			
			if( result > 0 ){
				return true;
			}else{
				return false;
			} 
		}
		
		/// <summary>
		/// Insert a operRoleGp to Database.
		/// </summary>
		/// <param name="order">Business entity representing the operRoleGp</param>
		/// <returns>OrderId</returns>
		public int Insert(OperRoleGpInfo operRoleGp){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_usyOperRoleGpInsert");
			db.AddInParameter(command, "OperRoleGpCode", DbType.String,operRoleGp.OperRoleGpCode);
			db.AddInParameter(command, "OperRoleGpName", DbType.String,operRoleGp.OperRoleGpName);
			return db.ExecuteNonQuery(command);
		}
		
		/// <summary>
		/// Method to Update an operRoleGp header
		/// </summary>
		/// <param name="order">Business entity representing the operRoleGp</param>
		public int Update(OperRoleGpInfo operRoleGp){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_usyOperRoleGpUpdate");
			db.AddInParameter(command, "OperRoleGpCode", DbType.String,operRoleGp.OperRoleGpCode);
			db.AddInParameter(command, "OperRoleGpName", DbType.String,operRoleGp.OperRoleGpName);
			return db.ExecuteNonQuery(command);
		}
		
		/// <summary>
		/// Method to delete an operRoleGp header
		/// </summary>
		/// <param name="order">Business entity representing the operRoleGp</param>
		public int Delete(OperRoleGpInfo operRoleGp){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_usyOperRoleGpDelete");
			db.AddInParameter(command, "OperRoleGpCode", DbType.String,operRoleGp.OperRoleGpCode);
			return db.ExecuteNonQuery(command);
		}
		
		public IDataReader getALL(){
			Database db = DatabaseFactory.CreateDatabase();
            IDataReader dr = null;
            DbCommand command = db.GetStoredProcCommand("usp_usyOperRoleGpGetAll");
			
            dr = db.ExecuteReader(command);
			
			return dr;
		}
		
		/// <summary>
		/// Reads the operRoleGp information for a primary key
		/// </summary>
		/// <returns>Business entity representing the operRoleGp</returns>
		
		public OperRoleGpInfo getOperRoleGp(System.String operRoleGpCode){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_usyOperRoleGpGet");
			db.AddInParameter(command, "OperRoleGpCode", DbType.String,operRoleGpCode);
			
			OperRoleGpInfo detail = new OperRoleGpInfo();

            using( IDataReader dr = db.ExecuteReader(command) ) {
                
                if( dr.Read() ){
					if (!dr.IsDBNull(0)) detail.OperRoleGpCode = dr.GetString(0);
					if (!dr.IsDBNull(1)) detail.OperRoleGpName = dr.GetString(1);
                }
            }
			
			return detail;
		}
		
		
		public IDataReader find(
			System.String operRoleGpCode, 
			System.String operRoleGpName, 
			int startRowIndex, 
			int maximumRows){
			Database db = DatabaseFactory.CreateDatabase();
			IDataReader dr = null;
            DbCommand command = db.GetStoredProcCommand("usp_usyOperRoleGpFind");
			db.AddParameter(command, "OperRoleGpCode", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, operRoleGpCode);
			db.AddParameter(command, "OperRoleGpName", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, operRoleGpName);
			db.AddInParameter(command, "startRowIndex", DbType.Int32, startRowIndex);
			db.AddInParameter(command, "maximumRows", DbType.Int32, maximumRows);
			dr = db.ExecuteReader(command);
			return dr;
		}
		
		public IDataReader find(OperRoleGpInfo operRoleGp, int startRowIndex, int maximumRows){
			return find(
				operRoleGp.OperRoleGpCode,  
				operRoleGp.OperRoleGpName,  
				startRowIndex, 
				maximumRows);
		}
		
		public int findCount(
			System.String operRoleGpCode, 
			System.String operRoleGpName, 
			int startRowIndex, 
			int maximumRows){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_usyOperRoleGpFindCount");
			db.AddParameter(command, "OperRoleGpCode", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, operRoleGpCode);
			db.AddParameter(command, "OperRoleGpName", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, operRoleGpName);
			db.AddInParameter(command, "startRowIndex", DbType.Int32, startRowIndex);
			db.AddInParameter(command, "maximumRows", DbType.Int32, maximumRows);
			db.AddParameter( command, "ReturnValue", DbType.Int32, ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Default, null );
			
			db.ExecuteNonQuery(command);
			
			int recordCount = (int)db.GetParameterValue( command, "ReturnValue" );
			return recordCount;
		}
		
		public int findCount(OperRoleGpInfo operRoleGp, int startRowIndex, int maximumRows){
			return findCount(
				operRoleGp.OperRoleGpCode, 
				operRoleGp.OperRoleGpName, 
				startRowIndex, 
				maximumRows);
		}
	}
}