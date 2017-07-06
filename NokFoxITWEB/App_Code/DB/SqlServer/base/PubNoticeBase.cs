using System;
using System.Data;
using System.Data.Common;

using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.ObjectBuilder;

namespace FIH.ForeignStaff.db{
	/// <summary>
	///
	/// </summary>
	public abstract class PubNoticeBase{
		
		/// <summary>
		/// If pubNotice is exist in database.
		/// </summary>
		/// <param name="pubNotice">Business entity representing the pubNotice.</param>
		/// <returns>bool. If exist return true, else return false.</returns>
		public bool isExist(PubNoticeInfo pubNotice){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_pubNoticeIsExist");
			
			db.AddInParameter(command, "NoticeCode", DbType.AnsiString,pubNotice.NoticeCode);
			
			int result = 0;
			result = db.ExecuteNonQuery(command);
			
			if( result > 0 ){
				return true;
			}else{
				return false;
			} 
		}
		
		/// <summary>
		/// Insert a pubNotice to Database.
		/// </summary>
		/// <param name="order">Business entity representing the pubNotice</param>
		/// <returns>OrderId</returns>
		public int Insert(PubNoticeInfo pubNotice){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_pubNoticeInsert");
			db.AddInParameter(command, "NoticeCode", DbType.AnsiString,pubNotice.NoticeCode);
			db.AddInParameter(command, "Title", DbType.String,pubNotice.Title);
			db.AddInParameter(command, "Memo", DbType.String,pubNotice.Memo);
			db.AddInParameter(command, "ModifyUser", DbType.AnsiString,pubNotice.ModifyUser);
			db.AddInParameter(command, "ModifyDate", DbType.DateTime,pubNotice.ModifyDate);
			db.AddInParameter(command, "CreateUser", DbType.AnsiString,pubNotice.CreateUser);
			db.AddInParameter(command, "CreateDate", DbType.DateTime,pubNotice.CreateDate);
			return db.ExecuteNonQuery(command);
		}
		
		/// <summary>
		/// Method to Update an pubNotice header
		/// </summary>
		/// <param name="order">Business entity representing the pubNotice</param>
		public int Update(PubNoticeInfo pubNotice){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_pubNoticeUpdate");
			db.AddInParameter(command, "NoticeCode", DbType.AnsiString,pubNotice.NoticeCode);
			db.AddInParameter(command, "Title", DbType.String,pubNotice.Title);
			db.AddInParameter(command, "Memo", DbType.String,pubNotice.Memo);
			db.AddInParameter(command, "ModifyUser", DbType.AnsiString,pubNotice.ModifyUser);
			db.AddInParameter(command, "ModifyDate", DbType.DateTime,pubNotice.ModifyDate);
			db.AddInParameter(command, "CreateUser", DbType.AnsiString,pubNotice.CreateUser);
			db.AddInParameter(command, "CreateDate", DbType.DateTime,pubNotice.CreateDate);
			return db.ExecuteNonQuery(command);
		}
		
		/// <summary>
		/// Method to delete an pubNotice header
		/// </summary>
		/// <param name="order">Business entity representing the pubNotice</param>
		public int Delete(PubNoticeInfo pubNotice){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_pubNoticeDelete");
			db.AddInParameter(command, "NoticeCode", DbType.AnsiString,pubNotice.NoticeCode);
			return db.ExecuteNonQuery(command);
		}
		
		public IDataReader getALL(){
			Database db = DatabaseFactory.CreateDatabase();
            IDataReader dr = null;
            DbCommand command = db.GetStoredProcCommand("usp_pubNoticeGetAll");
			
            dr = db.ExecuteReader(command);
			
			return dr;
		}
		
		/// <summary>
		/// Reads the pubNotice information for a primary key
		/// </summary>
		/// <returns>Business entity representing the pubNotice</returns>
		
		public PubNoticeInfo getPubNotice(System.String noticeCode){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_pubNoticeGet");
			db.AddInParameter(command, "NoticeCode", DbType.AnsiString,noticeCode);
			
			PubNoticeInfo detail = new PubNoticeInfo();

            using( IDataReader dr = db.ExecuteReader(command) ) {
                
                if( dr.Read() ){
					if (!dr.IsDBNull(0)) detail.NoticeCode = dr.GetString(0);
					if (!dr.IsDBNull(1)) detail.Title = dr.GetString(1);
					if (!dr.IsDBNull(2)) detail.Memo = dr.GetString(2);
					if (!dr.IsDBNull(3)) detail.ModifyUser = dr.GetString(3);
					if (!dr.IsDBNull(4)) detail.ModifyDate = dr.GetDateTime(4);
					if (!dr.IsDBNull(5)) detail.CreateUser = dr.GetString(5);
					if (!dr.IsDBNull(6)) detail.CreateDate = dr.GetDateTime(6);
                }
            }
			
			return detail;
		}
		
		
		public IDataReader find(
			System.String noticeCode, 
			System.String title, 
			System.String memo, 
			System.String modifyUser, 
			System.DateTime? modifyDate, 
			System.String createUser, 
			System.DateTime? createDate, 
			int startRowIndex, 
			int maximumRows){
			Database db = DatabaseFactory.CreateDatabase();
			IDataReader dr = null;
            DbCommand command = db.GetStoredProcCommand("usp_pubNoticeFind");
			db.AddParameter(command, "NoticeCode", DbType.AnsiString, 50, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, noticeCode);
			db.AddParameter(command, "Title", DbType.String, 50, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, title);
			db.AddParameter(command, "Memo", DbType.String, -1, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, memo);
			db.AddParameter(command, "ModifyUser", DbType.AnsiString, 50, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, modifyUser);
			db.AddParameter(command, "ModifyDate", DbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, modifyDate);
			db.AddParameter(command, "CreateUser", DbType.AnsiString, 50, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, createUser);
			db.AddParameter(command, "CreateDate", DbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, createDate);
			db.AddInParameter(command, "startRowIndex", DbType.Int32, startRowIndex);
			db.AddInParameter(command, "maximumRows", DbType.Int32, maximumRows);
			dr = db.ExecuteReader(command);
			return dr;
		}
		
		public IDataReader find(PubNoticeInfo pubNotice, int startRowIndex, int maximumRows){
			return find(
				pubNotice.NoticeCode,  
				pubNotice.Title,  
				pubNotice.Memo,  
				pubNotice.ModifyUser,  
				pubNotice.ModifyDate,  
				pubNotice.CreateUser,  
				pubNotice.CreateDate,  
				startRowIndex, 
				maximumRows);
		}
		
		public int findCount(
			System.String noticeCode, 
			System.String title, 
			System.String memo, 
			System.String modifyUser, 
			System.DateTime? modifyDate, 
			System.String createUser, 
			System.DateTime? createDate, 
			int startRowIndex, 
			int maximumRows){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_pubNoticeFindCount");
			db.AddParameter(command, "NoticeCode", DbType.AnsiString, 50, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, noticeCode);
			db.AddParameter(command, "Title", DbType.String, 50, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, title);
			db.AddParameter(command, "Memo", DbType.String, -1, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, memo);
			db.AddParameter(command, "ModifyUser", DbType.AnsiString, 50, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, modifyUser);
			db.AddParameter(command, "ModifyDate", DbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, modifyDate);
			db.AddParameter(command, "CreateUser", DbType.AnsiString, 50, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, createUser);
			db.AddParameter(command, "CreateDate", DbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, createDate);
			db.AddInParameter(command, "startRowIndex", DbType.Int32, startRowIndex);
			db.AddInParameter(command, "maximumRows", DbType.Int32, maximumRows);
			db.AddParameter( command, "ReturnValue", DbType.Int32, ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Default, null );
			
			db.ExecuteNonQuery(command);
			
			int recordCount = (int)db.GetParameterValue( command, "ReturnValue" );
			return recordCount;
		}
		
		public int findCount(PubNoticeInfo pubNotice, int startRowIndex, int maximumRows){
			return findCount(
				pubNotice.NoticeCode, 
				pubNotice.Title, 
				pubNotice.Memo, 
				pubNotice.ModifyUser, 
				pubNotice.ModifyDate, 
				pubNotice.CreateUser, 
				pubNotice.CreateDate, 
				startRowIndex, 
				maximumRows);
		}
	}
}