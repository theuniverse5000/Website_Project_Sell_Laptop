﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop_Models.Entities
{
    [Table("Role")]
    public class Role
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string? RoleName { get; set; }
        public string? Descripion { get; set; }
        public int Status { get; set; }
        public virtual ICollection<User>? User { get; set; }
    }
}
