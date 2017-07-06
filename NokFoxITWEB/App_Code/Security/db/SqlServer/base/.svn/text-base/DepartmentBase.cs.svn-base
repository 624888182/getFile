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
	public abstract class DepartmentBase{
		
		/// <summary>
		/// If department is exist in database.
		/// </summary>
		/// <param name="department">Business entity representing the department.</param>
		/// <returns>bool. If exist return true, else return false.</returns>
		public bool isExist(DepartmentInfo department){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_usyDepartmentIsExist");
			
			db.AddInParameter(command, "DepartmentID", DbType.String,department.DepartmentID);
			
			int result = 0;
			result = db.ExecuteNonQuery(command);
			
			if( result > 0 ){
				return true;
			}else{
				return false;
			} 
		}
		
		/// <summary>
		/// Insert a department to Database.
		/// </summary>
		/// <param name="order">Business entity representing the department</param>
		/// <returns>OrderId</returns>
		public int Insert(DepartmentInfo department){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_usyDepartmentInsert");
			db.AddInParameter(command, "DepartmentID", DbType.String,department.DepartmentID);
			db.AddInParameter(command, "DepartmentCode", DbType.String,department.DepartmentCode);
			db.AddInParameter(command, "DepartmentName", DbType.String,department.DepartmentName);
			db.AddInParameter(command, "DeptEnName", DbType.String,department.DeptEnName);
			db.AddInParameter(command, "ParentID", DbType.String,department.ParentID);
			db.AddInParameter(command, "PriceCode", DbType.String,department.PriceCode);
			db.AddInParameter(command, "LevelCode", DbType.Int32,department.LevelCode);
			db.AddInParameter(command, "SiteCode", DbType.String,department.SiteCode);
			db.AddInParameter(command, "Enable", DbType.Boolean,department.Enable);
			db.AddInParameter(command, "StandbyField1", DbType.String,department.StandbyField1);
			db.AddInParameter(command, "StandbyField2", DbType.String,department.StandbyField2);
			db.AddInParameter(command, "StandbyField3", DbType.Decimal,department.StandbyField3);
			db.AddInParameter(command, "StandbyField4", DbType.Int32,department.StandbyField4);
			db.AddInParameter(command, "CreaterID", DbType.String,department.CreaterID);
			db.AddInParameter(command, "CreaterDate", DbType.DateTime,department.CreaterDate);
			db.AddInParameter(command, "ModiID", DbType.String,department.ModiID);
			db.AddInParameter(command, "ModiDate", DbType.DateTime,department.ModiDate);
			return db.ExecuteNonQuery(command);
		}
		
		/// <summary>
		/// Method to Update an department header
		/// </summary>
		/// <param name="order">Business entity representing the department</param>
		public int Update(DepartmentInfo department){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_usyDepartmentUpdate");
			db.AddInParameter(command, "DepartmentID", DbType.String,department.DepartmentID);
			db.AddInParameter(command, "DepartmentCode", DbType.String,department.DepartmentCode);
			db.AddInParameter(command, "DepartmentName", DbType.String,department.DepartmentName);
			db.AddInParameter(command, "DeptEnName", DbType.String,department.DeptEnName);
			db.AddInParameter(command, "ParentID", DbType.String,department.ParentID);
			db.AddInParameter(command, "PriceCode", DbType.String,department.PriceCode);
			db.AddInParameter(command, "LevelCode", DbType.Int32,department.LevelCode);
			db.AddInParameter(command, "SiteCode", DbType.String,department.SiteCode);
			db.AddInParameter(command, "Enable", DbType.Boolean,department.Enable);
			db.AddInParameter(command, "StandbyField1", DbType.String,department.StandbyField1);
			db.AddInParameter(command, "StandbyField2", DbType.String,department.StandbyField2);
			db.AddInParameter(command, "StandbyField3", DbType.Decimal,department.StandbyField3);
			db.AddInParameter(command, "StandbyField4", DbType.Int32,department.StandbyField4);
			db.AddInParameter(command, "CreaterID", DbType.String,department.CreaterID);
			db.AddInParameter(command, "CreaterDate", DbType.DateTime,department.CreaterDate);
			db.AddInParameter(command, "ModiID", DbType.String,department.ModiID);
			db.AddInParameter(command, "ModiDate", DbType.DateTime,department.ModiDate);
			return db.ExecuteNonQuery(command);
		}
		
		/// <summary>
		/// Method to delete an department header
		/// </summary>
		/// <param name="order">Business entity representing the department</param>
		public int Delete(DepartmentInfo department){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_usyDepartmentDelete");
			db.AddInParameter(command, "DepartmentID", DbType.String,department.DepartmentID);
			return db.ExecuteNonQuery(command);
		}
		
		public IDataReader getALL(){
			Database db = DatabaseFactory.CreateDatabase();
            IDataReader dr = null;
            DbCommand command = db.GetStoredProcCommand("usp_usyDepartmentGetAll");
			
            dr = db.ExecuteReader(command);
			
			return dr;
		}
		
		/// <summary>
		/// Reads the department information for a primary key
		/// </summary>
		/// <returns>Business entity representing the department</returns>
		
		public DepartmentInfo getDepartment(System.String departmentID){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_usyDepartmentGet");
			db.AddInParameter(command, "DepartmentID", DbType.String,departmentID);
			
			DepartmentInfo detail = new DepartmentInfo();

            using( IDataReader dr = db.ExecuteReader(command) ) {
                
                if( dr.Read() ){
					if (!dr.IsDBNull(0)) detail.DepartmentID = dr.GetString(0);
					if (!dr.IsDBNull(1)) detail.DepartmentCode = dr.GetString(1);
					if (!dr.IsDBNull(2)) detail.DepartmentName = dr.GetString(2);
					if (!dr.IsDBNull(3)) detail.DeptEnName = dr.GetString(3);
					if (!dr.IsDBNull(4)) detail.ParentID = dr.GetString(4);
					if (!dr.IsDBNull(5)) detail.PriceCode = dr.GetString(5);
					if (!dr.IsDBNull(6)) detail.LevelCode = dr.GetInt32(6);
					if (!dr.IsDBNull(7)) detail.SiteCode = dr.GetString(7);
					if (!dr.IsDBNull(8)) detail.Enable = dr.GetBoolean(8);
					if (!dr.IsDBNull(9)) detail.StandbyField1 = dr.GetString(9);
					if (!dr.IsDBNull(10)) detail.StandbyField2 = dr.GetString(10);
					if (!dr.IsDBNull(11)) detail.StandbyField3 = dr.GetDecimal(11);
					if (!dr.IsDBNull(12)) detail.StandbyField4 = dr.GetInt32(12);
					if (!dr.IsDBNull(13)) detail.CreaterID = dr.GetString(13);
					if (!dr.IsDBNull(14)) detail.CreaterDate = dr.GetDateTime(14);
					if (!dr.IsDBNull(15)) detail.ModiID = dr.GetString(15);
					if (!dr.IsDBNull(16)) detail.ModiDate = dr.GetDateTime(16);
                }
            }
			
			return detail;
		}
		
		
		public IDataReader find(
			System.String departmentID, 
			System.String departmentCode, 
			System.String departmentName, 
			System.String deptEnName, 
			System.String parentID, 
			System.String priceCode, 
			System.Int32? levelCode, 
			System.String siteCode, 
			System.Boolean? enable, 
			System.String standbyField1, 
			System.String standbyField2, 
			System.Decimal? standbyField3, 
			System.Int32? standbyField4, 
			System.String createrID, 
			System.DateTime? createrDate, 
			System.String modiID, 
			System.DateTime? modiDate, 
			int startRowIndex, 
			int maximumRows){
			Database db = DatabaseFactory.CreateDatabase();
			IDataReader dr = null;
            DbCommand command = db.GetStoredProcCommand("usp_usyDepartmentFind");
			db.AddParameter(command, "DepartmentID", DbType.String, 50, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, departmentID);
			db.AddParameter(command, "DepartmentCode", DbType.String, 50, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, departmentCode);
			db.AddParameter(command, "DepartmentName", DbType.String, 300, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, departmentName);
			db.AddParameter(command, "DeptEnName", DbType.String, 300, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, deptEnName);
			db.AddParameter(command, "ParentID", DbType.String, 50, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, parentID);
			db.AddParameter(command, "PriceCode", DbType.String, 50, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, priceCode);
			db.AddParameter(command, "LevelCode", DbType.Int32, 4, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, levelCode);
			db.AddParameter(command, "SiteCode", DbType.String, 50, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, siteCode);
			db.AddParameter(command, "Enable", DbType.Boolean, 1, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, enable);
			db.AddParameter(command, "StandbyField1", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, standbyField1);
			db.AddParameter(command, "StandbyField2", DbType.String, 300, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, standbyField2);
			db.AddParameter(command, "StandbyField3", DbType.Decimal, 9, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, standbyField3);
			db.AddParameter(command, "StandbyField4", DbType.Int32, 4, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, standbyField4);
			db.AddParameter(command, "CreaterID", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, createrID);
			db.AddParameter(command, "CreaterDate", DbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, createrDate);
			db.AddParameter(command, "ModiID", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, modiID);
			db.AddParameter(command, "ModiDate", DbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, modiDate);
			db.AddInParameter(command, "startRowIndex", DbType.Int32, startRowIndex);
			db.AddInParameter(command, "maximumRows", DbType.Int32, maximumRows);
			dr = db.ExecuteReader(command);
			return dr;
		}
		
		public IDataReader find(DepartmentInfo department, int startRowIndex, int maximumRows){
			return find(
				department.DepartmentID,  
				department.DepartmentCode,  
				department.DepartmentName,  
				department.DeptEnName,  
				department.ParentID,  
				department.PriceCode,  
				department.LevelCode,  
				department.SiteCode,  
				department.Enable,  
				department.StandbyField1,  
				department.StandbyField2,  
				department.StandbyField3,  
				department.StandbyField4,  
				department.CreaterID,  
				department.CreaterDate,  
				department.ModiID,  
				department.ModiDate,  
				startRowIndex, 
				maximumRows);
		}
		
		public int findCount(
			System.String departmentID, 
			System.String departmentCode, 
			System.String departmentName, 
			System.String deptEnName, 
			System.String parentID, 
			System.String priceCode, 
			System.Int32? levelCode, 
			System.String siteCode, 
			System.Boolean? enable, 
			System.String standbyField1, 
			System.String standbyField2, 
			System.Decimal? standbyField3, 
			System.Int32? standbyField4, 
			System.String createrID, 
			System.DateTime? createrDate, 
			System.String modiID, 
			System.DateTime? modiDate, 
			int startRowIndex, 
			int maximumRows){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_usyDepartmentFindCount");
			db.AddParameter(command, "DepartmentID", DbType.String, 50, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, departmentID);
			db.AddParameter(command, "DepartmentCode", DbType.String, 50, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, departmentCode);
			db.AddParameter(command, "DepartmentName", DbType.String, 300, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, departmentName);
			db.AddParameter(command, "DeptEnName", DbType.String, 300, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, deptEnName);
			db.AddParameter(command, "ParentID", DbType.String, 50, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, parentID);
			db.AddParameter(command, "PriceCode", DbType.String, 50, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, priceCode);
			db.AddParameter(command, "LevelCode", DbType.Int32, 4, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, levelCode);
			db.AddParameter(command, "SiteCode", DbType.String, 50, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, siteCode);
			db.AddParameter(command, "Enable", DbType.Boolean, 1, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, enable);
			db.AddParameter(command, "StandbyField1", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, standbyField1);
			db.AddParameter(command, "StandbyField2", DbType.String, 300, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, standbyField2);
			db.AddParameter(command, "StandbyField3", DbType.Decimal, 9, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, standbyField3);
			db.AddParameter(command, "StandbyField4", DbType.Int32, 4, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, standbyField4);
			db.AddParameter(command, "CreaterID", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, createrID);
			db.AddParameter(command, "CreaterDate", DbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, createrDate);
			db.AddParameter(command, "ModiID", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, modiID);
			db.AddParameter(command, "ModiDate", DbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, modiDate);
			db.AddInParameter(command, "startRowIndex", DbType.Int32, startRowIndex);
			db.AddInParameter(command, "maximumRows", DbType.Int32, maximumRows);
			db.AddParameter( command, "ReturnValue", DbType.Int32, ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Default, null );
			
			db.ExecuteNonQuery(command);
			
			int recordCount = (int)db.GetParameterValue( command, "ReturnValue" );
			return recordCount;
		}
		
		public int findCount(DepartmentInfo department, int startRowIndex, int maximumRows){
			return findCount(
				department.DepartmentID, 
				department.DepartmentCode, 
				department.DepartmentName, 
				department.DeptEnName, 
				department.ParentID, 
				department.PriceCode, 
				department.LevelCode, 
				department.SiteCode, 
				department.Enable, 
				department.StandbyField1, 
				department.StandbyField2, 
				department.StandbyField3, 
				department.StandbyField4, 
				department.CreaterID, 
				department.CreaterDate, 
				department.ModiID, 
				department.ModiDate, 
				startRowIndex, 
				maximumRows);
		}
	}
}