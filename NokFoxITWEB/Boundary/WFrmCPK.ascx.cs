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
	///		WFrmCPK 的摘要说明。
	/// </summary>
    public partial class WFrmCPK : System.Web.UI.UserControl
	{
        //protected System.Web.UI.WebControls.DataGrid dgcpk1;
        //protected System.Web.UI.WebControls.DropDownList DropDownList3;
        //protected System.Web.UI.WebControls.DropDownList DropDownList2;
        //protected System.Web.UI.WebControls.DropDownList DropDownList1;
		System.Data.DataTable Table = null;
        //protected System.Web.UI.WebControls.Label lblWORKORDER;
        //protected System.Web.UI.WebControls.TextBox txtWORKORDER;
        //protected System.Web.UI.WebControls.Label lblSTARTTIME;
        //protected System.Web.UI.WebControls.TextBox txtSTARTTIME.DateTextBox;
        //protected System.Web.UI.WebControls.Label lblENDTIME;
        //protected System.Web.UI.WebControls.TextBox txtORDER;
        //protected System.Web.UI.WebControls.DropDownList dpdLINE;
        //protected System.Web.UI.WebControls.Button btnQUERY;
        //protected System.Web.UI.WebControls.TextBox txtEMPLOYEE;
        //protected System.Web.UI.WebControls.DropDownList dpdCOMPUTER;
        //protected System.Web.UI.WebControls.Button btnOUTPUT;
		string preTname;

        //private CultureInfo "SFCQuery" = new CultureInfo(CultureInfo.CurrentCulture.Name);
        //protected System.Web.UI.WebControls.Label lblModel;
        //protected System.Web.UI.WebControls.Label lblTStation;
        //protected System.Web.UI.WebControls.Label lblContent;
        //protected System.Web.UI.WebControls.Label lblWO;
        //protected System.Web.UI.WebControls.Label lblLine;
        //protected System.Web.UI.WebControls.Label lblEmp;
        //protected System.Web.UI.WebControls.Label lblTestPC;
        //protected System.Web.UI.WebControls.TextBox txtEndDate.DateTextBox;
        //private static ResourceManager rm = new ResourceManager(""SFCQuery".MultiLanguage."SFCQuery"",Assembly.GetExecutingAssembly());
        //protected System.Web.UI.HtmlControls.HtmlInputButton hbtndatefrm;
        //protected System.Web.UI.HtmlControls.HtmlInputButton hbtndateto;
        //protected System.Web.UI.WebControls.Label lblcountname;
        //protected System.Web.UI.WebControls.Label lblacount;
		static string objname="";
		static bool IsDone;
		static bool flag;
		static CDData conn;
		
		UsingClass.ComUtility cm=new ComUtility();


		private void Page_Load(object sender, System.EventArgs e)
        {
        //    this."SFCQuery"=new CultureInfo(Session["Language"].ToString());
        //    Thread.CurrentThread.CurrentUICulture=this."SFCQuery";
//			Ajax.Utility.RegisterTypeForAjax(typeof(WFrmCPK));
			
			if(!IsPostBack)
			{
				//			    Boundary.CDData.DP1Bind(this.DropDownList2);
				cm.getmodel(this.Page,this.DropDownList1);

				flag=false;
				IsDone=false;
				this.lblEmp.Text = (String)GetGlobalResourceObject("SFCQuery","EmpID");
				this.txtEMPLOYEE.ToolTip=(String)GetGlobalResourceObject("SFCQuery","EmpTip");
				this.lblContent.Text=(String)GetGlobalResourceObject("SFCQuery","TestContent");
				this.lblENDTIME.Text=(String)GetGlobalResourceObject("SFCQuery","DateTo");
				this.lblLine.Text=(String)GetGlobalResourceObject("SFCQuery","Line");
				this.lblModel.Text=(String)GetGlobalResourceObject("SFCQuery","Model");
				this.lblSTARTTIME.Text=(String)GetGlobalResourceObject("SFCQuery","DateFrom");
				this.lblTestPC.Text=(String)GetGlobalResourceObject("SFCQuery","TestPC");
				this.lblTStation.Text=(String)GetGlobalResourceObject("SFCQuery","TestStation");
				this.lblWO.Text=(String)GetGlobalResourceObject("SFCQuery","WO");
				this.btnOUTPUT.Text=(String)GetGlobalResourceObject("SFCQuery","OutPut");
				this.btnQUERY.Text=(String)GetGlobalResourceObject("SFCQuery","Query");
				this.txtSTARTTIME.DateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd")+" 08:00";
				this.txtEndDate.DateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
				if (this.txtSTARTTIME.DateTextBox.Text.CompareTo(this.txtEndDate.DateTextBox.Text)>0)
					this.txtSTARTTIME.DateTextBox.Text = this.txtEndDate.DateTextBox.Text;
				cm.getcount(this.Page,this.lblacount);                                                                                                                                                                                                                              
				
				this.DropDownList1.Items.Add((String)GetGlobalResourceObject("SFCQuery","ForSelect"));
				this.DropDownList1.SelectedIndex=this.DropDownList1.Items.Count-1;
            //this.hbtndatefrm.Attributes["onclick"]= "return showCalendar('"+this.txtSTARTTIME.DateTextBox.ClientID+"', '%Y-%m-%d %H:%M', '24', true);";
            //    this.hbtndateto.Attributes["onclick"]="return showCalendar('"+this.txtEndDate.DateTextBox.ClientID+"', '%Y-%m-%d %H:%M', '24', true);";
                txtEndDate.DateTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                txtSTARTTIME.DateTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
				//this.lblcountname.Text=(String)GetGlobalResourceObject("CountName","SFCQuery");
//				this.DropDownList1.Attributes.Add("onchange", "showNext(this.options[selectedIndex].value,'"+this.DropDownList2.ClientID+"');");
//				this.lblWORKORDER.Text=(String)GetGlobalResourceObject("WO");

//				Response.Write("<script type=\"text/javascript\" src=\"../js/calendar1.js\"></script>");
//				StringBuilder scriptstring = new StringBuilder();
//				scriptstring.Append("<script language=javascript>");
//				scriptstring.Append("function ShowInfo(){");
//				scriptstring.Append("getElementById(\"showtext\")");
//				scriptstring.Append("showtext.innerText='页面已经提交，请耐性等待'");
//				scriptstring.Append("</script>");
//				string js=scriptstring.ToString();
//				Page.RegisterStartupScript("ShowInfo",js);
//				this.btnQUERY.Attributes.Add("onclick","ShowInfo()");
			}
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
			this.DropDownList1.SelectedIndexChanged += new System.EventHandler(this.DropDownList1_SelectedIndexChanged);
			this.DropDownList2.SelectedIndexChanged += new System.EventHandler(this.DropDownList2_SelectedIndexChanged);
			this.DropDownList3.SelectedIndexChanged += new System.EventHandler(this.DropDownList3_SelectedIndexChanged);
			this.lblacount.Load += new System.EventHandler(this.lblacount_Load);
			this.btnQUERY.Click += new System.EventHandler(this.btnQUERY_Click);
			this.btnOUTPUT.Click += new System.EventHandler(this.btnOUTPUT_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		
		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			//binddata();
			bindcpk();
		}
		private void binddata()
		{			
//			"SFCQuery".Boundary.CDData conn=new "SFCQuery".Boundary.CDData();			
//			string strsql="select * from TDS_"+DropDownList3.SelectedItem.Text.Trim()+" order by type,ch,pwlv";
//			conn.GridDataBind(strsql,dgcpk2);
		}
		private void bindcpk()
		{
			if((this.txtORDER.Text.Trim().Length!=0)||((this.txtSTARTTIME.DateTextBox.Text.Trim().Length!=0)&&(txtEndDate.DateTextBox.Text.Trim().Length!=0))||(this.txtEMPLOYEE.Text.Trim().Length!=0)||(this.dpdCOMPUTER.SelectedIndex!=0)||(this.dpdLINE.SelectedIndex!=0))
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
				if(this.DropDownList3.SelectedIndex==this.DropDownList3.Items.Count-1)
				{
                    Response.Write("<script language=javascript>alert('" + (String)GetGlobalResourceObject("SFCQuery", "ErrorContent") + "')</script>");
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
				try
				{
					if(this.txtEMPLOYEE.Text.Trim().Length!=0)
					{
						if(this.txtEMPLOYEE.Text.Trim().Length!=8)
						{
                            Response.Write("<script language=javascript>alert('" + (String)GetGlobalResourceObject("SFCQuery", "ErrorEmp") + "')</script>");
							return;
						}
					}
					if(this.txtORDER.Text.Trim().Length!=0)
					{
						if(this.txtORDER.Text.Trim().Length!=8)
						{
                            Response.Write("<script language=javascript>alert('" + (String)GetGlobalResourceObject("SFCQuery", "WONotExist") + "')</script>");
							return;
						}
					}
				}
				catch(Exception err)
				{
					string k=err.Message;
				}
				string require = "";
				string sqllimit="";
				string sqlsum="";
				string teststation=this.DropDownList2.SelectedItem.Text.Trim();
				ArrayList values=null;
				float max=0.0f,min=0.0f,sigma=0.0f,avg=0.0f;
				double limitu=0.00,limitl=0.00;
				DataTable limittable = new DataTable();
				System.Data.OracleClient.OracleDataReader orr=null;
				
				if(this.txtEMPLOYEE.Text.Trim().Length!=0)
				{
					require+=" and EMPLOYEE='"+this.txtEMPLOYEE.Text.ToUpper().Trim()+"'";
				}
				if(this.txtORDER.Text.Trim().Length!=0)
				{
					require+=" and WORKORDER='"+this.txtORDER.Text.ToUpper().Trim()+"'";
				}
				if(this.dpdCOMPUTER.SelectedIndex!=0)
				{
					require+=" and COMPUTERNAME='"+this.dpdCOMPUTER.SelectedValue.Trim()+"'";
				}
				if(this.dpdLINE.SelectedIndex!=0)
				{
					require+=" and ONLINENAME='"+this.dpdLINE.SelectedValue.Trim()+"'";
				}
				if((teststation=="TB1")||(teststation=="TB2")||(teststation=="AB2")||(teststation=="AB3"))
				{
					if((this.txtSTARTTIME.DateTextBox.Text.Trim().Length!=0)&&(txtEndDate.DateTextBox.Text.Trim().Length!=0))
					{
						require+=" and BBDATE>=to_date('"+this.txtSTARTTIME.DateTextBox.Text.Trim()+"','yyyy-mm-dd hh24:mi:ss') and BBDATE<=to_date('"+txtEndDate.DateTextBox.Text.Trim()+"','yyyy-mm-dd hh24:mi:ss')";
					}
					preTname="BASEBANDTEST_"+teststation;
					sqllimit = "select TESTTYPE,TESTCONTENT,LIMITU,LIMITL from "+objname+".BASEBANDTEST_"+teststation+"_LIMIT where trim(TESTCONTENT)='"+this.DropDownList3.SelectedValue.Trim()+"'" ;
					Table = new DataTable();
					Table.Columns.Add("TestContent");
					Table.Columns.Add("TestType");
					Table.Columns.Add("Min");
					Table.Columns.Add("Max");
					Table.Columns.Add("AVG");
					Table.Columns.Add("Sigma");
					//				Table.Columns.Add("CP");
					Table.Columns.Add("CPK");
					try
					{
						conn.ExecuteAdapter(sqllimit).Fill(limittable);
						if(limittable==null)
							return;
						else
						{
							DataRow row=null;
							for(int i=0;i<limittable.Rows.Count;i++)
							{
								if((teststation=="AB2"))
								{
									sqlsum="select B.VALUE from "+objname+".BASEBANDTEST_"+teststation+"_BIG B,"+objname+".BASEBANDTEST_"+teststation+"_SMALL S,(SELECT C.PRODUCTID,MAX(D.BBDATE) FBBDATE,MAX(C.DATETIME) DATETIME,MAX(C.TSTEP) TSTEP FROM "+objname+".BASEBANDTEST_"+teststation+"_BIG C,"+objname+".BASEBANDTEST_"+teststation+"_SMALL D where C.PRODUCTID=D.PRODUCTID AND C.TESTCONTENT LIKE '%"+limittable.Rows[i]["TESTCONTENT"].ToString()+"' and C.TESTTYPE = '"+limittable.Rows[i]["TESTTYPE"].ToString()+"'"+require+"  GROUP BY C.PRODUCTID) F where S.PRODUCTID=F.PRODUCTID AND F.PRODUCTID=B.PRODUCTID AND S.BBDATE=F.FBBDATE and B.DATETIME=F.DATETIME and B.TSTEP=F.TSTEP AND B.TESTCONTENT LIKE '%"+limittable.Rows[i]["TESTCONTENT"].ToString()+"' and B.TESTTYPE = '"+limittable.Rows[i]["TESTTYPE"].ToString()+"'"+require+" order by to_number(rtrim(B.VALUE,'%'))";
								}
								else
								{
									sqlsum="select B.VALUE from "+objname+".BASEBANDTEST_"+teststation+"_BIG B,"+objname+".BASEBANDTEST_"+teststation+"_SMALL S,(SELECT C.PRODUCTID,MAX(D.BBDATE) FBBDATE,MAX(C.DATETIME) DATETIME,MAX(C.TSTEP) TSTEP FROM "+objname+".BASEBANDTEST_"+teststation+"_BIG C,"+objname+".BASEBANDTEST_"+teststation+"_SMALL D where C.PRODUCTID=D.PRODUCTID AND C.TESTCONTENT LIKE '"+limittable.Rows[i]["TESTCONTENT"].ToString()+"%' and C.TESTTYPE = '"+limittable.Rows[i]["TESTTYPE"].ToString()+"'"+require+" GROUP BY C.PRODUCTID) F where S.PRODUCTID=F.PRODUCTID AND F.PRODUCTID=B.PRODUCTID AND S.BBDATE=F.FBBDATE and B.DATETIME=F.DATETIME and B.TSTEP=F.TSTEP AND B.TESTCONTENT LIKE '"+limittable.Rows[i]["TESTCONTENT"].ToString()+"%' and B.TESTTYPE = '"+limittable.Rows[i]["TESTTYPE"].ToString()+"'"+require+" order by to_number(rtrim(B.VALUE,'%'))";
								}
								orr=null;
								orr=conn.ExecuteReader(sqlsum);
								if(orr==null)
									return;
								else
								{
									values=new ArrayList();
									while(orr.Read())
									{
										float t=0.00f;
										if(orr[0].ToString().IndexOf("%")!=-1)
										{
											t=float.Parse(orr[0].ToString().Trim('%'))/100.0f;
										}
										else
											t=float.Parse(orr[0].ToString());
										values.Add(t);
									}
									orr.Close();
									string sname=Server.UrlEncode(teststation+i.ToString());
									if(values.Count>0)
									{
										ComUtility com=new ComUtility(values);
										
										max = com.CalMax();
										min = com.CalMin();
										sigma= com.CalSig();
										avg=com.CalAvg();
										row=Table.NewRow();
										row["TestType"]=limittable.Rows[i]["TESTTYPE"].ToString();
										row["TestContent"]=limittable.Rows[i]["TESTCONTENT"].ToString();
										row["Min"]=min.ToString();
										row["AVG"]=avg.ToString();
										row["Sigma"]=sigma.ToString();
										row["Max"]=max.ToString();
										//									row["CP"]=Convert.ToString((max-min)/(6*sigma));
										if(limittable.Rows[i]["LIMITU"].ToString().Trim().Length==0)
										{
											limitl=float.Parse(limittable.Rows[i]["LIMITL"].ToString());
											limitu=Math.E;
											//											row["CPK"]="<a href='drawimage.aspx?sname="+sname+"' target=_blank>"+(avg-limitl)/(3*sigma)+"</a>";
											values.Insert(1,null);
											values.Insert(0,limitl);
										}
										else if(limittable.Rows[i]["LIMITL"].ToString().Trim().Length==0)
										{
											limitu=float.Parse(limittable.Rows[i]["LIMITU"].ToString());
											limitl=Math.E;
											values.Insert(0,null);
											values.Insert(1,limitu);
											//											row["CPK"]="<a href='drawimage.aspx?sname="+sname+"' target=_blank>"+(limitu-avg)/(3*sigma)+"</a>";
										}
										else
										{
											limitu=float.Parse(limittable.Rows[i]["LIMITU"].ToString());
											limitl=float.Parse(limittable.Rows[i]["LIMITL"].ToString());
											values.Insert(0,limitl);
											values.Insert(1,limitu);
										}
										row["CPK"]="<a href='drawimage.aspx?sname="+sname+"' target=_blank>"+com.CalCPK(limitu,limitl,sigma,avg)+"</a>";
										Table.Rows.Add(row);
										values.Insert(2,min);
										values.Insert(3,max);
										values.Insert(4,avg);
										values.Insert(5,sigma);
										Session[sname]=values;
									}
								}
							}
					
						}
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
					
					Table = new DataTable();
					
					Table.Columns.Add("TestContent");
					Table.Columns.Add("TestType");
					Table.Columns.Add("CH");
					Table.Columns.Add("PWLV");
					Table.Columns.Add("Min");
					Table.Columns.Add("Max");
					Table.Columns.Add("AVG");
					Table.Columns.Add("Sigma");
					Table.Columns.Add("CPK");
					Table.Columns.Add("PW");
		
					//					if((this.DropDownList3.SelectedValue.Trim()!="SWIT")&&(this.DropDownList3.SelectedValue.Trim()!="MOD"))
					//					{
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
						
					sqllimit = "select * from "+preTname+"_LIMIT where trim(TESTCONTENT)='"+this.DropDownList3.SelectedValue.Trim()+"' ORDER BY TESTTYPE,CH,PWLV" ;
					
					try
					{
						conn.ExecuteAdapter(sqllimit).Fill(limittable);
						if(limittable==null)
							return;
						else
						{
							for(int i=0;i<limittable.Rows.Count;i++)
							{
								if(this.DropDownList2.SelectedItem.Text.Trim()=="WL")
								{
									sqlsum="select B.VALUE from "+preTname+"_BIG B,"+preTname+"_SMALL S,(SELECT C.PRODUCTID,MAX(D.WLDATE) FWLDATE,MAX(C.DATETIME) DATETIME,MAX(C.TSTEP) TSTEP FROM "+preTname+"_BIG C,"+preTname+"_SMALL D where C.PRODUCTID=D.PRODUCTID AND C.TESTCONTENT='"+limittable.Rows[i]["TESTCONTENT"].ToString().Trim()+"' and C.TESTTYPE = '"+limittable.Rows[i]["TESTTYPE"].ToString().Trim()+"' and C.CH = '"+limittable.Rows[i]["CH"].ToString().Trim()+"' and C.PWLV = '"+limittable.Rows[i]["PWLV"].ToString().Trim()+"'"+require+" GROUP BY C.PRODUCTID) F where S.PRODUCTID=F.PRODUCTID AND F.PRODUCTID=B.PRODUCTID AND F.FWLDATE=S.WLDATE AND F.DATETIME=B.DATETIME AND F.TSTEP=B.TSTEP and B.TESTCONTENT= '"+limittable.Rows[i]["TESTCONTENT"].ToString().Trim()+"' and B.TESTTYPE = '"+limittable.Rows[i]["TESTTYPE"].ToString().Trim()+"' and CH = '"+limittable.Rows[i]["CH"].ToString().Trim()+"' and PWLV = '"+limittable.Rows[i]["PWLV"].ToString().Trim()+"'"+require+" order by to_number(rtrim(B.VALUE,'%'))";
								}
								else
								{
									if(limittable.Rows[i]["PW"].ToString().Trim().Length==0)
									{
										sqlsum="select B.VALUE from "+preTname+"_BIG B,"+preTname+"_SMALL S,(SELECT C.PRODUCTID,MAX(D.PTDATE) FPTDATE,MAX(C.DATETIME) DATETIME,MAX(C.TSTEP) TSTEP FROM "+preTname+"_BIG C,"+preTname+"_SMALL D where C.PRODUCTID=D.PRODUCTID AND C.TESTCONTENT='"+limittable.Rows[i]["TESTCONTENT"].ToString().Trim()+"' and C.TESTTYPE = '"+limittable.Rows[i]["TESTTYPE"].ToString().Trim()+"' and C.CH = '"+limittable.Rows[i]["CH"].ToString().Trim()+"' and C.PWLV = '"+limittable.Rows[i]["PWLV"].ToString().Trim()+"'"+require+" GROUP BY C.PRODUCTID) F where S.PRODUCTID=F.PRODUCTID AND B.PRODUCTID=F.PRODUCTID AND F.FPTDATE=S.PTDATE AND F.DATETIME=B.DATETIME AND F.TSTEP=B.TSTEP AND B.TESTCONTENT= '"+limittable.Rows[i]["TESTCONTENT"].ToString().Trim()+"' and B.TESTTYPE = '"+limittable.Rows[i]["TESTTYPE"].ToString().Trim()+"' and CH = '"+limittable.Rows[i]["CH"].ToString().Trim()+"' and PWLV = '"+limittable.Rows[i]["PWLV"].ToString().Trim()+"'"+require+"  order by to_number(rtrim(B.VALUE,'%'))";
									}
									else
									{
										sqlsum="select B.VALUE from "+preTname+"_BIG B,"+preTname+"_SMALL S,(SELECT C.PRODUCTID,MAX(D.PTDATE) FPTDATE,MAX(C.DATETIME) DATETIME,MAX(C.TSTEP) TSTEP FROM "+preTname+"_BIG C,"+preTname+"_SMALL D where C.PRODUCTID=D.PRODUCTID and C.TESTCONTENT='"+limittable.Rows[i]["TESTCONTENT"].ToString().Trim()+"' and C.TESTTYPE = '"+limittable.Rows[i]["TESTTYPE"].ToString().Trim()+"' and C.CH = '"+limittable.Rows[i]["CH"].ToString().Trim()+"' and C.PWLV = '"+limittable.Rows[i]["PWLV"].ToString().Trim()+"'"+require+" and C.PW='"+limittable.Rows[i]["PW"].ToString()+"' GROUP BY C.PRODUCTID) F where S.PRODUCTID=F.PRODUCTID AND B.PRODUCTID=F.PRODUCTID AND S.PTDATE=F.FPTDATE AND B.DATETIME=F.DATETIME AND B.TSTEP=F.TSTEP AND B.TESTCONTENT= '"+limittable.Rows[i]["TESTCONTENT"].ToString().Trim()+"' and B.TESTTYPE = '"+limittable.Rows[i]["TESTTYPE"].ToString().Trim()+"' and CH = '"+limittable.Rows[i]["CH"].ToString().Trim()+"' and PWLV = '"+limittable.Rows[i]["PWLV"].ToString().Trim()+"'"+require+" and B.PW='"+limittable.Rows[i]["PW"].ToString()+"' order by to_number(rtrim(B.VALUE,'%'))";
										//sqlsum="select B.VALUE from "+preTname+"_BIG B,"+preTname+"_SMALL S,(SELECT C.PRODUCTID,MAX(D.PTDATE) FPTDATE,MAX(C.DATETIME) DATETIME,MAX(C.TSTEP) TSTEP FROM "+preTname+"_BIG C,"+preTname+"_SMALL D where "+require+" and C.PRODUCTID=D.PRODUCTID and C.TESTCONTENT='"+limittable.Rows[i]["TESTCONTENT"].ToString().Trim()+"' and C.TESTTYPE = '"+limittable.Rows[i]["TESTTYPE"].ToString().Trim()+"' and C.CH = '"+limittable.Rows[i]["CH"].ToString().Trim()+"' and C.PWLV = '"+limittable.Rows[i]["PWLV"].ToString().Trim()+"' and C.PW='"+limittable.Rows[i]["PW"].ToString()+"' GROUP BY C.PRODUCTID) F where "+require+" and S.PRODUCTID=F.PRODUCTID AND B.PRODUCTID=F.PRODUCTID AND S.PTDATE=F.FPTDATE AND B.DATETIME=F.DATETIME AND B.TSTEP=F.TSTEP AND B.TESTCONTENT= '"+limittable.Rows[i]["TESTCONTENT"].ToString().Trim()+"' and B.TESTTYPE = '"+limittable.Rows[i]["TESTTYPE"].ToString().Trim()+"' and CH = '"+limittable.Rows[i]["CH"].ToString().Trim()+"' and PWLV = '"+limittable.Rows[i]["PWLV"].ToString().Trim()+"' and B.PW='"+limittable.Rows[i]["PW"].ToString()+"' order by to_number(rtrim(B.VALUE,'%'))";
									}
								}
								orr=null;
								orr=conn.ExecuteReader(sqlsum);
								if(orr==null)
									return;
								else
								{
									string sname=Server.UrlEncode(teststation+i.ToString());
									values=new ArrayList();
								
									while(orr.Read())
									{
										float t=0.00f;
										if(orr[0].ToString().IndexOf("%")!=-1)
										{
											t=float.Parse(orr[0].ToString().Trim('%'))/100.0f;
										}
										else
											t=float.Parse(orr[0].ToString());
										values.Add(t);
									}
									orr.Close();
										
									if(values.Count>0)
									{
										ComUtility com=new ComUtility(values);

										DataRow row=null;
										row=Table.NewRow();
											
										if(this.DropDownList2.SelectedItem.Text.Trim()!="WL")
										{
											if(limittable.Rows[i]["PW"].ToString().Trim().Length!=0)
											{
												row["PW"]=limittable.Rows[i]["PW"].ToString();
											}
										}
										max = com.CalMax();
										min = com.CalMin();
										//											}
										sigma= com.CalSig();
										avg=com.CalAvg();
										row["TestType"]=limittable.Rows[i]["TESTTYPE"].ToString();
										row["TestContent"]=limittable.Rows[i]["TESTCONTENT"].ToString();
										row["CH"]=limittable.Rows[i]["CH"].ToString();
										row["PWLV"]=limittable.Rows[i]["PWLV"].ToString();
										row["Min"]=min.ToString();
										row["AVG"]=avg.ToString();
										row["Sigma"]=sigma.ToString();
										row["Max"]=max.ToString();
										//									row["CP"]=Convert.ToString((max-min)/(6*sigma));
										if(limittable.Rows[i]["LIMITU"].ToString().Trim().Length==0)
										{
											limitl=float.Parse(limittable.Rows[i]["LIMITL"].ToString());
											limitu=Math.E;
											//											row["CPK"]="<a href='drawimage.aspx?sname="+sname+"' target=_blank>"+(avg-limitl)/(3*sigma)+"</a>";
											values.Insert(1,null);
											values.Insert(0,limitl);
										}
										else if(limittable.Rows[i]["LIMITL"].ToString().Trim().Length==0)
										{
											limitu=float.Parse(limittable.Rows[i]["LIMITU"].ToString());
											limitl=Math.E;
											values.Insert(0,null);
											values.Insert(1,limitu);
											//											row["CPK"]="<a href='drawimage.aspx?sname="+sname+"' target=_blank>"+(limitu-avg)/(3*sigma)+"</a>";
										}
										else
										{
											limitu=float.Parse(limittable.Rows[i]["LIMITU"].ToString());
											limitl=float.Parse(limittable.Rows[i]["LIMITL"].ToString());
											values.Insert(0,limitl);
											values.Insert(1,limitu);
										}
										row["CPK"]="<a href='drawimage.aspx?sname="+sname+"' target=_blank>"+com.CalCPK(limitu,limitl,sigma,avg)+"</a>";
										Table.Rows.Add(row);
										values.Insert(2,min);
										values.Insert(3,max);
										values.Insert(4,avg);
										values.Insert(5,sigma);
										Session[sname]=values;
									}
								}
							}
					
						}
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
					if(this.DropDownList3.SelectedValue.Trim()!="BlueTooth Reception Sensitivity Test")
					{
						sqllimit = "select TESTCONTENT,TESTTYPE,CH,LIMITU,LIMITL from "+preTname+"_LIMIT where trim(TESTCONTENT)='"+this.DropDownList3.SelectedValue.Trim()+"' ORDER BY TESTTYPE,CH" ;
					
						Table = new DataTable();
						Table.Columns.Add("TestContent");
						Table.Columns.Add("TestType");
						Table.Columns.Add("CH");
						Table.Columns.Add("Min");
						Table.Columns.Add("Max");
						Table.Columns.Add("AVG");
						Table.Columns.Add("Sigma");
						Table.Columns.Add("CPK");
						try
						{
							conn.ExecuteAdapter(sqllimit).Fill(limittable);
							if(limittable==null)
								return;
							else
							{
								DataRow row=null;
								for(int i=0;i<limittable.Rows.Count;i++)
								{
									sqlsum="select B.VALUE from "+preTname+"_BIG B,"+preTname+"_SMALL S,(SELECT C.PRODUCTID,MAX(D.PDDATE) FPDDATE,MAX(C.DATETIME) DATETIME,MAX(C.TSTEP) TSTEP FROM "+preTname+"_BIG C,"+preTname+"_SMALL D where C.PRODUCTID=D.PRODUCTID AND C.TESTCONTENT='"+limittable.Rows[i]["TESTCONTENT"].ToString()+"' and C.TESTTYPE = '"+limittable.Rows[i]["TESTTYPE"].ToString()+"' and C.CH = '"+limittable.Rows[i]["CH"].ToString()+"'"+require+" GROUP BY C.PRODUCTID) F where F.PRODUCTID=B.PRODUCTID AND S.PRODUCTID=F.PRODUCTID AND S.PDDATE=F.FPDDATE AND B.DATETIME=F.DATETIME AND B.TSTEP=F.TSTEP AND B.TESTCONTENT= '"+limittable.Rows[i]["TESTCONTENT"].ToString()+"' and B.TESTTYPE = '"+limittable.Rows[i]["TESTTYPE"].ToString()+"' and CH = '"+limittable.Rows[i]["CH"].ToString()+"'"+require+" order by to_number(rtrim(B.VALUE,'%'))";
									orr=null;
									orr=conn.ExecuteReader(sqlsum);
									if(orr==null)
										return;
									else
									{
										values=new ArrayList();
										string sname=Server.UrlEncode(teststation+i.ToString());
										while(orr.Read())
										{
											float t=0.00f;
											if(orr[0].ToString().IndexOf("%")!=-1)
											{
												t=float.Parse(orr[0].ToString().Trim('%'))/100.0f;
											}
											else
												t=float.Parse(orr[0].ToString());
											values.Add(t);
										}
										orr.Close();
										if(values.Count>0)
										{
											ComUtility com=new ComUtility(values);

											max = com.CalMax();
											min = com.CalMin();
											sigma= com.CalSig();
											avg=com.CalAvg();
											row=Table.NewRow();
											row["TestType"]=limittable.Rows[i]["TESTTYPE"].ToString();
											row["TestContent"]=limittable.Rows[i]["TESTCONTENT"].ToString();
											row["CH"]=limittable.Rows[i]["CH"].ToString();
											row["Min"]=min.ToString();
											row["AVG"]=avg.ToString();
											row["Sigma"]=sigma.ToString();
											row["Max"]=max.ToString();
											//									row["CP"]=Convert.ToString((max-min)/(6*sigma));
											if(limittable.Rows[i]["LIMITU"].ToString().Trim().Length==0)
											{
												limitl=float.Parse(limittable.Rows[i]["LIMITL"].ToString());
												limitu=Math.E;
												//											row["CPK"]="<a href='drawimage.aspx?sname="+sname+"' target=_blank>"+(avg-limitl)/(3*sigma)+"</a>";
												values.Insert(1,null);
												values.Insert(0,limitl);
											}
											else if(limittable.Rows[i]["LIMITL"].ToString().Trim().Length==0)
											{
												limitu=float.Parse(limittable.Rows[i]["LIMITU"].ToString());
												limitl=Math.E;
												values.Insert(0,null);
												values.Insert(1,limitu);
												//											row["CPK"]="<a href='drawimage.aspx?sname="+sname+"' target=_blank>"+(limitu-avg)/(3*sigma)+"</a>";
											}
											else
											{
												limitu=float.Parse(limittable.Rows[i]["LIMITU"].ToString());
												limitl=float.Parse(limittable.Rows[i]["LIMITL"].ToString());
												values.Insert(0,limitl);
												values.Insert(1,limitu);
											}
											row["CPK"]="<a href='drawimage.aspx?sname="+sname+"' target=_blank>"+com.CalCPK(limitu,limitl,sigma,avg)+"</a>";
											Table.Rows.Add(row);
											values.Insert(2,min);
											values.Insert(3,max);
											values.Insert(4,avg);
											values.Insert(5,sigma);
											Session[sname]=values;
										}
									}
								}
					
							}
						}
						catch(Exception err)
						{
							orr.Close();
							string k= err.Message;
							return;
						}
					}
					else
					{
						sqllimit = "select TESTCONTENT,TESTTYPE,TXCH,RXCH,LIMITU,LIMITL from "+preTname+"_LIMIT where trim(TESTCONTENT)='"+this.DropDownList3.SelectedValue.Trim()+"' ORDER BY TESTTYPE,CH" ;
					
						Table = new DataTable();
						Table.Columns.Add("TestContent");
						Table.Columns.Add("TestType");
						Table.Columns.Add("TXCH");
						Table.Columns.Add("RXCH");
						Table.Columns.Add("Min");
						Table.Columns.Add("Max");
						Table.Columns.Add("AVG");
						Table.Columns.Add("Sigma");
						//				Table.Columns.Add("CP");
						Table.Columns.Add("CPK");
						try
						{
							conn.ExecuteAdapter(sqllimit).Fill(limittable);
							if(limittable==null)
								return;
							else
							{
								DataRow row=null;
								for(int i=0;i<limittable.Rows.Count;i++)
								{
									sqlsum="select B.VALUE from "+preTname+"_BIG B,"+preTname+"_SMALL S,(SELECT C.PRODUCTID,MAX(D.PDDATE) FPDDATE,MAX(C.DATETIME) DATETIME,MAX(C.TSTEP) TSTEP FROM "+preTname+"_BIG C,"+preTname+"_SMALL D where C.PRODUCTID=D.PRODUCTID AND C.TESTCONTENT='"+limittable.Rows[i]["TESTCONTENT"].ToString()+"' and C.TESTTYPE = '"+limittable.Rows[i]["TESTTYPE"].ToString()+"' and TXCH = '"+limittable.Rows[i]["TXCH"].ToString()+"' and RXCH = '"+limittable.Rows[i]["RXCH"].ToString()+"'"+require+" GROUP BY C.PRODUCTID) F where F.PRODUCTID=S.PRODUCTID AND B.PRODUCTID=F.PRODUCTID AND S.PDDATE=F.FPDDATE AND F.DATETIME=B.DATETIME AND B.TSTEP=F.TSTEP AND B.TESTCONTENT= '"+limittable.Rows[i]["TESTCONTENT"].ToString()+"' and B.TESTTYPE = '"+limittable.Rows[i]["TESTTYPE"].ToString()+"' and TXCH = '"+limittable.Rows[i]["TXCH"].ToString()+"' and RXCH = '"+limittable.Rows[i]["RXCH"].ToString()+"'"+require+" order by to_number(rtrim(B.VALUE,'%'))";
									orr=null;
									orr=conn.ExecuteReader(sqlsum);
									if(orr==null)
										return;
									else
									{
										values=new ArrayList();
										string sname=Server.UrlEncode(teststation+i.ToString());
										while(orr.Read())
										{
											float t=0.00f;
											if(orr[0].ToString().IndexOf("%")!=-1)
											{
												t=float.Parse(orr[0].ToString().Trim('%'))/100.0f;
											}
											else
												t=float.Parse(orr[0].ToString());
											values.Add(t);
										}
										orr.Close();
										if(values.Count>0)
										{
											ComUtility com=new ComUtility(values);

											max = com.CalMax();
											min = com.CalMin();
											sigma= com.CalSig();
											avg=com.CalAvg();
											row=Table.NewRow();
											row["TestType"]=limittable.Rows[i]["TESTTYPE"].ToString();
											row["TestContent"]=limittable.Rows[i]["TESTCONTENT"].ToString();
											row["TXCH"]=limittable.Rows[i]["TXCH"].ToString();
											row["RXCH"]=limittable.Rows[i]["RXCH"].ToString();
											row["Min"]=min.ToString();
											row["AVG"]=avg.ToString();
											row["Sigma"]=sigma.ToString();
											row["Max"]=max.ToString();
											//									row["CP"]=Convert.ToString((max-min)/(6*sigma));
											if(limittable.Rows[i]["LIMITU"].ToString().Trim().Length==0)
											{
												limitl=float.Parse(limittable.Rows[i]["LIMITL"].ToString());
												limitu=Math.E;
												//											row["CPK"]="<a href='drawimage.aspx?sname="+sname+"' target=_blank>"+(avg-limitl)/(3*sigma)+"</a>";
												values.Insert(1,null);
												values.Insert(0,limitl);
											}
											else if(limittable.Rows[i]["LIMITL"].ToString().Trim().Length==0)
											{
												limitu=float.Parse(limittable.Rows[i]["LIMITU"].ToString());
												limitl=Math.E;
												values.Insert(0,null);
												values.Insert(1,limitu);
												//											row["CPK"]="<a href='drawimage.aspx?sname="+sname+"' target=_blank>"+(limitu-avg)/(3*sigma)+"</a>";
											}
											else
											{
												limitu=float.Parse(limittable.Rows[i]["LIMITU"].ToString());
												limitl=float.Parse(limittable.Rows[i]["LIMITL"].ToString());
												values.Insert(0,limitl);
												values.Insert(1,limitu);
											}
											row["CPK"]="<a href='drawimage.aspx?sname="+sname+"' target=_blank>"+com.CalCPK(limitu,limitl,sigma,avg)+"</a>";
											Table.Rows.Add(row);
											values.Insert(2,min);
											values.Insert(3,max);
											values.Insert(4,avg);
											values.Insert(5,sigma);
											Session[sname]=values;
										}
									}
								}
					
							}
						}
						catch(Exception err)
						{
							orr.Close();
							string k= err.Message;
							return;
						}
					}
				}
				IsDone=true;
			}
			else
			{
				string errorquery = (String)GetGlobalResourceObject("ErrorQuery","SFCQuery");
				Response.Write("<script language=javascript>alert('"+errorquery+"')</script>");
			}

			if(Table!=null)
			{
				System.Data .DataView source = null;
				source = new DataView ( Table );

			
				dgcpk1.DataSource = source;
				dgcpk1.DataBind ();
			}
			else
			{
				string nodata = (String)GetGlobalResourceObject("NoData","SFCQuery");
				Response.Write("<script language=javascript>alert('"+nodata+"')</script>");
			}
		}

		[Ajax.AjaxMethod]
		public DataSet getTestStation(string prename)
		{
			DataSet ds= new DataSet();
			try
			{
//				CDData cd = new CDData(prename);
				string strSQL = @"select TESTSTATION_NAME as txt,TESTSTATION_ID as vol from "+prename.Trim().ToUpper()+@"_TESTSTATION order by TESTSTATION_NAME";
				
				conn.ExecuteAdapter(strSQL).Fill(ds);
//				DataTable dt = ds.Tables[0];
//				string kk=dt.Rows[0][1].ToString();
			}
			catch(Exception err)
			{
			    string k = err.Message;
			}
			return ds;
		}
		private void DropDownList2_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string tmpname = this.DropDownList1.SelectedValue.Trim().ToLower();
//			conn= new CDData(tmpname+"_lh",tmpname);`
			conn.DP2Bind(this.DropDownList2,this.DropDownList3,tmpname);
            this.DropDownList3.Items.Add((String)GetGlobalResourceObject("SFCQuery", "ForSelect"));
			this.DropDownList3.SelectedIndex=this.DropDownList3.Items.Count-1;
		}

		private void btnQUERY_Click(object sender, System.EventArgs e)
		{
			this.bindcpk();
			if(flag==false)
			{
				cm.setcount(this.Page);
			}
			flag=true;
		} 

		private void DropDownList3_SelectedIndexChanged(object sender, System.EventArgs e)
		{
//			CDData conn = new CDData(this.DropDownList1.SelectedItem.Text.Trim().ToLower());
			string tmpname="";
			if((this.DropDownList2.SelectedItem.Text.Trim()=="TB1")||(this.DropDownList2.SelectedItem.Text.Trim()=="TB2")||(this.DropDownList2.SelectedItem.Text.Trim()=="AB2")||(this.DropDownList2.SelectedItem.Text.Trim()=="AB3"))
			{
				tmpname=objname+".BASEBANDTEST_"+this.DropDownList2.SelectedItem.Text.Trim();
			}
			else if(((this.DropDownList2.SelectedItem.Text.Trim()=="PT")||(this.DropDownList2.SelectedItem.Text.Trim()=="EPT")||(this.DropDownList2.SelectedItem.Text.Trim()=="WL")))
			{
					if(this.DropDownList2.SelectedItem.Text.Trim()=="PT")
					{
						tmpname=objname+".PRETEST_PT";
					}
					else if(this.DropDownList2.SelectedItem.Text.Trim()=="EPT")
					{
						tmpname=objname+".EDGEPRETEST_EPT";
					}
					else
					{
						tmpname=objname+".WIRELESS_WL";
					}
			}
			else if(((this.DropDownList2.SelectedItem.Text.Trim()=="BT")||(this.DropDownList2.SelectedItem.Text.Trim()=="BTWL")))
			{
				if(this.DropDownList2.SelectedItem.Text.Trim()=="BT")
				{
					tmpname=objname+".BLUETOOTH_BT";
							
				}
				else
				{
					tmpname=objname+".BTWIRELESS_BTWL";
				}
			}
			string strSQL = "select distinct ONLINENAME from "+tmpname+"_SMALL";
			System.Data.OracleClient.OracleDataReader tmpr = conn.ExecuteReader(strSQL);
			try
			{
				if(tmpr==null)
					return;
				if(this.dpdLINE.Items.Count>0)
					this.dpdLINE.Items.Clear();
                this.dpdLINE.Items.Add((String)GetGlobalResourceObject("SFCQuery", "ForSelect"));
				this.dpdLINE.SelectedIndex=this.dpdLINE.Items.Count-1;
				while(tmpr.Read())
				{
					this.dpdLINE.Items.Add(tmpr[0].ToString().Trim());
				}
			}
			catch(Exception err)
			{
				string k = err.Message;
				return;
			}
			finally
			{
			    tmpr.Close();
			}
			strSQL = "select distinct COMPUTERNAME from "+tmpname+"_SMALL";
			tmpr = conn.ExecuteReader(strSQL);
			try
			{
				if(tmpr==null)
					return;
				if(this.dpdCOMPUTER.Items.Count>0)
					this.dpdCOMPUTER.Items.Clear();
                this.dpdCOMPUTER.Items.Add((String)GetGlobalResourceObject("SFCQuery", "ForSelect"));
				this.dpdCOMPUTER.SelectedIndex=this.dpdCOMPUTER.Items.Count-1;
				while(tmpr.Read())
				{
					this.dpdCOMPUTER.Items.Add(tmpr[0].ToString().Trim());
				}
			}
			catch(Exception err)
			{
				string k = err.Message;
				return;
			}
			finally
			{
				tmpr.Close();
			}
		}

		private void dpdLINE_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		private void btnOUTPUT_Click(object sender, System.EventArgs e)
		{
			if(IsDone==true)
			{
				cm.ExcelOut(this.Page,this.dgcpk1);
			}
			else
			{
				string notable = (String)GetGlobalResourceObject("NoTable","SFCQuery");
			    Response.Write("<script language=javascript>alert('"+notable+"')</script>");
			}
		}

		private void DropDownList1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.DropDownList1.SelectedIndex!=this.DropDownList1.Items.Count-1)
			{
				string tmpname = this.DropDownList1.SelectedValue.Trim();//.ToLower();
				//objname=tmpname+"_LH";
                objname = tmpname;
				conn= new CDData(objname,objname);
				conn.DP1Bind(this.DropDownList2,tmpname);
                this.DropDownList2.Items.Add((String)GetGlobalResourceObject("SFCQuery", "ForSelect"));
				this.DropDownList2.SelectedIndex=this.DropDownList2.Items.Count-1;
			}
		}

		private void lblacount_Load(object sender, System.EventArgs e)
		{
		
		}

//		private void dpdLINE_SelectedIndexChanged(object sender, System.EventArgs e)
//		{
//			string strSQL = "select distinct COMPUTERNAME from "+preTname+"_SMALL where ONLINENAME='"+this.dpdLINE.SelectedValue.Trim()+"'";
//			DataTable smalltable = new DataTable();
//			conn.ExecuteAdapter(strSQL).Fill(smalltable);
//			this.dpdCOMPUTER.DataSource=smalltable;
//			this.dpdCOMPUTER.DataTextField="COMPUTERNAME";
//			this.dpdCOMPUTER.DataValueField="COMPUTERNAME";
//			
//			this.dpdCOMPUTER.DataBind();
//			this.dpdCOMPUTER.Items.Insert(0,"请选择");
//		}

	}
}
