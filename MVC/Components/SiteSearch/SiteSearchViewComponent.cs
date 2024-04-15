using CMS.Helpers;
using CMS.Membership;
using CMS.Search;
using Kentico.PageBuilder.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.Search;
using MVC.Models.Widgets.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Components.Search
{
    public class SiteSearchViewComponent : ViewComponent
    {
        public static readonly string[] searchIndex = new string[] { "SiteSearch" };

        // Sets the limit of items per page of search results
        private const int PAGE_SIZE = 10;

        public IViewComponentResult Invoke(string searchText, int categoryInt, int pageNumber = 1, string popularSearches = "")
        {
            // Displays the search page without any search results if the query is empty
            if (String.IsNullOrWhiteSpace(searchText) && categoryInt == 0)
            {
                // Creates a model representing empty search results
                SearchResultModel emptyModel = new SearchResultModel
                {
                    CurrentPage = -1,
                    TotalPages = 0,
                    TotalResults = 0,
                    Items = new List<SearchResultItem>(),
                    Query = String.Empty,
                    PopularSearches = popularSearches
                };

                return View(emptyModel);
            }

            pageNumber = pageNumber < 1 ? 1 : pageNumber;
            int startPage;
            int endPage;

            //var fuzzySearchTerm = String.IsNullOrEmpty(SearchSyntaxHelper.TransformToFuzzySearch(searchText)) ? searchText : SearchSyntaxHelper.TransformToFuzzySearch(searchText);

            if (!string.IsNullOrWhiteSpace(searchText) && categoryInt != 0)
            {
                searchText += " AND ";
            }

            if (categoryInt != 0)
            {
                searchText += "documentcategoryids:" + categoryInt.ToString();
            }

            var pattern = new SearchPattern(searchText, SearchOptionsEnum.FullSearch);

            // Searches the specified index and gets the matching results
            SearchParameters searchParameters = SearchParameters.PrepareForPages(pattern, searchIndex, pageNumber, PAGE_SIZE, MembershipContext.AuthenticatedUser, "en-us", true);
            SearchResult searchResult = SearchHelper.Search(searchParameters);

            var totalPages = ValidationHelper.GetInteger(Math.Ceiling(searchResult.TotalNumberOfResults / (float)PAGE_SIZE), 1);

            pageNumber = pageNumber > totalPages ? totalPages : pageNumber;

            startPage = (pageNumber - 4 < 1) ? 1 : pageNumber - 4;
            endPage = (startPage + 8 > totalPages) ? totalPages : startPage + 8;

            if (totalPages > 9 && endPage - startPage < 9)
            {
                startPage = endPage - 8;
            }


            // Creates a model with the search result items
            SearchResultModel model = new SearchResultModel
            {
                CurrentPage = pageNumber,
                StartingPage = startPage,
                EndingPage = endPage,
                TotalResults = searchResult.TotalNumberOfResults,
                Items = searchResult.Items,
                Query = searchText,
                TotalPages = totalPages,
                PopularSearches = popularSearches
            };

            return View(model);
        }
    }
}
