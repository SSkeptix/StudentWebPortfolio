using StudentWebPortfolio.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentWebPortfolio.Business.Queries
{
    public interface IPortfolioQueries
    {
        IQueryable<Portfolio> ByUserId(long userId);
    }
}
