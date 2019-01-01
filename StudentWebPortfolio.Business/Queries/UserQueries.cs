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
    internal class UserQueries : IUserQueries
    {
        private readonly ApplicationDbContext _context;

        public UserQueries(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<User> ById(long userId)
            => _context.Users.Where(_ => _.Id == userId);

        public IQueryable<User> ByEmail(string email)
            => _context.Users.Where(_ => _.NormalizedEmail == email.ToUpperInvariant());

        public IQueryable<User> ByUsername(string username)
            => _context.Users.Where(_ => _.NormalizedUserName == username.ToUpperInvariant());
    }
}
