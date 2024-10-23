using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Web;

namespace WebApplication3
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        //Databse Configuration String
        string cs = ConfigurationManager.ConnectionStrings["signupdb"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Page Load to show Grid View
            if (!IsPostBack)
            {
                BindGridView();
            }
        }

        private void BindGridView()
        {
            //This function is for inserting data in grid view and for showing it
            SqlConnection con = new SqlConnection(cs);
            string query = "select * from signup";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            //For saving Data in the database and grid view
            SqlConnection con = new SqlConnection(cs); //connection string
            string path = Server.MapPath("~/img/"); //image path
            string usertype = AdminRadioButton.Checked ? "Admin" : "User"; // admin Button for Radio Type

            if (FileUpload1.HasFile) // check if it has file or not
            {
                // Get file name
                string FileName = Path.GetFileName(FileUpload1.FileName);
                //Get Image Extension
                string extension = Path.GetExtension(FileName);
                // An HttpPostedFile for a file uploaded by using the FileUpload.
                HttpPostedFile postedfile = FileUpload1.PostedFile;
                //For checking Length of file
                int length = postedfile.ContentLength;

                if (extension.ToLower() == ".jpg" || extension.ToLower() == ".png" || extension.ToLower() == ".jpeg")
                {
                    if (length <= 1000000)  // File size check
                    {

                        FileUpload1.SaveAs(path + FileName);
                        string imgPath = "img/" + FileName;
                        // currently not using it for convert image into Binary Format
                        BinaryReader br = new BinaryReader(postedfile.InputStream);
                        byte[] imgData = br.ReadBytes(postedfile.ContentLength);
                        Session["PhotoName"] = FileName;
                        Session["PhotoBinary"] = imgData;

                        string query = "INSERT INTO signup (name, email, password, mobile, gender, usertype, img) " +
                                       "VALUES (@name, @email, @password, @mobile, @gender, @usertype, @img)";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@name", NameTextBox.Text);
                        cmd.Parameters.AddWithValue("@email", EmailTextBox.Text);
                        cmd.Parameters.AddWithValue("@password", PasswordTextBox.Text);
                        cmd.Parameters.AddWithValue("@mobile", MobileTextBox.Text);
                        cmd.Parameters.AddWithValue("@gender", GenderDropDownList.SelectedValue);
                        cmd.Parameters.AddWithValue("@usertype", usertype);
                        cmd.Parameters.AddWithValue("@img", imgPath);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        // Clear fields
                        NameTextBox.Text = "";
                        EmailTextBox.Text = "";
                        PasswordTextBox.Text = "";
                        MobileTextBox.Text = "";
                        Label1.ForeColor = Color.Green;
                        Label1.Text = "Registration successful!";
                        

                        BindGridView();
                    }
                    else
                    {
                        Label1.Text = "File size should be less than 1MB!";
                        Label1.ForeColor = Color.Red;
                    }
                }
                else
                {
                    Label1.Text = "Only .jpg, .png, or .jpeg formats are supported!";
                    Label1.ForeColor = Color.Red;
                }
            }
            else
            {
                Label1.Text = "Please upload an image!";
                Label1.ForeColor = Color.Red;
            }
        }

        protected void ReadButton_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "SELECT * FROM signup WHERE name = @name";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@name", NameTextBox.Text);
            
            con.Open();
          
            SqlDataReader reader = cmd.ExecuteReader();
            
            if (reader.Read())
            {
                EmailTextBox.Text = reader["email"].ToString();
                // it will not show password because in aspx password text box property is password
                PasswordTextBox.Text = reader["password"].ToString();
                MobileTextBox.Text = reader["mobile"].ToString();
                GenderDropDownList.SelectedValue = reader["gender"].ToString();
                string usertype = reader["usertype"].ToString();
                // Insted of this we can use ternary oprator
                String userTypee = AdminRadioButton.Checked ? "Admin" : "User";

                if (usertype == "Admin")
                {
                    AdminRadioButton.Checked = true;
                }
                else
                {
                    UserRadioButton.Checked = true;
                }
            }
            else
            {
                Label1.Text = "Data not found!";
                Label1.ForeColor = Color.Red;
            }

            reader.Close();
            con.Close();
            BindGridView();
        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string usertype = AdminRadioButton.Checked ? "Admin" : "User";
            string query = "UPDATE signup SET email = @Email, password = @Password, mobile = @Mobile, gender = @Gender, usertype = @UserType WHERE name = @Name";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Email", EmailTextBox.Text);
            cmd.Parameters.AddWithValue("@Password", PasswordTextBox.Text);
            cmd.Parameters.AddWithValue("@Mobile", MobileTextBox.Text);
            cmd.Parameters.AddWithValue("@Gender", GenderDropDownList.SelectedValue);
            cmd.Parameters.AddWithValue("@UserType", usertype);
            cmd.Parameters.AddWithValue("@Name", NameTextBox.Text);

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
            // Clear fields
            NameTextBox.Text = "";
            EmailTextBox.Text = "";
            PasswordTextBox.Text = "";
            MobileTextBox.Text = "";

            BindGridView();
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "DELETE FROM signup WHERE name = @Name";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Name", NameTextBox.Text);

            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();

            if (result > 0)
            {
                Label1.Text = "Data deleted successfully!";
                Label1.ForeColor = Color.Red;

                // Clear fields after delete
                NameTextBox.Text = "";
                EmailTextBox.Text = "";
                PasswordTextBox.Text = "";
                MobileTextBox.Text = "";
            }
            else
            {
                Response.Write("<script>alert('Delete failed!');</script>");
               
            }

            BindGridView();
        }

        protected void Login_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
        }
    }
}
