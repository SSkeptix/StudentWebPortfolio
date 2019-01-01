using StudentWebPortfolio.Business.Queries;
using StudentWebPortfolio.Common;
using StudentWebPortfolio.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentWebPortfolio.Business
{
    public class QueryContainer : IQueryContainer
    {
        private readonly ApplicationDbContext _context;

        public QueryContainer(ApplicationDbContext context)
        {
            _context = context;
        }

        private IPortfolioQueries _portfolios;
        public IPortfolioQueries Portfolios => _portfolios ?? (_portfolios = new PortfolioQueries(_context));

        private IUserQueries _users;
        public IUserQueries Users => _users ?? (_users = new UserQueries(_context));

        private ISkillQueries _skills;
        public ISkillQueries Skills => _skills ?? (_skills = new SkillQueries(_context));
    }
}
