﻿using StudentWebPortfolio.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentWebPortfolio.Business
{
    public interface ISkillQueries
    {
        IQueryable<Skill> All();
    }
}
