using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.InteropServices;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
using Microsoft.Office.Interop.Excel;

namespace WebApplication_stored_procedure
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["spdb"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridView();
            }
        }

        private void BindGridView()
        {
            using (SqlConnection con = new SqlConnection(cs)) {
                using (SqlCommand cmd = new SqlCommand("Select * from uplaodPdf",con)) {
                    using(SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        System.Data.DataTable dt = new System.Data.DataTable();
                        sda.Fill(dt);
                        gvspdb.DataSource = dt;
                        gvspdb.DataBind();
                    }
                }
            }
        }

        // Method to import an Excel file using ClosedXML
        //protected void btnImportClosedXML_Click(object sender, EventArgs e)
        //{
        //    if (fileUploadExcel.HasFile)
        //    {
        //        try
        //        {
        //            // Load Excel file using ClosedXML
        //            using (XLWorkbook workbook = new XLWorkbook(fileUploadExcel.PostedFile.InputStream))
        //            {
        //                IXLWorksheet sheet = workbook.Worksheet(1);
        //                System.Data.DataTable dt = new System.Data.DataTable();
        //                bool firstRow = true;

        //                foreach (IXLRow row in sheet.Rows())
        //                {
        //                    if (firstRow)
        //                    {
        //                        foreach (IXLCell cell in row.Cells())
        //                        {
        //                            dt.Columns.Add(cell.Value.ToString()); // Add columns
        //                        }
        //                        firstRow = false;
        //                    }
        //                    else
        //                    {
        //                        dt.Rows.Add(); // Add rows
        //                        int i = 0;
        //                        foreach (IXLCell cell in row.Cells())
        //                        {
        //                            dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString(); // Fill data
        //                            i++;
        //                        }
        //                    }
        //                }

        //                // Bind DataTable to GridView
        //                GridView1.DataSource = dt;
        //                GridView1.DataBind();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Response.Write("Error: " + ex.Message);
        //        }
        //    }
        //    else
        //    {
        //        Response.Write("Please upload a file.");
        //    }
        //}




        // Method to read Excel data using Microsoft.Office.Interop.Excel
        //public static System.Data.DataTable ReadExcelRange(Range excelRange)
        //{
        //    System.Data.DataTable dt = new System.Data.DataTable();
        //    DataRow row;
        //    int rowCount = excelRange.Rows.Count;
        //    int colCount = excelRange.Columns.Count;

        //    // Adding columns (assuming the first row has headers)
        //    for (int j = 1; j <= colCount; j++)
        //    {
        //        dt.Columns.Add(excelRange.Cells[1, j].Value2?.ToString() ?? $"Column{j}");
        //    }

        //    // Adding rows starting from the second row (as first row contains headers)
        //    for (int i = 2; i <= rowCount; i++)
        //    {
        //        row = dt.NewRow();
        //        for (int j = 1; j <= colCount; j++)
        //        {
        //            row[j - 1] = excelRange.Cells[i, j].Value2?.ToString() ?? "";
        //        }
        //        dt.Rows.Add(row);
        //    }

        //    return dt;
        //}

        // Method to handle Excel file upload using Microsoft.Office.Interop.Excel
        //protected void btnUploadInterop_Click(object sender, EventArgs e)
        //{
        //    if (fileUploadExcel.HasFile)
        //    {
        //        string filePath = Server.MapPath("~/fileupload/" + fileUploadExcel.FileName);
        //        fileUploadExcel.SaveAs(filePath);

        //        try
        //        {
        //            // Create Excel application object
        //            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
        //            Workbook excelWorkbook = excelApp.Workbooks.Open(filePath);
        //            Worksheet excelWorksheet = excelWorkbook.Sheets[1];
        //            Range excelRange = excelWorksheet.UsedRange;

        //            // Read data and bind to GridView
        //            GridView1.DataSource = ReadExcelRange(excelRange);
        //            GridView1.DataBind();

        //            // Clean up Excel objects
        //            excelWorkbook.Close();
        //            Marshal.ReleaseComObject(excelRange);
        //            Marshal.ReleaseComObject(excelWorksheet);
        //            Marshal.ReleaseComObject(excelWorkbook);
        //            excelApp.Quit();
        //            Marshal.ReleaseComObject(excelApp);
        //        }
        //        catch (Exception ex)
        //        {
        //            Response.Write("Error: " + ex.Message);
        //        }
        //    }
        //    else
        //    {
        //        Response.Write("Please upload a file.");
        //    }
        //}

        protected void btnupload_Click(object sender, EventArgs e)
        {
            if (fuPdf.HasFile)
            {
                string fileExtension = Path.GetExtension(fuPdf.FileName).ToLower();
                if (fileExtension != ".pdf")
                {
                    lblmessage.Text = "Only PDF files are allowed.";
                    return;
                }

                try
                {
                    string filename = Path.GetFileName(fuPdf.PostedFile.FileName);
                    string filePath = "~/fileupload/" + filename;
                    fuPdf.SaveAs(Server.MapPath(filePath));

                    using (SqlConnection con = new SqlConnection(cs))
                    {
                        using (SqlCommand cmd = new SqlCommand("Insert into uplaodPdf (nmae,pdfpath) values(@name,@pdfpath)", con))
                        {
                            cmd.Parameters.AddWithValue("@name", Txtname.Text);
                            cmd.Parameters.AddWithValue("@pdfpath", filePath);
                            con.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }

                    lblmessage.Text = "PDF uploaded successfully.";
                    BindGridView(); 
                }
                catch (Exception ex)
                {
                    lblmessage.Text = "Error: " + ex.Message;
                }
            }
            else
            {
                lblmessage.Text = "Please upload a pdf";
            }
        }

    }
}
