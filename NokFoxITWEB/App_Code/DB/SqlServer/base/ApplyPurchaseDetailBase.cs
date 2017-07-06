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
	public abstract class ApplyPurchaseDetailBase{
		
		/// <summary>
		/// If applyPurchaseDetail is exist in database.
		/// </summary>
		/// <param name="applyPurchaseDetail">Business entity representing the applyPurchaseDetail.</param>
		/// <returns>bool. If exist return true, else return false.</returns>
		public bool isExist(ApplyPurchaseDetailInfo applyPurchaseDetail){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("tbpurchase_ApplyPurchaseDetail_isExist_sp");
			
			db.AddInParameter(command, "ApplyPurchaseCode", DbType.String,applyPurchaseDetail.ApplyPurchaseCode);
			db.AddInParameter(command, "ItemNo", DbType.Int32,applyPurchaseDetail.ItemNo);
			
			int result = 0;
			result = db.ExecuteNonQuery(command);
			
			if( result > 0 ){
				return true;
			}else{
				return false;
			} 
		}
		
		/// <summary>
		/// Insert a applyPurchaseDetail to Database.
		/// </summary>
		/// <param name="order">Business entity representing the applyPurchaseDetail</param>
		/// <returns>OrderId</returns>
		public int Insert(ApplyPurchaseDetailInfo applyPurchaseDetail){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("tbpurchase_ApplyPurchaseDetail_insert_sp");
			db.AddInParameter(command, "ApplyPurchaseCode", DbType.String,applyPurchaseDetail.ApplyPurchaseCode);
			db.AddInParameter(command, "ItemNo", DbType.Int32,applyPurchaseDetail.ItemNo);
			db.AddInParameter(command, "CaseItemNo", DbType.Int32,applyPurchaseDetail.CaseItemNo);
			db.AddInParameter(command, "BOMNO", DbType.String,applyPurchaseDetail.BOMNO);
			db.AddInParameter(command, "BOMName", DbType.String,applyPurchaseDetail.BOMName);
			db.AddInParameter(command, "BOMBrand", DbType.String,applyPurchaseDetail.BOMBrand);
			db.AddInParameter(command, "Specification", DbType.String,applyPurchaseDetail.Specification);
			db.AddInParameter(command, "Manufacture", DbType.String,applyPurchaseDetail.Manufacture);
			db.AddInParameter(command, "Unit", DbType.String,applyPurchaseDetail.Unit);
			db.AddInParameter(command, "Price", DbType.Decimal,applyPurchaseDetail.Price);
			db.AddInParameter(command, "Quantity", DbType.Decimal,applyPurchaseDetail.Quantity);
			db.AddInParameter(command, "Sum", DbType.Decimal,applyPurchaseDetail.Sum);
			db.AddInParameter(command, "RequireDate", DbType.DateTime,applyPurchaseDetail.RequireDate);
			db.AddInParameter(command, "BudgetSum", DbType.Decimal,applyPurchaseDetail.BudgetSum);
			db.AddInParameter(command, "PurchaseCode", DbType.String,applyPurchaseDetail.PurchaseCode);
			db.AddInParameter(command, "PurchaseItemNo", DbType.Int32,applyPurchaseDetail.PurchaseItemNo);
			db.AddInParameter(command, "AccountingSubject", DbType.String,applyPurchaseDetail.AccountingSubject);
			db.AddInParameter(command, "PurchaseType", DbType.String,applyPurchaseDetail.PurchaseType);
			db.AddInParameter(command, "Memo", DbType.String,applyPurchaseDetail.Memo);
			db.AddInParameter(command, "StandbyField1", DbType.String,applyPurchaseDetail.StandbyField1);
			db.AddInParameter(command, "StandbyField2", DbType.String,applyPurchaseDetail.StandbyField2);
			db.AddInParameter(command, "StandbyField3", DbType.Decimal,applyPurchaseDetail.StandbyField3);
			db.AddInParameter(command, "StandbyField4", DbType.Int32,applyPurchaseDetail.StandbyField4);
			db.AddInParameter(command, "CreaterID", DbType.String,applyPurchaseDetail.CreaterID);
			db.AddInParameter(command, "CreaterDate", DbType.DateTime,applyPurchaseDetail.CreaterDate);
			db.AddInParameter(command, "ModiID", DbType.String,applyPurchaseDetail.ModiID);
			db.AddInParameter(command, "ModiDate", DbType.DateTime,applyPurchaseDetail.ModiDate);
			return db.ExecuteNonQuery(command);
		}
		
		/// <summary>
		/// Method to Update an applyPurchaseDetail header
		/// </summary>
		/// <param name="order">Business entity representing the applyPurchaseDetail</param>
		public int Update(ApplyPurchaseDetailInfo applyPurchaseDetail){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("tbpurchase_ApplyPurchaseDetail_update_sp");
			db.AddInParameter(command, "ApplyPurchaseCode", DbType.String,applyPurchaseDetail.ApplyPurchaseCode);
			db.AddInParameter(command, "ItemNo", DbType.Int32,applyPurchaseDetail.ItemNo);
			db.AddInParameter(command, "CaseItemNo", DbType.Int32,applyPurchaseDetail.CaseItemNo);
			db.AddInParameter(command, "BOMNO", DbType.String,applyPurchaseDetail.BOMNO);
			db.AddInParameter(command, "BOMName", DbType.String,applyPurchaseDetail.BOMName);
			db.AddInParameter(command, "BOMBrand", DbType.String,applyPurchaseDetail.BOMBrand);
			db.AddInParameter(command, "Specification", DbType.String,applyPurchaseDetail.Specification);
			db.AddInParameter(command, "Manufacture", DbType.String,applyPurchaseDetail.Manufacture);
			db.AddInParameter(command, "Unit", DbType.String,applyPurchaseDetail.Unit);
			db.AddInParameter(command, "Price", DbType.Decimal,applyPurchaseDetail.Price);
			db.AddInParameter(command, "Quantity", DbType.Decimal,applyPurchaseDetail.Quantity);
			db.AddInParameter(command, "Sum", DbType.Decimal,applyPurchaseDetail.Sum);
			db.AddInParameter(command, "RequireDate", DbType.DateTime,applyPurchaseDetail.RequireDate);
			db.AddInParameter(command, "BudgetSum", DbType.Decimal,applyPurchaseDetail.BudgetSum);
			db.AddInParameter(command, "PurchaseCode", DbType.String,applyPurchaseDetail.PurchaseCode);
			db.AddInParameter(command, "PurchaseItemNo", DbType.Int32,applyPurchaseDetail.PurchaseItemNo);
			db.AddInParameter(command, "AccountingSubject", DbType.String,applyPurchaseDetail.AccountingSubject);
			db.AddInParameter(command, "PurchaseType", DbType.String,applyPurchaseDetail.PurchaseType);
			db.AddInParameter(command, "Memo", DbType.String,applyPurchaseDetail.Memo);
			db.AddInParameter(command, "StandbyField1", DbType.String,applyPurchaseDetail.StandbyField1);
			db.AddInParameter(command, "StandbyField2", DbType.String,applyPurchaseDetail.StandbyField2);
			db.AddInParameter(command, "StandbyField3", DbType.Decimal,applyPurchaseDetail.StandbyField3);
			db.AddInParameter(command, "StandbyField4", DbType.Int32,applyPurchaseDetail.StandbyField4);
			db.AddInParameter(command, "CreaterID", DbType.String,applyPurchaseDetail.CreaterID);
			db.AddInParameter(command, "CreaterDate", DbType.DateTime,applyPurchaseDetail.CreaterDate);
			db.AddInParameter(command, "ModiID", DbType.String,applyPurchaseDetail.ModiID);
			db.AddInParameter(command, "ModiDate", DbType.DateTime,applyPurchaseDetail.ModiDate);
			return db.ExecuteNonQuery(command);
		}
		
		/// <summary>
		/// Method to delete an applyPurchaseDetail header
		/// </summary>
		/// <param name="order">Business entity representing the applyPurchaseDetail</param>
		public int Delete(ApplyPurchaseDetailInfo applyPurchaseDetail){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("tbpurchase_ApplyPurchaseDetail_delete_sp");
			db.AddInParameter(command, "ApplyPurchaseCode", DbType.String,applyPurchaseDetail.ApplyPurchaseCode);
			db.AddInParameter(command, "ItemNo", DbType.Int32,applyPurchaseDetail.ItemNo);
			return db.ExecuteNonQuery(command);
		}
		
		public IDataReader getALL(){
			Database db = DatabaseFactory.CreateDatabase();
            IDataReader dr = null;
            DbCommand command = db.GetStoredProcCommand("tbpurchase_ApplyPurchaseDetail_getAll_sp");
			
            dr = db.ExecuteReader(command);
			
			return dr;
		}
		
		/// <summary>
		/// Reads the applyPurchaseDetail information for a primary key
		/// </summary>
		/// <returns>Business entity representing the applyPurchaseDetail</returns>
		
		public ApplyPurchaseDetailInfo getApplyPurchaseDetail(System.String applyPurchaseCode, System.Int32 itemNo){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("tbpurchase_ApplyPurchaseDetail_get_sp");
			db.AddInParameter(command, "ApplyPurchaseCode", DbType.String,applyPurchaseCode);
			db.AddInParameter(command, "ItemNo", DbType.Int32,itemNo);
			
			ApplyPurchaseDetailInfo detail = new ApplyPurchaseDetailInfo();

            using( IDataReader dr = db.ExecuteReader(command) ) {
                
                if( dr.Read() ){
					if (!dr.IsDBNull(0)) detail.ApplyPurchaseCode = dr.GetString(0);
					if (!dr.IsDBNull(1)) detail.ItemNo = dr.GetInt32(1);
					if (!dr.IsDBNull(2)) detail.CaseItemNo = dr.GetInt32(2);
					if (!dr.IsDBNull(3)) detail.BOMNO = dr.GetString(3);
					if (!dr.IsDBNull(4)) detail.BOMName = dr.GetString(4);
					if (!dr.IsDBNull(5)) detail.BOMBrand = dr.GetString(5);
					if (!dr.IsDBNull(6)) detail.Specification = dr.GetString(6);
					if (!dr.IsDBNull(7)) detail.Manufacture = dr.GetString(7);
					if (!dr.IsDBNull(8)) detail.Unit = dr.GetString(8);
					if (!dr.IsDBNull(9)) detail.Price = dr.GetDecimal(9);
					if (!dr.IsDBNull(10)) detail.Quantity = dr.GetDecimal(10);
					if (!dr.IsDBNull(11)) detail.Sum = dr.GetDecimal(11);
					if (!dr.IsDBNull(12)) detail.RequireDate = dr.GetDateTime(12);
					if (!dr.IsDBNull(13)) detail.BudgetSum = dr.GetDecimal(13);
					if (!dr.IsDBNull(14)) detail.PurchaseCode = dr.GetString(14);
					if (!dr.IsDBNull(15)) detail.PurchaseItemNo = dr.GetInt32(15);
					if (!dr.IsDBNull(16)) detail.AccountingSubject = dr.GetString(16);
					if (!dr.IsDBNull(17)) detail.PurchaseType = dr.GetString(17);
					if (!dr.IsDBNull(18)) detail.Memo = dr.GetString(18);
					if (!dr.IsDBNull(19)) detail.StandbyField1 = dr.GetString(19);
					if (!dr.IsDBNull(20)) detail.StandbyField2 = dr.GetString(20);
					if (!dr.IsDBNull(21)) detail.StandbyField3 = dr.GetDecimal(21);
					if (!dr.IsDBNull(22)) detail.StandbyField4 = dr.GetInt32(22);
					if (!dr.IsDBNull(23)) detail.CreaterID = dr.GetString(23);
					if (!dr.IsDBNull(24)) detail.CreaterDate = dr.GetDateTime(24);
					if (!dr.IsDBNull(25)) detail.ModiID = dr.GetString(25);
					if (!dr.IsDBNull(26)) detail.ModiDate = dr.GetDateTime(26);
                }
            }
			
			return detail;
		}
		
		
		public IDataReader find(
			System.String applyPurchaseCode, 
			System.Int32 itemNo, 
			System.Int32? caseItemNo, 
			System.String bomno, 
			System.String bOMName, 
			System.String bOMBrand, 
			System.String specification, 
			System.String manufacture, 
			System.String unit, 
			System.Decimal? price, 
			System.Decimal? quantity, 
			System.Decimal? sum, 
			System.DateTime? requireDate, 
			System.Decimal? budgetSum, 
			System.String purchaseCode, 
			System.Int32? purchaseItemNo, 
			System.String accountingSubject, 
			System.String purchaseType, 
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
            DbCommand command = db.GetStoredProcCommand("tbpurchase_ApplyPurchaseDetail_find_sp");
			db.AddParameter(command, "ApplyPurchaseCode", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, applyPurchaseCode);
			db.AddParameter(command, "ItemNo", DbType.Int32, 4, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, (itemNo == 0) ? DBNull.Value : (object) itemNo);
			db.AddParameter(command, "CaseItemNo", DbType.Int32, 4, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, caseItemNo);
			db.AddParameter(command, "BOMNO", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, bomno);
			db.AddParameter(command, "BOMName", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, bOMName);
			db.AddParameter(command, "BOMBrand", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, bOMBrand);
			db.AddParameter(command, "Specification", DbType.String, 300, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, specification);
			db.AddParameter(command, "Manufacture", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, manufacture);
			db.AddParameter(command, "Unit", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, unit);
			db.AddParameter(command, "Price", DbType.Decimal, 9, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, price);
			db.AddParameter(command, "Quantity", DbType.Decimal, 9, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, quantity);
			db.AddParameter(command, "Sum", DbType.Decimal, 9, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, sum);
			db.AddParameter(command, "RequireDate", DbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, requireDate);
			db.AddParameter(command, "BudgetSum", DbType.Decimal, 9, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, budgetSum);
			db.AddParameter(command, "PurchaseCode", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, purchaseCode);
			db.AddParameter(command, "PurchaseItemNo", DbType.Int32, 4, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, purchaseItemNo);
			db.AddParameter(command, "AccountingSubject", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, accountingSubject);
			db.AddParameter(command, "PurchaseType", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, purchaseType);
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
		
		public IDataReader find(ApplyPurchaseDetailInfo applyPurchaseDetail, int startRowIndex, int maximumRows){
			return find(
				applyPurchaseDetail.ApplyPurchaseCode,  
				applyPurchaseDetail.ItemNo,  
				applyPurchaseDetail.CaseItemNo,  
				applyPurchaseDetail.BOMNO,  
				applyPurchaseDetail.BOMName,  
				applyPurchaseDetail.BOMBrand,  
				applyPurchaseDetail.Specification,  
				applyPurchaseDetail.Manufacture,  
				applyPurchaseDetail.Unit,  
				applyPurchaseDetail.Price,  
				applyPurchaseDetail.Quantity,  
				applyPurchaseDetail.Sum,  
				applyPurchaseDetail.RequireDate,  
				applyPurchaseDetail.BudgetSum,  
				applyPurchaseDetail.PurchaseCode,  
				applyPurchaseDetail.PurchaseItemNo,  
				applyPurchaseDetail.AccountingSubject,  
				applyPurchaseDetail.PurchaseType,  
				applyPurchaseDetail.Memo,  
				applyPurchaseDetail.StandbyField1,  
				applyPurchaseDetail.StandbyField2,  
				applyPurchaseDetail.StandbyField3,  
				applyPurchaseDetail.StandbyField4,  
				applyPurchaseDetail.CreaterID,  
				applyPurchaseDetail.CreaterDate,  
				applyPurchaseDetail.ModiID,  
				applyPurchaseDetail.ModiDate,  
				startRowIndex, 
				maximumRows);
		}
		
		public int findCount(
			System.String applyPurchaseCode, 
			System.Int32 itemNo, 
			System.Int32? caseItemNo, 
			System.String bomno, 
			System.String bOMName, 
			System.String bOMBrand, 
			System.String specification, 
			System.String manufacture, 
			System.String unit, 
			System.Decimal? price, 
			System.Decimal? quantity, 
			System.Decimal? sum, 
			System.DateTime? requireDate, 
			System.Decimal? budgetSum, 
			System.String purchaseCode, 
			System.Int32? purchaseItemNo, 
			System.String accountingSubject, 
			System.String purchaseType, 
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
            DbCommand command = db.GetStoredProcCommand("tbpurchase_ApplyPurchaseDetail_findCount_sp");
			db.AddParameter(command, "ApplyPurchaseCode", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, applyPurchaseCode);
			db.AddParameter(command, "ItemNo", DbType.Int32, 4, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, (itemNo == 0) ? DBNull.Value : (object) itemNo);
			db.AddParameter(command, "CaseItemNo", DbType.Int32, 4, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, caseItemNo);
			db.AddParameter(command, "BOMNO", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, bomno);
			db.AddParameter(command, "BOMName", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, bOMName);
			db.AddParameter(command, "BOMBrand", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, bOMBrand);
			db.AddParameter(command, "Specification", DbType.String, 300, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, specification);
			db.AddParameter(command, "Manufacture", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, manufacture);
			db.AddParameter(command, "Unit", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, unit);
			db.AddParameter(command, "Price", DbType.Decimal, 9, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, price);
			db.AddParameter(command, "Quantity", DbType.Decimal, 9, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, quantity);
			db.AddParameter(command, "Sum", DbType.Decimal, 9, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, sum);
			db.AddParameter(command, "RequireDate", DbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, requireDate);
			db.AddParameter(command, "BudgetSum", DbType.Decimal, 9, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, budgetSum);
			db.AddParameter(command, "PurchaseCode", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, purchaseCode);
			db.AddParameter(command, "PurchaseItemNo", DbType.Int32, 4, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, purchaseItemNo);
			db.AddParameter(command, "AccountingSubject", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, accountingSubject);
			db.AddParameter(command, "PurchaseType", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, purchaseType);
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
		
		public int findCount(ApplyPurchaseDetailInfo applyPurchaseDetail, int startRowIndex, int maximumRows){
			return findCount(
				applyPurchaseDetail.ApplyPurchaseCode, 
				applyPurchaseDetail.ItemNo, 
				applyPurchaseDetail.CaseItemNo, 
				applyPurchaseDetail.BOMNO, 
				applyPurchaseDetail.BOMName, 
				applyPurchaseDetail.BOMBrand, 
				applyPurchaseDetail.Specification, 
				applyPurchaseDetail.Manufacture, 
				applyPurchaseDetail.Unit, 
				applyPurchaseDetail.Price, 
				applyPurchaseDetail.Quantity, 
				applyPurchaseDetail.Sum, 
				applyPurchaseDetail.RequireDate, 
				applyPurchaseDetail.BudgetSum, 
				applyPurchaseDetail.PurchaseCode, 
				applyPurchaseDetail.PurchaseItemNo, 
				applyPurchaseDetail.AccountingSubject, 
				applyPurchaseDetail.PurchaseType, 
				applyPurchaseDetail.Memo, 
				applyPurchaseDetail.StandbyField1, 
				applyPurchaseDetail.StandbyField2, 
				applyPurchaseDetail.StandbyField3, 
				applyPurchaseDetail.StandbyField4, 
				applyPurchaseDetail.CreaterID, 
				applyPurchaseDetail.CreaterDate, 
				applyPurchaseDetail.ModiID, 
				applyPurchaseDetail.ModiDate, 
				startRowIndex, 
				maximumRows);
		}
	}
}