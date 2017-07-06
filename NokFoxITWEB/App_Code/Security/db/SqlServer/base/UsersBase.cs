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
	public abstract class UsersBase{
		
		/// <summary>
		/// If users is exist in database.
		/// </summary>
		/// <param name="users">Business entity representing the users.</param>
		/// <returns>bool. If exist return true, else return false.</returns>
		public bool isExist(UsersInfo users){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_usyUsersIsExist");
			
			db.AddInParameter(command, "UserID", DbType.String,users.UserID);
			
			int result = 0;
			result = db.ExecuteNonQuery(command);
			
			if( result > 0 ){
				return true;
			}else{
				return false;
			} 
		}
		
		/// <summary>
		/// Insert a users to Database.
		/// </summary>
		/// <param name="order">Business entity representing the users</param>
		/// <returns>OrderId</returns>
		public int Insert(UsersInfo users){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_usyUsersInsert");
			db.AddInParameter(command, "UserID", DbType.String,users.UserID);
			db.AddInParameter(command, "UserName", DbType.String,users.UserName);
			db.AddInParameter(command, "DeptNo", DbType.String,users.DeptNo);
			db.AddInParameter(command, "OperRoleGpCode", DbType.String,users.OperRoleGpCode);
			db.AddInParameter(command, "Mail", DbType.String,users.Mail);
			db.AddInParameter(command, "Tel", DbType.String,users.Tel);
			db.AddInParameter(command, "PassWD", DbType.String,users.PassWD);
			db.AddInParameter(command, "IsOnLine", DbType.Boolean,users.IsOnLine);
			db.AddInParameter(command, "GradeCode", DbType.String,users.GradeCode);
			db.AddInParameter(command, "GradeName", DbType.String,users.GradeName);
			db.AddInParameter(command, "PositionCode", DbType.String,users.PositionCode);
			db.AddInParameter(command, "PositionName", DbType.String,users.PositionName);
			db.AddInParameter(command, "PositionSeries", DbType.String,users.PositionSeries);
			db.AddInParameter(command, "PositionSeriesName", DbType.String,users.PositionSeriesName);
			db.AddInParameter(command, "InCompanyDate", DbType.DateTime,users.InCompanyDate);
			db.AddInParameter(command, "Status", DbType.Int32,users.Status);
			db.AddInParameter(command, "Remark", DbType.String,users.Remark);
			return db.ExecuteNonQuery(command);
		}
		
		/// <summary>
		/// Method to Update an users header
		/// </summary>
		/// <param name="order">Business entity representing the users</param>
		public int Update(UsersInfo users){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_usyUsersUpdate");
			db.AddInParameter(command, "UserID", DbType.String,users.UserID);
			db.AddInParameter(command, "UserName", DbType.String,users.UserName);
			db.AddInParameter(command, "DeptNo", DbType.String,users.DeptNo);
			db.AddInParameter(command, "OperRoleGpCode", DbType.String,users.OperRoleGpCode);
			db.AddInParameter(command, "Mail", DbType.String,users.Mail);
			db.AddInParameter(command, "Tel", DbType.String,users.Tel);
			db.AddInParameter(command, "PassWD", DbType.String,users.PassWD);
            db.AddInParameter(command, "IsOnLine", DbType.Boolean,users.IsOnLine);
			db.AddInParameter(command, "GradeCode", DbType.String,users.GradeCode);
			db.AddInParameter(command, "GradeName", DbType.String,users.GradeName);
			db.AddInParameter(command, "PositionCode", DbType.String,users.PositionCode);
			db.AddInParameter(command, "PositionName", DbType.String,users.PositionName);
			db.AddInParameter(command, "PositionSeries", DbType.String,users.PositionSeries);
			db.AddInParameter(command, "PositionSeriesName", DbType.String,users.PositionSeriesName);
			db.AddInParameter(command, "InCompanyDate", DbType.DateTime,users.InCompanyDate);
			db.AddInParameter(command, "Status", DbType.Int32,users.Status);
			db.AddInParameter(command, "Remark", DbType.String,users.Remark);
			return db.ExecuteNonQuery(command);
		}
		
		/// <summary>
		/// Method to delete an users header
		/// </summary>
		/// <param name="order">Business entity representing the users</param>
		public int Delete(UsersInfo users){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_usyUsersDelete");
			db.AddInParameter(command, "UserID", DbType.String,users.UserID);
			return db.ExecuteNonQuery(command);
		}
		
		public IDataReader getALL(){
			Database db = DatabaseFactory.CreateDatabase();
            IDataReader dr = null;
            DbCommand command = db.GetStoredProcCommand("usp_usyUsersGetAll");
			
            dr = db.ExecuteReader(command);
			
			return dr;
		}
		
		/// <summary>
		/// Reads the users information for a primary key
		/// </summary>
		/// <returns>Business entity representing the users</returns>
		
		public UsersInfo getUsers(System.String userID){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_usyUsersGet");
			db.AddInParameter(command, "UserID", DbType.String,userID);
			
			UsersInfo detail = new UsersInfo();

            using( IDataReader dr = db.ExecuteReader(command) ) {
                
                if( dr.Read() ){
					if (!dr.IsDBNull(0)) detail.UserID = dr.GetString(0);
					if (!dr.IsDBNull(1)) detail.UserName = dr.GetString(1);
					if (!dr.IsDBNull(2)) detail.DeptNo = dr.GetString(2);
					if (!dr.IsDBNull(3)) detail.OperRoleGpCode = dr.GetString(3);
					if (!dr.IsDBNull(4)) detail.Mail = dr.GetString(4);
					if (!dr.IsDBNull(5)) detail.Tel = dr.GetString(5);
					if (!dr.IsDBNull(6)) detail.PassWD = dr.GetString(6);
					if (!dr.IsDBNull(7)) detail.IsOnLine = dr.GetBoolean(7);
					if (!dr.IsDBNull(8)) detail.GradeCode = dr.GetString(8);
					if (!dr.IsDBNull(9)) detail.GradeName = dr.GetString(9);
					if (!dr.IsDBNull(10)) detail.PositionCode = dr.GetString(10);
					if (!dr.IsDBNull(11)) detail.PositionName = dr.GetString(11);
					if (!dr.IsDBNull(12)) detail.PositionSeries = dr.GetString(12);
					if (!dr.IsDBNull(13)) detail.PositionSeriesName = dr.GetString(13);
					if (!dr.IsDBNull(14)) detail.InCompanyDate = dr.GetDateTime(14);
					if (!dr.IsDBNull(15)) detail.Status = dr.GetInt32(15);
					if (!dr.IsDBNull(16)) detail.Remark = dr.GetString(16);
                }
            }
			
			return detail;
		}
		
		
		public IDataReader find(
			System.String userID, 
			System.String userName, 
			System.String deptNo, 
			System.String operRoleGpCode, 
			System.String mail, 
			System.String tel, 
			System.String passWD, 
			System.Boolean? isOnLine, 
			System.String gradeCode, 
			System.String gradeName, 
			System.String positionCode, 
			System.String positionName, 
			System.String positionSeries, 
			System.String positionSeriesName, 
			System.DateTime? inCompanyDate, 
			System.Int32? status, 
			System.String remark, 
			int startRowIndex, 
			int maximumRows){
			Database db = DatabaseFactory.CreateDatabase();
			IDataReader dr = null;
            DbCommand command = db.GetStoredProcCommand("usp_usyUsersFind");
			db.AddParameter(command, "UserID", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, userID);
			db.AddParameter(command, "UserName", DbType.String, 50, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, userName);
			db.AddParameter(command, "DeptNo", DbType.String, 50, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, deptNo);
			db.AddParameter(command, "OperRoleGpCode", DbType.String, 50, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, operRoleGpCode);
			db.AddParameter(command, "Mail", DbType.String, 80, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, mail);
			db.AddParameter(command, "Tel", DbType.String, 50, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, tel);
			db.AddParameter(command, "PassWD", DbType.String, 50, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, passWD);
			db.AddParameter(command, "IsOnLine", DbType.Boolean, 1, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, isOnLine);
			db.AddParameter(command, "GradeCode", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, gradeCode);
			db.AddParameter(command, "GradeName", DbType.String, 50, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, gradeName);
			db.AddParameter(command, "PositionCode", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, positionCode);
			db.AddParameter(command, "PositionName", DbType.String, 50, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, positionName);
			db.AddParameter(command, "PositionSeries", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, positionSeries);
			db.AddParameter(command, "PositionSeriesName", DbType.String, 50, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, positionSeriesName);
			db.AddParameter(command, "InCompanyDate", DbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, inCompanyDate);
			db.AddParameter(command, "Status", DbType.Int32, 4, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, status);
			db.AddParameter(command, "Remark", DbType.String, 200, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, remark);
			db.AddInParameter(command, "startRowIndex", DbType.Int32, startRowIndex);
			db.AddInParameter(command, "maximumRows", DbType.Int32, maximumRows);
			dr = db.ExecuteReader(command);
			return dr;
		}
		
		public IDataReader find(UsersInfo users, int startRowIndex, int maximumRows){
			return find(
				users.UserID,  
				users.UserName,  
				users.DeptNo,  
				users.OperRoleGpCode,  
				users.Mail,  
				users.Tel,  
				users.PassWD,  
				users.IsOnLine,  
				users.GradeCode,  
				users.GradeName,  
				users.PositionCode,  
				users.PositionName,  
				users.PositionSeries,  
				users.PositionSeriesName,  
				users.InCompanyDate,  
				users.Status,  
				users.Remark,  
				startRowIndex, 
				maximumRows);
		}
		
		public int findCount(
			System.String userID, 
			System.String userName, 
			System.String deptNo, 
			System.String operRoleGpCode, 
			System.String mail, 
			System.String tel, 
			System.String passWD, 
			System.Boolean? isOnLine, 
			System.String gradeCode, 
			System.String gradeName, 
			System.String positionCode, 
			System.String positionName, 
			System.String positionSeries, 
			System.String positionSeriesName, 
			System.DateTime? inCompanyDate, 
			System.Int32? status, 
			System.String remark, 
			int startRowIndex, 
			int maximumRows){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_usyUsersFindCount");
			db.AddParameter(command, "UserID", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, userID);
			db.AddParameter(command, "UserName", DbType.String, 50, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, userName);
			db.AddParameter(command, "DeptNo", DbType.String, 50, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, deptNo);
			db.AddParameter(command, "OperRoleGpCode", DbType.String, 50, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, operRoleGpCode);
			db.AddParameter(command, "Mail", DbType.String, 80, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, mail);
			db.AddParameter(command, "Tel", DbType.String, 50, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, tel);
			db.AddParameter(command, "PassWD", DbType.String, 50, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, passWD);
			db.AddParameter(command, "IsOnLine", DbType.Boolean, 1, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, isOnLine);
			db.AddParameter(command, "GradeCode", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, gradeCode);
			db.AddParameter(command, "GradeName", DbType.String, 50, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, gradeName);
			db.AddParameter(command, "PositionCode", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, positionCode);
			db.AddParameter(command, "PositionName", DbType.String, 50, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, positionName);
			db.AddParameter(command, "PositionSeries", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, positionSeries);
			db.AddParameter(command, "PositionSeriesName", DbType.String, 50, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, positionSeriesName);
			db.AddParameter(command, "InCompanyDate", DbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, inCompanyDate);
			db.AddParameter(command, "Status", DbType.Int32, 4, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, status);
			db.AddParameter(command, "Remark", DbType.String, 200, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, remark);
			db.AddInParameter(command, "startRowIndex", DbType.Int32, startRowIndex);
			db.AddInParameter(command, "maximumRows", DbType.Int32, maximumRows);
			db.AddParameter( command, "ReturnValue", DbType.Int32, ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Default, null );
			
			db.ExecuteNonQuery(command);
			
			int recordCount = (int)db.GetParameterValue( command, "ReturnValue" );
			return recordCount;
		}
		
		public int findCount(UsersInfo users, int startRowIndex, int maximumRows){
			return findCount(
				users.UserID, 
				users.UserName, 
				users.DeptNo, 
				users.OperRoleGpCode, 
				users.Mail, 
				users.Tel, 
				users.PassWD, 
				users.IsOnLine, 
				users.GradeCode, 
				users.GradeName, 
				users.PositionCode, 
				users.PositionName, 
				users.PositionSeries, 
				users.PositionSeriesName, 
				users.InCompanyDate, 
				users.Status, 
				users.Remark, 
				startRowIndex, 
				maximumRows);
		}
	}
}