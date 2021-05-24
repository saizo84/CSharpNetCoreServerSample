using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sample.Proto;

namespace DBEntities
{
    [Table("platform_accounts"), Index(nameof(UserId), nameof(PlatformType), nameof(PlatformId), IsUnique = true)]
    public class PlatformAccount
    {
        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public PlatformTypes PlatformType { get; set; }

        [Required, MaxLength(100)]
        public string PlatformId { get; set; }

        public long? UserId { get; set; }

        [Required, ForeignKey("UserId")]
        public User User { get; set; }

        public long PlatformAccountAuthorizationId { get; set; }
        [Required, ForeignKey("PlatformAccountAuthorizationId")]
        public PlatformAccountAuthorization PlatformAccountAuthorization { get; set; }
    }
}
