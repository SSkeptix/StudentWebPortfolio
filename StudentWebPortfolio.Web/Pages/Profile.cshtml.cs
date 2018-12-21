using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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
        private readonly IUserQueries _userQueries;

        public ProfileModel(SignInManager signInManager,
            UserManager userManager,
            IUserQueries userQueries)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userQueries = userQueries;
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
                    userQuery = _userQueries.ById(_userManager.GetUserId(User));
                else
                    return LocalRedirect("/");
            }
            else
                userQuery = _userQueries.ByUsername(username);

            UserModel = await userQuery
                .Include(_ => _.Portfolio)
                .FirstOrDefaultAsync();

            if (UserModel == null)
                return LocalRedirect("/");

            return Page();
        }
    }
}