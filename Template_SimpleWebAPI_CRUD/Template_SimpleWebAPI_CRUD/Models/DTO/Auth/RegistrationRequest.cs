using Template_SimpleWebAPI_CRUD.Helpers.Enums;

namespace Template_SimpleWebAPI_CRUD.Models.DTO.Auth
{
    public class RegistrationRequest
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required Role Role { get; set; }
    }
}