using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
namespace RSSParsing
{

    public partial class RSSparsing : System.Web.UI.Page
    {
        List<News> news = new List<News>();
        List<Image> images = new List<Image>(); //image list which holds ImagesUrl

        protected void Page_Load(object sender, EventArgs e)
        {
            RssReader();
            int i = 1;
            foreach (News n in news)
            {
                n.ImageUrl = images[i].ImageUrl;
            }
            WriteToDatabese();
       

        }
        
        private void RssReader()
        {
            string RssUrl = "http://ajans.dha.com.tr/dha_public_rss.php"; //Our Rss Url For Feeding

            try
            {
                XDocument XmlImage = new XDocument();   //Xml Document for Url in Image Tag
                XmlImage = XDocument.Load(RssUrl);  //Load RssUrl for Feed

                var image = (from i in XmlImage.Descendants("image")
                             select new
                             {
                                 imageurl = i.Element("url").Value  //select the url from the image tag
                             });

                XDocument Xmldocs = new XDocument();    //Xml Document for Title,PubDate,Descripton,Category and Author in Title Tag
                Xmldocs = XDocument.Load(RssUrl);   //Load RssUrl for Feed
                var items = (from x in Xmldocs.Descendants("item")
                             select new
                             {
                                 title = x.Element("title").Value,  //select the title from the item tag
                                 pubdate = x.Element("pubDate").Value,  //select the pubDate from the item tag
                                 description = x.Element("description").Value,  //select the description from the item tag
                                 category = x.Element("category").Value,    //select the category from the item tag
                                 author = x.Element("author").Value,    //select the author from the item tag
                             });
                if (items != null)
                {
                    int id = 1;

                    foreach (var i in items)
                    {
                        News n = new News
                        {
                            NewsID = id,    //assigning what received from the xml file to the elements of the news object
                            Title = i.title,
                            Category = i.category,
                            PubDate = i.pubdate,
                            Description = i.description,
                            Author = i.author,
                        };


                        id++;
                        news.Add(n);
                    }
                }
                if (image != null)
                {

                    foreach (var i in image)
                    {
                        Image n = new Image
                        {
                            ImageUrl=i.imageurl
                        };


                        images.Add(n);
                    }
                }
            }

            catch (Exception e)
            {
                Logging(e);   
                throw;
            }
        }
        public void Logging(Exception ex)  //Error Loging
        {
            string Path = System.AppDomain.CurrentDomain.BaseDirectory + "ErrorLog.txt";

            if (!File.Exists(Path))
            {
                File.Create(Path).Dispose();
            }
            using (StreamWriter writer = File.AppendText(Path))
            {
                writer.WriteLine("---Error---");
                writer.WriteLine("---Starting--- " + DateTime.Now);
                writer.WriteLine("Error Message: " + ex.Message);
                writer.WriteLine("Stack Trace: " + ex.StackTrace);
                writer.WriteLine("---Ending--- " + DateTime.Now);

            }
        }
        public void WriteToDatabese()
        {

            SqlConnection connect = new SqlConnection();  
            try
            {
               
                connect.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\NewsData.mdf;Integrated Security=True";

                connect.Open();

               
                foreach (News news in news)  
                {
                    using (SqlCommand command = new SqlCommand("insert into News(NewsID,Title,Description,Category,Author,Pubdate,ImageURL) VALUES(@newsId,@title,@description,@category,@author,@pubDate,@imageUrl)", connect))
                    {
                       

                        command.Parameters.AddWithValue("newsId", news.NewsID);
                        command.Parameters.AddWithValue("title", news.Title);
                        command.Parameters.AddWithValue("description", news.Description);
                        command.Parameters.AddWithValue("category", news.Category);
                        command.Parameters.AddWithValue("author", news.Author);
                        command.Parameters.AddWithValue("pubDate", news.PubDate);
                        command.Parameters.AddWithValue("imageUrl", news.ImageUrl);
                        command.ExecuteNonQuery();

                    }   
                }

            }
            catch (Exception e)
            {
                Logging(e);  
            }
            finally
            {
                connect.Close();
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = GridView1.SelectedIndex.ToString();
            Response.Redirect("NewsDetail.aspx?ID="+id);
        }
    }
}