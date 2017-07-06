using System;

namespace FIH.Security.db{
	///
	/// Summary description for UsersInfo
	///
	public class UsersInfo{
		private System.String _userID;
		private System.String _userName;
		private System.String _deptNo;
		private System.String _operRoleGpCode;
		private System.String _mail;
		private System.String _tel;
		private System.String _passWD;
		private System.Boolean? _isOnLine;
		private System.String _gradeCode;
		private System.String _gradeName;
		private System.String _positionCode;
		private System.String _positionName;
		private System.String _positionSeries;
		private System.String _positionSeriesName;
		private System.DateTime? _inCompanyDate;
		private System.Int32? _status;
		private System.String _remark;
		
		public UsersInfo(){
		}
		
		public UsersInfo(
				System.String  userID, 
				System.String  userName, 
				System.String  deptNo, 
				System.String  operRoleGpCode, 
				System.String  mail, 
				System.String  tel, 
				System.String  passWD, 
				System.Boolean?  isOnLine, 
				System.String  gradeCode, 
				System.String  gradeName, 
				System.String  positionCode, 
				System.String  positionName, 
				System.String  positionSeries, 
				System.String  positionSeriesName, 
				System.DateTime?  inCompanyDate, 
				System.Int32?  status, 
				System.String  remark 
			){
			this._userID  = userID;
			this._userName  = userName;
			this._deptNo  = deptNo;
			this._operRoleGpCode  = operRoleGpCode;
			this._mail  = mail;
			this._tel  = tel;
			this._passWD  = passWD;
			this._isOnLine  = isOnLine;
			this._gradeCode  = gradeCode;
			this._gradeName  = gradeName;
			this._positionCode  = positionCode;
			this._positionName  = positionName;
			this._positionSeries  = positionSeries;
			this._positionSeriesName  = positionSeriesName;
			this._inCompanyDate  = inCompanyDate;
			this._status  = status;
			this._remark  = remark;
		}
		
		public  System.String UserID{
			get{ return _userID; }
			set{ this._userID = value; }
		}
		
		public  System.String UserName{
			get{ return _userName; }
			set{ this._userName = value; }
		}
		
		public  System.String DeptNo{
			get{ return _deptNo; }
			set{ this._deptNo = value; }
		}
		
		public  System.String OperRoleGpCode{
			get{ return _operRoleGpCode; }
			set{ this._operRoleGpCode = value; }
		}
		
		public  System.String Mail{
			get{ return _mail; }
			set{ this._mail = value; }
		}
		
		public  System.String Tel{
			get{ return _tel; }
			set{ this._tel = value; }
		}
		
		public  System.String PassWD{
			get{ return _passWD; }
			set{ this._passWD = value; }
		}
		
		public  System.Boolean? IsOnLine{
			get{ return _isOnLine; }
			set{ this._isOnLine = value; }
		}
		
		public  System.String GradeCode{
			get{ return _gradeCode; }
			set{ this._gradeCode = value; }
		}
		
		public  System.String GradeName{
			get{ return _gradeName; }
			set{ this._gradeName = value; }
		}
		
		public  System.String PositionCode{
			get{ return _positionCode; }
			set{ this._positionCode = value; }
		}
		
		public  System.String PositionName{
			get{ return _positionName; }
			set{ this._positionName = value; }
		}
		
		public  System.String PositionSeries{
			get{ return _positionSeries; }
			set{ this._positionSeries = value; }
		}
		
		public  System.String PositionSeriesName{
			get{ return _positionSeriesName; }
			set{ this._positionSeriesName = value; }
		}
		
		public  System.DateTime? InCompanyDate{
			get{ return _inCompanyDate; }
			set{ this._inCompanyDate = value; }
		}
		
		public  System.Int32? Status{
			get{ return _status; }
			set{ this._status = value; }
		}
		
		public  System.String Remark{
			get{ return _remark; }
			set{ this._remark = value; }
		}
		
		
	}
}