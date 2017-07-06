using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OracleClient;
using DBAccess.EAI;
using ChartDirector;
using DB.EAI;

public partial class Boundary_WFrmRetestRateWeeklyReport : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MultiLanguage();
            BindModelAndStation();
            tbStartDate.DateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd") + " 08:00";
            tbEndDate.DateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            if (tbStartDate.DateTextBox.Text.CompareTo(tbEndDate.DateTextBox.Text) > 0)
                tbEndDate.DateTextBox.Text = tbStartDate.DateTextBox.Text;
        }
        btnQuery.Attributes["onclick"] = "go();";
    }

    private void MultiLanguage()
    {
        //modified by shujianbo at 2007/11/28  upgrade to vs 2005
        lblStartDate.Text = (String)GetGlobalResourceObject("SFCQuery", "DateFrom");//rm.GetString("DateFrom");
        lblEndDate.Text = (String)GetGlobalResourceObject("SFCQuery", "DateTo");//rm.GetString("DateTo");
        lblLine.Text = (String)GetGlobalResourceObject("SFCQuery", "Line");//rm.GetString("Line");
        btnQuery.Text = (String)GetGlobalResourceObject("SFCQuery", "Query");//rm.GetString("Query");
        lblModel.Text = (String)GetGlobalResourceObject("SFCQuery", "Model");
        ViewState["ErrorDate"] = (String)GetGlobalResourceObject("SFCQuery", "ErrorDateTime");//rm.GetString("ErrorDateTime");
        ViewState["WONotExist"] = (String)GetGlobalResourceObject("SFCQuery", "WONotExist");//rm.GetString("WONotExist");
    }

    private void BindModelAndStation()
    {
        //string StrSql = "SELECT DISTINCT MODEL FROM CVT.MES_DUMP_MODEL";
        //DataTable dt = ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0];
        //ddlModel.DataTextField = "Model";
        //ddlModel.DataValueField = "Model";
        //ddlModel.DataSource = dt.DefaultView;
        //ddlModel.DataBind();

        string strProcedureName = "SFCQUERY.GETMODELNAME";
        OracleParameter[] orapara = new OracleParameter[] { new OracleParameter("DATA", OracleType.Cursor) };
        orapara[0].Direction = ParameterDirection.Output;
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(strProcedureName, orapara).Tables[0];
        ddlModel.DataTextField = "MODEL";
        ddlModel.DataValueField = "MODEL";
        ddlModel.DataSource = dt.DefaultView;
        ddlModel.DataBind();

        strProcedureName = "SFCQUERY.GETSTATION";
        dt = ClsGlobal.objDataConnect.DataQuery(strProcedureName, orapara).Tables[0];
        ddlStationID.DataTextField = "DESCRIPTION";
        ddlStationID.DataValueField = "STATION_ID";
        ddlStationID.DataSource = dt.DefaultView;
        ddlStationID.DataBind();
        ddlStationID.Items.Insert(0, "");
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        if (!ClsCommon.CheckIsDateTime(tbStartDate.DateTextBox.Text.Trim()))
        {
            Label28.Text = ViewState["ErrorDate"].ToString();
            Label28.Visible = true;
            Label29.Visible = false;
            return;
        }
        if (!ClsCommon.CheckIsDateTime(tbEndDate.DateTextBox.Text.Trim()))
        {
            Label29.Text = ViewState["ErrorDate"].ToString();
            Label29.Visible = true;
            Label28.Visible = false;
            return;
        }

        Label28.Visible = false;
        Label29.Visible = false;
        WebChartViewer1.Visible = true;
        WebChartViewer2.Visible = true;
        string NowDate = DateTime.Now.ToString("HH:mm");
        //臨時取消 modified by shujianbo at 2008/02/21
        //if ((NowDate.CompareTo("08:20") >0 || NowDate.CompareTo("07:30") < 0) || (DateTime.Now.DayOfWeek != DayOfWeek.Tuesday) )
        //{
        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "DateTime", "<script language=javascript>alert('請在每周星期二的07:30--08:20期間運行此功能！');</script>");
        //    return;
        //}

        System.TimeSpan intday = Convert.ToDateTime(tbEndDate.DateTextBox.Text.Trim()) - Convert.ToDateTime(tbStartDate.DateTextBox.Text);
        if (intday.TotalDays > 7)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "DateTime", "<script language=javascript>alert('選取的時間不能大於一個星期！');</script>");
            return;
        }

        DataTable dt = null;

        lock (o)
        {
            //System.Threading.Thread.Sleep(30000);
            dt = this.GetData();
        }
        string[] lblStation = new string[dt.Rows.Count];
        double[] FirstYield = new double[dt.Rows.Count];
        double[] FinalYield = new double[dt.Rows.Count];
        double[] RetestRate = new double[dt.Rows.Count];
        int rowCount = 0;

        DataRow[] dr = dt.Select("", "SortCol");
        foreach (DataRow rw in dr)
        {
            lblStation[rowCount] = rw[0].ToString();
            FirstYield[rowCount] = Convert.ToDouble(rw[3].ToString().Substring(0, rw[3].ToString().IndexOf("%")));
            FinalYield[rowCount] = Convert.ToDouble(rw[5].ToString().Substring(0, rw[5].ToString().IndexOf("%")));
            RetestRate[rowCount] = Convert.ToDouble(rw[12].ToString().Substring(0, rw[12].ToString().IndexOf("%")));
            rowCount++;
        }
        switch (rblChartType.SelectedIndex)
        {
            case 0:
                createLineChart(WebChartViewer1, lblStation, FirstYield, FinalYield, "First Yield");
                createLineChart(WebChartViewer2, lblStation, RetestRate, null, "Retest Rate");
                break;
            case 1:
                createBarChart(WebChartViewer1, lblStation, FirstYield, FinalYield, "First Yield");
                createBarChart(WebChartViewer2, lblStation, RetestRate, null, "Retest Rate");
                break;
        }
    }

    private object o = new object();
    private DataTable GetData()
    {
        //ADD BY SHUJIANBO AT 2007/08/24
        DateTime dttmStartDate = Convert.ToDateTime(tbStartDate.DateTextBox.Text);
        DateTime dttmEndDate = Convert.ToDateTime(tbEndDate.DateTextBox.Text);
        string strModel = ddlModel.SelectedValue.ToUpper();
        string strLine = ddlLine.SelectedValue.ToUpper();
        bool blRepair = ckbRepair.Checked;
        int intWOType = rblWOType.SelectedIndex;
        string strStationID = ddlStationID.SelectedValue;
        clsDBYieldData objYieldData = new clsDBYieldData();
        DataTable dt = objYieldData.ShowYield(strModel, dttmStartDate, dttmEndDate, strLine, blRepair, intWOType,strStationID);
        dt.DefaultView.Sort = "SortCol";
        DataGrid1.DataSource = dt.DefaultView;
        DataGrid1.DataBind();
        return dt;
    }

    private void createLineChart(WebChartViewer viewer, string[] lblStation, double[] FirstYieldData, double[] FinalYieldData, string strTitle)
    //double[] software, double[] hardware, double[] services)
    {
        // Create a XYChart object of size 600 x 300 pixels, with a light grey (eeeeee)
        // background, black border, 1 pixel 3D border effect and rounded corners.
        XYChart c = new XYChart(600, 300, 0xeeeeee, 0x000000, 1);
        c.setRoundedFrame();

        // Set the plotarea at (60, 60) and of size 520 x 200 pixels. Set background
        // color to white (ffffff) and border and grid colors to grey (cccccc)
        c.setPlotArea(60, 60, 520, 200, 0xffffff, -1, 0xcccccc, 0xccccccc);

        // Add a title to the chart using 15pts Times Bold Italic font, with a light blue
        // (ccccff) background and with glass lighting effects.
        c.addTitle(strTitle,
            "Times New Roman Bold Italic", 15).setBackground(0xccccff, 0x000000,
            Chart.glassEffect());

        // Add a legend box at (70, 32) (top of the plotarea) with 9pts Arial Bold font
        c.addLegend(70, 32, false, "Arial Bold", 9).setBackground(Chart.Transparent);

        // Add a line chart layer using the supplied data
        LineLayer layer = c.addLineLayer2();

        ChartDirector.TextBox textbox = layer.addDataSet(FirstYieldData, 0xff0000, strTitle).setDataLabelStyle("Arial Bold Italic");

        // Set the data label font color for the  data set to yellow (0x0000FF)
        textbox.setFontColor(0x0000ff);
        //Set the Node's Style
        layer.getDataSet(0).setDataSymbol(Chart.DiamondShape, 11);

        if (FinalYieldData != null)
        {
            c.addTitle("First Yield And Final Yield",
                "Times New Roman Bold Italic", 15).setBackground(0xccccff, 0x000000,
                Chart.glassEffect());
            textbox = layer.addDataSet(FinalYieldData, 0x00ff00, "Final Yield").setDataLabelStyle("Arial Bold Italic");

            // Set the data label font color for the  data set to yellow (0x0000FF)
            textbox.setFontColor(0x00ff00);
            layer.getDataSet(1).setDataSymbol(Chart.CircleShape, 9);
        }
        //			layer.addDataSet(hardware, 0x00ff00, "Hardware").setDataSymbol(
        //				Chart.DiamondShape, 11);
        //			layer.addDataSet(services, 0xffaa00, "Services").setDataSymbol(Chart.Cross2Shape(
        //				), 11);

        // Set the line width to 3 pixels
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

    // private void createChart(WebChartViewer viewer, string[] XLabel, double[] Values, string strTitle)
    private void createBarChart(WebChartViewer viewer, string[] lblStation, double[] FirstYieldData, double[] FinalYieldData, string strTitle)
    //double[] software, double[] hardware, double[] services)
    {
        // Create a XYChart object of size 600 x 300 pixels, with a light grey (eeeeee)
        // background, black border, 1 pixel 3D border effect and rounded corners.
        XYChart c = new XYChart(600, 300, 0xeeeeee, 0x000000, 1);
        c.setRoundedFrame();

        // Set the plotarea at (60, 60) and of size 520 x 200 pixels. Set background
        // color to white (ffffff) and border and grid colors to grey (cccccc)
        //c.setPlotArea(60, 60, 520, 200, c.linearGradientColor(60, 40, 60, 280, 0xeeeeff, 0x0000cc), -1, 0xffffff, 0xffffff);// 0xcccccc, 0xccccccc);
        c.setPlotArea(60, 60, 520, 200, 0xffffff, -1, 0xcccccc, 0xccccccc);

        // Add a title to the chart using 15pts Times Bold Italic font, with a light blue
        // (ccccff) background and with glass lighting effects.
        c.addTitle(strTitle,
            "Times New Roman Bold Italic", 15).setBackground(0xccccff, 0x000000,
            Chart.glassEffect());

        // Add a legend box at (70, 32) (top of the plotarea) with 9pts Arial Bold font
        c.addLegend(70, 32, false, "Arial Bold", 9).setBackground(Chart.Transparent);

        // Add a line chart layer using the supplied data
        //LineLayer layer = c.addLineLayer2();
        //BarLayer layer = c.addBarLayer3(Values);

        // Add a multi-color bar chart layer using the supplied data
        BarLayer layer = c.addBarLayer2(Chart.Side, 3);


        //ChartDirector.TextBox textbox = 
        layer.addDataSet(FirstYieldData, 0x80ff80, strTitle).setDataLabelStyle("Arial Bold Italic");
        if (FinalYieldData != null)
            layer.addDataSet(FinalYieldData, 0x8080ff, "Final Yield").setDataLabelStyle("Arial Bold Italic");


        //Set to 3D
        //layer.set3D();

        // Set the data label font color for the  data set to yellow (0x0000FF)
        //textbox.setFontColor(0x0000ff);
        //Set the Node's Style
        layer.getDataSet(0).setDataSymbol(Chart.DiamondShape, 11);

        // Use glass lighting effect with light direction from the left
        layer.setBorderColor(Chart.Transparent, Chart.glassEffect(Chart.NormalGlare,
            Chart.Left));


        // Set the line width to 3 pixels
        layer.setLineWidth(3);

        //c.xAxis().
        c.xAxis().setLabels(lblStation);

        // Set the y axis title
        c.yAxis().setTitle(strTitle);

        // Set the labels on the x axis. Rotate the labels by 60 degrees.
        c.xAxis().setLabels(lblStation).setFontAngle(345);

        // Set axes width to 2 pixels
        c.xAxis().setWidth(2);
        c.yAxis().setWidth(2);

        // Set all axes to transparent
        c.xAxis().setColors(Chart.Transparent);
        c.yAxis().setColors(Chart.Transparent);
        c.yAxis2().setColors(Chart.Transparent);


        // Output the chart
        viewer.Image = c.makeWebImage(Chart.PNG);

        // Include tool tip for the chart
        viewer.ImageMap = c.getHTMLImageMap("", "",
            "title='{dataSetName}  for {xLabel} =  {value}%'");
    }
}
