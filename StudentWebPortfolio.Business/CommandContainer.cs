using StudentWebPortfolio.Business.Commands;
using StudentWebPortfolio.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentWebPortfolio.Business
{
    public class CommandContainer : ICommandContainer
    {
        private readonly ApplicationDbContext _context;

        public CommandContainer(ApplicationDbContext context)
        {
            _context = context;
        }

        private IPortfolioCommands _portfolios;
        public IPortfolioCommands Portfolios => _portfolios ?? (_portfolios = new PortfolioCommands(_context));

        private IUserSkillCommands _userSkills;
        public IUserSkillCommands UserSkills => _userSkills ?? (_userSkills = new UserSkillCommands(_context));
    }
}
