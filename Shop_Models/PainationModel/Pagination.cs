using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.HelpperModel
{
    public class Pagination<T> : IPagination
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int PageSize { get; set; }

        public int NumberOfRecords { get; set; }

        public int TotalRecords { get; set; }

        public IEnumerable<T> Content { get; set; }

        public Pagination()
        {
            PageSize = 20;
            CurrentPage = 1;
        }

        public Pagination(int currentPage, int pageSize)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
        }

        public Pagination(int totalRecords, int currentPage, int pageSize)
        {
            TotalRecords = totalRecords;
            CurrentPage = currentPage;
            PageSize = pageSize;
            NumberOfRecords = totalRecords;
            double a = (double)TotalRecords / (double)pageSize;
            TotalPages = (int)Math.Ceiling(a);
        }

        public Pagination(IEnumerable<T> content, int totalRecords, int currentPage, int pageSize)
        {
            Content = content;
            TotalRecords = totalRecords;
            CurrentPage = currentPage;
            PageSize = pageSize;
            NumberOfRecords = totalRecords;
            double a = (double)TotalRecords / (double)pageSize;
            TotalPages = (int)Math.Ceiling(a);
        }
    }
}
