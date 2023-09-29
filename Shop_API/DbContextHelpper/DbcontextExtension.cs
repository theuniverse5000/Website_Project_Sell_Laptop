using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Shop_Models.HelpperModel;
namespace Shop_API.HelpperModel
{
    public static class DbcontextExtension
    {
        public static async Task<Pagination<T>> GetPagedAsync<T>(this IQueryable<T> query, int currentPage, int pageSize) where T : class
        {
            var totalRecord =  query.Count();
            Pagination<T> result = new Pagination<T>(totalRecord, currentPage, pageSize);
            int count = (currentPage - 1) * pageSize;
            result.Content =  query.Skip(count).Take(pageSize).ToList();
            return result;
        }

    }
}
