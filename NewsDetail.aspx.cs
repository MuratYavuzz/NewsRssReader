using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RSSParsing
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        News new_ = new News();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                int i = Int32.Parse(Request.QueryString["id"]);
                i++;
                DatabaseToNewList(i);



                Category.Text = new_.Category;
                PubDate.Text = new_.PubDate;
                Title.Text = new_.Title;
                Description.Text = new_.Description;
                Image1.ImageUrl = new_.ImageUrl;

                if (i > 20 || i < 0)
                {
                    Response.Write("Invalid News ID !");
                }
            }
            else
                Response.Write("Please Specify News ID!");

        }
        public void DatabaseToNewList(int a)
        {

            SqlConnection connect = new SqlConnection();

            try
            {
                connect.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\NewsData.mdf;Integrated Security=True";

                connect.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM News where NewsID=" + a, connect))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        new_.NewsID = reader.GetInt32(0);
                        new_.Title = reader.GetString(1);
                        new_.Description = reader.GetString(2);
                        new_.Category = reader.GetString(3);
                        new_.PubDate = reader.GetString(5);
                        new_.ImageUrl = reader.GetString(6);

                    }
                }
            }
            catch
            {
            }
            finally
            {
                connect.Close();
            }
        }


    }

}