using System;

namespace FIH.Security.db{
	///
	/// Summary description for OperRoleGpInfo
	///
	public class OperRoleGpInfo{
		private System.String _operRoleGpCode;
		private System.String _operRoleGpName;
		
		public OperRoleGpInfo(){
		}
		
		public OperRoleGpInfo(
				System.String  operRoleGpCode, 
				System.String  operRoleGpName 
			){
			this._operRoleGpCode  = operRoleGpCode;
			this._operRoleGpName  = operRoleGpName;
		}
		
		public  System.String OperRoleGpCode{
			get{ return _operRoleGpCode; }
			set{ this._operRoleGpCode = value; }
		}
		
		public  System.String OperRoleGpName{
			get{ return _operRoleGpName; }
			set{ this._operRoleGpName = value; }
		}
		
		
	}
}