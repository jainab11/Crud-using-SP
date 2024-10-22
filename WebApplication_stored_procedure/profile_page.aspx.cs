using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication_stored_procedure
{
    public partial class profile_page : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["spdb"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                BindUserDetails();
            }
        }

        private void BindUserDetails()
        {
            if (Session["USERID"] != null)
            {
                int UserId = (int)Session["USERID"];
                using(SqlConnection con = new SqlConnection(cs))
                {
                    using(SqlCommand cmd= new SqlCommand("SP_MANAGE_USERS", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", "GETUSERDETAILS");
                        cmd.Parameters.AddWithValue("@ID", UserId);
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        if(dt.Rows.Count > 0)
                        {
                            DataRow row = dt.Rows[0];
                            lblName.Text = row["NAME"].ToString();
                            lblEmail.Text = row["EMAIL"].ToString();
                            lblMobile.Text = row["MOBILE"].ToString();
                            lblGender.Text = row["GENDER"].ToString();
                            imgProfile.ImageUrl = "~/Handler1.ashx?id=" + UserId;
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "NoData", "alert('No user data found.');",true);
                        }
                    }
                }
                    
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            // for modal save details
            using(SqlConnection con = new SqlConnection(cs))
            {
                using(SqlCommand cmd = new SqlCommand("SP_MANAGE_USERS", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTION", "EDITDETAILS");
                    cmd.Parameters.AddWithValue("@NAME", txtName.Text);
                    cmd.Parameters.AddWithValue("@EMAIL", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@MOBILE", txtMobile.Text);
                    cmd.Parameters.AddWithValue("@GENDER", GenderDropDownList.SelectedValue);
                    con.Open();
                    int res = cmd.ExecuteNonQuery();
                    if (res > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Update Aert","alert('Data updated');",true);
                        BindUserDetails();

                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Update Alert", "alert('Incorrect user Name');", true);
                    }

                }
            }
        }

        protected void savePasswordButton_Click(object sender, EventArgs e)
        {
            // fro change the password
            using(SqlConnection con = new SqlConnection(cs))
            {
                using(SqlCommand cmd = new SqlCommand("SP_MANAGE_USERS", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTION", "CHANGEPASSWORD");
                    cmd.Parameters.AddWithValue("@NAME", txtNamepass.Text);
                    cmd.Parameters.AddWithValue("@OLDPASSWORD", txtoldpass.Text);
                    cmd.Parameters.AddWithValue("@NEWPASSWORD", txtnewPass.Text);
                    con.Open();
                    int res = cmd.ExecuteNonQuery();
                    if (res > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "passwordChangeAlert", "alert('Password changed successfully');", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "passwordChangeAlert", "alert('Password cahnge failed');", true);
                    }

                }
            }
        }

        protected void ChangePasswordButton_Click(object sender, EventArgs e)
        {
            // For Modal Popup
            ClientScript.RegisterStartupScript(this.GetType(),"key", "launchModal();", true);
        }

        protected void EditButton_Click(object sender, EventArgs e)
        {
            // for modal popup
            ClientScript.RegisterStartupScript(this.GetType(),"key","launchModal2();",true);

        }
         private DataTable  GetUserDetails(int userid)
        {
            DataTable dt = new DataTable();
            using(SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT Name, Email, Mobile, Gender, UserType FROM REG_TABLE WHERE ID = @ID";
                using(SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ID", userid);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);

                }
            }
            return dt;
        }
        protected void btnPdfExport_Click(object sender, EventArgs e)
        {
            if (Session["USERID"]==null)
            {
                Response.Write("<script>alert(;plese log in');</script>");
                return;
            }
            int userid = (int)Session["USERID"];
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0);
            try
            {
                DataTable userDetails = GetUserDetails(userid);
                if(userDetails.Rows.Count == 0)
                {
                    Response.Write("<script>alert('No details found for the logged-in user.');</script>");
                    return;
                }
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment;filename = userDetails.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                using (MemoryStream ms = new MemoryStream())
                {
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, ms);
                    pdfDoc.Open();
                    pdfDoc.Add(new Paragraph("User Details"));
                    pdfDoc.Add(new Paragraph(" "));

                    foreach (DataRow row in userDetails.Rows)
                    {
                        pdfDoc.Add(new Paragraph($"Name: {row["Name"]}"));
                        pdfDoc.Add(new Paragraph($"Email: {row["Email"]}"));
                        pdfDoc.Add(new Paragraph($"Mobile: {row["Mobile"]}"));
                        pdfDoc.Add(new Paragraph($"Gender: {row["Gender"]}"));
                        pdfDoc.Add(new Paragraph($"User Type: {row["UserType"]}"));
                        pdfDoc.Add(new Paragraph(" "));

                    }
                    pdfDoc.Close();

                    Response.OutputStream.Write(ms.GetBuffer(), 0, ms.ToArray().Length);
                    Response.End();

                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('An error occured " + ex.Message + "');", true);
            }
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

    }
}