using Newtonsoft.Json;
using Shop_Models.Dto;

namespace WebApp.Services
{
    public static class SessionService
    {
        // 1 Đọc dl từ session =>  trả về 1 list
        public static List<CartItemDto> GetObjFromSession(ISession session, string key)
        {
            string jsonData = session.GetString(key);
            if (jsonData == null)
            {

                return new List<CartItemDto>();

            }
            else
            {
                var CartItemDtos = JsonConvert.DeserializeObject<List<CartItemDto>>(jsonData);
                return CartItemDtos;
            }
        }
        // 2. Ghi đè dữ liệu vào session từ 1 list
        public static void SetObjToSession(ISession session, string key, object data)
        {
            var jsonData = JsonConvert.SerializeObject(data);// chuyển đổi dữ liệu jsonData
            session.SetString(key, jsonData);// Ghi đè vào Session
        }
        // 3. Kiểm tra xem đối tượng có nằm trong 1 list hay không
        public static bool CheckObjInList(string code, List<CartItemDto> CartItemDtos)
        {
            return CartItemDtos.Any(x => x.MaProductDetail == code);
            // Any : một trong những , All : Tất cả
        }

        public static List<ProductDetailDto> GetFromSession(ISession session, string key)
        {
            string jsonData = session.GetString(key);
            if (jsonData == null)
            {

                return new List<ProductDetailDto>();
            }
            else
            {

                var ProductDetailViews = JsonConvert.DeserializeObject<List<ProductDetailDto>>(jsonData);
                return ProductDetailViews;
            }
        }
        // 2. Ghi đè dữ liệu vào session từ 1 list
        public static void SetToSession(ISession session, string key, object data)
        {
            var jsonData = JsonConvert.SerializeObject(data);// chuyển đổi dữ liệu jsonData
            session.SetString(key, jsonData);// Ghi đè vào Session
        }
    }
}
