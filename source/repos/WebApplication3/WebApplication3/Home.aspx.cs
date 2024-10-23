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
    public partial class Home : System.Web.UI.Page
    {
     
        string cs = ConfigurationManager.ConnectionStrings["signupdb"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        protected void LoginButton1_Click(object sender, EventArgs e)
        {
            // Redirectiong to login Page
            Response.Redirect("login.aspx");

        }

        protected void SignupButton_Click(object sender, EventArgs e)
        {
            //Redirecting to Registration Page
            Response.Redirect("signup.aspx");

        }
    }
}