using System;
using System.Data;
using System.Data.Common;

using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.ObjectBuilder;

namespace FIH.PM.db{
	/// <summary>
	///
	/// </summary>
	public abstract class ApplyPurchaseHeadBase{
		
		/// <summary>
		/// If applyPurchaseHead is exist in database.
		/// </summary>
		/// <param name="applyPurchaseHead">Business entity representing the applyPurchaseHead.</param>
		/// <returns>bool. If exist return true, else return false.</returns>
		public bool isExist(ApplyPurchaseHeadInfo applyPurchaseHead){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("tbpurchase_ApplyPurchaseHead_isExist_sp");
			
			db.AddInParameter(command, "ApplyPurchaseCode", DbType.String,applyPurchaseHead.ApplyPurchaseCode);
			
			int result = 0;
			result = db.ExecuteNonQuery(command);
			
			if( result > 0 ){
				return true;
			}else{
				return false;
			} 
		}
		
		/// <summary>
		/// Insert a applyPurchaseHead to Database.
		/// </summary>
		/// <param name="order">Business entity representing the applyPurchaseHead</param>
		/// <returns>OrderId</returns>
		public int Insert(ApplyPurchaseHeadInfo applyPurchaseHead){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("tbpurchase_ApplyPurchaseHead_insert_sp");
			db.AddInParameter(command, "ApplyPurchaseCode", DbType.String,applyPurchaseHead.ApplyPurchaseCode);
			db.AddInParameter(command, "ApplyPurchaseType", DbType.String,applyPurchaseHead.ApplyPurchaseType);
			db.AddInParameter(command, "DocumentDate", DbType.DateTime,applyPurchaseHead.DocumentDate);
			db.AddInParameter(command, "PurchaseDept", DbType.String,applyPurchaseHead.PurchaseDept);
			db.AddInParameter(command, "ApplyMan", DbType.String,applyPurchaseHead.ApplyMan);
			db.AddInParameter(command, "Phone", DbType.String,applyPurchaseHead.Phone);
			db.AddInParameter(command, "CaseCode", DbType.String,applyPurchaseHead.CaseCode);
			db.AddInParameter(command, "RequireID", DbType.String,applyPurchaseHead.RequireID);
			db.AddInParameter(command, "CorporationID", DbType.String,applyPurchaseHead.CorporationID);
			db.AddInParameter(command, "MoneyType", DbType.String,applyPurchaseHead.MoneyType);
			db.AddInParameter(command, "Memo", DbType.String,applyPurchaseHead.Memo);
			db.AddInParameter(command, "StandbyField1", DbType.String,applyPurchaseHead.StandbyField1);
			db.AddInParameter(command, "StandbyField2", DbType.String,applyPurchaseHead.StandbyField2);
			db.AddInParameter(command, "StandbyField3", DbType.Decimal,applyPurchaseHead.StandbyField3);
			db.AddInParameter(command, "StandbyField4", DbType.Int32,applyPurchaseHead.StandbyField4);
			db.AddInParameter(command, "CreaterID", DbType.String,applyPurchaseHead.CreaterID);
			db.AddInParameter(command, "CreaterDate", DbType.DateTime,applyPurchaseHead.CreaterDate);
			db.AddInParameter(command, "ModiID", DbType.String,applyPurchaseHead.ModiID);
			db.AddInParameter(command, "ModiDate", DbType.DateTime,applyPurchaseHead.ModiDate);
			return db.ExecuteNonQuery(command);
		}
		
		/// <summary>
		/// Method to Update an applyPurchaseHead header
		/// </summary>
		/// <param name="order">Business entity representing the applyPurchaseHead</param>
		public int Update(ApplyPurchaseHeadInfo applyPurchaseHead){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("tbpurchase_ApplyPurchaseHead_update_sp");
			db.AddInParameter(command, "ApplyPurchaseCode", DbType.String,applyPurchaseHead.ApplyPurchaseCode);
			db.AddInParameter(command, "ApplyPurchaseType", DbType.String,applyPurchaseHead.ApplyPurchaseType);
			db.AddInParameter(command, "DocumentDate", DbType.DateTime,applyPurchaseHead.DocumentDate);
			db.AddInParameter(command, "PurchaseDept", DbType.String,applyPurchaseHead.PurchaseDept);
			db.AddInParameter(command, "ApplyMan", DbType.String,applyPurchaseHead.ApplyMan);
			db.AddInParameter(command, "Phone", DbType.String,applyPurchaseHead.Phone);
			db.AddInParameter(command, "CaseCode", DbType.String,applyPurchaseHead.CaseCode);
			db.AddInParameter(command, "RequireID", DbType.String,applyPurchaseHead.RequireID);
			db.AddInParameter(command, "CorporationID", DbType.String,applyPurchaseHead.CorporationID);
			db.AddInParameter(command, "MoneyType", DbType.String,applyPurchaseHead.MoneyType);
			db.AddInParameter(command, "Memo", DbType.String,applyPurchaseHead.Memo);
			db.AddInParameter(command, "StandbyField1", DbType.String,applyPurchaseHead.StandbyField1);
			db.AddInParameter(command, "StandbyField2", DbType.String,applyPurchaseHead.StandbyField2);
			db.AddInParameter(command, "StandbyField3", DbType.Decimal,applyPurchaseHead.StandbyField3);
			db.AddInParameter(command, "StandbyField4", DbType.Int32,applyPurchaseHead.StandbyField4);
			db.AddInParameter(command, "CreaterID", DbType.String,applyPurchaseHead.CreaterID);
			db.AddInParameter(command, "CreaterDate", DbType.DateTime,applyPurchaseHead.CreaterDate);
			db.AddInParameter(command, "ModiID", DbType.String,applyPurchaseHead.ModiID);
			db.AddInParameter(command, "ModiDate", DbType.DateTime,applyPurchaseHead.ModiDate);
			return db.ExecuteNonQuery(command);
		}
		
		/// <summary>
		/// Method to delete an applyPurchaseHead header
		/// </summary>
		/// <param name="order">Business entity representing the applyPurchaseHead</param>
		public int Delete(ApplyPurchaseHeadInfo applyPurchaseHead){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("tbpurchase_ApplyPurchaseHead_delete_sp");
			db.AddInParameter(command, "ApplyPurchaseCode", DbType.String,applyPurchaseHead.ApplyPurchaseCode);
			return db.ExecuteNonQuery(command);
		}
		
		public IDataReader getALL(){
			Database db = DatabaseFactory.CreateDatabase();
            IDataReader dr = null;
            DbCommand command = db.GetStoredProcCommand("tbpurchase_ApplyPurchaseHead_getAll_sp");
			
            dr = db.ExecuteReader(command);
			
			return dr;
		}
		
		/// <summary>
		/// Reads the applyPurchaseHead information for a primary key
		/// </summary>
		/// <returns>Business entity representing the applyPurchaseHead</returns>
		
		public ApplyPurchaseHeadInfo getApplyPurchaseHead(System.String applyPurchaseCode){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("tbpurchase_ApplyPurchaseHead_get_sp");
			db.AddInParameter(command, "ApplyPurchaseCode", DbType.String,applyPurchaseCode);
			
			ApplyPurchaseHeadInfo detail = new ApplyPurchaseHeadInfo();

            using( IDataReader dr = db.ExecuteReader(command) ) {
                
                if( dr.Read() ){
					if (!dr.IsDBNull(0)) detail.ApplyPurchaseCode = dr.GetString(0);
					if (!dr.IsDBNull(1)) detail.ApplyPurchaseType = dr.GetString(1);
					if (!dr.IsDBNull(2)) detail.DocumentDate = dr.GetDateTime(2);
					if (!dr.IsDBNull(3)) detail.PurchaseDept = dr.GetString(3);
					if (!dr.IsDBNull(4)) detail.ApplyMan = dr.GetString(4);
					if (!dr.IsDBNull(5)) detail.Phone = dr.GetString(5);
					if (!dr.IsDBNull(6)) detail.CaseCode = dr.GetString(6);
					if (!dr.IsDBNull(7)) detail.RequireID = dr.GetString(7);
					if (!dr.IsDBNull(8)) detail.CorporationID = dr.GetString(8);
					if (!dr.IsDBNull(9)) detail.MoneyType = dr.GetString(9);
					if (!dr.IsDBNull(10)) detail.Memo = dr.GetString(10);
					if (!dr.IsDBNull(11)) detail.StandbyField1 = dr.GetString(11);
					if (!dr.IsDBNull(12)) detail.StandbyField2 = dr.GetString(12);
					if (!dr.IsDBNull(13)) detail.StandbyField3 = dr.GetDecimal(13);
					if (!dr.IsDBNull(14)) detail.StandbyField4 = dr.GetInt32(14);
					if (!dr.IsDBNull(15)) detail.CreaterID = dr.GetString(15);
					if (!dr.IsDBNull(16)) detail.CreaterDate = dr.GetDateTime(16);
					if (!dr.IsDBNull(17)) detail.ModiID = dr.GetString(17);
					if (!dr.IsDBNull(18)) detail.ModiDate = dr.GetDateTime(18);
                }
            }
			
			return detail;
		}
		
		
		public IDataReader find(
			System.String applyPurchaseCode, 
			System.String applyPurchaseType, 
			System.DateTime? documentDate, 
			System.String purchaseDept, 
			System.String applyMan, 
			System.String phone, 
			System.String caseCode, 
			System.String requireID, 
			System.String corporationID, 
			System.String moneyType, 
			System.String memo, 
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
            DbCommand command = db.GetStoredProcCommand("tbpurchase_ApplyPurchaseHead_find_sp");
			db.AddParameter(command, "ApplyPurchaseCode", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, applyPurchaseCode);
			db.AddParameter(command, "ApplyPurchaseType", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, applyPurchaseType);
			db.AddParameter(command, "DocumentDate", DbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, documentDate);
			db.AddParameter(command, "PurchaseDept", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, purchaseDept);
			db.AddParameter(command, "ApplyMan", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, applyMan);
			db.AddParameter(command, "Phone", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, phone);
			db.AddParameter(command, "CaseCode", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, caseCode);
			db.AddParameter(command, "RequireID", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, requireID);
			db.AddParameter(command, "CorporationID", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, corporationID);
			db.AddParameter(command, "MoneyType", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, moneyType);
			db.AddParameter(command, "Memo", DbType.String, 300, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, memo);
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
		
		public IDataReader find(ApplyPurchaseHeadInfo applyPurchaseHead, int startRowIndex, int maximumRows){
			return find(
				applyPurchaseHead.ApplyPurchaseCode,  
				applyPurchaseHead.ApplyPurchaseType,  
				applyPurchaseHead.DocumentDate,  
				applyPurchaseHead.PurchaseDept,  
				applyPurchaseHead.ApplyMan,  
				applyPurchaseHead.Phone,  
				applyPurchaseHead.CaseCode,  
				applyPurchaseHead.RequireID,  
				applyPurchaseHead.CorporationID,  
				applyPurchaseHead.MoneyType,  
				applyPurchaseHead.Memo,  
				applyPurchaseHead.StandbyField1,  
				applyPurchaseHead.StandbyField2,  
				applyPurchaseHead.StandbyField3,  
				applyPurchaseHead.StandbyField4,  
				applyPurchaseHead.CreaterID,  
				applyPurchaseHead.CreaterDate,  
				applyPurchaseHead.ModiID,  
				applyPurchaseHead.ModiDate,  
				startRowIndex, 
				maximumRows);
		}
		
		public int findCount(
			System.String applyPurchaseCode, 
			System.String applyPurchaseType, 
			System.DateTime? documentDate, 
			System.String purchaseDept, 
			System.String applyMan, 
			System.String phone, 
			System.String caseCode, 
			System.String requireID, 
			System.String corporationID, 
			System.String moneyType, 
			System.String memo, 
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
            DbCommand command = db.GetStoredProcCommand("tbpurchase_ApplyPurchaseHead_findCount_sp");
			db.AddParameter(command, "ApplyPurchaseCode", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, applyPurchaseCode);
			db.AddParameter(command, "ApplyPurchaseType", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, applyPurchaseType);
			db.AddParameter(command, "DocumentDate", DbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, documentDate);
			db.AddParameter(command, "PurchaseDept", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, purchaseDept);
			db.AddParameter(command, "ApplyMan", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, applyMan);
			db.AddParameter(command, "Phone", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, phone);
			db.AddParameter(command, "CaseCode", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, caseCode);
			db.AddParameter(command, "RequireID", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, requireID);
			db.AddParameter(command, "CorporationID", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, corporationID);
			db.AddParameter(command, "MoneyType", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, moneyType);
			db.AddParameter(command, "Memo", DbType.String, 300, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, memo);
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
		
		public int findCount(ApplyPurchaseHeadInfo applyPurchaseHead, int startRowIndex, int maximumRows){
			return findCount(
				applyPurchaseHead.ApplyPurchaseCode, 
				applyPurchaseHead.ApplyPurchaseType, 
				applyPurchaseHead.DocumentDate, 
				applyPurchaseHead.PurchaseDept, 
				applyPurchaseHead.ApplyMan, 
				applyPurchaseHead.Phone, 
				applyPurchaseHead.CaseCode, 
				applyPurchaseHead.RequireID, 
				applyPurchaseHead.CorporationID, 
				applyPurchaseHead.MoneyType, 
				applyPurchaseHead.Memo, 
				applyPurchaseHead.StandbyField1, 
				applyPurchaseHead.StandbyField2, 
				applyPurchaseHead.StandbyField3, 
				applyPurchaseHead.StandbyField4, 
				applyPurchaseHead.CreaterID, 
				applyPurchaseHead.CreaterDate, 
				applyPurchaseHead.ModiID, 
				applyPurchaseHead.ModiDate, 
				startRowIndex, 
				maximumRows);
		}
	}
}