using StudentWebPortfolio.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StudentWebPortfolio.Business.Queries
{
    public interface IUserQueries
    {
        IQueryable<User> ById(long userId);
        IQueryable<User> ByEmail(string email);
        IQueryable<User> ByUsername(string username);
    }
}
