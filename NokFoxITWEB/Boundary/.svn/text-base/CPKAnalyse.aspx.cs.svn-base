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
	/// CPKAnalyse 的摘要说明。
	/// </summary>
	public partial class CPKAnalyse : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{

			// 在此处放置用户代码以初始化页面
//			string strproject=Request.QueryString["1"];		//页面传
//			Image_Rect.Visible=true;
//			string Filename = "Draw//R_"+System.DateTime .Now .ToString ("yyyyMMddHHmmss")+".Jpeg";
//			string img = Filename.Trim ();
//			Filename = Server.MapPath (Filename);
//			int max=iNUM;
//			int total_max=0;
//			for(int jj=0;jj<max;jj++)
//			{
//				if(int.Parse(strCPKNO[jj,0])>=total_max)
//					total_max=int.Parse(strCPKNO[jj,0]);
//			}
//			try
//			{
//				Draw_Rectangle(strCPKNO,max,Filename,total_max,1);
//			}
//			catch
//			{
//
//			}
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


//		public string Draw_Rectangle(string[,] strData,int count,string ImgFileName,int Max,int iArrayIndex)//画矩形
//		{
//			const int RecordWidth = 15 ; //每条记录的宽度[即矩阵的宽度]
//			const int PolyWidth = 0;    //矩阵间的间隔
//			int height ,width ;//图片的大小
//			//int  Rowcount= 20;  //每列的打印说明块数量
//			//const int  Wwidth  = 180;  //即第排的宽度[间隔]
//
//			height = 380;  
//			width = 60 + ( count + 2 ) * ( RecordWidth + PolyWidth ) + PolyWidth ;/*+ (count/Rowcount+1)*Wwidth ;*/  //坐标初始矩离 + 矩形宽度 + 矩形初始间隔 + 说明块的宽度
//
//			System.Drawing.Bitmap b = new System.Drawing.Bitmap(width,height,System.Drawing.Imaging.PixelFormat.Format16bppRgb555);
//			System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(b);
//
//			System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.Black,1);
//            
//			System.Drawing.Font font = new System.Drawing.Font(new System.Drawing.FontFamily("宋体"),10);
//			System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
//		
//			int i = 0;
//			int y = 0;
//			int n;
//
//			int P_x =60 + (count+1) * ( RecordWidth + PolyWidth ) + PolyWidth ; //坐标的宽度(X)
//			
//			g.Clear(System.Drawing.Color.FromArgb(204,204,204));//清屏
//
//			g.DrawLine(pen,P_x,315,P_x+5,320);//
//			g.DrawLine(pen,60,320,P_x+5,320); //  --------------------------------------------------------------------------->
//			g.DrawLine(pen,P_x,325,P_x+5,320);//
//			g.DrawString ("[Test Volume]",font,brush,P_x-40,330);//
//
//			g.DrawLine(pen,55,10,60,5);		//					 
//			g.DrawLine(pen,60,5,60,320);	//                   |
//			g.DrawLine(pen,60,5,65,10);		//                   |
//			g.DrawString("[Quantity]",font,brush,16,2);//
//
//			pen = new System.Drawing.Pen(System.Drawing.Color.IndianRed,1);
//			g.DrawLine(pen,(60+PolyWidth+iMy*(PolyWidth+RecordWidth)-1),5,(60+PolyWidth+iMy*(PolyWidth+RecordWidth)-1),320);
//			g.DrawLine(pen,(60+PolyWidth+(3*iMy+1)*(PolyWidth+RecordWidth)-1),5,(60+PolyWidth+(3*iMy+1)*(PolyWidth+RecordWidth)-1),320);
//			pen = new System.Drawing.Pen(System.Drawing.Color.Red,1);
//			g.DrawLine(pen,((60+PolyWidth+(3*iMy+1)*(PolyWidth+RecordWidth)-1)+(60+PolyWidth+iMy*(PolyWidth+RecordWidth)-1))/2,5,((60+PolyWidth+(3*iMy+1)*(PolyWidth+RecordWidth)-1)+(60+PolyWidth+iMy*(PolyWidth+RecordWidth)-1))/2,320);
//			pen = new System.Drawing.Pen(System.Drawing.Color.Black,1);
//			g.DrawString("LOW="+iLow.ToString(),font,brush,(60+PolyWidth+iMy*(PolyWidth+RecordWidth)-1),330);
//			g.DrawString("HIGH="+iHigh.ToString(),font,brush,(60+PolyWidth+(3*iMy+1)*(PolyWidth+RecordWidth)-1),330);
//			g.DrawString("MID",font,brush,((60+PolyWidth+(3*iMy+1)*(PolyWidth+RecordWidth)-1)+(60+PolyWidth+iMy*(PolyWidth+RecordWidth)-1))/2,330);
//			
//			int avg=0;
//			//			avg=300/(maxheight+10);
//			avg=Max/15;
//
//
//			//avg 为除数
//			if (avg == 0)
//			{
//				avg = 1;
//			}
//			
//			//打印Y轴坐标刻度尺
//			for(i = 1;i<21;i++)
//			{
//				y = 320 - 15 * i;
//				if((i % 10)==0)
//				{
//					g.DrawLine(pen,56,y,60,y);
//					g.DrawString((i*avg).ToString(),font,brush,50- 7 - i.ToString().Length * 6,y - 6);
//				}
//				else
//				{
//					g.DrawLine(pen,58,y,60,y);
//				}
//			}	
//		
//			//标识原点坐标(0,0)
//			g.DrawString ("[0,0]",font,brush,35,325);
//
//			int k = 0;
//			int heigh=0;
//			heigh=500/count;
//			n=heigh/3;
//
//			if (n>20)
//				n=20;
//			n = RecordWidth ;  //每个矩阵的宽度
//
//			System.Drawing.Font font1 = new System.Drawing.Font(new System.Drawing.FontFamily("宋体"),8);
//			System.Drawing .SolidBrush lbrush =null;
//			System.Drawing .SolidBrush bb=new SolidBrush(System.Drawing .Color .Black );
//
//			for (i=0;i<count;i++)
//			{
//				k=Int32.Parse (strData[i,(iArrayIndex-1)].ToString ())*15/avg;   //对应的Y轴坐标高度[长度]
//
//				lbrush =new System.Drawing.SolidBrush (System.Drawing .Color.FromName (strData[i,iArrayIndex]));  //颜色
//
//				g.FillRectangle(lbrush,60+PolyWidth+i*(PolyWidth+RecordWidth),320-k,n,k);              //矩阵
//				g.DrawString (i.ToString(),font,brush,60+PolyWidth+i*(PolyWidth+RecordWidth)-1,320); 					
//				g.DrawString (strData[i,(iArrayIndex-1)],font,brush,60+PolyWidth+i*(PolyWidth+RecordWidth)-1,320-k-15);  //打印数值
//											
//			}
//
//			try
//			{
//				b.Save(ImgFileName,System.Drawing.Imaging.ImageFormat.Jpeg);
//				b.Dispose();
//		
//				GC.Collect ();
//				return width.ToString ();
//			}
//			catch
//			{
//				mess.ForeColor=System.Drawing.Color.Red;
//				mess.Font.Bold=true;
//				string strproject=project.SelectedValue;
//				string strstations=ddlprojectName.SelectedValue;
//				string strpage=Request.CurrentExecutionFilePath;		
//				System.Data.OleDb.OleDbConnection conn =new System.Data.OleDb.OleDbConnection(pub.oracleconn());	
//				try
//				{			
//					conn.Open();
//					string	insql="insert into errinfo (project,line,stations,process,errtype,errtime,resumetime,disposal,errdetail) values('"+strproject+"','','"+strstations+"','Webs','WEB-DRAW-001',to_date('"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+"','yyyy-mm-dd hh24:mi:ss'),'','','Error Path: "+Request.UserHostAddress+strpage+"_"+resManager.GetString("public_rectangle_error",ci)+"')";				
//					System.Data.OleDb.OleDbCommand inset=new System.Data.OleDb.OleDbCommand(insql,conn);
//					inset.ExecuteNonQuery();
//				}		
//				catch
//				{}
//				finally
//				{				
//					conn.Close();
//				}
//				return	mess.Text =resManager.GetString("public_rectangle_error",ci);	
//			}
//		}
//
//		public string Draw_Rectangle1(float fSigma,string[,] strData,int count,string ImgFileName,int Max,int iArrayIndex,float imax,float imin,float iTotalAve)//画矩形
//		{
//			const int RecordWidth = 15 ; //每条记录的宽度[即矩阵的宽度]
//			const int PolyWidth = 0;    //矩阵间的间隔
//			int height ,width ;//图片的大小
//			//int  Rowcount= 20;  //每列的打印说明块数量
//			//const int  Wwidth  = 180;  //即第排的宽度[间隔]
//
//			height = 380;  
//			width = 60 + ( count + 2 ) * ( RecordWidth + PolyWidth ) + PolyWidth ;/*+ (count/Rowcount+1)*Wwidth ;*/  //坐标初始矩离 + 矩形宽度 + 矩形初始间隔 + 说明块的宽度
//
//			System.Drawing.Bitmap b = new System.Drawing.Bitmap(width,height,System.Drawing.Imaging.PixelFormat.Format16bppRgb555);
//			System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(b);
//
//			System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.Black,1);
//            
//			System.Drawing.Font font = new System.Drawing.Font(new System.Drawing.FontFamily("Arial"),10);
//			System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
//
//			int i = 0;
//			//	int y = 0;
//			int n;
//
//			int P_x =60 + (count+1) * ( RecordWidth + PolyWidth ) + PolyWidth ; //坐标的宽度(X)
//			
//			g.Clear(System.Drawing.Color.FromArgb(204,204,204));
//
//			//g.DrawLine(pen,P_x,315,P_x+5,320);//
//			g.DrawLine(pen,60,350,P_x+5,350); //  ---------------------------------------------------------------------------
//						
//			//g.DrawLine(pen,P_x,325,P_x+5,320);//
//			//g.DrawString ("[测试值]",font,brush,P_x-40,330);//
//
//			//g.DrawLine(pen,55,10,60,5);		//					 
//			//g.DrawLine(pen,60,5,60,350);	//                   |
//			//g.DrawLine(pen,60,5,65,10);		//                   |
//			//g.DrawString("[数量]",font,brush,16,2);//
//			
//			//pen = new System.Drawing.Pen(System.Drawing.Color.Red,1);
//			//g.DrawLine(pen,((60+PolyWidth+(3*iMy+1)*(PolyWidth+RecordWidth)-1)+(60+PolyWidth+iMy*(PolyWidth+RecordWidth)-1))/2,5,((60+PolyWidth+(3*iMy+1)*(PolyWidth+RecordWidth)-1)+(60+PolyWidth+iMy*(PolyWidth+RecordWidth)-1))/2,320);
//			//坐标的(X)比例
//			mynfi.NumberDecimalDigits = 2;
//			float iavg=(imax-imin)/8;       
//			float iaga=(P_x-55)/16;
//			g.DrawString ((imin-2*iavg).ToString(),font,brush,60+iaga,360); 
//			g.DrawLine(pen,60+iaga,350,60+iaga,355);
//			//g.DrawString ((imin-iavg).ToString(),font,brush,60+2*iaga,360); 
//			//g.DrawLine(pen,60+2*iaga,350,60+2*iaga,355);
//			g.DrawString (imin.ToString(),font,brush,60+3*iaga,360); 
//			g.DrawLine(pen,60+3*iaga,350,60+3*iaga,355);
//			//g.DrawString ((imin+iavg).ToString(),font,brush,60+4*iaga,360); 
//			//g.DrawLine(pen,60+4*iaga,350,60+4*iaga,355);
//			g.DrawString ((imin+iavg*2).ToString(),font,brush,60+5*iaga,360); 
//			g.DrawLine(pen,60+5*iaga,350,60+5*iaga,355);
//			//g.DrawString ((imin+iavg*3).ToString(),font,brush,60+6*iaga,360); 
//			//g.DrawLine(pen,60+6*iaga,350,60+6*iaga,355);
//			g.DrawString ((imin+iavg*4).ToString(),font,brush,60+7*iaga,360); 
//			g.DrawLine(pen,60+7*iaga,350,60+7*iaga,355);
//			//g.DrawString ((imin+iavg*5).ToString(),font,brush,60+8*iaga,360); 
//			//g.DrawLine(pen,60+8*iaga,350,60+8*iaga,355);
//			g.DrawString ((imin+iavg*6).ToString(),font,brush,60+9*iaga,360); 
//			g.DrawLine(pen,60+9*iaga,350,60+9*iaga,355);
//			//g.DrawString ((imin+iavg*7).ToString(),font,brush,60+10*iaga,360); 
//			//g.DrawLine(pen,60+10*iaga,350,60+10*iaga,355);
//			g.DrawString ((imin+iavg*8).ToString(),font,brush,60+11*iaga,360); 			
//			g.DrawLine(pen,60+11*iaga,350,60+11*iaga,355);
//			//g.DrawString ((imin+iavg*9).ToString(),font,brush,60+12*iaga,360); 			
//			//g.DrawLine(pen,60+12*iaga,350,60+12*iaga,355);
//			g.DrawString ((imin+iavg*10).ToString(),font,brush,60+13*iaga,360); 			
//			g.DrawLine(pen,60+13*iaga,350,60+13*iaga,355);
//
//			int avg=0;
//			//			avg=300/(maxheight+10);
//			avg=Max/15;
//
//
//			//avg 为除数
//			if (avg == 0)
//			{
//				avg = 1;
//			}
//			
//			//打印Y轴坐标刻度尺
//			/*	for(i = 1;i<21;i++)
//				{
//					y = 320 - 15 * i;
//					if((i % 10)==0)
//					{
//						g.DrawLine(pen,56,y,60,y);
//						g.DrawString((i*avg).ToString(),font,brush,50- 7 - i.ToString().Length * 6,y - 6);
//					}
//					else
//					{
//						g.DrawLine(pen,58,y,60,y);
//					}
//				}	*/
//		
//			//标识原点坐标(0,0)
//			//g.DrawString ("[0,0]",font,brush,35,325);
//
//			//		int k = 0;
//			int heigh=0;
//			heigh=500/count;
//			n=heigh/3;
//
//			if (n>20)
//				n=20;
//			n = RecordWidth ;  //每个矩阵的宽度
//
//			System.Drawing.Font font1 = new System.Drawing.Font(new System.Drawing.FontFamily("宋体"),8);
//			//		System.Drawing .SolidBrush lbrush =null;
//			System.Drawing .SolidBrush bb=new SolidBrush(System.Drawing .Color .Black );
//
//			int[] m=new int[count];
//			int[] nn=new int[count];
//			int max=0;
//			int mmax=0;
//			int mmin=0;
//
//			for (i=0;i<count;i++)
//			{
//				//k=Int32.Parse (strData[i,(iArrayIndex-1)].ToString ())*15/avg;   //对应的Y轴坐标高度[长度]
//				
//				//lbrush =new System.Drawing.SolidBrush (System.Drawing .Color.FromName (strData[i,iArrayIndex]));  //颜色
//
//				//g.FillRectangle(lbrush,60+PolyWidth+i*(PolyWidth+RecordWidth),320-k,n,k);              //矩阵
//
//				//g.DrawString (strData[i,(iArrayIndex-1)],font,brush,60+PolyWidth+i*(PolyWidth+RecordWidth)-1,240-k-15);  //打印数值
//
//				m[i]=60+PolyWidth+i*(PolyWidth+RecordWidth);
//				mmin=m[0];
//				mmax=m[count-1];
//				max=max+m[i];
//				//nn[i]=240-k;				
//				//g.DrawString (strData[i,0],font,brush,60+PolyWidth+i*(PolyWidth+RecordWidth)-1,320); 
//				//g.DrawString (i.ToString(),font,brush,60+PolyWidth+i*(PolyWidth+RecordWidth)-1,360); 
//			}
//
//			/*Point[] pi = {
//							new Point(10, 100),   // start point of first spline
//							new Point(60, 50),    // first control point of first spline
//							new Point(60, 50),    // second control point of first spline
//							
//							new Point(110, 100)};  // end point of first spline and 
//			g.DrawCurve(pen, pi);
//							// start point of second spline			*/		
//			double x=0;	
//			double xx=0;
//			int mean=0;  //Mean的X值
//			float aga=0;
//			for(int ii=0;ii<count;ii++)  
//			{	
//				//p[ii]=new Point(m[ii],(int)(300-(((1/(fSigma*Math.Sqrt(2*Math.PI)))*(Math.Exp(-(((m[ii]-max/count)*(m[ii]-max/count))/(2*fSigma*fSigma))))))));
//                			
//				//pen = new System.Drawing.Pen(System.Drawing.Color.ForestGreen,1);
//				// x=((1/(fSigma*Math.Sqrt(2*Math.PI)))*(Math.Exp(-(((m[ii]-max/count)*(m[ii]-max/count))/(2*fSigma*fSigma)))));
//				x=(double)(300-(((1/(fSigma*Math.Sqrt(2*Math.PI)))*(Math.Exp(-(((m[ii]-max/count)*(m[ii]-max/count))/(2*fSigma*fSigma)))))));
//				if(ii==0)
//				{xx=x;}
//				if(x<xx)
//				{
//					xx=x;
//					mean=ii;
//				}
//				if(ii==(count-1))
//				{
//					//g.DrawString (xx.ToString(),font,brush,m[mean],(float)(300-(((1/(fSigma*Math.Sqrt(2*Math.PI)))*(Math.Exp(-(((m[mean]-max/count)*(m[mean]-max/count))/(2*fSigma*fSigma)))))*1500)));
//					//	g.DrawString (xx.ToString(),font,brush,m[mean],100);
//					aga=200/((float)(((1/(fSigma*Math.Sqrt(2*Math.PI)))*(Math.Exp(-(((m[mean]-max/count)*(m[mean]-max/count))/(2*fSigma*fSigma)))))));//对应的Y轴坐标比例值
//				}
//								
//			}; 
//			//函数描点
//			int pi=0;
//			Point[] p=new Point [count]; 
//			Point[] pp=new Point [count]; 
//			for(int ii=0;ii<count;ii++)
//			{
//				if(fSigma<20)
//				{
//					p[ii]=new Point((int)(m[ii]*((P_x-55)/(6*iaga)))-(int)(m[mean]*((P_x-55)/(6*iaga))-(60+11*iaga-(imax-iHigh)-(iHigh-iTotalAve)*iaga/iavg)),(int)(300-(((1/(fSigma*Math.Sqrt(2*Math.PI)))*(Math.Exp(-(((m[ii]-max/count)*(m[ii]-max/count))/(2*fSigma*fSigma)))))*aga)));			
//					//p[ii]=new Point((int)(m[ii]),(int)(300-(((1/(fSigma*Math.Sqrt(2*Math.PI)))*(Math.Exp(-(((m[ii]-max/count)*(m[ii]-max/count))/(2*fSigma*fSigma)))))*aga)));				
//					pp[pi]=p[ii];
//					pi++;	
//				}				
//				else if(m[ii]>=(m[mean]-4*fSigma)&&m[ii]<=(m[mean]+4*fSigma))
//				{	
//					p[ii]=new Point((int)(m[ii]*(iaga/fSigma))-(int)(m[mean]*(iaga/fSigma)-(60+11*iaga-(imax-iHigh)-(iHigh-iTotalAve)*iaga/iavg)),(int)(300-(((1/(fSigma*Math.Sqrt(2*Math.PI)))*(Math.Exp(-(((m[ii]-max/count)*(m[ii]-max/count))/(2*fSigma*fSigma)))))*aga)));	
//					//p[ii]=new Point((int)(m[ii]),(int)(300-(((1/(fSigma*Math.Sqrt(2*Math.PI)))*(Math.Exp(-(((m[ii]-max/count)*(m[ii]-max/count))/(2*fSigma*fSigma)))))*aga)));	
//					pp[pi]=p[ii];
//					pi++;					
//				}			   
//			}
//			Point[] ppp=new Point [pi]; 
//			for(int ppi=0;ppi<pi;ppi++)
//			{
//				ppp[ppi]=pp[ppi];
//			}
//			pen = new System.Drawing.Pen(System.Drawing.Color.ForestGreen,1);
//			//Pen pen = new Pen(Color.Blue);
//			//	g.DrawBeziers(pen, p);
//			if(fSigma!=0)
//			{
//				g.DrawCurve(pen, ppp);
//			
//				//g.SmoothingMode =g.SmoothingMode;
//				//g.DrawLines(pen, ppp);
//
//			
//				//坐标附加值
//				pen = new System.Drawing.Pen(System.Drawing.Color.Red,1);
//				g.DrawLine(pen,(60+11*iaga-(imax-iHigh)-(iHigh-iLow)*iaga/iavg),300,(60+11*iaga-(imax-iHigh)-(iHigh-iLow)*iaga/iavg),325);
//				g.DrawLine(pen,(60+11*iaga-(imax-iHigh)),300,(60+11*iaga-(imax-iHigh)),325);
//				g.DrawLine(pen,(60+11*iaga-(imax-iHigh)-(iHigh-iLow)*iaga/iavg),325,(60+11*iaga-(imax-iHigh)),325);
//				pen = new System.Drawing.Pen(System.Drawing.Color.Black,1);
//				g.DrawLine(pen,60,300,P_x+5,300);//-------------------
//				g.DrawString("LOW="+iLow.ToString(),font,brush,(60+11*iaga-(imax-iHigh)-(iHigh-iLow)*iaga/iavg)-15,330);
//				g.DrawString("HIGH="+iHigh.ToString(),font,brush,(60+11*iaga-(imax-iHigh))-15,330);
//				g.DrawString("Mean",font,brush,(60+11*iaga-(imax-iHigh)-(iHigh-iTotalAve)*iaga/iavg)-15,270);
//				g.DrawLine(pen,(60+11*iaga-(imax-iHigh)-(iHigh-iTotalAve)*iaga/iavg),300,(60+11*iaga-(imax-iHigh)-(iHigh-iTotalAve)*iaga/iavg),290);
//				g.DrawString("-3s",font,brush,(60+11*iaga-(imax-iHigh)-(iHigh-iTotalAve)*iaga/iavg)-(3*fSigma)*(iaga/fSigma)-12,270);
//				g.DrawLine(pen,(60+11*iaga-(imax-iHigh)-(iHigh-iTotalAve)*iaga/iavg)-(3*fSigma)*(iaga/fSigma),300,(60+11*iaga-(imax-iHigh)-(iHigh-iTotalAve)*iaga/iavg)-(3*fSigma)*(iaga/fSigma),290);	
//				g.DrawString("+3s",font,brush,(60+11*iaga-(imax-iHigh)-(iHigh-iTotalAve)*iaga/iavg)+(3*fSigma)*(iaga/fSigma)-12,270);
//				g.DrawLine(pen,(60+11*iaga-(imax-iHigh)-(iHigh-iTotalAve)*iaga/iavg)+(3*fSigma)*(iaga/fSigma),300,(60+11*iaga-(imax-iHigh)-(iHigh-iTotalAve)*iaga/iavg)+(3*fSigma)*(iaga/fSigma),290);	
//			}
//			//avg=300/Rowcount;
//			
//			/*
//			//说明
//			P_x =60 + (count) * ( RecordWidth + PolyWidth ) + PolyWidth;
//			n=1;
//			for (i=1;i<count;i++)
//			{
//				if(Convert.ToInt32 (strData[i,4])>0)  
//				{
//					if(n%Rowcount==0)
//						n++;
//
//					k=n/Rowcount;
//
//					heigh=avg*(n%Rowcount);
//
//					lbrush =new System.Drawing.SolidBrush (System.Drawing .Color.Blue/*FromName (strData[i,6])*//*);
//
//					g.FillRectangle(lbrush,P_x+(k*160),heigh,RecordWidth,12);
//				
//					g.DrawString(strData[i,1],font,bb,P_x+30+(k*Wwidth),heigh);		
//
//					n++;
//				
//				}
//			}
//			*/
//
//			try
//			{
//				b.Save(ImgFileName,System.Drawing.Imaging.ImageFormat.Jpeg);
//				b.Dispose();
//		
//				GC.Collect ();
//				return width.ToString ();
//			}
//			catch
//			{
//				mess.ForeColor=System.Drawing.Color.Red;
//				mess.Font.Bold=true;
//				string strproject=project.SelectedValue;
//				string strstations=ddlprojectName.SelectedValue;
//				string strpage=Request.CurrentExecutionFilePath;
//		
//				System.Data.OleDb.OleDbConnection conn =new System.Data.OleDb.OleDbConnection(pub.oracleconn());	
//				try
//				{			
//					conn.Open();
//					string	insql="insert into errinfo (project,line,stations,process,errtype,errtime,resumetime,disposal,errdetail) values('"+strproject+"','','"+strstations+"','Webs','WEB-DRAW-001',to_date('"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+"','yyyy-mm-dd hh24:mi:ss'),'','','Error Path: "+Request.UserHostAddress+strpage+"_"+resManager.GetString("public_rectangle_error",ci)+"')";				
//					System.Data.OleDb.OleDbCommand inset=new System.Data.OleDb.OleDbCommand(insql,conn);
//					inset.ExecuteNonQuery();
//				}		
//				catch
//				{}
//				finally
//				{				
//					conn.Close();
//				}
//				return	mess.Text =resManager.GetString("public_rectangle_error",ci);	
//			}
//		}
//
	

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
