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
	public abstract class EnterFactApplyMasterBase{
		
		/// <summary>
		/// If enterFactApplyMaster is exist in database.
		/// </summary>
		/// <param name="enterFactApplyMaster">Business entity representing the enterFactApplyMaster.</param>
		/// <returns>bool. If exist return true, else return false.</returns>
		public bool isExist(EnterFactApplyMasterInfo enterFactApplyMaster){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_appEnterFactApplyMasterIsExist");
			
			db.AddInParameter(command, "ApplyCode", DbType.String,enterFactApplyMaster.ApplyCode);
			
			int result = 0;
			result = db.ExecuteNonQuery(command);
			
			if( result > 0 ){
				return true;
			}else{
				return false;
			} 
		}
		
		/// <summary>
		/// Insert a enterFactApplyMaster to Database.
		/// </summary>
		/// <param name="order">Business entity representing the enterFactApplyMaster</param>
		/// <returns>OrderId</returns>
		public int Insert(EnterFactApplyMasterInfo enterFactApplyMaster){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_appEnterFactApplyMasterInsert");
			db.AddInParameter(command, "ApplyCode", DbType.String,enterFactApplyMaster.ApplyCode);
			db.AddInParameter(command, "ApplyDate", DbType.DateTime,enterFactApplyMaster.ApplyDate);
			db.AddInParameter(command, "ApplyId", DbType.String,enterFactApplyMaster.ApplyId);
			db.AddInParameter(command, "ApplyDepartment", DbType.String,enterFactApplyMaster.ApplyDepartment);
			db.AddInParameter(command, "Tel", DbType.String,enterFactApplyMaster.Tel);
			db.AddInParameter(command, "IsBUMgrConfirm", DbType.Boolean,enterFactApplyMaster.IsBUMgrConfirm);
			db.AddInParameter(command, "DivisionMgrId", DbType.String,enterFactApplyMaster.DivisionMgrId);
			db.AddInParameter(command, "DivisionConfirmDate", DbType.DateTime,enterFactApplyMaster.DivisionConfirmDate);
			db.AddInParameter(command, "BUMgrId", DbType.String,enterFactApplyMaster.BUMgrId);
			db.AddInParameter(command, "BUConfirmDate", DbType.DateTime,enterFactApplyMaster.BUConfirmDate);
			db.AddInParameter(command, "Memo", DbType.String,enterFactApplyMaster.Memo);
			db.AddInParameter(command, "Status", DbType.Int32,enterFactApplyMaster.Status);
			db.AddInParameter(command, "RejectReason", DbType.String,enterFactApplyMaster.RejectReason);
			db.AddInParameter(command, "DivisionId", DbType.String,enterFactApplyMaster.DivisionId);
			db.AddInParameter(command, "BUId", DbType.String,enterFactApplyMaster.BUId);
			db.AddInParameter(command, "ReserveField1", DbType.Int32,enterFactApplyMaster.ReserveField1);
			db.AddInParameter(command, "ReserveField2", DbType.Decimal,enterFactApplyMaster.ReserveField2);
			db.AddInParameter(command, "ReserveField3", DbType.String,enterFactApplyMaster.ReserveField3);
			db.AddInParameter(command, "ReserveField4", DbType.String,enterFactApplyMaster.ReserveField4);
			db.AddInParameter(command, "InitiateId", DbType.String,enterFactApplyMaster.InitiateId);
			db.AddInParameter(command, "InitiateDate", DbType.DateTime,enterFactApplyMaster.InitiateDate);
			db.AddInParameter(command, "ModiId", DbType.String,enterFactApplyMaster.ModiId);
			db.AddInParameter(command, "ModiDate", DbType.DateTime,enterFactApplyMaster.ModiDate);
			return db.ExecuteNonQuery(command);
		}
		
		/// <summary>
		/// Method to Update an enterFactApplyMaster header
		/// </summary>
		/// <param name="order">Business entity representing the enterFactApplyMaster</param>
		public int Update(EnterFactApplyMasterInfo enterFactApplyMaster){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_appEnterFactApplyMasterUpdate");
			db.AddInParameter(command, "ApplyCode", DbType.String,enterFactApplyMaster.ApplyCode);
			db.AddInParameter(command, "ApplyDate", DbType.DateTime,enterFactApplyMaster.ApplyDate);
			db.AddInParameter(command, "ApplyId", DbType.String,enterFactApplyMaster.ApplyId);
			db.AddInParameter(command, "ApplyDepartment", DbType.String,enterFactApplyMaster.ApplyDepartment);
			db.AddInParameter(command, "Tel", DbType.String,enterFactApplyMaster.Tel);
			db.AddInParameter(command, "IsBUMgrConfirm", DbType.Boolean,enterFactApplyMaster.IsBUMgrConfirm);
			db.AddInParameter(command, "DivisionMgrId", DbType.String,enterFactApplyMaster.DivisionMgrId);
			db.AddInParameter(command, "DivisionConfirmDate", DbType.DateTime,enterFactApplyMaster.DivisionConfirmDate);
			db.AddInParameter(command, "BUMgrId", DbType.String,enterFactApplyMaster.BUMgrId);
			db.AddInParameter(command, "BUConfirmDate", DbType.DateTime,enterFactApplyMaster.BUConfirmDate);
			db.AddInParameter(command, "Memo", DbType.String,enterFactApplyMaster.Memo);
			db.AddInParameter(command, "Status", DbType.Int32,enterFactApplyMaster.Status);
			db.AddInParameter(command, "RejectReason", DbType.String,enterFactApplyMaster.RejectReason);
			db.AddInParameter(command, "DivisionId", DbType.String,enterFactApplyMaster.DivisionId);
			db.AddInParameter(command, "BUId", DbType.String,enterFactApplyMaster.BUId);
			db.AddInParameter(command, "ReserveField1", DbType.Int32,enterFactApplyMaster.ReserveField1);
			db.AddInParameter(command, "ReserveField2", DbType.Decimal,enterFactApplyMaster.ReserveField2);
			db.AddInParameter(command, "ReserveField3", DbType.String,enterFactApplyMaster.ReserveField3);
			db.AddInParameter(command, "ReserveField4", DbType.String,enterFactApplyMaster.ReserveField4);
			db.AddInParameter(command, "InitiateId", DbType.String,enterFactApplyMaster.InitiateId);
			db.AddInParameter(command, "InitiateDate", DbType.DateTime,enterFactApplyMaster.InitiateDate);
			db.AddInParameter(command, "ModiId", DbType.String,enterFactApplyMaster.ModiId);
			db.AddInParameter(command, "ModiDate", DbType.DateTime,enterFactApplyMaster.ModiDate);
			return db.ExecuteNonQuery(command);
		}
		
		/// <summary>
		/// Method to delete an enterFactApplyMaster header
		/// </summary>
		/// <param name="order">Business entity representing the enterFactApplyMaster</param>
		public int Delete(EnterFactApplyMasterInfo enterFactApplyMaster){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_appEnterFactApplyMasterDelete");
			db.AddInParameter(command, "ApplyCode", DbType.String,enterFactApplyMaster.ApplyCode);
			return db.ExecuteNonQuery(command);
		}
		
		public IDataReader getALL(){
			Database db = DatabaseFactory.CreateDatabase();
            IDataReader dr = null;
            DbCommand command = db.GetStoredProcCommand("usp_appEnterFactApplyMasterGetAll");
			
            dr = db.ExecuteReader(command);
			
			return dr;
		}
		
		/// <summary>
		/// Reads the enterFactApplyMaster information for a primary key
		/// </summary>
		/// <returns>Business entity representing the enterFactApplyMaster</returns>
		
		public EnterFactApplyMasterInfo getEnterFactApplyMaster(System.String applyCode){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_appEnterFactApplyMasterGet");
			db.AddInParameter(command, "ApplyCode", DbType.String,applyCode);			
			EnterFactApplyMasterInfo detail = new EnterFactApplyMasterInfo();
            using( IDataReader dr = db.ExecuteReader(command) ) {                
                if( dr.Read() ){
					if (!dr.IsDBNull(0)) detail.ApplyCode = dr.GetString(0);
					if (!dr.IsDBNull(1)) detail.ApplyDate = dr.GetDateTime(1);
					if (!dr.IsDBNull(2)) detail.ApplyId = dr.GetString(2);
					if (!dr.IsDBNull(3)) detail.ApplyDepartment = dr.GetString(3);
					if (!dr.IsDBNull(4)) detail.Tel = dr.GetString(4);
					if (!dr.IsDBNull(5)) detail.IsBUMgrConfirm = dr.GetBoolean(5);
					if (!dr.IsDBNull(6)) detail.DivisionMgrId = dr.GetString(6);
					if (!dr.IsDBNull(7)) detail.DivisionConfirmDate = dr.GetDateTime(7);
					if (!dr.IsDBNull(8)) detail.BUMgrId = dr.GetString(8);
					if (!dr.IsDBNull(9)) detail.BUConfirmDate = dr.GetDateTime(9);
					if (!dr.IsDBNull(10)) detail.Memo = dr.GetString(10);
					if (!dr.IsDBNull(11)) detail.Status = dr.GetInt32(11);
					if (!dr.IsDBNull(12)) detail.RejectReason = dr.GetString(12);
					if (!dr.IsDBNull(13)) detail.DivisionId = dr.GetString(13);
					if (!dr.IsDBNull(14)) detail.BUId = dr.GetString(14);
					if (!dr.IsDBNull(15)) detail.ReserveField1 = dr.GetInt32(15);
					if (!dr.IsDBNull(16)) detail.ReserveField2 = dr.GetDecimal(16);
					if (!dr.IsDBNull(17)) detail.ReserveField3 = dr.GetString(17);
					if (!dr.IsDBNull(18)) detail.ReserveField4 = dr.GetString(18);
					if (!dr.IsDBNull(19)) detail.InitiateId = dr.GetString(19);
					if (!dr.IsDBNull(20)) detail.InitiateDate = dr.GetDateTime(20);
					if (!dr.IsDBNull(21)) detail.ModiId = dr.GetString(21);
					if (!dr.IsDBNull(22)) detail.ModiDate = dr.GetDateTime(22);
                }
            }
			
			return detail;
		}
		
		
		public IDataReader find(
			System.String applyCode, 
			System.DateTime? applyDate, 
			System.String applyId, 
			System.String applyDepartment, 
			System.String tel, 
			System.Boolean? isBUMgrConfirm, 
			System.String divisionMgrId, 
			System.DateTime? divisionConfirmDate, 
			System.String bUMgrId, 
			System.DateTime? bUConfirmDate, 
			System.String memo, 
			System.Int32? status, 
			System.String rejectReason, 
			System.String divisionId, 
			System.String bUId, 
			System.Int32? reserveField1, 
			System.Decimal? reserveField2, 
			System.String reserveField3, 
			System.String reserveField4, 
			System.String initiateId, 
			System.DateTime? initiateDate, 
			System.String modiId, 
			System.DateTime? modiDate, 
			int startRowIndex, 
			int maximumRows){
			Database db = DatabaseFactory.CreateDatabase();
			IDataReader dr = null;
            DbCommand command = db.GetStoredProcCommand("usp_appEnterFactApplyMasterFind");
			db.AddParameter(command, "ApplyCode", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, applyCode);
			db.AddParameter(command, "ApplyDate", DbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, applyDate);
			db.AddParameter(command, "ApplyId", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, applyId);
			db.AddParameter(command, "ApplyDepartment", DbType.String, 50, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, applyDepartment);
			db.AddParameter(command, "Tel", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, tel);
			db.AddParameter(command, "IsBUMgrConfirm", DbType.Boolean, 1, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, isBUMgrConfirm);
			db.AddParameter(command, "DivisionMgrId", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, divisionMgrId);
			db.AddParameter(command, "DivisionConfirmDate", DbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, divisionConfirmDate);
			db.AddParameter(command, "BUMgrId", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, bUMgrId);
			db.AddParameter(command, "BUConfirmDate", DbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, bUConfirmDate);
			db.AddParameter(command, "Memo", DbType.String, 300, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, memo);
			db.AddParameter(command, "Status", DbType.Int32, 4, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, status);
			db.AddParameter(command, "RejectReason", DbType.String, 300, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, rejectReason);
			db.AddParameter(command, "DivisionId", DbType.String, 50, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, divisionId);
			db.AddParameter(command, "BUId", DbType.String, 50, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, bUId);
			db.AddParameter(command, "ReserveField1", DbType.Int32, 4, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, reserveField1);
			db.AddParameter(command, "ReserveField2", DbType.Decimal, 9, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, reserveField2);
			db.AddParameter(command, "ReserveField3", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, reserveField3);
			db.AddParameter(command, "ReserveField4", DbType.String, 300, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, reserveField4);
			db.AddParameter(command, "InitiateId", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, initiateId);
			db.AddParameter(command, "InitiateDate", DbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, initiateDate);
			db.AddParameter(command, "ModiId", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, modiId);
			db.AddParameter(command, "ModiDate", DbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, modiDate);
			db.AddInParameter(command, "startRowIndex", DbType.Int32, startRowIndex);
			db.AddInParameter(command, "maximumRows", DbType.Int32, maximumRows);
			dr = db.ExecuteReader(command);
			return dr;
		}
		
		public IDataReader find(EnterFactApplyMasterInfo enterFactApplyMaster, int startRowIndex, int maximumRows){
			return find(
				enterFactApplyMaster.ApplyCode,  
				enterFactApplyMaster.ApplyDate,  
				enterFactApplyMaster.ApplyId,  
				enterFactApplyMaster.ApplyDepartment,  
				enterFactApplyMaster.Tel,  
				enterFactApplyMaster.IsBUMgrConfirm,  
				enterFactApplyMaster.DivisionMgrId,  
				enterFactApplyMaster.DivisionConfirmDate,  
				enterFactApplyMaster.BUMgrId,  
				enterFactApplyMaster.BUConfirmDate,  
				enterFactApplyMaster.Memo,  
				enterFactApplyMaster.Status,  
				enterFactApplyMaster.RejectReason,  
				enterFactApplyMaster.DivisionId,  
				enterFactApplyMaster.BUId,  
				enterFactApplyMaster.ReserveField1,  
				enterFactApplyMaster.ReserveField2,  
				enterFactApplyMaster.ReserveField3,  
				enterFactApplyMaster.ReserveField4,  
				enterFactApplyMaster.InitiateId,  
				enterFactApplyMaster.InitiateDate,  
				enterFactApplyMaster.ModiId,  
				enterFactApplyMaster.ModiDate,  
				startRowIndex, 
				maximumRows);
		}
		
		public int findCount(
			System.String applyCode, 
			System.DateTime? applyDate, 
			System.String applyId, 
			System.String applyDepartment, 
			System.String tel, 
			System.Boolean? isBUMgrConfirm, 
			System.String divisionMgrId, 
			System.DateTime? divisionConfirmDate, 
			System.String bUMgrId, 
			System.DateTime? bUConfirmDate, 
			System.String memo, 
			System.Int32? status, 
			System.String rejectReason, 
			System.String divisionId, 
			System.String bUId, 
			System.Int32? reserveField1, 
			System.Decimal? reserveField2, 
			System.String reserveField3, 
			System.String reserveField4, 
			System.String initiateId, 
			System.DateTime? initiateDate, 
			System.String modiId, 
			System.DateTime? modiDate, 
			int startRowIndex, 
			int maximumRows){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_appEnterFactApplyMasterFindCount");
			db.AddParameter(command, "ApplyCode", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, applyCode);
			db.AddParameter(command, "ApplyDate", DbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, applyDate);
			db.AddParameter(command, "ApplyId", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, applyId);
			db.AddParameter(command, "ApplyDepartment", DbType.String, 50, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, applyDepartment);
			db.AddParameter(command, "Tel", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, tel);
			db.AddParameter(command, "IsBUMgrConfirm", DbType.Boolean, 1, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, isBUMgrConfirm);
			db.AddParameter(command, "DivisionMgrId", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, divisionMgrId);
			db.AddParameter(command, "DivisionConfirmDate", DbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, divisionConfirmDate);
			db.AddParameter(command, "BUMgrId", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, bUMgrId);
			db.AddParameter(command, "BUConfirmDate", DbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, bUConfirmDate);
			db.AddParameter(command, "Memo", DbType.String, 300, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, memo);
			db.AddParameter(command, "Status", DbType.Int32, 4, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, status);
			db.AddParameter(command, "RejectReason", DbType.String, 300, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, rejectReason);
			db.AddParameter(command, "DivisionId", DbType.String, 50, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, divisionId);
			db.AddParameter(command, "BUId", DbType.String, 50, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, bUId);
			db.AddParameter(command, "ReserveField1", DbType.Int32, 4, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, reserveField1);
			db.AddParameter(command, "ReserveField2", DbType.Decimal, 9, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, reserveField2);
			db.AddParameter(command, "ReserveField3", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, reserveField3);
			db.AddParameter(command, "ReserveField4", DbType.String, 300, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, reserveField4);
			db.AddParameter(command, "InitiateId", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, initiateId);
			db.AddParameter(command, "InitiateDate", DbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, initiateDate);
			db.AddParameter(command, "ModiId", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, modiId);
			db.AddParameter(command, "ModiDate", DbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, modiDate);
			db.AddInParameter(command, "startRowIndex", DbType.Int32, startRowIndex);
			db.AddInParameter(command, "maximumRows", DbType.Int32, maximumRows);
			db.AddParameter( command, "ReturnValue", DbType.Int32, ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Default, null );
			
			db.ExecuteNonQuery(command);
			
			int recordCount = (int)db.GetParameterValue( command, "ReturnValue" );
			return recordCount;
		}
		
		public int findCount(EnterFactApplyMasterInfo enterFactApplyMaster, int startRowIndex, int maximumRows){
			return findCount(
				enterFactApplyMaster.ApplyCode, 
				enterFactApplyMaster.ApplyDate, 
				enterFactApplyMaster.ApplyId, 
				enterFactApplyMaster.ApplyDepartment, 
				enterFactApplyMaster.Tel, 
				enterFactApplyMaster.IsBUMgrConfirm, 
				enterFactApplyMaster.DivisionMgrId, 
				enterFactApplyMaster.DivisionConfirmDate, 
				enterFactApplyMaster.BUMgrId, 
				enterFactApplyMaster.BUConfirmDate, 
				enterFactApplyMaster.Memo, 
				enterFactApplyMaster.Status, 
				enterFactApplyMaster.RejectReason, 
				enterFactApplyMaster.DivisionId, 
				enterFactApplyMaster.BUId, 
				enterFactApplyMaster.ReserveField1, 
				enterFactApplyMaster.ReserveField2, 
				enterFactApplyMaster.ReserveField3, 
				enterFactApplyMaster.ReserveField4, 
				enterFactApplyMaster.InitiateId, 
				enterFactApplyMaster.InitiateDate, 
				enterFactApplyMaster.ModiId, 
				enterFactApplyMaster.ModiDate, 
				startRowIndex, 
				maximumRows);
		}
	}
}