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
	public abstract class GatehouseBase{
		
		/// <summary>
		/// If gatehouse is exist in database.
		/// </summary>
		/// <param name="gatehouse">Business entity representing the gatehouse.</param>
		/// <returns>bool. If exist return true, else return false.</returns>
		public bool isExist(GatehouseInfo gatehouse){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_pubGatehouseIsExist");
			
			db.AddInParameter(command, "GatehouseCode", DbType.String,gatehouse.GatehouseCode);
			
			int result = 0;
			result = db.ExecuteNonQuery(command);
			
			if( result > 0 ){
				return true;
			}else{
				return false;
			} 
		}
		
		/// <summary>
		/// Insert a gatehouse to Database.
		/// </summary>
		/// <param name="order">Business entity representing the gatehouse</param>
		/// <returns>OrderId</returns>
		public int Insert(GatehouseInfo gatehouse){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_pubGatehouseInsert");
			db.AddInParameter(command, "GatehouseCode", DbType.String,gatehouse.GatehouseCode);
			db.AddInParameter(command, "Description", DbType.String,gatehouse.Description);
			db.AddInParameter(command, "Memo", DbType.String,gatehouse.Memo);
			db.AddInParameter(command, "ReserveField1", DbType.Int32,gatehouse.ReserveField1);
			db.AddInParameter(command, "ReserveField2", DbType.Decimal,gatehouse.ReserveField2);
			db.AddInParameter(command, "ReserveField3", DbType.String,gatehouse.ReserveField3);
			db.AddInParameter(command, "ReserveField4", DbType.String,gatehouse.ReserveField4);
			db.AddInParameter(command, "InitiateId", DbType.String,gatehouse.InitiateId);
			db.AddInParameter(command, "InitiateDate", DbType.DateTime,gatehouse.InitiateDate);
			db.AddInParameter(command, "ModiId", DbType.String,gatehouse.ModiId);
			db.AddInParameter(command, "ModiDate", DbType.DateTime,gatehouse.ModiDate);
			return db.ExecuteNonQuery(command);
		}
		
		/// <summary>
		/// Method to Update an gatehouse header
		/// </summary>
		/// <param name="order">Business entity representing the gatehouse</param>
		public int Update(GatehouseInfo gatehouse){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_pubGatehouseUpdate");
			db.AddInParameter(command, "GatehouseCode", DbType.String,gatehouse.GatehouseCode);
			db.AddInParameter(command, "Description", DbType.String,gatehouse.Description);
			db.AddInParameter(command, "Memo", DbType.String,gatehouse.Memo);
			db.AddInParameter(command, "ReserveField1", DbType.Int32,gatehouse.ReserveField1);
			db.AddInParameter(command, "ReserveField2", DbType.Decimal,gatehouse.ReserveField2);
			db.AddInParameter(command, "ReserveField3", DbType.String,gatehouse.ReserveField3);
			db.AddInParameter(command, "ReserveField4", DbType.String,gatehouse.ReserveField4);
			db.AddInParameter(command, "InitiateId", DbType.String,gatehouse.InitiateId);
			db.AddInParameter(command, "InitiateDate", DbType.DateTime,gatehouse.InitiateDate);
			db.AddInParameter(command, "ModiId", DbType.String,gatehouse.ModiId);
			db.AddInParameter(command, "ModiDate", DbType.DateTime,gatehouse.ModiDate);
			return db.ExecuteNonQuery(command);
		}
		
		/// <summary>
		/// Method to delete an gatehouse header
		/// </summary>
		/// <param name="order">Business entity representing the gatehouse</param>
		public int Delete(GatehouseInfo gatehouse){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_pubGatehouseDelete");
			db.AddInParameter(command, "GatehouseCode", DbType.String,gatehouse.GatehouseCode);
			return db.ExecuteNonQuery(command);
		}
		
		public IDataReader getALL(){
			Database db = DatabaseFactory.CreateDatabase();
            IDataReader dr = null;
            DbCommand command = db.GetStoredProcCommand("usp_pubGatehouseGetAll");
			
            dr = db.ExecuteReader(command);
			
			return dr;
		}
		
		/// <summary>
		/// Reads the gatehouse information for a primary key
		/// </summary>
		/// <returns>Business entity representing the gatehouse</returns>
		
		public GatehouseInfo getGatehouse(System.String gatehouseCode){
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("usp_pubGatehouseGet");
			db.AddInParameter(command, "GatehouseCode", DbType.String,gatehouseCode);
			
			GatehouseInfo detail = new GatehouseInfo();

            using( IDataReader dr = db.ExecuteReader(command) ) {
                
                if( dr.Read() ){
					if (!dr.IsDBNull(0)) detail.GatehouseCode = dr.GetString(0);
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
			System.String gatehouseCode, 
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
            DbCommand command = db.GetStoredProcCommand("usp_pubGatehouseFind");
			db.AddParameter(command, "GatehouseCode", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, gatehouseCode);
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
		
		public IDataReader find(GatehouseInfo gatehouse, int startRowIndex, int maximumRows){
			return find(
				gatehouse.GatehouseCode,  
				gatehouse.Description,  
				gatehouse.Memo,  
				gatehouse.ReserveField1,  
				gatehouse.ReserveField2,  
				gatehouse.ReserveField3,  
				gatehouse.ReserveField4,  
				gatehouse.InitiateId,  
				gatehouse.InitiateDate,  
				gatehouse.ModiId,  
				gatehouse.ModiDate,  
				startRowIndex, 
				maximumRows);
		}
		
		public int findCount(
			System.String gatehouseCode, 
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
            DbCommand command = db.GetStoredProcCommand("usp_pubGatehouseFindCount");
			db.AddParameter(command, "GatehouseCode", DbType.String, 30, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, gatehouseCode);
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
		
		public int findCount(GatehouseInfo gatehouse, int startRowIndex, int maximumRows){
			return findCount(
				gatehouse.GatehouseCode, 
				gatehouse.Description, 
				gatehouse.Memo, 
				gatehouse.ReserveField1, 
				gatehouse.ReserveField2, 
				gatehouse.ReserveField3, 
				gatehouse.ReserveField4, 
				gatehouse.InitiateId, 
				gatehouse.InitiateDate, 
				gatehouse.ModiId, 
				gatehouse.ModiDate, 
				startRowIndex, 
				maximumRows);
		}
	}
}