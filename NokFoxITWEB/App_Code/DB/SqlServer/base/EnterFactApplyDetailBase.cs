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
	public abstract class EnterFactApplyDetailBase{
		
		/// <summary>
		/// If enterFactApplyDetail is exist in database.
		/// </summary>
		/// <param name="enterFactApplyDetail">Business entity representing the enterFactApplyDetail.</param>
		/// <returns>bool. If exist return true, else return false.</returns>
		public bool isExist(EnterFactApplyDetailInfo enterFactApplyDetail){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_appEnterFactApplyDetailIsExist");
			
			db.AddInParameter(command, "ApplyCode", DbType.String,enterFactApplyDetail.ApplyCode);
			db.AddInParameter(command, "ItemNo", DbType.Int32,enterFactApplyDetail.ItemNo);
			
			int result = 0;
			result = db.ExecuteNonQuery(command);
			
			if( result > 0 ){
				return true;
			}else{
				return false;
			} 
		}
		
		/// <summary>
		/// Insert a enterFactApplyDetail to Database.
		/// </summary>
		/// <param name="order">Business entity representing the enterFactApplyDetail</param>
		/// <returns>OrderId</returns>
		public int Insert(EnterFactApplyDetailInfo enterFactApplyDetail){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_appEnterFactApplyDetailInsert");
			db.AddInParameter(command, "ApplyCode", DbType.String,enterFactApplyDetail.ApplyCode);
			db.AddInParameter(command, "ItemNo", DbType.Int32,enterFactApplyDetail.ItemNo);
			db.AddInParameter(command, "StaffName", DbType.String,enterFactApplyDetail.StaffName);
			db.AddInParameter(command, "IDCardNo", DbType.String,enterFactApplyDetail.IDCardNo);
			db.AddInParameter(command, "Company", DbType.String,enterFactApplyDetail.Company);
			db.AddInParameter(command, "Tel", DbType.String,enterFactApplyDetail.Tel);
			db.AddInParameter(command, "EnterFactReason", DbType.String,enterFactApplyDetail.EnterFactReason);
			db.AddInParameter(command, "GateHouse", DbType.String,enterFactApplyDetail.GateHouse);
			db.AddInParameter(command, "ReceptionDept", DbType.String,enterFactApplyDetail.ReceptionDept);
			db.AddInParameter(command, "ReceptionStaff", DbType.String,enterFactApplyDetail.ReceptionStaff);
			db.AddInParameter(command, "ReceptionTel", DbType.String,enterFactApplyDetail.ReceptionTel);
			db.AddInParameter(command, "ExpectedEnterDate", DbType.DateTime,enterFactApplyDetail.ExpectedEnterDate);
			db.AddInParameter(command, "ExpectedEnterTime", DbType.String,enterFactApplyDetail.ExpectedEnterTime);
			db.AddInParameter(command, "ExpectedLeaveDate", DbType.DateTime,enterFactApplyDetail.ExpectedLeaveDate);
			db.AddInParameter(command, "ExpectedLeaveTime", DbType.String,enterFactApplyDetail.ExpectedLeaveTime);
			db.AddInParameter(command, "ActualEnterDate", DbType.DateTime,enterFactApplyDetail.ActualEnterDate);
			db.AddInParameter(command, "ActualEnterTime", DbType.String,enterFactApplyDetail.ActualEnterTime);
			db.AddInParameter(command, "ActualLeaveDate", DbType.DateTime,enterFactApplyDetail.ActualLeaveDate);
			db.AddInParameter(command, "ActualLeaveTime", DbType.String,enterFactApplyDetail.ActualLeaveTime);
			db.AddInParameter(command, "CardNo", DbType.String,enterFactApplyDetail.CardNo);
			db.AddInParameter(command, "RightDescription", DbType.String,enterFactApplyDetail.RightDescription);
			db.AddInParameter(command, "CardStatus", DbType.String,enterFactApplyDetail.CardStatus);
			db.AddInParameter(command, "TakeItems", DbType.String,enterFactApplyDetail.TakeItems);
			db.AddInParameter(command, "Memo", DbType.String,enterFactApplyDetail.Memo);
			db.AddInParameter(command, "ReserveField1", DbType.Int32,enterFactApplyDetail.ReserveField1);
			db.AddInParameter(command, "ReserveField2", DbType.Decimal,enterFactApplyDetail.ReserveField2);
			db.AddInParameter(command, "ReserveField3", DbType.String,enterFactApplyDetail.ReserveField3);
			db.AddInParameter(command, "ReserveField4", DbType.String,enterFactApplyDetail.ReserveField4);
			db.AddInParameter(command, "InitiateId", DbType.String,enterFactApplyDetail.InitiateId);
			db.AddInParameter(command, "InitiateDate", DbType.DateTime,enterFactApplyDetail.InitiateDate);
			db.AddInParameter(command, "ModiId", DbType.String,enterFactApplyDetail.ModiId);
			db.AddInParameter(command, "ModiDate", DbType.DateTime,enterFactApplyDetail.ModiDate);
			return db.ExecuteNonQuery(command);
		}
		
		/// <summary>
		/// Method to Update an enterFactApplyDetail header
		/// </summary>
		/// <param name="order">Business entity representing the enterFactApplyDetail</param>
		public int Update(EnterFactApplyDetailInfo enterFactApplyDetail){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_appEnterFactApplyDetailUpdate");
			db.AddInParameter(command, "ApplyCode", DbType.String,enterFactApplyDetail.ApplyCode);
			db.AddInParameter(command, "ItemNo", DbType.Int32,enterFactApplyDetail.ItemNo);
			db.AddInParameter(command, "StaffName", DbType.String,enterFactApplyDetail.StaffName);
			db.AddInParameter(command, "IDCardNo", DbType.String,enterFactApplyDetail.IDCardNo);
			db.AddInParameter(command, "Company", DbType.String,enterFactApplyDetail.Company);
			db.AddInParameter(command, "Tel", DbType.String,enterFactApplyDetail.Tel);
			db.AddInParameter(command, "EnterFactReason", DbType.String,enterFactApplyDetail.EnterFactReason);
			db.AddInParameter(command, "GateHouse", DbType.String,enterFactApplyDetail.GateHouse);
			db.AddInParameter(command, "ReceptionDept", DbType.String,enterFactApplyDetail.ReceptionDept);
			db.AddInParameter(command, "ReceptionStaff", DbType.String,enterFactApplyDetail.ReceptionStaff);
			db.AddInParameter(command, "ReceptionTel", DbType.String,enterFactApplyDetail.ReceptionTel);
			db.AddInParameter(command, "ExpectedEnterDate", DbType.DateTime,enterFactApplyDetail.ExpectedEnterDate);
			db.AddInParameter(command, "ExpectedEnterTime", DbType.String,enterFactApplyDetail.ExpectedEnterTime);
			db.AddInParameter(command, "ExpectedLeaveDate", DbType.DateTime,enterFactApplyDetail.ExpectedLeaveDate);
			db.AddInParameter(command, "ExpectedLeaveTime", DbType.String,enterFactApplyDetail.ExpectedLeaveTime);
			db.AddInParameter(command, "ActualEnterDate", DbType.DateTime,enterFactApplyDetail.ActualEnterDate);
			db.AddInParameter(command, "ActualEnterTime", DbType.String,enterFactApplyDetail.ActualEnterTime);
			db.AddInParameter(command, "ActualLeaveDate", DbType.DateTime,enterFactApplyDetail.ActualLeaveDate);
			db.AddInParameter(command, "ActualLeaveTime", DbType.String,enterFactApplyDetail.ActualLeaveTime);
			db.AddInParameter(command, "CardNo", DbType.String,enterFactApplyDetail.CardNo);
			db.AddInParameter(command, "RightDescription", DbType.String,enterFactApplyDetail.RightDescription);
			db.AddInParameter(command, "CardStatus", DbType.String,enterFactApplyDetail.CardStatus);
			db.AddInParameter(command, "TakeItems", DbType.String,enterFactApplyDetail.TakeItems);
			db.AddInParameter(command, "Memo", DbType.String,enterFactApplyDetail.Memo);
			db.AddInParameter(command, "ReserveField1", DbType.Int32,enterFactApplyDetail.ReserveField1);
			db.AddInParameter(command, "ReserveField2", DbType.Decimal,enterFactApplyDetail.ReserveField2);
			db.AddInParameter(command, "ReserveField3", DbType.String,enterFactApplyDetail.ReserveField3);
			db.AddInParameter(command, "ReserveField4", DbType.String,enterFactApplyDetail.ReserveField4);
			db.AddInParameter(command, "InitiateId", DbType.String,enterFactApplyDetail.InitiateId);
			db.AddInParameter(command, "InitiateDate", DbType.DateTime,enterFactApplyDetail.InitiateDate);
			db.AddInParameter(command, "ModiId", DbType.String,enterFactApplyDetail.ModiId);
			db.AddInParameter(command, "ModiDate", DbType.DateTime,enterFactApplyDetail.ModiDate);
			return db.ExecuteNonQuery(command);
		}
		
		/// <summary>
		/// Method to delete an enterFactApplyDetail header
		/// </summary>
		/// <param name="order">Business entity representing the enterFactApplyDetail</param>
		public int Delete(EnterFactApplyDetailInfo enterFactApplyDetail){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_appEnterFactApplyDetailDelete");
			db.AddInParameter(command, "ApplyCode", DbType.String,enterFactApplyDetail.ApplyCode);
			db.AddInParameter(command, "ItemNo", DbType.Int32,enterFactApplyDetail.ItemNo);
			return db.ExecuteNonQuery(command);
		}
		
		public IDataReader getALL(){
			Database db = DatabaseFactory.CreateDatabase();
            IDataReader dr = null;
            DbCommand command = db.GetStoredProcCommand("usp_appEnterFactApplyDetailGetAll");
			
            dr = db.ExecuteReader(command);
			
			return dr;
		}
		
		/// <summary>
		/// Reads the enterFactApplyDetail information for a primary key
		/// </summary>
		/// <returns>Business entity representing the enterFactApplyDetail</returns>
		
		public EnterFactApplyDetailInfo getEnterFactApplyDetail(System.String applyCode, System.Int32 itemNo){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_appEnterFactApplyDetailGet");
			db.AddInParameter(command, "ApplyCode", DbType.String,applyCode);
			db.AddInParameter(command, "ItemNo", DbType.Int32,itemNo);
			
			EnterFactApplyDetailInfo detail = new EnterFactApplyDetailInfo();

            using( IDataReader dr = db.ExecuteReader(command) ) {
                
                if( dr.Read() ){
					if (!dr.IsDBNull(0)) detail.ApplyCode = dr.GetString(0);
					if (!dr.IsDBNull(1)) detail.ItemNo = dr.GetInt32(1);
					if (!dr.IsDBNull(2)) detail.StaffName = dr.GetString(2);
					if (!dr.IsDBNull(3)) detail.IDCardNo = dr.GetString(3);
					if (!dr.IsDBNull(4)) detail.Company = dr.GetString(4);
					if (!dr.IsDBNull(5)) detail.Tel = dr.GetString(5);
					if (!dr.IsDBNull(6)) detail.EnterFactReason = dr.GetString(6);
					if (!dr.IsDBNull(7)) detail.GateHouse = dr.GetString(7);
					if (!dr.IsDBNull(8)) detail.ReceptionDept = dr.GetString(8);
					if (!dr.IsDBNull(9)) detail.ReceptionStaff = dr.GetString(9);
					if (!dr.IsDBNull(10)) detail.ReceptionTel = dr.GetString(10);
					if (!dr.IsDBNull(11)) detail.ExpectedEnterDate = dr.GetDateTime(11);
					if (!dr.IsDBNull(12)) detail.ExpectedEnterTime = dr.GetString(12);
					if (!dr.IsDBNull(13)) detail.ExpectedLeaveDate = dr.GetDateTime(13);
					if (!dr.IsDBNull(14)) detail.ExpectedLeaveTime = dr.GetString(14);
					if (!dr.IsDBNull(15)) detail.ActualEnterDate = dr.GetDateTime(15);
					if (!dr.IsDBNull(16)) detail.ActualEnterTime = dr.GetString(16);
					if (!dr.IsDBNull(17)) detail.ActualLeaveDate = dr.GetDateTime(17);
					if (!dr.IsDBNull(18)) detail.ActualLeaveTime = dr.GetString(18);
					if (!dr.IsDBNull(19)) detail.CardNo = dr.GetString(19);
					if (!dr.IsDBNull(20)) detail.RightDescription = dr.GetString(20);
					if (!dr.IsDBNull(21)) detail.CardStatus = dr.GetString(21);
					if (!dr.IsDBNull(22)) detail.TakeItems = dr.GetString(22);
					if (!dr.IsDBNull(23)) detail.Memo = dr.GetString(23);
					if (!dr.IsDBNull(24)) detail.ReserveField1 = dr.GetInt32(24);
					if (!dr.IsDBNull(25)) detail.ReserveField2 = dr.GetDecimal(25);
					if (!dr.IsDBNull(26)) detail.ReserveField3 = dr.GetString(26);
					if (!dr.IsDBNull(27)) detail.ReserveField4 = dr.GetString(27);
					if (!dr.IsDBNull(28)) detail.InitiateId = dr.GetString(28);
					if (!dr.IsDBNull(29)) detail.InitiateDate = dr.GetDateTime(29);
					if (!dr.IsDBNull(30)) detail.ModiId = dr.GetString(30);
					if (!dr.IsDBNull(31)) detail.ModiDate = dr.GetDateTime(31);
                }
            }
			
			return detail;
		}
		
		
		public IDataReader find(
			System.String applyCode, 
			System.Int32 itemNo, 
			System.String staffName, 
			System.String iDCardNo, 
			System.String company, 
			System.String tel, 
			System.String enterFactReason, 
			System.String gateHouse, 
			System.String receptionDept, 
			System.String receptionStaff, 
			System.String receptionTel, 
			System.DateTime? expectedEnterDate, 
			System.String expectedEnterTime, 
			System.DateTime? expectedLeaveDate, 
			System.String expectedLeaveTime, 
			System.DateTime? actualEnterDate, 
			System.String actualEnterTime, 
			System.DateTime? actualLeaveDate, 
			System.String actualLeaveTime, 
			System.String cardNo, 
			System.String rightDescription, 
			System.String cardStatus, 
			System.String takeItems, 
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
            DbCommand command = db.GetStoredProcCommand("usp_appEnterFactApplyDetailFind");
			db.AddParameter(command, "ApplyCode", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, applyCode);
			db.AddParameter(command, "ItemNo", DbType.Int32, 4, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, (itemNo == 0) ? DBNull.Value : (object) itemNo);
			db.AddParameter(command, "StaffName", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, staffName);
			db.AddParameter(command, "IDCardNo", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, iDCardNo);
			db.AddParameter(command, "Company", DbType.String, 300, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, company);
			db.AddParameter(command, "Tel", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, tel);
			db.AddParameter(command, "EnterFactReason", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, enterFactReason);
			db.AddParameter(command, "GateHouse", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, gateHouse);
			db.AddParameter(command, "ReceptionDept", DbType.String, 300, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, receptionDept);
			db.AddParameter(command, "ReceptionStaff", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, receptionStaff);
			db.AddParameter(command, "ReceptionTel", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, receptionTel);
			db.AddParameter(command, "ExpectedEnterDate", DbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, expectedEnterDate);
			db.AddParameter(command, "ExpectedEnterTime", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, expectedEnterTime);
			db.AddParameter(command, "ExpectedLeaveDate", DbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, expectedLeaveDate);
			db.AddParameter(command, "ExpectedLeaveTime", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, expectedLeaveTime);
			db.AddParameter(command, "ActualEnterDate", DbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, actualEnterDate);
			db.AddParameter(command, "ActualEnterTime", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, actualEnterTime);
			db.AddParameter(command, "ActualLeaveDate", DbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, actualLeaveDate);
			db.AddParameter(command, "ActualLeaveTime", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, actualLeaveTime);
			db.AddParameter(command, "CardNo", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, cardNo);
			db.AddParameter(command, "RightDescription", DbType.String, 300, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, rightDescription);
			db.AddParameter(command, "CardStatus", DbType.String, 1, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, cardStatus);
			db.AddParameter(command, "TakeItems", DbType.String, 1000, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, takeItems);
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
		
		public IDataReader find(EnterFactApplyDetailInfo enterFactApplyDetail, int startRowIndex, int maximumRows){
			return find(
				enterFactApplyDetail.ApplyCode,  
				enterFactApplyDetail.ItemNo,  
				enterFactApplyDetail.StaffName,  
				enterFactApplyDetail.IDCardNo,  
				enterFactApplyDetail.Company,  
				enterFactApplyDetail.Tel,  
				enterFactApplyDetail.EnterFactReason,  
				enterFactApplyDetail.GateHouse,  
				enterFactApplyDetail.ReceptionDept,  
				enterFactApplyDetail.ReceptionStaff,  
				enterFactApplyDetail.ReceptionTel,  
				enterFactApplyDetail.ExpectedEnterDate,  
				enterFactApplyDetail.ExpectedEnterTime,  
				enterFactApplyDetail.ExpectedLeaveDate,  
				enterFactApplyDetail.ExpectedLeaveTime,  
				enterFactApplyDetail.ActualEnterDate,  
				enterFactApplyDetail.ActualEnterTime,  
				enterFactApplyDetail.ActualLeaveDate,  
				enterFactApplyDetail.ActualLeaveTime,  
				enterFactApplyDetail.CardNo,  
				enterFactApplyDetail.RightDescription,  
				enterFactApplyDetail.CardStatus,  
				enterFactApplyDetail.TakeItems,  
				enterFactApplyDetail.Memo,  
				enterFactApplyDetail.ReserveField1,  
				enterFactApplyDetail.ReserveField2,  
				enterFactApplyDetail.ReserveField3,  
				enterFactApplyDetail.ReserveField4,  
				enterFactApplyDetail.InitiateId,  
				enterFactApplyDetail.InitiateDate,  
				enterFactApplyDetail.ModiId,  
				enterFactApplyDetail.ModiDate,  
				startRowIndex, 
				maximumRows);
		}
		
		public int findCount(
			System.String applyCode, 
			System.Int32 itemNo, 
			System.String staffName, 
			System.String iDCardNo, 
			System.String company, 
			System.String tel, 
			System.String enterFactReason, 
			System.String gateHouse, 
			System.String receptionDept, 
			System.String receptionStaff, 
			System.String receptionTel, 
			System.DateTime? expectedEnterDate, 
			System.String expectedEnterTime, 
			System.DateTime? expectedLeaveDate, 
			System.String expectedLeaveTime, 
			System.DateTime? actualEnterDate, 
			System.String actualEnterTime, 
			System.DateTime? actualLeaveDate, 
			System.String actualLeaveTime, 
			System.String cardNo, 
			System.String rightDescription, 
			System.String cardStatus, 
			System.String takeItems, 
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
            DbCommand command = db.GetStoredProcCommand("usp_appEnterFactApplyDetailFindCount");
			db.AddParameter(command, "ApplyCode", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, applyCode);
			db.AddParameter(command, "ItemNo", DbType.Int32, 4, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, (itemNo == 0) ? DBNull.Value : (object) itemNo);
			db.AddParameter(command, "StaffName", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, staffName);
			db.AddParameter(command, "IDCardNo", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, iDCardNo);
			db.AddParameter(command, "Company", DbType.String, 300, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, company);
			db.AddParameter(command, "Tel", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, tel);
			db.AddParameter(command, "EnterFactReason", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, enterFactReason);
			db.AddParameter(command, "GateHouse", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, gateHouse);
			db.AddParameter(command, "ReceptionDept", DbType.String, 300, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, receptionDept);
			db.AddParameter(command, "ReceptionStaff", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, receptionStaff);
			db.AddParameter(command, "ReceptionTel", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, receptionTel);
			db.AddParameter(command, "ExpectedEnterDate", DbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, expectedEnterDate);
			db.AddParameter(command, "ExpectedEnterTime", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, expectedEnterTime);
			db.AddParameter(command, "ExpectedLeaveDate", DbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, expectedLeaveDate);
			db.AddParameter(command, "ExpectedLeaveTime", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, expectedLeaveTime);
			db.AddParameter(command, "ActualEnterDate", DbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, actualEnterDate);
			db.AddParameter(command, "ActualEnterTime", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, actualEnterTime);
			db.AddParameter(command, "ActualLeaveDate", DbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, actualLeaveDate);
			db.AddParameter(command, "ActualLeaveTime", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, actualLeaveTime);
			db.AddParameter(command, "CardNo", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, cardNo);
			db.AddParameter(command, "RightDescription", DbType.String, 300, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, rightDescription);
			db.AddParameter(command, "CardStatus", DbType.String, 1, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, cardStatus);
			db.AddParameter(command, "TakeItems", DbType.String, 1000, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, takeItems);
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
		
		public int findCount(EnterFactApplyDetailInfo enterFactApplyDetail, int startRowIndex, int maximumRows){
			return findCount(
				enterFactApplyDetail.ApplyCode, 
				enterFactApplyDetail.ItemNo, 
				enterFactApplyDetail.StaffName, 
				enterFactApplyDetail.IDCardNo, 
				enterFactApplyDetail.Company, 
				enterFactApplyDetail.Tel, 
				enterFactApplyDetail.EnterFactReason, 
				enterFactApplyDetail.GateHouse, 
				enterFactApplyDetail.ReceptionDept, 
				enterFactApplyDetail.ReceptionStaff, 
				enterFactApplyDetail.ReceptionTel, 
				enterFactApplyDetail.ExpectedEnterDate, 
				enterFactApplyDetail.ExpectedEnterTime, 
				enterFactApplyDetail.ExpectedLeaveDate, 
				enterFactApplyDetail.ExpectedLeaveTime, 
				enterFactApplyDetail.ActualEnterDate, 
				enterFactApplyDetail.ActualEnterTime, 
				enterFactApplyDetail.ActualLeaveDate, 
				enterFactApplyDetail.ActualLeaveTime, 
				enterFactApplyDetail.CardNo, 
				enterFactApplyDetail.RightDescription, 
				enterFactApplyDetail.CardStatus, 
				enterFactApplyDetail.TakeItems, 
				enterFactApplyDetail.Memo, 
				enterFactApplyDetail.ReserveField1, 
				enterFactApplyDetail.ReserveField2, 
				enterFactApplyDetail.ReserveField3, 
				enterFactApplyDetail.ReserveField4, 
				enterFactApplyDetail.InitiateId, 
				enterFactApplyDetail.InitiateDate, 
				enterFactApplyDetail.ModiId, 
				enterFactApplyDetail.ModiDate, 
				startRowIndex, 
				maximumRows);
		}
	}
}