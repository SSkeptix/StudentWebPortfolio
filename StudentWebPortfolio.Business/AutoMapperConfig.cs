using AutoMapper;
using StudentWebPortfolio.Common;
using StudentWebPortfolio.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace StudentWebPortfolio.Business
{
    public static class AutoMapperConfig
    {
        public static void Initialize(IMapperConfigurationExpression cfg)
        {
            // Register domain entities to ifself, need to avoid bug with project to itself.
            AppDomain.CurrentDomain.GetAssemblies()
                .First(_ => _.FullName.Contains("StudentWebPortfolio.Data"))
                .GetTypes()
                .Where(_ => _.Namespace == "StudentWebPortfolio.Data.Entities" && !_.IsInterface)
                .ForEach(_ => cfg.CreateMap(_, _));
        }
    }
}
