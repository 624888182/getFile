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
	public abstract class PubAttachmentsBase{
		
		/// <summary>
		/// If pubAttachments is exist in database.
		/// </summary>
		/// <param name="pubAttachments">Business entity representing the pubAttachments.</param>
		/// <returns>bool. If exist return true, else return false.</returns>
		public bool isExist(PubAttachmentsInfo pubAttachments){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_pubAttachmentsIsExist");
			
			db.AddInParameter(command, "AttachID", DbType.Int32,pubAttachments.AttachID);
			
			int result = 0;
			result = db.ExecuteNonQuery(command);
			
			if( result > 0 ){
				return true;
			}else{
				return false;
			} 
		}
		
		/// <summary>
		/// Insert a pubAttachments to Database.
		/// </summary>
		/// <param name="order">Business entity representing the pubAttachments</param>
		/// <returns>OrderId</returns>
		public int Insert(PubAttachmentsInfo pubAttachments){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_pubAttachmentsInsert");
			db.AddInParameter(command, "BaseType", DbType.AnsiString,pubAttachments.BaseType);
			db.AddInParameter(command, "BaseID", DbType.Int32,pubAttachments.BaseID);
			db.AddInParameter(command, "BaseCode", DbType.AnsiString,pubAttachments.BaseCode);
			db.AddInParameter(command, "OriginalFileName", DbType.String,pubAttachments.OriginalFileName);
			db.AddInParameter(command, "UniqueFileName", DbType.String,pubAttachments.UniqueFileName);
			db.AddInParameter(command, "FileType", DbType.AnsiString,pubAttachments.FileType);
			db.AddInParameter(command, "CreateUser", DbType.AnsiString,pubAttachments.CreateUser);
			db.AddInParameter(command, "CreateDate", DbType.DateTime,pubAttachments.CreateDate);
			return db.ExecuteNonQuery(command);
		}
		
		/// <summary>
		/// Method to Update an pubAttachments header
		/// </summary>
		/// <param name="order">Business entity representing the pubAttachments</param>
		public int Update(PubAttachmentsInfo pubAttachments){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_pubAttachmentsUpdate");
			db.AddInParameter(command, "AttachID", DbType.Int32,pubAttachments.AttachID);
			db.AddInParameter(command, "BaseType", DbType.AnsiString,pubAttachments.BaseType);
			db.AddInParameter(command, "BaseID", DbType.Int32,pubAttachments.BaseID);
			db.AddInParameter(command, "BaseCode", DbType.AnsiString,pubAttachments.BaseCode);
			db.AddInParameter(command, "OriginalFileName", DbType.String,pubAttachments.OriginalFileName);
			db.AddInParameter(command, "UniqueFileName", DbType.String,pubAttachments.UniqueFileName);
			db.AddInParameter(command, "FileType", DbType.AnsiString,pubAttachments.FileType);
			db.AddInParameter(command, "CreateUser", DbType.AnsiString,pubAttachments.CreateUser);
			db.AddInParameter(command, "CreateDate", DbType.DateTime,pubAttachments.CreateDate);
			return db.ExecuteNonQuery(command);
		}
		
		/// <summary>
		/// Method to delete an pubAttachments header
		/// </summary>
		/// <param name="order">Business entity representing the pubAttachments</param>
		public int Delete(PubAttachmentsInfo pubAttachments){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_pubAttachmentsDelete");
			db.AddInParameter(command, "AttachID", DbType.Int32,pubAttachments.AttachID);
			return db.ExecuteNonQuery(command);
		}
		
		public IDataReader getALL(){
			Database db = DatabaseFactory.CreateDatabase();
            IDataReader dr = null;
            DbCommand command = db.GetStoredProcCommand("usp_pubAttachmentsGetAll");
			
            dr = db.ExecuteReader(command);
			
			return dr;
		}
		
		/// <summary>
		/// Reads the pubAttachments information for a primary key
		/// </summary>
		/// <returns>Business entity representing the pubAttachments</returns>
		
		public PubAttachmentsInfo getPubAttachments(System.Int32 attachID){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_pubAttachmentsGet");
			db.AddInParameter(command, "AttachID", DbType.Int32,attachID);
			
			PubAttachmentsInfo detail = new PubAttachmentsInfo();

            using( IDataReader dr = db.ExecuteReader(command) ) {
                
                if( dr.Read() ){
					if (!dr.IsDBNull(0)) detail.AttachID = dr.GetInt32(0);
					if (!dr.IsDBNull(1)) detail.BaseType = dr.GetString(1);
					if (!dr.IsDBNull(2)) detail.BaseID = dr.GetInt32(2);
					if (!dr.IsDBNull(3)) detail.BaseCode = dr.GetString(3);
					if (!dr.IsDBNull(4)) detail.OriginalFileName = dr.GetString(4);
					if (!dr.IsDBNull(5)) detail.UniqueFileName = dr.GetString(5);
					if (!dr.IsDBNull(6)) detail.FileType = dr.GetString(6);
					if (!dr.IsDBNull(7)) detail.CreateUser = dr.GetString(7);
					if (!dr.IsDBNull(8)) detail.CreateDate = dr.GetDateTime(8);
                }
            }
			
			return detail;
		}
		
		
		public IDataReader find(
			System.Int32 attachID, 
			System.String baseType, 
			System.Int32? baseID, 
			System.String baseCode, 
			System.String originalFileName, 
			System.String uniqueFileName, 
			System.String fileType, 
			System.String createUser, 
			System.DateTime? createDate, 
			int startRowIndex, 
			int maximumRows){
			Database db = DatabaseFactory.CreateDatabase();
			IDataReader dr = null;
            DbCommand command = db.GetStoredProcCommand("usp_pubAttachmentsFind");
			db.AddParameter(command, "BaseType", DbType.AnsiString, 20, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, baseType);
			db.AddParameter(command, "BaseID", DbType.Int32, 4, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, baseID);
			db.AddParameter(command, "BaseCode", DbType.AnsiString, 50, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, baseCode);
			db.AddParameter(command, "OriginalFileName", DbType.String, 250, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, originalFileName);
			db.AddParameter(command, "UniqueFileName", DbType.String, 250, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, uniqueFileName);
			db.AddParameter(command, "FileType", DbType.AnsiString, 50, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, fileType);
			db.AddParameter(command, "CreateUser", DbType.AnsiString, 50, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, createUser);
			db.AddParameter(command, "CreateDate", DbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, createDate);
			db.AddInParameter(command, "startRowIndex", DbType.Int32, startRowIndex);
			db.AddInParameter(command, "maximumRows", DbType.Int32, maximumRows);
			dr = db.ExecuteReader(command);
			return dr;
		}
		
		public IDataReader find(PubAttachmentsInfo pubAttachments, int startRowIndex, int maximumRows){
			return find(
				pubAttachments.AttachID,  
				pubAttachments.BaseType,  
				pubAttachments.BaseID,  
				pubAttachments.BaseCode,  
				pubAttachments.OriginalFileName,  
				pubAttachments.UniqueFileName,  
				pubAttachments.FileType,  
				pubAttachments.CreateUser,  
				pubAttachments.CreateDate,  
				startRowIndex, 
				maximumRows);
		}
		
		public int findCount(
			System.Int32 attachID, 
			System.String baseType, 
			System.Int32? baseID, 
			System.String baseCode, 
			System.String originalFileName, 
			System.String uniqueFileName, 
			System.String fileType, 
			System.String createUser, 
			System.DateTime? createDate, 
			int startRowIndex, 
			int maximumRows){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_pubAttachmentsFindCount");
			db.AddParameter(command, "BaseType", DbType.AnsiString, 20, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, baseType);
			db.AddParameter(command, "BaseID", DbType.Int32, 4, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, baseID);
			db.AddParameter(command, "BaseCode", DbType.AnsiString, 50, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, baseCode);
			db.AddParameter(command, "OriginalFileName", DbType.String, 250, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, originalFileName);
			db.AddParameter(command, "UniqueFileName", DbType.String, 250, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, uniqueFileName);
			db.AddParameter(command, "FileType", DbType.AnsiString, 50, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, fileType);
			db.AddParameter(command, "CreateUser", DbType.AnsiString, 50, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, createUser);
			db.AddParameter(command, "CreateDate", DbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, createDate);
			db.AddInParameter(command, "startRowIndex", DbType.Int32, startRowIndex);
			db.AddInParameter(command, "maximumRows", DbType.Int32, maximumRows);
			db.AddParameter( command, "ReturnValue", DbType.Int32, ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Default, null );
			
			db.ExecuteNonQuery(command);
			
			int recordCount = (int)db.GetParameterValue( command, "ReturnValue" );
			return recordCount;
		}
		
		public int findCount(PubAttachmentsInfo pubAttachments, int startRowIndex, int maximumRows){
			return findCount(
				pubAttachments.AttachID, 
				pubAttachments.BaseType, 
				pubAttachments.BaseID, 
				pubAttachments.BaseCode, 
				pubAttachments.OriginalFileName, 
				pubAttachments.UniqueFileName, 
				pubAttachments.FileType, 
				pubAttachments.CreateUser, 
				pubAttachments.CreateDate, 
				startRowIndex, 
				maximumRows);
		}
	}
}