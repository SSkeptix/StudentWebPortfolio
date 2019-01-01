using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudentWebPortfolio.Data;
using StudentWebPortfolio.Data.Entities;

namespace StudentWebPortfolio.Business.Queries
{
    internal class SkillQueries : ISkillQueries
    {
        private readonly ApplicationDbContext _context;

        public SkillQueries(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Skill> All() => _context.Skills;
    }
}
