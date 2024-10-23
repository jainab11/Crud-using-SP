using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{
    public partial class login : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["signupdb"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userName"] != null)
            {
                // Redirect to the appropriate page based on role
                string role = Session["role"]?.ToString().ToLower();
                if (role == "admin")
                {
                    Response.Redirect("admindatabase.aspx");
                }
                else if (role == "user")
                {
                    Response.Redirect("userdatabase.aspx");
                }
            }

        }
        protected void LoginButton_Click(object sender, EventArgs e)
        {
            string query = "SELECT usertype FROM signup WHERE name = @name AND password = @password";
            using (SqlConnection con = new SqlConnection(cs))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@name", UsernameTextBox.Text.Trim());
                cmd.Parameters.AddWithValue("@password", PasswordTextBox.Text.Trim());

                try
                {
                    con.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        string userRole = result.ToString().ToLower();
                        Session["userName"] = UsernameTextBox.Text.Trim();
                        Session["role"] = userRole;

                        
                 
                        if (userRole == "user")
                        {
                            Response.Redirect("userdatabase.aspx");
                        }
                        else if (userRole == "admin")
                        {
                            Response.Redirect("admindatabase.aspx");
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Unknown user role');</script>");
                        }
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Login failed');</script>");
                    }
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", $"<script>alert('An error occurred: {ex.Message}');</script>");
                }
            }
        }



        protected void signup_Click(object sender, EventArgs e)
        {
            Response.Redirect("signup.aspx");
        }
    }
}