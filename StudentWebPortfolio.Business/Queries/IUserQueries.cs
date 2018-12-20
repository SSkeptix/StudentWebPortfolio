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
        IQueryable<User> ByIdAsync(long userId);
        IQueryable<User> ByEmailAsync(string email);
    }
}
