using SimpleBlog.DAO;
using System.Data.Entity;

namespace SimpleBlog.DAL
{
    public class DAOContext : DAOInteface
    {
        private DataBaseEntities _context;

        public DAOContext() {
            _context= new DataBaseEntities();
        }
      
        public void Delete(object obj)
        {
            if (obj is Author)
            {
                _context.Author.Remove((Author)obj);

            }
            if (obj is Post)
            {
                _context.Post.Remove((Post)obj);
            }

            _context.SaveChanges();
            
        }
        public DbSet<Author> getAuthors()
        {
            return _context.Author;
        }
        public DbSet<Post> getPosts()
        {
            return _context.Post;
        }
        public DataBaseEntities getContext()
        {
            return _context;
        }

        public void AddOrUpdate(object obj)
        {
            if (obj is Author)
            {
               
                _context.Author.Add((Author)obj);

            }
            if (obj is Post)
            {
                _context.Post.Add((Post)obj);
            }
            

            _context.SaveChanges();
        }

        public void Dispose() {
            _context.Dispose();

        }

 
    }
}