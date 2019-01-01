using Microsoft.EntityFrameworkCore;
using StudentWebPortfolio.Data;
using StudentWebPortfolio.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentWebPortfolio.Business.Commands
{
    internal class PortfolioCommands : IPortfolioCommands
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

        public async Task Update(Portfolio portfolio, IEnumerable<long> skillsIds)
        {
            _context.Update(portfolio);

            var userSkills = await _context.UserSkills
                .Where(_ => _.UserId == portfolio.UserId)
                .ToArrayAsync();

            var skillsToRemove = userSkills.Where(_ => !skillsIds.Contains(_.SkillId));
            _context.RemoveRange(skillsToRemove);

            var skillsToAdd = skillsIds
                .Where(_ => !userSkills.Any(u => u.SkillId == _))
                .Select(_ => new UserSkill
                {
                    UserId = portfolio.UserId,
                    SkillId = _,
                });
            _context.AddRange(skillsToAdd);

            await _context.SaveChangesAsync();
        }
    }
}
