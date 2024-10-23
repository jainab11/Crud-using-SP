using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{
    public partial class admindatabase : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["signupdb"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["UserName"] == null) // for logout
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
            SqlConnection con = new SqlConnection(cs);
            string query = "select * from Signup";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            gvhome.DataSource = dt;
            gvhome.DataBind();
        }
        protected void LogoutButon_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect("login.aspx");
        }

        protected void gvhome_RowEditing1(object sender, GridViewEditEventArgs e)
        {

 
            gvhome.EditIndex = e.NewEditIndex;

 
            BindGridView();

        }

        //protected void gvhome_RowUpdating1(object sender, GridViewUpdateEventArgs e)
        //{


        //    int id = Convert.ToInt32(gvhome.DataKeys[e.RowIndex].Value.ToString());

        //    TextBox txtName = (TextBox)gvhome.Rows[e.RowIndex].FindControl("TextBox2");
        //    TextBox txtEmail = (TextBox)gvhome.Rows[e.RowIndex].FindControl("TextBox3");
        //    TextBox txtPassword = (TextBox)gvhome.Rows[e.RowIndex].FindControl("TextBox4");
        //    TextBox txtMobile = (TextBox)gvhome.Rows[e.RowIndex].FindControl("TextBox5");
        //    DropDownList ddlGender = (DropDownList)gvhome.Rows[e.RowIndex].FindControl("ddlGender");
        //    DropDownList ddlUserType = (DropDownList)gvhome.Rows[e.RowIndex].FindControl("ddlUserType");
        //    // Get the text from the TextBox controls
        //    string name = txtName?.Text;
        //    string email = txtEmail?.Text;
        //    string password = txtPassword?.Text;
        //    string mobile = txtMobile?.Text;
        //    string gender = ddlGender?.SelectedValue;
        //    string usertype = ddlUserType?.SelectedValue;

        //    string query = "UPDATE signup SET name = @name, email = @email, password = @password, mobile = @mobile, gender = @gender, usertype = @usertype WHERE id = @id";

        //    using (SqlConnection con = new SqlConnection(cs))
        //    {
        //        using (SqlCommand cmd = new SqlCommand(query, con))
        //        {
        //            cmd.Parameters.AddWithValue("@id", id);
        //            cmd.Parameters.AddWithValue("@name", name);
        //            cmd.Parameters.AddWithValue("@email", email);
        //            cmd.Parameters.AddWithValue("@password", password);
        //            cmd.Parameters.AddWithValue("@mobile", mobile);
        //            cmd.Parameters.AddWithValue("@gender", gender);
        //            cmd.Parameters.AddWithValue("@usertype", usertype);

        //            con.Open();
        //            int result = cmd.ExecuteNonQuery();
        //            if (result > 0)
        //            {
        //                // Display a success message
        //                Response.Write("<script>alert('Data has been updated successfully');</script>");

        //                // Reset the GridView to normal mode (cancel edit mode)
        //                gvhome.EditIndex = -1;
        //                BindGridView(); // Refresh the GridView
        //            }
        //            else
        //            {
        //                // Handle update failure
        //                Response.Write("<script>alert('Update failed');</script>");
        //            }
        //        }
        //    }
        //}
        protected void gvhome_RowUpdating1(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(gvhome.DataKeys[e.RowIndex].Value.ToString());

            // Retrieve updated user details from the GridView controls
            TextBox txtName = (TextBox)gvhome.Rows[e.RowIndex].FindControl("TextBox2");
            TextBox txtEmail = (TextBox)gvhome.Rows[e.RowIndex].FindControl("TextBox3");
            TextBox txtPassword = (TextBox)gvhome.Rows[e.RowIndex].FindControl("TextBox4");
            TextBox txtMobile = (TextBox)gvhome.Rows[e.RowIndex].FindControl("TextBox5");
            DropDownList ddlGender = (DropDownList)gvhome.Rows[e.RowIndex].FindControl("ddlGender");
            DropDownList ddlUserType = (DropDownList)gvhome.Rows[e.RowIndex].FindControl("ddlUserType");

            // Get the new image if uploaded
            FileUpload fileUpload = (FileUpload)gvhome.Rows[e.RowIndex].FindControl("FileUploadEdit");
            string imgPath = string.Empty;

            // Handle the image upload if a new file was selected
            if (fileUpload.HasFile)
            {
                string fileName = Path.GetFileName(fileUpload.FileName);
                string extension = Path.GetExtension(fileName);
                int fileSize = fileUpload.PostedFile.ContentLength;

                
                if (extension.ToLower() == ".jpg" || extension.ToLower() == ".png" || extension.ToLower() == ".jpeg")
                {
                    if (fileSize <= 1000000) 
                    {
                        string filePath = Server.MapPath("~/img/") + fileName;
                        fileUpload.SaveAs(filePath);
                        imgPath = "img/" + fileName; 
                    }
                    else
                    {
                        Response.Write("<script>alert('File size should be less than 1MB!');</script>");
                        return;
                    }
                }
                else
                {
                    Response.Write("<script>alert('Only .jpg, .png, or .jpeg formats are supported!');</script>");
                    return;
                }
            }
            else
            {
                // If no new image is uploaded, keep the old image path
                imgPath = (gvhome.Rows[e.RowIndex].FindControl("ImageEdit") as Image).ImageUrl;
            }

            // Update the database with the new/old image and user details
            string query = "UPDATE signup SET name = @name, email = @email, password = @password, mobile = @mobile, gender = @gender, usertype = @usertype, img = @img WHERE id = @id";

            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                    cmd.Parameters.AddWithValue("@mobile", txtMobile.Text);
                    cmd.Parameters.AddWithValue("@gender", ddlGender.SelectedValue);
                    cmd.Parameters.AddWithValue("@usertype", ddlUserType.SelectedValue);
                    cmd.Parameters.AddWithValue("@img", imgPath);

                    con.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        // Success message and refresh GridView
                        Response.Write("<script>alert('Data has been updated successfully');</script>");
                        gvhome.EditIndex = -1; // Exit edit mode
                        BindGridView(); // Refresh data
                    }
                    else
                    {
                        // Failure message
                        Response.Write("<script>alert('Update failed');</script>");
                    }
                }
            }
        }

        protected void gvhome_RowCancelingEdit2(object sender, GridViewCancelEditEventArgs e)
        {
            gvhome.EditIndex = -1;
            BindGridView();
        }

        protected void gvhome_RowDeleting1(object sender, GridViewDeleteEventArgs e)
        {
            string query = "DELETE FROM signup WHERE id = @id";
            int id = Convert.ToInt32(gvhome.DataKeys[e.RowIndex].Value.ToString());
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameter for the id
                    cmd.Parameters.AddWithValue("@id", id);

                    con.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        // Display a success message
                        Response.Write("<script>alert('Data has been deleted successfully');</script>");

                        // Reset the GridView to normal mode
                        gvhome.EditIndex = -1;
                        BindGridView(); // Refresh the GridView
                    }
                    else
                    {
                        // Handle delete failure
                        Response.Write("<script>alert('Delete failed');</script>");
                    }
                }
            }
        }

        protected void gvhome_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Addnew"))
            {
                try
                {
                    // Get the file upload control
                    FileUpload fileUpload = gvhome.FooterRow.FindControl("FileUploadFooter") as FileUpload;
                    string imgPath = string.Empty;

                    // Handle file upload
                    if (fileUpload != null && fileUpload.HasFile)
                    {
                        string fileName = Path.GetFileName(fileUpload.FileName);
                        string extension = Path.GetExtension(fileName);
                        int fileSize = fileUpload.PostedFile.ContentLength;

                        if (extension.ToLower() == ".jpg" || extension.ToLower() == ".png" || extension.ToLower() == ".jpeg")
                        {
                            if (fileSize <= 1000000) // Check file size (1MB limit)
                            {
                                string filePath = Server.MapPath("~/img/") + fileName;
                                fileUpload.SaveAs(filePath);
                                imgPath = "img/" + fileName; // Ensure this path is correct
                            }
                            else
                            {
                                Label1.Text = "File size should be less than 1MB!";
                                return;
                            }
                        }
                        else
                        {
                            Label1.Text = "Only .jpg, .png, or .jpeg formats are supported!";
                            return;
                        }
                    }

                    // Retrieve other data from the footer controls
                    TextBox txtNameFooter = gvhome.FooterRow.FindControl("txtnamefooter") as TextBox;
                    TextBox txtEmailFooter = gvhome.FooterRow.FindControl("txtemailfooter") as TextBox;
                    TextBox txtPasswordFooter = gvhome.FooterRow.FindControl("txtpasswordfooter") as TextBox;
                    TextBox txtMobileFooter = gvhome.FooterRow.FindControl("txtmobilefooter") as TextBox;
                    DropDownList ddlGenderFooter = gvhome.FooterRow.FindControl("ddlGenderFooter") as DropDownList;
                    DropDownList ddlUserTypeFooter = gvhome.FooterRow.FindControl("ddlUserTypeFooter") as DropDownList;

                    string name = txtNameFooter?.Text;
                    string email = txtEmailFooter?.Text;
                    string password = txtPasswordFooter?.Text;
                    string mobile = txtMobileFooter?.Text;
                    string gender = ddlGenderFooter?.SelectedValue;
                    string usertype = ddlUserTypeFooter?.SelectedValue;

                    string query = "INSERT INTO Signup (name, email, password, mobile, gender, usertype, img) VALUES (@name, @email, @password, @mobile, @gender, @usertype, @img)";

                    using (SqlConnection con = new SqlConnection(cs))
                    {
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@name", name);
                            cmd.Parameters.AddWithValue("@email", email);
                            cmd.Parameters.AddWithValue("@password", password);
                            cmd.Parameters.AddWithValue("@mobile", mobile);
                            cmd.Parameters.AddWithValue("@gender", gender);
                            cmd.Parameters.AddWithValue("@usertype", usertype);
                            cmd.Parameters.AddWithValue("@img", imgPath);

                            con.Open();
                            int result = cmd.ExecuteNonQuery();
                            if (result > 0)
                            {
                                // Display a success message
                                Response.Write("<script>alert('Data has been inserted successfully');</script>");
                                BindGridView(); // Refresh the GridView
                            }
                            else
                            {
                                // Handle insert failure
                                Response.Write("<script>alert('Insert failed');</script>");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
                }
            }
        }

    }
}