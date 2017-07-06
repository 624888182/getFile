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
	public abstract class OperRoleDetailBase{
		
		/// <summary>
		/// If operRoleDetail is exist in database.
		/// </summary>
		/// <param name="operRoleDetail">Business entity representing the operRoleDetail.</param>
		/// <returns>bool. If exist return true, else return false.</returns>
		public bool isExist(OperRoleDetailInfo operRoleDetail){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_usyOperRoleDetailIsExist");
			
			db.AddInParameter(command, "OperRoleGpCode", DbType.String,operRoleDetail.OperRoleGpCode);
			db.AddInParameter(command, "ModuleCode", DbType.String,operRoleDetail.ModuleCode);
			
			int result = 0;
			result = db.ExecuteNonQuery(command);
			
			if( result > 0 ){
				return true;
			}else{
				return false;
			} 
		}
		
		/// <summary>
		/// Insert a operRoleDetail to Database.
		/// </summary>
		/// <param name="order">Business entity representing the operRoleDetail</param>
		/// <returns>OrderId</returns>
		public int Insert(OperRoleDetailInfo operRoleDetail){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_usyOperRoleDetailInsert");
			db.AddInParameter(command, "OperRoleGpCode", DbType.String,operRoleDetail.OperRoleGpCode);
			db.AddInParameter(command, "ModuleCode", DbType.String,operRoleDetail.ModuleCode);
			db.AddInParameter(command, "OperCode", DbType.String,operRoleDetail.OperCode);
			return db.ExecuteNonQuery(command);
		}
		
		/// <summary>
		/// Method to Update an operRoleDetail header
		/// </summary>
		/// <param name="order">Business entity representing the operRoleDetail</param>
		public int Update(OperRoleDetailInfo operRoleDetail){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_usyOperRoleDetailUpdate");
			db.AddInParameter(command, "OperRoleGpCode", DbType.String,operRoleDetail.OperRoleGpCode);
			db.AddInParameter(command, "ModuleCode", DbType.String,operRoleDetail.ModuleCode);
			db.AddInParameter(command, "OperCode", DbType.String,operRoleDetail.OperCode);
			return db.ExecuteNonQuery(command);
		}
		
		/// <summary>
		/// Method to delete an operRoleDetail header
		/// </summary>
		/// <param name="order">Business entity representing the operRoleDetail</param>
		public int Delete(OperRoleDetailInfo operRoleDetail){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_usyOperRoleDetailDelete");
			db.AddInParameter(command, "OperRoleGpCode", DbType.String,operRoleDetail.OperRoleGpCode);
			db.AddInParameter(command, "ModuleCode", DbType.String,operRoleDetail.ModuleCode);
			return db.ExecuteNonQuery(command);
		}
		
		public IDataReader getALL(){
			Database db = DatabaseFactory.CreateDatabase();
            IDataReader dr = null;
            DbCommand command = db.GetStoredProcCommand("usp_usyOperRoleDetailGetAll");
			
            dr = db.ExecuteReader(command);
			
			return dr;
		}
		
		/// <summary>
		/// Reads the operRoleDetail information for a primary key
		/// </summary>
		/// <returns>Business entity representing the operRoleDetail</returns>
		
		public OperRoleDetailInfo getOperRoleDetail(System.String operRoleGpCode, System.String moduleCode){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_usyOperRoleDetailGet");
			db.AddInParameter(command, "OperRoleGpCode", DbType.String,operRoleGpCode);
			db.AddInParameter(command, "ModuleCode", DbType.String,moduleCode);
			
			OperRoleDetailInfo detail = new OperRoleDetailInfo();

            using( IDataReader dr = db.ExecuteReader(command) ) {
                
                if( dr.Read() ){
					if (!dr.IsDBNull(0)) detail.OperRoleGpCode = dr.GetString(0);
					if (!dr.IsDBNull(1)) detail.ModuleCode = dr.GetString(1);
					if (!dr.IsDBNull(2)) detail.OperCode = dr.GetString(2);
                }
            }
			
			return detail;
		}
		
		
		public IDataReader find(
			System.String operRoleGpCode, 
			System.String moduleCode, 
			System.String operCode, 
			int startRowIndex, 
			int maximumRows){
			Database db = DatabaseFactory.CreateDatabase();
			IDataReader dr = null;
            DbCommand command = db.GetStoredProcCommand("usp_usyOperRoleDetailFind");
			db.AddParameter(command, "OperRoleGpCode", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, operRoleGpCode);
			db.AddParameter(command, "ModuleCode", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, moduleCode);
			db.AddParameter(command, "OperCode", DbType.String, 300, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, operCode);
			db.AddInParameter(command, "startRowIndex", DbType.Int32, startRowIndex);
			db.AddInParameter(command, "maximumRows", DbType.Int32, maximumRows);
			dr = db.ExecuteReader(command);
			return dr;
		}
		
		public IDataReader find(OperRoleDetailInfo operRoleDetail, int startRowIndex, int maximumRows){
			return find(
				operRoleDetail.OperRoleGpCode,  
				operRoleDetail.ModuleCode,  
				operRoleDetail.OperCode,  
				startRowIndex, 
				maximumRows);
		}
		
		public int findCount(
			System.String operRoleGpCode, 
			System.String moduleCode, 
			System.String operCode, 
			int startRowIndex, 
			int maximumRows){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_usyOperRoleDetailFindCount");
			db.AddParameter(command, "OperRoleGpCode", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, operRoleGpCode);
			db.AddParameter(command, "ModuleCode", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, moduleCode);
			db.AddParameter(command, "OperCode", DbType.String, 300, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, operCode);
			db.AddInParameter(command, "startRowIndex", DbType.Int32, startRowIndex);
			db.AddInParameter(command, "maximumRows", DbType.Int32, maximumRows);
			db.AddParameter( command, "ReturnValue", DbType.Int32, ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Default, null );
			
			db.ExecuteNonQuery(command);
			
			int recordCount = (int)db.GetParameterValue( command, "ReturnValue" );
			return recordCount;
		}
		
		public int findCount(OperRoleDetailInfo operRoleDetail, int startRowIndex, int maximumRows){
			return findCount(
				operRoleDetail.OperRoleGpCode, 
				operRoleDetail.ModuleCode, 
				operRoleDetail.OperCode, 
				startRowIndex, 
				maximumRows);
		}
	}
}