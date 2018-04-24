using SimpleBlog.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBlog.Views.Home
{
    public class IndexViewModel
    {
        public IEnumerable<SimpleBlog.DAO.Author> Authors { get; set; }
        public IEnumerable<Post> Last5Posts { get; set; }
        public IEnumerable<Post> NewPosts { get; set; }
        public Dictionary<string, Int32> TopAuthors { get; set; }
        public string TextShort(String content) {  return content.Substring(0, (content.Length/2)-1);  }
    }
}