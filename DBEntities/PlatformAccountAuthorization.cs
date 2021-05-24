using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBEntities
{
    [Table("platform_account_authorizations")]
    public class PlatformAccountAuthorization
    {
        [Key]
        public long Id { get; set; }

        [Required, ForeignKey("PlatformAccount")]
        public long PlatformAccountId { get; set; }

        [Required, MaxLength(100)]
        public string Password { get; set; }
    }
}
