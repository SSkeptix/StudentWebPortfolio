using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using StudentWebPortfolio.Common;
using StudentWebPortfolio.Data;
using StudentWebPortfolio.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StudentWebPortfolio.Business.Queries
{
    public class UserQueries : IUserQueries
    {
        private readonly ApplicationDbContext _context;

        public UserQueries(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<T> ByIdAsync<T>(long userId, Expression<Func<User, T>> selector = null)
            => _context.Users.Where(_ => _.Id == userId).MapOrSelect(selector).FirstOrDefaultAsync();

        public Task<T> ByEmailAsync<T>(string email, Expression<Func<User, T>> selector = null)
            => _context.Users.Where(_ => _.NormalizedEmail == email.ToUpperInvariant()).MapOrSelect(selector).FirstOrDefaultAsync();
    }
}
