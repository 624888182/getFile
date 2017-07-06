using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using DBAccess.EAI;
using System.Reflection;
using System.Resources;
using System.Globalization;
using DB.EAI;
using System.Threading;
using ChartDirector;
using System.Data.OracleClient;

public partial class Boundary_WFrmLineStatusMonitor : System.Web.UI.UserControl
{
    protected string strLine;
    protected string strModel;

    protected void Page_Load(object sender, System.EventArgs e)
	{  
		// 在這裡放置使用者程式碼以初始化網頁

        strLine = Request.QueryString["Line"].ToString();
        strModel = Request.QueryString["Model"].ToString();

        show();
	}

	#region Web Form 設計工具產生的程式碼
	override protected void OnInit(EventArgs e)
	{
		//
		// CODEGEN: 此為 ASP.NET Web Form 設計工具所需的呼叫。
		//
		InitializeComponent();
		base.OnInit(e);
	}
	
	/// <summary>
	///		此為設計工具支援所必須的方法 - 請勿使用程式碼編輯器修改
	///		這個方法的內容。
	/// </summary>
	private void InitializeComponent()
	{

	}
	#endregion

    //private void MultiLanguage()
    //{ 
    //    lblLine.Text = (String)GetGlobalResourceObject("SFCQuery", "Line");//rm.GetString("Line");
    //    btnQuery.Text = (String)GetGlobalResourceObject("SFCQuery", "Query");//rm.GetString("Query");
    //    lblModel.Text = (String)GetGlobalResourceObject("SFCQuery", "Model"); 
    //}

	protected void show()
	{

        lbLine.Text = strLine;
        lbModel.Text = strModel;
        string strTime;
        string[] strTimeTep;
        strTimeTep=(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)).GetDateTimeFormats();
        strTime = strTimeTep[40];
        lbDateTime.Text = strTime;
        WebChartViewer1.Visible = true;

        DataTable dt = null;

        dt = this.GetData();

        string[] lblStation = new string[dt.Rows.Count];
        double[] FinalYield = new double[dt.Rows.Count];
        int rowCount = 0;

        DataRow[] dr = dt.Select("", "SortCol");
        foreach (DataRow rw in dr)
        {
            lblStation[rowCount] = rw[0].ToString();
            FinalYield[rowCount] = Convert.ToDouble(rw[5].ToString().Substring(0, rw[5].ToString().IndexOf("%")));
            rowCount++;
        }
        switch (0)
        {
            case 0:
                createLineChart(WebChartViewer1, lblStation,FinalYield, "Through Rate"); 
                break;
        }
	}

	private DataTable GetData()
	{  

        string strStartTime;
        string strEndTime;
        string[] strTempStart;
        string[] strTempEnd; 

        strTempEnd = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)).GetDateTimeFormats();
        strEndTime = strTempEnd[40];
        DateTime endtime = Convert.ToDateTime(strEndTime);
        DateTime starttime;
        if (DateTime.Now.Hour >= 8 && DateTime.Now.Hour <= 20)
        {
            strTempStart = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,8,0, 0)).GetDateTimeFormats();
            strStartTime = strTempStart[40];
            starttime = Convert.ToDateTime(strStartTime);
        }
        else
        {
            strTempStart = (new DateTime(DateTime.Now.AddDays(-1).Year, DateTime.Now.AddDays(-1).Month, DateTime.Now.AddDays(-1).Day,20,0, 0)).GetDateTimeFormats();
            strStartTime = strTempStart[40];
            starttime = Convert.ToDateTime(strStartTime);
        }        
        
		clsDBYieldData objYieldData = new clsDBYieldData();

		DataTable dt = objYieldData.ShowYield(strModel,starttime,endtime,strLine,false,0,"");
		dt.DefaultView.Sort = "SortCol";
		DataGrid1.DataSource = dt.DefaultView;
		DataGrid1.DataBind();
		return dt;
	}

    private void createLineChart(WebChartViewer viewer, string[] lblStation,double[] FinalYieldData, string strTitle)
    {
        // Create a XYChart object of size 600 x 300 pixels, with a light grey (eeeeee)
        // background, black border, 1 pixel 3D border effect and rounded corners.
        XYChart c = new XYChart(740, 260, 0xeeeeee, 0x000000, 1);
        c.setRoundedFrame();

        // Set the plotarea at (60, 60) and of size 520 x 200 pixels. Set background
        // color to white (ffffff) and border and grid colors to grey (cccccc)
        c.setPlotArea(60, 60, 630, 160, 0xffffff, -1, 0xcccccc, 0xccccccc);

        // Add a title to the chart using 15pts Times Bold Italic font, with a light blue
        // (ccccff) background and with glass lighting effects.
        c.addTitle(strTitle,
            "Times New Roman Bold Italic", 15).setBackground(0xccccff, 0x000000, Chart.glassEffect());

        // Add a legend box at (70, 32) (top of the plotarea) with 9pts Arial Bold font
        c.addLegend(70, 32, false, "Arial Bold", 9).setBackground(Chart.Transparent);

        // Add a line chart layer using the supplied data
        LineLayer layer = c.addLineLayer2();

        //ChartDirector.TextBox textbox = layer.addDataSet(FirstYieldData, 0xff0000, strTitle).setDataLabelStyle("Arial Bold Italic"); 
        // Set the data label font color for the  data set to yellow (0x0000FF)
        //textbox.setFontColor(0x0000ff);
        //Set the Node's Style
        //layer.getDataSet(0).setDataSymbol(Chart.DiamondShape, 11);

        if (FinalYieldData != null)
        {
            c.addTitle("Through Rate",
                "Times New Roman Bold Italic", 15).setBackground(0xccccff, 0x000000,
                Chart.glassEffect());
            ChartDirector.TextBox textbox = layer.addDataSet(FinalYieldData, 0x00ff00, "Through Rate").setDataLabelStyle("Arial Bold Italic");

            // Set the data label font color for the  data set to yellow (0x0000FF)
            textbox.setFontColor(0x00ff00);
            layer.getDataSet(0).setDataSymbol(Chart.CircleShape, 9);
        }
        layer.setLineWidth(3);

        c.xAxis().setLabels(lblStation);

        // Set the y axis title
        c.yAxis().setTitle("良率百分比(%)");

        // Set axes width to 2 pixels
        c.xAxis().setWidth(2);
        c.yAxis().setWidth(2);

        // Output the chart
        viewer.Image = c.makeWebImage(Chart.PNG);

        // Include tool tip for the chart
        viewer.ImageMap = c.getHTMLImageMap("", "",
            "title='{dataSetName}  for {xLabel} =  {value}%'");
    }  
}
