using System;

namespace FIH.ForeignStaff.db{
	///
	/// Summary description for GatehouseInfo
	///
	public class GatehouseInfo{
		private System.String _gatehouseCode;
		private System.String _description;
		private System.String _memo;
		private System.Int32? _reserveField1;
		private System.Decimal? _reserveField2;
		private System.String _reserveField3;
		private System.String _reserveField4;
		private System.String _initiateId;
		private System.DateTime? _initiateDate;
		private System.String _modiId;
		private System.DateTime? _modiDate;
		
		public GatehouseInfo(){
		}
		
		public GatehouseInfo(
				System.String  gatehouseCode, 
				System.String  description, 
				System.String  memo, 
				System.Int32?  reserveField1, 
				System.Decimal?  reserveField2, 
				System.String  reserveField3, 
				System.String  reserveField4, 
				System.String  initiateId, 
				System.DateTime?  initiateDate, 
				System.String  modiId, 
				System.DateTime?  modiDate 
			){
			this._gatehouseCode  = gatehouseCode;
			this._description  = description;
			this._memo  = memo;
			this._reserveField1  = reserveField1;
			this._reserveField2  = reserveField2;
			this._reserveField3  = reserveField3;
			this._reserveField4  = reserveField4;
			this._initiateId  = initiateId;
			this._initiateDate  = initiateDate;
			this._modiId  = modiId;
			this._modiDate  = modiDate;
		}
		
		public  System.String GatehouseCode{
			get{ return _gatehouseCode; }
			set{ this._gatehouseCode = value; }
		}
		
		public  System.String Description{
			get{ return _description; }
			set{ this._description = value; }
		}
		
		public  System.String Memo{
			get{ return _memo; }
			set{ this._memo = value; }
		}
		
		public  System.Int32? ReserveField1{
			get{ return _reserveField1; }
			set{ this._reserveField1 = value; }
		}
		
		public  System.Decimal? ReserveField2{
			get{ return _reserveField2; }
			set{ this._reserveField2 = value; }
		}
		
		public  System.String ReserveField3{
			get{ return _reserveField3; }
			set{ this._reserveField3 = value; }
		}
		
		public  System.String ReserveField4{
			get{ return _reserveField4; }
			set{ this._reserveField4 = value; }
		}
		
		public  System.String InitiateId{
			get{ return _initiateId; }
			set{ this._initiateId = value; }
		}
		
		public  System.DateTime? InitiateDate{
			get{ return _initiateDate; }
			set{ this._initiateDate = value; }
		}
		
		public  System.String ModiId{
			get{ return _modiId; }
			set{ this._modiId = value; }
		}
		
		public  System.DateTime? ModiDate{
			get{ return _modiDate; }
			set{ this._modiDate = value; }
		}
		
		
	}
}