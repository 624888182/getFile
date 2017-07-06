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
	public abstract class SysConfigBase{
		
		/// <summary>
		/// If sysConfig is exist in database.
		/// </summary>
		/// <param name="sysConfig">Business entity representing the sysConfig.</param>
		/// <returns>bool. If exist return true, else return false.</returns>
		public bool isExist(SysConfigInfo sysConfig){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_usySysConfigIsExist");
			
			db.AddInParameter(command, "PageRow", DbType.String,sysConfig.PageRow);
			
			int result = 0;
			result = db.ExecuteNonQuery(command);
			
			if( result > 0 ){
				return true;
			}else{
				return false;
			} 
		}
		
		/// <summary>
		/// Insert a sysConfig to Database.
		/// </summary>
		/// <param name="order">Business entity representing the sysConfig</param>
		/// <returns>OrderId</returns>
		public int Insert(SysConfigInfo sysConfig){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_usySysConfigInsert");
			db.AddInParameter(command, "PageRow", DbType.String,sysConfig.PageRow);
			db.AddInParameter(command, "DefaultPasswd", DbType.String,sysConfig.DefaultPasswd);
			return db.ExecuteNonQuery(command);
		}
		
		/// <summary>
		/// Method to Update an sysConfig header
		/// </summary>
		/// <param name="order">Business entity representing the sysConfig</param>
		public int Update(SysConfigInfo sysConfig){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_usySysConfigUpdate");
			db.AddInParameter(command, "PageRow", DbType.String,sysConfig.PageRow);
			db.AddInParameter(command, "DefaultPasswd", DbType.String,sysConfig.DefaultPasswd);
			return db.ExecuteNonQuery(command);
		}
		
		/// <summary>
		/// Method to delete an sysConfig header
		/// </summary>
		/// <param name="order">Business entity representing the sysConfig</param>
		public int Delete(SysConfigInfo sysConfig){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_usySysConfigDelete");
			db.AddInParameter(command, "PageRow", DbType.String,sysConfig.PageRow);
			return db.ExecuteNonQuery(command);
		}
		
		public IDataReader getALL(){
			Database db = DatabaseFactory.CreateDatabase();
            IDataReader dr = null;
            DbCommand command = db.GetStoredProcCommand("usp_usySysConfigGetAll");
			
            dr = db.ExecuteReader(command);
			
			return dr;
		}
		
		/// <summary>
		/// Reads the sysConfig information for a primary key
		/// </summary>
		/// <returns>Business entity representing the sysConfig</returns>
		
		public SysConfigInfo getSysConfig(System.String pageRow){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_usySysConfigGet");
			db.AddInParameter(command, "PageRow", DbType.String,pageRow);
			
			SysConfigInfo detail = new SysConfigInfo();

            using( IDataReader dr = db.ExecuteReader(command) ) {
                
                if( dr.Read() ){
					if (!dr.IsDBNull(0)) detail.PageRow = dr.GetString(0);
					if (!dr.IsDBNull(1)) detail.DefaultPasswd = dr.GetString(1);
                }
            }
			
			return detail;
		}
		
		
		public IDataReader find(
			System.String pageRow, 
			System.String defaultPasswd, 
			int startRowIndex, 
			int maximumRows){
			Database db = DatabaseFactory.CreateDatabase();
			IDataReader dr = null;
            DbCommand command = db.GetStoredProcCommand("usp_usySysConfigFind");
			db.AddParameter(command, "PageRow", DbType.String, 20, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, pageRow);
			db.AddParameter(command, "DefaultPasswd", DbType.String, 80, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, defaultPasswd);
			db.AddInParameter(command, "startRowIndex", DbType.Int32, startRowIndex);
			db.AddInParameter(command, "maximumRows", DbType.Int32, maximumRows);
			dr = db.ExecuteReader(command);
			return dr;
		}
		
		public IDataReader find(SysConfigInfo sysConfig, int startRowIndex, int maximumRows){
			return find(
				sysConfig.PageRow,  
				sysConfig.DefaultPasswd,  
				startRowIndex, 
				maximumRows);
		}
		
		public int findCount(
			System.String pageRow, 
			System.String defaultPasswd, 
			int startRowIndex, 
			int maximumRows){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_usySysConfigFindCount");
			db.AddParameter(command, "PageRow", DbType.String, 20, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, pageRow);
			db.AddParameter(command, "DefaultPasswd", DbType.String, 80, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, defaultPasswd);
			db.AddInParameter(command, "startRowIndex", DbType.Int32, startRowIndex);
			db.AddInParameter(command, "maximumRows", DbType.Int32, maximumRows);
			db.AddParameter( command, "ReturnValue", DbType.Int32, ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Default, null );
			
			db.ExecuteNonQuery(command);
			
			int recordCount = (int)db.GetParameterValue( command, "ReturnValue" );
			return recordCount;
		}
		
		public int findCount(SysConfigInfo sysConfig, int startRowIndex, int maximumRows){
			return findCount(
				sysConfig.PageRow, 
				sysConfig.DefaultPasswd, 
				startRowIndex, 
				maximumRows);
		}
	}
}