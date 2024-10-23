using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{
    
    public partial class userdatabase : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["signupdb"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            // Prevent caching
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            Response.Cache.SetNoStore();
            Response.Cache.AppendCacheExtension("no-store");

            // Check if user is authenticated
            if (Session["UserName"] == null) // Adjust based on how you store user session
            {
                Response.Redirect("login.aspx");
            }

            if (!IsPostBack)
            {
                BindGridView();
            }
        }

        private void BindGridView()
        {
            string query = "SELECT * FROM signup WHERE usertype = @usertype";

            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.Add("@usertype", SqlDbType.VarChar).Value = "user";
                        con.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            gvhome.DataSource = reader;
                            gvhome.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Label1.Text = "Error: " + ex.Message;
                Label1.Visible = true;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
        }

        protected void EditButton_Click(object sender, EventArgs e)
        {

            ClientScript.RegisterStartupScript(this.GetType(), "key", "launchModal();", true);
            
        }

        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            string query;
            string imgPath = Server.MapPath("img/");

            // Check if a new image is uploaded
            if (FileUpload1.HasFile)
            {
                string fileName = Path.GetFileName(FileUpload1.FileName);
                string extension = Path.GetExtension(fileName);
                int fileSize = FileUpload1.PostedFile.ContentLength;

                if ((extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png") && fileSize <= 1048576)
                {
                    imgPath = "img/" + fileName;
                    FileUpload1.SaveAs(Server.MapPath(imgPath));
                }
                else
                {
                    Response.Write("<script>alert('Invalid file format or size. Only .jpg/.jpeg/.png files under 1MB are allowed.');</script>");
                    
                   
                    return; // Exit if validation fails
                }
            }
            else
            {
                // If no image is uploaded, keep the old image path
                imgPath = HiddenFieldOriginalName.Value;
            }

            // Update query with or without image path
            query = "UPDATE signup SET email = @Email, mobile = @Mobile, gender = @Gender, img = @Img WHERE name = @Name";

            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text);
                    cmd.Parameters.AddWithValue("@Gender", GenderDropDownList.SelectedValue);
                    cmd.Parameters.AddWithValue("@Img", imgPath);
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);

                    con.Open();
                    int result = cmd.ExecuteNonQuery();
                    con.Close();

                    if (result > 0)
                    {
                        Response.Write("<script>alert('Data updated successfully!');</script>");   
                    }
                    else

                    {
                        Response.Write("<script>alert('Update failed!');</script>");
                        
                    }
                }
            }

            BindGridView(); // Refresh GridView
        }


        protected void ChangePasswordButton_Click(object sender, EventArgs e)
        {
           
            ClientScript.RegisterStartupScript(this.GetType(), "key", "launchModal2();", true);

        }


        protected void savePasswordButton_Click(object sender, EventArgs e)
        {
            string query = "UPDATE signup SET password = @NewPassword WHERE name = @Username AND password = @CurrentPassword";
            int rowsAffected = 0;

            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Username", txtnmae.Text); 
                    cmd.Parameters.AddWithValue("@CurrentPassword", txtOldPass.Text);
                    cmd.Parameters.AddWithValue("@NewPassword", txtNewPass.Text); 

                    con.Open();
                    rowsAffected = cmd.ExecuteNonQuery(); 

                    if (rowsAffected > 0)
                    {
                        Response.Write("<script>alert('Password changed successfully!');</script>");
                    }
                    else
                    {

                        Response.Write("<script>alert('Old password is incorrect. Please try again.');</script>");
                    }
                }
            }
        }


        protected void LogoutButton_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("login.aspx");

        }
    }
}