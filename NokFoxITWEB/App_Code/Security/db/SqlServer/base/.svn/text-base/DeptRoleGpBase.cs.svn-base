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
	public abstract class DeptRoleGpBase{
		
		/// <summary>
		/// If deptRoleGp is exist in database.
		/// </summary>
		/// <param name="deptRoleGp">Business entity representing the deptRoleGp.</param>
		/// <returns>bool. If exist return true, else return false.</returns>
		public bool isExist(DeptRoleGpInfo deptRoleGp){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_usyDeptRoleGpIsExist");
			
			db.AddInParameter(command, "DeptRoleGpCode", DbType.String,deptRoleGp.DeptRoleGpCode);
			
			int result = 0;
			result = db.ExecuteNonQuery(command);
			
			if( result > 0 ){
				return true;
			}else{
				return false;
			} 
		}
		
		/// <summary>
		/// Insert a deptRoleGp to Database.
		/// </summary>
		/// <param name="order">Business entity representing the deptRoleGp</param>
		/// <returns>OrderId</returns>
		public int Insert(DeptRoleGpInfo deptRoleGp){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_usyDeptRoleGpInsert");
			db.AddInParameter(command, "DeptRoleGpCode", DbType.String,deptRoleGp.DeptRoleGpCode);
			db.AddInParameter(command, "DeptRoleGpName", DbType.String,deptRoleGp.DeptRoleGpName);
			db.AddInParameter(command, "DivNo", DbType.String,deptRoleGp.DivNo);
			return db.ExecuteNonQuery(command);
		}
		
		/// <summary>
		/// Method to Update an deptRoleGp header
		/// </summary>
		/// <param name="order">Business entity representing the deptRoleGp</param>
		public int Update(DeptRoleGpInfo deptRoleGp){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_usyDeptRoleGpUpdate");
			db.AddInParameter(command, "DeptRoleGpCode", DbType.String,deptRoleGp.DeptRoleGpCode);
			db.AddInParameter(command, "DeptRoleGpName", DbType.String,deptRoleGp.DeptRoleGpName);
			db.AddInParameter(command, "DivNo", DbType.String,deptRoleGp.DivNo);
			return db.ExecuteNonQuery(command);
		}
		
		/// <summary>
		/// Method to delete an deptRoleGp header
		/// </summary>
		/// <param name="order">Business entity representing the deptRoleGp</param>
		public int Delete(DeptRoleGpInfo deptRoleGp){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_usyDeptRoleGpDelete");
			db.AddInParameter(command, "DeptRoleGpCode", DbType.String,deptRoleGp.DeptRoleGpCode);
			return db.ExecuteNonQuery(command);
		}
		
		public IDataReader getALL(){
			Database db = DatabaseFactory.CreateDatabase();
            IDataReader dr = null;
            DbCommand command = db.GetStoredProcCommand("usp_usyDeptRoleGpGetAll");
			
            dr = db.ExecuteReader(command);
			
			return dr;
		}
		
		/// <summary>
		/// Reads the deptRoleGp information for a primary key
		/// </summary>
		/// <returns>Business entity representing the deptRoleGp</returns>
		
		public DeptRoleGpInfo getDeptRoleGp(System.String deptRoleGpCode){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_usyDeptRoleGpGet");
			db.AddInParameter(command, "DeptRoleGpCode", DbType.String,deptRoleGpCode);
			
			DeptRoleGpInfo detail = new DeptRoleGpInfo();

            using( IDataReader dr = db.ExecuteReader(command) ) {
                
                if( dr.Read() ){
					if (!dr.IsDBNull(0)) detail.DeptRoleGpCode = dr.GetString(0);
					if (!dr.IsDBNull(1)) detail.DeptRoleGpName = dr.GetString(1);
					if (!dr.IsDBNull(2)) detail.DivNo = dr.GetString(2);
                }
            }
			
			return detail;
		}
		
		
		public IDataReader find(
			System.String deptRoleGpCode, 
			System.String deptRoleGpName, 
			System.String divNo, 
			int startRowIndex, 
			int maximumRows){
			Database db = DatabaseFactory.CreateDatabase();
			IDataReader dr = null;
            DbCommand command = db.GetStoredProcCommand("usp_usyDeptRoleGpFind");
			db.AddParameter(command, "DeptRoleGpCode", DbType.String, 10, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, deptRoleGpCode);
			db.AddParameter(command, "DeptRoleGpName", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, deptRoleGpName);
			db.AddParameter(command, "DivNo", DbType.String, -1, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, divNo);
			db.AddInParameter(command, "startRowIndex", DbType.Int32, startRowIndex);
			db.AddInParameter(command, "maximumRows", DbType.Int32, maximumRows);
			dr = db.ExecuteReader(command);
			return dr;
		}
		
		public IDataReader find(DeptRoleGpInfo deptRoleGp, int startRowIndex, int maximumRows){
			return find(
				deptRoleGp.DeptRoleGpCode,  
				deptRoleGp.DeptRoleGpName,  
				deptRoleGp.DivNo,  
				startRowIndex, 
				maximumRows);
		}
		
		public int findCount(
			System.String deptRoleGpCode, 
			System.String deptRoleGpName, 
			System.String divNo, 
			int startRowIndex, 
			int maximumRows){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_usyDeptRoleGpFindCount");
			db.AddParameter(command, "DeptRoleGpCode", DbType.String, 10, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, deptRoleGpCode);
			db.AddParameter(command, "DeptRoleGpName", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, deptRoleGpName);
			db.AddParameter(command, "DivNo", DbType.String, -1, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, divNo);
			db.AddInParameter(command, "startRowIndex", DbType.Int32, startRowIndex);
			db.AddInParameter(command, "maximumRows", DbType.Int32, maximumRows);
			db.AddParameter( command, "ReturnValue", DbType.Int32, ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Default, null );
			
			db.ExecuteNonQuery(command);
			
			int recordCount = (int)db.GetParameterValue( command, "ReturnValue" );
			return recordCount;
		}
		
		public int findCount(DeptRoleGpInfo deptRoleGp, int startRowIndex, int maximumRows){
			return findCount(
				deptRoleGp.DeptRoleGpCode, 
				deptRoleGp.DeptRoleGpName, 
				deptRoleGp.DivNo, 
				startRowIndex, 
				maximumRows);
		}
	}
}