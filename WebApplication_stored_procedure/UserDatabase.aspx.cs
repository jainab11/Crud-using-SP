using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.Xml.Schema;
namespace WebApplication_stored_procedure
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["spdb"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {


            if (Session["USERID"] == null)
            {
                Response.Redirect("Login.aspx", true);
            }

            if (!IsPostBack)
            {
                BindGridView();
            }
        }

        private void BindGridView()
        {
            if (Session["USERID"] != null)
            {
                int Userid = (int)Session["USERID"];

                using (SqlConnection con = new SqlConnection(cs))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_MANAGE_USERS", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", "GETUSERDETAILS");
                        cmd.Parameters.AddWithValue("ID", Userid);

                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            gvspdb.DataSource = dt;
                            gvspdb.DataBind();
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "NoData", "alert('No user data found.');", true);
                        }
                    }
                }
            }
        }

        protected void EditButton_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "key", "launchModal();", true);
        }

        protected void ChangePasswordButton_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "key", "launchModal2();", true);
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand("SP_MANAGE_USERS", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTION", "EDITDETAILS");
                    cmd.Parameters.AddWithValue("@NAME", txtName.Text);
                    cmd.Parameters.AddWithValue("@EMAIL", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@MOBILE", txtMobile.Text);
                    cmd.Parameters.AddWithValue("@GENDER", GenderDropDownList.SelectedValue);

                    con.Open();
                    int ro = cmd.ExecuteNonQuery();
                    con.Close();

                    if (ro > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Update Alert", "alert('Data updated');", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Update Alert", "alert('No data found for the given Name');", true);
                    }

                    this.BindGridView();
                }
            }
        }

        protected void savePasswordButton_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand("SP_MANAGE_USERS", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTION", "CHANGEPASSWORD");
                    cmd.Parameters.AddWithValue("@NAME", txtNamepass.Text);
                    cmd.Parameters.AddWithValue("@OLDPASSWORD", txtoldpass.Text);
                    cmd.Parameters.AddWithValue("@NEWPASSWORD", txtnewPass.Text);

                    con.Open();
                    int ro = cmd.ExecuteNonQuery();
                    con.Close();


                    if (ro > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "passwordChangeAlert", "alert('Password changed successfully');", true);
                    }

                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "passwordChangeAlert", "alert('Password change failed');", true);
                    }

                }
            }
            this.BindGridView();
        }

        protected void btnlogout_Click(object sender, EventArgs e)
        {

            Session.Clear();
            Session.Abandon();

            if (Request.Cookies["ASP.NET_SessionId"] != null)
            {
                Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddDays(-1);
            }


            FormsAuthentication.SignOut();


            Response.Redirect("Login.aspx", true);
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected void btnPdfExport_Click(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
            {
                Response.Write("<script>alert('Please log in to download your details.');</script>");
                return;
            }

            int userId = (int)Session["USERID"];

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=UserDetails.pdf");
            Response.Charset = "";
            Response.ContentType = "application/pdf";

            
            BindUserDetailsToGridView(userId);
            try
            {
                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                    {
                        gvspdb.RenderControl(hw); // Render the GridView to HTML

                        using (StringReader sr = new StringReader(sw.ToString()))
                        {
                            using (Document doc = new Document(PageSize.A4, 10f, 10f, 10f, 0f))
                            {
                                PdfWriter writer = PdfWriter.GetInstance(doc, Response.OutputStream);
                                doc.Open();

                                // Parse the HTML into the PDF document
                                XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, sr);
                                doc.Close();
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('Error generating PDF: " + ex.Message + "');</script>");
            }

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        private void BindUserDetailsToGridView(int userId)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand("SP_MANAGE_USERS", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTION", "GETUSERDETAILS");
                    cmd.Parameters.AddWithValue("@ID", userId);

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        gvspdb.DataSource = dt;
                        gvspdb.DataBind();
                    }
                    else
                    {
                        
                        throw new Exception("No user details found.");
                    }
                }
            }
        }
        //protected void btnPdfExport_Click(object sender, EventArgs e)
        //{
        //    Response.Clear();
        //    Response.Buffer = true;
        //    Response.AddHeader("content-disposition", "attachment; filename=UserDetails.pdf");
        //    Response.Charset = "";
        //    Response.ContentType = "application/pdf";


        //    gvspdb.AllowPaging = false;
        //    this.BindGridView();

        //    using (StringWriter sw = new StringWriter())
        //    {
        //        using (HtmlTextWriter hw = new HtmlTextWriter(sw))
        //        {
        //            // Render the GridView to HTML
        //            gvspdb.RenderControl(hw);
        //            string htmlOutput = sw.ToString();

        //            using (StringReader sr = new StringReader(htmlOutput))
        //            {
        //                using (Document doc = new Document(PageSize.A4, 10f, 10f, 10f, 0f))
        //                {
        //                    PdfWriter writer = PdfWriter.GetInstance(doc, Response.OutputStream);
        //                    doc.Open();

        //                    // Parse the HTML into the PDF document
        //                    XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, sr);
        //                    doc.Close();
        //                }
        //            }
        //        }
        //    }

        //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //    HttpContext.Current.ApplicationInstance.CompleteRequest();
        //}

    }
}

