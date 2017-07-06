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
	public class ApplyPurchaseHead : ApplyPurchaseHeadBase{

        public int DeleteHeadAndBody(ApplyPurchaseHeadInfo myEntityInfoHead)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("tbpurchase_ApplyPurchaseHead_deleteAll_sp");
            db.AddInParameter(command, "ApplyPurchaseCode", DbType.String, myEntityInfoHead.ApplyPurchaseCode);
            return db.ExecuteNonQuery(command);


        }
		
	}
}