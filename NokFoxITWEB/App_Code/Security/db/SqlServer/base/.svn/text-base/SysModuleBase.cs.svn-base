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
	public abstract class SysModuleBase{
		
		/// <summary>
		/// If sysModule is exist in database.
		/// </summary>
		/// <param name="sysModule">Business entity representing the sysModule.</param>
		/// <returns>bool. If exist return true, else return false.</returns>
		public bool isExist(SysModuleInfo sysModule){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_usySysModuleIsExist");
			
			db.AddInParameter(command, "ModuleCode", DbType.String,sysModule.ModuleCode);
			
			int result = 0;
			result = db.ExecuteNonQuery(command);
			
			if( result > 0 ){
				return true;
			}else{
				return false;
			} 
		}
		
		/// <summary>
		/// Insert a sysModule to Database.
		/// </summary>
		/// <param name="order">Business entity representing the sysModule</param>
		/// <returns>OrderId</returns>
		public int Insert(SysModuleInfo sysModule){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_usySysModuleInsert");
			db.AddInParameter(command, "ModuleCode", DbType.String,sysModule.ModuleCode);
			db.AddInParameter(command, "ModuleNameCn", DbType.String,sysModule.ModuleNameCn);
			db.AddInParameter(command, "ModuleNameEn", DbType.String,sysModule.ModuleNameEn);
			db.AddInParameter(command, "SeqNo", DbType.Int32,sysModule.SeqNo);
			db.AddInParameter(command, "ParentModuleCode", DbType.String,sysModule.ParentModuleCode);
			db.AddInParameter(command, "OperCodeGroup", DbType.String,sysModule.OperCodeGroup);
			db.AddInParameter(command, "URL", DbType.String,sysModule.URL);
			db.AddInParameter(command, "SysName", DbType.String,sysModule.SysName);
			db.AddInParameter(command, "IsOperModule", DbType.String,sysModule.IsOperModule);
			db.AddInParameter(command, "IsRole", DbType.String,sysModule.IsRole);
			return db.ExecuteNonQuery(command);
		}
		
		/// <summary>
		/// Method to Update an sysModule header
		/// </summary>
		/// <param name="order">Business entity representing the sysModule</param>
		public int Update(SysModuleInfo sysModule){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_usySysModuleUpdate");
			db.AddInParameter(command, "ModuleCode", DbType.String,sysModule.ModuleCode);
			db.AddInParameter(command, "ModuleNameCn", DbType.String,sysModule.ModuleNameCn);
			db.AddInParameter(command, "ModuleNameEn", DbType.String,sysModule.ModuleNameEn);
			db.AddInParameter(command, "SeqNo", DbType.Int32,sysModule.SeqNo);
			db.AddInParameter(command, "ParentModuleCode", DbType.String,sysModule.ParentModuleCode);
			db.AddInParameter(command, "OperCodeGroup", DbType.String,sysModule.OperCodeGroup);
			db.AddInParameter(command, "URL", DbType.String,sysModule.URL);
			db.AddInParameter(command, "SysName", DbType.String,sysModule.SysName);
			db.AddInParameter(command, "IsOperModule", DbType.String,sysModule.IsOperModule);
			db.AddInParameter(command, "IsRole", DbType.String,sysModule.IsRole);
			return db.ExecuteNonQuery(command);
		}
		
		/// <summary>
		/// Method to delete an sysModule header
		/// </summary>
		/// <param name="order">Business entity representing the sysModule</param>
		public int Delete(SysModuleInfo sysModule){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_usySysModuleDelete");
			db.AddInParameter(command, "ModuleCode", DbType.String,sysModule.ModuleCode);
			return db.ExecuteNonQuery(command);
		}
		
		public IDataReader getALL(){
			Database db = DatabaseFactory.CreateDatabase();
            IDataReader dr = null;
            DbCommand command = db.GetStoredProcCommand("usp_usySysModuleGetAll");
			
            dr = db.ExecuteReader(command);
			
			return dr;
		}
		
		/// <summary>
		/// Reads the sysModule information for a primary key
		/// </summary>
		/// <returns>Business entity representing the sysModule</returns>
		
		public SysModuleInfo getSysModule(System.String moduleCode){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_usySysModuleGet");
			db.AddInParameter(command, "ModuleCode", DbType.String,moduleCode);
			
			SysModuleInfo detail = new SysModuleInfo();

            using( IDataReader dr = db.ExecuteReader(command) ) {
                
                if( dr.Read() ){
					if (!dr.IsDBNull(0)) detail.ModuleCode = dr.GetString(0);
					if (!dr.IsDBNull(1)) detail.ModuleNameCn = dr.GetString(1);
					if (!dr.IsDBNull(2)) detail.ModuleNameEn = dr.GetString(2);
					if (!dr.IsDBNull(3)) detail.SeqNo = dr.GetInt32(3);
					if (!dr.IsDBNull(4)) detail.ParentModuleCode = dr.GetString(4);
					if (!dr.IsDBNull(5)) detail.OperCodeGroup = dr.GetString(5);
					if (!dr.IsDBNull(6)) detail.URL = dr.GetString(6);
					if (!dr.IsDBNull(7)) detail.SysName = dr.GetString(7);
					if (!dr.IsDBNull(8)) detail.IsOperModule = dr.GetString(8);
					if (!dr.IsDBNull(9)) detail.IsRole = dr.GetString(9);
                }
            }
			
			return detail;
		}
		
		
		public IDataReader find(
			System.String moduleCode, 
			System.String moduleNameCn, 
			System.String moduleNameEn, 
			System.Int32? seqNo, 
			System.String parentModuleCode, 
			System.String operCodeGroup, 
			System.String url, 
			System.String sysName, 
			System.String isOperModule, 
			System.String isRole, 
			int startRowIndex, 
			int maximumRows){
			Database db = DatabaseFactory.CreateDatabase();
			IDataReader dr = null;
            DbCommand command = db.GetStoredProcCommand("usp_usySysModuleFind");
			db.AddParameter(command, "ModuleCode", DbType.String, 20, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, moduleCode);
			db.AddParameter(command, "ModuleNameCn", DbType.String, 80, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, moduleNameCn);
			db.AddParameter(command, "ModuleNameEn", DbType.String, 80, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, moduleNameEn);
			db.AddParameter(command, "SeqNo", DbType.Int32, 4, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, seqNo);
			db.AddParameter(command, "ParentModuleCode", DbType.String, 20, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, parentModuleCode);
			db.AddParameter(command, "OperCodeGroup", DbType.String, 100, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, operCodeGroup);
			db.AddParameter(command, "URL", DbType.String, 80, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, url);
			db.AddParameter(command, "SysName", DbType.String, 40, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, sysName);
			db.AddParameter(command, "IsOperModule", DbType.String, 1, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, isOperModule);
			db.AddParameter(command, "IsRole", DbType.String, 1, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, isRole);
			db.AddInParameter(command, "startRowIndex", DbType.Int32, startRowIndex);
			db.AddInParameter(command, "maximumRows", DbType.Int32, maximumRows);
			dr = db.ExecuteReader(command);
			return dr;
		}
		
		public IDataReader find(SysModuleInfo sysModule, int startRowIndex, int maximumRows){
			return find(
				sysModule.ModuleCode,  
				sysModule.ModuleNameCn,  
				sysModule.ModuleNameEn,  
				sysModule.SeqNo,  
				sysModule.ParentModuleCode,  
				sysModule.OperCodeGroup,  
				sysModule.URL,  
				sysModule.SysName,  
				sysModule.IsOperModule,  
				sysModule.IsRole,  
				startRowIndex, 
				maximumRows);
		}
		
		public int findCount(
			System.String moduleCode, 
			System.String moduleNameCn, 
			System.String moduleNameEn, 
			System.Int32? seqNo, 
			System.String parentModuleCode, 
			System.String operCodeGroup, 
			System.String url, 
			System.String sysName, 
			System.String isOperModule, 
			System.String isRole, 
			int startRowIndex, 
			int maximumRows){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_usySysModuleFindCount");
			db.AddParameter(command, "ModuleCode", DbType.String, 20, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, moduleCode);
			db.AddParameter(command, "ModuleNameCn", DbType.String, 80, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, moduleNameCn);
			db.AddParameter(command, "ModuleNameEn", DbType.String, 80, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, moduleNameEn);
			db.AddParameter(command, "SeqNo", DbType.Int32, 4, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, seqNo);
			db.AddParameter(command, "ParentModuleCode", DbType.String, 20, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, parentModuleCode);
			db.AddParameter(command, "OperCodeGroup", DbType.String, 100, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, operCodeGroup);
			db.AddParameter(command, "URL", DbType.String, 80, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, url);
			db.AddParameter(command, "SysName", DbType.String, 40, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, sysName);
			db.AddParameter(command, "IsOperModule", DbType.String, 1, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, isOperModule);
			db.AddParameter(command, "IsRole", DbType.String, 1, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, isRole);
			db.AddInParameter(command, "startRowIndex", DbType.Int32, startRowIndex);
			db.AddInParameter(command, "maximumRows", DbType.Int32, maximumRows);
			db.AddParameter( command, "ReturnValue", DbType.Int32, ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Default, null );
			
			db.ExecuteNonQuery(command);
			
			int recordCount = (int)db.GetParameterValue( command, "ReturnValue" );
			return recordCount;
		}
		
		public int findCount(SysModuleInfo sysModule, int startRowIndex, int maximumRows){
			return findCount(
				sysModule.ModuleCode, 
				sysModule.ModuleNameCn, 
				sysModule.ModuleNameEn, 
				sysModule.SeqNo, 
				sysModule.ParentModuleCode, 
				sysModule.OperCodeGroup, 
				sysModule.URL, 
				sysModule.SysName, 
				sysModule.IsOperModule, 
				sysModule.IsRole, 
				startRowIndex, 
				maximumRows);
		}
	}
}