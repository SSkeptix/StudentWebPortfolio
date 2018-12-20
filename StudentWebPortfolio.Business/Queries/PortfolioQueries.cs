using StudentWebPortfolio.Data;
using StudentWebPortfolio.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentWebPortfolio.Business.Queries
{
    public class PortfolioQueries : IPortfolioQueries
    {
        private readonly ApplicationDbContext _context;

        public PortfolioQueries(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Portfolio> ByUserId(long userId)
            => _context.Portfolios.Where(_ => _.UserId == userId);
        
    }
}
