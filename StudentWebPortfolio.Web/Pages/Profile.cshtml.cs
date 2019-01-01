using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudentWebPortfolio.Business;
using StudentWebPortfolio.Business.Queries;
using StudentWebPortfolio.Common;
using StudentWebPortfolio.Data.Entities;
using StudentWebPortfolio.Web.Managers;

namespace StudentWebPortfolio.Web.Pages
{
    public class ProfileModel : PageModel
    {
        #region Ctor
        private readonly SignInManager _signInManager;
        private readonly UserManager _userManager;
        private readonly IQueryContainer _queries;

        public ProfileModel(SignInManager signInManager,
            UserManager userManager,
            IQueryContainer queries)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _queries = queries;
        }
        #endregion

        #region Model
        public User UserModel { get; set; }
        #endregion

        public async Task<IActionResult> OnGetAsync(string username)
        {
            IQueryable<User> userQuery;

            if (username == null)
            {
                if (_signInManager.IsSignedIn(User))
                    userQuery = _queries.Users.ById(_userManager.GetUserId(User));
                else
                    return LocalRedirect("/");
            }
            else
                userQuery = _queries.Users.ByUsername(username);

            UserModel = await userQuery
                .Include(_ => _.Portfolio)
                .FirstOrDefaultAsync();

            if (UserModel == null)
                return LocalRedirect("/");

            return Page();
        }
    }
}