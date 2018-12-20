using StudentWebPortfolio.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentWebPortfolio.Business.Commands
{
    public interface IPortfolioCommands
    {
        Task Add(Portfolio portfolio);
        Task Update(Portfolio portfolio);
    }
}
