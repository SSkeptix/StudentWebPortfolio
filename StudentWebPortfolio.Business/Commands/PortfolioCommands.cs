using StudentWebPortfolio.Data;
using StudentWebPortfolio.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentWebPortfolio.Business.Commands
{
    public class PortfolioCommands : IPortfolioCommands
    {
        private readonly ApplicationDbContext _context;

        public PortfolioCommands(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task Add(Portfolio portfolio)
        {
            _context.Add(portfolio);
            return _context.SaveChangesAsync();
        }

        public Task Update(Portfolio portfolio)
        {
            _context.Update(portfolio);
            return _context.SaveChangesAsync();
        }
    }
}
