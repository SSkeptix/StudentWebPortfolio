using System;
using System.Collections.Generic;
using System.Text;

namespace StudentWebPortfolio.Business
{
    public interface ICommandContainer
    {
        IPortfolioCommands Portfolios { get; }
        IUserSkillCommands UserSkills { get; }
    }
}
