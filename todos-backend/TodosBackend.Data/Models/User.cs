using System.ComponentModel.DataAnnotations.Schema;

namespace TodosBackend.Data.Models
{
    [Table("users")]
    public class User : BaseModel
    {
        [Column("email")]
        public string Email { get; set; }
        [Column("username")]
        public string UserName { get; set; }
        [Column("password_hash")]
        public string PasswordHash { get; set; }
        [Column("role")]
        public string Role { get; set; } = String.Empty;
        [Column("refresh_token")]
        public string RefreshToken { get; set; } = String.Empty;
        [Column("token_created")]
        public DateTime TokenCreated { get; set; }
        [Column("token_expires")]
        public DateTime TokenExpires { get; set; }
    }
}
