using System;

namespace FIH.ForeignStaff.db{
	///
	/// Summary description for PubAttachmentsInfo
	///
	public class PubAttachmentsInfo{
		private System.Int32 _attachID;
		private System.String _baseType;
		private System.Int32? _baseID;
		private System.String _baseCode;
		private System.String _originalFileName;
		private System.String _uniqueFileName;
		private System.String _fileType;
		private System.String _createUser;
		private System.DateTime? _createDate;
		
		public PubAttachmentsInfo(){
		}
		
		public PubAttachmentsInfo(
				System.Int32  attachID, 
				System.String  baseType, 
				System.Int32?  baseID, 
				System.String  baseCode, 
				System.String  originalFileName, 
				System.String  uniqueFileName, 
				System.String  fileType, 
				System.String  createUser, 
				System.DateTime?  createDate 
			){
			this._attachID  = attachID;
			this._baseType  = baseType;
			this._baseID  = baseID;
			this._baseCode  = baseCode;
			this._originalFileName  = originalFileName;
			this._uniqueFileName  = uniqueFileName;
			this._fileType  = fileType;
			this._createUser  = createUser;
			this._createDate  = createDate;
		}
		
		public  System.Int32 AttachID{
			get{ return _attachID; }
			set{ this._attachID = value; }
		}
		
		public  System.String BaseType{
			get{ return _baseType; }
			set{ this._baseType = value; }
		}
		
		public  System.Int32? BaseID{
			get{ return _baseID; }
			set{ this._baseID = value; }
		}
		
		public  System.String BaseCode{
			get{ return _baseCode; }
			set{ this._baseCode = value; }
		}
		
		public  System.String OriginalFileName{
			get{ return _originalFileName; }
			set{ this._originalFileName = value; }
		}
		
		public  System.String UniqueFileName{
			get{ return _uniqueFileName; }
			set{ this._uniqueFileName = value; }
		}
		
		public  System.String FileType{
			get{ return _fileType; }
			set{ this._fileType = value; }
		}
		
		public  System.String CreateUser{
			get{ return _createUser; }
			set{ this._createUser = value; }
		}
		
		public  System.DateTime? CreateDate{
			get{ return _createDate; }
			set{ this._createDate = value; }
		}
		
		
	}
}