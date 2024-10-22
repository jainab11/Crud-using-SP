using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication_stored_procedure
{
    /// <summary>
    /// this handler code for handling th image iamge is converted into binary 
    /// </summary>
    public class Handler1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int id = Convert.ToInt32(context.Request.QueryString["id"]);
            // fetch the image format
            string cs = ConfigurationManager.ConnectionStrings["spdb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT IMG FROM REG_TABLE WHERE ID = @ID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ID", id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    byte[] imgData = rdr["IMG"] as byte[];
                    if (imgData != null)
                    {
                        context.Response.ContentType = "image/jpg";
                        context.Response.BinaryWrite(imgData);
                        context.Response.End();
                    }
                }

            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
