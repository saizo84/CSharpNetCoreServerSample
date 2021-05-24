using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBEntities
{
    [Table("users")]
    public class User
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public DateTime CreatedTime { get; set; } = DateTime.UtcNow;

        [Required]
        public string Nickname { get; set; } = String.Empty;

        [Required]
        public int AccountId { get; set; }

        [Required]
        public int ServerId { get; set; } = 1;

        public List<Device> Devices { get; set; } = new();
        public List<PlatformAccount> PlatformAccounts { get; set; } = new();
    }
}