using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web;
using System.Drawing;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.Security.Cryptography;
using System.Text;
using System.Data.OleDb;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;




namespace WebApplication_stored_procedure
{
    public partial class WebForm5 : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["spdb"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.BindGridView();
            }

            if (Session["USERNAME"] == null)
            {
                // Redirect to login if session is null (user is not logged in)
                Response.Redirect("Login.aspx");
            }
            else
            {
                // Set up cache control to prevent page from being cached
                Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetNoStore();
                Response.AddHeader("Cache-Control", "no-cache, no-store, must-revalidate, max-age=0");
                Response.AddHeader("Pragma", "no-cache");
                Response.AddHeader("Expires", "-1");

                if (!this.IsPostBack)
                {
                    // Bind GridView data if the page is not a postback
                    this.BindGridView();
                }
            }
        }



        /// <summary>
        /// this bind grid is for encrypted password
        /// </summary>
        private void BindGridView2()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand("SP_MANAGE_USERS", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTION", "SHOWUSERS");
                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        string decrypted = dr["PASSWORD"].ToString();
                        Decrypt(decrypted);
                    }
                    con.Close();
                    System.Data.DataTable dt = new System.Data.DataTable();
                    sda.Fill(dt);

                    gvspdb.DataSource = dt;
                    gvspdb.DataBind();
                }
            }
        }
        /// <summary>
        /// This -BindGridView is for decrypted passowd show on database
        /// </summary>
        private void BindGridView()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand("SP_MANAGE_USERS", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTION", "SHOWUSERS");
                    con.Open();

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    System.Data.DataTable dt = new System.Data.DataTable();
                    sda.Fill(dt);

                    // Loop through the rows and decrypt the password
                    foreach (DataRow row in dt.Rows)
                    {
                        string encryptedPassword = row["PASSWORD"].ToString();
                        string decryptedPassword = Decrypt(encryptedPassword);
                        row["PASSWORD"] = decryptedPassword;
                    }

                    gvspdb.DataSource = dt;
                    gvspdb.DataBind();
                }
            }
        }

        /// <summary>
        /// use fro displaying Image
        /// </summary>
        /// <param name="userID"> this parameter is geeting the id from so i can export Image</param>
        /// <returns>Image</returns>
        protected string GetImageUrl(int userID)
        {
            string imageUrl = string.Empty;
            string cs = ConfigurationManager.ConnectionStrings["spdb"].ConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_MANAGE_USERS", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", "READ");
                        cmd.Parameters.AddWithValue("@ID", userID);

                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                byte[] imageData = reader["IMG"] as byte[];
                                if (imageData != null)
                                {
                                    imageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(imageData);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library)
                // Example: LogError(ex);
                imageUrl = "data:image/png;base64," + Convert.ToBase64String(new byte[0]); // Optional: return a placeholder image
            }

            return imageUrl;
        }

        protected void gvspdb_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvspdb.EditIndex = -1;
            this.BindGridView();
        }
        /// <summary>
        /// This method is use for inseritng value in grid view from footer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvspdb_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Insert")
            {
                GridViewRow footerRow = gvspdb.FooterRow;
                string name = (footerRow.FindControl("txtNameFooter") as System.Web.UI.WebControls.TextBox).Text;
                string email = (footerRow.FindControl("txtEmailFooter") as System.Web.UI.WebControls.TextBox).Text;
                string password = (footerRow.FindControl("txtPasswordFooter") as System.Web.UI.WebControls.TextBox).Text;
                string mobile = (footerRow.FindControl("txtMobileFooter") as System.Web.UI.WebControls.TextBox).Text;
                string gender = (footerRow.FindControl("ddlGenderFooter") as DropDownList).SelectedValue;
                string usertype = (footerRow.FindControl("ddlUserTypeFooter") as DropDownList).SelectedValue;
                FileUpload fuImage = footerRow.FindControl("txtImageFooter") as FileUpload;
                byte[] imgData = null;
                if (fuImage.HasFile)
                {
                    string fileExtension = Path.GetExtension(fuImage.FileName).ToLower();
                    if (fileExtension == ".jpg" || fileExtension == ".png")
                    {
                        if (fuImage.PostedFile.ContentLength <= 1048576)
                        {

                            using (BinaryReader br = new BinaryReader(fuImage.PostedFile.InputStream))
                            {
                                imgData = br.ReadBytes(fuImage.PostedFile.ContentLength);

                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "FileSizeError", "alert('File size must be less than 1MB');", true);
                            return;
                        }
                    }
                    else
                    {
                        // Invalid file type
                        ScriptManager.RegisterStartupScript(this, GetType(), "FileTypeError", "alert('Only .jpg, .jpeg, or .png files are allowed');", true);
                        return;
                    }
                }
                try
                {
                    using (SqlConnection con = new SqlConnection(cs))
                    {
                        using (SqlCommand cmd = new SqlCommand("SP_MANAGE_USERS", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ACTION", "CREATE");
                            cmd.Parameters.AddWithValue("@NAME", name);
                            cmd.Parameters.AddWithValue("@EMAIL", email);
                            cmd.Parameters.AddWithValue("@PASSWORD", Encrypt(password));
                            cmd.Parameters.AddWithValue("@MOBILE", mobile);
                            cmd.Parameters.AddWithValue("@GENDER", gender);
                            cmd.Parameters.AddWithValue("@USERTYPE", usertype);
                            //cmd.Parameters.AddWithValue("@IMG", imgPath);

                            cmd.Parameters.AddWithValue("@IMG", imgData ?? (object)DBNull.Value);

                            con.Open();
                            int result = cmd.ExecuteNonQuery();
                            if (result > 0)
                            {
                                Response.Write("<script>alert('Data inserted successfully');</script>");
                                gvspdb.EditIndex = -1;
                                Response.Redirect(Request.RawUrl, false);
                            }
                            else
                            {
                                Response.Write("<script>alert('Insert failed');</script>");
                            }
                            con.Close();
                        }
                    }
                    this.BindGridView();
                }
                catch (Exception ex)
                {
                    string script = $"alert('Error: {ex.Message}')";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ErrorAlert", script, true);
                }
            }

        }


        private string getEncryptedPassword(int id)
        {
            string storedpassword = string.Empty;
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT PASSWORD FROM REG_TABLE WHERE ID = ID", con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    con.Open();
                    storedpassword = cmd.ExecuteScalar()?.ToString();
                    con.Close();
                }
            }
            return storedpassword;
        }
        protected void gvspdb_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(gvspdb.DataKeys[e.RowIndex].Value);
                string name = (gvspdb.Rows[e.RowIndex].FindControl("name") as System.Web.UI.WebControls.TextBox).Text;
                string email = (gvspdb.Rows[e.RowIndex].FindControl("email") as System.Web.UI.WebControls.TextBox).Text;
                string currentPassword = (gvspdb.Rows[e.RowIndex].FindControl("password") as System.Web.UI.WebControls.TextBox).Text;
                string dbpassword = getEncryptedPassword(id);
                string mobile = (gvspdb.Rows[e.RowIndex].FindControl("mobile") as System.Web.UI.WebControls.TextBox).Text;

                // Retrieve selected gender and user type
                string gender = (gvspdb.Rows[e.RowIndex].FindControl("ddlGenderEdit") as DropDownList).SelectedValue;
                string usertype = (gvspdb.Rows[e.RowIndex].FindControl("ddlUserTypeEdit") as DropDownList).SelectedValue;

                // Retrieve file upload control and hidden field
                FileUpload fuImage = gvspdb.Rows[e.RowIndex].FindControl("fuImage") as FileUpload;
                HiddenField hfImagePath = gvspdb.Rows[e.RowIndex].FindControl("hfImagePath") as HiddenField;

                byte[] imgData = null;
                if (fuImage.HasFile)
                {
                    using (BinaryReader br = new BinaryReader(fuImage.PostedFile.InputStream))
                    {
                        imgData = br.ReadBytes(fuImage.PostedFile.ContentLength);
                    }
                }
                else if (!string.IsNullOrEmpty(hfImagePath.Value))
                {
                    imgData = Convert.FromBase64String(hfImagePath.Value);  // Convert the Base64 string back to byte[]
                }

                // Add the image parameter



                string existingpassword = getEncryptedPassword(id);
                using (SqlConnection con = new SqlConnection(cs))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_MANAGE_USERS", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", "UPDATE");
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.Parameters.AddWithValue("@NAME", name);
                        cmd.Parameters.AddWithValue("@EMAIL", email);
                        if (currentPassword != dbpassword)
                        {
                            cmd.Parameters.AddWithValue("@PASSWORD", Encrypt(currentPassword));
                        }
                        else
                        {

                            cmd.Parameters.AddWithValue("@PASSWORD", currentPassword);

                        }



                        cmd.Parameters.AddWithValue("@MOBILE", mobile);
                        cmd.Parameters.AddWithValue("@GENDER", gender);
                        cmd.Parameters.AddWithValue("@USERTYPE", usertype);
                        //if (imgData != null)
                        //{
                        //    cmd.Parameters.AddWithValue("@IMG", imgData);
                        //}
                        cmd.Parameters.AddWithValue("@IMG", imgData ?? (object)DBNull.Value);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }

                gvspdb.EditIndex = -1;
                BindGridView();
            }
            catch (Exception ex)
            {
                string script = $"alert('Error: {ex.Message}');";
                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorAlert", script, true);
            }

        }

        protected void gvspdb_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gvspdb.DataKeys[e.RowIndex].Value);
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand("SP_MANAGE_USERS", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTION", "DELETE");
                    cmd.Parameters.AddWithValue("@ID", id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                }
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Data deleted successfully');", true);
            this.BindGridView();
        }

        protected void gvspdb_RowEditing(object sender, GridViewEditEventArgs e)
        {

            gvspdb.EditIndex = e.NewEditIndex;
            this.BindGridView();
        }

        public string Encrypt(string plainText)
        {
            string SecretKey = "$ASPcAwSNIgcPPEoTS78ODw#"; // Your encryption key
            byte[] secretBytes = Encoding.UTF8.GetBytes(SecretKey);

            using (Aes aes = Aes.Create())
            {
                aes.Key = secretBytes;
                aes.Mode = CipherMode.ECB;
                aes.Padding = PaddingMode.PKCS7;

                using (ICryptoTransform encryptor = aes.CreateEncryptor())
                {
                    byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                    byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
                    return Convert.ToBase64String(encryptedBytes);
                }
            }
        }

        public string Decrypt(string encryptedText)
        {
            string SecretKey = "$ASPcAwSNIgcPPEoTS78ODw#"; // Your encryption key
            byte[] secretBytes = Encoding.UTF8.GetBytes(SecretKey);
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = secretBytes;
                aes.Mode = CipherMode.ECB;
                aes.Padding = PaddingMode.PKCS7;

                using (ICryptoTransform decryptor = aes.CreateDecryptor())
                {
                    byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                    return Encoding.UTF8.GetString(decryptedBytes);
                }
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            // Do nothing
        }
        protected void btnPDF_Click(object sender, EventArgs e)
        {

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename = gridview.pdf");
            Response.Charset = "";
            Response.ContentType = "Application/pdf";

            gvspdb.AllowPaging = false;
            this.BindGridView2();
            using (MemoryStream ms = new MemoryStream())
            {
                using (Document doc = new Document(PageSize.A3, 10f, 10f, 10f, 0f))
                {
                    using (PdfWriter writer = PdfWriter.GetInstance(doc, ms))
                    {
                        doc.Open();
                        using (StringWriter sw = new StringWriter())
                        {
                            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                            {
                                //int i;
                                //for(i=0; i < gvspdb.Columns.Count; i++)
                                //{
                                //    if (gvspdb.Columns[i].HeaderText== "Actions")
                                //    {
                                //        gvspdb.Columns[i].Visible = false;
                                //        break;
                                //    }

                                //}

                                gvspdb.Columns[8].Visible = false;
                                gvspdb.Columns[9].Visible = false;
                                gvspdb.Columns[10].Visible = false;
                                gvspdb.Columns[11].Visible = false;
                                gvspdb.RenderControl(hw);
                            }
                            string htmlContent = sw.ToString();
                            XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, new StringReader(htmlContent));
                        }
                        doc.Close();
                    }
                }
                // convert the input stream to the byte array
                //byte[] bytes = ms.ToArray();
                // write the input data to memory stram
                Response.OutputStream.Write(ms.ToArray(), 0, ms.ToArray().Length);
                Response.Flush();
                Response.End();
            }
        }


        protected void btnExcel_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/octet-stream";

            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                gvspdb.AllowPaging = false;
                this.BindGridView2();

                gvspdb.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in gvspdb.HeaderRow.Cells)
                {
                    cell.BackColor = gvspdb.HeaderStyle.BackColor;
                }

                foreach (GridViewRow row in gvspdb.Rows)
                {
                    row.BackColor = Color.White;
                    row.Height = Unit.Pixel(110);
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        TableCell cell = row.Cells[i];

                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = gvspdb.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = gvspdb.RowStyle.BackColor;
                        }

                        // Check if this is the cell that should contain the image
                        if (gvspdb.Columns[i].HeaderText == "Image")
                        {
                            System.Web.UI.WebControls.Image imgControl = cell.FindControl("imgUser") as System.Web.UI.WebControls.Image;
                            if (imgControl != null && !string.IsNullOrEmpty(imgControl.ImageUrl))
                            {
                                // Set the display property to inline-block to help with horizontal alignment
                                string imgTag = $"<img src='{imgControl.ImageUrl}' width='100' height='100' style='display: inline-block; vertical-align: middle;' />";
                                cell.Text = imgTag;
                            }
                        }

                        cell.CssClass = "textmode";
                    }
                }
                gvspdb.Columns[1].Visible = false;

                gvspdb.RenderControl(hw);

                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }


        protected void btnLogout_Click(object sender, EventArgs e)
        {

            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut();


            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetNoStore();

            Response.Redirect("Login.aspx");
        }

        protected void btnOwnDetails_Click(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
            {
                Response.Write("<script>alert('No user is logged in');</script>");
                return;
            }
            int userid = (int)Session["USERID"];

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=adminDetails.pdf");
            Response.Charset = "";
            Response.ContentType = "application/pdf";
            BindSingleDetails(userid);
            try
            {
                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                    {

                        gvspdb.Columns[8].Visible = false;
                        gvspdb.Columns[9].Visible = false;
                        gvspdb.Columns[10].Visible = false;
                        gvspdb.RenderControl(hw);
                        using (StringReader sr = new StringReader(sw.ToString()))
                        {
                            using (Document doc = new Document(PageSize.A4, 10f, 10f, 10, 0))
                            {
                                PdfWriter writer = PdfWriter.GetInstance(doc, Response.OutputStream);
                                doc.Open();
                                XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, sr);
                                doc.Close();


                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                Response.Write("<script>alert('error :" + Ex.Message + "');</script>;");
            }

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        private void BindSingleDetails(int userid)
        {

            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand("SP_MANAGE_USERS", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTION", "GETUSERDETAILS");
                    cmd.Parameters.AddWithValue("@ID", userid);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    System.Data.DataTable dt = new System.Data.DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        gvspdb.DataSource = dt;
                        gvspdb.DataBind();
                    }
                    else
                    {
                        throw new Exception("no user details found");
                    }
                }
            }
        }

        protected void gvspdb_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //fetch the current Url
                Uri uri = Request.Url;
                string applicationUrl = string.Format("{0}://{1}{2}", uri.Scheme, uri.Authority, uri.AbsolutePath);
                applicationUrl = applicationUrl.Replace(Request.Url.Segments[Request.Url.Segments.Length - 1], "");

                //fetch th image id from the datakeys name property
                int id = Convert.ToInt32(gvspdb.DataKeys[e.Row.RowIndex].Values[0]);
                string imageUrl = String.Format("{0}Handler1.ashx?id={1}", applicationUrl, id, DateTime.Now.Ticks);

                var imgControl = e.Row.FindControl("imgUser") as System.Web.UI.WebControls.Image;
                if (imgControl != null)
                {
                    imgControl.ImageUrl = imageUrl;
                    imgControl.CssClass = "custom-image";
                    imgControl.Width = Unit.Pixel(100);
                    imgControl.Height = Unit.Pixel(100);

                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Image control 'imgUser' not found in the GridView row.");
                }



            }
        }

        public static System.Data.DataTable ReadExcelRange(Range excelRange)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            DataRow row;
            int rowCount = excelRange.Rows.Count;
            int colCount = excelRange.Columns.Count;
            //first row as header
            for (int j = 1; j <= colCount; j++)
            {
                dt.Columns.Add(excelRange.Cells[1, j].Value2?.ToString() ?? $"Column{j}");
            }

            // data start from second row
            for (int i = 2; i <= rowCount; i++)
            {
                row = dt.NewRow();
                for (int j = 1; j <= colCount; j++)
                {
                    row[j - 1] = excelRange.Cells[i, j].Value2?.ToString() ?? "";
                }
                dt.Rows.Add(row);
            }

            return dt;
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (fileUploadExcel.HasFile)
            {
                string filePath = Server.MapPath("~/fileupload/" + fileUploadExcel.FileName);
                fileUploadExcel.SaveAs(filePath);

                try
                {
                    // Create Excel application object
                    Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                    Workbook excelWorkbook = excelApp.Workbooks.Open(filePath);
                    Worksheet excelWorksheet = excelWorkbook.Sheets[1];
                    Range excelRange = excelWorksheet.UsedRange;

                    // Read data from Excel
                    System.Data.DataTable newData = ReadExcelRange(excelRange);

                    // Merge new data with the existing data in GridView
                    System.Data.DataTable existingData = (System.Data.DataTable)gvspdb2.DataSource;
                    if (existingData != null)
                    {
                        existingData.Merge(newData);
                    }
                    else
                    {
                        existingData = newData;
                    }

                    // Rebind updated data to the GridView
                    gvspdb2.DataSource = existingData;
                    gvspdb2.DataBind();

                    // Clean up Excel objects
                    excelWorkbook.Close();
                    Marshal.ReleaseComObject(excelRange);
                    Marshal.ReleaseComObject(excelWorksheet);
                    Marshal.ReleaseComObject(excelWorkbook);
                    excelApp.Quit();
                    Marshal.ReleaseComObject(excelApp);
                }
                catch (Exception ex)
                {
                    Response.Write("Error: " + ex.Message);
                }
            }
            else
            {
                Response.Write("Please upload a file.");
            }
        }
    }
}
