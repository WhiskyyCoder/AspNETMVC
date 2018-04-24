using SimpleBlog.DAO;
using System;
using System.Collections.Generic;
using System.Data.Entity;


namespace SimpleBlog.DAL
{
    interface DAOInteface
    {
       DataBaseEntities getContext();
       void AddOrUpdate(Object obj);
       void Delete(Object obj);
      
    }
}
