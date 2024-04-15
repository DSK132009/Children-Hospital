using CMS.Search;
using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using MVC.Models.FormComponents.CustomLabel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models.Search
{
    public class SearchResultModel
    {
        public string Query { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int StartingPage { get; set; }
        public int EndingPage { get; set; }
        public int TotalResults { get; set; }
        public IEnumerable<SearchResultItem> Items { get; set; }
        public string PopularSearches { get; set; }
    }
}
