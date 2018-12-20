using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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
        private readonly IPortfolioQueries _portfolioQueries;
        private readonly IPortfolioCommands _portfolioCommands;
        private readonly UserManager _userManager;

        public PortfolioModel(IPortfolioQueries portfolioQueries,
            IPortfolioCommands portfolioCommands,
            UserManager userManager)
        {
            _portfolioQueries = portfolioQueries;
            _portfolioCommands = portfolioCommands;
            _userManager = userManager;
        }
        #endregion

        #region Model
        [BindProperty]
        public InputModel Input { get; set; }
        public MessageBox MessageBox { get; set; }

        public class InputModel
        {
            public string GitHubUrl { get; set; }
            public string Group { get; set; }
            public English English { get; set; }
            public string Description { get; set; }
        }

        #endregion

        public async Task OnGetAsync()
        {
            var userId = _userManager.GetUserId(User);

            Input = await _portfolioQueries.ByUserId(userId).ProjectTo<InputModel>().FirstOrDefaultAsync();           
        }

        public async Task OnPostAsync()
        {
            var userId = _userManager.GetUserId(User);

            var portfolio = Input.MapTo<Portfolio>();
            portfolio.UserId = userId;

            if (await _portfolioQueries.ByUserId(userId).AnyAsync())
                await _portfolioCommands.Update(portfolio);
            else
                await _portfolioCommands.Add(portfolio);

            MessageBox = MessageBox.Success("Portfolio successfully updated.");
        }
    }
}