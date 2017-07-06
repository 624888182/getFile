namespace SFCQuery.Boundary
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Collections;
	using System.Globalization;
	using System.Resources;
	using System.Reflection;
	using System.Threading;
	using System.Text;
	using System.IO;
	using System.Xml;
	using UsingClass;
  
	/// <summary>
	///		WFrmDataQuery 的摘要说明。
	/// </summary>
	public partial class WFrmDataQuery : System.Web.UI.UserControl
	{
		//System.Data.DataTable Table = null;
		System.Data.DataTable limittable = null;
		string preTname;
        //private CultureInfo "SFCQuery" = new CultureInfo(CultureInfo.CurrentCulture.Name);
        //private static ResourceManager rm=new ResourceManager("SFCQuery.MultiLanguage.SFCQuery",Assembly.GetExecutingAssembly());
		static string objname="";
		static bool IsDone;
		//static bool flag;
		static CDData conn;
		string require = "";
		static string sqlsum;
	
		UsingClass.ComUtility cm = new ComUtility();
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
            //this."SFCQuery"=new CultureInfo(Session["Language"].ToString());
            //Thread.CurrentThread.CurrentUICulture=this."SFCQuery";
			if (!IsPostBack)
			{
				cm.getmodel(this.Page,this.DropDownList1);
				//flag=false;
				IsDone=false;
				this.lblModel.Text=(String)GetGlobalResourceObject("SFCQuery","Model");
				this.lblProductid.Text=(String)GetGlobalResourceObject("SFCQuery","ProductID");
				this.CheckBox1.Text=(String)GetGlobalResourceObject("SFCQuery","DateFrom");
				this.lblTStation.Text=(String)GetGlobalResourceObject("SFCQuery","TestStation");
				this.lblENDTIME.Text=(String)GetGlobalResourceObject("SFCQuery","DateTo");
				this.lblcountname.Text=(String)GetGlobalResourceObject("SFCQuery","CountName");
				this.btnOUTPUT.Text=(String)GetGlobalResourceObject("SFCQuery","OutPut");
				this.btnQUERY.Text=(String)GetGlobalResourceObject("SFCQuery","Query");
				setcount(this.Page);
				getcount(this.Page,this.lbltestcount);
				this.DropDownList1.Items.Add((String)GetGlobalResourceObject("SFCQuery","ForSelect"));
				this.DropDownList1.SelectedIndex=this.DropDownList1.Items.Count-1;
                //this.hbtndatefrm.Attributes["onclick"]= "return showCalendar('"+this.txtSTARTTIME.ClientID+"', '%Y-%m-%d 08:00', '24', true);";
                //this.hbtndateto.Attributes["onclick"]="return showCalendar('"+this.txtEndDate.ClientID+"', '%Y-%m-%d %H:%M', '24', true);";
                
                txtEndDate.DateTextBox.Enabled = false;
                txtSTARTTIME.DateTextBox.Enabled = false;
                //if (this.txtSTARTTIME.DateTextBox.Text.CompareTo(this.txtEndDate.DateTextBox.Text)>0)
                //    this.txtSTARTTIME.DateTextBox.Text = this.txtEndDate.DateTextBox.Text;
			}
		}
		public void setcount(System.Web.UI.Page pg)
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(pg.Server.MapPath("DataQueryCount.xml"));
			XmlNode rootnode = doc.DocumentElement;
			int count=0;
			if(rootnode!=null)
			{
				XmlNode xmlelem=rootnode.FirstChild;
				string nodevalue=xmlelem.InnerText;
				count=int.Parse(nodevalue)+1;
				xmlelem.InnerText=count.ToString();
			}
			doc.Save(pg.Server.MapPath("DataQueryCount.xml"));
		}
		public void getcount(System.Web.UI.Page pg,System.Web.UI.WebControls.Label lb)
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(pg.Server.MapPath("DataQueryCount.xml"));
			XmlNode rootnode = doc.DocumentElement;
			int count=0;
			if(rootnode!=null)
			{
				XmlNode xmlelem=rootnode.FirstChild;
				string nodevalue=xmlelem.InnerText;
				count=int.Parse(nodevalue);
			}
			lb.Text=count.ToString();
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
		///		设计器支持所需的方法 - 不要使用代码编辑器
		///		修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.dgdata1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgdata1_PageIndexChanged);

		}
		#endregion
        [Ajax.AjaxMethod]
		public DataSet getTestStation(string prename)
		{
			DataSet ds=new DataSet();
			try
			{
				string strSQL = @"select TESTSTATION_NAME as txt from "+prename.Trim().ToUpper()+@"_TESTSTATION order by TESTSTATION_NAME";
				conn.ExecuteAdapter(strSQL).Fill(ds);	
			}
			catch(Exception err)
			{
				string k=err.Message;
			}
			return ds;
		}
		protected void btnQUERY_Click(object sender, System.EventArgs e)
		{
			binddata();
		}
        private void binddata()
		{
		
			if((this.CheckBox1.Checked==true)&&(this.txtSTARTTIME.DateTextBox.Text.Trim().Length!=0)&&(txtEndDate.DateTextBox.Text.Trim().Length!=0))
			{
				if(this.DropDownList1.SelectedIndex==this.DropDownList1.Items.Count-1)
				{
                    Response.Write("<script language=javascript>alert('" + (String)GetGlobalResourceObject("SFCQuery", "ErrorModel") + "')</script>");
					return;
				}
				if(this.DropDownList2.SelectedIndex==this.DropDownList2.Items.Count-1)
				{
                    Response.Write("<script language=javascript>alert('" + (String)GetGlobalResourceObject("SFCQuery", "ErrorStation") + "')</script>");
					return;
				}
				if(this.txtSTARTTIME.DateTextBox.Text.Trim().Length!=0&&this.txtEndDate.DateTextBox.Text.Trim().Length!=0)
				{
					if(Convert.ToDateTime(this.txtSTARTTIME.DateTextBox.Text.Trim())>=Convert.ToDateTime(this.txtEndDate.DateTextBox.Text.Trim()))
					{
                        Response.Write("<script language=javascript>alert('" + (String)GetGlobalResourceObject("SFCQuery", "ErrorTime") + "')</script>");
						return;
					}
				}
				else
				{
                    Response.Write("<script language=javascript>alert('" + (String)GetGlobalResourceObject("SFCQuery", "ErrorTimenull") + "')</script>");
					return;
				}
				sqlsum="";
				string teststation=this.DropDownList2.SelectedItem.Text.Trim();
				string testproductid=this.txtProductid.Text.Trim();
				System.Data.OracleClient.OracleDataReader orr=null;
			    if((teststation=="TB1")||(teststation=="TB2")||(teststation=="AB2")||(teststation=="AB3"))
				{
					limittable = new DataTable();
					if((this.txtSTARTTIME.DateTextBox.Text.Trim().Length!=0)&&(txtEndDate.DateTextBox.Text.Trim().Length!=0))
					{
						require+=" and BBDATE>=to_date('"+this.txtSTARTTIME.DateTextBox.Text.Trim()+"','yyyy-mm-dd hh24:mi:ss') and BBDATE<=to_date('"+txtEndDate.DateTextBox.Text.Trim()+"','yyyy-mm-dd hh24:mi:ss')";
					}
					preTname="BASEBANDTEST_"+teststation;
					if(testproductid.Length==0)
					{
						sqlsum="select B.PRODUCTID,B.TESTTYPE,B.MESSAGE,B.TESTCONTENT,B.VALUE,B.TSTEP,B.DATETIME,C.BBDATE from "+objname+".BASEBANDTEST_"+teststation+"_BIG B,"+objname+".BASEBANDTEST_"+teststation+"_SMALL C,(SELECT D.PRODUCTID,MAX(S.BBDATE) FBBDATE,MAX(D.TSTEP) TSTEP FROM "+objname+".BASEBANDTEST_"+teststation+"_BIG D,"+objname+".BASEBANDTEST_"+teststation+"_SMALL S where D.PRODUCTID=S.PRODUCTID "+require+" GROUP BY D.PRODUCTID) F WHERE B.PRODUCTID=F.PRODUCTID AND C.BBDATE=F.FBBDATE "+require+" ORDER BY B.PRODUCTID,B.TSTEP";
					}
					else
					{
						sqlsum="select B.PRODUCTID,B.TESTTYPE,B.MESSAGE,B.TESTCONTENT,B.VALUE,B.TSTEP,B.DATETIME,C.BBDATE from "+objname+".BASEBANDTEST_"+teststation+"_BIG B,"+objname+".BASEBANDTEST_"+teststation+"_SMALL C,(SELECT D.PRODUCTID,MAX(S.BBDATE) FBBDATE,MAX(D.TSTEP) TSTEP FROM "+objname+".BASEBANDTEST_"+teststation+"_BIG D,"+objname+".BASEBANDTEST_"+teststation+"_SMALL S where D.PRODUCTID=S.PRODUCTID "+require+" GROUP BY D.PRODUCTID) F WHERE B.PRODUCTID=F.PRODUCTID AND C.BBDATE=F.FBBDATE "+require+" AND B.PRODUCTID='"+testproductid.ToUpper()+"' ORDER BY B.PRODUCTID,B.TSTEP";
					}
					try
					{					
						conn.ExecuteAdapter(sqlsum).Fill(limittable);
						if(limittable==null)
							return;
						dgdata1.DataSource=limittable;
						dgdata1.DataBind();
						limittable.Dispose();
					}
					catch(Exception err)
					{
						orr.Close();
						string k= err.Message;
						return;
					}
				}
				else if(((this.DropDownList2.SelectedItem.Text.Trim()=="PT")||(this.DropDownList2.SelectedItem.Text.Trim()=="EPT")||(this.DropDownList2.SelectedItem.Text.Trim()=="WL")))
				{
					limittable = new DataTable();
					if(this.DropDownList2.SelectedItem.Text.Trim()=="PT")
					{
						if((this.txtSTARTTIME.DateTextBox.Text.Trim().Length!=0)&&(txtEndDate.DateTextBox.Text.Trim().Length!=0))
						{
							require+=" and PTDATE>=to_date('"+this.txtSTARTTIME.DateTextBox.Text.Trim()+"','yyyy-mm-dd hh24:mi:ss') and PTDATE<=to_date('"+txtEndDate.DateTextBox.Text.Trim()+"','yyyy-mm-dd hh24:mi:ss')";
						}
						preTname=objname+".PRETEST_PT";
					}
					else if(this.DropDownList2.SelectedItem.Text.Trim()=="EPT")
					{
						if((this.txtSTARTTIME.DateTextBox.Text.Trim().Length!=0)&&(txtEndDate.DateTextBox.Text.Trim().Length!=0))
						{
							require+=" and PTDATE>=to_date('"+this.txtSTARTTIME.DateTextBox.Text.Trim()+"','yyyy-mm-dd hh24:mi:ss') and PTDATE<=to_date('"+txtEndDate.DateTextBox.Text.Trim()+"','yyyy-mm-dd hh24:mi:ss')";
						}
						preTname=objname+".EDGEPRETEST_EPT";
					}
					else
					{
						if((this.txtSTARTTIME.DateTextBox.Text.Trim().Length!=0)&&(txtEndDate.DateTextBox.Text.Trim().Length!=0))
						{
							require+=" and WLDATE>=to_date('"+this.txtSTARTTIME.DateTextBox.Text.Trim()+"','yyyy-mm-dd hh24:mi:ss') and WLDATE<=to_date('"+txtEndDate.DateTextBox.Text.Trim()+"','yyyy-mm-dd hh24:mi:ss')";
						}
						preTname=objname+".WIRELESS_WL";
					}
					if (testproductid.Length==0)
					{
						if((this.DropDownList2.SelectedItem.Text.Trim()=="PT")||(this.DropDownList2.SelectedItem.Text.Trim()=="EPT"))
						{
							sqlsum="select B.PRODUCTID,B.TESTTYPE,B.CH,B.PWLV,B.PW,B.TESTCONTENT,B.VALUE,B.TSTEP,B.DATETIME,C.PTDATE from "+preTname+"_BIG B,"+preTname+"_SMALL C,(SELECT D.PRODUCTID,MAX(S.PTDATE) FBBDATE,MAX(D.TSTEP) TSTEP FROM "+preTname+"_BIG D,"+preTname+"_SMALL S where D.PRODUCTID=S.PRODUCTID "+require+" GROUP BY D.PRODUCTID) F WHERE B.PRODUCTID=F.PRODUCTID AND C.PTDATE=F.FBBDATE "+require+" ORDER BY B.PRODUCTID,B.TSTEP";					
						}
						else
						{
							sqlsum="select B.PRODUCTID,B.TESTTYPE,B.CH,B.PWLV,B.TESTCONTENT,B.VALUE,B.TSTEP,B.DATETIME,C.WLDATE from "+preTname+"_BIG B,"+preTname+"_SMALL C,(SELECT D.PRODUCTID,MAX(S.WLDATE) FBBDATE,MAX(D.TSTEP) TSTEP FROM "+preTname+"_BIG D,"+preTname+"_SMALL S where D.PRODUCTID=S.PRODUCTID "+require+" GROUP BY D.PRODUCTID) F WHERE B.PRODUCTID=F.PRODUCTID AND C.WLDATE=F.FBBDATE "+require+" ORDER BY B.PRODUCTID,B.TSTEP";					
						}
					}
					else
					{
						if((this.DropDownList2.SelectedItem.Text.Trim()=="PT")||(this.DropDownList2.SelectedItem.Text.Trim()=="EPT")) 
						{
							sqlsum="select B.PRODUCTID,B.TESTTYPE,B.CH,B.PWLV,B.PW,B.TESTCONTENT,B.VALUE,B.TSTEP,B.DATETIME,C.PTDATE from "+preTname+"_BIG B,"+preTname+"_SMALL C,(SELECT D.PRODUCTID,MAX(S.PTDATE) FBBDATE,MAX(D.TSTEP) TSTEP FROM "+preTname+"_BIG D,"+preTname+"_SMALL S where D.PRODUCTID=S.PRODUCTID "+require+" GROUP BY D.PRODUCTID) F WHERE B.PRODUCTID=F.PRODUCTID AND C.PTDATE=F.FBBDATE "+require+" AND B.PRODUCTID='"+testproductid.ToUpper()+"' ORDER BY B.PRODUCTID,B.TSTEP";
						}
						else
						{
							sqlsum="select B.PRODUCTID,B.TESTTYPE,B.CH,B.PWLV,B.TESTCONTENT,B.VALUE,B.TSTEP,B.DATETIME,C.WLDATE from "+preTname+"_BIG B,"+preTname+"_SMALL C,(SELECT D.PRODUCTID,MAX(S.WLDATE) FBBDATE,MAX(D.TSTEP) TSTEP FROM "+preTname+"_BIG D,"+preTname+"_SMALL S where D.PRODUCTID=S.PRODUCTID "+require+" GROUP BY D.PRODUCTID) F WHERE B.PRODUCTID=F.PRODUCTID AND C.WLDATE=F.FBBDATE "+require+" AND B.PRODUCTID='"+testproductid.ToUpper()+"' ORDER BY B.PRODUCTID,B.TSTEP";
						}
					}
					try
					{
						conn.ExecuteAdapter(sqlsum).Fill(limittable);
						if(limittable==null)
							return;
						dgdata1.DataSource=limittable;
						dgdata1.DataBind();	
					} 
					catch(Exception err)
					{
						orr.Close();
						string k= err.Message;
						return;
					}
				}
				else if(((this.DropDownList2.SelectedItem.Text.Trim()=="BT")||(this.DropDownList2.SelectedItem.Text.Trim()=="BTWL")))
				{
					limittable = new DataTable();
					if((this.txtSTARTTIME.DateTextBox.Text.Trim().Length!=0)&&(txtEndDate.DateTextBox.Text.Trim().Length!=0))
					{
						require +=" and PDDATE>to_date('"+this.txtSTARTTIME.DateTextBox.Text.Trim()+"','yyyy-mm-dd hh24:mi:ss') and PDDATE<to_date('"+txtEndDate.DateTextBox.Text.Trim()+"','yyyy-mm-dd hh24:mi:ss')";
					}	
					if(this.DropDownList2.SelectedItem.Text.Trim()=="BT")
					{
						preTname=objname+".BLUETOOTH_BT";							
					}
					else
					{
						preTname=objname+".BTWIRELESS_BTWL";
					}
					if(testproductid.Length==0)
					{
						sqlsum="select B.PRODUCTID,B.TESTTYPE,B.CH,B.TXCH,B.RXCH,B.TESTCONTENT,B.VALUE,B.TSTEP,B.DATETIME,C.PDDATE from  "+preTname+"_BIG B,"+preTname+"_SMALL C,(SELECT D.PRODUCTID,MAX(S.PDDATE) FBBDATE,MAX(D.TSTEP) TSTEP FROM  "+preTname+"_BIG D,"+preTname+"_SMALL S where D.PRODUCTID=S.PRODUCTID "+require+" GROUP BY D.PRODUCTID) F WHERE B.PRODUCTID=F.PRODUCTID AND C.PDDATE=F.FBBDATE "+require+" ORDER BY B.PRODUCTID,B.TSTEP";
					}
					else
					{
						sqlsum="select B.PRODUCTID,B.TESTTYPE,B.CH,B.TXCH,B.RXCH,B.TESTCONTENT,B.VALUE,B.TSTEP,B.DATETIME,C.PDDATE from  "+preTname+"_BIG B,"+preTname+"_SMALL C,(SELECT D.PRODUCTID,MAX(S.PDDATE) FBBDATE,MAX(D.TSTEP) TSTEP FROM  "+preTname+"_BIG D,"+preTname+"_SMALL S where D.PRODUCTID=S.PRODUCTID "+require+" GROUP BY D.PRODUCTID) F WHERE B.PRODUCTID=F.PRODUCTID AND C.PDDATE=F.FBBDATE "+require+" AND B.PRODUCTID='"+testproductid.ToUpper()+"' ORDER BY B.PRODUCTID,B.TSTEP";
					}
					try
					{
						conn.ExecuteAdapter(sqlsum).Fill(limittable);
						if(limittable==null)
							return;
						dgdata1.DataSource=limittable;
						dgdata1.DataBind();	
					}
					catch(Exception err)
					{
						orr.Close();
						string k= err.Message;
						return;
					}
				}
				IsDone=true;
			}				
            else if ((this.CheckBox1.Checked==false)&&(this.txtSTARTTIME.DateTextBox.Text.Trim().Length==0)&&(this.txtEndDate.DateTextBox.Text.Trim().Length==0))
			{
				if(this.DropDownList1.SelectedIndex==this.DropDownList1.Items.Count-1)
				{
                    Response.Write("<script language=javascript>alert('" + (String)GetGlobalResourceObject("SFCQuery", "ErrorModel") + "')</script>");
					return;
				}
				if(this.DropDownList2.SelectedIndex==this.DropDownList2.Items.Count-1)
				{
                    Response.Write("<script language=javascript>alert('" + (String)GetGlobalResourceObject("SFCQuery", "ErrorStation") + "')</script>");
					return;
				}
				if(this.txtProductid.Text.Trim().Length==0)
				{
                    Response.Write("<script language=javascript>alert('" + (String)GetGlobalResourceObject("SFCQuery", "ErrorProductID") + "')</script>");
					return;
				}
				sqlsum="";
				string teststation=this.DropDownList2.SelectedItem.Text.Trim();
				string testproductid=this.txtProductid.Text.Trim();
				System.Data.OracleClient.OracleDataReader orr=null;
				if((teststation=="TB1")||(teststation=="TB2")||(teststation=="AB2")||(teststation=="AB3"))
				{
					preTname="BASEBANDTEST_"+teststation;
					limittable = new DataTable();
					sqlsum="select * from "+objname+".BASEBANDTEST_"+teststation+"_BIG B where B.PRODUCTID='"+testproductid.ToUpper()+"' ORDER BY B.PRODUCTID,B.TSTEP";
					try
					{  			
						conn.ExecuteAdapter(sqlsum).Fill(limittable);
						if(limittable==null)
							return;
						dgdata1.DataSource=limittable;
						dgdata1.DataBind();
						limittable.Dispose();
					}
					catch(Exception err)
					{
						orr.Close();
						string k= err.Message;
						return;
					}
				}
				else if(((this.DropDownList2.SelectedItem.Text.Trim()=="PT")||(this.DropDownList2.SelectedItem.Text.Trim()=="EPT")||(this.DropDownList2.SelectedItem.Text.Trim()=="WL")))
				{
					limittable = new DataTable();
					if (this.DropDownList2.SelectedItem.Text=="PT")
					{
						preTname=objname+".PRETEST_PT";
					}
					else if (this.DropDownList2.SelectedItem.Text=="EPT")
					{
						preTname=objname+".EDGEPRETEST_EPT";
					}
					else
					{
						preTname=objname+".WIRELESS_WL";
					}
					sqlsum="select * from "+preTname+"_BIG B WHERE B.PRODUCTID='"+testproductid.ToUpper()+"' ORDER BY B.PRODUCTID,B.TSTEP";
					try
					{												
						conn.ExecuteAdapter(sqlsum).Fill(limittable);
						if(limittable==null)
							return;
						dgdata1.DataSource=limittable;
						dgdata1.DataBind();
						limittable.Dispose();
					}
					catch(Exception err)
					{
						orr.Close();
						string k= err.Message;
						return;
					}
				}
				else if(((this.DropDownList2.SelectedItem.Text.Trim()=="BT")||(this.DropDownList2.SelectedItem.Text.Trim()=="BTWL")))
				{
					limittable = new DataTable();
					if(this.DropDownList2.SelectedItem.Text=="BT")
					{
						preTname=objname+".BLUETOOTH_BT";							
					}
					else
					{
						preTname=objname+".BTWIRELESS_BTWL";
					}
					sqlsum="select * from "+preTname+"_BIG B WHERE B.PRODUCTID='"+testproductid.ToUpper()+"' ORDER BY B.PRODUCTID,B.TSTEP";
					try
					{												
						conn.ExecuteAdapter(sqlsum).Fill(limittable);
						if(limittable==null)
							return;
						dgdata1.DataSource=limittable;
						dgdata1.DataBind();
						limittable.Dispose();
					}
					catch(Exception err)
					{
						orr.Close();
						string k= err.Message;
						return;
					}
				}
				IsDone=true;
            }
			else
			{
                string errorquery = (String)GetGlobalResourceObject("SFCQuery", "ErrorQuery");
				Response.Write("<script language=javascript>alert('"+errorquery+"')</script>");
			}
			if(limittable!=null)
			{
				System.Data.DataView source = null;
				source = new DataView (limittable);	
				dgdata1.DataSource = source;
				dgdata1.DataBind ();
			}
			else
			{
                string nodata = (String)GetGlobalResourceObject("SFCQuery", "NoData");
				Response.Write("<script language=javascript>alert('"+nodata+"')</script>");
			}
		}
		protected void DropDownList2_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string tmpname = this.DropDownList1.SelectedValue.Trim().ToLower();

		}

		protected void DropDownList1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.DropDownList1.SelectedIndex!=this.DropDownList1.Items.Count-1)
			{
				string tmpname = this.DropDownList1.SelectedValue.Trim();//.ToLower();
				objname=tmpname+"_LH";
				conn= new CDData(objname,objname);
				conn.DP1Bind(this.DropDownList2,tmpname);
                this.DropDownList2.Items.Add((String)GetGlobalResourceObject("SFCQuery", "ForSelect"));
				this.DropDownList2.SelectedIndex=this.DropDownList2.Items.Count-1;
			}
		}
		protected void btnOUTPUT_Click(object sender, System.EventArgs e)
		{
			if(IsDone==true)
			{ 
				DataTable dt=new DataTable();
				conn.ExecuteAdapter(sqlsum).Fill(dt);
				if(dt==null)
					return;
				DataGrid gd=new DataGrid();
				gd.DataSource=dt;
			    gd.DataBind();
				cm.ExcelOut(this.Page,gd);
			}
			else
			{
                string notable = (String)GetGlobalResourceObject("SFCQuery", "NoTable");
				Response.Write("<script language=javascript>alert('"+notable+"')</script>");
			}
		}
		private void dgdata1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			dgdata1.CurrentPageIndex=e.NewPageIndex;
			this.binddata();
		}
		protected void CheckBox1_CheckedChanged(object sender, System.EventArgs e)
		{
			
            if (CheckBox1.Checked==false)
			{
				CheckBox1.Checked=true;
				CheckBox1.Checked=false;
				this.txtSTARTTIME.DateTextBox.Enabled=false;
				this.txtSTARTTIME.DateTextBox.BackColor=System.Drawing.Color.LightGray;
				this.txtEndDate.DateTextBox.Enabled=false;
				this.txtEndDate.DateTextBox.BackColor=System.Drawing.Color.LightGray;
				this.txtSTARTTIME.DateTextBox.Text="";
				this.txtEndDate.DateTextBox.Text="";
			}
			else if (CheckBox1.Checked==true)
			{
				CheckBox1.Checked=false;
				CheckBox1.Checked=true;
				this.txtSTARTTIME.DateTextBox.Enabled=true;
				this.txtSTARTTIME.DateTextBox.BackColor=System.Drawing.Color.White;
				this.txtEndDate.DateTextBox.Enabled=true;
				this.txtEndDate.DateTextBox.BackColor=System.Drawing.Color.White;
                txtEndDate.DateTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd") + " 08:00";
                txtSTARTTIME.DateTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
			}
		}
	}
}
