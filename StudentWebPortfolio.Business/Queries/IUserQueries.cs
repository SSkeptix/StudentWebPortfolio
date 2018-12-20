using StudentWebPortfolio.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StudentWebPortfolio.Business.Queries
{
    public interface IUserQueries
    {
        Task<T> ByIdAsync<T>(long userId, Expression<Func<User, T>> selector = null);
        Task<T> ByEmailAsync<T>(string email, Expression<Func<User, T>> selector = null);
    }
}
