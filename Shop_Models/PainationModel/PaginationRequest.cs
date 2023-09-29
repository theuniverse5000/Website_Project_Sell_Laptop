using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.HelpperModel
{
    public class PaginationRequest
    {
        public virtual string Sort { get; set; } = string.Empty;


        [Range(1, int.MaxValue)]
        public int? CurrentPage { get; set; } = 1;


        [Range(1, int.MaxValue)]
        public int? PageSize { get; set; } = 20;


        public string? Filter { get; set; } = "{}";


        public string? FullTextSearch { get; set; }

        public IEnumerable<string>? ListTextSearch { get; set; }
    }
}
