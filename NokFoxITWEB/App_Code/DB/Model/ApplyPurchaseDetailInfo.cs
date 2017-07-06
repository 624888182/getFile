using System;

namespace FIH.PM.db{
	///
	/// Summary description for ApplyPurchaseDetailInfo
	///
	public class ApplyPurchaseDetailInfo{
		private System.String applyPurchaseCode;
		private System.Int32 itemNo;
		private System.Int32? caseItemNo;
		private System.String bomno;
		private System.String bOMName;
		private System.String bOMBrand;
		private System.String specification;
		private System.String manufacture;
		private System.String unit;
		private System.Decimal? price;
		private System.Decimal? quantity;
		private System.Decimal? sum;
		private System.DateTime? requireDate;
		private System.Decimal? budgetSum;
		private System.String purchaseCode;
		private System.Int32? purchaseItemNo;
		private System.String accountingSubject;
		private System.String purchaseType;
		private System.String memo;
		private System.String standbyField1;
		private System.String standbyField2;
		private System.Decimal? standbyField3;
		private System.Int32? standbyField4;
		private System.String createrID;
		private System.DateTime? createrDate;
		private System.String modiID;
		private System.DateTime? modiDate;
		
		public ApplyPurchaseDetailInfo(){
		}
		
		public ApplyPurchaseDetailInfo(
				System.String  applyPurchaseCode, 
				System.Int32  itemNo, 
				System.Int32?  caseItemNo, 
				System.String  bomno, 
				System.String  bOMName, 
				System.String  bOMBrand, 
				System.String  specification, 
				System.String  manufacture, 
				System.String  unit, 
				System.Decimal?  price, 
				System.Decimal?  quantity, 
				System.Decimal?  sum, 
				System.DateTime?  requireDate, 
				System.Decimal?  budgetSum, 
				System.String  purchaseCode, 
				System.Int32?  purchaseItemNo, 
				System.String  accountingSubject, 
				System.String  purchaseType, 
				System.String  memo, 
				System.String  standbyField1, 
				System.String  standbyField2, 
				System.Decimal?  standbyField3, 
				System.Int32?  standbyField4, 
				System.String  createrID, 
				System.DateTime?  createrDate, 
				System.String  modiID, 
				System.DateTime?  modiDate 
			){
			this.applyPurchaseCode  = applyPurchaseCode;
			this.itemNo  = itemNo;
			this.caseItemNo  = caseItemNo;
			this.bomno  = bomno;
			this.bOMName  = bOMName;
			this.bOMBrand  = bOMBrand;
			this.specification  = specification;
			this.manufacture  = manufacture;
			this.unit  = unit;
			this.price  = price;
			this.quantity  = quantity;
			this.sum  = sum;
			this.requireDate  = requireDate;
			this.budgetSum  = budgetSum;
			this.purchaseCode  = purchaseCode;
			this.purchaseItemNo  = purchaseItemNo;
			this.accountingSubject  = accountingSubject;
			this.purchaseType  = purchaseType;
			this.memo  = memo;
			this.standbyField1  = standbyField1;
			this.standbyField2  = standbyField2;
			this.standbyField3  = standbyField3;
			this.standbyField4  = standbyField4;
			this.createrID  = createrID;
			this.createrDate  = createrDate;
			this.modiID  = modiID;
			this.modiDate  = modiDate;
		}
		
		public  System.String ApplyPurchaseCode{
			get{ return applyPurchaseCode; }
			set{ this.applyPurchaseCode = value; }
		}
		
		public  System.Int32 ItemNo{
			get{ return itemNo; }
			set{ this.itemNo = value; }
		}
		
		public  System.Int32? CaseItemNo{
			get{ return caseItemNo; }
			set{ this.caseItemNo = value; }
		}
		
		public  System.String BOMNO{
			get{ return bomno; }
			set{ this.bomno = value; }
		}
		
		public  System.String BOMName{
			get{ return bOMName; }
			set{ this.bOMName = value; }
		}
		
		public  System.String BOMBrand{
			get{ return bOMBrand; }
			set{ this.bOMBrand = value; }
		}
		
		public  System.String Specification{
			get{ return specification; }
			set{ this.specification = value; }
		}
		
		public  System.String Manufacture{
			get{ return manufacture; }
			set{ this.manufacture = value; }
		}
		
		public  System.String Unit{
			get{ return unit; }
			set{ this.unit = value; }
		}
		
		public  System.Decimal? Price{
			get{ return price; }
			set{ this.price = value; }
		}
		
		public  System.Decimal? Quantity{
			get{ return quantity; }
			set{ this.quantity = value; }
		}
		
		public  System.Decimal? Sum{
			get{ return sum; }
			set{ this.sum = value; }
		}
		
		public  System.DateTime? RequireDate{
			get{ return requireDate; }
			set{ this.requireDate = value; }
		}
		
		public  System.Decimal? BudgetSum{
			get{ return budgetSum; }
			set{ this.budgetSum = value; }
		}
		
		public  System.String PurchaseCode{
			get{ return purchaseCode; }
			set{ this.purchaseCode = value; }
		}
		
		public  System.Int32? PurchaseItemNo{
			get{ return purchaseItemNo; }
			set{ this.purchaseItemNo = value; }
		}
		
		public  System.String AccountingSubject{
			get{ return accountingSubject; }
			set{ this.accountingSubject = value; }
		}
		
		public  System.String PurchaseType{
			get{ return purchaseType; }
			set{ this.purchaseType = value; }
		}
		
		public  System.String Memo{
			get{ return memo; }
			set{ this.memo = value; }
		}
		
		public  System.String StandbyField1{
			get{ return standbyField1; }
			set{ this.standbyField1 = value; }
		}
		
		public  System.String StandbyField2{
			get{ return standbyField2; }
			set{ this.standbyField2 = value; }
		}
		
		public  System.Decimal? StandbyField3{
			get{ return standbyField3; }
			set{ this.standbyField3 = value; }
		}
		
		public  System.Int32? StandbyField4{
			get{ return standbyField4; }
			set{ this.standbyField4 = value; }
		}
		
		public  System.String CreaterID{
			get{ return createrID; }
			set{ this.createrID = value; }
		}
		
		public  System.DateTime? CreaterDate{
			get{ return createrDate; }
			set{ this.createrDate = value; }
		}
		
		public  System.String ModiID{
			get{ return modiID; }
			set{ this.modiID = value; }
		}
		
		public  System.DateTime? ModiDate{
			get{ return modiDate; }
			set{ this.modiDate = value; }
		}
		
		
	}
}