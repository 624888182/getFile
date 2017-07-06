using System;
using System.Data;
using System.Data.SqlClient;

namespace SQLHelperA
{
	/// <summary>
	/// 自定义Exception
	/// </summary>
	public class MisSystemException:Exception
	{
		/// <summary>
		/// 包含系统Excepton
		/// </summary>
		public MisSystemException(string source,string message,Exception inner):base(message,inner)
		{
			base.Source=source;
		}

		/// <summary>
		/// 不包含系统Excepton
		/// </summary>			
		public MisSystemException(string source,string message):base(message)
		{
			base.Source=source;
		}
	}

	/// <summary>
	/// 处理网页中的HTML代码，并消除危险字符
	/// </summary>
	public class SystemHTML
	{
		private static string HTMLEncode(string fString)
		{
			if(fString!=string.Empty)
			{
				///替换尖括号
				fString.Replace("<","&lt;");
				fString.Replace(">","&rt;");
				///替换引号
				fString.Replace(((char)34).ToString(), "&quot;");
				fString.Replace(((char)39).ToString(), "&#39;");
				///替换空格
				fString.Replace(((char)13).ToString(), "");
				///替换换行符
				fString.Replace(((char)10).ToString(), "<BR> ");
			}
			return(fString);
		}
	}

	/// <summary>
	/// SystemTools 的摘要说明。
	/// </summary>
	public class SystemTools
	{		
		/// <summary>
		/// 将DataReader 转为 DataTable
		/// </summary>
		/// <param name="DataReader">DataReader</param>
        //public static DataTable ConvertDataReaderToDataTable(SqlDataReader dataReader)
        //{
        //    ///定义DataTable和模式
        //    DataTable datatable = new DataTable();
        //    DataTable schemaTable = dataReader.GetSchemaTable();			
			
        //    try
        //    {	///动态添加表的数据列
        //        foreach(DataRow myRow in schemaTable.Rows)
        //        {
        //            DataColumn myDataColumn = new DataColumn();
        //            myDataColumn.DataType	= myRow.GetType();
        //            myDataColumn.ColumnName = myRow[0].ToString();
        //            datatable.Columns.Add(myDataColumn);
        //        }
				
        //        ///添加表的数据
        //        while(dataReader.Read())
        //        {
        //            DataRow myDataRow = datatable.NewRow();
        //            for(int i=0;i<schemaTable.Rows.Count;i++)
        //            {
        //                myDataRow[i] = dataReader[i];
        //            }
        //            datatable.Rows.Add(myDataRow);
        //            myDataRow = null;
        //        }
        //        schemaTable = null;
				
        //        ///关闭数据读取器
        //        dataReader.Close();
        //        return datatable;
        //    }
        //    catch(Exception ex)
        //    {
        //        ///抛出类型转换错误
        //        SystemError.SystemLog(ex.Message);
        //        throw new Exception(ex.Message,ex);
        //    }			
        //}
        public static DataTable ConvertDataReaderToDataTable( IDataReader reader ) {
            DataTable objDataTable = new DataTable();
            int intFieldCount = reader.FieldCount;
            for( int intCounter = 0; intCounter < intFieldCount; ++intCounter ) {
                objDataTable.Columns.Add( reader.GetName( intCounter ), reader.GetFieldType( intCounter ) );
            }

            objDataTable.BeginLoadData();

            object[] objValues = new object[intFieldCount];
            while( reader.Read() ) {
                reader.GetValues( objValues );
                objDataTable.LoadDataRow( objValues, true );
            }
            reader.Close();
            objDataTable.EndLoadData();

            return objDataTable;
        }
	}
}
