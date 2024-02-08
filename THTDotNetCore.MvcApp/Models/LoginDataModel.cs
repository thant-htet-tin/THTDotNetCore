using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace THTDotNetCore.MvcApp.Models
{

    [Table("Tbl_Login")]
    public class LoginDataModel
    {

        [Key]
        public int UserId { get; set; }
        public string? UserName { get; set; }

        public required string Email { get; set; }
        public required string Password { get; set; }

        public string? FullName { get; set; }
        public string? Role { get; set; }
    }
}
