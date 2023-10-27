using Shop_API.HelpperModel;
using Shop_API.HelpperModel;
using Shop_Models.HelpperModel;

namespace Shop_API.RequestModel
{
    public class ProductDetailQueryModel : PaginationRequest
    {
        public double? FromPrice { get; set; }
        public double? ToPrice { get; set;}
    }
}
