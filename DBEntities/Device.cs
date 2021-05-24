using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sample.Proto;

namespace DBEntities
{
    [Table("devices")]
    public class Device
    {
        [Required, Key]
        public long Id { get; set; }

        [Required, MaxLength(200)]
        public string Model { get; set; }

        [StringLength(5)]
        public string MCC { get; set; }

        [Required, MaxLength(80)]
        public string OsVersion { get; set; }

        [Required]
        public OsTypes OsType { get; set; }

        [Required, MaxLength(100)]
        public string DeviceId { get; set; }

        [Required, MaxLength(200)]
        public string PushToken { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
