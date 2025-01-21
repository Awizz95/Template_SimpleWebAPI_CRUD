using Microsoft.AspNetCore.Identity;
using Template_SimpleWebAPI_CRUD.Helpers.Enums;

namespace Template_SimpleWebAPI_CRUD.Models
{
    public class AppUser : IdentityUser
    {
        public Role Role { get; set; }
    }
}
