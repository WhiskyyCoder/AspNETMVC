
using System.Collections.Generic;

namespace SimpleBlog.Views.Author
{
    public class AuthorViewModel
    {
        public SimpleBlog.DAO.Author author { get; set; }
        public IList<SimpleBlog.DAO.Post> posts { get; set; }
        public int countPosts { get; set; }
    }
}