using Shop_Models.Entities;

namespace Shop_Models.Dto
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string Provider { get; set; }
        public string Password { get; set; }
        public bool isAdmin { get; set; }

    }
}
