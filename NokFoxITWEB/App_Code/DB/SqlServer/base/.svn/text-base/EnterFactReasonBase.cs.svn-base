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
	public abstract class EnterFactReasonBase{
		
		/// <summary>
		/// If enterFactReason is exist in database.
		/// </summary>
		/// <param name="enterFactReason">Business entity representing the enterFactReason.</param>
		/// <returns>bool. If exist return true, else return false.</returns>
		public bool isExist(EnterFactReasonInfo enterFactReason){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_pubEnterFactReasonIsExist");
			
			db.AddInParameter(command, "ReasonCode", DbType.String,enterFactReason.ReasonCode);
			
			int result = 0;
			result = db.ExecuteNonQuery(command);
			
			if( result > 0 ){
				return true;
			}else{
				return false;
			} 
		}
		
		/// <summary>
		/// Insert a enterFactReason to Database.
		/// </summary>
		/// <param name="order">Business entity representing the enterFactReason</param>
		/// <returns>OrderId</returns>
		public int Insert(EnterFactReasonInfo enterFactReason){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_pubEnterFactReasonInsert");
			db.AddInParameter(command, "ReasonCode", DbType.String,enterFactReason.ReasonCode);
			db.AddInParameter(command, "Description", DbType.String,enterFactReason.Description);
			db.AddInParameter(command, "Memo", DbType.String,enterFactReason.Memo);
			db.AddInParameter(command, "ReserveField1", DbType.Int32,enterFactReason.ReserveField1);
			db.AddInParameter(command, "ReserveField2", DbType.Decimal,enterFactReason.ReserveField2);
			db.AddInParameter(command, "ReserveField3", DbType.String,enterFactReason.ReserveField3);
			db.AddInParameter(command, "ReserveField4", DbType.String,enterFactReason.ReserveField4);
			db.AddInParameter(command, "InitiateId", DbType.String,enterFactReason.InitiateId);
			db.AddInParameter(command, "InitiateDate", DbType.DateTime,enterFactReason.InitiateDate);
			db.AddInParameter(command, "ModiId", DbType.String,enterFactReason.ModiId);
			db.AddInParameter(command, "ModiDate", DbType.DateTime,enterFactReason.ModiDate);
			return db.ExecuteNonQuery(command);
		}
		
		/// <summary>
		/// Method to Update an enterFactReason header
		/// </summary>
		/// <param name="order">Business entity representing the enterFactReason</param>
		public int Update(EnterFactReasonInfo enterFactReason){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_pubEnterFactReasonUpdate");
			db.AddInParameter(command, "ReasonCode", DbType.String,enterFactReason.ReasonCode);
			db.AddInParameter(command, "Description", DbType.String,enterFactReason.Description);
			db.AddInParameter(command, "Memo", DbType.String,enterFactReason.Memo);
			db.AddInParameter(command, "ReserveField1", DbType.Int32,enterFactReason.ReserveField1);
			db.AddInParameter(command, "ReserveField2", DbType.Decimal,enterFactReason.ReserveField2);
			db.AddInParameter(command, "ReserveField3", DbType.String,enterFactReason.ReserveField3);
			db.AddInParameter(command, "ReserveField4", DbType.String,enterFactReason.ReserveField4);
			db.AddInParameter(command, "InitiateId", DbType.String,enterFactReason.InitiateId);
			db.AddInParameter(command, "InitiateDate", DbType.DateTime,enterFactReason.InitiateDate);
			db.AddInParameter(command, "ModiId", DbType.String,enterFactReason.ModiId);
			db.AddInParameter(command, "ModiDate", DbType.DateTime,enterFactReason.ModiDate);
			return db.ExecuteNonQuery(command);
		}
		
		/// <summary>
		/// Method to delete an enterFactReason header
		/// </summary>
		/// <param name="order">Business entity representing the enterFactReason</param>
		public int Delete(EnterFactReasonInfo enterFactReason){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_pubEnterFactReasonDelete");
			db.AddInParameter(command, "ReasonCode", DbType.String,enterFactReason.ReasonCode);
			return db.ExecuteNonQuery(command);
		}
		
		public IDataReader getALL(){
			Database db = DatabaseFactory.CreateDatabase();
            IDataReader dr = null;
            DbCommand command = db.GetStoredProcCommand("usp_pubEnterFactReasonGetAll");
			
            dr = db.ExecuteReader(command);
			
			return dr;
		}
		
		/// <summary>
		/// Reads the enterFactReason information for a primary key
		/// </summary>
		/// <returns>Business entity representing the enterFactReason</returns>
		
		public EnterFactReasonInfo getEnterFactReason(System.String reasonCode){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_pubEnterFactReasonGet");
			db.AddInParameter(command, "ReasonCode", DbType.String,reasonCode);
			
			EnterFactReasonInfo detail = new EnterFactReasonInfo();

            using( IDataReader dr = db.ExecuteReader(command) ) {
                
                if( dr.Read() ){
					if (!dr.IsDBNull(0)) detail.ReasonCode = dr.GetString(0);
					if (!dr.IsDBNull(1)) detail.Description = dr.GetString(1);
					if (!dr.IsDBNull(2)) detail.Memo = dr.GetString(2);
					if (!dr.IsDBNull(3)) detail.ReserveField1 = dr.GetInt32(3);
					if (!dr.IsDBNull(4)) detail.ReserveField2 = dr.GetDecimal(4);
					if (!dr.IsDBNull(5)) detail.ReserveField3 = dr.GetString(5);
					if (!dr.IsDBNull(6)) detail.ReserveField4 = dr.GetString(6);
					if (!dr.IsDBNull(7)) detail.InitiateId = dr.GetString(7);
					if (!dr.IsDBNull(8)) detail.InitiateDate = dr.GetDateTime(8);
					if (!dr.IsDBNull(9)) detail.ModiId = dr.GetString(9);
					if (!dr.IsDBNull(10)) detail.ModiDate = dr.GetDateTime(10);
                }
            }
			
			return detail;
		}
		
		
		public IDataReader find(
			System.String reasonCode, 
			System.String description, 
			System.String memo, 
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
            DbCommand command = db.GetStoredProcCommand("usp_pubEnterFactReasonFind");
			db.AddParameter(command, "ReasonCode", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, reasonCode);
			db.AddParameter(command, "Description", DbType.String, 300, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, description);
			db.AddParameter(command, "Memo", DbType.String, 300, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, memo);
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
		
		public IDataReader find(EnterFactReasonInfo enterFactReason, int startRowIndex, int maximumRows){
			return find(
				enterFactReason.ReasonCode,  
				enterFactReason.Description,  
				enterFactReason.Memo,  
				enterFactReason.ReserveField1,  
				enterFactReason.ReserveField2,  
				enterFactReason.ReserveField3,  
				enterFactReason.ReserveField4,  
				enterFactReason.InitiateId,  
				enterFactReason.InitiateDate,  
				enterFactReason.ModiId,  
				enterFactReason.ModiDate,  
				startRowIndex, 
				maximumRows);
		}
		
		public int findCount(
			System.String reasonCode, 
			System.String description, 
			System.String memo, 
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
            DbCommand command = db.GetStoredProcCommand("usp_pubEnterFactReasonFindCount");
			db.AddParameter(command, "ReasonCode", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, reasonCode);
			db.AddParameter(command, "Description", DbType.String, 300, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, description);
			db.AddParameter(command, "Memo", DbType.String, 300, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, memo);
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
		
		public int findCount(EnterFactReasonInfo enterFactReason, int startRowIndex, int maximumRows){
			return findCount(
				enterFactReason.ReasonCode, 
				enterFactReason.Description, 
				enterFactReason.Memo, 
				enterFactReason.ReserveField1, 
				enterFactReason.ReserveField2, 
				enterFactReason.ReserveField3, 
				enterFactReason.ReserveField4, 
				enterFactReason.InitiateId, 
				enterFactReason.InitiateDate, 
				enterFactReason.ModiId, 
				enterFactReason.ModiDate, 
				startRowIndex, 
				maximumRows);
		}
	}
}