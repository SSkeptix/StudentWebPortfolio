using StudentWebPortfolio.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentWebPortfolio.Business.Commands
{
    internal class UserSkillCommands : IUserSkillCommands
    {
        private readonly ApplicationDbContext _context;

        public UserSkillCommands(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
