using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudentWebPortfolio.Business;
using StudentWebPortfolio.Business.Commands;
using StudentWebPortfolio.Business.Queries;
using StudentWebPortfolio.Common;
using StudentWebPortfolio.Data.Entities;
using StudentWebPortfolio.Web.Helpers;
using StudentWebPortfolio.Web.Managers;

namespace StudentWebPortfolio.Web.Areas.Identity.Pages.Account.Manage
{
    public class PortfolioModel : PageModel
    {
        #region Ctor
        private readonly IQueryContainer _queries;
        private readonly ICommandContainer _commands;
        private readonly UserManager _userManager;

        public PortfolioModel(IQueryContainer queries,
            ICommandContainer commands,
            UserManager userManager)
        {
            _queries = queries;
            _commands = commands;
            _userManager = userManager;
        }
        #endregion

        #region Model
        [BindProperty]
        public InputModel Input { get; set; }
        [BindProperty]
        public SkillModel[] Skills { get; set; }
        public MessageBox MessageBox { get; set; }

        public class InputModel
        {
            public string GitHubUrl { get; set; }
            public string Group { get; set; }
            public English English { get; set; }
            public string Description { get; set; }
        }

        public class SkillModel
        {
            public long SkillId { get; set; }
            public string Name { get; set; }
            public bool IsChecked { get; set; }
        }
        #endregion

        public async Task OnGetAsync()
        {
            var userId = _userManager.GetUserId(User);

            Input = await _queries.Portfolios.ByUserId(userId).ProjectTo<InputModel>().FirstOrDefaultAsync();
            await Initialize();
        }

        public async Task OnPostAsync()
        {
            var userId = _userManager.GetUserId(User);

            var portfolio = Input.MapTo<Portfolio>();
            portfolio.UserId = userId;

            if (await _queries.Portfolios.ByUserId(userId).AnyAsync())
                await _commands.Portfolios.Update(portfolio,
                    Skills.Where(_ => _.IsChecked).Select(_ => _.SkillId));
            else
                await _commands.Portfolios.Add(portfolio);

            MessageBox = MessageBox.Success("Portfolio successfully updated.");
            await Initialize();
        }

        private async Task Initialize()
        {
            var userId = _userManager.GetUserId(User);

            Skills = await _queries.Skills.All()
                .Include(_ => _.UserSkills)
                .Select(_ => new SkillModel
                {
                    SkillId = _.SkillId,
                    Name = _.Name,
                    IsChecked = _.UserSkills.Any(s => s.UserId == userId)
                })
                .ToArrayAsync();
        }
    }
}