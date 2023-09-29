using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.HelpperModel
{
    public interface IPagination
    {
        int CurrentPage { get; set; }

        int TotalPages { get; set; }

        int PageSize { get; set; }

        int NumberOfRecords { get; set; }

        int TotalRecords { get; set; }
    }
}
