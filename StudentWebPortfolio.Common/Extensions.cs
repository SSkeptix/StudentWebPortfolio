using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace StudentWebPortfolio.Common
{
    public static class MapperExtension
    {
        public static TDestination MapTo<TDestination>(this object source)
            where TDestination : class, new()
            => Mapper.Map<TDestination>(source);

        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
            where TSource : class, new()
            where TDestination : class, new()
            => Mapper.Map<TSource, TDestination>(source, destination);

        public static IEnumerable<TDestination> MapTo<TDestination>(this IEnumerable<object> source) 
            where TDestination : class, new()
            => source.Select(_ => _.MapTo<TDestination>());

        public static IQueryable<TDestination> MapOrSelect<TDestination, TSourse>(this IQueryable<TSourse> query, Expression<Func<TSourse, TDestination>> select = null)
            => select != null ? query.Select(select) : query.ProjectTo<TDestination>();
    }

    public static class CommonExtenstions
    {
        public static void ForEach<T>(this IEnumerable<T> elements, Action<T> action)
        {
            foreach (var element in elements)
                action(element);
        }
    }
}
