using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace SFCQuery.Boundary
{
	/// <summary>
	/// DataQuery 的摘要说明。
	/// </summary>
	public partial class DataQuery : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DataGrid dgdata1;
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			draw_Total();
		}
		private void draw_Total()
		{          
			int height,width;

			height = 400;
			width =920;

			System.Drawing.Bitmap b = new System.Drawing.Bitmap(width,height,System.Drawing.Imaging.PixelFormat.Format16bppRgb555);	
			
			System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(b);

			System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.Black  ,1);	
			System.Drawing.Font font = new System.Drawing.Font(new System.Drawing.FontFamily("Arial"),10);
			System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
		

			g.Clear(System.Drawing.Color.FromArgb(210,210,231));  //清屏
				
			//原点坐标[0,0]
			g.DrawString ( "0",font,brush,65,355);
			
			g.DrawString ( "Line",font,brush,50,365);
			
			g.DrawLine (pen,50,10,900,10);
			g.DrawLine (pen,50,40,900,40);
			g.DrawLine (pen,50,10,50,40);
			g.DrawLine (pen,900,10,900,40);

			g.DrawString ("PASS",font,brush,60,20);
			
			try
			{
				b.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
				//	b.Save(ImgFileName,System.Drawing.Imaging.ImageFormat.Jpeg);
			}
			catch
			{
				return;
			}
			finally
			{
				b.Dispose ();
			}

			GC.Collect ();

		}
		#region Web 窗体设计器生成的代码
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{    
		}
		#endregion
	}
}
