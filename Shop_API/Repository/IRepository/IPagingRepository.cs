using Shop_Models.Dto;

namespace Shop_API.Repository.IRepository
{
    public interface IPagingRepository   /* public interface IPagingRepository<T> where T : class*/
    {
       

        List<PagingDto> GetAll(string search, double? from, double? to, string sortBy, int page);
		List<PagingDto> GetAllColor(string? search, double? from, double? to, string? sortBy, int page);
		List<PagingDto> GetAllRam(string? search, double? from, double? to, string? sortBy, int page);
	}
}
