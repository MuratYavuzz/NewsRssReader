using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RSSParsing
{
    public class News
    {
        public int NewsID {get; set;}
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Author { get; set; }
        public string PubDate { get; set; }
        public string ImageUrl { get; set; }

        //public News(int NewsID, string Title, string Description,
        //string Category, string Author, DateTime PubDate, string ImageUrl)
        //public News(string Title, string Description,string Link, string PublishDate)
        //{
        //    //this.NewsID = NewsID;
        //    this.Title = string.Empty; 
        //    this.Description = string.Empty; 
        //    this.Link = string.Empty; 
        //    this.PublishDate = string.Empty; 
        //    //this.Category = Category;
        //    //this.Author = Author;
        //    //this.PubDate = PubDate;
        //    //this.ImageUrl = ImageUrl;
        //}
       
    }
}