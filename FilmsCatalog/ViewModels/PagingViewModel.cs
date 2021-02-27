

using System;

namespace FilmsCatalog.ViewModels
{
    public class PagingViewModel
    {
        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int PerPage { get; set; }

        public PagingViewModel(int totalItems, int currentPage, int perPage)
        {
            CurrentPage = currentPage > 0 ? currentPage : 1;
            TotalItems = totalItems;
            TotalPages = (int) Math.Ceiling(totalItems / (double)perPage);
            PerPage = perPage;
        }

        public bool HasPreviousPage => (CurrentPage > 1);
        public bool HasNextPage => (CurrentPage < TotalPages);
    }
}
