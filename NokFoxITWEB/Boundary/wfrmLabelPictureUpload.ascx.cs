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
using System.Data.OleDb;
using System.Data.OracleClient;
using System.IO;
using DBAccess.EAI;

public partial class Boundary_wfrmLabelPictureUpload : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BindPictureType();
        BindPictureTypeExist();
    }
    public void BindPictureType()
    {
        string sql = "SELECT distinct BARCODE_TYPE FROM SHP.CMCS_SFC_IMEIPRINT_FUNCTION order by BARCODE_TYPE ASC";
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(sql).Tables[0];
        if (dt.Rows.Count > 0)
        {
            DropDownList2.DataTextField = "BARCODE_TYPE";
            DropDownList2.DataValueField = "BARCODE_TYPE";
            DropDownList2.DataSource = dt.DefaultView;
            DropDownList2.DataBind();
        }
        DropDownList2.Items.Add("");
        DropDownList2.Items.FindByText("").Selected = true;
    }
    public void BindPictureTypeExist()
    {
        string sql = "SELECT distinct BARCODE_TYPE FROM SHP.CMCS_SFC_IMEIPRINT_FUNCTION where PICTURE_NAME is not null order by BARCODE_TYPE ASC";
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(sql).Tables[0];
        if (dt.Rows.Count > 0)
        {
            DropDownList1.DataTextField = "BARCODE_TYPE";
            DropDownList1.DataValueField = "BARCODE_TYPE";
            DropDownList1.DataSource = dt.DefaultView;
            DropDownList1.DataBind();
        }
        DropDownList1.Items.Add("");
        DropDownList1.Items.FindByText("").Selected = true;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (DropDownList2.SelectedItem.Text.Trim() == "")
        {
            Label1.Text = "提示：請選擇圖片類型！！";
            return;
        }
        if (FileUpload1.HasFile)
        {
            string fileContentType = FileUpload1.PostedFile.ContentType;
            if (fileContentType == "image/bmp" || fileContentType == "image/gif" || fileContentType == "image/pjpeg")
            {
                string name = FileUpload1.PostedFile.FileName; // 客户端文件路径

                FileInfo file = new FileInfo(name);
                string fileName = file.Name; // 文件名称
                string fileName_s = "s_" + file.Name; // 缩略图文件名称
                string fileName_sy = "sy_" + file.Name; // 水印图文件名称（文字）
                string fileName_syp = "syp_" + file.Name; // 水印图文件名称（图片）
                string webFilePath = Server.MapPath("~/file/" + fileName); // 服务器端文件路径
                string webFilePath_s = Server.MapPath("~/file/" + fileName_s);  // 服务器端缩略图路径
                string webFilePath_sy = Server.MapPath("~/file/" + fileName_sy); // 服务器端带水印图路径(文字)
                string webFilePath_syp = Server.MapPath("~/file/" + fileName_syp); // 服务器端带水印图路径(图片)
                string webFilePath_sypf = Server.MapPath("~/file/shuiyin.jpg"); // 服务器端水印图路径(图片)

                if (!File.Exists(webFilePath))
                {
                    try
                    {
                        FileUpload1.SaveAs(webFilePath); // 使用 SaveAs 方法保存文件

                        //  AddShuiYinWord(webFilePath, webFilePath_sy);
                        // AddShuiYinPic(webFilePath, webFilePath_syp, webFilePath_sypf);
                        MakeThumbnail(webFilePath, webFilePath_s, 130, 130, "Cut"); // 生成缩略图方法
                        //File.Delete(webFilePath);


                        OleDbConnection conn = new OleDbConnection();
                        conn.ConnectionString = "Provider=OraOLEDB.Oracle.1;Data Source=tysfc;user id=shp;password=shp";
                        conn.Open();
                        string cmd = "update shp.CMCS_SFC_IMEIPRINT_FUNCTION set PICTURE_NAME=:PICTURENAME,PICTURE=:PICTURE where BARCODE_TYPE='" + DropDownList2.SelectedItem.Text.Trim() + "'";

                        OleDbCommand sql = new OleDbCommand(cmd, conn);

                        HttpPostedFile UpFile = FileUpload1.PostedFile;
                        int FileLength = UpFile.ContentLength;


                        Byte[] FileByteArray = new byte[FileLength]; //图象文件临时储存Byte数组
                        Stream StreamObject = UpFile.InputStream;//建立数据流对像
                        StreamObject.Read(FileByteArray, 0, FileLength);//读取图象文件数据，FileByteArray为数据储存体，0为数据指针位置、FileLnegth为数据长度

                        sql.Parameters.Add(":PICTURENAME", OleDbType.VarChar, 50).Value = fileName; //记录文件类型
                        sql.Parameters.Add(":PICTURE", OleDbType.LongVarBinary, FileLength).Value = FileByteArray;

                        sql.ExecuteNonQuery();
                        conn.Close();
                        Label1.Text = "提示：文件“" + fileName + "”成功上传，并生成“" + fileName_s + "”缩略图，文件类型为：" + FileUpload1.PostedFile.ContentType + "，文件大小为：" + FileUpload1.PostedFile.ContentLength + "B";

                        BindPictureTypeExist();

                    }
                    catch (Exception ex)
                    {
                        Label1.Text = "提示：文件上传失败，失败原因：" + ex.Message;
                    }
                }
                else
                {
                    Label1.Text = "提示：文件已经存在，请重命名后上传";
                }
            }
            else
            {
                Label1.Text = "提示：文件类型不符";
            }
        }
    }
    public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode)
    {
        System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);

        int towidth = width;
        int toheight = height;

        int x = 0;
        int y = 0;
        int ow = originalImage.Width;
        int oh = originalImage.Height;

        switch (mode)
        {
            case "HW"://指定高宽缩放（可能变形） 
                break;
            case "W"://指定宽，高按比例 
                toheight = originalImage.Height * width / originalImage.Width;
                break;
            case "H"://指定高，宽按比例
                towidth = originalImage.Width * height / originalImage.Height;
                break;
            case "Cut"://指定高宽裁减（不变形） 
                if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                {
                    oh = originalImage.Height;
                    ow = originalImage.Height * towidth / toheight;
                    y = 0;
                    x = (originalImage.Width - ow) / 2;
                }
                else
                {
                    ow = originalImage.Width;
                    oh = originalImage.Width * height / towidth;
                    x = 0;
                    y = (originalImage.Height - oh) / 2;
                }
                break;
            default:
                break;
        }

        //新建一个bmp图片
        System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

        //新建一个画板
        System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);
        //设置高质量插值法
        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

        //设置高质量,低速度呈现平滑程度
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

        //清空画布并以透明背景色填充
        g.Clear(System.Drawing.Color.Transparent);

        //在指定位置并且按指定大小绘制原图片的指定部分
        g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, towidth, toheight),
        new System.Drawing.Rectangle(x, y, ow, oh),
        System.Drawing.GraphicsUnit.Pixel);

        try
        {
            //以jpg格式保存缩略图
            bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
        catch (System.Exception e)
        {
            throw e;
        }
        finally
        {
            originalImage.Dispose();
            bitmap.Dispose();
            g.Dispose();
        }
    }


    protected void AddShuiYinWord(string Path, string Path_sy)
    {
        string addText = "测试水印";
        System.Drawing.Image image = System.Drawing.Image.FromFile(Path);
        System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image);
        g.DrawImage(image, 0, 0, image.Width, image.Height);
        System.Drawing.Font f = new System.Drawing.Font("Verdana", 16);
        System.Drawing.Brush b = new System.Drawing.SolidBrush(System.Drawing.Color.Blue);

        g.DrawString(addText, f, b, 15, 15);
        g.Dispose();

        image.Save(Path_sy);
        image.Dispose();
    }

    /**/
    /// <summary>
    /// 在图片上生成图片水印
    /// </summary>
    /// <param name="Path">原服务器图片路径</param>
    /// <param name="Path_syp">生成的带图片水印的图片路径</param>
    /// <param name="Path_sypf">水印图片路径</param>
    protected void AddShuiYinPic(string Path, string Path_syp, string Path_sypf)
    {
        System.Drawing.Image image = System.Drawing.Image.FromFile(Path);
        System.Drawing.Image copyImage = System.Drawing.Image.FromFile(Path_sypf);
        System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image);
        g.DrawImage(copyImage, new System.Drawing.Rectangle(image.Width - copyImage.Width, image.Height - copyImage.Height, copyImage.Width, copyImage.Height), 0, 0, copyImage.Width, copyImage.Height, System.Drawing.GraphicsUnit.Pixel);
        g.Dispose();

        image.Save(Path_syp);
        image.Dispose();
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string sql = "SELECT distinct PICTURE_NAME FROM SHP.CMCS_SFC_IMEIPRINT_FUNCTION where BARCODE_TYPE='" + DropDownList1.SelectedItem.Text.Trim() + "'";
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(sql).Tables[0];
        if (dt.Rows.Count > 0)
        {
            Image1.ImageUrl = "~/file/" + dt.Rows[0][0];
        }
        else
        {
            Image1.ImageUrl = "";
        }
    }
}