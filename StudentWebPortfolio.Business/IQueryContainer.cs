using StudentWebPortfolio.Business.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentWebPortfolio.Business
{
    public interface IQueryContainer
    {
        IPortfolioQueries Portfolios { get; }
        IUserQueries Users { get; }
        ISkillQueries Skills { get; }
    }
}
