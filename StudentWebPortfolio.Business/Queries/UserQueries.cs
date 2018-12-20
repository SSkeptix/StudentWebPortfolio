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

        public IQueryable<User> ByIdAsync(long userId)
            => _context.Users.Where(_ => _.Id == userId);

        public IQueryable<User> ByEmailAsync(string email)
            => _context.Users.Where(_ => _.NormalizedEmail == email.ToUpperInvariant());
    }
}
