using System;

namespace FIH.ForeignStaff.db{
	///
	/// Summary description for EnterFactApplyMasterInfo
	///
	public class EnterFactApplyMasterInfo{
		private System.String _applyCode;
		private System.DateTime? _applyDate;
		private System.String _applyId;
		private System.String _applyDepartment;
		private System.String _tel;
		private System.Boolean? _isBUMgrConfirm;
		private System.String _divisionMgrId;
		private System.DateTime? _divisionConfirmDate;
		private System.String _bUMgrId;
		private System.DateTime? _bUConfirmDate;
		private System.String _memo;
		private System.Int32? _status;
		private System.String _rejectReason;
		private System.String _divisionId;
		private System.String _bUId;
		private System.Int32? _reserveField1;
		private System.Decimal? _reserveField2;
		private System.String _reserveField3;
		private System.String _reserveField4;
		private System.String _initiateId;
		private System.DateTime? _initiateDate;
		private System.String _modiId;
		private System.DateTime? _modiDate;
		
		public EnterFactApplyMasterInfo(){
		}
		
		public EnterFactApplyMasterInfo(
				System.String  applyCode, 
				System.DateTime?  applyDate, 
				System.String  applyId, 
				System.String  applyDepartment, 
				System.String  tel, 
				System.Boolean?  isBUMgrConfirm, 
				System.String  divisionMgrId, 
				System.DateTime?  divisionConfirmDate, 
				System.String  bUMgrId, 
				System.DateTime?  bUConfirmDate, 
				System.String  memo, 
				System.Int32?  status, 
				System.String  rejectReason, 
				System.String  divisionId, 
				System.String  bUId, 
				System.Int32?  reserveField1, 
				System.Decimal?  reserveField2, 
				System.String  reserveField3, 
				System.String  reserveField4, 
				System.String  initiateId, 
				System.DateTime?  initiateDate, 
				System.String  modiId, 
				System.DateTime?  modiDate 
			){
			this._applyCode  = applyCode;
			this._applyDate  = applyDate;
			this._applyId  = applyId;
			this._applyDepartment  = applyDepartment;
			this._tel  = tel;
			this._isBUMgrConfirm  = isBUMgrConfirm;
			this._divisionMgrId  = divisionMgrId;
			this._divisionConfirmDate  = divisionConfirmDate;
			this._bUMgrId  = bUMgrId;
			this._bUConfirmDate  = bUConfirmDate;
			this._memo  = memo;
			this._status  = status;
			this._rejectReason  = rejectReason;
			this._divisionId  = divisionId;
			this._bUId  = bUId;
			this._reserveField1  = reserveField1;
			this._reserveField2  = reserveField2;
			this._reserveField3  = reserveField3;
			this._reserveField4  = reserveField4;
			this._initiateId  = initiateId;
			this._initiateDate  = initiateDate;
			this._modiId  = modiId;
			this._modiDate  = modiDate;
		}
		
		public  System.String ApplyCode{
			get{ return _applyCode; }
			set{ this._applyCode = value; }
		}
		
		public  System.DateTime? ApplyDate{
			get{ return _applyDate; }
			set{ this._applyDate = value; }
		}
		
		public  System.String ApplyId{
			get{ return _applyId; }
			set{ this._applyId = value; }
		}
		
		public  System.String ApplyDepartment{
			get{ return _applyDepartment; }
			set{ this._applyDepartment = value; }
		}
		
		public  System.String Tel{
			get{ return _tel; }
			set{ this._tel = value; }
		}
		
		public  System.Boolean? IsBUMgrConfirm{
			get{ return _isBUMgrConfirm; }
			set{ this._isBUMgrConfirm = value; }
		}
		
		public  System.String DivisionMgrId{
			get{ return _divisionMgrId; }
			set{ this._divisionMgrId = value; }
		}
		
		public  System.DateTime? DivisionConfirmDate{
			get{ return _divisionConfirmDate; }
			set{ this._divisionConfirmDate = value; }
		}
		
		public  System.String BUMgrId{
			get{ return _bUMgrId; }
			set{ this._bUMgrId = value; }
		}
		
		public  System.DateTime? BUConfirmDate{
			get{ return _bUConfirmDate; }
			set{ this._bUConfirmDate = value; }
		}
		
		public  System.String Memo{
			get{ return _memo; }
			set{ this._memo = value; }
		}
		
		public  System.Int32? Status{
			get{ return _status; }
			set{ this._status = value; }
		}
		
		public  System.String RejectReason{
			get{ return _rejectReason; }
			set{ this._rejectReason = value; }
		}
		
		public  System.String DivisionId{
			get{ return _divisionId; }
			set{ this._divisionId = value; }
		}
		
		public  System.String BUId{
			get{ return _bUId; }
			set{ this._bUId = value; }
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