using AutoMapper;
using StudentWebPortfolio.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentWebPortfolio.Business
{
    public static class AutoMapperConfig
    {
        public static void Initialize(IMapperConfigurationExpression cfg)
        {
            //cfg.CreateMap<User, User>(MemberList.None);
        }
    }
}
