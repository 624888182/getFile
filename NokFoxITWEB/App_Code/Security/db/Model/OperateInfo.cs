using System;

namespace FIH.Security.db{
	///
	/// Summary description for OperateInfo
	///
	public class OperateInfo{
		private System.String _operCode;
		private System.String _operName;
		
		public OperateInfo(){
		}
		
		public OperateInfo(
				System.String  operCode, 
				System.String  operName 
			){
			this._operCode  = operCode;
			this._operName  = operName;
		}
		
		public  System.String OperCode{
			get{ return _operCode; }
			set{ this._operCode = value; }
		}
		
		public  System.String OperName{
			get{ return _operName; }
			set{ this._operName = value; }
		}
		
		
	}
}