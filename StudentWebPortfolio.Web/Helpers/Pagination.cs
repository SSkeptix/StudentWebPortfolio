using JW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentWebPortfolio.Web.Helpers
{
    public class Pagination
    {
        public Pager Pager { get; set; }
        public string BaseUrl { get; set; }
        public string PageTag { get; set; }
        public string OnClick { get; set; }

        public static Pagination SimplePagination(int totalItems, int currentPage, string baseUrl, string pageTage)
        {
            var pagination = new Pagination();
            pagination.Pager = new Pager(totalItems, currentPage);
            pagination.BaseUrl = baseUrl;
            pagination.PageTag = pageTage;
            return pagination;
        }

        public static Pagination SimplePagination(int totalItems, int currentPage, string onClick)
        {
            var pagination = new Pagination();
            pagination.Pager = new Pager(totalItems, currentPage);
            pagination.OnClick = onClick;
            return pagination;
        }
    }
}
